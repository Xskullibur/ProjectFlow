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
        
        protected void Page_Load(object sender, EventArgs e)
        {
           //lbMember2.Text = (string)Session["SSCreatedBy"];
           //lbIssue.Text = (string)Session["SSDesc"];
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Label1.Text = "Yes";
            vote(true);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Label1.Text = "No";
            vote(false);
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int decision = rnd.Next(10);
            if (decision > 5) {
                Label1.Text = "Yes";
                vote(true);
            }
            else
            {
                Label1.Text = "No";
                vote(false);
            }
        }

        protected void vote(bool choice)
        {

            if (Page.IsValid)
            {

                // Create Task Object
                Polling newPoll = new Polling();
                //newPoll.issueID = 1;
                //newPoll.voterID = 4;   //this is a placeholder and needs to be fixed
                //newPoll.vote = choice;      //this is also a placeholder and also needs to be fixed

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);
            }

        }
    }
}