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

            if (Page.IsValid)
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

        }

        private bool verifyAddTask()
        {
            bool result = true;

            // Task Name
            if (string.IsNullOrEmpty(tNameTxt.Text))
            {
                tNameRequiredValidator.IsValid = false;
                result = false;
            }

            if (tNameTxt.Text.Length > 255)
            {
                tNameRegexValidator.IsValid = false;
                result = false;
            }

            // Description
            if (string.IsNullOrEmpty(tDescTxt.Text))
            {
                tDescRequiredValidator.IsValid = false;
                result = false;
            }

            if (tDescTxt.Text.Length > 255)
            {
                tDescRegexValidator.IsValid = false;
                result = false;
            }

            // Milestone
            if (milestoneDDL.SelectedIndex == -1)
            {
                milestoneRequiredValidator.IsValid = false;
                result = false;
            }

            // Status
            if (statusDDL.SelectedIndex == -1)
            {
                statusRequiredValidator.IsValid = false;
                result = false;
            }

            return result;
        }

        // Verify Start and End Dates are valid
        protected void startEndDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Verify Both Start Date and End Date has values
            if (!string.IsNullOrEmpty(tStartTxt.Text) && !string.IsNullOrEmpty(tEndTxt.Text))
            {
                // Verify Valid DateTime
                if (DateTime.TryParse(args.Value, out DateTime dateTime))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

    }
}