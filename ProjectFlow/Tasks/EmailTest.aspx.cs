using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmailHelper emailHelper = new EmailHelper();
            string textBody = emailHelper.GetTaskNotificationTemplate(1, "Do this", "Do this or do that", new DateTime(), new DateTime(), "Project Closure", "Work-in-progress", "Nosla");

            emailHelper.SendEmail(new List<string> { "194897L@mymail.nyp.edu.sg" }, "Task Alert!", textBody);
        }
    }
}