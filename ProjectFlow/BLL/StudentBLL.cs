using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class StudentBLL
    {

        public Student GetLeaderByTaskID(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            ProjectTeam team = taskDAO.GetProjectTeamByTaskID(id);

            StudentDAO studentDAO = new StudentDAO();
            Student leader = studentDAO.GetTeamLeaderByTeamID(team.teamID);

            return leader;
        }

        public List<Student> GetAllocationsByTaskID(int id)
        {
            StudentDAO studentDAO = new StudentDAO();
            List<Student> students = studentDAO.GetAllocationsByTaskID(id);

            return students;
        }

    }
}