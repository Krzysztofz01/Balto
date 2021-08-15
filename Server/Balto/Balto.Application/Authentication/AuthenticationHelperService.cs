﻿using Balto.Application.Settings;
using Balto.Domain.Aggregates.User;
using Balto.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Balto.Application.Authentication
{
    public class AuthenticationHelperService : IAuthenticationHelperService
    {
        private readonly JsonWebTokenSettings _jsonWebTokenSettings;

        public AuthenticationHelperService(
            IOptions<JsonWebTokenSettings> jsonWebTokenSettings)
        {
            _jsonWebTokenSettings = jsonWebTokenSettings.Value ??
                throw new ArgumentNullException(nameof(jsonWebTokenSettings));
        }

        public bool CheckIfPasswordsMatch(string contractPassword, string contractPasswordRepeat)
        {
            return contractPassword == contractPasswordRepeat;
        }

        public string GenerateJsonWebToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, ParseRole(user)),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jsonWebTokenSettings.TokenSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_jsonWebTokenSettings.TokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string ParseRole(User user)
        {
            if (user.IsLeader) return "Leader";

            return "Default";
        }

        public string HashPassword(string contractPassword)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(5);

            return BCrypt.Net.BCrypt.HashPassword(contractPassword, salt);
        }

        public bool VerifyPasswordHash(string contractPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(contractPassword, passwordHash);
        }
    }
}
