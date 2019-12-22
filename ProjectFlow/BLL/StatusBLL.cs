using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class StatusBLL
    {
        public Dictionary<int, string> Get()
        {
            StatusDAO statusDAO = new StatusDAO();
            return statusDAO.Get();
        }
    }
}