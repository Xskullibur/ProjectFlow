using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Utils
{
    public static class ProjectFlowMasterServiceWithContentUtils
    {
        /// <summary>
        /// Set the service page title
        /// </summary>
        /// <param name="page"></param>
        /// <param name="headerText"></param>
        public static void SetHeader(this Page page, string headerText)
        {
            (page.Master as ServicesWithContent).Header = headerText;
        }

    }
}