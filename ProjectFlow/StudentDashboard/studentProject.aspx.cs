﻿using ProjectFlow.BLL;
using ProjectFlow.Login;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            if (identity.IsStudent)
            {
                Session["StudentID"] = identity.Student.UserId.ToString();
                Session["StudentTeamID"] = null;
                ShowProject();
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

                (Master as ServicesWithContent).SetCurrentProject(projectTeam.Project);

                Response.Redirect("FileUpload.aspx");
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
    }
}