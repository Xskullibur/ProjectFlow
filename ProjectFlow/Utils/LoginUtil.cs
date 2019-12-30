using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Utils
{
    public class LoginUtil
    {

        public static String ConvertAuthenticatedUserToRole(AuthenticatedUser authenticatedUser)
        {
            if (authenticatedUser.IsStudent) return "Student";
            else if (authenticatedUser.IsTutor) return "Tutor";
            else throw new ArgumentException("Not Authenticated");
        }

    }
}