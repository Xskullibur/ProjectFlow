using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Login
{
    public class SessionAuthCallback : IAuthCallback
    {
        public void AuthCallback(Page page, CallbackEvent callbackEvent)
        {
            page.Session["Authenticated"] = callbackEvent.IsAuthenticated;
            page.Session["User"] = callbackEvent.User;
        }
    }
}