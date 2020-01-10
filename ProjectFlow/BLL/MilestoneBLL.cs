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

        public List<string> ValidateCreateMilestone(string Name, string ProjectID, int TeamID, DateTime StartDate, DateTime EndDate)
        {
            List<string> errorList = new List<string> { };
            DateTime validateValue;
            MilestoneDAO dao = new MilestoneDAO();

            if (Name.Equals(""))
            {
                errorList.Add("Name cannot be empty<br>");
            }

            if(Name.Length > 255)
            {
                errorList.Add("Name cannot be more than 255 character<br>");
            }

            if (ProjectID.Equals(""))
            {
                errorList.Add("Project ID cannot be empty<br>");
            }

            if (ProjectID.Length != 6)
            {
                errorList.Add("Project ID must be 6 character<br>");
            }

            if (TeamID.ToString().Equals(""))
            {
                errorList.Add("Team ID cannot be empty<br>");
            }

            if (TeamID.ToString().Length > 4)
            {
                errorList.Add("Team ID cannot be more than 4 character<br>");
            }

            if (!DateTime.TryParse(StartDate.ToString(), out validateValue))
            {
                errorList.Add("Start Date not valid<br>");
            }

            if (!DateTime.TryParse(EndDate.ToString(), out validateValue))
            {
                errorList.Add("End Date not valid<br>");
            }

            if(errorList.Count == 0)
            {
                dao.CreateMileStone(Name, ProjectID, TeamID, StartDate, EndDate);
            }

            return errorList;
        }
    }
}