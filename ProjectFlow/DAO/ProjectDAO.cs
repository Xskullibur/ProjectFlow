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

        public void UpdateProject(string ProjectID, string Name, string Desc)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = dbContext.Projects.Single(x => x.projectID.Equals(ProjectID));
                if(project != null)
                {                   
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

        public void InsertTeam(string TeamName, string Desc, string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var projectTeam = new ProjectTeam() {
                    teamName = TeamName,
                    teamDescription = Desc,
                    projectID = ProjectID
                };
                dbContext.ProjectTeams.Add(projectTeam);
                dbContext.SaveChanges();
            }
        }

        public List<ProjectTeam> GetTeam(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Where(x => x.projectID.Equals(ProjectID)).ToList();
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