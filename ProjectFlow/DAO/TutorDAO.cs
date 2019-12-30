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
        /// <param name="email">student email of the Tutor</param>
        /// <returns>Instance of the found Tutor object, null if email does not exist in the Tutor table</returns>
        public Tutor FindTutorByEmail(string email)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Tutors.FirstOrDefault(tutor => tutor.email.Equals(email));
            }
        }

    }
}