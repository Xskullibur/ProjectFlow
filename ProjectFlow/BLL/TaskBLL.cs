using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class TaskBLL  : Task
    {

        public List<object> GetTasksByTeamId(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            var tasks = taskDAO.getTasksByTeamId(id).ToList();

            return tasks;
        }

    }
}