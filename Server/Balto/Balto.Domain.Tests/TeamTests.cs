using Balto.Domain.Aggregates.Team;
using Xunit;

namespace Balto.Domain.Tests
{
    public class TeamTests
    {
        [Fact]
        public void TeamCanBeCreated()
        {
            string teamName = "My team";
            string teamColor = "#123ABC";

            var team = Team.Factory.Create(teamName, teamColor);

            Assert.Equal(teamName, team.Name.Value);
            Assert.Equal(teamColor, team.Color.Value);
        }

        [Fact]
        public void TeamCanBeUpdated()
        {
            string teamName = "My team";
            string teamColor = "#123ABC";

            var team = Team.Factory.Create(teamName, teamColor);

            Assert.Equal(teamName, team.Name.Value);
            Assert.Equal(teamColor, team.Color.Value);

            string newTeamName = "My new team";
            string newTeamColor = "#ABC123";

            team.Update(newTeamName, newTeamColor);

            Assert.Equal(newTeamName, team.Name.Value);
            Assert.Equal(newTeamColor, team.Color.Value);
        }

        [Fact]
        public void TeamCanBeDeleted()
        {
            string teamName = "My team";
            string teamColor = "#123ABC";

            var team = Team.Factory.Create(teamName, teamColor);

            team.Delete();

            Assert.NotNull(team.DeletedAt);
        }
    }
}
