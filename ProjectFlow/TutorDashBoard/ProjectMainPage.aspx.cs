using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class ProjectMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["PassProjectID"] != null && Session["PassTeamID"] != null) {
                    InfoLabel.Text = Session["PassProjectID"].ToString();
                    InfoLabel.Text = "Project ID: " + Session["PassProjectID"].ToString() + " - " + Session["PassProjectName"].ToString()
                                     + " -> Team ID: " + Session["PassTeamID"].ToString() + " - " + Session["PassTeamName"].ToString();
                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }
            }
        }
    }
}