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

        // Public Attributes and Methods
        public enum IssueViews
        {
            iDetailedView,
            iDroppedView
        }

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

            if (Page.IsValid)
            {

                // Create Task Object
                Issue newIssue = new Issue();
                newIssue.title = tNameTxt.Text;
                newIssue.description = tDescTxt.Text;
                newIssue.taskID = 5;                    //this is a placeholder
                newIssue.createdBy = TEST_TEAM_ID;      //this is also a placeholder

                // Submit Query
                IssueBLL issueBLL = new IssueBLL();
                bool result = issueBLL.Add(newIssue);

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

            return result;
        }

        protected void taskViewDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (taskViewDDL.SelectedIndex)
            {

                case 0:
                    Response.Redirect("iDetailedView.aspx");
                    break;

                case 1:
                    Response.Redirect("iDroppedView.aspx");
                    break;

                default:
                    break;

            }
        }

        public void changeSelectedView(IssueViews selectedView)
        {
            switch (selectedView)
            {
                case IssueViews.iDetailedView:
                    taskViewDDL.SelectedIndex = 0;
                    break;
                case IssueViews.iDroppedView:
                    taskViewDDL.SelectedIndex = 1;
                    break;
                default:
                    break;
            }
        }
    }
}