using ProjectFlow.BLL;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.DashBoard
{
    public partial class ProjectTeamMenu : System.Web.UI.Page
    {
        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassProjectID"] != null)
                {
                    Session["PassTeamID"] = null;
                    Session["PassTeamName"] = null;
                    ShowTeam();
                    InfoLabel.Text = "Module: (" + Session["PassProjectID"].ToString() + ") " + Session["PassProjectName"].ToString() + " >>> (Team Select)";
                }
                else
                {
                    Response.Redirect("ProjectMenu.aspx");
                }
            }              
        }
        public string GetProjectID()
        {
            return Session["PassProjectID"].ToString();
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            string teamName = NameTB.Text;
            string desc = DescTB.Text;
            List<string> errorList = new List<string> { };


            errorList = bll.InsertProjectTeam(teamName, desc, Session["PassProjectID"].ToString());
            if (errorList.Count > 0)
            {
                string total = "";
                foreach (string errorItem in errorList)
                {
                    total += errorItem;
                }
                errorLabel.Text = total;
                NameTB.Text = teamName;
                DescTB.Text = desc;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateTeam').modal('show');", true);
            }
            else
            {
                Master.ShowAlert("Successfully Created Team", BootstrapAlertTypes.SUCCESS);
                ClearModel();
                ShowTeam();
            }
            
        }

        public void ShowTeam()
        {
            ProjectBLL projectBLL = new ProjectBLL();
            
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectBLL.GetProjectTeam(GetProjectID());
            TeamGV.DataSource = teamList;
            TeamGV.DataBind();
        }

        protected void TeamGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TeamGV.EditIndex = -1;
            ShowTeam();
        }

        protected void TeamGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = TeamGV.Rows[e.RowIndex];          
            ProjectBLL projectBLL = new ProjectBLL();

            int teamID = int.Parse(row.Cells[0].Text);
            TextBox editName = (TextBox)row.FindControl("editNameTB");
            TextBox editDesc = (TextBox)row.FindControl("editDescTB");
                    
            List<string> error = projectBLL.UpdateProjectTeam(teamID, editName.Text, editDesc.Text);
            TeamGV.EditIndex = -1;
            Master.ShowAlert("Successfully Updated Team", BootstrapAlertTypes.SUCCESS);
            ShowTeam();
        }

        protected void TeamGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = TeamGV.SelectedRow;
            Label name = (Label)row.FindControl("nameLabel");

            Session["PassTeamID"] = row.Cells[0].Text;
            Session["PassTeamName"] = name.Text;
            Response.Redirect("ProjectMainPage.aspx");
        }

        protected void TeamGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TeamGV.EditIndex = e.NewEditIndex;
            ShowTeam();
        }

        protected void TeamGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TeamGV.PageIndex = e.NewPageIndex;
            ShowTeam();
        }

        private void ClearModel()
        {
            NameTB.Text = "";
            DescTB.Text = "";
            errorLabel.Text = "";
        }

        protected void TeamGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)TeamGV.Rows[e.RowIndex];
            projectTeamBLL.DeleteTeam(int.Parse(row.Cells[0].Text));
            ShowTeam();
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectTeamMenu.aspx");
        }
    }
}