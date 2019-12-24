using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow
{
    public partial class ProjectMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowProject(1);
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            int tutorID = 1;

            projectBLL.CreateProject(projectID, projectName, projectDesc, tutorID);
        }

        public void ShowProject(int tutorID)
        {           
            ProjectBLL projectBLL = new ProjectBLL();
            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetProjectTutor(tutorID);
            GridView1.DataSource = projectList;
            GridView1.DataBind();
        }
    }
}