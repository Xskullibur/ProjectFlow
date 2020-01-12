using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class JobDetailsBLL
    {

        public List<QRTZ_JOB_DETAILS> GetJobDetailsByTaskID(int taskID)
        {
            string strTaskID = taskID.ToString();

            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                List<QRTZ_JOB_DETAILS> jobDetails = dbContext.QRTZ_JOB_DETAILS
                    .Include(x => x.QRTZ_TRIGGERS)
                    .Where(x => x.JOB_NAME.Contains(strTaskID))
                    .ToList();

                return jobDetails;
            }
        }

    }
}