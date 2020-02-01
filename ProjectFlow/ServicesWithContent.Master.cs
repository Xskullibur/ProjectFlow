﻿using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.MaterialIO;
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

        public ProjectFlowIdentity Identity { get => HttpContext.Current.User.Identity as ProjectFlowIdentity; }
        public MaterialSidebar matSidebar { get => Identity.IsStudent ? matStudentSidebar : matTutorSidebar; }

        public string ProfileUrl
        {
            get
            {
                if(Identity.aspnet_Users.ProfileImagePath != null)
                {
                    return "/Profile/ProfileImages/" + Identity.aspnet_Users.ProfileImagePath;
                }
                else
                {
                    return "/Profile/ProfileImages/default-picture.png";
                }
                
            }
        }

        //Set service page header title
        public string Header { get => HeaderLbl.Text; set => HeaderLbl.Text = value; }

        protected void Page_Init(object sender, EventArgs e)
        {

            //Page Redirection
            matSidebar.NavClickListeners += (redirectionPage) =>
            {
                Response.Redirect(redirectionPage);
            };

#if SELECTEDPROJECT
            ProjectBLL projectBLL = new ProjectBLL();
            //Set Current Project
            SetCurrentProject(projectBLL.GetProjectByProjectId("ITP213"));

#endif

            var user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated)
            {
                var projectFlowIdentity = user.Identity as ProjectFlowIdentity;
                if (projectFlowIdentity.IsStudent)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Student.aspnet_Users.aspnet_Membership.Email;

                    //Check if project is selected if not redirect to project selection screen
                    string studentDashboardPath = "/StudentDashboard/studentProject.aspx";
                    if (CurrentProject == null && !HttpContext.Current.Request.Url.AbsolutePath.Equals(studentDashboardPath))
                    {
                        Response.Redirect(studentDashboardPath);
                    }
                    else
                    {
                        //Get session
                        var projectTeam = Session["CurrentProjectTeam"] as ProjectTeam;
                        var project = Session["CurrentProject"] as Project;

                        //Inject ID into html
                        InjectHTMLForProjectTeamAndProject(project, projectTeam);
                    }
                }
                else if (projectFlowIdentity.IsTutor)
                {
                    this.LoginUsernameLbl.Text = "Welcome, " + projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginUsernameProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.UserName;
                    this.LoginEmailProfileLbl.Text = projectFlowIdentity.Tutor.aspnet_Users.aspnet_Membership.Email;

                    //Check if project is selected if not redirect to project selection screen
                    string tutorDashboardPath = "/TutorDashboard/ProjectMenu.aspx";
                    if (CurrentProject == null && !HttpContext.Current.Request.Url.AbsolutePath.Equals(tutorDashboardPath))
                    {
                        Response.Redirect(tutorDashboardPath);
                    }
                    else
                    {
                        //Get session
                        var projectTeam = Session["CurrentProjectTeam"] as ProjectTeam;
                        var project = Session["CurrentProject"] as Project;

                        //Inject ID into html
                        InjectHTMLForProjectTeamAndProject(project, projectTeam);
                    }
                }

                //Set project title
                ProjectTitleLbl.Text = CurrentProject?.projectName;

            }

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
        /// all view should reponse accordingly and reflect on the changes such as the project tasks, etc.
        /// Setting this with null will clear the project
        /// </summary>
        public void SetCurrentProject(Project project)
        {

            //Check if setting project to null
            if(project == null)
            {
                //Clear the session
                Session["CurrentProjectTeam"] = null;
                Session["CurrentProject"] = null;
                return;
            }

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

                        //Inject ID into html
                        InjectHTMLForProjectTeamAndProject(project, projectTeam);

                    }
                    else
                    {
                        Response.Redirect("InvalidRequest.aspx");
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
                        InjectHTMLForProjectTeamAndProject(project, null);

                    }
                    else
                    {
                        Response.Redirect("InvalidRequest.aspx");
                    }
                }
            }

        }

        private void InjectHTMLForProjectTeamAndProject(Project project, ProjectTeam projectTeam)
        {
            //Inject html for project
            if(project != null)
                ProjectID.Value = project.projectID;
            if (projectTeam != null)
                TeamID.Value = projectTeam.teamID.ToString();
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
                    return Session["CurrentProjectTeam"] as ProjectTeam;
                }
                else
                {
                    throw new InvalidOperationException("Not Authenticated");
                }
            }
        }

        protected void ProjectBtnEvent(object sender, EventArgs e)
        {
            //Clear current project and go to project screen by refreshing
            SetCurrentProject(null);

            //Refresh
            Response.Redirect(Request.RawUrl);
        }
    }
}