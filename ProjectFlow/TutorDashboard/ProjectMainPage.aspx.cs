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

namespace ProjectFlow
{
    public partial class ProjectMainPage : System.Web.UI.Page
    {
        TeamMemberBLL teamMemberBLL = new TeamMemberBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                ShowMember();
                this.SetHeader("Team Members that are in this team");
            }
        }
        public int GetTeamID()
        {            
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        public void ShowMember()
        {
            ProjectBLL projectBLL = new ProjectBLL();

            List<TeamMember> memberList = new List<TeamMember> { };
            memberList = projectBLL.GetTeamMember(GetTeamID());
            MemberGV.DataSource = memberList;
            MemberGV.DataBind();

            Dictionary<string, string> studentlist = teamMemberBLL.GetAllStudent();
            studentList.DataSource = studentlist;
            studentList.DataTextField = "Value";
            studentList.DataValueField = "Key";
            studentList.DataBind();
        }

        public void SearchMember(string search)
        {            
            List<TeamMember> memberList = new List<TeamMember> { };
            memberList = teamMemberBLL.SearchMember(GetTeamID(), search);
            MemberGV.DataSource = memberList;
            MemberGV.DataBind();           
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            List<string> errorList = new List<string> { };                       
            int roleID = 0;
                    
            if(studentList.SelectedIndex == -1)
            {
                errorList.Add("Must Select a student");
            }
            else
            {
                foreach(ListItem item in studentList.Items.Cast<ListItem>().Where(x => x.Selected))
                {
                    if (RoleDP.SelectedIndex == 0)
                    {
                        roleID = 2;
                    }
                    else
                    {
                        if (teamMemberBLL.CheckLeaderExist(GetTeamID()))
                        {
                            roleID = 2;
                        }
                        else
                        {
                            roleID = 1;
                        }
                    }
                    errorList = bll.ValidateInsertMember(item.Value.ToString(), GetTeamID(), roleID);
                }
                
            }

            if(errorList.Count > 0)
            {
                string total = "";
                foreach(string error in errorList)
                {
                    total += error;
                }
                errorLabel.Text = total;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateMember').modal('show');", true);
            }
            else
            {                              
                ClearModel();
                ShowMember();
                Master.ShowAlert("Successfully Added Member", BootstrapAlertTypes.SUCCESS);
            }
        }

        protected void MemberGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = MemberGV.Rows[e.RowIndex];
            ProjectBLL projectBLL = new ProjectBLL();
            int role = 1;
            int memberID = int.Parse(row.Cells[0].Text);
            DropDownList editRole = (DropDownList)row.FindControl("editRoleDP");

            if(editRole.SelectedIndex == 0)
            {
                role = 2;
                List<string> errorList = projectBLL.ValidateUpdateMember(memberID, role);
                Master.ShowAlert("Successfully Updated Member", BootstrapAlertTypes.SUCCESS);
            }
            else
            {
                if (teamMemberBLL.CheckLeaderExist(GetTeamID()) == false)
                {
                    role = 1;
                    List<string> errorList = projectBLL.ValidateUpdateMember(memberID, role);
                    Master.ShowAlert("Successfully Updated Member", BootstrapAlertTypes.SUCCESS);
                }
                else
                {
                    Master.ShowAlert("Team can't have 2 leaders", BootstrapAlertTypes.DANGER);
                }              
            }
                     
            MemberGV.EditIndex = -1;            
            ShowMember();
        }

        protected void MemberGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MemberGV.EditIndex = e.NewEditIndex;
            ShowMember();
        }

        protected void MemberGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {                     
            MemberGV.EditIndex = -1;
            ShowMember();
        }

        protected void CreateMemberBtn_Click(object sender, EventArgs e)
        {

        }

        protected void MemberGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MemberGV.PageIndex = e.NewPageIndex;
            ShowMember();
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ClearModel()
        {
            RoleDP.SelectedIndex = 0;
            errorLabel.Text = "";
            CloseModel();
        }

        private void CloseModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#CreateMember').modal('hide')", true);
        }

        private void OpenModel()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateMember').modal('show');", true);
        }

        protected void MemberGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)MemberGV.Rows[e.RowIndex];
            teamMemberBLL.DeleteMember(int.Parse(row.Cells[0].Text));
            ShowMember();
            Master.ShowAlert("Successfully deleted member", BootstrapAlertTypes.SUCCESS);
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectMainPage.aspx");
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchMember(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            ShowMember();
        }

        protected void RoleDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OpenModel();
        }
    }
}