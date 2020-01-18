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

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            SmsHelper smsHelper = new SmsHelper();
            smsHelper.SendSMS("90843083", ".");

            //NotificationHelper.Default_AddTask_Setup(1033);
            //Response.Write("Jobs Created");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //NotificationHelper.Task_Drop_Setup(1033);
            //Response.Write("Jobs Paused");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //NotificationHelper.Task_Restore_Setup(1033);
            //Response.Write("Jobs Resumed");
        }
    }
}