using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Projects
{
    public class ProjectOwnerId : Identifier
    {
        private ProjectOwnerId() { }
        private ProjectOwnerId(Guid value) : base(value) { }

        public static implicit operator string(ProjectOwnerId ownerId) => ownerId.Value.ToString();
        public static implicit operator Guid(ProjectOwnerId ownerId) => ownerId.Value;

        public static ProjectOwnerId FromGuid(Guid guid) => new ProjectOwnerId(guid);
        public static ProjectOwnerId FromString(string guid) => new ProjectOwnerId(Guid.Parse(guid));
    }
}
