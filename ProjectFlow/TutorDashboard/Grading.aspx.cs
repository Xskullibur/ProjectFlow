using ProjectFlow.BLL;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.IO;
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
            HideGroupScore();

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
            bool status = true;

            TextBox editProposalTB = (TextBox)row.FindControl("editProposalTB");
            TextBox editReportTB = (TextBox)row.FindControl("editReportTB");
            TextBox editReviewOneTB = (TextBox)row.FindControl("editReviewOneTB");
            TextBox editReviewTwoTB = (TextBox)row.FindControl("editReviewTwoTB");
            TextBox editPreTB = (TextBox)row.FindControl("editPreTB");
            TextBox editTestTB = (TextBox)row.FindControl("editTestTB");
            TextBox editsdlTB = (TextBox)row.FindControl("editsdlTB");
            TextBox editPartTB = (TextBox)row.FindControl("editPartTB");
            Label scoreLabel = (Label)row.FindControl("scoreLabel");

            try
            {
                int id = int.Parse(scoreLabel.Text);
                float proposal = float.Parse(editProposalTB.Text);
                float report = float.Parse(editReportTB.Text);
                float reviewOne = float.Parse(editReviewOneTB.Text);
                float reviewTwo = float.Parse(editReviewTwoTB.Text);
                float pre = float.Parse(editPreTB.Text);
                float test = float.Parse(editTestTB.Text);
                float sdl = float.Parse(editsdlTB.Text);
                float part = float.Parse(editPartTB.Text);

                if (proposal < 0 || proposal > 5)
                {
                    status = false;
                }

                if (report < 0 || report > 5)
                {
                    status = false;
                }

                if (reviewOne < 0 || reviewOne > 5)
                {
                    status = false;
                }

                if (reviewTwo < 0 || reviewTwo > 15)
                {
                    status = false;
                }

                if (pre < 0 || proposal > 15)
                {
                    status = false;
                }

                if (test < 0 || test > 20)
                {
                    status = false;
                }

                if (sdl < 0 || sdl > 10)
                {
                    status = false;
                }

                if (part < 0 || part > 10)
                {
                    status = false;
                }

                if (status == true)
                {
                    teamMemberBLL.UpdateScore(id, proposal, report, reviewOne, reviewTwo, pre, test, sdl, part);
                    Master.ShowAlert("Student Score Updated Successfully", BootstrapAlertTypes.SUCCESS);
                }
                else
                {
                    Master.ShowAlert("value must be in range", BootstrapAlertTypes.DANGER);
                }
                gradeGV.EditIndex = -1;
                ShowGrade();
               
            }catch(System.FormatException exception)
            {
                gradeGV.EditIndex = -1;
                ShowGrade();
                Master.ShowAlert("value must be a number", BootstrapAlertTypes.DANGER);
            }
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
            bool status = true;

            TextBox editProposalGlTB = (TextBox)row.FindControl("editProposalGTB");
            TextBox editReportGTB = (TextBox)row.FindControl("editReportGTB");
            TextBox editPreGTB = (TextBox)row.FindControl("editPreGTB");

            try
            {
                float proposal = float.Parse(editProposalGlTB.Text);
                float report = float.Parse(editReportGTB.Text);
                float pre = float.Parse(editPreGTB.Text);

                if (proposal < 0 || proposal > 5)
                {
                    status = false;
                }

                if (report < 0 || report > 5)
                {
                    status = false;
                }

                if (pre < 0 || pre > 5)
                {
                    status = false;
                }

                if (status == true)
                {
                    teamMemberBLL.UpdateGroupScore(GetTeamID(), proposal, report, pre);
                    Master.ShowAlert("Group Score Updated Successfully", BootstrapAlertTypes.SUCCESS);
                }
                else
                {
                    Master.ShowAlert("value must be in range", BootstrapAlertTypes.DANGER);
                }

                GroupScoreGV.EditIndex = -1;
                ShowGrade();
                
            }catch(System.FormatException exception)
            {
                GroupScoreGV.EditIndex = -1;
                ShowGrade();
                Master.ShowAlert("value must be a number", BootstrapAlertTypes.DANGER);
            }
        }

        protected void exportBtn_Click(object sender, EventArgs e)
        {           
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=marks.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            ShowGroupScore();
            gradeGV.Columns[15].Visible = false;
            gradeGV.Columns[16].Visible = false;

            gradeGV.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();

            HideGroupScore();
            gradeGV.Columns[15].Visible = true;
            gradeGV.Columns[16].Visible = true;
        }

        public void HideGroupScore()
        {
            gradeGV.Columns[13].Visible = false;
            gradeGV.Columns[11].Visible = false;
            gradeGV.Columns[12].Visible = false;
        }

        public void ShowGroupScore()
        {
            gradeGV.Columns[13].Visible = true;
            gradeGV.Columns[11].Visible = true;
            gradeGV.Columns[12].Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }

        protected void gradeGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gradeGV.PageIndex = e.NewPageIndex;
            ShowGrade();
        }
    }
}