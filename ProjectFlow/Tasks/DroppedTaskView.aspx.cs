using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class DroppedTaskView : System.Web.UI.Page
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(TaskNested.TaskViews.DroppedTaskView);
                refreshData();
            }
        }

        private void refreshData()
        {
            TaskBLL taskBLL = new TaskBLL();
            var dropped_tasks = taskBLL.GetDroppedTasksByTeamId(TEST_TEAM_ID);

            taskGrid.DataSource = dropped_tasks;
            taskGrid.DataBind();
        }

        protected void taskGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Selected Task ID
            int id = Convert.ToInt32(taskGrid.Rows[e.RowIndex].Cells[1].Text);

            // Delete Task
            TaskBLL taskBLL = new TaskBLL();
            bool result = taskBLL.Restore(id);
            refreshData();

            if (result)
            {
                this.Master.Master.ShowAlertWithTiming("Successfully Restore Task!", BootstrapAlertTypes.SUCCESS, 2000);
            }
            else
            {
                this.Master.Master.ShowAlert("Failed to Restore Task!", BootstrapAlertTypes.DANGER);
            }
        }

        protected void taskGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            taskGrid.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}