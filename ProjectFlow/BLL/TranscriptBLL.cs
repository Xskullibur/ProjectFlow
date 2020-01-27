using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class TranscriptBLL
    {
        /// <summary>
        /// Returns a list of Transcript of the Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<Transcript> GetListOfTranscripts(Room room)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _room = dbContext.Rooms.Include(x => x.Transcripts.Select(y => y.aspnet_Users))
                    .First(x => room.roomID == x.roomID);
                return _room.Transcripts.ToList();
            }
        }

    }
}