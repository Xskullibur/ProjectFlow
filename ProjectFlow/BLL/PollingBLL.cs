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
        public bool Check(int iID, int vID)
        {
            PollingDAO pollingDAO = new PollingDAO();
            var list = pollingDAO.GetPollByID(vID).ToList();
            var exist = list.Contains(iID);
            return exist;
        }
        public List<int> Getcheck(int vID) //this is just for testing and needs to be removed
        {
            PollingDAO pollingDAO = new PollingDAO();
            var list = pollingDAO.GetPollByID(vID).ToList();
            return list;
        }
    }
}