using ProjectFlow.DAO;
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
            aspnet_UsersDAO dao = new aspnet_UsersDAO();
            var user = dao.GetUserFromUserName(username);
            return user;
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