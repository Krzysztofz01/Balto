using Balto.Application.Settings;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Balto.Application.Authentication
{
    public class AuthenticationWrapperService : IAuthenticationWrapperService
    {
        private readonly JsonWebTokenSettings _jwtSettings;

        public AuthenticationWrapperService(IOptions<JsonWebTokenSettings> jsonWebTokenSettings)
        {
            _jwtSettings = jsonWebTokenSettings.Value ??
                throw new ArgumentNullException(nameof(jsonWebTokenSettings));
        }

        public string GenerateJsonWebToken(Identity identity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, identity.Id.ToString()),
                new Claim(ClaimTypes.Email, identity.Email),
                new Claim(ClaimTypes.Name, identity.Name),
                new Claim(ClaimTypes.Role, identity.Role)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.TokenSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.BearerTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public string HashPassword(string contractPasswordString)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(5);

            return BCrypt.Net.BCrypt.HashPassword(contractPasswordString, salt);
        }

        public string HashString(string contractValue)
        {
            throw new NotImplementedException("Implement the SHA256");
        }

        public bool ValidatePasswords(string contractPasswordString, string contractPasswordStringRepeat)
        {
            return contractPasswordString == contractPasswordStringRepeat;
        }

        public bool VerifyPasswordHashes(string contractPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(contractPassword, storedPasswordHash);
        }

        public bool VerifyStringHashes(string contractValue, string storedValueHash)
        {
            throw new NotImplementedException("Implement the SHA256");
        }
    }
}
