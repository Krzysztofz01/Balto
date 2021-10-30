using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTask : Entity
    {
        //title, content, color, creatorid, assignedUserId, startingdate, deadline, finished, priority, ordinalnumber

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
