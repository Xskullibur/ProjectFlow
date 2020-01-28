using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Utils.Filter
{
    public class Filter
    {
        public string filterType { get; set; }
        public string filterValue { get; set; }

        public Filter()
        {

        }

        public Filter(string type, string val)
        {
            filterType = type;
            filterValue = val;
        }
    }
}