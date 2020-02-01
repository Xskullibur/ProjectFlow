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

namespace ProjectFlow.DashBoard
{
    public partial class studentProject : System.Web.UI.Page
    {
        ProjectBLL projectBLL = new ProjectBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            if (identity.IsStudent)
            {
                Session["StudentID"] = identity.Student.UserId.ToString();
                Session["Student"] = identity.Student.studentID.ToString();
                ShowProject();
            }            
        }

        private string getStudentID()
        {
            return Session["Student"].ToString();
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //Make gridview row clickable
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ProjectGV, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view project";
            }
        }



        protected void ProjectGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ProjectGV.SelectedRow;
            int teamID;

            if(int.TryParse(row.Cells[0].Text, out teamID))
            {

                ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
                ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByTeamID(teamID);

                //(Master as ServicesWithContent).SetCurrentProject(projectTeam.Project);
                Session["StudentTeamID"] = row.Cells[0].Text;

                Response.Redirect("/Tasks/OngoingTaskView.aspx");
            }
            else
            {
                throw new InvalidOperationException("Invalid team ID given!");
            }
        }

        protected void ProjectGV_PageIndexChanged(object sender, EventArgs e)
        {

        }


        public void ShowProject()
        {
            StudentBLL studentBLL = new StudentBLL();
            IEnumerable<ProjectTeam> teamList = studentBLL.GetStudentProject(Guid.Parse(Session["StudentID"].ToString()));
            ProjectGV.DataSource = teamList;
            ProjectGV.DataBind();

            IEnumerable<ProjectTeam> avaliableList = studentBLL.ShowAvailbleProject(getStudentID());
            availableGV.DataSource = avaliableList;
            availableGV.DataBind();
        }

        public void SearchProject(string search)
        {
            StudentBLL studentBLL = new StudentBLL();
            IEnumerable<ProjectTeam> teamList = studentBLL.SearchStudentProject(Guid.Parse(Session["StudentID"].ToString()), search);
            ProjectGV.DataSource = teamList;
            ProjectGV.DataBind();
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("studentProject.aspx");
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchProject(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            ShowProject();
        }

        protected void availableGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = availableGV.SelectedRow;
            projectBLL.InsertMember(getStudentID(), int.Parse(row.Cells[0].Text), 2);
            Master.ShowAlert("Joined Team", BootstrapAlertTypes.SUCCESS);
            ShowProject();
        }
    }
}