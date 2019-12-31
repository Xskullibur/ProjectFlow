using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.DAO
{
    public class aspnet_UsersDAO
    {
        public aspnet_Users GetUserFromUserName(string username)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.Include("Student").Include("Tutor").FirstOrDefault(user => user.UserName.Equals(username));
            }
        }

    }
}