using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services
{

    /// <summary>
    /// Displays project tasks
    /// </summary>
    [Mark(Category = Category.General, RequiresRoles = Role.BasicUser | Role.PremiumUser)]
    public partial class TasksService : ServicePage
    {

        public TasksService() : base("Tasks", "Services/Tasks.aspx") {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}