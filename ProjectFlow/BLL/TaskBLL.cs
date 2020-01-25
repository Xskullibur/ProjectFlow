using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class TaskBLL
    {

        public List<Task> GetOngoingTasksBetween(DateTime startDate, DateTime endDate)
        {

            if (HttpContext.Current.Session["CurrentProjectTeam"] != null)
            {
                ProjectTeam projectTeam = HttpContext.Current.Session["CurrentProjectTeam"] as ProjectTeam;
                int teamID = projectTeam.teamID;

                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.dropped != true)
                        .Where(x => (x.startDate >= startDate && x.startDate <= endDate))
                        .ToList();

                    return list;
                }
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<object> GetOngoingDataSource(int id)
        {
            var ds = GetOngoingTasksByTeamId(id)
                .Select(y => new
                {
                    ID = y.taskID,
                    Task = y.taskName,
                    Description = y.taskDescription,
                    MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                    Start = y.startDate,
                    End = y.endDate,
                    Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                    Status = y.Status.status1
                }).ToList();

            return ds;
        }

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
                    try
                    {

                        dbContext.Tasks.Add(task);      // Add Task to DB
                        dbContext.TaskAllocations.AddRange(taskAllocations);        // Add Allocations to DB

                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error While Adding Task: {e.InnerException.Message}");
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
        /// Update Task and Allocations
        /// </summary>
        /// <param name="task"></param>
        /// <param name="updated_Allocations"></param>
        /// <returns>Boolean</returns>
        public bool Update(Task task, List<TaskAllocation> updated_Allocations)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    dbContext.Tasks.Attach(task);

                    // Update Task
                    dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;

                    // Update Allocations
                    List<TaskAllocation> old_Allocations = dbContext.TaskAllocations.Where(x => x.taskID == task.taskID).ToList();

                    List<TaskAllocation> added_Allocations = updated_Allocations.Except(old_Allocations).ToList();
                    List<TaskAllocation> deleted_allocations = old_Allocations.Except(updated_Allocations).ToList();

                    deleted_allocations.ForEach(x => dbContext.Entry(x).State = System.Data.Entity.EntityState.Deleted);
                    added_Allocations.ForEach(x => dbContext.Entry(x).State = System.Data.Entity.EntityState.Added);

                    // Save Changes
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Task: {e.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Drop Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Boolean</returns>
        public bool Delete(Task task)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    task.dropped = true;
                    dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Deleted Task: {e.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Restore Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool Restore(Task task)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    task.dropped = false;
                    dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Deleted Task: {e.Message}");
                    return false;
                }
            }
        }


        /// <summary>
        /// Get Task by Task ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        public Task GetTaskByID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    Task task = dbContext.Tasks
                        .Include(x => x.Status)
                        .First(x => x.taskID == id);
                    return task;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }
            }
        }


        /// <summary>
        /// Gets all Ongoing Task information (incl. Task Allocations) By Team ID
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, Milestone, StartDate, EndDate, Allocations, Status)
        public List<Task> GetOngoingTasksByTeamId(int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {

                    var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                        .Include("Milestone")
                        .Include("Status")
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.dropped != true)
                        .OrderBy(x => x.startDate)
                        .ThenBy(x => x.endDate)
                        .ToList();

                    return list.ToList();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }


        public IEnumerable<object> GetOngoingTasksByTeamIdWithStudent(int teamID, Student currentUser)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {

                    var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                        .Include("Milestone")
                        .Include("Status")
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.TaskAllocations.Select(allocation => allocation.TeamMember.Student.studentID).Contains(currentUser.studentID))
                        .Where(x => x.dropped != true)
                        .OrderBy(x => x.startDate)
                        .ThenBy(x => x.endDate)
                        .ToList().Select(y => new
                        {
                            ID = y.taskID,
                            Task = y.taskName,
                            Description = y.taskDescription,
                            MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                            Start = y.startDate,
                            End = y.endDate,
                            Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                            Status = y.Status.status1
                        });

                    return list.ToList();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }


        /// <summary>
        /// Gets all Drropped Task information (incl. Task Allocations) By Team ID
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, Milestone, StartDate, EndDate, Allocations, Status)
        public IEnumerable<object> GetDroppedTasksByTeamId(int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {

                    var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                        .Include("Milestone")
                        .Include("Status")
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.dropped == true)
                        .ToList().Select(y => new
                        {
                            ID = y.taskID,
                            Task = y.taskName,
                            Description = y.taskDescription,
                            MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                            Start = y.startDate,
                            End = y.endDate,
                            Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                            Status = y.Status.status1
                        });

                    return list.ToList();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }


        public  IEnumerable<object> GetDroppedTaskByTeamIdWithStudent(int teamID, Student student)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {

                    var list = dbContext.Tasks.Include("TaskAllocations.TeamMember.Student")
                        .Include("Milestone")
                        .Include("Status")
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.TaskAllocations.Select(allocated => allocated.TeamMember.Student.studentID).Contains(student.studentID))
                        .Where(x => x.dropped == true)
                        .ToList().Select(y => new
                        {
                            ID = y.taskID,
                            Task = y.taskName,
                            Description = y.taskDescription,
                            MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                            Start = y.startDate,
                            End = y.endDate,
                            Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                            Status = y.Status.status1
                        });

                    return list.ToList();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }

            }
        }


        /// <summary>
        /// Get ProjectTeam by Task ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>ProjectTeam</returns>
        public ProjectTeam GetProjectTeamByTaskID(int taskID)
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    ProjectTeam team = dbContext.Tasks.Where(x => x.taskID == taskID)
                        .Select(x => x.ProjectTeam).First();

                    return team;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Retrieving ProjectTeam: {e.Message}");
                return null;
            }
        }

    }
}