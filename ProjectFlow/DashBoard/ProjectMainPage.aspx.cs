using ProjectFlow.BLL;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["PassProjectID"] != null && Session["PassTeamID"] != null) {
                    InfoLabel.Text = "Project ID: " + Session["PassProjectID"].ToString() + " - " + Session["PassProjectName"].ToString()
                                     + " -> Team ID: " + Session["PassTeamID"].ToString() + " - " + Session["PassTeamName"].ToString();
                    ShowMember();
                    PageSelectDP.SelectedIndex = 0;
                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }
            }
        }
        public int GetTeamID()
        {
            return int.Parse(Session["PassTeamID"].ToString());
        }

        public void ShowMember()
        {
            ProjectBLL projectBLL = new ProjectBLL();

            List<TeamMember> memberList = new List<TeamMember> { };
            memberList = projectBLL.GetTeamMember(GetTeamID());
            MemberGV.DataSource = memberList;
            MemberGV.DataBind();
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            List<string> errorList = new List<string> { };
            string studentID = studentIDTB.Text;
            int teamID = int.Parse(Session["PassTeamID"].ToString());
            int roleID = int.Parse(RoleIDTB.Text);

            errorList = bll.ValidateInsertMember(studentID, teamID, roleID);
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

            int memberID = int.Parse(row.Cells[0].Text);
            TextBox editRole = (TextBox)row.FindControl("editRoleTB");
           
            List<string> errorList = projectBLL.ValidateUpdateMember(memberID, int.Parse(editRole.Text));
                     
            MemberGV.EditIndex = -1;
            Master.ShowAlert("Successfully Updated Member", BootstrapAlertTypes.SUCCESS);
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
            if(PageSelectDP.SelectedIndex == 1)
            {
                Response.Redirect("addMilestone.aspx");
            }
        }

        private void ClearModel()
        {
            studentIDTB.Text = "";
            RoleIDTB.Text = "";
            errorLabel.Text = "";
        }
    }
}