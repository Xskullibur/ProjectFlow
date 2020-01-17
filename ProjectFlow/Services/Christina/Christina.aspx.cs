using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;

namespace ProjectFlow.Services.Christina
{
    public partial class Christina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get speakers belong to the current project team
            ServicesWithContent servicesWithContent = this.Master as ServicesWithContent;

            Project selectedProject = servicesWithContent.CurrentProject;
            var projectFlowIdentity = this.User.Identity as ProjectFlowIdentity;
            Student student = projectFlowIdentity.Student;

            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();

            ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByStudentAndProject(student, selectedProject);

            //Create all the speakers in client side
            Student[] students = projectTeamBLL.GetTeamMembersFromProjectTeam(projectTeam).Select(tm => tm.Student).ToArray();
            InjectSpeaker(students);
        }

        /// <summary>
        /// Create speaker on the client side so all the speaker are displayed
        /// 
        /// Called during Page_Load
        /// 
        /// </summary>
        /// <param name="speakers"></param>
        private void InjectSpeaker(Student[] speakers)
        {
            
            string createSpeakers = @"
                <script type=""text/javascript"">
                    $(document).ready(function (){
            ";

            aspnet_UsersBLL aspnet_UsersBLL = new aspnet_UsersBLL();

            foreach(var speaker in speakers)
            {

                aspnet_Users aspnet_Users = aspnet_UsersBLL.Getaspnet_UsersByStudent(speaker);

                createSpeakers += $"speakers_circles.push(create_speaker('{aspnet_Users.UserName}', null));";
            }
            createSpeakers += @"
                    });
                </script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "create_speakers", createSpeakers, false);
        }

        protected void SuggestEvent(object sender, EventArgs e)
        {
            string text = SuggestionTextBox.Text;

            SemanticsParser parser = new SemanticsParser();
            List<Speaker> speakers = parser.Parse(text);


            
        }
    }
}