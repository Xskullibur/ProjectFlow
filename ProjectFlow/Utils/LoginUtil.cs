using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectFlow.Utils
{
    public class LoginUtil
    {

        public static String ConvertAuthenticatedUserToRole(MembershipUser authenticatedUser)
        {
            LoginBLL loginBLL = new LoginBLL();
            var user = loginBLL.GetUserFromUsername(authenticatedUser.UserName);
            if (user.IsStudent()) return "Student";
            else if (user.IsTutor()) return "Tutor";
            else throw new ArgumentException("Not Authenticated");
        }

    }
}