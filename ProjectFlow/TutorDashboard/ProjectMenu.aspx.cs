using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;
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
    public partial class ProjectMenu : System.Web.UI.Page
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
                    Session["PassProjectID"] = null;
                    Session["PassProjectName"] = null;
                    Session["PassTeamID"] = null;
                    Session["PassTeamName"] = null;
                    this.SetHeader("Modules that I teach");
                    ShowProject();
                }
            }                                
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            Guid tutorID = Guid.Parse(Session["TutorID"].ToString());  

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
                ShowModel();                
            }
            else
            {
                ClearField();
                ShowProject();
                Master.ShowAlert("Project Successfully Created", BootstrapAlertTypes.SUCCESS);
            }
        }

        public void ShowProject()
        {           
            ProjectBLL projectBLL = new ProjectBLL();

            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.GetProjectTutor(Guid.Parse(Session["TutorID"].ToString()));
            projectGV.DataSource = projectList;
            projectGV.DataBind();
            PageSelectDP.SelectedIndex = 0;
        }

        public void SearchProject(string search)
        {
            ProjectBLL projectBLL = new ProjectBLL();

            List<Project> projectList = new List<Project> { };
            projectList = projectBLL.SearchProject(Guid.Parse(Session["TutorID"].ToString()), search);
            projectGV.DataSource = projectList;
            projectGV.DataBind();
            PageSelectDP.SelectedIndex = 0;

        }

        protected void projectGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = projectGV.SelectedRow;
            Label name = (Label)row.FindControl("nameLabel");
            Session["PassProjectID"] = row.Cells[0].Text;
            Session["PassProjectName"] = name.Text;

            //Temp fix
            ProjectBLL projectBLL = new ProjectBLL();
            var project = projectBLL.GetProjectByProjectId(row.Cells[0].Text);
            (Master as ServicesWithContent).SetCurrentProject(project);

            Response.Redirect("ProjectTeamMenu.aspx");
        }

        public void ClearField()
        {
            ProjectIdTB.Text = "";
            NameTB.Text = "";
            DescTB.Text = "";
            errorLabel.Text = "";           
        }

        public void HideModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#uploadModal').modal('hide')", true);
        }

        public void ShowModel()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateProject').modal('show');", true);
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
            
            TextBox editName = (TextBox)row.FindControl("editNameTB");
            TextBox editDesc = (TextBox)row.FindControl("editDescTB");           

            string projectID = row.Cells[0].Text;

            List<string> error = projectBLL.ValidateUpdate(projectID, editName.Text, editDesc.Text, Guid.Parse(Session["TutorID"].ToString()));
            projectGV.EditIndex = -1;
            ShowProject();
            Master.ShowAlert("Project Updated Successfully", BootstrapAlertTypes.SUCCESS);
        }

        protected void projectGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projectGV.PageIndex = e.NewPageIndex;
            ShowProject();
        }

        protected void projectGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)projectGV.Rows[e.RowIndex];
            projectBLL.DeleteProject(row.Cells[0].Text);
            ShowProject();
            Master.ShowAlert("Project Successfully Deleted", BootstrapAlertTypes.SUCCESS);
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectMenu.aspx");
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageSelectDP.SelectedIndex == 1)
            {
                Response.Redirect("RestoreDashboard/ProjectMenuRestore.aspx");
            }
        }

        protected void createAnotherBtn_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            string projectID = ProjectIdTB.Text;
            string projectName = NameTB.Text;
            string projectDesc = DescTB.Text;
            Guid tutorID = Guid.Parse(Session["TutorID"].ToString());

            List<string> error = projectBLL.ValidateProject(projectID, projectName, projectDesc, tutorID);

            if (error.Count > 0)
            {
                string total = "";
                foreach (string errorItem in error)
                {
                    total += errorItem;
                }
                errorLabel.Text = total;
                ProjectIdTB.Text = projectID;
                NameTB.Text = projectName;
                DescTB.Text = projectDesc;               
            }
            else
            {
                ClearField();
                ShowProject();
            }
            ShowModel();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchProject(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            ShowProject();
        }
    }
}