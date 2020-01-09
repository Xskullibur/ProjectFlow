﻿using ProjectFlow.BLL;
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
                    ShowMilestone(int.Parse(Session["PassTeamID"].ToString()));
                    PageSelectDP.SelectedIndex = 1;
                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }
            }
        }

        public void ShowMilestone(int TeamID)
        {
            MilestoneBLL milestoneBLL = new MilestoneBLL();
            List<Milestone> milestoneList = milestoneBLL.GetMilestoneByTeamID(TeamID);
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
    }
}