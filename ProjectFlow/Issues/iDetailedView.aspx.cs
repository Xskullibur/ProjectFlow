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
        private const int TEST_TEAM_ID = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int TaskId = int.Parse((string)Session["SSTaskID"]);
                //refreshData(TaskId);
                
            }
            refreshData(TEST_TEAM_ID);
        }
        private void refreshData(int id)
        {
            IssueBLL issueBLL = new IssueBLL();

            IssueView.DataSource = issueBLL.GetIssueById(id);
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
            Session["SSCreatedBy"] = row.Cells[3].Text;
            Session["SSDesc"] = row.Cells[1].Text;
            Response.Redirect("../Issues/IssueRes.aspx");
        }
    }
}