using Balto.Application.Abstraction;
using Balto.Domain.Tags;
using System;

namespace Balto.Application.Tags
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Tag>
            {
                public string Title { get; set; }

                public string Color { get; set; }
            }

            public class Delete : IApplicationCommand<Tag>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
