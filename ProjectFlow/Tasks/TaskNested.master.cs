using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class TaskNested : System.Web.UI.MasterPage
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Status
                StatusBLL statusBLL = new StatusBLL();
                Dictionary<int, string> statusDict = statusBLL.Get();

                statusDDL.DataSource = statusDict;
                statusDDL.DataTextField = "Value";
                statusDDL.DataValueField = "Key";

                statusDDL.DataBind();

                // Allocations
                TeamMemberBLL memberBLL = new TeamMemberBLL();
                Dictionary<int, string> memberList = memberBLL.GetTeamMembersByTeamID(TEST_TEAM_ID);

                allocationList.DataSource = memberList;
                allocationList.DataTextField = "Value";
                allocationList.DataValueField = "Key";

                allocationList.DataBind();

                // Milestone
                MilestoneBLL milestoneBLL = new MilestoneBLL();
                var teamMilestones = milestoneBLL.GetMilestoneByTeamID(TEST_TEAM_ID);

                milestoneDDL.DataSource = teamMilestones;
                milestoneDDL.DataTextField = "milestoneName";
                milestoneDDL.DataValueField = "milestoneID";

                milestoneDDL.DataBind();

                milestoneDDL.Items.Insert(0, new ListItem("-- No Milestone --", "-1"));

            }
        }

        // Add Task OnClick Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            showModal();
        }

        // Show Add Modal
        private void showModal()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('show')", true);
        }

        private void hideModal()
        {
            // Clear Fields
            tNameTxt.Text = string.Empty;
            tDescTxt.Text = string.Empty;
            tStartTxt.Text = string.Empty;
            tEndTxt.Text = string.Empty;
            allocationList.ClearSelection();
            statusDDL.ClearSelection();
            milestoneDDL.ClearSelection();

            // Hide Modal
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('hide')", true);
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {
            int selected_milestone = Convert.ToInt32(milestoneDDL.SelectedValue);

            // Create Task Object
            Task newTask = new Task();
            newTask.taskName = tNameTxt.Text;
            newTask.taskDescription = tDescTxt.Text;
            newTask.startDate = Convert.ToDateTime(tStartTxt.Text);
            newTask.endDate = Convert.ToDateTime(tEndTxt.Text);
            newTask.teamID = TEST_TEAM_ID;
            newTask.statusID = Convert.ToInt32(statusDDL.SelectedValue);

            if (selected_milestone != -1)
            {
                newTask.milestoneID = selected_milestone;
            }

            // Create all Task Allocations
            List<TaskAllocation> taskAllocations = new List<TaskAllocation>();

            foreach (ListItem item in allocationList.Items.Cast<ListItem>().Where( x => x.Selected))
            {
                TaskAllocation newAllocation = new TaskAllocation();
                newAllocation.taskID = newTask.taskID;
                newAllocation.assignedTo = Convert.ToInt32(item.Value);

                taskAllocations.Add(newAllocation);
            }

            // Submit Query
            TaskBLL taskBLL = new TaskBLL();
            bool result = taskBLL.Add(newTask, taskAllocations);

            // Show Result
            if (result)
            {
                hideModal();
            }
            else
            {

            }

        }

        //// Show Edit Modal
        //private void showEditTaskModal()
        //{
        //    tTitleLbl.Text = "Update Task";
        //    tSaveBtn.Text = "Update";
        //    tSaveBtn.Click += new EventHandler(updateTask_Click);

        //    // Fill Update Data

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
        //    tUpdatePanel.Update();
        //}

        // Update Task Event
        protected void updateTask_Click(object sender, EventArgs e)
        {

        }

    }
}