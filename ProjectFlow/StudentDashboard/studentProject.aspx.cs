using ProjectFlow.BLL;
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
            Session["StudentTeamID"] = row.Cells[0].Text;
            Response.Redirect("FileUpload.aspx");
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
    }
}