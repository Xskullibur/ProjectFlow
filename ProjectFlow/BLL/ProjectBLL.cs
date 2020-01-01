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
        public string RemoveWhiteSpace(string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
          
        public List<Project> GetProjectTutor(int tutorID)
        {
            ProjectDAO projectDAO = new ProjectDAO();
            return projectDAO.GetProjectTutor(tutorID);
        }

        public List<ProjectTeam> GetProjectTeam(string ProjectID)
        {
            ProjectDAO projectDAO = new ProjectDAO();
            return projectDAO.GetTeam(ProjectID);
        }

        public List<string> InsertProjectTeam(string TeamName, string Desc, string ProjectID)
        {
            ProjectDAO projectDAO = new ProjectDAO();
            List<string> errorList = new List<string> { };

            if (TeamName.Equals(""))
            {
                errorList.Add("Name is empty<br>");
            }

            if(TeamName.Length > 255)
            {
                errorList.Add("Name cannot be more than 255 empty<br>");
            }

            if (Desc.Length > 255)
            {
                errorList.Add("Description cannot be more than 255 empty<br>");
            }

            if (errorList.Count == 0)
            {
                projectDAO.InsertTeam(TeamName, Desc, ProjectID);
            }

            return errorList;          
        }

        public List<string> ValidateProject(string ProjectID, string Name, string Desc, int TutorID)
        {
            ProjectDAO projectDAO = new ProjectDAO();
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
            ProjectDAO projectDAO = new ProjectDAO();
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

        public bool CheckProjectExist(int TutorID)
        {
            ProjectDAO dao = new ProjectDAO();
            return dao.CheckProjectExist(TutorID);
        }

        public bool CheckProjectTeamExist(string ProjectID)
        {
            ProjectDAO dao = new ProjectDAO();
            return dao.CheckProjectTeamExist(ProjectID);
        }
    }  
}