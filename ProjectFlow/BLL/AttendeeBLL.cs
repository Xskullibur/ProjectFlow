using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class AttendeeBLL
    {
        /// <summary>
        /// Create a new Attendee inside Room
        /// </summary>
        /// <param name="room"></param>
        /// <param name="attendee"></param>
        public void CreateAttendeeInRoom(Room room, Attendee attendee)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _room = dbContext.Rooms.Find(room.roomID);
                _room.Attendees.Add(attendee);
                dbContext.SaveChanges();
            }
        }

    }
}