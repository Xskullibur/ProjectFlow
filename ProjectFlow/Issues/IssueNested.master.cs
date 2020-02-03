using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueNested : System.Web.UI.MasterPage
    {

        public event EventHandler refreshGrid;
        // Public Attributes and Methods
        public enum IssueViews
        {
            iAllView,
            iDetailedView,
            iDroppedView
        }

        public void changeSelectedView(IssueViews selectedView)
        {
            switch (selectedView)
            {
                case IssueViews.iAllView:
                    taskViewDDL.SelectedIndex = 0;
                    break;
                case IssueViews.iDetailedView:
                    taskViewDDL.SelectedIndex = 1;
                    break;
                case IssueViews.iDroppedView:
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Task ID
                TaskBLL taskBLL = new TaskBLL();
                ProjectTeam currentTeam = GetCurrentProjectTeam();
                List<Task> ongoingTasks = taskBLL.GetOngoingTasksByTeamId(currentTeam.teamID);
                List<int> taskIdList = new List<int>();

                Dictionary<int, string> TaskDict = new Dictionary<int, string>();

                foreach (Task i in ongoingTasks)
                {
                    taskIdList.Add(i.taskID);
                    TaskDict.Add(i.taskID, i.taskName);
                }

                //TaskIdDLL.DataSource = taskIdList;
                TaskIdDLL.DataSource = TaskDict;
                TaskIdDLL.DataTextField = "Value";
                TaskIdDLL.DataValueField = "Key";
                TaskIdDLL.DataBind();

                // Status
                StatusBLL statusBLL = new StatusBLL();
                Dictionary<int, string> statusDict = statusBLL.Get();

                IssueStatusDLL.DataSource = statusDict;
                IssueStatusDLL.DataTextField = "Value";
                IssueStatusDLL.DataValueField = "Key";

                IssueStatusDLL.DataBind();

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
                bool IsPublic = checkCB();
                ProjectTeam currentTeam = GetCurrentProjectTeam();
                int teamID = currentTeam.teamID;
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                
                if (identity.IsStudent)
                {
                    Guid Uid = identity.Student.aspnet_Users.UserId;
                    // Create Task Object
                    Issue newIssue = new Issue();
                    newIssue.title = tNameTxt.Text;
                    newIssue.description = tDescTxt.Text;
                    newIssue.taskID = Convert.ToInt32(TaskIdDLL.SelectedValue);
                    //newIssue.createdBy = teamID;
                    newIssue.createdBy = teammemberBLL.GetMemIdbyUID(Uid);
                    newIssue.active = true;
                    newIssue.statusID = Convert.ToInt32(IssueStatusDLL.SelectedValue);
                    newIssue.votePublic = IsPublic;

                    // Submit Query
                    IssueBLL issueBLL = new IssueBLL();
                    bool result = issueBLL.Add(newIssue);

                    // Show Result
                    if (result)
                    {
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

        protected bool checkCB()
        {
            if (cbPublic.Checked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void taskViewDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (taskViewDDL.SelectedIndex)
            {
                case 0:
                    Response.Redirect("iAllView.aspx");
                    break;

                case 1:
                    Response.Redirect("iDetailedView.aspx");
                    break;

                case 2:
                    Response.Redirect("iDroppedView.aspx");
                    break;

                default:
                    break;

            }         
        }
    }
}