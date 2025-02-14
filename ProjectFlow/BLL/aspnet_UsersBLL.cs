﻿using System;
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

        /// <summary>
        /// Get aspnet_Users by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public aspnet_Users Getaspnet_UsersByUserName(string username)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users.FirstOrDefault(x => x.UserName.Equals(username));
            }
        }

        /// <summary>
        /// Get aspnet_Users by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public aspnet_Users Getaspnet_UsersByUserId(Guid userId)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                return dbContext.aspnet_Users
                    .Include(x => x.Student)
                    .Include(x => x.Tutor)
                    .FirstOrDefault(x => x.UserId.Equals(userId));
            }
        }

        /// <summary>
        /// Set profile image filename of the user
        /// </summary>
        /// <param name="filename"></param>
        public void UpdateProfilePicture(aspnet_Users user,  string filename)
        {
            using (ProjectFlowEntities dbContext = new ProjectFlowEntities())
            {
                var _user = dbContext.aspnet_Users.Find(user.UserId);
                _user.ProfileImagePath = filename;
                dbContext.SaveChanges();
            }
        }

    }
}