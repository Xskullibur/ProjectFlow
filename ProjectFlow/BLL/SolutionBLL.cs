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
                    var list = dbContext.Solutions.Include("aspnet_Users")
                        .Where(x => x.issueId == id)
                        .Select(y => new
                        {
                            solutionId = y.solutionID,
                            Title = y.title,
                            Description = y.description,
                            startDate = y.startdate,
                            CreatedBy = y.aspnet_Users.UserName,
                            privacy = y.votePublic,
                            votePass = y.success
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
                        Solution _solution = dbContext.Solutions.Find(solution.solutionID);
                        _solution.Pollings.ToList().ForEach(x => dbContext.Pollings.Remove(x));

                        dbContext.SaveChanges();

                        dbContext.Solutions.Attach(_solution);
                        dbContext.Solutions.Remove(_solution);
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
        /// see how many votes needed to pass based on team id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        public bool getPass(int TId, int SId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    double list = dbContext.Solutions.Include("Issues.TeamMember")
                        .Where(x => x.Issue.TeamMember.teamID == TId)
                        .Select(y => y
                        ).Count();

                    int passnum = (int)Math.Round(list/2);

                    int upvote = dbContext.Pollings
                        .Where(x => x.solutionID == SId)
                        .Where(x => x.vote == true)
                        .Select(y => y
                        ).Count();

                    if (upvote >= passnum)
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
        /// updates solution
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>Boolean</returns>
        public bool Update(Solution solution)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    // Update 
                    dbContext.Entry(solution).State = System.Data.Entity.EntityState.Modified;

                    // Save Changes
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error While Updating solutions: {e.Message}");
                    return false;
                }
            }
        }

    }
}