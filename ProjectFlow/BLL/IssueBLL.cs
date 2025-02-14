﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class IssueBLL
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
                        Console.Error.WriteLine($"Error While Adding issue: {e.Message}");
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
        /// Gets all Issues By IssueID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// 
        public IEnumerable<object> GetAllIssuesByTeamId(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Include("Status")
                        .Where(x => x.Task.teamID == id)
                        .Select(y => new
                        {
                            ID = y.issueID,
                            TaskID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy = y.TeamMember.Student.aspnet_Users.UserName,
                            Active = y.active,
                            Status = y.Status.status1,
                            IsPublic = y.votePublic
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
        /// Gets active Issues By IssueID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// 
        public IEnumerable<object> GetActiveIssuesByTeamId(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Where(x => x.Task.teamID == id)
                        .Where(x => x.active != false)
                        .Select(y => new
                        {
                            ID = y.issueID,
                            TaskID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy = y.TeamMember.Student.aspnet_Users.UserName,
                            Active = y.active,
                            Status = y.Status.status1,
                            IsPublic = y.votePublic
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
        /// Gets droppped Issues By IssueID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// 
        public IEnumerable<object> GetDroppedIssueByTeamId(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Where(x => x.Task.teamID == id)
                        .Where(x => x.active == false)
                        .Select(y => new
                        {
                            ID = y.issueID,
                            TaskID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy = y.TeamMember.Student.aspnet_Users.UserName,
                            Active = y.active,
                            Status = y.Status.status1,
                            IsPublic = y.votePublic
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
        /// Gets Issues By ID
        /// </summary>
        /// <param name="id"></param>
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


        /// <summary>
        /// Drop Issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>Boolean</returns>
        public bool Drop(int id)
        {
            Issue issue = GetIssueByID(id);

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

        /// <summary>
        /// check if Issue is public
        /// </summary>
        /// <param name="iTD"></param>
        /// <returns>Boolean</returns>
        public bool isPublic(int iID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMembers.Student")
                        .Include("Task")
                        .Where(x => x.issueID == iID)
                        .Select(y => y.votePublic).ToString();

                    if (list == "true")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return false;
                }


            }
        }

        /// <summary>
        /// updates Issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>Boolean</returns>
        public bool Update(Issue issue)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    // Update 
                    dbContext.Entry(issue).State = System.Data.Entity.EntityState.Modified;

                    // Save Changes
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating Task: {e.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Restore Issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public bool Restore(Issue issue)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    issue.active = true;
                    dbContext.Entry(issue).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Restoring Issue: {e.Message}");
                    return false;
                }
            }
        }

    }
}