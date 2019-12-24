using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueRes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbMember.Text += (string)Session["SSCreatedBy"];
                lbIssue.Text += (string)Session["SSDesc"];
                refreshData();
            }
        }
        private void refreshData()
        {
        }
    }
}