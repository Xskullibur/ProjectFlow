using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class MilestoneDAO
    {
        public IEnumerable<object> GetMilestonesByTeamID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var teamMilestone = dbContext.Milestones.Where(x => x.teamID == id)
                    .Select(y => new
                    {
                        ID = y.milestoneID,
                        Milestone = y.milestoneName,
                        Start = y.startDate,
                        End = y.endDate
                    }).ToList();

                return teamMilestone;
            }
        }
    }
}