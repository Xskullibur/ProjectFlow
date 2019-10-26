using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Login
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
            if (callbackEvent.IsAuthenticated) page.Response.Redirect(Path);
            else page.Response.Redirect("Failed");
        }
    }
}