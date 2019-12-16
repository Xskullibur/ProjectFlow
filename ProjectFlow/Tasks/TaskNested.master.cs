using ProjectFlow.BLL;
using ProjectFlow.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class TaskNested : System.Web.UI.MasterPage
    {
        private const int TEST_TEAM_ID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Status
                StatusBLL statusBLL = new StatusBLL();
                Dictionary<int, string> statusDict = statusBLL.Get();

                statusDDL.DataSource = statusDict;
                statusDDL.DataTextField = "Value";
                statusDDL.DataValueField = "Key";


                // Allocations
                TeamMemberBLL memberBLL = new TeamMemberBLL();
                Dictionary<int, string> memberList = memberBLL.GetTeamMembersByTeamID(TEST_TEAM_ID);

                allocationList.DataSource = memberList;
                allocationList.DataTextField = "Value";
                allocationList.DataValueField = "Key";
                allocationList.DataBind();

                // Milestone
                MilestoneBLL milestoneBLL = new MilestoneBLL();
                var teamMilestones = milestoneBLL.GetMilestoneByTeamID(TEST_TEAM_ID);

                milestoneDDL.DataSource = teamMilestones;
                milestoneDDL.DataTextField = "Milestone";
                milestoneDDL.DataValueField = "ID";
                milestoneDDL.DataBind();

                milestoneDDL.Items.Insert(0, new ListItem("-- No Milestone --", "-1"));

            }
        }

        // Add Task OnClick Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            showAddTaskModal();
        }

        // Show Add Modal
        private void showAddTaskModal()
        {
            tTitleLbl.Text = "Add Task";
            tSaveBtn.Text = "Save";
            tSaveAnotherBtn.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
            tUpdatePanel.Update();
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {

            Helper.ShowAlert(this, "Successfully Added Task", Helper.AlertType.Success);

            //int selected_milestone = Convert.ToInt32(milestoneDDL.SelectedValue);

            //// Create Task Object
            //Task newTask = new Task();
            //newTask.taskName = tNameTxt.Text;
            //newTask.taskDescription = tDescTxt.Text;
            //newTask.startDate = Convert.ToDateTime(tStartTxt.Text);
            //newTask.endDate = Convert.ToDateTime(tEndTxt.Text);
            //newTask.teamID = TEST_TEAM_ID;
            //newTask.statusID = Convert.ToInt32(statusDDL.SelectedValue);

            //if (selected_milestone != -1)
            //{
            //    newTask.milestoneID = selected_milestone;
            //}

            //TaskBLL taskBLL = new TaskBLL();

            //if (taskBLL.Add(newTask))
            //{
            //    Helper.ShowAlert(this, "Successfully Added Task", Helper.AlertType.Success);
            //}
            //else
            //{
            //    Helper.ShowAlert(this, "Failed to Add Task", Helper.AlertType.Error);
            //}

        }

        //// Show Edit Modal
        //private void showEditTaskModal()
        //{
        //    tTitleLbl.Text = "Update Task";
        //    tSaveBtn.Text = "Update";
        //    tSaveBtn.Click += new EventHandler(updateTask_Click);

        //    // Fill Update Data

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
        //    tUpdatePanel.Update();
        //}

        // Update Task Event
        protected void updateTask_Click(object sender, EventArgs e)
        {

        }

    }
}