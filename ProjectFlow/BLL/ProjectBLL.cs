using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class ProjectBLL
    {

        public void CreateProject(string ProjectID, string Name, string Desc, Guid tutorUserId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = new Project()
                {
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

        public Project GetProjectByProjectId(string projectId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Projects.Find(projectId);
            }
        }

        /// <summary>
        /// Check if the student belongs to a project team
        /// </summary>
        /// <param name="teamMember">student which will be use for checking if the student belongs to the project group</param>
        /// <param name="projectTeam">The project team which will be use to check if the student belong to this project team</param>
        /// <returns>true if the student belongs to the project team, false otherwise</returns>
        public bool BelongsToProjectTeam(Student student, ProjectTeam projectTeam)
        {
            StudentBLL studentBLL = new StudentBLL();
            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
            return studentBLL.ContainsProject(student, projectTeamBLL.GetProject(projectTeam));
        }

    }
}