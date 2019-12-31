using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFlow.DAO;

namespace ProjectFlow.BLL
{
    public class ProjectBLL
    {
        ProjectDAO projectDAO = new ProjectDAO();

        public void CreateProject(string ProjectID, string Name, string Desc, Guid tutorUserId)
        {
            projectDAO.InsertProject(ProjectID, Name, Desc, tutorUserId);
        }

        public List<Project> GetProjectTutor(Guid tutorUserId)
        {
            return projectDAO.GetProjectTutor(tutorUserId);
        }
    }
}