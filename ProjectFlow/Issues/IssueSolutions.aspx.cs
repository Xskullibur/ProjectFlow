using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using ProjectFlow.Utils;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class IssueSolutions : System.Web.UI.Page
    {
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

        protected Guid get_GUID()
        {
            Guid Uid;
            var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
            if (GetCurrentIdentiy().IsStudent)
            {
                Uid = identity.Student.aspnet_Users.UserId;
            }
            else
            {
                Uid = identity.Tutor.aspnet_Users.UserId;
            }
            return Uid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // get solution id from session
            int idSolution = (int)Session["SSSId"];
            if (!IsPostBack)
            {
                // getting identity
                Guid Uid = get_GUID();

                // get solution based on id
                SolutionBLL solutionBLL = new SolutionBLL();
                Solution current_solution = solutionBLL.GetSolutionByID(idSolution);

                // get username
                aspnet_UsersBLL user = new aspnet_UsersBLL();
                aspnet_Users current_user = user.Getaspnet_UsersByUserId(current_solution.createdBy);

                if (current_solution != null)
                {
                    // feed data to the controls
                    DateTime startDate = Convert.ToDateTime(current_solution.startdate);

                    lbSolutionInfo.Text = "Created on " + startDate.Date.ToString("yyyy-MM-dd") + " by " + current_user.UserName.ToString();
                    lbSolutionTitle.Text = "<h3>" + current_solution.title + "</h3>";
                    lbSolutionDesc.Text = current_solution.description;

                    // other checks

                    check();
                    isPublic(current_solution.votePublic);

                    if (Uid == current_solution.createdBy || GetCurrentIdentiy().IsTutor)
                    {
                        deleteSolutionBtn.Enabled = true;
                    }
                }
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            bool vote_check = vote(true);
            if (vote_check == false)
            {
                update(true);
            }
            check();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            bool vote_check = vote(false);
            if (vote_check == false)
            {
                update(false);
            }
            check();
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int decision = rnd.Next(10);
            if (decision > 5)
            {
                bool vote_check = vote(true);
                if (vote_check == false)
                {
                    update(true);
                }
                check();
            }
            else
            {

                bool vote_check = vote(false);
                if (vote_check == false)
                {
                    update(false);
                }
                check();
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

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                Polling newPoll = pollingBLL.GetVoteByID((int)Session["SSSId"], voterId);
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

                // Create Task Object
                Polling newPoll = new Polling();
                newPoll.solutionID = (int)Session["SSSId"];
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

        protected string getUserbySelection(int iID, bool selection)
        {
            PollingBLL pollingBLL = new PollingBLL();
            var myList = pollingBLL.GetVotersBySelection(iID, selection);
            string combindString = string.Join(",", myList);

            return combindString;
        }

        protected void isPublic(bool choice)
        {
            if (choice)
            {
                int idIssue = (int)Session["SSSId"];
                btnNo.ToolTip = getUserbySelection(idIssue, false);
                btnYes.ToolTip = getUserbySelection(idIssue, true);
            }
            
        }

        protected void check()
        {
            int iID = (int)Session["SSSId"];
            PollingBLL pollingBLL = new PollingBLL();
            List<int> result = pollingBLL.GetResult(iID);
            btnYesCount.Text = result[0].ToString();
            btnNoCount.Text = result[1].ToString();

            if (GetCurrentIdentiy().IsTutor)
            {
                // disables voting buttons if isTutor
                btnYes.Enabled = false;
                btnNo.Enabled = false;
                btnRandom.Enabled = false;
                btnYes.Visible = false;
                btnYesCount.Visible = false;
                btnNo.Visible = false;
                btnNoCount.Visible = false;
                btnRandom.Visible = false;
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

        protected void solutionDelete(object sender, EventArgs e)
        {
            SolutionBLL solutionBLL = new SolutionBLL();
            var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;

            // getting identity
            Guid Uid = get_GUID();
            int idSolution = (int)Session["SSSId"];

            // get current solution
            Solution current_solution = solutionBLL.GetSolutionByID(idSolution);
            Session["SSIId"] = current_solution.issueId;

            if (Uid == current_solution.createdBy || GetCurrentIdentiy().IsTutor)
            {
                // delete solution
                solutionBLL.delete(current_solution);

                // redirect
            
                Response.Redirect("../Issues/IssueRes.aspx");
            }
            else
            {
                this.Master.ShowAlert("Enable to delete solution!", BootstrapAlertTypes.DANGER);
            }
        }

    }
}