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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "init_selectpicker", "$('.selectpicker').selectpicker();", true);
        }


        /**
         * TASK MANIPULATION
         **/

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

            if (!taskVerified())
            {
                tTitleLbl.Text = "Error";
            }
            else
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

        // Verify Add Task
        private bool taskVerified()
        {
            bool verified = true;
            List<string> tNameErrors = new List<string>();
            List<string> tDescErrors = new List<string>();
            List<string> tMilestoneErrors = new List<string>();
            List<string> tStartDateErrors = new List<string>();
            List<string> tEndDateErrors = new List<string>();
            List<string> tStatusErrors = new List<string>();
            List<string> startEndDateErrors = new List<string>();

            /**
             * Check For Errors
             **/

            // Task Name
            if (string.IsNullOrEmpty(tNameTxt.Text))
            {
                tNameErrors.Add("Task Name Field is Required!");
                verified = false;
            }
            else if (tNameTxt.Text.Length > 255)
            {
                tNameErrors.Add("Maximum Length of 255 Characters!");
                verified = false;
            }

            // Description
            if (string.IsNullOrEmpty(tDescTxt.Text))
            {
                tDescErrors.Add("Description Field is Required!");
                verified = false;
            }
            else if (tDescTxt.Text.Length > 255)
            {
                tDescErrors.Add("Maximum Length of 255 Characters!");
                verified = false;
            }

            // Milestone
            if (milestoneDDL.SelectedIndex == -1)
            {
                tMilestoneErrors.Add("Milestone Field is Required!");
                verified = false;
            }

            // Start Date
            if (string.IsNullOrEmpty(tStartTxt.Text))
            {
                tStartDateErrors.Add("Start Date Field is Required!");
                verified = false;
            }
            else if (!DateTime.TryParse(tStartTxt.Text, out DateTime start))
            {
                tStartDateErrors.Add("Invalid Format!");
                verified = false;
            }

            // End Date
            if (string.IsNullOrEmpty(tEndTxt.Text))
            {
                tEndDateErrors.Add("End Date Field is Required!");
                verified = false;
            }
            else if (!DateTime.TryParse(tEndTxt.Text, out DateTime end))
            {
                tEndDateErrors.Add("Invalid Format!");
                verified = false;
            }

            // Compare Start End Date
            if (tStartDateErrors.Count == 0 && tEndDateErrors.Count  == 0)
            {
                if (DateTime.Compare(DateTime.Parse(tStartTxt.Text), DateTime.Parse(tEndTxt.Text)) >= 0)
                {
                    startEndDateErrors.Add("Start Date cannot be later than End Date!");
                    verified = false;
                }
            }

            // Status
            if (statusDDL.SelectedIndex == -1)
            {
                tStatusErrors.Add("Status Field is Required!");
                verified = false;
            }

            /**
             * Show Error Messages
             **/

            if (!verified)
            {
                // Name
                if (tNameErrors.Count > 0)
                {
                    tNameErrorLbl.Visible = true;
                    tNameErrorLbl.Text = string.Join("<br>", tNameErrors);
                }

                // Description
                if (tDescErrors.Count > 0)
                {
                    tDescErrorLbl.Visible = true;
                    tDescErrorLbl.Text = string.Join("<br>", tDescErrors);
                }

                // Milestone
                if (tMilestoneErrors.Count > 0)
                {
                    tMilestoneErrorLbl.Visible = true;
                    tMilestoneErrorLbl.Text = string.Join("<br>", tMilestoneErrors);
                }

                // Start Date
                if (tStartDateErrors.Count > 0)
                {
                    tStartDateErrorLbl.Visible = true;
                    tStartDateErrorLbl.Text = string.Join("<br>", tStartDateErrors);
                }

                // End Date
                if (tEndDateErrors.Count > 0)
                {
                    tEndDateErrorLbl.Visible = true;
                    tEndDateErrorLbl.Text = string.Join("<br>", tEndDateErrors);
                }

                // Start End Date
                if (startEndDateErrors.Count > 0)
                {
                    startEndDateErrorLbl.Visible = true;
                    startEndDateErrorLbl.Text = string.Join("<br>", startEndDateErrors);
                }

                // Status
                if (tStatusErrors.Count > 0)
                {
                    statusErrorLbl.Visible = true;
                    statusErrorLbl.Text = string.Join("<br>", tStatusErrors);
                }
            }

            return verified;
        }
    }
}