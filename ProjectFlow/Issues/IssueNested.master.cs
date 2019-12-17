using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class IssueNested : System.Web.UI.MasterPage
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbMember.Text += (string)Session["SSTest"];
                lbIssue.Text += (string)Session["SSDesc"];
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

        /*private void refreshData()
        {
            TaskBLL taskBLL = new TaskBLL();

            taskView.DataSource = taskBLL.GetTasksByTeamId(TEST_TEAM_ID);
            taskView.DataBind();

            if (taskView.Rows.Count > 0)
            {
                taskView.HeaderRow.TableSection = TableRowSection.TableHeader;
                taskView.UseAccessibleHeader = true;
            }
        }*/
    }
}