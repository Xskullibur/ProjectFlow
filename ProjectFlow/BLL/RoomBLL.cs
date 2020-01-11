using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class RoomBLL
    {

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