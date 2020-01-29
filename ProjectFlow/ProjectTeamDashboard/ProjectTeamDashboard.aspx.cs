using ProjectFlow.BLL;
using ProjectFlow.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ProjectFlow.ProjectTeamDashboard
{
    public partial class ProjectTeamDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectTeam currentTeam = (Master as ServicesWithContent).CurrentProjectTeam;

            TaskBLL taskBLL = new TaskBLL();
            List<Task> tasks = taskBLL.GetOngoingTasksByTeamId(currentTeam.teamID);

            FillPriorityTasks(tasks);
            FillUpcomingTasks(tasks);
            FillOverdueTasks(tasks);
        }

        public void FillPriorityTasks(List<Task> tasks)
        {
            PriorityBLL priorityBLL = new PriorityBLL();
            List<Priority> priorities = priorityBLL.Get();

            int count = 0;

            foreach (Priority priority in priorities)
            {
                count++;
                string priorityName = priority.priority1;
                var priorityTasks = tasks.Where(x => x.priorityID == priority.ID).ToList();
                double completedTasks = priorityTasks.Count(x => x.Status.status1 == "Completed");
                double uncompletedTasks = priorityTasks.Count(x => x.Status.status1 != "Completed");
                double totalTasks = priorityTasks.Count();

                Panel panel = new Panel();
                panel.ID = $"{priorityName}_panel";
                panel.CssClass = "col-4 border border-secondary";

                string chartID = $"{priorityName}_chart";

                LiteralControl literalControl = new LiteralControl();
                literalControl.Text = $@"<div class='row'>
                                            <div class='col'>
                                                <h5 class='text-center'>{priorityName} Priority Tasks</h5>
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col'>
                                                <canvas id='{chartID}'></canvas>
                                                {completedTasks}/{totalTasks}
                                            </div>
                                        </div>";

                panel.Controls.Add(literalControl);
                TaskPanel.Controls.Add(panel);

                ClientScript.RegisterStartupScript(this.GetType(), $"{chartID}_script", $"<script type='text/javascript'>loadDoughnutChart('{chartID}', {completedTasks}, {uncompletedTasks});</script>");
            }

        }

        public void FillUpcomingTasks(List<Task> tasks)
        {
            List<Task> upcomingTasks = tasks.Where(x => x.endDate >= DateTime.Now.Date)
                .OrderBy(x => x.endDate)
                .Take(5)
                .ToList();

            TaskBLL taskBLL = new TaskBLL();
            var ds = taskBLL.ConvertToDataSource(upcomingTasks);

            upcomingTaskGrid.DataSource = ds;
            upcomingTaskGrid.DataBind();
        }

        public void FillOverdueTasks(List<Task> tasks)
        {
            List<Task> upcomingTasks = tasks.Where(x => x.endDate < DateTime.Now.Date)
                .OrderBy(x => x.endDate)
                .Take(5)
                .ToList();

            overdueTaskGrid.DataSource = upcomingTasks;
            overdueTaskGrid.DataBind();
        }

        [WebMethod]
        public static string LoadDoughnutChart()
        {
            var data = new
            {
                datasets = new[]
                {
                    new {
                        data = new[] { 50, 50 },
                        backgroundColor = new[]
                        {
                            "Green"
                        }
                    },
                }
            };

            return data.ToJSON();
        }

        protected void overdueTaskGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell DueDateCell = e.Row.Cells[0];

                // Task End Date
                DateTime EndDate = DateTime.ParseExact(e.Row.Cells[3].Text, "dd/MM/yyyy", null);
                int DaysLeft = TaskHelper.GetDaysLeft(EndDate);

                DueDateCell.Text = $"{Math.Abs(DaysLeft)} Days";

                if (DaysLeft >=  -5)
                {
                    DueDateCell.CssClass = "text-warning";
                }
                else if (DaysLeft <-5)
                {
                    DueDateCell.CssClass = "text-danger";
                }
            }
        }
    }

    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}