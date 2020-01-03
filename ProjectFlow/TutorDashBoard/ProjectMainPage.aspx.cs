﻿using ProjectFlow.BLL;
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
                    InfoLabel.Text = "Project ID: " + Session["PassProjectID"].ToString() + " - " + Session["PassProjectName"].ToString()
                                     + " -> Team ID: " + Session["PassTeamID"].ToString() + " - " + Session["PassTeamName"].ToString();
                    ShowMember(int.Parse(Session["PassTeamID"].ToString()));

                }
                else
                {
                    Response.Redirect("ProjectTeamMenu.aspx");
                }
            }
        }

        public void ShowMember(int TeamID)
        {
            ProjectBLL projectBLL = new ProjectBLL();
            if (projectBLL.CheckProjectMemberExist(TeamID))
            {
                List<TeamMember> memberList = new List<TeamMember> { };
                memberList = projectBLL.GetTeamMember(TeamID);
                MemberGV.DataSource = memberList;
                MemberGV.DataBind();
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {

        }
    }
}