using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class PollingBLL
    {
        public bool Add(Polling vote)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (vote != null)
                {
                    bool preCheck = Check(vote.solutionID, vote.voterID);
                    if (preCheck != true)
                    {
                        try
                        {

                            dbContext.Pollings.Add(vote);
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
                        Console.Error.WriteLine("User has already voted");
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }

        public bool Check(int iID, int vID)
        {
            var list = GetPollByID(vID).ToList();
            var exist = list.Contains(iID);
            return exist;
        }

        public List<int> GetResult(int iID)
        {
            var result = GetResultByID(iID);
            return result;
        }

        /// <summary>
        /// Gets Issues By ID
        /// </summary>
        /// <param name="tID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public Polling GetVoteByID(int iID, int vID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    Polling polling = dbContext.Pollings
                        .Where(x => x.solutionID == iID)
                        .First(x => x.voterID == vID);
                    return polling;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }


            }
        }

        public IEnumerable<int> GetPollByID(int vID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.Pollings
                        .Where(x => x.voterID == vID)
                        .Select(y =>

                            y.solutionID
                        ).ToList();

                    return list;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }
            }
        }

        public List<int> GetResultByID(int iID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    int upvote = dbContext.Pollings
                        .Where(x => x.solutionID == iID)
                        .Where(x => x.vote == true)
                        .Select(y => y
                        ).Count();

                    int downvote = dbContext.Pollings
                        .Where(x => x.solutionID == iID)
                        .Where(x => x.vote == false)
                        .Select(y => y
                        ).Count(); ;

                    List<int> count = new List<int>();
                    count.Add(upvote);
                    count.Add(downvote);

                    return count;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }
            }
        }

        public IEnumerable<string> GetVotersBySelection(int iID, bool selection)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    var count = dbContext.Pollings.Include("TeamMembers.Student")
                        .Where(x => x.solutionID == iID)
                        .Where(x => x.vote == selection)
                        .Select(y => y.TeamMember.Student.aspnet_Users.UserName
                        ).ToList();

                    System.Diagnostics.Debug.WriteLine(count);
                    return count;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return null;
                }
            }
        }

        /// <summary>
        /// updates vote
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>Boolean</returns>
        public bool Update(Polling vote)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    // Update 
                    dbContext.Entry(vote).State = System.Data.Entity.EntityState.Modified;

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

    }
}