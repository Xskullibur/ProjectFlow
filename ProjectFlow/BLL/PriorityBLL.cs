using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class PriorityBLL
    {

        public Dictionary<int, string> GetDict()
        {
            Dictionary<int, string> priorityDict = Get().ToDictionary(x => x.ID, x => x.priority1);
            return priorityDict;
        }

        public List<Priority> Get()
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                List<Priority> priorities = dbContext.Priorities.Select(x => x).ToList();
                return priorities;
            }
        }
    }
}