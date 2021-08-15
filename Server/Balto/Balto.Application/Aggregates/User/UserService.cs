using Balto.Domain.Aggregates.User;
using Balto.Domain.Common;
using Balto.Infrastructure.Authentication;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.User.Contracts;

namespace Balto.Application.Aggregates.User
{
    public class UserService : IApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationHelperService _authenticationHelperService;
        private readonly IRequestContext _requestContext;

        public UserService(
            IUserRepository userRepository,
            IAuthenticationRepository authenticationRepository,
            IUnitOfWork unitOfWork,
            IAuthenticationHelperService authenticationHelperService,
            IRequestContext requestContext)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _authenticationRepository = authenticationRepository ??
                throw new ArgumentNullException(nameof(authenticationRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _authenticationHelperService = authenticationHelperService ??
                throw new ArgumentNullException(nameof(authenticationHelperService));

            _requestContext = requestContext ??
                throw new ArgumentNullException(nameof(requestContext));
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.UserDelete cmd: 
                    await HandleUpdate(cmd.TargetUserId, c => c.Delete()); 
                    break;

                case V1.UserActivate cmd:
                    await HandleUpdate(cmd.TargetUserId, c => c.Activate());
                    break;

                case V1.UserTeamUpdate cmd:
                    await HandleUpdate(cmd.TargetUserId, c => c.SetTeam(cmd.TeamId));
                    break;

                case V1.UserColorUpdate cmd:
                    await HandleUpdate(cmd.TargetUserId, c => c.SetColor(cmd.Color));
                    break;

                case V1.UserLeaderStatusUpdate cmd:
                    await HandleUpdate(cmd.TargetUserId, c => c.PromoteToLeader());
                    break;
            }
        }

        private async Task HandleUpdate(Guid userId, Action<Domain.Aggregates.User.User> operation)
        {
            var user = await _userRepository.Load(userId.ToString());
            if (user is null) throw new InvalidOperationException($"User with given id not found.");

            operation(user);

            await _unitOfWork.Commit();
        }

        public async Task<Authentication.Contracts.V1.AuthResponse> HandleWithResponseV1(object command)
        {

            switch(command)
            {
                case V1.UserLogin cmd: 
                    return await HandleUserLoginV1(cmd);

                case V1.UserRefresh cmd: 
                    return await HandleUserRefreshV1(cmd);    
            }

            throw new InvalidOperationException($"{command.GetType()} - This handler can not process this command.");
        }

        private async Task<Authentication.Contracts.V1.AuthResponse> HandleUserLoginV1(V1.UserLogin cmd)
        {
            var user = await _authenticationRepository.GetUserByEmail(cmd.Email);

            if (!_authenticationHelperService.VerifyPasswordHash(cmd.Password, user.Password))
                throw new ArgumentException("Invalid authentication credentials.");

            user.Authenticate(_requestContext.UserGetIpAddress());

            string refreshToken = user.RefreshTokens.Last().Token;

            string jwtToken = _authenticationHelperService.GenerateJsonWebToken(user);

            await _unitOfWork.Commit();

            return new Authentication.Contracts.V1.AuthResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }

        private async Task<Authentication.Contracts.V1.AuthResponse> HandleUserRefreshV1(V1.UserRefresh cmd)
        {
            var user = await _authenticationRepository.GetUserByRefreshToken(cmd.Token);

            user.TokenRefresh(_requestContext.UserGetIpAddress(), cmd.Token);

            string refreshToken = user.RefreshTokens.Last().Token;

            string jwtToken = _authenticationHelperService.GenerateJsonWebToken(user);

            await _unitOfWork.Commit();

            return new Authentication.Contracts.V1.AuthResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }

    }
}
