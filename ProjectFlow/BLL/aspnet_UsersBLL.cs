using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectFlow.BLL
{
    public class aspnet_UsersBLL
    {

        /// <summary>
        /// Get aspnet_Users by Student 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public aspnet_Users Getaspnet_UsersByStudent(Student student)
        {
            using(ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.Include(user => user.aspnet_Membership).First(user => user.Student.studentID == student.studentID);
            }
        }

    }
}