using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.StudentDashboard
{
    public partial class ManageMembers : System.Web.UI.Page
    {
        StudentBLL studentBLL = new StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentTeamID"] != null)
            {
                if (!IsPostBack)
                {
                    showTeam();
                }
            }
            else
            {
                Response.Redirect("StudentProject.aspx");
            }
        }

        private string getStudentID()
        {
            return Session["StudentID"].ToString();
        }

        private void showTeam()
        {
            ProjectBLL projectBLL = new ProjectBLL();

            List<TeamMember> memberList = new List<TeamMember> { };
            memberList = projectBLL.GetTeamMember(GetTeamID());
            MemberGV.DataSource = memberList;
            MemberGV.DataBind();

            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()));
            int totalRow = MemberGV.Rows.Count;

            if (member.roleID == 1)
            {
                yourStatus.Text = "Status : Leader";               
                
                for(int i = 0; i < totalRow; i++)
                {
                    GridViewRow row = MemberGV.Rows[i];
                    Button deleteBtn = (Button)row.FindControl("deleteBtn");
                    deleteBtn.Visible = true;
                }                             
            }
            else
            {
                yourStatus.Text = "Status : Member";
                for (int i = 0; i < totalRow; i++)
                {
                    GridViewRow row = MemberGV.Rows[i];
                    Button deleteBtn = (Button)row.FindControl("deleteBtn");
                    deleteBtn.Visible = false;
                }
            }
            
        }

        public int GetTeamID()
        {
            return int.Parse(Session["StudentTeamID"].ToString());
        }

        protected void MemberGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}