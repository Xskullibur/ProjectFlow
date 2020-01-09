using ProjectFlow.BLL;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassProjectID"] != null)
                {
                    Session["PassTeamID"] = null;
                    Session["PassTeamName"] = null;
                    ShowTeam(Session["PassProjectID"].ToString());
                    InfoLabel.Text = "Project ID: " + Session["PassProjectID"].ToString() + " - " + Session["PassProjectName"].ToString();
                }
                else
                {
                    Response.Redirect("ProjectMenu.aspx");
                }
            }              
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
                Response.Redirect("ProjectTeamMenu.aspx");
            }
            
        }

        public void ShowTeam(string ProjectID)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            if (projectBLL.CheckProjectTeamExist(ProjectID))
            {
                List<ProjectTeam> teamList = new List<ProjectTeam> { };
                teamList = projectBLL.GetProjectTeam(ProjectID);
                TeamGV.DataSource = teamList;
                TeamGV.DataBind();
            }           
        }

        protected void TeamGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TeamGV.EditIndex = -1;
            ShowTeam(Session["PassProjectID"].ToString());
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
            ShowTeam(Session["PassProjectID"].ToString());
        }

        protected void TeamGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = TeamGV.SelectedRow;
            Session["PassTeamID"] = row.Cells[0].Text;
            Session["PassTeamName"] = row.Cells[1].Text;
            Response.Redirect("ProjectMainPage.aspx");
        }

        protected void TeamGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TeamGV.EditIndex = e.NewEditIndex;
            ShowTeam(Session["PassProjectID"].ToString());
        }

        protected void TeamGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TeamGV.PageIndex = e.NewPageIndex;
            ShowTeam(Session["PassProjectID"].ToString());
        }
    }
}