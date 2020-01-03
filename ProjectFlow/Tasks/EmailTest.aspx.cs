using ProjectFlow.BLL;
using ProjectFlow.Scheduler;
using ProjectFlow.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EmailHelper emailHelper = new EmailHelper();
            //string textBody = emailHelper.GetTaskNotificationTemplate(1, "Do this", "Do this or do that", new DateTime(), new DateTime(), "Project Closure", "Work-in-progress", "Nosla");
        }

        protected async void Button1_ClickAsync(object sender, EventArgs e)
        {

            //DateTime date = Convert.ToDateTime(TextBox1.Text);

            //date = date.AddDays(-1);

            //IJobDetail job = JobScheduler.CreateEmailJob("habsdkas", new List<string> { "test" }, "asdsad", "asdasd");
            //ISimpleTrigger trigger = JobScheduler.CreateSimpleTrigger(job, date.ToUniversalTime());

            //Response.Write(date);
        }
    }
}