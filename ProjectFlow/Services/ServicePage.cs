using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Services
{
    public abstract class ServicePage : Page
    {
        
        public string ServiceName { get; }

        public string Path { get; }
        protected ServicePage(string serviceName, string path)
        {
            ServiceName = serviceName;
            Path = path;
        }


        public static Dictionary<Category, List<ServicePage>> Services;
        /// <summary>
        /// Register service page to be displayed under Default.aspx
        /// </summary>
        /// <param name="servicePage">ServicePage to be registered</param>
       public void RegisterServicePage<T>(T servicePage) where T : ServicePage
        {
            //Get ServicePage page attributes
            var properties = typeof(T).GetCustomAttributes();

            //Extract ProjectFlow's Mark attribute using reflection
            foreach(var property in properties)
            {
                if(property is Mark)
                {
                    Mark mark = property as Mark;

                    if (!Services.ContainsKey(mark.Category)) Services.Add(mark.Category, new List<ServicePage>() { servicePage});
                    else
                    {
                        Services[mark.Category].Add(servicePage);
                    }

                }
            }



        }



    }
}