using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class WhiteboardSessionBLL
    {
        public void CreateNewSession(WhiteboardSession session){
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.WhiteboardSessions.Add(session);
                dbContext.SaveChanges();
            }
        }

        public bool CheckIfCanAccessWhiteboardSession(WhiteboardSession session, Student student)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _whiteboardSession = dbContext.WhiteboardSessions.Find(session.sessionID);
                var _student = dbContext.Students.Find(student.UserId);


                return _student.TeamMembers
                    .Any(teamMember => teamMember.ProjectTeam.teamID.Equals(_whiteboardSession.teamID));

            }
        }

        public void SaveStokePathToWhiteboardSession(WhiteboardSession session, string strokePath)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.WhiteboardSessions.Find(session.sessionID).strokesJsonPath = strokePath;
                dbContext.SaveChanges();
            }
       }


        public WhiteboardSession GetWhiteboardSessionFromSessonId(Guid guid)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.WhiteboardSessions.Find(guid);
            }
        }

        public List<WhiteboardSession> GetWhiteboardSessionsByTeamID (int teamID){
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities()){
                    return dbContext.WhiteboardSessions.Where(x => x.teamID == teamID)
                        .ToList();
            }
        }
    }
}