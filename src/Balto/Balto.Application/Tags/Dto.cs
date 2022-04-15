using System;

namespace Balto.Application.Tags
{
    public class Dto
    {
        public class TagSimple
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }

        public class TagDetails
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }
    }
}
