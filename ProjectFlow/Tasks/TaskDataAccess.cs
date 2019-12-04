using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Tasks
{
    public class TaskDataAccess
    {
        
        public List<TasksTB> getAllTasks()
        {
            using (ProjectFlowEntities db = new ProjectFlowEntities())
            {
                return db.TasksTBs.Select(x => x).ToList();
            }
        }

        public List<TasksTB> getTaskByMemberId(int id)
        {
            using (ProjectFlowEntities db = new ProjectFlowEntities())
            {
                return db.TaskAllocationTBs.Where(x => x.memberID == id).Select(y => y.TasksTB).ToList();
            }
        }

    }
}