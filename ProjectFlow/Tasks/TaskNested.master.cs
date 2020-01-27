﻿using ProjectFlow.BLL;
using ProjectFlow.Login;
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
        // Public Attributes and Methods
        public event EventHandler refreshGrid;
        public bool PersonalTaskSelected = false;
        public enum TaskViews
        {
            OngoingTaskView,
            DroppedTaskView,
            Calendar,
            Swimlane
        }

        // Change Selected  Task View
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
        
        // Get Current Project
        public ProjectTeam GetCurrentProjectTeam()
        {
            ServicesWithContent servicesWithContent = Master as ServicesWithContent;

            return servicesWithContent.CurrentProjectTeam;
        }

        // Get Current User
        public ProjectFlowIdentity GetCurrentIdentiy()
        {
            var projectFlowIdentity = HttpContext.Current.User.Identity as ProjectFlowIdentity;

            return projectFlowIdentity;
        }


        /**
         * Main Program
         **/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Current Team
                ProjectTeam currentTeam = GetCurrentProjectTeam();
                int teamID = currentTeam.teamID;

                // Status
                StatusBLL statusBLL = new StatusBLL();
                Dictionary<int, string> statusDict = statusBLL.Get();

                statusDDL.DataSource = statusDict;
                statusDDL.DataTextField = "Value";
                statusDDL.DataValueField = "Key";
                statusDDL.DataBind();

                // Priority
                PriorityBLL priorityBLL = new PriorityBLL();
                List<Priority> priorities = priorityBLL.Get();

                priorityDDL.DataSource = priorities;
                priorityDDL.DataTextField = "priority1";
                priorityDDL.DataValueField = "ID";
                priorityDDL.DataBind();

                // Allocations
                TeamMemberBLL memberBLL = new TeamMemberBLL();
                Dictionary<int, string> memberList = memberBLL.GetTeamMembersByTeamID(teamID);

                allocationList.DataSource = memberList;
                allocationList.DataTextField = "Value";
                allocationList.DataValueField = "Key";

                allocationList.DataBind();

                // Milestone
                MilestoneBLL milestoneBLL = new MilestoneBLL();
                var teamMilestones = milestoneBLL.GetMilestonesByTeamID(teamID);

                milestoneDDL.DataSource = teamMilestones;
                milestoneDDL.DataTextField = "milestoneName";
                milestoneDDL.DataValueField = "milestoneID";

                milestoneDDL.DataBind();

                milestoneDDL.Items.Insert(0, new ListItem("-- No Milestone --", "-1"));

            }

            if (GetCurrentIdentiy().IsTutor)
            {
                addTaskBtn.Visible = false;
            }

            updateCurrentFilterPanel();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
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

                case 2:
                    Response.Redirect("CalendarView.aspx");
                    break;

                default:
                    break;

            }
        }

        // Filter Task
        protected void fTaskNameBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fTaskNameTxt.Text))
            {

                string filterTaskName = fTaskNameTxt.Text;

                if (Session["filterTaskName"] != null)
                {
                    currentFiltersPanel.Controls.Remove(currentFiltersPanel.FindControl("cancelTaskNameFilter"));
                }

                Session["filterTaskName"] = fTaskNameTxt.Text;
                refreshGrid?.Invoke(this, EventArgs.Empty);
                updateCurrentFilterPanel();
            }
        }

        // Add Current Applied Filters
        private void updateCurrentFilterPanel()
        {
            if (Session["filterTaskName"] != null)
            {
                string taskName = Session["filterTaskName"].ToString();

                LinkButton linkButton = new LinkButton();
                linkButton.ID = "cancelTaskNameFilter";
                linkButton.Text = $"{taskName}<i class='fa fa-close ml-2'></i>";
                linkButton.CssClass = "btn btn-danger my-2";
                linkButton.Click += new EventHandler(removeTaskNameFilter_Click);

                currentFiltersPanel.Controls.Add(linkButton);
            }
        }

        // Remove Task Name Filter
        protected void removeTaskNameFilter_Click(object sender,EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            currentFiltersPanel.Controls.Remove(linkButton);

            Session["filterTaskName"] = null;
            refreshGrid?.Invoke(this, EventArgs.Empty);
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

            priorityErrorLbl.Text = string.Empty;
            priorityErrorLbl.Visible = false;
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
            // Get CurrentTeam
            ProjectTeam currentTeam = GetCurrentProjectTeam();
            int teamID = currentTeam.teamID;

            // Create Verification Variable
            bool verified = true;

            // Get Attributes
            string taskName = tNameTxt.Text;
            string taskDesc = tDescTxt.Text;
            string milestoneID = milestoneDDL.SelectedValue;
            string startDate = tStartTxt.Text;
            string endDate = tEndTxt.Text;
            string statusID = statusDDL.SelectedValue;
            string priorityID = priorityDDL.SelectedValue;

            // Clear all Error Messages
            ClearErrorMessages();

            // Verify Attributes
            TaskHelper taskVerification = new TaskHelper();
            verified = taskVerification.VerifyAddTask(teamID, taskName, taskDesc, milestoneID, startDate, endDate, statusID, priorityID);

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

                // Priority
                if (taskVerification.TPriorityErrors.Count > 0)
                {
                    priorityErrorLbl.Visible = true;
                    priorityErrorLbl.Text = string.Join("<br>", taskVerification.TPriorityErrors);
                }

            }
            else
            {
                /**
                 * Add Task
                 **/

                // Create Task Object
                Task newTask = new Task();
                newTask.taskName = taskName;
                newTask.taskDescription = taskDesc;
                newTask.startDate = DateTime.Parse(startDate);
                newTask.endDate = DateTime.Parse(endDate);
                newTask.statusID = Convert.ToInt32(statusID);
                newTask.priorityID = Convert.ToInt32(priorityID);

                if (Convert.ToInt32(milestoneID) == -1)
                {
                    newTask.milestoneID = null;
                }
                else
                {
                    newTask.milestoneID = Convert.ToInt32(milestoneID);
                }

                newTask.teamID = teamID;

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

        // Filter Task
        protected void personalChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                PersonalTaskSelected = true;
            }
            else
            {
                PersonalTaskSelected = false;
            }

            refreshGrid?.Invoke(this, EventArgs.Empty);
        }

    }
}