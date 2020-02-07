using ProjectFlow.BLL;
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
        /**
         * Public Attributes and Methods
         **/

        // Event Handler to refresh grid
        public event EventHandler refreshGrid;

        // Types of Task Filters
        private struct FilterType
        {
            public static string KEYWORD = "filterTaskName";
            public static string PRIORITY = "filterPriority";
            public static string STATUS = "filterStatus";
            public static string ALLOCATION = "filterAllocation";
        }

        // Types of Views
        public enum TaskViews
        {
            OngoingTaskView,
            DroppedTaskView,
            Calendar
        }

        // Change Selected Task View
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

        // Apply Filter to DataSources
        public IEnumerable<object> ApplyFilters(List<Task> data)
        {
            if (Session[FilterType.KEYWORD] != null)
            {
                string filterKeyword = Session[FilterType.KEYWORD].ToString();
                data = data.Where(x => x.taskName.ToLower().Contains(filterKeyword.ToLower()))
                    .ToList();
            }

            if (Session[FilterType.PRIORITY] != null)
            {
                Dictionary<int, string> priorityDict = (Session[FilterType.PRIORITY] as Dictionary<int, string>);

                foreach (var item in priorityDict)
                {
                    data = data.Where(x => x.priorityID == item.Key)
                        .ToList();
                }
            }

            if (Session[FilterType.STATUS] != null)
            {
                Dictionary<int, string> statusDict = (Session[FilterType.STATUS] as Dictionary<int, string>);

                foreach (var item in statusDict)
                {
                    data = data.Where(x => x.statusID == item.Key)
                        .ToList();
                }
            }

            if (Session[FilterType.ALLOCATION] != null)
            {
                Dictionary<int, string> allocationDict = (Session[FilterType.ALLOCATION] as Dictionary<int, string>);

                foreach (var item in allocationDict)
                {                    
                    data = data.Where(x => x.TaskAllocations.Select(allocation => allocation.TeamMember.memberID).Contains(item.Key))
                        .ToList();
                }

            }

            TaskBLL taskBLL = new TaskBLL();
            var ds = taskBLL.ConvertToDataSource(data);
            return ds;
        }


        /**
         * Main Program
         **/

        // On Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /**
                 * Page Header
                 **/

                (this.Master as ServicesWithContent).Header = "Task Management";


                /**
                 * Input Setup
                 **/

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

                fStatusListBox.DataSource = statusDict;
                fStatusListBox.DataTextField = "Value";
                fStatusListBox.DataValueField = "Key";
                fStatusListBox.DataBind();

                // Priority
                PriorityBLL priorityBLL = new PriorityBLL();
                List<Priority> priorities = priorityBLL.Get();

                priorityDDL.DataSource = priorities;
                priorityDDL.DataTextField = "priority1";
                priorityDDL.DataValueField = "ID";
                priorityDDL.DataBind();

                fPriorityListBox.DataSource = priorities;
                fPriorityListBox.DataTextField = "priority1";
                fPriorityListBox.DataValueField = "ID";
                fPriorityListBox.DataBind();

                // Allocations
                TeamMemberBLL memberBLL = new TeamMemberBLL();
                Dictionary<int, string> memberList = memberBLL.GetTeamMembersByTeamID(teamID);

                allocationList.DataSource = memberList;
                allocationList.DataTextField = "Value";
                allocationList.DataValueField = "Key";
                allocationList.DataBind();

                fAllocationListBox.DataSource = memberList;
                fAllocationListBox.DataTextField = "Value";
                fAllocationListBox.DataValueField = "Key";
                fAllocationListBox.DataBind();

                // Milestone
                MilestoneBLL milestoneBLL = new MilestoneBLL();
                var teamMilestones = milestoneBLL.GetMilestonesByTeamID(teamID);

                milestoneDDL.DataSource = teamMilestones;
                milestoneDDL.DataTextField = "milestoneName";
                milestoneDDL.DataValueField = "milestoneID";

                milestoneDDL.DataBind();

                // Update Filter Toolbox
                UpdateFiltersToolBox();
            }

            if (GetCurrentIdentiy().IsTutor)
            {
                addTaskBtn.Visible = false;
                taskModal.Visible = false;
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
            UpdateCurrentFilterPanel();
        }

        // Switch Task Views
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


        /**
         * FILTER
         **/

        // Filter Add Event
        protected void filterBtn_Click(object sender, EventArgs e)
        {
            // Remove All Controls From Filter Panel
            currentFiltersPanel.Controls.Clear();

            // Check Keyword Fiter
            if (!string.IsNullOrEmpty(fTaskNameTxt.Text))
            {
                string filterTaskName = fTaskNameTxt.Text;
                Session[FilterType.KEYWORD] = fTaskNameTxt.Text;
            }

            // Check Priority Filter
            CheckFiltersInListBox(fPriorityListBox, FilterType.PRIORITY);

            // Status Filter
            CheckFiltersInListBox(fStatusListBox, FilterType.STATUS);

            // Allocation Filter
            CheckFiltersInListBox(fAllocationListBox, FilterType.ALLOCATION);

            UpdateCurrentFilterPanel();
            refreshGrid?.Invoke(this, EventArgs.Empty);
        }
        
        // Update Filter Panel (Check for filters and Add Button to Panel)
        private void UpdateCurrentFilterPanel()
        {
            if (Session[FilterType.KEYWORD] != null)
            {
                string taskName = Session[FilterType.KEYWORD].ToString();

                LinkButton linkButton = CreateFilterButton(taskName, FilterType.KEYWORD, removeTaskNameFilter_Click);
                currentFiltersPanel.Controls.Add(linkButton);
            }

            if (Session[FilterType.PRIORITY] != null)
            {
                AddFilterButtonsToPanel(FilterType.PRIORITY);
            }

            if (Session[FilterType.STATUS] != null)
            {
                AddFilterButtonsToPanel(FilterType.STATUS);
            }

            if (Session[FilterType.ALLOCATION] != null)
            {
                AddFilterButtonsToPanel(FilterType.ALLOCATION);
            }

        }

        // Create and add buttons to filter panel
        private void AddFilterButtonsToPanel(string sessionName)
        {
            List<string> filters = (Session[sessionName] as Dictionary<int, string>).Values.ToList();

            foreach (string filter in filters) { 
            
                LinkButton linkButton = new LinkButton();

                if (sessionName == FilterType.KEYWORD)
                {
                    linkButton = CreateFilterButton(filter, sessionName, removeTaskNameFilter_Click);
                }
                else if (sessionName == FilterType.PRIORITY)
                {
                    linkButton = CreateFilterButton(filter, sessionName, removePriorityFilter_Click);
                }
                else if (sessionName == FilterType.STATUS)
                {
                    linkButton = CreateFilterButton(filter, sessionName, removeStatusFilter_Click);
                }
                else if (sessionName == FilterType.ALLOCATION)
                {
                    linkButton = CreateFilterButton(filter, sessionName, removeAllocationFilter_Click);
                }

                currentFiltersPanel.Controls.Add(linkButton);
            }
        }

        // Check ListBox for Filters
        private void CheckFiltersInListBox(ListBox listBox, string filterType)
        {
            if (listBox.GetSelectedIndices().Length != 0)
            {
                Dictionary<int, string> currrentFilters = new Dictionary<int, string>();

                foreach (int index in listBox.GetSelectedIndices())
                {
                    var item = listBox.Items[index];
                    currrentFilters.Add(Convert.ToInt32(item.Value), item.Text);
                }

                Session[filterType] = currrentFilters;
            }
        }

        // Update values based on filters applied
        private void UpdateFiltersToolBox()
        {
            fTaskNameTxt.Text = string.Empty;
            if (Session[FilterType.KEYWORD] !=  null)
            {
                fTaskNameTxt.Text = Session[FilterType.KEYWORD].ToString();
            }

            fPriorityListBox.ClearSelection();
            if (Session[FilterType.PRIORITY] != null)
            {
                Dictionary<int, string> priorityDict = (Session[FilterType.PRIORITY] as Dictionary<int, string>);

                foreach (var priority in priorityDict)
                {
                    fPriorityListBox.SelectedValue = priority.Key.ToString();
                }
            }

            fStatusListBox.ClearSelection();
            if (Session[FilterType.STATUS] != null)
            {
                Dictionary<int, string> statusDict = (Session[FilterType.STATUS] as Dictionary<int, string>);

                foreach (var status in statusDict)
                {
                    fStatusListBox.SelectedValue = status.Key.ToString();
                }
            }

            fAllocationListBox.ClearSelection();
            if (Session[FilterType.ALLOCATION] !=  null)
            {
                Dictionary<int, string> allocationDict = (Session[FilterType.ALLOCATION] as Dictionary<int, string>);

                foreach (var allocation in allocationDict)
                {
                    fAllocationListBox.SelectedValue = allocation.Key.ToString();
                }
            }
        }


        /**
         * Create Filter Button
         **/

        private LinkButton CreateFilterButton(string filterName, string filterType, EventHandler eventHandler)
        {
            LinkButton linkButton = new LinkButton();

            linkButton.ID = $"filter_{filterType}_{filterName}";
            linkButton.Text = $"{filterName} <i class='fa fa-close ml-2'></i>";
            linkButton.CssClass = "btn btn-danger my-4 mr-1";
            linkButton.Click += eventHandler;

            return linkButton;
        }


        /**
         * REMOVE FILTER EVENTS
         **/

        // Remove Filter Event Handler
        private void FilterHandler(object sender, string sessionName)
        {
            LinkButton linkButton = (LinkButton)sender;
            currentFiltersPanel.Controls.Remove(linkButton);

            string value = linkButton.Text;
            Dictionary<int, string> filters = (Session[sessionName] as Dictionary<int, string>);

            int key = filters.FirstOrDefault(x => value.Contains(x.Value)).Key;
            filters.Remove(key);

            if (filters.Count() == 0)
                Session[sessionName] = null;

            refreshGrid?.Invoke(this, EventArgs.Empty);
            UpdateFiltersToolBox();
        }

        // Keyword
        protected void removeTaskNameFilter_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            currentFiltersPanel.Controls.Remove(linkButton);

            Session[FilterType.KEYWORD] = null;

            refreshGrid?.Invoke(this, EventArgs.Empty);
            UpdateFiltersToolBox();
        }

        // Allocation
        private void removeAllocationFilter_Click(object sender, EventArgs e)
        {
            FilterHandler(sender, FilterType.ALLOCATION);
        }

        // Status
        private void removeStatusFilter_Click(object sender, EventArgs e)
        {
            FilterHandler(sender, FilterType.STATUS);
        }
        
        // Priority
        protected void removePriorityFilter_Click(object sender, EventArgs e)
        {
            FilterHandler(sender, FilterType.PRIORITY);
        }


        /**
         * MODAL MANIPULATION
         **/

        // On UpdatePanel Init
        protected void modalUpdatePanel_Init(object sender, EventArgs e)
        {
            // Init SelectPicker And DateTimePicker
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "init_pickers", "$('.selectpicker').selectpicker();", true);
            alertLbl.Visible = false;
            alertLbl.Text = string.Empty;
        }

        // Clear Input
        private void ClearModalInputs()
        {
            tNameTxt.Text = string.Empty;
            tDescTxt.Text = string.Empty;
            tStartTxt.Text = string.Empty;
            tEndTxt.Text = string.Empty;
            milestoneDDL.ClearSelection();
            statusDDL.ClearSelection();
            priorityDDL.ClearSelection();
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
            ClearModalInputs();

            // Hide Modal
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('hide')", true);
        }

        // Verify Task Details and Show Error Messages
        private bool VerifyTask(
            out int teamID, 
            out string taskName, 
            out string taskDesc, 
            out string milestoneID,
            out string startDate, 
            out string endDate, 
            out string statusID, 
            out string priorityID)
        {
            // Get CurrentTeam
            ProjectTeam currentTeam = GetCurrentProjectTeam();
            teamID = currentTeam.teamID;

            // Create Verification Variable
            bool verified = true;

            // Get Attributes
            taskName = tNameTxt.Text;
            taskDesc = tDescTxt.Text;
            startDate = tStartTxt.Text;
            endDate = tEndTxt.Text;
            milestoneID = milestoneDDL.SelectedValue;
            statusID = statusDDL.SelectedValue;
            priorityID = priorityDDL.SelectedValue;

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

            return verified;
        }

        // Add Task
        private bool AddTask(int teamID, string taskName, string taskDesc, string milestoneID, string startDate, string endDate, string statusID, string priorityID)
        {
            // Create Task Object
            Task newTask = new Task();
            newTask.taskName = taskName;
            newTask.taskDescription = taskDesc;
            newTask.startDate = DateTime.Parse(startDate);
            newTask.endDate = DateTime.Parse(endDate);
            newTask.statusID = Convert.ToInt32(statusID);
            newTask.priorityID = Convert.ToInt32(priorityID);
            newTask.milestoneID = Convert.ToInt32(milestoneID);

            newTask.teamID = teamID;

            // Create Task Allocations
            List<TaskAllocation> taskAllocations = new List<TaskAllocation>();

            foreach (ListItem item in allocationList.Items.Cast<ListItem>().Where(x => x.Selected))
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

                refreshGrid?.Invoke(this, EventArgs.Empty);
                Master.ShowAlertWithTiming("Task Successfully Added!", BootstrapAlertTypes.SUCCESS, 3000);
            }
            else
            {
                Master.ShowAlertWithTiming("Error While Adding Task, Please Try Again", BootstrapAlertTypes.DANGER, 3000);
            }

            return result;
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {
            // Verify Task
            bool verified = VerifyTask(out int teamID, 
                out string taskName, 
                out string taskDesc, 
                out string milestoneID,
                out string startDate, 
                out string endDate, 
                out string statusID,
                out string priorityID);

            // Task Verified
            if (verified)
            {
                // Add Task
                bool result = AddTask(teamID, taskName, taskDesc, milestoneID, startDate, endDate, statusID, priorityID);

                if (result)
                {
                    hideModal();
                }
            }

        }

        protected void tSaveAnotherBtn_Click(object sender, EventArgs e)
        {
            bool verified = VerifyTask(out int teamID,
                out string taskName,
                out string taskDesc,
                out string milestoneID,
                out string startDate,
                out string endDate,
                out string statusID,
                out string priorityID);

            if (verified)
            {
                bool result = AddTask(teamID, taskName, taskDesc, milestoneID, startDate, endDate, statusID, priorityID);

                if (result)
                {
                    ClearModalInputs();

                    alertLbl.Visible = true;
                    alertLbl.Text = "Successfully Added!";
                    alertLbl.CssClass = "text-success";
                }
                else
                {
                    alertLbl.Visible = true;
                    alertLbl.Text = "Failed to Add Task, Please try again later!";
                    alertLbl.CssClass = "text-danger";
                }
            }

        }
    }
}