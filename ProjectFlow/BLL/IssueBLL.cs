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

        public List<object> GetIssueByTeamId(int id)
        {
            IssueDAO issueDAO = new IssueDAO();
            var issue = issueDAO.GetIssueByTeamID(id).ToList();
            return issue;
        }

        public List<object> GetDroppedIssueByTeamId(int id)
        {
            IssueDAO issueDAO = new IssueDAO();
            var issue = issueDAO.GetDroppedIssueByTeamID(id).ToList();
            return issue;
        }

        public bool Drop(int id, int UserId)
        {
            IssueDAO issueDAO = new IssueDAO();
            Issue deleted_task = issueDAO.GetIssueByID(id);

            return issueDAO.drop(deleted_task, UserId);
        }
    }
}