using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskCreatorId : Identifier
    {
        private ProjectTaskCreatorId() : base() { }
        private ProjectTaskCreatorId(Guid value) : base(value) { }

        public static implicit operator string(ProjectTaskCreatorId ownerId) => ownerId.Value.ToString();
        public static implicit operator Guid(ProjectTaskCreatorId ownerId) => ownerId.Value;

        public static ProjectTaskCreatorId FromString(string guid) => new ProjectTaskCreatorId(Guid.Parse(guid));
        public static ProjectTaskCreatorId FromGuid(Guid guid) => new ProjectTaskCreatorId(guid);
    }
}
