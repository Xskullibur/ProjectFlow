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
            //lbMember2.Text = (string)Session["SSCreatedBy"];
            //lbIssue.Text = (string)Session["SSDesc"];
            check("{ issueID = 1 }", idVoter);
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
                newPoll.issueID = idIssue;    //this is a placeholder and needs to be fixed
                newPoll.voterID = idVoter;    //this is also a placeholder and also needs to be fixed
                newPoll.vote = choice;      

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);
            }
        }

        protected void check(string iID, int vID)
        {
            PollingBLL pollingBLL = new PollingBLL();

            bool checking = pollingBLL.Check(iID, vID);
            
            if(checking == true)
            {
                btnYes.Enabled = false;
                btnNo.Enabled = false;
                btnRandom.Enabled = false;
                Label1.Text = checking.ToString();
            }
            else
            {
                Label1.Text = checking.ToString();
            }

            List<object> MyList = pollingBLL.Getcheck(vID);
            Label1.Text = string.Join(",", MyList);
        }
    }
}