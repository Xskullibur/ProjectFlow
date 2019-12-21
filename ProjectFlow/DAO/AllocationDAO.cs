using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class AllocationDAO
    {
        /// <summary>
        /// Adds a single Task Allocation to DB
        /// </summary>
        /// <param name="taskAllocation"></param>
        /// <returns>Boolean</returns>
        public bool Add(TaskAllocation taskAllocation)
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    dbContext.TaskAllocations.Add(taskAllocation);
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        /// <summary>
        /// Adds a List of Allocations to DB
        /// </summary>
        /// <param name="taskAllocations"></param>
        /// <returns>Boolean</returns>
        public bool AddRange(List<TaskAllocation> taskAllocations)
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    dbContext.TaskAllocations.AddRange(taskAllocations);
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}