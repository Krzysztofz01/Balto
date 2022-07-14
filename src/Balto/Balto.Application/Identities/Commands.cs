using Balto.Application.Abstraction;
using Balto.Domain.Identities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Identities
{
    public static class Commands
    {
        public static class V1
        {
            public class Delete : IApplicationCommand<Identity>
            {
                public Guid Id { get; set; }
            }

            public class Update : IApplicationCommand<Identity>
            {
                public Guid Id { get; set; }

                public string Color { get; set; }
            }

            public class Activation : IApplicationCommand<Identity>
            {
                public Guid Id { get; set; }

                public bool? Activated { get; set; }
            }

            public class RoleChange : IApplicationCommand<Identity>
            {
                public Guid Id { get; set; }

                public UserRole? Role { get; set; }
            }

            [Obsolete]
            public class TeamChange : IApplicationCommand<Identity>
            {
                public Guid Id { get; set; }

                public Guid? TeamId { get; set; }
            }
        }
    }
}
