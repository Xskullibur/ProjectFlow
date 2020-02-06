using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;

namespace ProjectFlow.Issues
{
    public partial class IDroppedView : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iDroppedView);
                refreshData();
            }
            if (Master.GetCurrentIdentiy().IsTutor)
            {
                IssueView.Columns[IssueView.Columns.Count - 1].Visible = false;
            }
            IssueView.Font.Size = 11;
        }

        private void refreshData()
        {
            IssueBLL issueBLL = new IssueBLL();

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            IssueView.DataSource = issueBLL.GetDroppedIssueByTeamId(currentTeam.teamID);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        protected void IssueView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void IssueView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Selected Task ID
            int id = Convert.ToInt32(IssueView.Rows[e.RowIndex].Cells[0].Text);

            // Delete Task
            IssueBLL issueBLL = new IssueBLL();
            bool result = issueBLL.Restore(issueBLL.GetIssueByID(id));
            refreshData();
            //bool result = true;
            if (result)
            {
                //NotificationHelper.Task_Restore_Setup(id);
                this.Master.Master.ShowAlertWithTiming("Task Successfully Restored !", BootstrapAlertTypes.SUCCESS, 2000);
            }
            else
            {
                this.Master.Master.ShowAlert("Failed to Restore Task!", BootstrapAlertTypes.DANGER);
            }
        }

        //Pagination
        protected void IssueView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IssueView.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}