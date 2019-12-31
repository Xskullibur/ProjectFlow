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
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.aspnet_Membership.Email;
                }
                else if (projectFlowIdentity.IsTutor)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.aspnet_Membership.Email;
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

        public void ShowAlertWithTiming(string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {

            if (AlertsPlaceHolder.Controls.Count >= 10)
            {
                //Max alert reached
                Console.Error.WriteLine("Max Alerts reached!");
                return;
            }

            string id = Guid.NewGuid().ToString();
            string html = CreateAlertHTML(alertMsg, alertType, id, escapedHtml, dismissable);

            var control = new LiteralControl();
            control.Text = html;

            AlertsPlaceHolder.Controls.Add(control);

            string timeoutScript = $@"
                   <script type=""text/javascript"">
                        setTimeout(function(){{ $('#{id}').alert('close'); }}, {time});
                   </script>
            ";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "timeout-" + id, timeoutScript, false);
        }

        public void ShowAlert(string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            if (AlertsPlaceHolder.Controls.Count >= 10)
            {
                //Max alert reached
                Console.Error.WriteLine("Max Alerts reached!");
                return;
            }

            string html = CreateAlertHTML(alertMsg, alertType, null, escapedHtml, dismissable);

            var control = new LiteralControl();
            control.Text = html;

            AlertsPlaceHolder.Controls.Add(control);

        }

        private string CreateAlertHTML(string alertMsg, string alertType, string id, bool escapedHtml, bool dismissable)
        {

            string closeBtn = @"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</ span >
            </button> ";

            return $@"<div {((id != null) ? $"id=\"{id}\"" : "")} class=""alert {alertType} {(dismissable ? "alert-dismissible" : "")} fade show"" role=""alert"">
                    {(escapedHtml ? Server.HtmlEncode(alertMsg) : alertMsg)}
                    {(dismissable ? closeBtn : "")}
            </div> ";
        }

    }
}