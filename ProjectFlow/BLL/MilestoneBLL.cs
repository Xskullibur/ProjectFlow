using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class MilestoneBLL
    {
     
        public List<string> ValidateCreateMilestone(string Name, string ProjectID, int TeamID, string StartDate, string EndDate)
        {
            List<string> errorList = new List<string> { };
            DateTime validateValue;

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

            if (StartDate.Equals(""))
            {
                errorList.Add("Start date cannot be empty<br>");
            }

            if (EndDate.Equals(""))
            {
                errorList.Add("End date cannot be empty<br>");
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
                CreateMilestone(Name, ProjectID, TeamID, Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate));
            }

            return errorList;
        }

        public List<string> ValidateUpdateMilestone(int MilestoneID, string Name, string StartDate, string EndDate)
        {
            List<string> errorList = new List<string> { };
            DateTime validateValue;

            if (Name.Equals(""))
            {
                errorList.Add("Name cannot be empty<br>");
            }

            if (Name.Length > 255)
            {
                errorList.Add("Name cannot be more than 255 character<br>");
            }
            
            if (StartDate.Equals(""))
            {
                errorList.Add("Start date cannot be empty<br>");
            }

            if (EndDate.Equals(""))
            {
                errorList.Add("End date cannot be empty<br>");
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
                UpdateMilestone(MilestoneID, Name, Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate));
            }

            return errorList;
        }

        public List<Milestone> GetMilestonesByTeamID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var teamMilestone = dbContext.Milestones.Where(x => x.teamID == id && x.dropped == false)
                    .OrderBy(x => x.endDate)
                    .ToList();

                return teamMilestone;
            }
        }

        public List<Milestone> SearchMilestone(int id, string name)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var teamMilestone = dbContext.Milestones.Where(x => x.teamID == id && x.dropped == false && x.milestoneName.ToLower().Contains(name.ToLower())).ToList();

                return teamMilestone;
            }
        }

        public Milestone GetMilestoneByID(int? id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                Milestone milestone = dbContext.Milestones.First(x => x.milestoneID == id && x.dropped == false);

                return milestone;
            }
        }

        public void CreateMilestone(string Name, string ProjectID, int TeamID, DateTime StartDate, DateTime EndDate)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var milestone = new Milestone
                {
                    milestoneName = Name,
                    startDate = StartDate,
                    endDate = EndDate,
                    projectID = ProjectID,
                    teamID = TeamID
                };
                dbContext.Milestones.Add(milestone);
                dbContext.SaveChanges();
            }
        }

        public void CreateDefaultTask(int TeamID, int MilestoneID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var task = new Task {
                    taskName = "Choosing Theme for Project",
                    taskDescription = "ask yourn team members to join this group and start to brainstorm ideas",
                    startDate = DateTime.Now.Date,
                    endDate = DateTime.Now.AddDays(7).Date,
                    teamID = TeamID,
                    milestoneID = MilestoneID,
                    priorityID = 2,
                    statusID = 1,
                    dropped = false
                };
                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();
            }
        }


        public bool CheckMilestoneTableIsNotEmpty(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Milestones.Any(x => x.teamID == TeamID && x.dropped == false);
            }
        }

        public void CreateTemplateMilestone(string ProjectID, int TeamID)
        {
            CreateMilestone("Proposal Presentation & Report", ProjectID, TeamID, DateTime.Now.Date, DateTime.Now.Date.AddDays(14));
            CreateMilestone("Technical Review 1", ProjectID, TeamID, DateTime.Now.Date.AddDays(14), DateTime.Now.Date.AddDays(6*7));
            CreateMilestone("Technical Review 2", ProjectID, TeamID, DateTime.Now.Date.AddDays(6*7), DateTime.Now.Date.AddDays(8*7));
            CreateMilestone("Project Completion", ProjectID, TeamID, DateTime.Now.Date.AddDays(8*7), DateTime.Now.Date.AddDays(70));
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var milestone = dbContext.Milestones.First(x => x.milestoneName.Equals("Proposal Presentation & Report") && x.teamID == TeamID && x.projectID.Equals(ProjectID) && x.dropped == false);
                CreateDefaultTask(TeamID, milestone.milestoneID);
            }           
        }



        public void UpdateMilestone(int MilestoneID, string Name, DateTime StartDate, DateTime EndDate)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var milestone = dbContext.Milestones.Single(x => x.milestoneID == MilestoneID);

                if (milestone != null)
                {
                    milestone.milestoneName = Name;
                    milestone.startDate = StartDate;
                    milestone.endDate = EndDate;
                    dbContext.SaveChanges();
                }
            }
        }

        public void DeleteMilestone(int MilestoneID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var milestone = dbContext.Milestones.Find(MilestoneID);
                if (milestone != null)
                {
                    milestone.dropped = true;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}