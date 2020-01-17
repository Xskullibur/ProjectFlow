﻿using System;
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
                    SingleOrDefault(team => team.TeamMembers.Any(tm => tm.Student.studentID.Equals(student.studentID)));
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
                return dbContext.ProjectTeams.Find(TeamID);
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
            }
        }
    }
}