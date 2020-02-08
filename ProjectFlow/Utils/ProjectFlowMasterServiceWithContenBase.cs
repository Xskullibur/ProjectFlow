using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjectFlow.Utils.Base
{
    public static class ProjectFlowMasterServiceWithContentBaseUtils
    {
        /// <summary>
        /// Set the service page title
        /// </summary>
        /// <param name="page"></param>
        /// <param name="headerText"></param>
        public static void SetHeaderFromServiceWtihContentBase(this Page page, string headerText)
        {
            (page.Master as ServicesWithContentBase).Header = headerText;
        }

    }
}