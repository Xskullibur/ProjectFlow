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
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.changeSelectedView(IssueNested.IssueViews.iDroppedView);
            refreshData(TEST_TEAM_ID);
        }

        private void refreshData(int id)
        {
            IssueBLL issueBLL = new IssueBLL();

            IssueView.DataSource = issueBLL.GetDroppedIssueByTeamId(id);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }
    }
}