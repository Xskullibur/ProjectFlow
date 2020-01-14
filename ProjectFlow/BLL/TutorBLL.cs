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
        /// <returns>only true if the student have this project in his/her project's list</returns>
        public bool ContainsProject(Tutor tutor, Project project)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Projects.Find(project.).FirstOrDefault(project => project.UserId.Equals(tutor.UserId)).Contains(project.projectID);
            }
        }


    }
}