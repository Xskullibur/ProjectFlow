﻿using ProjectFlow.DAO;
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

        public Task GetTaskById(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            return taskDAO.GetTaskByID(id);
        }

        public List<object> GetTasksByTeamId(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            var tasks = taskDAO.GetTasksByTeamID(id).ToList();

            return tasks;
        }
    }
}