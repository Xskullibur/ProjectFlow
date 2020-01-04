using ProjectFlow.DAO;
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
            MilestoneDAO milestoneDAO = new MilestoneDAO();
            var milestoneList = milestoneDAO.GetMilestonesByTeamID(id);

            return milestoneList;
        }

        public Milestone GetMilestoneByID(int? id)
        {
            MilestoneDAO milestoneDAO = new MilestoneDAO();
            var milestone = milestoneDAO.GetMilestoneByID(id);

            return milestone;
        }
    }
}