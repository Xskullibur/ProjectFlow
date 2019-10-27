using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Mark : Attribute
    {

        public Category Category;
        public Role RequiresRoles;



    }

    public enum Category
    {
        General, Settings, Premium
    }

    [Flags]
    public enum Role
    {
        BasicUser, PremiumUser
    }

}