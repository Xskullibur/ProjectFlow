using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class ServicesWithContent : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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