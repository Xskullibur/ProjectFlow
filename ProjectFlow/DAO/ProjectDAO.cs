using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class ProjectDAO
    {
        public void InsertProject(string ProjectID, string Name, string Desc, Guid tutorUserId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = new Project() {
                    projectID = ProjectID,
                    projectName = Name,
                    projectDescription = Desc,
                    UserId = tutorUserId
                };
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }
        }

        public List<Project> GetProjectTutor(Guid tutorUserId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {               
                Tutor tutor = dbContext.Tutors.First(x => x.UserId.Equals(tutorUserId));
                return tutor.Projects.ToList();
            }
        }
    }
}