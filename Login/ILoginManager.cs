using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Login
{

    /// <summary>
    /// Perform and handle login authentication.
    /// Extends this class when you want a custom login authentication.
    /// Some ILoginManagers already defined:
    /// <see cref="LoginManagers.BasicLoginManager"></see>
    /// <see cref="LoginManagers.GoogleLoginManager"></see>
    /// </summary>
    public interface ILoginManager {
        /// <summary>
        /// Handle login authentication from user
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        bool HandleAuth(LoginCredential credential);


    }

    public class LoginCredential {

        public string Username { get;}
        public string PasswordHashed { get; }

        public LoginCredential(string username, string passwordHashed)
        {
            Username = username;
            PasswordHashed = passwordHashed;
        }
        

    }


}