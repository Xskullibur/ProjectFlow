using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services.Whiteboard
{
    public partial class Whiteboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetHeader("Whiteboard");
        }
    }
}