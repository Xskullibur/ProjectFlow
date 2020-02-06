using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class TeamMemberBLL
    {
        public Dictionary<int, string> GetTeamMembersByTeamID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var members = dbContext.TeamMembers.Include("Students")
                    .Where(x => x.teamID == id)
                    .Select(y => new
                    {
                        ID = y.memberID,
                        Name = y.Student.firstName + " " + y.Student.lastName
                    }).ToDictionary(key => key.ID, value => value.Name);

                return members;
            }
        }

        public List<string> GetUsersByTeamID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var users = dbContext.TeamMembers.Include("Students")
                    .Where(x => x.teamID == id)
                    .Select(y => y.Student.aspnet_Users.UserName)
                    .ToList();

                return users;
            }
        }
     
        public void DeleteMember(int MemberID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Find(MemberID);
                if(member != null)
                {
                    member.dropped = true;
                    dbContext.SaveChanges();
                }              
            }
        }

        public Dictionary<string, string> GetAllStudent()
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.Select(x => new
                {
                    StudentID = x.studentID,
                    Name = x.firstName + " " + x.lastName
                }).ToDictionary(key => key.StudentID, value => value.Name);

                return student;
            }
        }

        public List<TeamMember> SearchMember(int TeamID, string search)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.TeamMembers.Include(x => x.Student).Where(x => x.Student.firstName.ToLower().Contains(search.ToLower())).Include(x => x.Role).Where(x => x.teamID == TeamID).ToList();
            }
        }

        public bool CheckLeaderExist(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.TeamMembers.Any(x => x.roleID == 1 && x.teamID == TeamID);
            }
        }

        public int GetMemIdbyUID(Guid Uid)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var MID = dbContext.TeamMembers
                    .First(x => x.UserId == Uid)
                    .memberID;
                    

                //int MID = int.Parse(student);

                return MID;
            }
        }

        public void ToLeader(Guid userID, int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Single(x => x.UserId == userID && x.teamID == teamID);
                if (member != null)
                {
                    member.roleID = 1;
                    dbContext.SaveChanges();
                }
            }
        }

        public void ToMember(Guid userID, int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Single(x => x.UserId == userID && x.teamID == teamID);
                if (member != null)
                {
                    member.roleID = 2;
                    dbContext.SaveChanges();
                }
            }
        }

        public List<Score> GetGradeByProjectID(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Scores.Include(x => x.Student).Where(x => x.projectID.Equals(ProjectID)).ToList();
            }
        }

        public List<Score> GetGradeByTeamID(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Scores.Include(x => x.Student).Where(x => x.teamID == TeamID).ToList();
            }
        }
        
        public void UpdateScore(int scoreID, float Proposal, float Report, float ReviewOne, float ReviewTwo, float Presentation, float Test, float SDL, float Participation)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var score = dbContext.Scores.Find(scoreID);
                if(score != null)
                {
                    score.proposal = Proposal;
                    score.report = Report;
                    score.reviewOne = ReviewOne;
                    score.reviewTwo = ReviewTwo;
                    score.presentation = Presentation;
                    score.test = Test;
                    score.sdl = SDL;
                    score.participation = Participation;
                    dbContext.SaveChanges();
                }
            }
        }

        public void UpdateGroupScore(int TeamID, double Proposal, double Report, double Presentation)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                List<Score> scoreList = dbContext.Scores.Where(x => x.teamID == TeamID).ToList();
                foreach(Score s in scoreList)
                {
                    s.presentationG = Presentation;
                    s.reportG = Report;
                    s.proposalG = Proposal;
                }
                dbContext.SaveChanges();
            }
        }

        public void RemoveMember(int MemberID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Find(MemberID);
                if(member != null)
                {
                    member.dropped = true;
                    member.roleID = 2;
                    dbContext.SaveChanges();
                }
            }
        }

        public ProjectTeam FindTeam(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Find(TeamID);              
            }
        }

        public void UpdateTeamName(int TeamID, string name)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var team = dbContext.ProjectTeams.Find(TeamID);
                if(team != null)
                {
                    team.teamName = name;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}