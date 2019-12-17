using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{

    public class TaskDAO
    {
        /// <summary>
        /// Add a new Task into DB with its Allocations
        /// </summary>
        /// <param name="task">Task to be Added</param>
        /// <param name="taskAllocations">Members assigned to the Task</param>
        /// <returns>Boolean</returns>
        public bool Add(Task task, List<TaskAllocation> taskAllocations)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (task != null)
                {
                    try {
                        
                        dbContext.Tasks.Add(task);      // Add Task to DB
                        dbContext.TaskAllocations.AddRange(taskAllocations);        // Add Allocations to DB

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

        /// <summary>
        /// Gets all Task information (incl. Task Allocations) By Team ID
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, Milestone, StartDate, EndDate, Allocations, Status)
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