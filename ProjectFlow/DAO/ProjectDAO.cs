using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class ProjectDAO
    {
        public void InsertProject(string ProjectID, string Name, string Desc, Guid TutorID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                var project = new Project() {
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
                if(project != null)
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
                var projectTeam = new ProjectTeam() {
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
                if(projectTeam != null)
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

                var member = new TeamMember() {
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
                if(member != null)
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
                if(dbContext.Projects.Any(x => x.projectID == ProjectID))
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