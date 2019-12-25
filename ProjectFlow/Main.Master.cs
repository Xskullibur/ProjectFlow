using ProjectFlow.Utils.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class Main : ProjectFlowMasterPage
    {
        public override Panel AlertsPanel => AlertsPlaceHolder;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        

    }


}