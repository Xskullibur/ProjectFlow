using ProjectFlow.BLL;
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
                TeamMemberBLL memberBLL = new TeamMemberBLL();
                var memberList = memberBLL.GetTeamMembersByTeamID(TEST_TEAM_ID);

                allocationDDL.DataSource = memberList;
                allocationDDL.DataTextField = "Value";
                allocationDDL.DataValueField = "Key";

                allocationDDL.DataBind();
            }
        }

        // Show Add Modal
        private void showAddTaskModal()
        {
            tTitleLbl.Text = "Add Task";
            tSaveBtn.Text = "Save";
            tSaveAnotherBtn.Visible = true;
            tSaveBtn.Click += new EventHandler(addTask_Click);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
            tUpdatePanel.Update();
        }

        // Show Edit Modal
        private void showEditTaskModal()
        {
            tTitleLbl.Text = "Update Task";
            tSaveBtn.Text = "Update";
            tSaveBtn.Click += new EventHandler(updateTask_Click);

            // Fill Update Data

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
            tUpdatePanel.Update();
        }

        // Add Task OnClick Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            showAddTaskModal();
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {
            var allocations = allocationDDL.SelectedValue;
        }

        // Update Task Event
        protected void updateTask_Click(object sender, EventArgs e)
        {

        }

    }
}