using System;
using System.Collections.Generic;
using System.Linq;
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
}