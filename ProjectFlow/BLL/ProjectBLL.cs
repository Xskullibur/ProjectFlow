using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ProjectFlow.DAO;

namespace ProjectFlow.BLL
{
    public class ProjectBLL
    {
        ProjectDAO projectDAO = new ProjectDAO();

        public string RemoveWhiteSpace(string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        public void CreateProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            projectDAO.InsertProject(ProjectID, Name, Desc, TutorID);
        }

        public List<Project> GetProjectTutor(int tutorID)
        {
            return projectDAO.GetProjectTutor(tutorID);
        }

        public bool ValidateProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            ProjectID = ProjectID.Trim();
            Name = Name.Trim();
            Desc = Desc.Trim();           

            bool noError = true;

            if (ProjectID.Equals("") || Name.Equals("") || Desc.Equals("") || TutorID == null) { }
            {
                noError = false; 
            }




            return noError;
        }
    }
}