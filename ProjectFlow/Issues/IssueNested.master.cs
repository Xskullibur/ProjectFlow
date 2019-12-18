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
                refreshData();
            }
        }

        private void refreshData()
        {
            TaskBLL taskBLL = new TaskBLL();

            taskView.DataSource = taskBLL.GetTasksByTeamId(TEST_TEAM_ID);
            taskView.DataBind();

            if (taskView.Rows.Count > 0)
            {
                taskView.HeaderRow.TableSection = TableRowSection.TableHeader;
                taskView.UseAccessibleHeader = true;
            }
        }
    }
}