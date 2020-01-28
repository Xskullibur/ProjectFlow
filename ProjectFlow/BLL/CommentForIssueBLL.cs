using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class CommentForIssueBLL
    {
 	    /// <summary>
        /// Add a new Comment into DB
        /// </summary>
        /// <param name="comment">comment to be Added</param>
        /// <returns>Boolean</returns>
        public bool Add(CommentForIssue comment)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                // Check if object exist
                if (comment != null)
                {
                    try
                    {

                        dbContext.CommentForIssues.Add(comment);
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
        /// Gets Comments
        /// </summary>
        /// <param name="iID"></param>
        /// <returns>Anonymous Object</returns>
        /// 
        /// (ID, Task, Description, IdTask)
        public IEnumerable<object> GetCommentByIssueId(int id)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {

                try
                {
                    var list = dbContext.CommentForIssues.Include("TeamMates.Student")
                        .Where(x => x.issueID == id)
                        .Select(y => new
                        {
                            Comment = y.comment,
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
    }
}