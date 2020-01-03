using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace ProjectFlow.DAO
{
    public class StudentDAO
    {
        //not for actual use
        public List<Student> GetStudents()
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.ToList();
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

        public Project GetProjectByProjectId(string projectId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Projects.Find(projectId);
            }
        }

        public void Call()
        {
            Project project = GetProjectByProjectId("0001");
            CreateNewMileStone(new Milestone
            {
                endDate = DateTime.Now,
                milestoneName = "NEW Milestone",
                Project = project
            });
        }

        public void CreateNewMileStone(Milestone milestone)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.Milestones.Add(milestone);
                dbContext.SaveChanges();
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

    }
}