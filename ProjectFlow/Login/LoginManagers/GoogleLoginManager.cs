using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Login
{
    public class GoogleLoginManager : ILoginManager
    {
        public static string RedirectionUrl = "";
        public static string ClientId = "";
        public static string ClientSecret = "";

        public AuthenticatedCredential HandleAuth(LoginCredential credential)
        {
            throw new NotImplementedException();
        }
    }
}