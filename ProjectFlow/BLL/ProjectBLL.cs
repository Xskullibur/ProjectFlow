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
          
        public List<Project> GetProjectTutor(int tutorID)
        {
            return projectDAO.GetProjectTutor(tutorID);
        }

        public List<string> ValidateProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            ProjectID = RemoveWhiteSpace(ProjectID);
            Name = Name.Trim();
            Desc = Desc.Trim();

            List<string> errorList = new List<string> { };
          
            if (ProjectID.Equals(""))
            {
                errorList.Add("ID is empty<br>");
            }

            if (Name.Equals(""))
            {
                errorList.Add("Name is empty<br>");
            }

            if (Desc.Equals(""))
            {
                errorList.Add("Description is empty<br>");
            }

            if (TutorID == null)
            {
                errorList.Add("TutorID is empty<br>");
            }

            if(ProjectID.Length > 6)
            {
                errorList.Add("ID cannot be longer than 6<br>");
            }

            if (Name.Length > 255)
            {
                errorList.Add("Name cannot be longer than 255<br>");
            }

            if (Desc.Length > 255)
            {
                errorList.Add("Description cannot be longer than 6<br>");
            }

            if (projectDAO.CheckUniqueProjectID(ProjectID) == false)
            {
                errorList.Add("Project ID is taken");
            }

            if (errorList.Count == 0)
            {
                projectDAO.InsertProject(ProjectID, Name, Desc, TutorID);
            }

            return errorList;
        }

        public List<string> ValidateUpdate(string ProjectID, string Name, string Desc, int TutorID)
        {
            ProjectID = RemoveWhiteSpace(ProjectID);
            Name = Name.Trim();
            Desc = Desc.Trim();
            
            List<string> errorList = new List<string> { };

            if (ProjectID.Equals(""))
            {
                errorList.Add("ID is empty<br>");
            }

            if (Name.Equals(""))
            {
                errorList.Add("Name is empty<br>");
            }

            if (Desc.Equals(""))
            {
                errorList.Add("Description is empty<br>");
            }
          
            if (ProjectID.Length > 6)
            {
                errorList.Add("ID cannot be longer than 6<br>");
            }

            if (Name.Length > 255)
            {
                errorList.Add("Name cannot be longer than 255<br>");
            }

            if (Desc.Length > 255)
            {
                errorList.Add("Description cannot be longer than 6<br>");
            }

            if(projectDAO.CheckOwnership(ProjectID, TutorID) == false)
            {
                errorList.Add("Project is does not belong to you");
            }
           
            if (errorList.Count == 0)
            {
                projectDAO.UpdateProject(ProjectID, Name, Desc);
            }

            return errorList;
        }
    }
}