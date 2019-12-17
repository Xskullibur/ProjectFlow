using ProjectFlow.DAO;
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
            TeamMemberDAO memberDAO = new TeamMemberDAO();
            var memberList = memberDAO.GetTeamMembersByTeamID(id);

            return memberList;
        }
    }
}