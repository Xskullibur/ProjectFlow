using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{

    public class GridViewTask
    {

    }

    public class TaskDAO
    {

        public IEnumerable<object> getTasksByTeamId(int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                    .Include("Milestone")
                    .Include("Status")
                    .Where(x => x.teamID == teamID)
                    .ToList().Select(y => new
                    {
                        ID = y.taskID,
                        Task = y.taskName,
                        Description = y.taskDescription,
                        MileStone = y.Milestone == null ? "Nothing" : y.Milestone.milestoneName,
                        Start = y.startDate,
                        End = y.endDate,
                        Allocation = y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + b.TeamMember.Student.username),
                        Status = y.Status.status1
                    });

                return list;

            }
        }
    }
}