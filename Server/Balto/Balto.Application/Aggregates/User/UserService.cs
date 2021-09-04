using Balto.Domain.Aggregates.User;
using Balto.Domain.Common;
using System;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.User.Commands;

namespace Balto.Application.Aggregates.User
{
    public class UserService : IApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
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
            if (user is null) throw new InvalidOperationException($"User with given id: { userId } not found.");

            operation(user);

            await _unitOfWork.Commit();
        }
    }
}
