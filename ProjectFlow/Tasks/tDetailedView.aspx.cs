using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class tDetailedView : System.Web.UI.Page
    {

        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            refreshData();
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            TaskBLL taskBLL = new TaskBLL();

            taskGrid.DataSource = taskBLL.GetTasksByTeamId(TEST_TEAM_ID);
            taskGrid.DataBind();

            if (taskGrid.Rows.Count > 0)
            {
                taskGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                taskGrid.UseAccessibleHeader = true;
            }
        }

        protected void taskGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            taskGrid.EditIndex = e.NewEditIndex;
            DataBind();
        }

        protected void taskGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) != 0)
                {

                    object rowItems = e.Row.DataItem;

                    // Status
                    DropDownList editStatusDDL = (DropDownList)e.Row.FindControl("editStatusDDL");

                    StatusBLL statusBLL = new StatusBLL();
                    Dictionary<int, string> statusDict = statusBLL.Get();

                    editStatusDDL.DataSource = statusDict;
                    editStatusDDL.DataTextField = "Value";
                    editStatusDDL.DataValueField = "Key";
                    editStatusDDL.DataBind();

                    string statusVal = DataBinder.Eval(rowItems, "Status").ToString();
                    editStatusDDL.SelectedValue = statusDict.First(x => x.Value == statusVal).Key.ToString();

                    // Milestone
                    DropDownList editMilestoneDDL = (DropDownList)e.Row.FindControl("editMilestoneDDL");

                    MilestoneBLL milestoneBLL = new MilestoneBLL();
                    var teamMilestone = milestoneBLL.GetMilestoneByTeamID(TEST_TEAM_ID);

                    editMilestoneDDL.DataSource = teamMilestone;
                    editMilestoneDDL.DataValueField = "ID";
                    editMilestoneDDL.DataTextField = "Milestone";
                    editMilestoneDDL.DataBind();
                }
            }
        }
    }
}