using ProjectFlow.BLL;
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

        }

        private int GetTeamID()
        {           
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        private void ShowGrade()
        {
            List<TeamMember> studentList = teamMemberBLL.GetUserGradesByTeamID(GetTeamID());
            gradeGV.DataSource = studentList;
            gradeGV.DataBind();
        }

        protected void MemberGV_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gradeGV_RowEditing(object sender, GridViewEditEventArgs e)
        {

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
    }
}