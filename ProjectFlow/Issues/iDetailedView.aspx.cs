using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class iDetailedView : System.Web.UI.Page
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iDetailedView);
                refreshData(TEST_TEAM_ID); 
            }
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData(TEST_TEAM_ID);
        }

        private void refreshData(int id)
        {
            IssueBLL issueBLL = new IssueBLL();

            IssueView.DataSource = issueBLL.GetActiveIssuesByTeamId(id);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }

        protected void IssueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = IssueView.SelectedRow;
            Session["SSName"] = row.Cells[2].Text;
            Session["SSDesc"] = row.Cells[3].Text;
            Session["SSIId"] = int.Parse(row.Cells[0].Text);
            Session["SSIsPublic"] = row.Cells[7].Text;
            Response.Redirect("../Issues/IssueRes.aspx");
        }

        protected void IssueView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            // Selected Task ID
            int id = Convert.ToInt32(IssueView.Rows[e.RowIndex].Cells[0].Text);

            // Delete Task
            IssueBLL issueBLL = new IssueBLL();
            bool result = issueBLL.Drop(id, TEST_TEAM_ID);

            refreshData(TEST_TEAM_ID);

            if (result)
            {
                //TODO: Notify Delete Successful
            }
            else
            {
                //TODO: Notify Delete Failed
            }
        }
    }
}