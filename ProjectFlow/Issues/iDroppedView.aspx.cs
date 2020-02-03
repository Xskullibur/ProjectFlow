﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;

namespace ProjectFlow.Issues
{
    public partial class IDroppedView : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Master.GetCurrentProjectTeam() == null)
            {
                if (Master.GetCurrentIdentiy().IsTutor)
                {
                    Response.Redirect("/TutorDashboard/ProjectTeamMenu.aspx");
                }
                else if (Master.GetCurrentIdentiy().IsStudent)
                {
                    Response.Redirect("/StudentDashboard/studentProject.aspx");
                }
            }

            //Master.refreshGrid += new EventHandler(refreshBtn_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.changeSelectedView(IssueNested.IssueViews.iDroppedView);
                refreshData();
            }

            IssueView.Font.Size = 11;
        }

        private void refreshData()
        {
            IssueBLL issueBLL = new IssueBLL();

            // Get Current Project Team
            ProjectTeam currentTeam = Master.GetCurrentProjectTeam();

            IssueView.DataSource = issueBLL.GetDroppedIssueByTeamId(currentTeam.teamID);
            IssueView.DataBind();

            if (IssueView.Rows.Count > 0)
            {
                IssueView.HeaderRow.TableSection = TableRowSection.TableHeader;
                IssueView.UseAccessibleHeader = true;
            }
        }
        protected void IssueView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                }
                else
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell PublicCell = e.Row.Cells[5];
                        if (PublicCell.Text == "True")
                        {
                            PublicCell.Text = "<i class='fa fa-check-circle fa-lg text-success'></i>";
                        }

                        else
                        {
                            PublicCell.Text = "<i class='fa fa-times-circle fa-lg text-danger'></i>";
                        }
                    }
                }
            }

        }

        //Pagination
        protected void IssueView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IssueView.PageIndex = e.NewPageIndex;
            refreshData();
        }
    }
}