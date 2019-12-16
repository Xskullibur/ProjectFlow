using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class MilestoneBLL
    {
        public List<object> GetMilestoneByTeamID(int id)
        {
            MilestoneDAO milestoneDAO = new MilestoneDAO();
            var milestoneList = milestoneDAO.GetMilestonesByTeamID(id).ToList();

            return milestoneList;
        }
    }
}