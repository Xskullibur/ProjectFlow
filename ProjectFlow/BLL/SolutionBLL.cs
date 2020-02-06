using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class SolutionBLL
    {
        /// <summary>
        /// Gets Solution By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public Solution GetSolutionByID(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    Solution solution = dbContext.Solutions.First(x => x.solutionID == id);
                    return solution;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }

        /// <summary>
        /// Add a new Solution into DB
        /// </summary>
        /// <param name="solution">Solution to be Added</param>
        /// <returns>Boolean</returns>
        public bool Add(Solution solution)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (solution != null)
                {
                    try
                    {

                        dbContext.Solutions.Add(solution);
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Error While Adding solution: {e.Message}");
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
        /// Gets all Solution By IssueID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// 
        public IEnumerable<object> GetSolutionByIssueId(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Solutions.Include("TeamMembers.Student")
                        .Where(x => x.issueId == id)
                        .Select(y => new
                        {
                            solutionId = y.solutionID,
                            Title = y.title,
                            Description = y.description,
                            startDate = y.startdate,
                            CreatedBy = y.createdBy
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
        /// Add a new Solution into DB
        /// </summary>
        /// <param name="solution">Solution to be Added</param>
        /// <returns>Boolean</returns>
        public bool delete(Solution solution)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (solution != null)
                {
                    try
                    {

                        dbContext.Solutions.Attach(solution);
                        dbContext.Solutions.Remove(solution);
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Error While Adding solution: {e.Message}");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

    }
}