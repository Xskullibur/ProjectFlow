using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class iAllView : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iAllView);
                refreshData();
            }
            IssueView.Font.Size = 11;
        }
        private void refreshData()
        {
            IssueBLL issueBLL = new IssueBLL();

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            IssueView.DataSource = issueBLL.GetAllIssuesByTeamId(currentTeam.teamID);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }

        protected void IssueView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                }
                else
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell PublicCell = e.Row.Cells[5];
                        if (PublicCell.Text == "True")
                        {
                            PublicCell.Text = "<i class='fa fa-check-circle fa-lg text-success'></i>";
                        }

                        else
                        {
                            PublicCell.Text = "<i class='fa fa-times-circle fa-lg text-danger'></i>";
                        }
                    }
                }
            }
        }

        protected void IssueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = IssueView.SelectedRow;

            Session["SSIId"] = int.Parse(row.Cells[0].Text);

            Response.Redirect("../Issues/IssueRes.aspx");
        }

        protected void IssueView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            /*
            // Selected Task ID
            int id = Convert.ToInt32(IssueView.Rows[e.RowIndex].Cells[0].Text);

            // Delete Task
            IssueBLL issueBLL = new IssueBLL();
            bool result = issueBLL.Drop(id, TEST_TEAM_ID);

            refreshData();

            if (result)
            {
                //TODO: Notify Delete Successful
            }
            else
            {
                //TODO: Notify Delete Failed
            }*/
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        //Pagination
        protected void IssueView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IssueView.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}