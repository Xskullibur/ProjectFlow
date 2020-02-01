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

namespace ProjectFlow.Issues
{
    public partial class IssueRes : System.Web.UI.Page
    {
        
        int idIssue;
        string ispublic;
        protected void Page_Load(object sender, EventArgs e)
        {
            idIssue = (int)Session["SSIId"];
            if (!IsPostBack)
            {
                
                IssueBLL issueBLL = new IssueBLL();
                Issue updated_issue = issueBLL.GetIssueByID(idIssue);
                lbMember.Text = "<h3>"+ updated_issue.title + "</h3>";
                lbIssue.Text = updated_issue.description;
                IssueActive.Text = "";
                IssuePublic.Text = updated_issue.votePublic.ToString();
                IssueRaisedBy.Text = updated_issue.createdBy.ToString();
                IssueStatus.Text = "";
                

                ispublic = (string)Session["SSIsPublic"];
                check(idIssue);
                refreshCommentData(idIssue);
                //Label1.ToolTip = getUserbySelection(idIssue, false); // this works but needs to be removed
                isPublic(ispublic);
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            idIssue = (int)Session["SSIId"];

            vote(true);
            check(idIssue);
            isPublic(ispublic);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            idIssue = (int)Session["SSIId"];
            vote(false);
            check(idIssue);
            isPublic(ispublic);
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int decision = rnd.Next(10);
            if (decision > 5) {
                vote(true);
                check(idIssue);
                isPublic(ispublic);
            }
            else
            {

                vote(false);
                check(idIssue);
                isPublic(ispublic);
            }
        }

        protected void vote(bool choice)
        {
            if (Page.IsValid)
            {
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);

                // Create Task Object
                Polling newPoll = new Polling();
                newPoll.issueID = idIssue;    
                newPoll.voterID = voterId;    
                newPoll.vote = choice;      

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);
            }
        }

        protected void check(int iID)
        {
            PollingBLL pollingBLL = new PollingBLL();
            var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
            TeamMemberBLL teammemberBLL = new TeamMemberBLL();
            Guid Uid = identity.Student.aspnet_Users.UserId;
            int voterId = teammemberBLL.GetMemIdbyUID(Uid);

            bool checking = pollingBLL.Check(iID, voterId);

            List<int> result = pollingBLL.GetResult(iID);
            
            btnYesCount.Text = result[0].ToString();
            btnNoCount.Text = result[1].ToString();

            if (checking == true)
            {
                btnYes.Enabled = false;
                btnNo.Enabled = false;
                btnRandom.Enabled = false;
                this.Master.ShowAlert("You have already voted!", BootstrapAlertTypes.DANGER);
            }
            else
            {

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
            //IssueBLL issueBLL = new IssueBLL();
            //bool isPub = issueBLL.isPublic(iID);
            if (choice == "True")
            {
                btnNo.ToolTip = getUserbySelection(idIssue, false);
                btnYes.ToolTip = getUserbySelection(idIssue, true);
            } else
            {

            }
        }
    }
}