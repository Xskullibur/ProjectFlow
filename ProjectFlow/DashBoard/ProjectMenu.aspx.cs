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
            if (!IsPostBack)
            {
                Session["PassProjectID"] = null;
                Session["PassProjectName"] = null;
                Session["PassTeamID"] = null;
                Session["PassTeamName"] = null;
                ShowProject();                        
            }                   
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            ProjectBLL projectBLL = new ProjectBLL();
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            Guid tutorID = Guid.Parse("5863511C-849B-443D-AA95-CFCE7DDAEBE3");  //currently hardcoded

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateProject').modal('show');", true);                
            }
            else
            {
                ClearField();               
                Response.Redirect("ProjectMenu.aspx");
            }
        }

        public void ShowProject()
        {           
            ProjectBLL projectBLL = new ProjectBLL();

            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetProjectTutor(Guid.Parse("5863511C-849B-443D-AA95-CFCE7DDAEBE3"));
            projectGV.DataSource = projectList;
            projectGV.DataBind();
        }

        protected void projectGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = projectGV.SelectedRow;
            Session["PassProjectID"] = row.Cells[0].Text;
            Session["PassProjectName"] = row.Cells[1].Text;
            Response.Redirect("ProjectTeamMenu.aspx");
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

        protected void projectGV_RowEditing(object sender, GridViewEditEventArgs e)
        {            
            projectGV.EditIndex = e.NewEditIndex;
            ShowProject();
        }

        protected void projectGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            projectGV.EditIndex = -1;
            ShowProject();
        }

        protected void projectGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = projectGV.Rows[e.RowIndex];
            ProjectBLL projectBLL = new ProjectBLL();

            TextBox editName = (TextBox)row.FindControl("editNameTB");
            TextBox editDesc = (TextBox)row.FindControl("editDescTB");           

            string projectID = row.Cells[0].Text;

            List<string> error = projectBLL.ValidateUpdate(projectID, editName.Text, editDesc.Text, Guid.Parse("5863511C-849B-443D-AA95-CFCE7DDAEBE3"));
            projectGV.EditIndex = -1;
            ShowProject();

        }

        protected void projectGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projectGV.PageIndex = e.NewPageIndex;
            ShowProject();
        }
    }
}