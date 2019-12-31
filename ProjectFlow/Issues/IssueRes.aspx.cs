using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueRes : System.Web.UI.Page
    {
        int idIssue = 1;
        int idVoter = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbMember.Text = (string)Session["SSName"];
                lbIssue.Text = (string)Session["SSDesc"];
                check(idIssue, idVoter);
                refreshCommentData(idIssue);
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Label1.Text = "Yes";
            vote(true);
            check(idIssue, idVoter);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Label1.Text = "No";
            vote(false);
            check(idIssue, idVoter);
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int decision = rnd.Next(10);
            if (decision > 5) {
                Label1.Text = "Yes";
                vote(true);
                check(idIssue, idVoter);
            }
            else
            {
                Label1.Text = "No";
                vote(false);
                check(idIssue, idVoter);
            }
        }

        protected void vote(bool choice)
        {
            if (Page.IsValid)
            {

                // Create Task Object
                Polling newPoll = new Polling();
                newPoll.issueID = idIssue;    //this is a placeholder and needs to be fixed
                newPoll.voterID = idVoter;    //this is also a placeholder and also needs to be fixed
                newPoll.vote = choice;      

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);
            }
        }

        protected void check(int iID, int vID)
        {
            PollingBLL pollingBLL = new PollingBLL();

            bool checking = pollingBLL.Check(iID, vID);

            int result = pollingBLL.GetResult(iID);
            Label2.Text = result.ToString();

            if (checking == true)
            {
                btnYes.Enabled = false;
                btnNo.Enabled = false;
                btnRandom.Enabled = false;
                Label1.Text = "Already voted!";
            }
            else
            {
                //Label1.Text = checking.ToString();
            }

            //List<int> MyList = pollingBLL.Getcheck(vID);
            //Label1.Text = string.Join(",", MyList);
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

                // Create Task Object
                CommentForIssue newComment = new CommentForIssue();
                newComment.comment = tbComments.Text;
                newComment.issueID = idIssue;
                newComment.createdBy = idVoter;    //this is a placeholder  

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
            addComment();
            tbComments.Text = "";
        }
    }
}