using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class IssueDAO
    {
        /// <summary>
        /// Get Issue by Issue ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Issue</returns>
        public Issue GetIssueByID1(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    Issue issue = dbContext.Issues.First(x => x.taskID == id);
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
        /// Gets all Task information (incl. Task Allocations) By taskID
        /// </summary>
        /// <param name="tID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public IEnumerable<object> GetIssueByID2(int tID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Issues.Include("TeamMates.Student")
                        .Where(x => x.taskID == tID)
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
    }
}