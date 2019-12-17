using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Helpers
{
    public static class Helper
    {

        public enum AlertType
        {
            Success,
            Error,
            Warning,
            Primary,
            Secondary,
            Info,
            Light,
            Dark
        }

        public static void ShowAlert(object sender, string message, AlertType type)
        {
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), System.Guid.NewGuid().ToString(), $"ShowAlert({message}, {type});", true);
        }
    }
}