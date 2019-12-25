using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueNested : System.Web.UI.MasterPage
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

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

            // Hide Modal
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('hide')", true);
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {

            /*if (Page.IsValid)
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
                    hideModal();
                }
                else
                {

                }
            }*/

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

            return result;
        }
    }
}