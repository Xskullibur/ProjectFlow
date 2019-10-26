using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Login
{
    public class BasicLoginManager : ILoginManager
    {
        public bool HandleAuth(LoginCredential credential)
        {
            //TODO handle login from user and check if user exists
            return credential.Username == "Hello" && credential.PasswordHashed == "Password";
        }
    }
}