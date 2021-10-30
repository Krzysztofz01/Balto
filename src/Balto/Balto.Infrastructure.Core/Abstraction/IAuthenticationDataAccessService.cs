﻿using Balto.Domain.Identities;
using System.Threading.Tasks;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IAuthenticationDataAccessService
    {
        Task<Identity> GetUserByEmail(string email);
        Task<Identity> GetUserByRefreshToken(string refreshToken);
        Task<bool> UserWithEmailExsits(string email);
    }
}
