using ProjectFlow.BLL;
using ProjectFlow.Scheduler;
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
    public partial class TaskNested : MasterPage
    {
        private const int TEST_TEAM_ID = 2;

        // Public Attributes and Methods
        public event EventHandler refreshGrid;
        public enum TaskViews
        {
            OngoingTaskView,
            DroppedTaskView,
            Calendar,
            Swimlane
        }

        public void changeSelectedView(TaskViews selectedView)
        {
            switch (selectedView)
            {
                case TaskViews.OngoingTaskView:
                    taskViewDDL.SelectedIndex = 0;
                    break;
                case TaskViews.DroppedTaskView:
                    taskViewDDL.SelectedIndex = 1;
                    break;
                case TaskViews.Calendar:
                    taskViewDDL.SelectedIndex = 2;
                    break;
                case TaskViews.Swimlane:
                    taskViewDDL.SelectedIndex = 3;
                    break;
                default:
                    break;
            }
        }

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

        // Switch Views
        protected void taskViewDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (taskViewDDL.SelectedIndex)
            {

                case 0:
                    Response.Redirect("OngoingTaskView.aspx");
                    break;

                case 1:
                    Response.Redirect("DroppedTaskView.aspx");
                    break;

                default:
                    break;

            }
        }

        /**
         * MODAL MANIPULATION
         **/

        // Add Task OnClick Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('show');", true);
        }

        // On UpdatePanel Init
        protected void modalUpdatePanel_Init(object sender, EventArgs e)
        {
            // Init SelectPicker And DateTimePicker
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "init_pickers", "$('.selectpicker').selectpicker();", true);
        }


        /**
         * TASK MANIPULATION
         **/

        // Clear Task Errors
        private void ClearErrorMessages()
        {
            tNameErrorLbl.Text = string.Empty;
            tNameErrorLbl.Visible = false;

            tDescErrorLbl.Text = string.Empty;
            tDescErrorLbl.Visible = false;

            tMilestoneErrorLbl.Text = string.Empty;
            tMilestoneErrorLbl.Visible = false;

            tStartDateErrorLbl.Text = string.Empty;
            tStartDateErrorLbl.Visible = false;

            tEndDateErrorLbl.Text = string.Empty;
            tEndDateErrorLbl.Visible = false;

            startEndDateErrorLbl.Text = string.Empty;
            startEndDateErrorLbl.Visible = false;

            statusErrorLbl.Text = string.Empty;
            statusErrorLbl.Visible = false;
        }

        // Remove Add Task Panel
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

            // Get Attributes
            string taskName = tNameTxt.Text;
            string taskDesc = tDescTxt.Text;
            int milestoneIndex = milestoneDDL.SelectedIndex;
            string startDate = tStartTxt.Text;
            string endDate = tEndTxt.Text;
            int statusIndex = statusDDL.SelectedIndex;

            // Clear all Error Messages
            ClearErrorMessages();

            // Verify Attributes
            TaskHelper taskVerification = new TaskHelper();
            bool verified = taskVerification.Verify(taskName, taskDesc, milestoneIndex, startDate, endDate, statusIndex);

            if (!verified)
            {
                /**
                 * Show Error Messages
                 **/

                // Name
                if (taskVerification.TNameErrors.Count > 0)
                {
                    tNameErrorLbl.Visible = true;
                    tNameErrorLbl.Text = string.Join("<br>", taskVerification.TNameErrors);
                }

                // Description
                if (taskVerification.TDescErrors.Count > 0)
                {
                    tDescErrorLbl.Visible = true;
                    tDescErrorLbl.Text = string.Join("<br>", taskVerification.TDescErrors);
                }

                // Milestone
                if (taskVerification.TMilestoneErrors.Count > 0)
                {
                    tMilestoneErrorLbl.Visible = true;
                    tMilestoneErrorLbl.Text = string.Join("<br>", taskVerification.TMilestoneErrors);
                }

                // Start Date
                if (taskVerification.TStartDateErrors.Count > 0)
                {
                    tStartDateErrorLbl.Visible = true;
                    tStartDateErrorLbl.Text = string.Join("<br>", taskVerification.TStartDateErrors);
                }

                // End Date
                if (taskVerification.TEndDateErrors.Count > 0)
                {
                    tEndDateErrorLbl.Visible = true;
                    tEndDateErrorLbl.Text = string.Join("<br>", taskVerification.TEndDateErrors);
                }

                // Start End Date
                if (taskVerification.StartEndDateErrors.Count > 0)
                {
                    startEndDateErrorLbl.Visible = true;
                    startEndDateErrorLbl.Text = string.Join("<br>", taskVerification.StartEndDateErrors);
                }

                // Status
                if (taskVerification.TStatusErrors.Count > 0)
                {
                    statusErrorLbl.Visible = true;
                    statusErrorLbl.Text = string.Join("<br>", taskVerification.TStatusErrors);
                }

            }
            else
            {
                /**
                 * Add Task
                 **/

                string selected_milestone = milestoneDDL.SelectedValue;

                // Create Task Object
                Task newTask = new Task();
                newTask.taskName = taskName;
                newTask.taskDescription = taskDesc;
                newTask.startDate = DateTime.Parse(startDate);
                newTask.endDate = DateTime.Parse(endDate);
                newTask.statusID = Convert.ToInt32(statusDDL.SelectedValue);

                newTask.teamID = TEST_TEAM_ID; // TODO: Change to TeamID Session

                if (selected_milestone != "-1")
                {
                    newTask.milestoneID = Convert.ToInt32(selected_milestone);
                }

                // Create Task Allocations
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
                    // Default Notification Setup (One Day Reminder + Delay Update and Alert)
                    NotificationHelper.Default_AddTask_Setup(newTask.taskID);

                    // Update Page
                    hideModal();
                    refreshGrid?.Invoke(this, EventArgs.Empty);
                    this.Master.ShowAlertWithTiming("Task Successfully Added!", BootstrapAlertTypes.SUCCESS, 2000);
                }
                else
                {
                    this.Master.ShowAlert("Failed to Add Task!", BootstrapAlertTypes.DANGER);
                }
            }

        }

    }
}