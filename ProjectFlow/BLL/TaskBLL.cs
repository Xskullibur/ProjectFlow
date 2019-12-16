using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class TaskBLL
    {
        public bool Add(Task task)
        {
            TaskDAO taskDAO = new TaskDAO();
            return taskDAO.Add(task);
        }

        public List<object> GetTasksByTeamId(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            var tasks = taskDAO.getTasksByTeamID(id).ToList();

            return tasks;
        }
    }
}