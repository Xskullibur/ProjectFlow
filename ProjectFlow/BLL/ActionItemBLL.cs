using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class ActionItemBLL
    {
        /// <summary>
        /// Insert new Action Item into database
        /// </summary>
        /// <param name="actionItem"></param>
        public void AddActionItem(RoomActionItem actionItem)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.RoomActionItems.Add(actionItem);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Remove an existing RoomActionItem from database
        /// </summary>
        /// <param name="roomActionItemID">roo</param>
        public void RemoveActionItem(int roomActionItemID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var actionItem = dbContext.RoomActionItems.Find(roomActionItemID);
                dbContext.RoomActionItems.Remove(actionItem);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Return a list of RoomActionItem from a Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<RoomActionItem> GetListOfRoomActionItemsFromRoom(Room room)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _room = dbContext.Rooms.Find(room.roomID);
                return _room.RoomActionItems.ToList();
            }

        }


    }
}