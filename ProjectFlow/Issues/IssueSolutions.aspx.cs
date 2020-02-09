using ProjectFlow.FileManagement;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.IO;
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
                // calling tutorbll
                TutorBLL tutorBLL = new TutorBLL();

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

                    //creating button for file download
                    if(current_solution.associatedFile != null)
                    {

                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\" +  "(PLAIN)" + current_solution.associatedFile))
                        {
                            Button1.Enabled = true;
                            Button1.Visible = true;
                            Button1.Text = current_solution.associatedFile;
                        }

                    }

                    // other checks

                    check();
                    isPublic(current_solution.votePublic);

                    if (Uid == current_solution.createdBy || GetCurrentIdentiy().IsTutor)
                    {
                        deleteSolutionBtn.Visible = true;
                        deleteSolutionBtn.Enabled = true;
                    }

                    if (tutorBLL.CheckTutorByUID(current_solution.createdBy) == true)
                    {
                        //disable vote if solution is creaeted by teacher
                        disable_vote();
                    }
                }
            }

            // just checking again
            //updateIfPass();
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
                Guid Uid = get_GUID();

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                Polling newPoll = pollingBLL.GetVoteByID((int)Session["SSSId"], Uid);
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
                Guid Uid = get_GUID();

                // Create Task Object
                Polling newPoll = new Polling();
                newPoll.solutionID = (int)Session["SSSId"];
                newPoll.voterID = Uid;
                newPoll.vote = choice;

                // Submit Query
                PollingBLL pollingBLL = new PollingBLL();
                bool result = pollingBLL.Add(newPoll);

                updateIfPass();

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
                //disable_vote();
            }
            else
            {
                var identity = HttpContext.Current.User.Identity as ProjectFlowIdentity;
                TeamMemberBLL teammemberBLL = new TeamMemberBLL();
                Guid Uid = identity.Student.aspnet_Users.UserId;
                //the above only checks for the user id if it is a student, tutors must also be accounted for
                int voterId = teammemberBLL.GetMemIdbyUID(Uid);
                bool checking = pollingBLL.Check(iID, Uid);

                if (checking == true)
                {
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

        protected void updateIfPass()
        {
            // getting stuff
            Guid Uid = get_GUID();
            int idSolution = (int)Session["SSSId"];
            ProjectTeam currentTeam = GetCurrentProjectTeam();
            int currentTeamId = currentTeam.teamID;

            SolutionBLL solution = new SolutionBLL();
            bool votePass = solution.getPass(currentTeamId, idSolution);

            Solution current_solution = solution.GetSolutionByID(idSolution);


            if (votePass)
            {
                current_solution.success = true;

            }
            else
            {
                current_solution.success = false;
            }
            solution.Update(current_solution);
        }

        protected void disable_vote()
        {
            btnYes.Enabled = false;
            btnNo.Enabled = false;
            btnRandom.Enabled = false;
            btnYes.Visible = false;
            btnYesCount.Visible = false;
            btnNo.Visible = false;
            btnNoCount.Visible = false;
            btnRandom.Visible = false;
        }

        protected void GoBackEvent(object sender, EventArgs e)
        {
            Response.Redirect("IssueRes.aspx");
        }

        //file download stuff

        private void DownloadFile(string FileName, string Path)
        {
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AddHeader("Content-Disposition", "filename=" + FileName);
            Response.TransmitFile(Path + FileName);
            Response.Flush();
            Response.End();
        }

        protected void file_download(string fileName)
        {
            string storagePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\";
            DownloadFile("(PLAIN)" + fileName, storagePath);
        }

        public int GetTeamID()
        {
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        private Button CreateFilterButton(string filterName, EventHandler eventHandler)
        {
            Button linkButton = new Button();

            linkButton.ID = $"filter_{filterName}";
            linkButton.Text = $"{filterName} <i class='fa fa-close ml-2'></i>";
            linkButton.CssClass = "btn btn-danger my-4 mr-1";
            linkButton.Click += eventHandler;

            return linkButton;
        }

        // this needs to be edited, the download functio does not work
        private void file_link(string taskName)
        {
            Button linkButton = CreateFilterButton(taskName, download_Click);
            currentFiltersPanel.Controls.Add(linkButton);
        }

        protected void download_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            file_download(button.Text);
        }
    }
}