using System;
using System.Collections.Generic;
using System.Linq;
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
        public Student FindStudentById(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.Find(id);
            }
        }

        /// <summary>
        /// Find the Student using email
        /// </summary>
        /// <param name="email">email of the Student</param>
        /// <returns>Instance of the found Student object, null if email does not exist in the Student table</returns>
        public Student FindStudentByEmail(string email)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Students.FirstOrDefault(student => student.email.Equals(email));
            }
        }


    }
}