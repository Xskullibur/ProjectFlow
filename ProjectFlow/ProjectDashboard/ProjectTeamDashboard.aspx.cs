using ProjectFlow.BLL;
using ProjectFlow.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.ProjectTeamDashboard
{
    public partial class ProjectTeamDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set title
                (this.Master as ServicesWithContent).Header = "Overall Project Dashboard";
            }

            ProjectTeam currentTeam = (Master as ServicesWithContent).CurrentProjectTeam;

            TaskBLL taskBLL = new TaskBLL();
            List<Task> tasks = taskBLL.GetOngoingTasksByTeamId(currentTeam.teamID);

            FillPriorityTasks(tasks);
            FillUpcomingTasks(tasks);
            FillOverdueTasks(tasks);
            FillMilestoneBar(tasks, currentTeam.teamID);
        }

        public void FillMilestoneBar(List<Task> tasks, int teamID)
        {

            string startString = "<ul id='progressbar' class='cdt-step-progressbar horizontal pt-5'>";
            string endString = "</ul>";
            int count = 1;

            MilestoneBLL milestoneBLL = new MilestoneBLL();
            List<Milestone> milestones = milestoneBLL.GetMilestonesByTeamID(teamID)
                .OrderBy(x => x.endDate)
                .ToList();

            foreach (Milestone milestone in milestones)
            {
                List<Task> milestoneTasks = tasks.Where(x => x.milestoneID == milestone.milestoneID)
                    .ToList();

                int totalTasks = milestoneTasks.Count();
                int completedTasks = milestoneTasks.Count(x => x.Status.status1 == StatusBLL.COMPLETED);

                string col = $@"
                    <li class='pb-0'>
                        <span class='indicator'>{count}</span>
                        <p class='title font-weight-bold'>{milestone.milestoneName}</p>
                        <p class='content'>{completedTasks} / {totalTasks} Completed</p>
                    </li>
                ";

                startString += col;
                count++;
            }

            string progressBar = startString + endString;
            milestoneLiteral.Text = progressBar;

            int? milestoneID = tasks.GroupBy(x => x.milestoneID)
                .First(x => x.Count(task => task.Status.status1 == StatusBLL.COMPLETED) != x.Count()).Key;

            int activeIndex = milestones.FindIndex(x => x.milestoneID == milestoneID);

            ClientScript.RegisterStartupScript(this.GetType(), $"milestone_script", $"<script type='text/javascript'>loadProgressBar({activeIndex});</script>");
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
                panel.CssClass = "col-4 p-0";

                string chartID = $"{priorityName}_chart";

                LiteralControl literalControl = new LiteralControl
                {
                    Text = $@"<div class='card card-body projectflow-card-shadow p-2'>
                                <div class='row'>
                                    <div class='col'>
                                        <h5 class='text-center'>{priorityName} Priority Tasks</h5>
                                    </div>
                                </div>
                                <div class='row'>
                                    <div class='col'>
                                        <canvas id='{chartID}'></canvas>
                                        {completedTasks}/{totalTasks}
                                    </div>
                                </div>
                            </div>"
                };

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
}