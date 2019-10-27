using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Login
{
    public class BasicLoginManager : ILoginManager
    {
        public AuthenticatedCredential HandleAuth(LoginCredential credential)
        {
            //TODO handle login from user and check if user exists
            if(credential.Username == "Hello" && credential.PasswordHashed == "Password")
            {
                return new AuthenticatedCredential(credential.Username, Services.Role.BasicUser);
            }
            else if(credential.Username == "Admin" && credential.PasswordHashed == "Password")
            {
                return new AuthenticatedCredential(credential.Username, Services.Role.PremiumUser);
            }
            return null;
        }
    }
}