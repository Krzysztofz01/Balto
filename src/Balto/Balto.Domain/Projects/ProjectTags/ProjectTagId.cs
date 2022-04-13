using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Projects.ProjectTags
{
    public class ProjectTagId : Identifier
    {
        private ProjectTagId() { }
        private ProjectTagId(Guid value) : base(value) { }

        public static implicit operator string(ProjectTagId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(ProjectTagId contributorId) => contributorId.Value;

        public static ProjectTagId FromGuid(Guid guid) => new ProjectTagId(guid);
        public static ProjectTagId FromString(string guid) => new ProjectTagId(Guid.Parse(guid));
    }
}
