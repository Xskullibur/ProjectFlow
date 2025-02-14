﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace ProjectFlow.BLL
{
    public class StudentBLL
    {

        //not for actual use
        public List<Student> GetStudents()
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.ToList();
            }
        }

        public Student GetStudentByStudentID(Guid userId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.FirstOrDefault(student => student.UserId.Equals(userId));
            }
        }

        public List<Student> GetStudentsByName(string surname)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.Where(x => x.firstName.Equals(surname)).ToList();
            }
        }

        public List<String> GetMilestonesByProjectName(string projectname)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                Project project = dbContext.Projects.First(x => x.projectName.Equals(projectname));
                return project.Milestones.Select(x => x.milestoneName).ToList();
            }
        }

        public void UpdateProjectNameByProjectID(string projectId, string newProjectName)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                Project project = dbContext.Projects.Find(projectId);
                project.projectName = newProjectName;
                dbContext.SaveChanges();
            }
        }

        public Student GetLeaderByTaskID(int id)
        {
            TaskBLL taskBLL = new TaskBLL();
            ProjectTeam team = taskBLL.GetProjectTeamByTaskID(id);

            Student leader = GetTeamLeaderByTeamID(team.teamID);

            return leader;
        }

        /// <summary>
        /// Returns the Student by the TeamMember
        /// </summary>
        /// <param name="teamMember"></param>
        /// <returns>the Student object which is referencing from TeamMember</returns>
        public Student GetStudentByTeamMember(TeamMember teamMember)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.TeamMembers.Include(x => x.Student.aspnet_Users)
                    .Select(x => x.Student)
                    .FirstOrDefault(x => x.UserId == teamMember.UserId);
            }
        }

        /// <summary>
        /// Find the Student using student id
        /// </summary>
        /// <param name="id">student id of the Studennt</param>
        /// <returns>Instance of the found Student object</returns>
        public Student FindStudentByAdminNo(string adminNo)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.FirstOrDefault(student => student.studentID.Equals(adminNo));
            }
        }

        /// <summary>
        /// Find the Student using email
        /// </summary>
        /// <param name="email">email of the Student</param>
        /// <returns>Instance of the found Student object, null if email does not exist in the database</returns>
        public Student FindStudentByEmail(string email)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Membership.FirstOrDefault(membership => membership.Email.Equals(email))?.aspnet_Users?.Student;
            }
        }

        /// <summary>
        /// Find the Student using username
        /// </summary>
        /// <param name="username">username of the Student</param>
        /// <returns>Instance of the found Student object, null if username does not exist in the database</returns>
        public Student FindStudentByUsername(string username)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.Include("aspnet_Membership").FirstOrDefault(user => user.UserName.Equals(username))?.Student;
            }
        }

        /// <summary>
        /// Get Team Leader By Team ID
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns>Student</returns>
        public Student GetTeamLeaderByTeamID(int teamID)
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    Student leader = dbContext.TeamMembers
                        .Where(x => x.teamID == teamID)
                        .Where(x => x.roleID == 1)
                        .Select(x => x.Student)
                        .Include(x => x.aspnet_Users.aspnet_Membership)
                        .First();

                    return leader;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Retrieving Student: {e.Message}");
                return null;
            }
        }


        /// <summary>
        /// Get Allocations by Task ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>Students</returns>
        public List<Student> GetAllocationsByTaskID(int taskID)
        {
            try
            {
                using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
                {
                    List<Student> allocationList = dbContext.TaskAllocations
                        .Where(x => x.taskID == taskID)
                        .Select(x => x.TeamMember.Student)
                        .Include(x => x.aspnet_Users.aspnet_Membership)
                        .ToList();

                    return allocationList;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Retrieving Students: {e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Check if the student have this project in his/her list of projects
        /// 
        /// List of projects: Each student will have a list of projects which includes the current, past or even
        /// the future projects which the student will have to complete.
        /// 
        /// This method check whether if the student have a particular project which the student may have to complete
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <param name="project"></param>
        /// <returns>only true if the student have this project in his/her project's list</returns>
        public bool ContainsProject(Student student, Project project)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.Find(student.UserId).TeamMembers.Select(tm => tm.ProjectTeam.Project.projectID).Contains(project.projectID);
            }
        }

        /// <summary>
        /// Check if the student belongs to a project team
        /// 
        /// Project team consist of many students doing for a particular project
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns>only true if the student is inside the project team</returns>
        public bool HaveProjectTeam(Student student, ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var teamMembers = dbContext.Students.Find(student.UserId).TeamMembers;
                return teamMembers.Select(tm => tm.ProjectTeam.projectID).Contains(projectTeam.projectID);
            }
        }

        public IEnumerable<ProjectTeam> GetStudentProject(Guid UserID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.Find(UserID);
                var projectteam = dbContext.ProjectTeams.Include(x => x.Project).Where(x => x.dropped == false);
                List<ProjectTeam> emptyList = new List<ProjectTeam> { };
                if(student != null)
                {
                    return student.TeamMembers.Where(x => x.dropped == false).Select(x => x.ProjectTeam).Where(x => x.dropped == false).ToList();
                }
                else
                {
                    return emptyList;
                }                              
            }
        }

        public IEnumerable<ProjectTeam> SearchStudentProject(Guid UserID, string search)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var student = dbContext.Students.Find(UserID);
                List<ProjectTeam> emptyList = new List<ProjectTeam> { };
                if (student != null)
                {
                    return student.TeamMembers.Where(x => x.dropped == false).Select(x => x.ProjectTeam).Where(x => x.dropped == false && x.teamName.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    return emptyList;
                }
            }
        }

        public IEnumerable<ProjectTeam> ShowAvailbleProject(string studentID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                List<ProjectTeam> teamList = new List<ProjectTeam> { };

                var project = dbContext.ProjectTeams.Where(x => x.dropped == false && x.open == true).Select(x => x.Project).ToList();
                var student = dbContext.Students.Single(x => x.studentID.Equals(studentID));
                var member = dbContext.TeamMembers.Where(x => x.dropped == true && x.UserId == student.UserId);

                foreach (Project p in project)
                {
                    var teams = dbContext.ProjectTeams.Where(x => x.projectID == p.projectID && x.group == student.group).ToList();

                    if (!ContainsProject(student, p))
                    {
                        teamList = dbContext.ProjectTeams.Where(x => x.dropped == false && x.open == true && x.projectID.Equals(p.projectID) && x.group == student.group).ToList();
                    }
                    else
                    {
                        bool status = true;
                        foreach (ProjectTeam t in teams)
                        {
                            if ((dbContext.TeamMembers.Count(x => x.dropped == false && x.UserId == student.UserId && x.teamID == t.teamID) > 0)){
                                status = false;
                            }
                        }

                        if(status == true)
                        {
                            teamList = dbContext.ProjectTeams.Where(x => x.dropped == false && x.open == true && x.projectID.Equals(p.projectID) && x.group == student.group).ToList();
                        }
                    }
                }
                return teamList;
            }
        }

        public TeamMember getMemberByAdmin(Guid userID, int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.TeamMembers.Single(x => x.UserId == userID && x.teamID == TeamID);
            }
        }

        public bool gotLeader(int teamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                List<TeamMember> memberList = dbContext.TeamMembers.Where(x => x.teamID == teamID && x.roleID == 1).ToList();
                foreach(TeamMember m in memberList)
                {
                    if (m.roleID == 1)
                    {
                        return true;
                    }                    
                }
                return false;
            }
        }
    }
}