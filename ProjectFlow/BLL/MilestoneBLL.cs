using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class MilestoneBLL
    {
        public List<Milestone> GetMilestoneByTeamID(int id)
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

        public void CreateNewMileStone(Milestone milestone)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.Milestones.Add(milestone);
                dbContext.SaveChanges();
            }
        }

    }
}