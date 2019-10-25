using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Login
{

    /// <summary>
    /// LoginService used for login purpose.
    /// LoginService depends on, <see cref="ILoginManager"/> and <see cref="IAuthCallback"/>
    /// Pass both this classes as dependency into LoginService constructor
    /// </summary>
    public class LoginService
    {
        public LoginService(ILoginManager loginManager, IAuthCallback authCallback)
        {
            this.loginManager = loginManager;
            this.authCallback = authCallback;
        }

        private ILoginManager loginManager { get; }
        private IAuthCallback authCallback { get; }

        /// <summary>
        /// Authenticate user from the credential passed into this method
        /// </summary>
        /// <param name="page">Page where this this method called</param>
        /// <param name="credential">Credential to authenticate the user</param>
        public void Authenticate(Page page, LoginCredential credential)
        {
            var callbackEvent = new CallbackEvent(loginManager.HandleAuth(credential));
            authCallback.AuthCallback(page, callbackEvent);
        }

    }

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

    /// <summary>
    /// Handle post authentication such as Redirecting user, creating new user etc
    /// Some IAuthCallback already defined:
    /// <see cref="AuthCallbacks.RedirectAuthCallback"/>
    /// </summary>
    public interface IAuthCallback
    {
        void AuthCallback(Page page, CallbackEvent callbackEvent);

    }

    public class CallbackEvent
    {
        public bool IsAuthenticated { get; }

        internal protected CallbackEvent(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

    }

    namespace LoginManagers
    {
        public class BasicLoginManager : ILoginManager
        {
            public bool HandleAuth(LoginCredential credential)
            {
                //TODO handle login from user and check if user exists
                return credential.Username == "Hello" && credential.PasswordHashed == "Password";
            }
        }

        public class GoogleLoginManager : ILoginManager
        {
            public static string RedirectionUrl = "";
            public static string ClientId = "";
            public static string ClientSecret = "";

            public bool HandleAuth(LoginCredential credential)
            {
                throw new NotImplementedException();
            }
        }
    }

    namespace AuthCallbacks
    {
        public class RedirectAuthCallback : IAuthCallback
        {

            private string Path;

            public RedirectAuthCallback(string path)
            {
                Path = path;
            }

            public void AuthCallback(Page page, CallbackEvent callbackEvent)
            {
                if(callbackEvent.IsAuthenticated)page.Response.Redirect(Path);
                else page.Response.Redirect("Failed");
            }
        }
    }


}