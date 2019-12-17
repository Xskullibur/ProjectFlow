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
    }
}