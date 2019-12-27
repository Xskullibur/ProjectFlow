using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class PollingDAO
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

                    //System.Diagnostics.Debug.WriteLine("hello world");
                    //System.Diagnostics.Debug.WriteLine(list);
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