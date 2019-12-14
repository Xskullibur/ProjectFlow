using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Tasks
{
    public partial class tDetailedView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TaskBLL taskBLL = new TaskBLL();
            
            taskGrid.DataSource = taskBLL.GetTasksByTeamId(2);
            taskGrid.DataBind();

        }
    }
}