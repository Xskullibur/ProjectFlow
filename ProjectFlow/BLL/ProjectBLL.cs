using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectFlow.BLL
{
    public class ProjectBLL
    {        
        public string RemoveWhiteSpace(string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
                 
        public List<ProjectTeam> GetProjectTeam(string ProjectID)
        {
            return GetTeam(ProjectID);
        }

        public List<string> InsertProjectTeam(string TeamName, string Desc, string ProjectID)
        {
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
                InsertTeam(TeamName, Desc, ProjectID);
            }

            return errorList;          
        }

        public List<string> ValidateProject(string ProjectID, string Name, string Desc, Guid TutorID)
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

            if (CheckUniqueProjectID(ProjectID) == false)
            {
                errorList.Add("Project ID is taken");
            }

            if (errorList.Count == 0)
            {
                InsertProject(ProjectID, Name, Desc, TutorID);
            }

            return errorList;
        }

        public List<string> ValidateUpdate(string ProjectID, string Name, string Desc, Guid TutorID)
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

            if(CheckOwnership(ProjectID, TutorID) == false)
            {
                errorList.Add("Project is does not belong to you");
            }
           
            if (errorList.Count == 0)
            {
                UpdateProject(ProjectID, Name, Desc);
            }

            return errorList;
        }

        public List<string> UpdateProjectTeam(int TeamID, string TeamName, string Desc)
        {
            List<string> errorList = new List<string> { };
            
            if (TeamName.Equals(""))
            {
                errorList.Add("Name is empty<br>");
            }

            if (TeamName.Length > 255)
            {
                errorList.Add("Name cannot be more than 255 empty<br>");
            }

            if (Desc.Length > 255)
            {
                errorList.Add("Description cannot be more than 255 empty<br>");
            }

            if (errorList.Count == 0)
            {
                UpdateTeam(TeamID, TeamName, Desc);
            }

            return errorList;
        }
      
        public List<TeamMember> GetTeamMember(int TeamID)
        {
            return GetMember(TeamID);
        }

        public List<string> ValidateInsertMember(string StudentID, int TeamID, int RoleID)
        {
            List<string> errorList = new List<string> { };

            if (StudentID.Equals(""))
            {
                errorList.Add("student ID is empty<br>");
            }

            if(CheckStudentExist(StudentID) == false)
            {
                errorList.Add("student ID does not exist<br>");
            }

            if(CheckStudentAlreadyExist(StudentID, TeamID))
            {
                errorList.Add("student already is a member<br>");
            }

            if(TeamID.ToString().Length > 4)
            {
                errorList.Add("team ID cannot contain more than 4 digit<br>");
            }

            if (RoleID.ToString().Length > 4)
            {
                errorList.Add("role ID cannot contain more than 4 digit<br>");
            }

            if (TeamID < 0)
            {
                errorList.Add("team ID cannot be negative<br>");
            }

            if (RoleID < 0)
            {
                errorList.Add("role ID cannot be negative<br>");
            }

            if(errorList.Count == 0)
            {
                InsertMember(StudentID, TeamID, RoleID);
            }

            return errorList;
        }

        public List<string> ValidateUpdateMember(int MemberID, int RoleID)
        {
            List<string> errorList = new List<string> { };

            if (RoleID.ToString().Length > 4)
            {
                errorList.Add("role ID cannot contain more than 4 digit<br>");
            }

            if (RoleID < 0)
            {
                errorList.Add("role ID cannot be negative<br>");
            }

            if (RoleID == null)
            {
                errorList.Add("role ID cannot be null<br>");
            }

            if (errorList.Count == 0)
            {
                UpdateMember(MemberID, RoleID);
            }
            return errorList;
        }

        public void InsertProject(string ProjectID, string Name, string Desc, Guid TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                var project = new Project()
                {
                    projectID = ProjectID,
                    projectName = Name,
                    projectDescription = Desc,
                    UserId = TutorID
                };
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }
        }

        public void UpdateProject(string ProjectID, string Name, string Desc)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var project = dbContext.Projects.Single(x => x.projectID.Equals(ProjectID));
                if (project != null)
                {
                    project.projectName = Name;
                    project.projectDescription = Desc;
                    dbContext.SaveChanges();
                }
            }
        }

        public List<Project> GetProjectTutor(Guid TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                Tutor tutor = dbContext.Tutors.First(x => x.UserId == TutorID);
                return tutor.Projects.ToList();
            }
        }

        public void InsertTeam(string TeamName, string Desc, string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var projectTeam = new ProjectTeam()
                {
                    teamName = TeamName,
                    teamDescription = Desc,
                    projectID = ProjectID
                };
                dbContext.ProjectTeams.Add(projectTeam);
                dbContext.SaveChanges();
            }
        }

        public void UpdateTeam(int TeamID, string TeamName, string Desc)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var projectTeam = dbContext.ProjectTeams.Single(x => x.teamID == TeamID);
                if (projectTeam != null)
                {
                    projectTeam.teamName = TeamName;
                    projectTeam.teamDescription = Desc;
                    dbContext.SaveChanges();
                }
            }
        }

        public List<ProjectTeam> GetTeam(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Where(x => x.projectID.Equals(ProjectID)).ToList();
            }
        }

        public List<TeamMember> GetMember(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.TeamMembers.Where(x => x.teamID == TeamID).ToList();
            }
        }

        public void InsertMember(string StudentID, int TeamID, int RoleID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.First(x => x.studentID.Equals(StudentID));

                var member = new TeamMember()
                {
                    UserId = student.UserId,
                    teamID = TeamID,
                    roleID = RoleID
                };
                dbContext.TeamMembers.Add(member);
                dbContext.SaveChanges();
            }
        }

        public void UpdateMember(int MemberID, int RoleID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var member = dbContext.TeamMembers.Single(x => x.memberID == MemberID);
                if (member != null)
                {
                    member.roleID = RoleID;
                    dbContext.SaveChanges();
                }
            }
        }

        public bool CheckUniqueProjectID(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.Projects.Any(x => x.projectID == ProjectID))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool CheckOwnership(string ProjectID, Guid TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.Projects.Any(x => x.projectID == ProjectID && x.UserId == TutorID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckProjectExist(Guid TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.Projects.Any(x => x.UserId == TutorID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckProjectTeamExist(string ProjectID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.ProjectTeams.Any(x => x.projectID == ProjectID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckProjectMemberExist(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                if (dbContext.TeamMembers.Any(x => x.teamID == TeamID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckStudentExist(string StudentID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.First(x => x.studentID.Equals(StudentID));
                if (dbContext.Students.Any(x => x.UserId.Equals(student.UserId)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckStudentAlreadyExist(string StudentID, int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.First(x => x.studentID.Equals(StudentID));
                if (dbContext.TeamMembers.Any(x => x.UserId.Equals(student.UserId) && x.teamID == TeamID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }     
    }  
}