using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.DashBoard
{
    public partial class ProjectTeamMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ShowTeam();
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {

        }

        public void ShowTeam(string ProjectID)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectBLL.GetProjectTeam(ProjectID);
            TeamGV.DataSource = teamList;
            TeamGV.DataBind();
        }
    }
}