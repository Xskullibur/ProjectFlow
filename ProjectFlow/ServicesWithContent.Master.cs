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
                this.LoginUsernameLbl.Text = "Welcome, " + (user.Identity as ProjectFlowIdentity).Student.username;
                this.LoginUsernameProfileLbl.Text = (user.Identity as ProjectFlowIdentity).Student.username;
                this.LoginEmailProfileLbl.Text = (user.Identity as ProjectFlowIdentity).Student.email;
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