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
            if (!IsPostBack)
            {
                refreshData();
            }
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

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        /**
         * EDITING FUNCTIONS
         **/

        // Enter Editing Mode
        protected void taskGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            taskGrid.EditIndex = e.NewEditIndex;
            refreshData();
        }

        //  Setup Editing Data
        protected void taskGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    object rowItems = e.Row.DataItem;

                    /**
                     * NAME
                     **/

                    TextBox editTaskTxt = (TextBox)e.Row.FindControl("editTaskTxt");
                    editTaskTxt.Style.Add("width", "auto");

                    /**
                     * DESCRIPTION
                     **/

                    TextBox editDescTxt = (TextBox)e.Row.FindControl("editDescTxt");
                    editDescTxt.Style.Add("width", "auto");

                    /**
                     * START DATE
                     **/

                    TextBox editStartTxt = (TextBox)e.Row.FindControl("editStartDate");
                    DateTime startDate = Convert.ToDateTime(DataBinder.Eval(rowItems, "Start"));
                    editStartTxt.Text = startDate.Date.ToString("yyyy-MM-dd");

                    /**
                     * END DATE
                     **/

                    TextBox editEndTxt = (TextBox)e.Row.FindControl("editEndDate");
                    DateTime endDate = Convert.ToDateTime(DataBinder.Eval(rowItems, "Start"));
                    editEndTxt.Text = endDate.Date.ToString("yyyy-MM-dd");

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

                    /**
                     * ALLOCATIONS
                     **/

                    //Get ListBox
                    ListBox editAllocationList = (ListBox)e.Row.FindControl("editAllocationList");

                    //Set ListBox Datasource
                    TeamMemberBLL memberBLL = new TeamMemberBLL();
                    Dictionary<int, string> memberList = memberBLL.GetTeamMembersByTeamID(TEST_TEAM_ID);

                    editAllocationList.DataSource = memberList;
                    editAllocationList.DataTextField = "Value";
                    editAllocationList.DataValueField = "Key";

                    editAllocationList.DataBind();

                    //Set Inital Value
                    string allocationVals = DataBinder.Eval(rowItems, "Allocation").ToString();
                    string[] allocations = allocationVals.Trim().Split(',');

                    foreach (string allocation in allocations)
                    {
                        editAllocationList.Items.FindByText(allocation.Trim()).Selected = true;
                    }

                    /**
                     * MILESTONE
                     **/

                    //Get Milestone Dropdownlist
                    DropDownList editMilestoneDDL = (DropDownList)e.Row.FindControl("editMilestoneDDL");

                    //Set Style
                    editMilestoneDDL.Style.Add("width", "auto");

                    //Set Dropdownlist Datasource
                    MilestoneBLL milestoneBLL = new MilestoneBLL();
                    var teamMilestone = milestoneBLL.GetMilestoneByTeamID(TEST_TEAM_ID);

                    editMilestoneDDL.DataSource = teamMilestone;
                    editMilestoneDDL.DataValueField = "milestoneID";
                    editMilestoneDDL.DataTextField = "milestoneName";
                    editMilestoneDDL.DataBind();

                    editMilestoneDDL.Items.Insert(0, new ListItem("-- No Milestone --", "-1"));

                    //Set Inital Value
                    string milestoneVal = DataBinder.Eval(rowItems, "Milestone").ToString();

                    if (milestoneVal == "-")
                    {
                        editMilestoneDDL.SelectedValue = "-1";
                    }
                    else
                    {
                        editMilestoneDDL.SelectedValue = teamMilestone.First(x => x.milestoneName == milestoneVal).milestoneID.ToString();
                    }
                }
            }

        }

        // Editing Canceled
        protected void taskGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            taskGrid.EditIndex = -1;
            refreshData();
        }

        // Updating
        protected void taskGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get Values
            GridViewRow row = taskGrid.Rows[e.RowIndex];

            int id = Convert.ToInt32(row.Cells[1].Text);
            string name = ((TextBox)row.FindControl("editTaskTxt")).Text;
            string desc = ((TextBox)row.FindControl("editDescTxt")).Text;
            int milestoneID = Convert.ToInt32(((DropDownList)row.FindControl("editMilestoneDDL")).SelectedValue);
            DateTime startDate = Convert.ToDateTime(((TextBox)row.FindControl("editStartDate")).Text);
            DateTime endDate = Convert.ToDateTime(((TextBox)row.FindControl("editEndDate")).Text);
            int statusID = Convert.ToInt32(((DropDownList)row.FindControl("editStatusDDL")).SelectedValue);

            // Get Task to Update
            TaskBLL taskBLL = new TaskBLL();
            Task updated_task = taskBLL.GetTaskById(id);

            // Update Task
            updated_task.taskName = name;
            updated_task.taskDescription = desc;
            updated_task.startDate = startDate;
            updated_task.endDate = endDate;

            if (milestoneID != -1)
            {
                updated_task.milestoneID = milestoneID;
            }
            else
            {
                updated_task.milestoneID = null;
            }

            updated_task.statusID = statusID;

            // Get Edit Allocation List Control
            ListBox editAllocationList = (ListBox)row.FindControl("editAllocationList");

            // Init Updated Allocations
            List<TaskAllocation> updated_Allocations = new List<TaskAllocation>();

            // Create Updated Allocations
            foreach (ListItem item in editAllocationList.Items.Cast<ListItem>().Where(x => x.Selected))
            {
                TaskAllocation newAllocation = new TaskAllocation();
                newAllocation.taskID = updated_task.taskID;
                newAllocation.assignedTo = Convert.ToInt32(item.Value);

                updated_Allocations.Add(newAllocation);
            }

            // Update Task and Allocations
            if (taskBLL.Update(updated_task, updated_Allocations))
            {
                // TODO: Notify Successful Update
            }
            else
            {
                // TODO: Notify Failed Update
            }

            // Return to READ MODE
            taskGrid.EditIndex = -1;
            refreshData();
        }
    }
}
