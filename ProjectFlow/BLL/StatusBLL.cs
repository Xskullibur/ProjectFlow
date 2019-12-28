using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class StatusBLL
    {

        public static string PENDING = "Pending";
        public static string WORK_IN_PROGRESS = "Work-in-progress";
        public static string VERIFICATON = "Verification";
        public static string COMPLETED = "Completed";

        public Dictionary<int, string> Get()
        {
            StatusDAO statusDAO = new StatusDAO();
            return statusDAO.Get();
        }

        public string GetStatusByID(int id)
        {
            StatusDAO statusDAO = new StatusDAO();
            return statusDAO.GetStatusByID(id);
        }

    }
}