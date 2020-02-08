using ProjectFlow.BLL;
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
    public partial class ServicesWithContent : MasterPage, IAlert
    {
        public Panel AlertsPanel => Master.AlertsPanel;

        public void ShowAlert(string alertMsg, string alertType, bool escapedHtml = true, bool dismissable = true)
        {
            Master.ShowAlert(alertMsg, alertType, escapedHtml, dismissable);
        }

        public void ShowAlertWithTiming(string alertMsg, string alertType, int time, bool escapedHtml = true, bool dismissable = true)
        {
            Master.ShowAlertWithTiming(alertMsg, alertType, time, escapedHtml, dismissable);
        }

        public ProjectFlowIdentity Identity { get => Master.Identity; }
        public MaterialSidebar matSidebar { get => Identity.IsStudent ? matStudentSidebar : matTutorSidebar; }

        public string ProfileUrl { get => Master.ProfileUrl;  }

        public string Header { get => Master.Header; set { Master.Header = value; } }

        /// <summary>
        /// Change the currently selected project team, 
        /// all view should reponse accordingly and reflect on the changes such as the project tasks, etc.
        /// Setting this with null will clear the project team
        /// </summary>
        public void SetCurrentProjectTeam(ProjectTeam projectTeam)
        {
            Master.SetCurrentProjectTeam(projectTeam);
        }

        /// <summary>
        /// Change the currently selected project, 
        /// all view should reponse accordingly and reflect on the changes such as the project tasks, etc.
        /// Setting this with null will clear the project
        /// </summary>
        public void SetCurrentProject(Project project)
        {
            Master.SetCurrentProject(project);
        }

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

                    //Check if project is selected if not redirect to project selection screen
                    string studentDashboardPath = "/StudentDashboard/studentProject.aspx";
                    if (CurrentProject == null && !HttpContext.Current.Request.Url.AbsolutePath.Equals(studentDashboardPath))
                    {
                        Response.Redirect(studentDashboardPath);
                    }
                }
                else if (projectFlowIdentity.IsTutor)
                {

                    //Check if project is selected if not redirect to project selection screen
                    string tutorDashboardPath = "/TutorDashboard/ProjectMenu.aspx";
                    if (CurrentProject == null && !HttpContext.Current.Request.Url.AbsolutePath.Equals(tutorDashboardPath))
                    {
                        Response.Redirect(tutorDashboardPath);
                    }
                }

                //Set project title
                ProjectTitleLbl.Text = CurrentProject?.projectName;

            }

        }

        /// <summary>
        /// Return the current selected project
        /// </summary>
        public Project CurrentProject
        {
            get => Master.CurrentProject;
        }

        /// <summary>
        /// Return the current selected project team
        /// </summary>
        public ProjectTeam CurrentProjectTeam
        {
            get => Master.CurrentProjectTeam;
        }


    }
}