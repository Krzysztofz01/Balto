using Balto.Domain.Core.Events;
using System;

namespace Balto.Domain.Team
{
    public static class Events
    {
        public static class V1
        {
            public class TeamCreated : IEventBase
            {
                public string Name { get; set; }
                public string Color { get; set; }
            }

            public class TeamDeleted : IEvent
            {
                public Guid Id { get; set; }
            }

            public class TeamMemberAdded : IEvent
            {
                public Guid Id { get; set; }
                public Guid IdentityId { get; set; }
            }

            public class TeamMemberDeleted : IEvent
            {
                public Guid Id { get; set; }
                public Guid IdentityId { get; set; }
            }
        }
    }
}
