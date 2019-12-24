using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class StudentDAO
    {
        
        
        /// <summary>
        /// Find the Student using student id
        /// </summary>
        /// <param name="id">student id of the Studennt</param>
        /// <returns>Instance of the found Student object</returns>
        public Student FindStudentById(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.Find(id);
            }
        }

        /// <summary>
        /// Find the Student using student email
        /// </summary>
        /// <param name="email">student email of the Student</param>
        /// <returns>Instance of the found Student object</returns>
        public Student FindStudentByEmail(string email)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.FirstOrDefault(student => student.email.Equals(email));
            }
        }



    }
}