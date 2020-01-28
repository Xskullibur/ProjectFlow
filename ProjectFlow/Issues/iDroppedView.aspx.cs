using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class IDroppedView : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Master.GetCurrentProjectTeam() == null)
            {
                if (Master.GetCurrentIdentiy().IsTutor)
                {
                    Response.Redirect("/TutorDashboard/ProjectTeamMenu.aspx");
                }
                else if (Master.GetCurrentIdentiy().IsStudent)
                {
                    Response.Redirect("/StudentDashboard/studentProject.aspx");
                }
            }

            //Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iDroppedView);
                refreshData();
            }

            IssueView.Font.Size = 11;
        }

        private void refreshData()
        {
            IssueBLL issueBLL = new IssueBLL();

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            IssueView.DataSource = issueBLL.GetDroppedIssueByTeamId(currentTeam.teamID);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }
    }
}