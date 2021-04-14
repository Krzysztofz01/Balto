using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Balto.Service
{
    class UserService : IUserService
    {
        private readonly JWTSettings jwtSettings;
        private readonly IUserRepository userRepository;

        public UserService(
            IOptions<JWTSettings> jwtSettings,
            IUserRepository userRepository)
        {
            this.jwtSettings = jwtSettings.Value ??
                throw new ArgumentNullException(nameof(jwtSettings));

            this.userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public Task<UserDto> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
