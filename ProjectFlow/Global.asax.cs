using ProjectFlow.App_Start;
using ProjectFlow.Login;
using StackExchange.Redis;
using ProjectFlow.Scheduler;
using Quartz;
using Quartz.Impl;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using ProjectFlow.BLL;

namespace ProjectFlow
{
    /// <summary>
    /// ProjectFlow global configuartions
    /// </summary>
    public class Global : System.Web.HttpApplication
    {

        public static ConnectionMultiplexer Redis = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            JobScheduler.StartAsync();

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


            ScriptManager.ScriptResourceMapping.AddDefinition("signalr", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery.signalR-2.4.1.min.js",
                DebugPath = "~/Scripts/jquery.signalR-2.4.1.js"
            });


            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Create redis connection
            Redis = ConnectionMultiplexer.Connect("192.168.99.100");

            ScriptManager.ScriptResourceMapping.AddDefinition("bootstrap-datetimepicker", new ScriptResourceDefinition
            {
                Path = "~/Scripts/Bootstrap_DateTimePicker/bootstrap-datetimepicker.min.js",
                DebugPath = "~/Scripts/Bootstrap_DateTimePicker/bootstrap-datetimepicker.min.js"
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
                        string role = roles[0];

                        string username = id.Name;

                        if (role.Equals("Student"))
                        {
                            StudentBLL bll = new StudentBLL();
                            Student student = bll.FindStudentByUsername(username);

                            var projectFlowIdentity = new ProjectFlowIdentity(student, id);
                            var principal = new GenericPrincipal(projectFlowIdentity, roles);

                            HttpContext.Current.User = principal;
                        }
                        else if(role.Equals("Tutor"))
                        {
                            TutorBLL bll = new TutorBLL();
                            Tutor tutor = bll.FindTutorByUsername(username);

                            var projectFlowIdentity = new ProjectFlowIdentity(tutor, id);
                            var principal = new GenericPrincipal(projectFlowIdentity, roles);

                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            throw new Exception("No such role: " + role);
                        }

                       
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
            //Close redis connection
            Redis.Close();
            // Shutdown Scheduler
            IScheduler scheduler = (IScheduler)StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Clear();
            scheduler.Shutdown();
        }
    }
}