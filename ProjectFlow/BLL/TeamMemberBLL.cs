using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}