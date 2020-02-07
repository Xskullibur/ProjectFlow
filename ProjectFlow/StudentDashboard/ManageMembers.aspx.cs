using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;
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
        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GetTeamID().ToString() != null)
            {
                if (!IsPostBack)
                {
                    showTeam();                   
                    this.SetHeader("Members in my team");
                }
            }
            else
            {
                Response.Redirect("StudentProject.aspx");
            }
        }

        private string getStudentID()
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            return identity.Student.UserId.ToString();
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
            CheckStatus();

            if (member.roleID == 1)
            {
                yourStatus.Text = "Status : Leader";
                STbtn.Enabled = true;
                NameTB.ReadOnly = false;
                changeButton.Enabled = true;
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
                NameTB.ReadOnly = true;
                changeButton.Enabled = false;
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

            NameTB.Text = teamMemberBLL.FindTeam(GetTeamID()).teamName;
        }

        public int GetTeamID()
        {
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        protected void MemberGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = MemberGV.SelectedRow;
            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()), GetTeamID());
            if(int.Parse(row.Cells[0].Text) == member.memberID)
            {
                Master.ShowAlert("You cannot kick yourselft", BootstrapAlertTypes.DANGER);               
            }
            else
            {
                teamMemberBLL.RemoveMember(int.Parse(row.Cells[0].Text));
                Master.ShowAlert("Successfully kicked member", BootstrapAlertTypes.SUCCESS);
                showTeam();
            }
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

        protected void lockBtn_Click(object sender, EventArgs e)
        {
            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()), GetTeamID());
            if (member.roleID == 1)
            {
                projectTeamBLL.lockOneTeam(true, GetTeamID());
                Master.ShowAlert("Team is closed", BootstrapAlertTypes.SUCCESS);
            }
            else
            {
                Master.ShowAlert("Only Leader allowed to make changes", BootstrapAlertTypes.DANGER);
            }          
            showTeam();
        }

        protected void unlockBtn_Click(object sender, EventArgs e)
        {
            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()), GetTeamID());
            if(member.roleID == 1)
            {
                projectTeamBLL.lockOneTeam(false, GetTeamID());
                Master.ShowAlert("Team is open to new members", BootstrapAlertTypes.SUCCESS);
            }
            else
            {
                Master.ShowAlert("Only Leader allowed to make changes", BootstrapAlertTypes.DANGER);
            }
            
            showTeam();
        }

        public void CheckStatus()
        {
            ProjectTeam team = teamMemberBLL.FindTeam(GetTeamID());
            if (team.open == false)
            {
                lockStatus.Text = "Status : Lock";
            }           
            else
            {
                lockStatus.Text = "Status : Unlock";
            }
        }

        protected void leaveBtn_Click(object sender, EventArgs e)
        {
            TeamMember member = studentBLL.getMemberByAdmin(Guid.Parse(getStudentID()), GetTeamID());
            teamMemberBLL.RemoveMember(member.memberID);
            Response.Redirect("StudentProject.aspx");
        }

        protected void changeButton_Click(object sender, EventArgs e)
        {
            if (!NameTB.Text.Equals(""))
            {
                teamMemberBLL.UpdateTeamName(GetTeamID(), NameTB.Text);
                Master.ShowAlert("Name changed successfully", BootstrapAlertTypes.SUCCESS);
            }
        }
    }
}