using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Utils.Alerts
{

    public interface IAlert
    {
        Panel AlertsPanel { get; }

        void ShowAlert(string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true);
        void ShowAlertWithTiming(string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true);
    }

    public abstract class ProjectFlowMasterPage : MasterPage, IAlert
    {
        public abstract Panel AlertsPanel { get; }

        public void ShowAlertWithTiming(string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {

            if (AlertsPanel.Controls.Count >= 10)
            {
                //Max alert reached
                Console.Error.WriteLine("Max Alerts reached!");
                return;
            }

            string id = Guid.NewGuid().ToString();
            string html = CreateAlertHTML(alertMsg, alertType, id, escapedHtml, dismissable);

            var control = new LiteralControl();
            control.Text = html;

            AlertsPanel.Controls.Add(control);

            string timeoutScript = $@"
                   <script type=""text/javascript"">
                        setTimeout(function(){{ $('#{id}').alert('close'); }}, {time});
                   </script>
            ";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "timeout-" + id, timeoutScript, false);
        }

        public void ShowAlert(string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            if (AlertsPanel.Controls.Count >= 10)
            {
                //Max alert reached
                Console.Error.WriteLine("Max Alerts reached!");
                return;
            }

            string html = CreateAlertHTML(alertMsg, alertType, null, escapedHtml, dismissable);

            var control = new LiteralControl();
            control.Text = html;

            AlertsPanel.Controls.Add(control);

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


    public static class AlertPageExtension
    {

        public static void ShowAlert(this Page page, string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            (page.Master as IAlert).ShowAlert(alertMsg, alertType, escapedHtml, dismissable);
        }

        public static void ShowAlertWithTiming(this Page page, string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {
            (page.Master as IAlert).ShowAlertWithTiming(alertMsg, alertType, time, escapedHtml, dismissable);
        }

        public static void ShowAlert(this MasterPage page, string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            (page as IAlert).ShowAlert(alertMsg, alertType, escapedHtml, dismissable);
        }

        public static void ShowAlertWithTiming(this MasterPage page, string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {
            (page as IAlert).ShowAlertWithTiming(alertMsg, alertType, time, escapedHtml, dismissable);
        }

    }
}