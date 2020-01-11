using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class StatusBLL
    {

        public static string PENDING = "Pending";
        public static string WORK_IN_PROGRESS = "Work-in-progress";
        public static string VERIFICATON = "Verification";
        public static string COMPLETED = "Completed";

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

    }
}