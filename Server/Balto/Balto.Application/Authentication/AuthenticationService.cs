using Balto.Domain.Aggregates.User;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Balto.Application.Authentication.Commands;

namespace Balto.Application.Authentication
{
    public class AuthenticationService : IApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationHandler _authenticationHandler;
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;

        public AuthenticationService(
            IUserRepository userRepository,
            IAuthenticationRepository authenticationRepository,
            IUnitOfWork unitOfWork,
            IAuthenticationHandler authenticationHandler,
            IRequestAuthorizationHandler requestAuthorizationHandler
            )
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _authenticationRepository = authenticationRepository ??
                throw new ArgumentNullException(nameof(authenticationRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _authenticationHandler = authenticationHandler ??
                throw new ArgumentNullException(nameof(authenticationHandler));

            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));
        }

        public async Task Handle(object command)
        {
            switch (command)
            {
                case V1.UserLogout cmd:
                    await HandleUpdate(_requestAuthorizationHandler.GetUserGuid(), c =>
                        c.TokenRevoke(cmd.Token, _requestAuthorizationHandler.GetIpAddress()));
                    break;

                case V1.UserRegister cmd:
                    await HandleRegisterV1(cmd);
                    break;

                case V1.UserResetPassword cmd:
                    if (!_authenticationHandler.CheckIfPasswordsMatch(cmd.Password, cmd.PasswordRepeat))
                        throw new ArgumentException("Invalid authentication credentials.");

                    string password = _authenticationHandler.HashPassword(cmd.Password);

                    await HandleUpdate(_requestAuthorizationHandler.GetUserGuid(), c => c.ChangePassword(password));
                    break;
            }
        }

        public async Task<V1.AuthResponse> HandleWithResponse(object command)
        {
            switch (command)
            {
                case V1.UserLogin cmd:
                    return await HandleUserLoginV1(cmd);

                case V1.UserRefresh cmd:
                    return await HandleUserRefreshV1(cmd);
            }

            throw new InvalidOperationException($"{command.GetType()} - This handler can not process this command.");
        }

        private async Task HandleUpdate(Guid userId, Action<User> operation)
        {
            var user = await _userRepository.Load(userId.ToString());
            if (user is null) throw new InvalidOperationException($"User with given id not found.");

            operation(user);

            await _unitOfWork.Commit();
        }

        private async Task HandleRegisterV1(V1.UserRegister cmd)
        {
            if (await _authenticationRepository.UserWithEmailExists(cmd.Email))
                throw new InvalidOperationException("User with given email already exists.");

            if (!_authenticationHandler.CheckIfPasswordsMatch(cmd.Password, cmd.PasswordRepate))
                throw new ArgumentException("Invalid authentication credentials.");

            string password = _authenticationHandler.HashPassword(cmd.Password);


            var user = User.Factory.Create(
                UserName.FromString(cmd.Name),
                UserEmail.FromString(cmd.Email),
                UserPassword.FromHash(password));

            await _userRepository.Add(user);

            await _unitOfWork.Commit();
        }

        private async Task<V1.AuthResponse> HandleUserLoginV1(V1.UserLogin cmd)
        {
            var user = await _authenticationRepository.GetUserByEmail(cmd.Email);

            if (!_authenticationHandler.VerifyPasswordHash(cmd.Password, user.Password))
                throw new ArgumentException("Invalid authentication credentials.");

            user.Authenticate(_requestAuthorizationHandler.GetIpAddress());

            string refreshToken = user.RefreshTokens.Last().Token;

            string jwtToken = _authenticationHandler.GenerateJsonWebToken(user);

            await _unitOfWork.Commit();

            return new V1.AuthResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }

        private async Task<V1.AuthResponse> HandleUserRefreshV1(V1.UserRefresh cmd)
        {
            var user = await _authenticationRepository.GetUserByRefreshToken(cmd.Token);

            user.TokenRefresh(_requestAuthorizationHandler.GetIpAddress(), cmd.Token);

            string refreshToken = user.RefreshTokens.Last().Token;

            string jwtToken = _authenticationHandler.GenerateJsonWebToken(user);

            await _unitOfWork.Commit();

            return new V1.AuthResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
