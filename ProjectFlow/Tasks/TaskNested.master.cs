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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> tempNames = new List<string>()
                {
                    "John", "Ben", "Tom", "Tuturu~"
                };

                allocationDLL.DataSource = tempNames;
                allocationDLL.DataBind();
            }
        }

        // Show Add Task Modal
        private void showAddTaskModal()
        {
            tTitleLbl.Text = "Add Task";
            tSaveBtn.Text = "Save";
            tSaveAnotherBtn.Visible = true;
            tSaveBtn.Click += new EventHandler(addTask_Click);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
            tUpdatePanel.Update();
        }

        // Show Edit Task Modal
        private void showEditTaskModal()
        {
            tTitleLbl.Text = "Update Task";
            tSaveBtn.Text = "Update";
            tSaveBtn.Click += new EventHandler(updateTask_Click);

            // Fill Update Data

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#taskModal').modal('toggle')", true);
            tUpdatePanel.Update();
        }

        // Show Add Modal Event
        protected void showTaskModal_Click(object sender, EventArgs e)
        {
            //showAddTaskModal();
            showEditTaskModal();
        }

        // Add Task Event
        protected void addTask_Click(object sender, EventArgs e)
        {
            
        }

        // Update Task Event
        protected void updateTask_Click(object sender, EventArgs e)
        {

        }

    }
}