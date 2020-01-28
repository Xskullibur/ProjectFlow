using ProjectFlow.BLL;
using ProjectFlow.Login;
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
    public partial class ProjectMenuRestore : System.Web.UI.Page
    {
        ProjectBLL projectBLL = new ProjectBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            if (identity.IsTutor)
            {
                if (!IsPostBack)
                {
                    Session["TutorID"] = identity.Tutor.UserId.ToString();
                    DisplayProject();
                }
            }
        }

        private void DisplayProject()
        {
            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetDeletedProjectTutor(Guid.Parse(Session["TutorID"].ToString()));
            projectRestoreGV.DataSource = projectList;
            projectRestoreGV.DataBind();
            PageSelectDP.SelectedIndex = 1;
        }

        private void SearchProject(string search)
        {
            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.SearchDeleteProject(Guid.Parse(Session["TutorID"].ToString()), search);
            projectRestoreGV.DataSource = projectList;
            projectRestoreGV.DataBind();
        }

        protected void projectRestoreGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void projectRestoreGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = projectRestoreGV.SelectedRow;
            string projectID = row.Cells[1].Text;
            projectBLL.RestoreProject(projectID);
            DisplayProject();
            Master.ShowAlert("Project Successfully restored", BootstrapAlertTypes.SUCCESS);
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectMenuRestore.aspx");
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PageSelectDP.SelectedIndex == 0)
            {
                Response.Redirect("../ProjectMenu.aspx");
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchProject(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            DisplayProject();
        }
    }
}