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
    }
}