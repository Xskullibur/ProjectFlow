using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class ProjectTeamBLL
    {

        /// <summary>
        /// Get the project from project team
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <returns></returns>
        public Project GetProject(ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Entry(projectTeam).Entity.Project;
            }
        }

    }
}