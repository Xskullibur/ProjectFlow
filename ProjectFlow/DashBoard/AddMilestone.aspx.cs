using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.DashBoard
{
    public partial class AddMilestone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["PassProjectID"] != null && Session["PassTeamID"] != null)
                {
                    InfoLabel.Text = "Project ID: " + Session["PassProjectID"].ToString() + " - " + Session["PassProjectName"].ToString()
                                     + " -> Team ID: " + Session["PassTeamID"].ToString() + " - " + Session["PassTeamName"].ToString();
                    ShowMilestone();
                    PageSelectDP.SelectedIndex = 1;
                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }
            }
        }

        public string GetProjectID()
        {
            return Session["PassProjectID"].ToString();
        }

        public int GetTeamID()
        {
            return int.Parse(Session["PassTeamID"].ToString());
        }

        public void ShowMilestone()
        {
            MilestoneBLL milestoneBLL = new MilestoneBLL();
            List<Milestone> milestoneList = milestoneBLL.GetMilestoneByTeamID(GetTeamID());
            MilestoneGV.DataSource = milestoneList;
            MilestoneGV.DataBind();       
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageSelectDP.SelectedIndex == 0)
            {
                Response.Redirect("ProjectMainPage.aspx");
            }
        }

        protected void MilestoneGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            MilestoneGV.EditIndex = -1;
            ShowMilestone();
        }

        protected void MilestoneGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MilestoneGV.EditIndex = e.NewEditIndex;
            ShowMilestone();
        }

        protected void MilestoneGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            MilestoneGV.EditIndex = -1;
            ShowMilestone();
        }

        protected void MilestoneGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MilestoneGV.PageIndex = e.NewPageIndex;
            ShowMilestone();
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            MilestoneBLL bll = new MilestoneBLL();
            string name = NameTB.Text;
            string projectID = GetProjectID();
            int teamID = GetTeamID();
            string start = startTB.Text;
            string end = endTB.Text;

            List<string> errorList = bll.ValidateCreateMilestone(name, projectID, teamID, start, end);

            if(errorList.Count > 0)
            {
                string total = "";
                foreach(string error in errorList)
                {
                    total += error;
                }
                errorLabel.Text = total;
                NameTB.Text = name;
                startTB.Text = start.ToString();
                endTB.Text = end.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#addMilestone').modal('show');", true);
            }
            else
            {
                Response.Redirect("AddMilestone.aspx");
            }
        }
    }
}