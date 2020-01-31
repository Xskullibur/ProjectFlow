using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class TaskBLL
    {
        // All Tasks
        /// <summary>
        /// Get All Tasks (Incl. TaskAllocation, TeamMember, Student, Priority, Milestone, Status)
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public List<Task> GetAllTasksByTeamID(int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    var tasks = dbContext.Tasks
                        .Include("TaskAllocations.TeamMember.Student")
                        .Include("Priority")
                        .Include("Milestone")
                        .Include("Status")
                        .Where(x => x.teamID == teamID)
                        .OrderBy(x => x.startDate)
                        .ThenBy(x => x.endDate)
                        .ToList();

                    return tasks;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving All Task: {e.Message}\n");
                    return null;
                }
            }
        }


        // Ongoing Tasks
        /// <summary>
        /// Get Ongoing Tasks (Tasks that are not dropped)
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public List<Task> GetOngoingTasksByTeamId(int teamID)
        {
            try
            {
                var ongoingTasks = GetAllTasksByTeamID(teamID)
                    .Where(x => x.dropped == false)
                    .ToList();

                return ongoingTasks;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving Ongoing Task: {e.Message}\n");
                return null;
            }
        }


        // Get Ongoing Tasks Between Start and End Dates
        /// <summary>
        /// Get Ongoing Tasks Between a Specified Start and End Date (Used for Calendar View)
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Task> GetOngoingTasksBetween(DateTime startDate, DateTime endDate)
        {

            try
            {
                if (HttpContext.Current.Session["CurrentProjectTeam"] != null)
                {
                    ProjectTeam projectTeam = HttpContext.Current.Session["CurrentProjectTeam"] as ProjectTeam;
                    int teamID = projectTeam.teamID;

                    var tasks = GetOngoingTasksByTeamId(teamID)
                        .Where(x => (x.startDate >= startDate && x.startDate <= endDate))
                        .OrderBy(x => x.priorityID)
                        .ToList();

                    return tasks;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving Ongoing Task Between Dates: {e.Message}\n");
                return null;
            }

        }


        // Ongoing Task DataSource
        /// <summary>
        /// Get Ongoing DataSource (ID, Task, Description, MileStone, Start, End, Allocation, Status, Priority)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<object> GetOngoingDataSource(int id)
        {
            try
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
                        Status = y.Status.status1,
                        Priority = y.Priority.priority1
                    }).ToList();

                return ds;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving Ongoing Task DataSource: {e.Message}\n");
                return null;
            }
        }


        // Dropped Tasks
        /// <summary>
        /// Get Dropped Tasks
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public List<Task> GetDroppedTasksByTeamID(int teamID)
        {
            try
            {
                var droppedTasks = GetAllTasksByTeamID(teamID)
                    .Where(x => x.dropped == true)
                    .ToList();

                return droppedTasks;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving All Dropped Task: {e.Message}\n");
                return null;
            }
        }


        // Dropped Task DataSource
        /// <summary>
        /// Gets all Drropped Task information (incl. Task Allocations) By Team ID
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, Milestone, StartDate, EndDate, Allocations, Status)
        public IEnumerable<object> GetDroppedTaskDataSource(int teamID)
        {
            try
            {
                var droppedTask = GetDroppedTasksByTeamID(teamID)
                    .Select(y => new
                    {
                        ID = y.taskID,
                        Task = y.taskName,
                        Description = y.taskDescription,
                        MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                        Start = y.startDate,
                        End = y.endDate,
                        Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                        Status = y.Status.status1,
                        Priority = y.Priority.priority1
                    }).ToList();

                return droppedTask;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\n\nError While Retrieving Dropped Task DataSource: {e.Message}\n");
                return null;
            }
        }


        public IEnumerable<object> ConvertToDataSource(List<Task> tasks)
        {
            var ds = tasks.Select(y => new
            {
                ID = y.taskID,
                Task = y.taskName,
                Description = y.taskDescription,
                MileStone = y.Milestone == null ? "-" : y.Milestone.milestoneName,
                Start = y.startDate,
                End = y.endDate,
                Allocation = y.TaskAllocations.Count == 0 ? "-" : y.TaskAllocations.Aggregate("", (a, b) => (a == "" ? "" : a + ", ") + (b.TeamMember.Student.firstName + " " + b.TeamMember.Student.lastName)),
                Status = y.Status.status1,
                Priority = y.Priority.priority1
            }).ToList();

            return ds;
        }


        public IEnumerable<object> GetDroppedTaskByTeamIdWithStudent(int teamID, Student student)
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
                        .Include("TaskAllocations.TeamMember.Student")
                        .Include("Priority")
                        .Include("Milestone")
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

    }
}