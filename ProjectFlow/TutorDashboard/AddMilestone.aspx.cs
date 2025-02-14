﻿using ProjectFlow.BLL;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
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
        MilestoneBLL milestoneBLL = new MilestoneBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                ShowMilestone();
                this.SetHeader("Milestone for team: (" + (Master as ServicesWithContent).CurrentProjectTeam.teamName + ")");
            }
        }

        public string GetProjectID()
        {            
            return (Master as ServicesWithContent).CurrentProject.projectID;
        }

        public int GetTeamID()
        {           
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        public void OpenModel()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#addMilestone').modal('show');", true);
        }

        public void CloseModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#CreateMember').modal('hide')", true);
        }

        public void ShowMilestone()
        {
            List<Milestone> milestoneList = milestoneBLL.GetMilestonesByTeamID(GetTeamID());
            MilestoneGV.DataSource = milestoneList;
            MilestoneGV.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
        }

        public void SearchMilestone(string search)
        {
            List<Milestone> milestoneList = milestoneBLL.SearchMilestone(GetTeamID(), search);
            MilestoneGV.DataSource = milestoneList;
            MilestoneGV.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
            GridViewRow row = MilestoneGV.Rows[e.RowIndex];

            int milestoneID = int.Parse(row.Cells[0].Text);
            TextBox editName = (TextBox)row.FindControl("editNameTB");
            TextBox editStartDate = (TextBox)row.FindControl("editStartTB");
            TextBox editEndDate = (TextBox)row.FindControl("editEndTB");

            List<string> errorList = milestoneBLL.ValidateUpdateMilestone(milestoneID, editName.Text, editStartDate.Text, editEndDate.Text);           
            MilestoneGV.EditIndex = -1;
            Master.ShowAlert("Milestone successfully updated", BootstrapAlertTypes.SUCCESS);
            ShowMilestone();
        }

        protected void MilestoneGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MilestoneGV.PageIndex = e.NewPageIndex;
            ShowMilestone();
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {           
            string name = NameTB.Text;
            string projectID = GetProjectID();
            int teamID = GetTeamID();
            string start = startTB.Text;
            string end = endTB.Text;

            List<string> errorList = milestoneBLL.ValidateCreateMilestone(name, projectID, teamID, start, end);

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
                OpenModel();
            }
            else
            {
                CloseModel();
                Master.ShowAlert("Milestone successfully created", BootstrapAlertTypes.SUCCESS);
                ShowMilestone();
            }
        }

        protected void MilestoneGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)MilestoneGV.Rows[e.RowIndex];
            milestoneBLL.DeleteMilestone(int.Parse(row.Cells[0].Text));
            ShowMilestone();
            Master.ShowAlert("Milestone successfully deleted", BootstrapAlertTypes.SUCCESS);
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddMilestone.aspx");
        }
        private void ClearModel()
        {
            NameTB.Text = "";
            startTB.Text = "";
            endTB.Text = "";
        }
        protected void addAnotherBtn_Click(object sender, EventArgs e)
        {
            string name = NameTB.Text;
            string projectID = GetProjectID();
            int teamID = GetTeamID();
            string start = startTB.Text;
            string end = endTB.Text;

            List<string> errorList = milestoneBLL.ValidateCreateMilestone(name, projectID, teamID, start, end);

            if (errorList.Count > 0)
            {
                string total = "";
                foreach (string error in errorList)
                {
                    total += error;
                }
                errorLabel.Text = total;
                NameTB.Text = name;
                startTB.Text = start.ToString();
                endTB.Text = end.ToString();
            }
            else
            {
                

                Master.ShowAlert("Milestone successfully created", BootstrapAlertTypes.SUCCESS);
                ShowMilestone();
            }
            ClearModel();
            OpenModel();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchMilestone(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            ShowMilestone();
        }
    }
}