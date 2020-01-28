using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class RoomBLL
    {
        /// <summary>
        /// Find Room by room id
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public Room GetRoomByRoomID(int roomID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Rooms.Find(roomID);
            }
        }
        public void CreateRoom(Room room)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Returns a list of Room which is created by a ProjectTeam
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <returns></returns>
        public List<Room> GetListOfRoomsBelongingToAProjectTeam(ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Rooms
                    .Include("Student.aspnet_Users")//For displaying student name
                    .Where(room => room.teamID == projectTeam.teamID)
                    .OrderByDescending(room => room.creationDate).ToList();
            }
        }

        /// <summary>
        /// Returns a list of Room which is created by a ProjectTeam's creator
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public List<Room> GetListOfRoomsBelongingToAProjectTeamAndCreator(ProjectTeam projectTeam, aspnet_Users creator)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Rooms
                    .Include("Student.aspnet_Users")//For displaying student name
                    .Where(room => room.teamID == projectTeam.teamID && room.createdBy == creator.UserId)
                    .OrderByDescending(room => room.creationDate).ToList();
            }
        }

        /// <summary>
        /// Returns a list of Room which is created by a ProjectTeam's creator
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public List<Room> GetListOfRoomsBelongingToAProjectTeamAndListOfStudent(ProjectTeam projectTeam, List<aspnet_Users> filters)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Rooms
                    .Include("Student.aspnet_Users")//For displaying student name
                    .Where(room => room.teamID == projectTeam.teamID && filters.Contains(room.Student.aspnet_Users))
                    .OrderByDescending(room => room.creationDate).ToList();
            }
        }
    }
}