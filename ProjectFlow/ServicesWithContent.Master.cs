﻿using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils.Alerts;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class ServicesWithContent : ProjectFlowMasterPage
    {
        public override Panel AlertsPanel => AlertsPlaceHolder;

        protected void Page_Init(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated)
            {
                var projectFlowIdentity = user.Identity as ProjectFlowIdentity;
                if (projectFlowIdentity.IsStudent)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.aspnet_Membership.Email;
                }
                else if (projectFlowIdentity.IsTutor)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.aspnet_Membership.Email;
                }
            }

#if SELECTEDPROJECT
            ProjectBLL projectBLL = new ProjectBLL();
            //Set Current Project
            SetCurrentProject(projectBLL.GetProjectByProjectId("ITP213"));

#endif

            //Check if project is selected if not redirect to project selection screen
            if (CurrentProject == null) Response.Redirect("/Dashboard/ProjectMenu.aspx");

        }

        protected void LogoutEvent(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        /// <summary>
        /// Change the currently selected project, 
        /// all view should reponse accordingly and reflect on the changes such as the project tasks, etc
        /// </summary>
        public void SetCurrentProject(Project project)
        {

            //Check for access right into the project
            var user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated)
            {
                var projectFlowIdentity = user.Identity as ProjectFlowIdentity;
                if (projectFlowIdentity.IsStudent)
                {
                    var student = projectFlowIdentity.Student;

                    StudentBLL studentBLL = new StudentBLL();
                    if (studentBLL.ContainsProject(student, project))
                    {
                        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
                        //Get project team
                        ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByStudentAndProject(student, project);
                        Session["CurrentProjectTeam"] = projectTeam;

                        //Set the session of the current projects
                        Session["CurrentProject"] = project;


                        //Inject html for project
                        ProjectID.Value = project.projectID;
                        TeamID.Value = projectTeam.teamID.ToString();

                    }
                }
                else if (projectFlowIdentity.IsTutor)
                {
                    var tutor = projectFlowIdentity.Tutor;
                    TutorBLL tutorBLL = new TutorBLL();
                    if (tutorBLL.ContainsProject(tutor, project))
                    {
                        //Set the session of the current projects
                        Session["CurrentProject"] = project;


                        //Inject html for project
                        ProjectID.Value = project.projectID;

                    }
                }
            }

        }

        /// <summary>
        /// Return the current selected project
        /// </summary>
        public Project CurrentProject {
            get => Session["CurrentProject"] as Project;
        }

        /// <summary>
        /// Return the current selected project team
        /// </summary>
        public ProjectTeam CurrentProjectTeam
        {
            get {
                var user = HttpContext.Current.User;
                if (user.Identity.IsAuthenticated)
                {
                    var projectFlowIdentity = user.Identity as ProjectFlowIdentity;
                    if (projectFlowIdentity.IsStudent)
                    {
                        return Session["CurrentProjectTeam"] as ProjectTeam;
                    }
                    else
                    {
                        throw new InvalidOperationException("Trying to access Project Team as a Tutor is not allowed");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Not Authenticated");
                }
            }
        }


    }
}