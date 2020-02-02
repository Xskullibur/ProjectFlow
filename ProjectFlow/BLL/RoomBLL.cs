using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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
        /// <summary>
        /// Create a new Room
        /// </summary>
        /// <param name="room"></param>
        public void CreateRoom(Room room)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Get list of Attendees from a Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<Attendee> GetListOfAttendeesFromRoom(Room room)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Rooms.Find(room.roomID).Attendees.ToList();
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
                var rooms = dbContext.Rooms
                    .Include(room => room.Student.aspnet_Users)//For displaying student name
                    .Where(room => room.teamID == projectTeam.teamID)
                    .OrderByDescending(room => room.creationDate).ToList();
                return rooms
                    .Where(room => filters
                    .Select(x => x.UserId)
                    .Contains(room.Student.aspnet_Users.UserId))
                    .ToList();
            }
        }

        /// <summary>
        /// Set Room access token to new access token
        /// </summary>
        /// <param name="room"></param>
        /// <param name="accessToken"></param>
        public void UpdateRoomAccessToken(Room room, byte[] accessToken)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _room = dbContext.Rooms.Find(room.roomID);
                _room.accessToken = accessToken;
                dbContext.SaveChanges();
            }
        }

    }
}