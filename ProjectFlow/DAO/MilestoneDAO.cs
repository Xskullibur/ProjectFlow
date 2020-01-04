using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class MilestoneDAO
    {
        public List<Milestone> GetMilestonesByTeamID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var teamMilestone = dbContext.Milestones.Where(x => x.teamID == id).ToList();

                return teamMilestone;
            }
        }

        public Milestone GetMilestoneByID(int? id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                Milestone milestone = dbContext.Milestones.First(x => x.milestoneID == id);

                return milestone;
            }
        }

    }
}