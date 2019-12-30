using ProjectFlow.Login;
using ProjectFlow.Utils.Alerts;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class ServicesWithContent : ProjectFlowMasterPage
    {
        public override Panel AlertsPanel => AlertsPlaceHolder;

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated)
            {
                var projectFlowIdentity = user.Identity as ProjectFlowIdentity;
                if (projectFlowIdentity.IsStudent)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Student.username;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Student.username;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Student.email;
                }
                else if (projectFlowIdentity.IsTutor)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Tutor.username;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Tutor.username;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Tutor.email;
                }
            }
            
        }

        protected void LogoutEvent(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}