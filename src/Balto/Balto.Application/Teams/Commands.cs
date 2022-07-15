using Balto.Application.Abstraction;
using Balto.Domain.Team;
using System;

namespace Balto.Application.Teams
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Team>
            {
                public string Name { get; set; }
                public string Color { get; set; }
            }

            public class Delete : IApplicationCommand<Team>
            {
                public Guid Id { get; set; }
            }

            public class AddMember : IApplicationCommand<Team>
            {
                public Guid Id { get; set; }
                public Guid UserId { get; set; }
            }

            public class DeleteMember : IApplicationCommand<Team>
            {
                public Guid Id { get; set; }
                public Guid UserId { get; set; }
            }
        }
    }
}
