using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class PollingBLL
    {
        public bool Add(Polling vote)
        {
            PollingDAO PollingDAO = new PollingDAO();
            return PollingDAO.Add(vote);
        }
    }
}