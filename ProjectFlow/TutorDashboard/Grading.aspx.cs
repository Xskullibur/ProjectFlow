using ProjectFlow.BLL;
using ProjectFlow.Utils;
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
            List<Score> studentList = teamMemberBLL.GetGradeByProjectID(GetProjectD());
            gradeGV.DataSource = studentList;
            gradeGV.DataBind();
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
    }
}