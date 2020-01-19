using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.BLL
{
    public class ActionItemBLL
    {
        /// <summary>
        /// Insert new Action Item into database
        /// </summary>
        /// <param name="actionItem"></param>
        public void AddActionItem(RoomActionItem actionItem)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                dbContext.RoomActionItems.Add(actionItem);
                dbContext.SaveChanges();
            }
        }

    }
}