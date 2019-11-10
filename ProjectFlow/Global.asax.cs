﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace ProjectFlow
{
    /// <summary>
    /// ProjectFlow global configuartions
    /// </summary>
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.4.1.min.js",
                DebugPath = "~/Scripts/jquery-3.4.1.js"
            });

            ScriptManager.ScriptResourceMapping.AddDefinition("bootstrap", new ScriptResourceDefinition
            {
                Path = "~/Scripts/bootstrap.min.js",
                DebugPath = "~/Scripts/bootstrap.js"
            });

            ScriptManager.ScriptResourceMapping.AddDefinition("popper", new ScriptResourceDefinition
            {
                Path = "~/Scripts/umd/popper.min.js",
                DebugPath = "~/Scripts/umd/popper.js"
            });

            ScriptManager.ScriptResourceMapping.AddDefinition("bootstrap-select", new ScriptResourceDefinition
            {
                Path = "~/Scripts/bootstrap-select.min.js",
                DebugPath = "~/Scripts/bootstrap-select.js"
            });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}