using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class IssueBLL
    {
        public bool Add(Issue issue)
        {
            IssueDAO IssueDAO = new IssueDAO();
            return IssueDAO.Add(issue);
        }
        public List<object> GetIssueById(int id)
        {
            IssueDAO issueDAO = new IssueDAO();
            var issue = issueDAO.GetIssueByID(id).ToList();
            return issue;
        }
    }
}