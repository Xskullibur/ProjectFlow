using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;

namespace ProjectFlow.Issues
{
    public partial class iDetailedView : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iDetailedView);
                refreshData(); 
            }
            if (Master.GetCurrentIdentiy().IsTutor)
            {
                IssueView.Columns[IssueView.Columns.Count - 1].Visible = false;
            }
            IssueView.Font.Size = 11;
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            IssueBLL issueBLL = new IssueBLL();

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            IssueView.DataSource = issueBLL.GetActiveIssuesByTeamId(currentTeam.teamID);
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

            Session["SSIId"] = int.Parse(row.Cells[0].Text);

            Response.Redirect("../Issues/IssueRes.aspx");
        }

        protected void IssueView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            // Selected Issue ID
            int id = Convert.ToInt32(IssueView.Rows[e.RowIndex].Cells[0].Text);

            // Delete Task
            IssueBLL issueBLL = new IssueBLL();
            bool result = issueBLL.Drop(id);

            refreshData();

            if (result)
            {
                //NotificationHelper.Task_Drop_Setup(id);
                this.Master.Master.ShowAlertWithTiming("Task Successfully Dropped!", BootstrapAlertTypes.SUCCESS, 2000);
            }
            else
            {
                this.Master.Master.ShowAlert("Failed to Drop Task", BootstrapAlertTypes.DANGER);
            }
        }

        protected void IssueView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            IssueView.EditIndex = e.NewEditIndex;
            refreshData();
        }

        protected void IssueView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    object rowItems = e.Row.DataItem;

                    /**
                     * STATUS
                     **/

                    //Get Control
                    DropDownList editStatusDDL = (DropDownList)e.Row.FindControl("editStatusDDL");
                    TextBox editNameTxt = (TextBox)e.Row.FindControl("editNameTxt");
                    TextBox editDescTxt = (TextBox)e.Row.FindControl("editDescTxt");

                    //Set Style
                    editStatusDDL.Style.Add("width", "auto");
                    editNameTxt.Style.Add("width", "auto");
                    editDescTxt.Style.Add("width", "auto");

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

        protected void IssueView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IssueView.EditIndex = -1;
            refreshData();
        }

        // Updating
        protected void IssueView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get Values
            GridViewRow row = IssueView.Rows[e.RowIndex];

            // Verify Task ID
            IssueBLL issueBLL = new IssueBLL();

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
                        //NotificationHelper.Default_TaskUpdate_Setup(id);
                        this.Master.Master.ShowAlertWithTiming("Task Successfully Updated!", BootstrapAlertTypes.SUCCESS, 2000);
                    }
                    else
                    {
                        this.Master.Master.ShowAlert("Failed to Update Task!", BootstrapAlertTypes.DANGER);
                    }

                    // Return to READ MODE
                    IssueView.EditIndex = -1;
                    refreshData();

                }

            }

        }

        //Pagination
        protected void IssueView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IssueView.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}