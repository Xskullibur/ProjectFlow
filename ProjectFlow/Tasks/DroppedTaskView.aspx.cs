using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(TaskNested.TaskViews.DroppedTaskView);
                refreshData();
            }
            taskGrid.Font.Size = 11;
        }

        private void refreshData()
        {
            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            TaskBLL taskBLL = new TaskBLL();

            if (!Master.PersonalTaskSelected)
            {
                taskGrid.DataSource = taskBLL.GetDroppedTasksByTeamId(currentTeam.teamID);
                taskGrid.DataBind();
            }
            else
            {
                // Get Current User
                Student currentUser = Master.GetCurrentUser();

                taskGrid.DataSource = taskBLL.GetDroppedTaskByTeamIdWithStudent(currentTeam.teamID, currentUser);
                taskGrid.DataBind();
            }
            
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        protected void taskGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Selected Task ID
            int id = Convert.ToInt32(taskGrid.Rows[e.RowIndex].Cells[1].Text);

            // Delete Task
            TaskBLL taskBLL = new TaskBLL();
            bool result = taskBLL.Restore(taskBLL.GetTaskByID(id));
            refreshData();

            if (result)
            {
                NotificationHelper.Task_Restore_Setup(id);
                this.Master.Master.ShowAlertWithTiming("Task Successfully Restored !", BootstrapAlertTypes.SUCCESS, 2000);
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