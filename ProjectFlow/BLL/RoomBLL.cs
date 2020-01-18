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

    }
}