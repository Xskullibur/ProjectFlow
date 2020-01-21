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
        public List<int> Getcheck(int vID) //this is just for testing and needs to be removed
        {
            var list = GetPollByID(vID).ToList();
            return list;
        }
        public int GetResult(int iID)
        {
            var result = GetResultByID(iID);
            return result;
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

                            y.issueID
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

        public int GetResultByID(int iID)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                try
                {
                    var count = dbContext.Pollings
                        .Where(x => x.issueID == iID)
                        .Where(x => x.vote == true)
                        .Select(y => y
                        ).Count() - dbContext.Pollings
                        .Where(x => x.issueID == iID)
                        .Where(x => x.vote == false)
                        .Select(y => y
                        ).Count(); ;

                    System.Diagnostics.Debug.WriteLine(count);
                    return count;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error While Retrieving Task: {e.Message}");
                    return 0;
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
                        .Where(x => x.issueID == iID)
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

    }
}