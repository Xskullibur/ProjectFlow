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

        protected void taskGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            IssueView.EditIndex = e.NewEditIndex;
            refreshData(TEST_TEAM_ID);
        }

        protected void taskGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    object rowItems = e.Row.DataItem;

                    /**
                     * STATUS
                     **/

                    //Get Status Dropdownlist
                    DropDownList editStatusDDL = (DropDownList)e.Row.FindControl("editStatusDDL");

                    //Set Style
                    editStatusDDL.Style.Add("width", "auto");

                    //Set Dropdownlist Datasource
                    StatusBLL statusBLL = new StatusBLL();
                    Dictionary<int, string> statusDict = statusBLL.Get();

                    editStatusDDL.DataSource = statusDict;
                    editStatusDDL.DataTextField = "Value";
                    editStatusDDL.DataValueField = "Key";
                    editStatusDDL.DataBind();

                    //Set Inital Value
                    string statusVal = DataBinder.Eval(rowItems, "Status").ToString();
                    editStatusDDL.SelectedValue = statusDict.First(x => x.Value == statusVal).Key.ToString();

                }
            }

        }

        protected void taskGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IssueView.EditIndex = -1;
            refreshData(TEST_TEAM_ID);
        }

        // Updating
        protected void taskGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get Values
            GridViewRow row = IssueView.Rows[e.RowIndex];

            // Verify Task ID
            IssueBLL issueBLL = new IssueBLL();

            TaskBLL taskBLL = new TaskBLL();
            //int id = Convert.ToInt32(row.Cells[0].Text);
            int id = int.Parse(row.Cells[0].Text);

            Issue updated_issue = issueBLL.GetIssueByID(id);

            if (updated_issue == null)
            {
                // TODO: Error Message Task ID Not Found
            }
            else
            {
                // Get Controls
                TextBox nameTxt = (TextBox)row.FindControl("editNameTxt");
                TextBox descTxt = (TextBox)row.FindControl("editDescTxt");
                DropDownList statusDDL = (DropDownList)row.FindControl("editStatusDDL");


                // Attributes
                string name = nameTxt.Text;
                string desc = descTxt.Text;
                int statusIndex = statusDDL.SelectedIndex;

                // Clear Error Messages


                // Verify
                bool verified = true;

                // Show Errors
                if (!verified)
                {
                    
                }
                else
                {

                    /**
                     * UPDATE TASK
                     **/

                    int statusID = Convert.ToInt32((statusDDL).SelectedValue);

                    // Update Task

                    updated_issue.title = name;
                    updated_issue.description = desc;
                    updated_issue.statusID = statusID;

                    // Update Task and Allocations
                    if (issueBLL.Update(updated_issue))
                    {
                        // TODO: Notify Successful Update
                    }
                    else
                    {
                        // TODO: Notify Failed Update
                    }

                    // Return to READ MODE
                    IssueView.EditIndex = -1;
                    refreshData(TEST_TEAM_ID);

                }

            }

        }
    }
}