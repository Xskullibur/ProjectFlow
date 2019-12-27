using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class ProjectDAO
    {
        public void InsertProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = new Project() {
                    projectID = ProjectID,
                    projectName = Name,
                    projectDescription = Desc,
                    tutorID = TutorID                   
                };
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }
        }

        public List<Project> GetProjectTutor(int TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {               
                Tutor tutor = dbContext.Tutors.First(x => x.tutorID == TutorID);                
                return tutor.Projects.ToList();
            }
        }

        public bool CheckUniqueProjectID(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if(dbContext.Projects.Any(x => x.projectID == ProjectID))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}