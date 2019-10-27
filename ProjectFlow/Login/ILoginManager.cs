using ProjectFlow.Services;
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
    /// <see cref="BasicLoginManager"></see>
    /// <see cref="GoogleLoginManager"></see>
    /// </summary>
    public interface ILoginManager {
        /// <summary>
        /// Handle login authentication from user
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>A object containing all the authenticated credentials, if authentications is not successful returns null</returns>
        AuthenticatedCredential HandleAuth(LoginCredential credential);


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

    /// <summary>
    /// TODO this class will be replaced by a DAO class of User separated by the data access layer under N-tier architecture
    /// </summary>
    [Obsolete("This class is only temporary and will be replaced by a DAO class of User", false)]
    public class AuthenticatedCredential {
        public AuthenticatedCredential(string username, Role role)
        {
            Username = username;
            Role = role;
        }

        public string Username { get; }
        public Role Role { get; }

    }


}