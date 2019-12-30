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
        /// <returns>A instance of AuthenticatedUser containing Student or Tutor object belonging to the login credentials</returns>
        public AuthenticatedUser LoginValidate(string email, string password)
        {
            AuthenticatedUser authenticatedUser = new AuthenticatedUser();

            StudentDAO studentDAO = new StudentDAO();
            Student student = studentDAO.FindStudentByEmail(email);

            //First check if the email belongs to a student
            if (student != null)
            {
                //Check password is the same
                if (student != null && student.password.Equals(password))
                {
                    authenticatedUser.Student = student;
                }
            }
            else
            {
                //If not we find the tutor with the email instead

                TutorDAO tutorDAO = new TutorDAO();
                Tutor tutor = tutorDAO.FindTutorByEmail(email);

                //Check if the email belongs to a tutor
                if(tutor != null)
                {
                    //Check password is the same
                    if (tutor != null && tutor.password.Equals(password))
                    {
                        authenticatedUser.Tutor = tutor;
                    }
                }

            }

            return authenticatedUser;

        }

    }

    public struct AuthenticatedUser
    {
        public Student Student;
        public Tutor Tutor;

        public bool IsTutor { get => Tutor != null; }
        public bool IsStudent { get => Student != null; }

        public bool Authenticated { get => IsTutor || IsStudent; }

    }

}