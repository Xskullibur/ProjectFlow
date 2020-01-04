using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class TutorDAO
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

    }
}