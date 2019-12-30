using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class LoginBLL
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
            StudentDAO dao = new StudentDAO();

            Student student = dao.FindStudentByEmail(email);

            //Check password is the same
            if (student != null && student.password.Equals(password)){
                return student;
            }else
            {
                return null;
            }

        }

    }
}