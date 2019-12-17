using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueNested : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            taskView.DataSource = new List<string>()
            {
                "TESTMEM1",
                "TESTMEM2",
                "TESTMEM3",
                "TESTMEM4",
                "TESTMEM5"
            };
            taskView.DataBind();
        }
    }
}