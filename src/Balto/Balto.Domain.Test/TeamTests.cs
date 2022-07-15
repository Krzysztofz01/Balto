using Balto.Domain.Core.Exceptions;
using Balto.Domain.Teams;
using System;
using Xunit;

namespace Balto.Domain.Test
{
    public class TeamTests
    {
        [Fact]
        public void TeamShouldCreate()
        {
            Team.Factory.Create(new Events.V1.TeamCreated
            {
                Color = "#123ABC",
                Name = "Team name"
            });
        }

        [Fact]
        public void TeamShouldThrowForInvalidColor()
        {
            Assert.Throws<ValueObjectValidationException>(() =>
            {
                Team.Factory.Create(new Events.V1.TeamCreated
                {
                    Color = "@QQQZZZ",
                    Name = "Team name"
                });
            });
        }

        [Fact]
        public void TeamShouldDelete()
        {
            var team = Team.Factory.Create(new Events.V1.TeamCreated
            {
                Color = "#123ABC",
                Name = "Team name"
            });

            team.Apply(new Events.V1.TeamDeleted
            {
                Id = team.Id
            });

            Assert.NotNull(team.DeletedAt);
        }

        [Fact]
        public void TeamShouldAddMember()
        {
            var team = Team.Factory.Create(new Events.V1.TeamCreated
            {
                Color = "#123ABC",
                Name = "Team name"
            });

            Assert.Empty(team.Members);

            var userId = Guid.NewGuid();

            team.Apply(new Events.V1.TeamMemberAdded
            {
                Id = team.Id,
                IdentityId = userId
            });

            Assert.NotEmpty(team.Members);
        }

        [Fact]
        public void TeamShoulThrowOndAddExistingMember()
        {
            var team = Team.Factory.Create(new Events.V1.TeamCreated
            {
                Color = "#123ABC",
                Name = "Team name"
            });

            Assert.Empty(team.Members);

            var userId = Guid.NewGuid();

            team.Apply(new Events.V1.TeamMemberAdded
            {
                Id = team.Id,
                IdentityId = userId
            });

            Assert.NotEmpty(team.Members);

            Assert.Throws<BusinessLogicException>(() =>
            {
                team.Apply(new Events.V1.TeamMemberAdded
                {
                    Id = team.Id,
                    IdentityId = userId
                });
            });
        }

        [Fact]
        public void TeamShouldDeleteMember()
        {
            var team = Team.Factory.Create(new Events.V1.TeamCreated
            {
                Color = "#123ABC",
                Name = "Team name"
            });

            Assert.Empty(team.Members);

            var userId = Guid.NewGuid();

            team.Apply(new Events.V1.TeamMemberAdded
            {
                Id = team.Id,
                IdentityId = userId
            });

            Assert.NotEmpty(team.Members);

            team.Apply(new Events.V1.TeamMemberDeleted
            {
                Id = team.Id,
                IdentityId = userId
            });

            Assert.Empty(team.Members);
        }
    }
}
