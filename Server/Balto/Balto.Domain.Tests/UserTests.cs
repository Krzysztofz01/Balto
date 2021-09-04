using Balto.Domain.Aggregates.User;
using Balto.Domain.Exceptions;
using System;
using Xunit;

namespace Balto.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void UserCanBeCreated()
        {
            var user = MockupUser();

            Assert.NotNull(user);
        }

        [Fact]
        public void UserCanBeDeleted()
        {
            var user = MockupUser();

            user.Delete();
        }

        [Fact]
        public void UserCanAuthenticate()
        {
            var user = MockupUser();

            Assert.Throws<InvalidEntityStateException>(() => user.Authenticate("127.0.0.1"));

            user.Activate();

            user.Authenticate("127.0.0.1");
        }

        [Fact]
        public void UserActivationStatusCanBeChanged()
        {
            var user = MockupUser();

            user.Activate();
        }

        [Fact]
        public void UserCanRefreshToken()
        {
            var user = MockupUser();

            user.Activate();

            user.Authenticate("127.0.0.1");

            var token = user.GetLatestRefreshToken();

            user.TokenRefresh(token.Token, "127.0.0.1");

            var token2 = user.GetLatestRefreshToken();

            int expectedRefreshTokenCount = 2;
            Assert.Equal(user.RefreshTokens.Count, expectedRefreshTokenCount);

            user.Activate();

            Assert.Throws<InvalidEntityStateException>(() => user.TokenRefresh(token2.Token, "127.0.0.1"));
        }

        [Fact]
        public void UserCanRevokeToken()
        {
            var user = MockupUser();

            user.Activate();

            user.Authenticate("127.0.0.1");

            var token = user.GetLatestRefreshToken();

            user.TokenRevoke(token.Token, "127.0.0.1");
        }

        [Fact]
        public void UserCanChangePassword()
        {
            var user = MockupUser();

            Assert.Throws<InvalidEntityStateException>(() => user.ChangePassword("hash_placeholder"));

            user.Activate();

            user.ChangePassword("hash_placeholder");
        }

        [Fact]
        public void UserTeamCanBeChanged()
        {
            var user = MockupUser();

            user.SetTeam(Guid.NewGuid());
        }

        [Fact]
        public void UserColorCanBeChanged()
        {
            var user = MockupUser();

            user.SetColor("#121212");
        }

        [Fact]
        public void UserLeaderStatusCanBeChanged()
        {
            var user = MockupUser();

            user.PromoteToLeader();
        }

        private User MockupUser()
        {
            return User.Factory.Create(
                UserName.FromString("Jan Kowalski"),
                UserEmail.FromString("jan@kowalski.com"),
                UserPassword.FromHash("1q2w3e4razsxdc"),
                "127.0.0.1");
        }
    }
}
