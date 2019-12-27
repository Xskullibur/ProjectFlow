using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class CommentForIssueBLL
    {
        public List<object> GetCommentByIssueId(int id)
        {
            CommentForIssueDAO commentForIssueDAO = new CommentForIssueDAO();
            var comment = commentForIssueDAO.GetCommentByissueID(id).ToList();
            return comment;
        }
    }
}