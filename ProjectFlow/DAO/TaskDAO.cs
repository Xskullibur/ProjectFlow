using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BusinessLogic
{
    public class TaskLogic
    {

        private List<Task> getTasksByProjectID(string projID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.FirstOrDefault(x => x.projectID == projID)
                    .Tasks.ToList();
            }
        }

    }
}