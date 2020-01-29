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

        public void DeleteMember(int MemberID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Find(MemberID);
                if(member != null)
                {
                    dbContext.TeamMembers.Remove(member);
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

        public void ToLeader(Guid userID, int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Single(x => x.UserId == userID && x.teamID == teamID);
                if(member != null)
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
    }
}