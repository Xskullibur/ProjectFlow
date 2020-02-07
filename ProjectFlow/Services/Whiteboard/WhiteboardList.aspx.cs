using ProjectFlow.BLL;
using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services.Whiteboard
{
    public partial class WhiteboardList : System.Web.UI.Page
    {
        private List<WhiteboardSession> Board_Sessions
        {
            get => Session["WhiteboardSession"] as List<WhiteboardSession>;
            set {
                Session["WhiteboardSession"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SetHeader("Whiteboard Sessions");
                refreshGridView();
            }
        }

        private void refreshGridView()
        {
            var projectTeam = (Master as ServicesWithContent).CurrentProjectTeam;

            WhiteboardSessionBLL sessionBLL = new WhiteboardSessionBLL();
            List<WhiteboardSession> sessions = sessionBLL.GetWhiteboardSessionsByTeamID(projectTeam.teamID)
                .OrderByDescending(x => x.creationDateTime)
                .ToList();

            sessionGrid.DataSource = sessions;
            sessionGrid.DataBind();

            Board_Sessions = sessions;
        }

        protected void sessionGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            sessionGrid.PageIndex = e.NewPageIndex;
            refreshGridView();
        }

        protected void sessionGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = sessionGrid.SelectedRow.RowIndex;
            var whiteboardSession = Board_Sessions[index + (sessionGrid.PageIndex * sessionGrid.PageSize)];

            // Navigate to Whiteboard
            Response.Redirect("Whiteboard.aspx/?Action=Join&SessionID=" + whiteboardSession.sessionID);
        }

        protected void sessionGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Make gridview row clickable
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass += "pointer";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Go to this Whiteboard";
            }
        }

        protected void createBtn_Click(object sender, EventArgs e)
        {
            string groupName = sessionNameTxt.Text;
            Response.Redirect("Whiteboard.aspx/?Action=Create&GroupName=" + groupName);
        }
    }
}