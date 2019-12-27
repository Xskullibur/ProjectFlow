using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class testIssueComments : System.Web.UI.Page
    {
        int idIssue = 1;
        int idVoter = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            refreshData(idIssue);
        }

        private void refreshData(int id)
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
                newComment.comment = TextBox1.Text;
                newComment.issueID = idIssue;                 
                newComment.createdBy = idVoter;    //this is a placeholder  

                // Submit Query
                CommentForIssueBLL commentBLL = new CommentForIssueBLL();
                bool result = commentBLL.Add(newComment);

                // Show Result
                if (result)
                {
                    refreshData(idIssue);
                }
                else
                {

                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            addComment();
        }
    }
}