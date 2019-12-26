using ProjectFlow.Login;
using ProjectFlow.Utils.Alerts;
using System;
using System.Web;
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
                this.LoginUsernameLbl.Text = (user.Identity as ProjectFlowIdentity).Student.username;
            }
        }
    }
}