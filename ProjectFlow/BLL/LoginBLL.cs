using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class LoginBLL
    {
        public aspnet_Users GetUserFromUsername(string username)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.Include("Student").Include("Tutor").FirstOrDefault(user => user.UserName.Equals(username));
            }
        }


    }

    public static class aspnet_UsersExtension
    {
        public static bool IsStudent(this aspnet_Users user)
        {
            return user.Student != null;
        }

        public static bool IsTutor(this aspnet_Users user)
        {
            return user.Tutor != null;
        }
    }

}