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
        public LoginService(ILoginManager loginManager, params IAuthCallback[] authCallbacks)
        {
            this.loginManager = loginManager;
            this.authCallbacks.AddRange(authCallbacks);
        }

        private ILoginManager loginManager { get; }
        private List<IAuthCallback> authCallbacks = new List<IAuthCallback>();

        /// <summary>
        /// Authenticate user from the credential passed into this method
        /// </summary>
        /// <param name="page">Page where this this method called</param>
        /// <param name="credential">Credential to authenticate the user (can be nullable if using external authentications method)</param>
        /// <returns>is the request authenticated</returns>  
        public bool Authenticate(Page page, LoginCredential credential = null)
        {
            var user = loginManager.HandleAuth(credential);
            var callbackEvent = new CallbackEvent(user != null, user);
            authCallbacks.ForEach(x => x.AuthCallback(page, callbackEvent));
            return callbackEvent.IsAuthenticated;
        }

    }
}