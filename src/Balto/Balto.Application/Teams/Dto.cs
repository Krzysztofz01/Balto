using System;
using System.Collections.Generic;

namespace Balto.Application.Teams
{
    public static class Dto
    {
        public class TeamSimple
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }

        public class TeamDetails
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public IEnumerable<MemberDetails> Members { get; set; }
        }

        public class MemberDetails
        {
            public Guid Id { get; set; }
        }
    }
}
