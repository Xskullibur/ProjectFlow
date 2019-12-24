using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class StudentDAO
    {
        /// <summary>
        /// Validate the email exist and matches the password in the database.
        /// NOTE: password hashing is not perform inside this function,
        /// The password should be hashed before calling this method
        /// </summary>
        /// <param name="email">email of the login credential</param>
        /// <param name="password">hashed password of the user</param>
        /// <returns>The Student object which belongs to the login credential</returns>
        public Student LoginValidate(string email, string password)
        {
            using(ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.FirstOrDefault(student => student.email.Equals(email) && student.password.Equals(password));
            }
        }


    }
}