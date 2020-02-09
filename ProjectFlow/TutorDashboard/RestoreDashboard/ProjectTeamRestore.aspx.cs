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

namespace ProjectFlow.TutorDashboard.RestoreDashboard
{
    public partial class ProjectTeamRestore : System.Web.UI.Page
    {
        ProjectBLL projectBLL = new ProjectBLL();
        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayTeam();
            this.SetHeader("Teams that I can Restore");
        }
        public string GetProjectID()
        {
            return (Master as ServicesWithContent).CurrentProject.projectID;
        }

        private void DisplayTeam()
        {
            PageSelectDP.SelectedIndex = 1;
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectBLL.GetDeletedTeam(GetProjectID());
            DeletedTeamGV.DataSource = teamList;
            DeletedTeamGV.DataBind();
        }

        private void SearchTeam(string search)
        {
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectTeamBLL.SearchDeletedTeam(GetProjectID(), search);
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
            if(PageSelectDP.SelectedIndex == 1)
            {
                Response.Redirect("../ProjectTeamMenu.aspx");
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchTeam(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            DisplayTeam();
        }
    }
}