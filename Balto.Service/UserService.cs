using Balto.Domain;
using Balto.Repository;
using Balto.Service.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class UserService : IUserService
    {
        private readonly JWTSettings jwtSettings;
        private readonly IUserRepository userRepository;

        //Token expiration should be shorter but i set it fixed to one hour
        //before i implement refresh tokens
        private const int tokenExpiration = 60;

        public UserService(
            IOptions<JWTSettings> jwtSettings,
            IUserRepository userRepository)
        {
            this.jwtSettings = jwtSettings.Value ??
                throw new ArgumentNullException(nameof(jwtSettings));

            this.userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<string> Authenticate(string email, string password, string ipAddress)
        {
            //Search for user with given email
            var user = await userRepository.SingleOrDefault(u => u.Email == email);
            if (user is null) return null;

            //Compare password with hash from database
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                //Update login control properties
                user.LastLoginDate = DateTime.Now;
                user.LastLoginIp = (ipAddress is null) ? "" : ipAddress;

                userRepository.UpdateState(user);
                await userRepository.Save();

                //Create and write JWToken
                string token = GenerateJsonWebToken(user);

                return token;
            }
            return null;
        }

        private string GenerateJsonWebToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(tokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(string email, string password, string ipAddress)
        {
            //Check if email is available
            var userByEmail = await userRepository.SingleOrDefault(u => u.Email == email);
            if (userByEmail is null)
            {
                var user = new User()
                {
                    Email = email,
                    LastLoginDate = DateTime.Now,
                    LastLoginIp = (ipAddress is null) ? "" : ipAddress,
                };

                //Generate salt than hash and assign the password
                string salt = BCrypt.Net.BCrypt.GenerateSalt(5);
                user.Password = BCrypt.Net.BCrypt.HashPassword(password, salt);

                //Add new created user
                await userRepository.Add(user);
                if (await userRepository.Save() > 0) return true;
            }
            return false;
        }
    }
}
