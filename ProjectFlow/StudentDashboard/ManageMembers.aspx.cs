using ProjectFlow.BLL;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
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
        TeamMemberBLL teamMemberBLL = new TeamMemberBLL();
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

            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()), GetTeamID());
            int totalRow = MemberGV.Rows.Count;

            if (member.roleID == 1)
            {
                yourStatus.Text = "Status : Leader";
                STbtn.Enabled = true;
                for (int i = 0; i < totalRow; i++)
                {
                    GridViewRow row = MemberGV.Rows[i];
                    Button deleteBtn = (Button)row.FindControl("deleteBtn");                                          
                    deleteBtn.Visible = true;                   
                }                
            }
            else
            {
                yourStatus.Text = "Status : Member";
                STbtn.Enabled = false;
                for (int i = 0; i < totalRow; i++)
                {
                    GridViewRow row = MemberGV.Rows[i];
                    Button deleteBtn = (Button)row.FindControl("deleteBtn");
                    deleteBtn.Visible = false;
                }
            }

            if (studentBLL.gotLeader(GetTeamID()))
            {
                leaderBtn.Enabled = false;
            }
            
        }

        public int GetTeamID()
        {
            return int.Parse(Session["StudentTeamID"].ToString());
        }

        protected void MemberGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void leaderBtn_Click(object sender, EventArgs e)
        {
            teamMemberBLL.ToLeader(Guid.Parse(getStudentID()), GetTeamID());
            Master.ShowAlert("Successfully become leader", BootstrapAlertTypes.SUCCESS);
            showTeam();
        }

        protected void STbtn_Click(object sender, EventArgs e)
        {
            teamMemberBLL.ToMember(Guid.Parse(getStudentID()),GetTeamID());
            showTeam();
            leaderBtn.Enabled = true;
            Master.ShowAlert("Successfully Stepped down", BootstrapAlertTypes.SUCCESS);
        }
    }
}