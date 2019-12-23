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

        public void CreateProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            projectDAO.InsertProject(ProjectID, Name, Desc, TutorID);
        }
    }
}