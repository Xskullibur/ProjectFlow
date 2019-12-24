using ProjectFlow.BLL;
using ProjectFlow.DAO;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');

                        string email = id.Name;
                        StudentDAO dao = new StudentDAO();
                        Student student = dao.FindStudentByEmail(email);

                        var projectFlowIdentity = new ProjectFlowIdentity(student, id);
                        var principal = new GenericPrincipal(projectFlowIdentity, roles);

                        HttpContext.Current.User = principal;
                    }
                }
            }
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