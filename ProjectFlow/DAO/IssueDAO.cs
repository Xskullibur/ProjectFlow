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
        /// Gets all Task information (incl. Task Allocations) By taskID
        /// </summary>
        /// <param name="tID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public IEnumerable<object> GetIssueByID(int tID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMates.Student")
                        .Where(x => x.taskID == tID)
                        .Where(x => x.active != false)
                        .Select(y => new
                        {
                            ID = y.taskID,
                            Task = y.title,
                            Description = y.description,
                            CreatedBy= y.TeamMember.Student.username,
                           
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

        /*public IEnumerable<object> GetCommentsByIssue(int tID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("CommentForIssue")
                        .Where(x => x.issueID == tID)
                        .Where(x => x.active != false)
                        .Select(y => new
                        {
                            //comment = y.CommentForIssue.comment,
                            //createdBy = ,
                            //issue = 
                        }).ToList();

                    return list;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }*/
    }
}