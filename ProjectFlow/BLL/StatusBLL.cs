using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class StatusBLL
    {

        public static string PENDING = "Pending";
        public static string WORK_IN_PROGRESS = "Work-in-progress";
        public static string VERIFICATON = "Verification";
        public static string COMPLETED = "Completed";

        public static string GetNextStatus(string currentStatus)
        {
            if (currentStatus == PENDING)
            {
                return WORK_IN_PROGRESS;
            }
            else if (currentStatus == WORK_IN_PROGRESS)
            {
                return VERIFICATON;
            }
            else if (currentStatus == VERIFICATON)
            {
                return COMPLETED;
            }
            else
            {
                return null;
            }
        }

        private enum STATUS
        {
            PENDING = 1,
            WORK_IN_PROGRESS = 2,
            VERIFICATION = 3,
            COMPLETED = 4
        }

        public Dictionary<int, string> Get()
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    var status = dbContext.Status.Select(x => new
                    {
                        Key = x.ID,
                        Value = x.status1
                    }).ToDictionary(y => y.Key, z => z.Value);

                    return status;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Retrieving Status: {e.Message}");
                return null;
            }
        }

        public Status GetStatusByID(int id)
        {
            try
            {

                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    Status status = dbContext.Status
                        .First(x => x.ID == id);

                    return status;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Retrieving Status: {e.Message}");
                return null;
            }
        }


        /// <summary>
        /// Update Status Accordingly (PENDING -> WIP, WIP -> VERIFICATION, VERIFICATION -> COMPLETED)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateStatusByTaskID(int id)
        {

            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    // Get Task
                    Task task = dbContext.Tasks
                        .First(x => x.taskID == id);

                    switch (task.statusID)
                    {

                        case (int)STATUS.PENDING:
                            task.statusID = (int)STATUS.WORK_IN_PROGRESS;
                            break;

                        case (int)STATUS.WORK_IN_PROGRESS:
                            task.statusID = (int)STATUS.VERIFICATION;
                            break;

                        case (int)STATUS.VERIFICATION:
                            task.statusID = (int)STATUS.COMPLETED;
                            break;

                        default:
                            return false;

                    }

                    dbContext.Entry(task).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Status: {e.Message}");
                    return false;
                }
            }

        }

    }
}