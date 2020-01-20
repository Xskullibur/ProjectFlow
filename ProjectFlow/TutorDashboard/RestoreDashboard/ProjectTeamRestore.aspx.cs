using ProjectFlow.BLL;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.TutorDashboard.RestoreDashboard
{
    public partial class ProjectTeamRestore : System.Web.UI.Page
    {
        ProjectBLL projectBLL = new ProjectBLL();
        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassProjectID"] != null)
                {                   
                    DisplayTeam();
                    InfoLabel.Text = "Module: (" + Session["PassProjectID"].ToString() + ") " + Session["PassProjectName"].ToString() + " >>> (Team Restore)";
                }
                else
                {
                    Response.Redirect("../ProjectMenu.aspx");
                }
            }
        }
        public string GetProjectID()
        {
            return Session["PassProjectID"].ToString();
        }

        private void DisplayTeam()
        {
            PageSelectDP.SelectedIndex = 1;
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectBLL.GetDeletedTeam(GetProjectID());
            DeletedTeamGV.DataSource = teamList;
            DeletedTeamGV.DataBind();
        }

        protected void DeletedTeamGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = DeletedTeamGV.SelectedRow;
            int teamID = int.Parse(row.Cells[1].Text);
            projectTeamBLL.RestoreTeam(teamID);
            DisplayTeam();
            Master.ShowAlert("Successfully Restore Team", BootstrapAlertTypes.SUCCESS);
        }

        protected void DeletedTeamGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectTeamRestore.aspx");
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PageSelectDP.SelectedIndex == 0)
            {
                Response.Redirect("../ProjectTeamMenu.aspx");
            }
        }
    }
}