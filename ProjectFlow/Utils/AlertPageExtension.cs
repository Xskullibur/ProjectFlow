using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Utils.Alerts
{
    public static class AlertPageExtension
    {

        public static void ShowAlert(this Page page, string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            (page.Master as Main).ShowAlert(alertMsg, alertType, escapedHtml, dismissable);
        }

        public static void ShowAlertWithTiming(this Page page, string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {
            (page.Master as Main).ShowAlertWithTiming(alertMsg, alertType, time, escapedHtml, dismissable);
        }
    }
}