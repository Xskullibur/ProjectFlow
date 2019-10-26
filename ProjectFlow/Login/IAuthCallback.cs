using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Login
{
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
}