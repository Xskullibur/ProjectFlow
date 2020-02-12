using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class ProjectTeamBLL
    {

        /// <summary>
        /// Get the project from project team
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <returns></returns>
        public Project GetProject(ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Find(projectTeam.teamID).Project;
            }
        }

        /// <summary>
        /// Get project team from student and project
        /// if the student is doing a particular project, the student will belong to a project team
        /// </summary>
        /// <param name="student"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public ProjectTeam GetProjectTeamByStudentAndProject(Student student, Project project)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.Projects.Find(project.projectID).ProjectTeams.
                    SingleOrDefault(team => team.TeamMembers.Any(tm => tm.Student.studentID.Equals(student.studentID) && tm.dropped == false));
            }
        }

        /// <summary>
        /// Get a list of project team members inside a project team
        /// </summary>
        /// <param name="projectTeam"></param>
        /// <returns></returns>
        public IEnumerable<TeamMember> GetTeamMembersFromProjectTeam(ProjectTeam projectTeam)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Include(team => team.TeamMembers.Select(tm => tm.Student))
                    .SingleOrDefault(team => team.projectID == projectTeam.projectID).TeamMembers.ToList();

            }
        }

        public ProjectTeam GetProjectTeamByTeamID(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Include(x => x.Project).FirstOrDefault(x => x.teamID == TeamID);
            }
        }

        public List<ProjectTeam> SearchGroup(string ProjectID, int Group)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Where(x => x.projectID.Equals(ProjectID) && x.dropped == false && x.group == Group).ToList();
            }
        }

        public List<ProjectTeam> SearchDeletedTeam(string ProjectID, string search)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.ProjectTeams.Where(x => x.projectID.Equals(ProjectID) && x.dropped == true && x.teamName.ToLower().Contains(search.ToLower())).ToList();
            }
        }

        public void DeleteTeam(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var projectTeam = dbContext.ProjectTeams.Single(x => x.teamID == TeamID);
                if (projectTeam != null)
                {
                    projectTeam.dropped = true;
                    dbContext.SaveChanges();
                }
                var milestone = dbContext.Milestones.Where(x => x.teamID == TeamID);
                if(milestone != null)
                {
                    foreach(Milestone mile in milestone)
                    {
                        mile.dropped = true;
                    }
                    dbContext.SaveChanges();
                }
                var member = dbContext.TeamMembers.Where(x => x.teamID == TeamID);
                if (member != null)
                {
                    foreach (TeamMember mem in member)
                    {
                        mem.dropped = true;
                    }
                    dbContext.SaveChanges();
                }
            }
        }

        public void RestoreTeam(int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var projectTeam = dbContext.ProjectTeams.Single(x => x.teamID == TeamID);
                if (projectTeam != null)
                {
                    projectTeam.dropped = false;
                    dbContext.SaveChanges();
                }
                var milestone = dbContext.Milestones.Where(x => x.teamID == TeamID);
                if (milestone != null)
                {
                    foreach (Milestone mile in milestone)
                    {
                        mile.dropped = false;
                    }
                    dbContext.SaveChanges();
                }
            }
        }
        public void lockTeam(string ProjectID, bool status, int Group)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                var projectTeam = dbContext.ProjectTeams.Where(x => x.projectID.Equals(ProjectID) && x.group == Group && x.dropped == false).ToList();
                if (projectTeam != null)
                {
                    foreach(var item in projectTeam)
                    {
                        if (status)
                        {
                            item.open = false;
                        }
                        else
                        {
                            item.open = true;
                        }
                    }                  
                    dbContext.SaveChanges();
                }
            }
        }

        public void lockOneTeam(bool status, int TeamID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var team = dbContext.ProjectTeams.Find(TeamID);
                
                if (team != null)
                {
                    if (status)
                    {
                        team.open = false;
                    }
                    else
                    {
                        team.open = true;
                    }
                    dbContext.SaveChanges();
                }
            }
        }
    }
}