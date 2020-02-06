using ProjectFlow.BLL;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.TutorDashboard
{
    public partial class Grading : System.Web.UI.Page
    {
        TeamMemberBLL teamMemberBLL = new TeamMemberBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(GetProjectD() != null && GetTeamID().ToString() != null)
                {
                    ShowGrade();
                    this.SetHeader("Student's Grades");
                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }              
            }
        }

        private int GetTeamID()
        {           
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        private string GetProjectD()
        {
            return (Master as ServicesWithContent).CurrentProject.projectID;
        }

        private void ShowGrade()
        {
            List<Score> studentList = teamMemberBLL.GetGradeByTeamID(GetTeamID());
            gradeGV.DataSource = studentList;
            gradeGV.DataBind();

            GroupScoreGV.DataSource = studentList;
            GroupScoreGV.DataBind();
            for(int i = 1; i < studentList.Count; i++)
            {
                GridViewRow row = GroupScoreGV.Rows[i];
                row.Visible = false;
            }
        }

        public string CheckFailure(double score)
        {
            if(score >= 80)
            {
                return "<i style=\"color: green;\" class=\"fas fa-lg fa-trophy\"></i>";
            }
            else if (score >= 50 && score < 80)
            {
                return "<i style=\"color: green;\" class=\"fas fa-lg fa-check-circle\"></i>";
            }
            else
            {
                return "<i style=\"color: red;\" class=\"fas fa-lg fa-exclamation-triangle\"></i>";
            }
        }

        protected void MemberGV_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gradeGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gradeGV.EditIndex = e.NewEditIndex;
            ShowGrade();
        }

        protected void gradeGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gradeGV.Rows[e.RowIndex];

            TextBox editProposalTB = (TextBox)row.FindControl("editProposalTB");
            TextBox editReportTB = (TextBox)row.FindControl("editReportTB");
            TextBox editReviewOneTB = (TextBox)row.FindControl("editReviewOneTB");
            TextBox editReviewTwoTB = (TextBox)row.FindControl("editReviewTwoTB");
            TextBox editPreTB = (TextBox)row.FindControl("editPreTB");
            TextBox editTestTB = (TextBox)row.FindControl("editTestTB");
            TextBox editsdlTB = (TextBox)row.FindControl("editsdlTB");
            TextBox editPartTB = (TextBox)row.FindControl("editPartTB");
            Label scoreLabel = (Label)row.FindControl("scoreLabel");

            int id = int.Parse(scoreLabel.Text);
            float proposal = float.Parse(editProposalTB.Text);
            float report = float.Parse(editReportTB.Text);
            float reviewOne = float.Parse(editReviewOneTB.Text);
            float reviewTwo = float.Parse(editReviewTwoTB.Text);
            float pre = float.Parse(editPreTB.Text);
            float test = float.Parse(editTestTB.Text);
            float sdl = float.Parse(editsdlTB.Text);
            float part = float.Parse(editPartTB.Text);

            teamMemberBLL.UpdateScore(id, proposal, report, reviewOne, reviewTwo, pre, test, sdl, part);

            gradeGV.EditIndex = -1;
            ShowGrade();
            Master.ShowAlert("Student Score Updated Successfully", BootstrapAlertTypes.SUCCESS);
        }

        protected void gradeGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            ShowGrade();
        }

        protected void gradeGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gradeGV.EditIndex = -1;
            ShowGrade();
        }

        protected void GroupScoreGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GroupScoreGV.EditIndex = e.NewEditIndex;
            ShowGrade();
        }

        protected void GroupScoreGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GroupScoreGV.EditIndex = -1;
            ShowGrade();
        }

        protected void GroupScoreGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GroupScoreGV.Rows[e.RowIndex];

            TextBox editProposalGlTB = (TextBox)row.FindControl("editProposalGTB");
            TextBox editReportGTB = (TextBox)row.FindControl("editReportGTB");
            TextBox editPreGTB = (TextBox)row.FindControl("editPreGTB");
                      
            float proposal = float.Parse(editProposalGlTB.Text);
            float report = float.Parse(editReportGTB.Text);
            float pre = float.Parse(editPreGTB.Text);

            teamMemberBLL.UpdateGroupScore(GetTeamID(), proposal, report, pre);

            GroupScoreGV.EditIndex = -1;
            ShowGrade();
            Master.ShowAlert("Group Score Updated Successfully", BootstrapAlertTypes.SUCCESS);
        }              
    }
}