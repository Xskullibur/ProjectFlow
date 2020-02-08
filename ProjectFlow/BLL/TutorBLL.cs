using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class TutorBLL
    {
        /// <summary>
        /// Find the Tutor using email
        /// </summary>
        /// <param name="email">email of the Tutor</param>
        /// <returns>Instance of the found Tutor object, null if email does not exist in the database</returns>
        public Tutor FindTutorByEmail(string email)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Membership.FirstOrDefault(membership => membership.Email.Equals(email))?.aspnet_Users?.Tutor;
            }
        }

        /// <summary>
        /// Find the Tutor using username
        /// </summary>
        /// <param name="username">username of the Tutor</param>
        /// <returns>Instance of the found Tutor object, null if username does not exist in the database</returns>
        public Tutor FindTutorByUsername(string username)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.Include("aspnet_Membership").FirstOrDefault(user => user.UserName.Equals(username))?.Tutor;
            }
        }

        /// <summary>
        /// Check if the tutor have this project in his/her list of created projects
        /// 
        /// List of created projects: Each tutor will have a list of created projects which includes the current, past or even
        /// the future projects which the tutor have created to assign to project teams.
        /// 
        /// This method check whether if the student have a particular project which the tutor have created
        /// 
        /// </summary>
        /// <param name="tutor"></param>
        /// <param name="project"></param>
        /// <returns>only true if the tutor have this project in his/her list of created projects</returns>
        public bool ContainsProject(Tutor tutor, Project project)
        {
            return tutor.UserId.Equals(project.UserId);
        }

        /// <summary>
        /// Check if the a ProjectTeam belongs to a Tutor
        /// </summary>
        /// <param name="tutor"></param>
        /// <param name="projectTeam"></param>
        /// <returns></returns>
        public bool HaveProjectTeam(Tutor tutor, ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _tutor = dbContext.Tutors.Find(tutor.UserId);
                return _tutor.Projects
                    .Any(project => project.ProjectTeams
                    .Any(_projectTeam => _projectTeam.teamID.Equals(projectTeam.teamID)));
            }
        }

        /// <summary>
        /// Find the Tutor using UID
        /// </summary>
        /// <param name="username">username of the Tutor</param>
        /// <returns>Instance of the found Tutor object, null if username does not exist in the database</returns>
        public bool CheckTutorByUID(Guid Uid)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var list = dbContext.Tutors.Select(y => y.UserId).ToList();
                var exist = list.Contains(Uid);
                return exist;
            }
        }
    }
}