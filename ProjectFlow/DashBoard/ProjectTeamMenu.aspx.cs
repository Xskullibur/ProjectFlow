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
            if (!IsPostBack)
            {
                if (Session["PassProjectID"] != null)
                {
                    ShowTeam(Session["PassProjectID"].ToString());
                }
                else
                {
                    Response.Redirect("ProjectMenu.aspx");
                }
            }              
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            string teamName = NameTB.Text;
            string desc = DescTB.Text;

            bll.InsertProjectTeam(teamName, desc, Session["PassProjectID"].ToString());
            ShowTeam(Session["PassProjectID"].ToString());
        }

        public void ShowTeam(string ProjectID)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            if (projectBLL.CheckProjectTeamExist(ProjectID))
            {
                List<ProjectTeam> teamList = new List<ProjectTeam> { };
                teamList = projectBLL.GetProjectTeam(ProjectID);
                TeamGV.DataSource = teamList;
                TeamGV.DataBind();
            }           
        }
    }
}