using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class StatusDAO
    {

        public Dictionary<int, string> Get()
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

    }
}