using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class TaskBLL
    {
        public bool Add(Task task, List<TaskAllocation> taskAllocations)
        {
            TaskDAO taskDAO = new TaskDAO();
            return taskDAO.Add(task, taskAllocations);
        }

        public bool Update(Task updated_task, List<TaskAllocation> updated_allocations)
        {
            TaskDAO taskDAO = new TaskDAO();
            return taskDAO.Update(updated_task, updated_allocations);
        }

        public bool Delete(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            Task deleted_task = taskDAO.GetTaskByID(id);

            return taskDAO.Delete(deleted_task);
        }

        public bool Restore(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            Task restored_task = taskDAO.GetTaskByID(id);

            return taskDAO.Restore(restored_task);
        }


        public Task GetTaskById(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            return taskDAO.GetTaskByID(id);
        }

        public List<object> GetOngoingTasksByTeamId(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            var tasks = taskDAO.GetOngoingTasksByTeamID(id).ToList();

            return tasks;
        }

        public List<object> GetDroppedTasksByTeamId(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            var tasks = taskDAO.GetDroppedTasksByTeamID(id).ToList();

            return tasks;
        }
    }
}