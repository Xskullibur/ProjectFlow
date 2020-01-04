using ProjectFlow.BLL;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class tDetailedView : System.Web.UI.Page
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
                Master.changeSelectedView(TaskNested.TaskViews.OngoingTaskView);
                refreshData();
            }
        }

        private void refreshData()
        {
            TaskBLL taskBLL = new TaskBLL();

            taskGrid.DataSource = taskBLL.GetOngoingTasksByTeamId(TEST_TEAM_ID);
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

        // On DataBind
        protected void taskGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    // Setup Edit Mode

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

                    if (allocationVals != "-")
                    {
                        string[] allocations = allocationVals.Trim().Split(',');

                        foreach (string allocation in allocations)
                        {
                            editAllocationList.Items.FindByText(allocation.Trim()).Selected = true;
                        }
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
                else
                {
                    // Setup Due Date

                    TableCell DueDateCell = e.Row.Cells[1];

                    // Task End Date
                    DateTime EndDate = DateTime.Parse(((Label)e.Row.FindControl("gridEnd")).Text);
                    int DaysLeft = TaskVerification.VerifyDaysLeft(EndDate);

                    if (DaysLeft > 0)
                    {
                        DueDateCell.Text = $"{DaysLeft} Days";

                        if (DaysLeft > 5)
                        {
                            DueDateCell.CssClass = "text-success";
                        }
                        else
                        {
                            DueDateCell.CssClass = "text-warning";
                        }

                    }
                    else if (DaysLeft == 0)
                    {
                        DueDateCell.Text = "Today";
                        DueDateCell.CssClass = "text-danger";
                    }
                    else
                    {
                        DueDateCell.Text = $"Overdue {Math.Abs(DaysLeft)} Days!";
                        DueDateCell.CssClass = "text-danger";
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

            // Verify Task ID
            TaskBLL taskBLL = new TaskBLL();
            int id = Convert.ToInt32(row.Cells[0].Text);
            
            Task updated_task = taskBLL.GetTaskById(id);
            if (updated_task == null)
            {
                this.Master.Master.ShowAlert("Task Not Found!", BootstrapAlertTypes.DANGER);
            }
            else
            {
                // Get Controls
                TextBox nameTxt = (TextBox)row.FindControl("editTaskTxt");
                TextBox descTxt = (TextBox)row.FindControl("editDescTxt");
                DropDownList milestoneDDL = (DropDownList)row.FindControl("editMilestoneDDL");
                TextBox startTxt = (TextBox)row.FindControl("editStartDate");
                TextBox endTxt = (TextBox)row.FindControl("editEndDate");
                DropDownList statusDDL = (DropDownList)row.FindControl("editStatusDDL");

                Label tNameErrorLbl = (Label)row.FindControl("tNameErrorLbl");
                Label tDescErrorLbl = (Label)row.FindControl("tDescErrorLbl");
                Label tMilestoneErrorLbl = (Label)row.FindControl("tMilestoneErrorLbl");
                Label tStartDateErrorLbl = (Label)row.FindControl("tStartDateErrorLbl");
                Label tEndDateErrorLbl = (Label)row.FindControl("tEndDateErrorLbl");
                Label startEndDateErrorLbl = (Label)row.FindControl("startEndDateErrorLbl");
                Label statusErrorLbl = (Label)row.FindControl("statusErrorLbl");

                // Attributes
                string name = nameTxt.Text;
                string desc = descTxt.Text;
                int milestoneIndex = milestoneDDL.SelectedIndex;
                string startDate = startTxt.Text;
                string endDate = endTxt.Text;
                int statusIndex = statusDDL.SelectedIndex;

                // Clear Error Messages
                tNameErrorLbl.Text = string.Empty;
                tNameErrorLbl.Visible = false;

                tDescErrorLbl.Text = string.Empty;
                tDescErrorLbl.Visible = false;

                tMilestoneErrorLbl.Text = string.Empty;
                tMilestoneErrorLbl.Visible = false;

                tStartDateErrorLbl.Text = string.Empty;
                tStartDateErrorLbl.Visible = false;

                tEndDateErrorLbl.Text = string.Empty;
                tEndDateErrorLbl.Visible = false;

                startEndDateErrorLbl.Text = string.Empty;
                startEndDateErrorLbl.Visible = false;

                statusErrorLbl.Text = string.Empty;
                statusErrorLbl.Visible = false;

                // Verify
                TaskVerification taskVerification = new TaskVerification();
                bool verified = taskVerification.Verify(name, desc, milestoneIndex, startDate, endDate,  statusIndex);

                // Show Errors
                if (!verified)
                {
                    // Name
                    if (taskVerification.TNameErrors.Count > 0)
                    {
                        tNameErrorLbl.Visible = true;
                        tNameErrorLbl.Text = string.Join("<br>", taskVerification.TNameErrors);
                    }

                    // Description
                    if (taskVerification.TDescErrors.Count > 0)
                    {
                        tDescErrorLbl.Visible = true;
                        tDescErrorLbl.Text = string.Join("<br>", taskVerification.TDescErrors);
                    }

                    // Milestone
                    if (taskVerification.TMilestoneErrors.Count > 0)
                    {
                        tMilestoneErrorLbl.Visible = true;
                        tMilestoneErrorLbl.Text = string.Join("<br>", taskVerification.TMilestoneErrors);
                    }

                    // Start Date
                    if (taskVerification.TStartDateErrors.Count > 0)
                    {
                        tStartDateErrorLbl.Visible = true;
                        tStartDateErrorLbl.Text = string.Join("<br>", taskVerification.TStartDateErrors);
                    }

                    // End Date
                    if (taskVerification.TEndDateErrors.Count > 0)
                    {
                        tEndDateErrorLbl.Visible = true;
                        tEndDateErrorLbl.Text = string.Join("<br>", taskVerification.TEndDateErrors);
                    }

                    // Start End Date
                    if (taskVerification.StartEndDateErrors.Count > 0)
                    {
                        startEndDateErrorLbl.Visible = true;
                        startEndDateErrorLbl.Text = string.Join("<br>", taskVerification.StartEndDateErrors);
                    }

                    // Status
                    if (taskVerification.TStatusErrors.Count > 0)
                    {
                        statusErrorLbl.Visible = true;
                        statusErrorLbl.Text = string.Join("<br>", taskVerification.TStatusErrors);
                    }
                }
                else
                {
                    
                    /**
                     * UPDATE TASK
                     **/

                    int milestoneID = Convert.ToInt32((milestoneDDL).SelectedValue);
                    int statusID = Convert.ToInt32((statusDDL).SelectedValue);
                
                    // Update Task
                    updated_task.taskName = name;
                    updated_task.taskDescription = desc;
                    updated_task.startDate = Convert.ToDateTime(startDate);
                    updated_task.endDate = Convert.ToDateTime(endDate);

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
                        this.Master.Master.ShowAlertWithTiming("Task Successfully Updated!", BootstrapAlertTypes.SUCCESS, 2000);
                    }
                    else
                    {
                        this.Master.Master.ShowAlert("Failed to Update Task!", BootstrapAlertTypes.DANGER);
                    }

                    // Return to READ MODE
                    taskGrid.EditIndex = -1;
                    refreshData();

                }

            }

        }

        // Deleting
        protected void taskGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Selected Task ID
            int id = Convert.ToInt32(taskGrid.Rows[e.RowIndex].Cells[0].Text);

            // Delete Task
            TaskBLL taskBLL = new TaskBLL();
            bool result = taskBLL.Delete(id);

            refreshData();

            if (result)
            {
                this.Master.Master.ShowAlertWithTiming("Task Successfully Dropped!", BootstrapAlertTypes.SUCCESS, 2000);
            }
            else
            {
                this.Master.Master.ShowAlert("Failed to Drop Task", BootstrapAlertTypes.DANGER);
            }

        }

        protected void taskGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            taskGrid.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}
