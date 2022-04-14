using Balto.Application.Abstraction;
using Balto.Domain.Tags;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Tags
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Tag>
            {
                [Required]
                public string Title { get; set; }

                [Required]
                public string Color { get; set; }
            }

            public class Delete : IApplicationCommand<Tag>
            {
                [Required]
                public Guid Id { get; set; }
            }
        }
    }
}
