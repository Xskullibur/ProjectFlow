using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{

    public class TaskDAO
    {

        /**
         * Insert Task
         **/
        public bool Add(Task task)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (task != null)
                {
                    try
                    {
                        // Add to DB Context and Save Changes
                        dbContext.Tasks.Add(task);
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Error While Adding Task: {e.Message}");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }



        /**
         * Get Task By Team ID
         * (ID, Task, Description, Milestone, StartDate, EndDate, Allocations, Status)
         **/

        public IEnumerable<object> getTasksByTeamID(int teamID)
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