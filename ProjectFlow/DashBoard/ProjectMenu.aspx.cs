using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.DashBoard
{
    public partial class ProjectMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid tutorUserId = Guid.Parse("5863511C-849B-443D-AA95-CFCE7DDAEBE3");
            ShowProject(tutorUserId);
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            Guid tutorUserId = Guid.Parse("5863511C-849B-443D-AA95-CFCE7DDAEBE3");

            projectBLL.CreateProject(projectID, projectName, projectDesc, tutorUserId);
        }

        public void ShowProject(Guid tutorUserId)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetProjectTutor(tutorUserId);
            projectGV.DataSource = projectList;
            projectGV.DataBind();
        }

        protected void projectGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = projectGV.SelectedRow;
            Session["PassProjectID"] = row.Cells[0].Text;
            Response.Redirect("ProjectMainPage.aspx");
        }
    }
}