using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ProjectFlow.Login.Models
{
    public class ProjectFlowUser : IPrincipal
    {
        public Student Student { get; set; }

        public IIdentity Identity { get; private set; }

        public ProjectFlowUser(Student student)
        {
            this.Student = student;
            this.Identity = new GenericIdentity(student.username);
        }

        /// <summary>
        /// Check role is a sutdent or teacher
        /// <see >
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            //Now only check for student since teacher is not implemented
            switch (role)
            {
                case "Student":
                    if (Student != null) return true;
                    else return false;
                default:
                    return false;
            }
        }

        public bool IsInProjectTeam(ProjectTeam projectTeam)
        {
            return projectTeam.ContainsStudent(Student);
        }


    }

   
}
namespace ProjectFlow
{
    public partial class ProjectTeam
    {
        public bool ContainsTeamMember(TeamMember teamMember)
        {
            return this.TeamMembers.Contains(teamMember);
        }

        public bool ContainsStudent(Student student)
        {
            return this.TeamMembers.Select(tm => tm.Student).Contains(student);
        }

    }
}