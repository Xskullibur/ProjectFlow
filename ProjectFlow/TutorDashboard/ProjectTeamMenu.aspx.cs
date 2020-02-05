using ProjectFlow.BLL;
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
    public partial class ProjectTeamMenu : System.Web.UI.Page
    {
        ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetProjectID() != null)
                {
                    Session["PassTeamID"] = null;
                    Session["PassTeamName"] = null;
                    ShowTeam();
                    InfoLabel.Text = "Module: (" + GetProjectID() + ") " + Session["PassProjectName"].ToString() + " >>> (Team Select)";
                    this.SetHeader("Teams that are in this Module");

                    
                }
                else
                {
                    Response.Redirect("ProjectMenu.aspx");
                }
            }              
        }
        public string GetProjectID()
        {
            return (Master as ServicesWithContent).CurrentProject.projectID;
        }

        private void OpenModel()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CreateTeam').modal('show');", true);
        }

        private void CloseModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#CreateTeam').modal('hide')", true);
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            string teamName = NameTB.Text;
            string desc = DescTB.Text;
            int group = int.Parse(GroupDP.SelectedValue);
            List<string> errorList = new List<string> { };


            errorList = bll.InsertProjectTeam(teamName, desc, group, Session["PassProjectID"].ToString());
            if (errorList.Count > 0)
            {
                string total = "";
                foreach (string errorItem in errorList)
                {
                    total += errorItem;
                }
                errorLabel.Text = total;
                NameTB.Text = teamName;
                DescTB.Text = desc;
                OpenModel();
            }
            else
            {
                Master.ShowAlert("Successfully Created Team", BootstrapAlertTypes.SUCCESS);
                ClearModel();
                CloseModel();
                ShowTeam();
            }
            
        }

        public void ShowTeam()
        {
            PageSelectDP.SelectedIndex = 0;
            ProjectBLL projectBLL = new ProjectBLL();
           
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectBLL.GetProjectTeam(GetProjectID());
            TeamGV.DataSource = teamList;
            TeamGV.DataBind();
        }

        private void SearchGroup(int Group)
        {
            List<ProjectTeam> teamList = new List<ProjectTeam> { };
            teamList = projectTeamBLL.SearchGroup(GetProjectID(), Group);
            TeamGV.DataSource = teamList;
            TeamGV.DataBind();
        }

        protected void TeamGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TeamGV.EditIndex = -1;
            ShowTeam();
        }

        protected void TeamGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = TeamGV.Rows[e.RowIndex];          
            ProjectBLL projectBLL = new ProjectBLL();

            int teamID = int.Parse(row.Cells[0].Text);
            TextBox editName = (TextBox)row.FindControl("editNameTB");
            TextBox editDesc = (TextBox)row.FindControl("editDescTB");
            DropDownList editGroup = (DropDownList)row.FindControl("editGroupDP");
                    
            List<string> error = projectBLL.UpdateProjectTeam(teamID, editName.Text, editDesc.Text, int.Parse(editGroup.SelectedValue));
            TeamGV.EditIndex = -1;
            Master.ShowAlert("Successfully Updated Team", BootstrapAlertTypes.SUCCESS);
            ShowTeam();
        }

        protected void TeamGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = TeamGV.SelectedRow;
            Label name = (Label)row.FindControl("nameLabel");

            Session["PassTeamID"] = row.Cells[0].Text;
            Session["PassTeamName"] = name.Text;

            // Alson Edit
            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
            ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByTeamID(Convert.ToInt32(row.Cells[0].Text));

            Session["CurrentProjectTeam"] = projectTeam;
            // Alson Edit Ends


            (Master as ServicesWithContent).SetCurrentProjectTeam(projectTeamBLL.GetProjectTeamByTeamID(Convert.ToInt32(row.Cells[0].Text)));
            Response.Redirect("../ProjectDashboard/ProjectTeamDashboard.aspx");
        }

        protected void TeamGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TeamGV.EditIndex = e.NewEditIndex;
            ShowTeam();
        }

        protected void TeamGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TeamGV.PageIndex = e.NewPageIndex;
            ShowTeam();
        }

        private void ClearModel()
        {
            NameTB.Text = "";
            DescTB.Text = "";
            errorLabel.Text = "";
        }

        protected void TeamGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)TeamGV.Rows[e.RowIndex];
            projectTeamBLL.DeleteTeam(int.Parse(row.Cells[0].Text));
            ShowTeam();
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectTeamMenu.aspx");
        }

        protected void PageSelectDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageSelectDP.SelectedIndex == 1)
            {
                Response.Redirect("RestoreDashboard/ProjectTeamRestore.aspx");
            }
        }

        protected void createAnotherBtn_Click(object sender, EventArgs e)
        {
            ProjectBLL bll = new ProjectBLL();
            string teamName = NameTB.Text;
            string desc = DescTB.Text;
            int group = int.Parse(GroupDP.SelectedValue);
            List<string> errorList = new List<string> { };

            errorList = bll.InsertProjectTeam(teamName, desc, group, Session["PassProjectID"].ToString());

            if (errorList.Count > 0)
            {
                string total = "";
                foreach (string errorItem in errorList)
                {
                    total += errorItem;
                }
                errorLabel.Text = total;
                NameTB.Text = teamName;
                DescTB.Text = desc;                
            }
            else
            {
                Master.ShowAlert("Successfully Created Team", BootstrapAlertTypes.SUCCESS);
                ClearModel();
            }
            ShowTeam();
            OpenModel();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            //SearchTeam(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            ShowTeam();
        }

        protected void sortGroupDP_SelectedIndexChanged(object sender, EventArgs e)
        {         
            if(int.Parse(sortGroupDP.SelectedValue) == 0)
            {
                ShowTeam();
            }
            else
            {
                SearchGroup(int.Parse(sortGroupDP.SelectedValue));
            }
        }

        protected void bulkCreateBtn_Click(object sender, EventArgs e)
        {

        }

        protected void bulkAddBtn_Click(object sender, EventArgs e)
        {
            int total = int.Parse(amountDP.SelectedValue);
            int group = int.Parse(bulkGroupDP.SelectedValue);
            ProjectBLL bll = new ProjectBLL();

            for(int i = 1; i <= total; i++)
            {
                List<string> errorList = bll.InsertProjectTeam("team" + i.ToString(), "", group, Session["PassProjectID"].ToString());
            }
            ShowTeam();
            Master.ShowAlert(total + " team add for group "  + group, BootstrapAlertTypes.SUCCESS);
        }

        protected void lockBtn_Click(object sender, EventArgs e)
        {
            projectTeamBLL.lockTeam(GetProjectID(), true, int.Parse(groupAccessDP.SelectedValue));
            Master.ShowAlert("Teams locked", BootstrapAlertTypes.SUCCESS);
            ShowTeam();
        }

        protected void unlockDP_Click(object sender, EventArgs e)
        {
            projectTeamBLL.lockTeam(GetProjectID(), false, int.Parse(groupAccessDP.SelectedValue));
            Master.ShowAlert("Team open for people to join", BootstrapAlertTypes.SUCCESS);
            ShowTeam();
        }

    }
}