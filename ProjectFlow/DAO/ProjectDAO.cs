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

        public void UpdateProject(string ProjectID, string Name, string Desc, string ChangeID = "")
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = dbContext.Projects.Single(x => x.projectID.Equals(ProjectID));
                if(project != null)
                {
                    if (!ChangeID.Equals(""))
                    {
                        project.projectID = ChangeID;
                    }
                    project.projectName = Name;
                    project.projectDescription = Desc;
                    dbContext.SaveChanges();
                }               
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

        public bool CheckOwnership(string ProjectID, int TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.Projects.Any(x => x.projectID == ProjectID && x.tutorID == TutorID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}