using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using ProjectFlow.Utils;

namespace ProjectFlow.Issues
{
    public partial class IssueRes : System.Web.UI.Page
    {
        
        string ispublic;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this);
            int idIssue = (int)Session["SSIId"];
            //ScriptManager1.RegisterAsyncPostBackControl(tSaveBtn);
            if (!IsPostBack)
            {
                //Databinding non responsive elements
                IssueBLL issueBLL = new IssueBLL();
                Issue updated_issue = issueBLL.GetIssueByID(idIssue);

                //Set header
                this.SetHeader("Issue: " + updated_issue.issueID);

                lbMember.Text = "<h3>"+ updated_issue.title + "</h3>";
                lbIssue.Text = updated_issue.description;
                IssueActive.Text = updated_issue.active.ToString();
                IssuePublic.Text = updated_issue.votePublic.ToString();
                TeamMemberBLL teammember = new TeamMemberBLL();
                IssueRaisedBy.Text = teammember.GetUsernamebyMID(updated_issue.createdBy);
                StatusBLL status = new StatusBLL();
                Status currentstat = status.GetStatusByID(updated_issue.statusID.GetValueOrDefault());
                IssueStatus.Text = currentstat.status1;

                

                ispublic = updated_issue.votePublic.ToString();
                check();
                refreshCommentData(idIssue);
                isActive(updated_issue.active);
                isPublic(ispublic);
                refreshData();
            }
            solutionView.Font.Size = 11;
        }

        // Get Current User
        public ProjectFlowIdentity GetCurrentIdentiy()
        {
            var projectFlowIdentity = HttpContext.Current.User.Identity as ProjectFlowIdentity;

            return projectFlowIdentity;
        }

        // Get Current Project
        public ProjectTeam GetCurrentProjectTeam()
        {
            ServicesWithContent servicesWithContent = Master as ServicesWithContent;

            return servicesWithContent.CurrentProjectTeam;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            bool vote_check = vote(true);
            if (vote_check == false)
            {
                update(true);
            }
            check();
            isPublic(ispublic);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            bool vote_check = vote(false);
            if (vote_check == false)
            {
                update(false);
            }
            check();
            isPublic(ispublic);
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int decision = rnd.Next(10);
            if (decision > 5) {
                bool vote_check = vote(true);
                if (vote_check == false)
                {
                    update(true);
                }
                check();
                isPublic(ispublic);
            }
            else
            {

                bool vote_check = vote(false);
                if (vote_check == false)
                {
                    update(false);
                }
                check();
                isPublic(ispublic);
            }
        }
        
        protected void update(bool choice)
        {
            if (Page.IsValid)
            {
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);

                // Create Task Object
                /*Polling newPoll = new Polling();
                newPoll.issueID = (int)Session["SSIId"];
                newPoll.voterID = voterId;
                newPoll.vote = choice;*/

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                Polling newPoll = pollingBLL.GetVoteByID((int)Session["SSIId"], voterId);
                newPoll.vote = choice;
                bool result = pollingBLL.Update(newPoll);
            }
        }

        protected bool vote(bool choice)
        {
            if (Page.IsValid)
            {
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);

                // Creaddte Task Object
                Polling newPoll = new Polling();
                newPoll.solutionID = (int)Session["SSIId"];
                newPoll.voterID = voterId;
                newPoll.vote = choice;

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);
                return result;
            }
            else
            {
                return false;
            }            
        }

        protected void check()
        {
            int iID = (int)Session["SSIId"];
            PollingBLL pollingBLL = new PollingBLL();
            List<int> result = pollingBLL.GetResult(iID);


            if (GetCurrentIdentiy().IsTutor)
            {

            }
            else
            {               
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                //the above only checks for the user id if it is a student, tutors must also be accounted for
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);
                bool checking = pollingBLL.Check(iID, voterId);

                if (checking == true)
                {
                    //btnYes.Enabled = false;
                    //btnNo.Enabled = false;
                    //btnRandom.Enabled = false;
                    this.Master.ShowAlert("You have already voted!", BootstrapAlertTypes.DANGER);
                }
                else
                {

                }
            }
        }

        private void refreshCommentData(int id)
        {
            CommentForIssueBLL commentForIssueBLL = new CommentForIssueBLL();

            Repeater1.DataSource = commentForIssueBLL.GetCommentByIssueId(id);
            Repeater1.DataBind();

        }

        protected void addComment()
        {
            int idIssue = (int)Session["SSIId"];
            if (Page.IsValid)
            {
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);

                // Create Task Object
                CommentForIssue newComment = new CommentForIssue();
                newComment.comment = tbComments.Text;
                newComment.issueID = idIssue;
                newComment.createdBy = voterId;    //this is a placeholder  

                // Submit Query
                CommentForIssueBLL commentBLL = new CommentForIssueBLL();
                bool result = commentBLL.Add(newComment);

                // Show Result
                if (result)
                {
                    refreshCommentData(idIssue);
                }
                else
                {

                }
            }

        }

        protected void btnCommentSubmit_Click(object sender, EventArgs e)
        {
            if(tbComments.Text != "")
            {
                addComment();
                tbComments.Text = "";
            }
            else
            {
                //tbComments.ToolTip = "Please enter a comment";
            }
        }

        protected string getUserbySelection(int iID, bool selection)
        {
            PollingBLL pollingBLL = new PollingBLL();
            var myList = pollingBLL.GetVotersBySelection(iID, selection);
            string combindString = string.Join(",", myList);

            return combindString;
        }

        protected void isPublic(string choice)
        {
            int idIssue = (int)Session["SSIId"];
            if (choice == "True")
            {

            } else
            {

            }
        }

        protected void isActive(bool active)
        {
            if (active == true)
            {
                //todo
            }
            else
            {
                disablecomments();

                this.Master.ShowAlert("Issue has been dropped", BootstrapAlertTypes.DANGER);
            }
        }

        protected void disablecomments()
        {
            tbComments.Visible = false;
            tbComments.Enabled = false;
            btnComment.Visible = false;
            btnComment.Enabled = false;
        }

        // Add Task OnClick Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal1", "$('#taskModal1').modal('show')", true);
        }

        private void hideModal()
        {
            // Clear Fields
            tNameTxt.Text = string.Empty;
            tDescTxt.Text = string.Empty;

            // Hide Modal
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal1", "$('#taskModal1').modal('hide')", true);
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

        protected void addTask_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // calling various stuff
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                SolutionBLL solutionBLL = new SolutionBLL();

                // getting data
                string title = tNameTxt.Text;
                string description = tDescTxt.Text;
                int id = (int)Session["SSIId"];
                DateTime currentTime = DateTime.Now;
                Guid Uid = identity.Student.aspnet_Users.UserId;

                // bind date to new solution
                Solution new_solution = new Solution();
                new_solution.title = title;
                new_solution.description = description;
                new_solution.issueId = id;
                new_solution.startdate = currentTime;
                new_solution.createdBy = Uid;
                new_solution.votePublic = checkCB();

                // clear error messages
                // TODO

                // submit solution
                bool submission = solutionBLL.Add(new_solution);

                // alert
                if (submission)
                {
                    //NotificationHelper.Default_TaskUpdate_Setup(id);
                    this.Master.ShowAlertWithTiming("Solution Successfully Added!", BootstrapAlertTypes.SUCCESS, 2000);
                }
                else
                {
                    this.Master.ShowAlert("Failed to add Solution!", BootstrapAlertTypes.DANGER);
                }

                // hide modal;
                hideModal();
                refreshData();
            }
        }

        private void refreshData()
        {
            // calling various stuff
            int issueId = (int)Session["SSIId"];
            SolutionBLL solutionBLL = new SolutionBLL();

            // binding data to table
            solutionView.DataSource = solutionBLL.GetSolutionByIssueId(issueId);
            solutionView.DataBind();
        }

        protected void showSolution_click(object sender, EventArgs e)
        {
            GridViewRow row = solutionView.SelectedRow;
            Session["SSSId"] = int.Parse(row.Cells[0].Text);
            Response.Redirect("../Issues/IssueSolutions.aspx");
        }

        protected void IssueDelete(object sender, EventArgs e)
        {

            // Get Current Project Team
            ProjectTeam currentTeam = GetCurrentProjectTeam();

            // Selected Issue ID
            int id = (int)Session["SSIId"];

            // Delete Task
            IssueBLL issueBLL = new IssueBLL();
            bool result = issueBLL.Drop(id);

            if (result)
            {
                Response.Redirect("../Issues/iAllView.aspx");
                //this.Master.Master.ShowAlertWithTiming("Issue Successfully Dropped!", BootstrapAlertTypes.SUCCESS, 2000);
            }
            else
            {
                this.Master.ShowAlert("Failed to Drop Task", BootstrapAlertTypes.DANGER);
            }
        }

        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Repeater rptDemo = sender as Repeater; // Get the Repeater control object.

            // If the Repeater contains no data.
            if (Repeater1 != null && Repeater1.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    // Show the Error Label (if no data is present).
                    Label lblErrorMsg = e.Item.FindControl("lblErrorMsg") as Label;
                    if (lblErrorMsg != null)
                    {
                        lblErrorMsg.Visible = true;
                    }
                }
            }
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //Make gridview row clickable
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(solutionView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Go to this room";
            }
        }

    }
}