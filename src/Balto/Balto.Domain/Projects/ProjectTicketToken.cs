using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using System;
using System.Security.Cryptography;

namespace Balto.Domain.Projects
{
    public class ProjectTicketToken : ValueObject<ProjectTicketToken>
    {
        private const int _ticketTokenLength = 64;

        public string Value { get; private set; }

        private ProjectTicketToken() { }
        private ProjectTicketToken(string value)
        {
            Value = value;
        }

        public static implicit operator bool(ProjectTicketToken ticketToken) => !ticketToken.Value.IsEmpty();
        public static implicit operator string(ProjectTicketToken ticketToken) => ticketToken.Value;

        private static string GenerateTicketToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(_ticketTokenLength);

            return Convert.ToBase64String(randomBytes);
        }

        public static ProjectTicketToken Enable => new ProjectTicketToken(GenerateTicketToken());
        public static ProjectTicketToken Disable => new ProjectTicketToken(null);
    }
}
