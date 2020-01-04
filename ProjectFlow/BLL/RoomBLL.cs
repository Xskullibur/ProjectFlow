using ProjectFlow.DAO;
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
            RoomDAO dao = new RoomDAO();
            dao.CreateRoom(room);
        }

    }
}