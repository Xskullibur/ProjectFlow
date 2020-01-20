﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class IssueDAO
    {
        /// <summary>
        /// Add a new Issue into DB
        /// </summary>
        /// <param name="issue">Issue to be Added</param>
        /// <returns>Boolean</returns>
        public bool Add(Issue issue)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (issue != null)
                {
                    try
                    {

                        dbContext.Issues.Add(issue);   
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Error While Adding Task: {e.Message}");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Gets Issues By taskID
        /// </summary>
        /// <param name="tID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public Issue GetIssueByID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    Issue issue = dbContext.Issues.First(x => x.issueID == id);
                    return issue;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }

        public IEnumerable<object> GetIssueByTeamID(int tID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Where(x => x.Task.teamID == tID)
                        .Where(x => x.active != false)
                        .Select(y => new
                        {
                            ID = y.issueID,
                            TaskID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy = y.TeamMember.Student.aspnet_Users.UserName,

                        }).ToList();

                    return list;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }

        public IEnumerable<object> GetDroppedIssueByTeamID(int tID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Where(x => x.Task.teamID == tID)
                        .Where(x => x.active == false)
                        .Select(y => new
                        {
                            ID = y.issueID,
                            TaskID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy = y.TeamMember.Student.aspnet_Users.UserName,

                        }).ToList();

                    return list;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }

        /// <summary>
        /// Drop Issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>Boolean</returns>
        public bool drop(Issue issue)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    issue.active = false;
                    dbContext.Entry(issue).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Deleted Task: {e.Message}");
                    return false;
                }
            }
        }
    }
}