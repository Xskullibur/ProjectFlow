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
            ShowProject(1);            
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            ProjectBLL projectBLL = new ProjectBLL();
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            int tutorID = 1;

            //projectBLL.CreateProject(projectID, projectName, projectDesc, tutorID);
            List<string> error = projectBLL.ValidateProject(projectID, projectName, projectDesc, tutorID);

            if(error.Count > 0)
            {
                string total = "";
                foreach(string errorItem in error)
                {
                    total += errorItem;
                }
                errorLabel.Text = total;
                ProjectIdTB.Text = projectID;
                NameTB.Text = projectName;
                DescTB.Text = projectDesc;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "$('#CreateProject').modal('show');", true);
            }
            else
            {
                ClearField();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "$('#CreateProject').modal('hide');", true);
            }
        }

        public void ShowProject(int tutorID)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetProjectTutor(tutorID);
            projectGV.DataSource = projectList;
            projectGV.DataBind();
        }

        protected void projectGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = projectGV.SelectedRow;
            Session["PassProjectID"] = row.Cells[0].Text;
            Response.Redirect("ProjectMainPage.aspx");
        }

        public void ClearField()
        {
            ProjectIdTB.Text = "";
            NameTB.Text = "";
            DescTB.Text = "";
            errorLabel.Text = "";
        }

        protected void newProjectBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}