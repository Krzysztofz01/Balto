using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Service.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class UserService : IUserService
    {
        private readonly JWTSettings jwtSettings;
        private readonly LeaderSettings leaderSettings;
        private readonly IUserRepository userRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;

        //Token expiration should be shorter but i set it fixed to one hour
        //before i implement refresh tokens
        private const int tokenExpiration = 60;

        public UserService(
            IOptions<JWTSettings> jwtSettings,
            IOptions<LeaderSettings> leaderSettings,
            IUserRepository userRepository,
            ITeamRepository teamRepository,
            IMapper mapper)
        {
            this.jwtSettings = jwtSettings.Value ??
                throw new ArgumentNullException(nameof(jwtSettings));

            this.leaderSettings = leaderSettings.Value ??
                throw new ArgumentNullException(nameof(leaderSettings));

            this.userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            this.teamRepository = teamRepository ??
                throw new ArgumentNullException(nameof(teamRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResult<string>> Authenticate(string email, string password, string ipAddress)
        {
            //Search for user with given email
            var user = await userRepository.SingleOrDefault(u => u.Email == email);
            if (user is null) return new ServiceResult<string>(ResultStatus.NotFound);

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

                return new ServiceResult<string>(token);
            }
            return new ServiceResult<string>(ResultStatus.NotPermited);
        }

        private string GenerateJsonWebToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Role, (user.IsLeader) ? "Leader" : "Deafult")
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

        public async Task<IServiceResult> Register(string email, string password, string ipAddress)
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

                //On register check if user with given email is set to be the leader
                if (leaderSettings.LeaderEmails.Any(x => x == email)) user.IsLeader = true;

                //Generate salt than hash and assign the password
                string salt = BCrypt.Net.BCrypt.GenerateSalt(5);
                user.Password = BCrypt.Net.BCrypt.HashPassword(password, salt);

                //Add new created user
                await userRepository.Add(user);
                if (await userRepository.Save() > 0) return new ServiceResult<string>(ResultStatus.Sucess);
                return new ServiceResult<string>(ResultStatus.Failed);
            }
            return new ServiceResult<string>(ResultStatus.Conflict);
        }

        public async Task<UserDto> GetUserFromPayload(IEnumerable<Claim> claims)
        {
            var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (idClaim is null) throw new ArgumentException(nameof(idClaim));

            var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim is null) throw new ArgumentException(nameof(emailClaim));

            var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            if (nameClaim is null) throw new ArgumentException(nameof(nameClaim));

            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim is null) throw new ArgumentException(nameof(roleClaim));

            return new UserDto()
            {
                Id = Convert.ToInt64(idClaim.Value),
                Email = emailClaim.Value,
                Name = nameClaim.Value,
                IsLeader = (roleClaim.Value == "Leader") ? true : false
            };
        }

        public async Task<long?> GetIdByEmail(string email)
        {
            var user = await userRepository.SingleOrDefault(x => x.Email == email);
            if (user is null) return null;

            return user.Id;
        }

        public async Task<ServiceResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = userRepository.GetAllUsers();
            var usersMapped = mapper.Map<IEnumerable<UserDto>>(users);

            return new ServiceResult<IEnumerable<UserDto>>(usersMapped);
        }

        public async Task<IServiceResult> UserSetTeam(long userId, long teamId, long leaderUserId)
        {
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult(ResultStatus.NotPermited);
            
            var team = await teamRepository.SingleTeam(teamId);
            if (team is null) return new ServiceResult(ResultStatus.NotFound);

            var user = await userRepository.GetSingleUser(userId);
            if (user is null) return new ServiceResult(ResultStatus.NotFound);

            if(user.TeamId != team.Id)
            {
                user.TeamId = team.Id;

                userRepository.UpdateState(user);
                if (await userRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
                return new ServiceResult(ResultStatus.Failed);
            }
            return new ServiceResult(ResultStatus.Sucess);
        }

        public async Task<IServiceResult> Reset(string email, string password)
        {
            //Search for user with given email
            var user = await userRepository.SingleOrDefault(u => u.Email == email);
            if (user is null) return new ServiceResult<string>(ResultStatus.NotFound);

            //Generate salt than hash and assign the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt(5);
            user.Password = BCrypt.Net.BCrypt.HashPassword(password, salt);

            //Update user data
            userRepository.UpdateState(user);
            if (await userRepository.Save() > 0) return new ServiceResult<string>(ResultStatus.Sucess); 
            return new ServiceResult<string>(ResultStatus.Failed);
        }

        public async Task<ServiceResult<UserDto>> Get(long userId)
        {
            var user = await userRepository.GetSingleUser(userId);
            if (user is null) return new ServiceResult<UserDto>(ResultStatus.NotFound);

            var userMapped = mapper.Map<UserDto>(user);
            return new ServiceResult<UserDto>(userMapped);
        }

        public async Task<IServiceResult> Delete(long userId)
        {
            var user = await userRepository.GetSingleUser(userId);
            if (user is null) return new ServiceResult<UserDto>(ResultStatus.NotFound);

            userRepository.Remove(user);

            if (await userRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }
    }
}
