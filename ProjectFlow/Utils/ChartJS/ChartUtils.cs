using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ProjectFlow.Utils.ChartJS
{
    public class ChartUtils
    {

        public static string CreateChart(string id)
        {
            string chart = $"<canvas id='{id}'></canvas>";
            return chart;
        }

        [WebMethod]
        public static string LoadDoughnutChart()
        {
            var data = new
            {
                labels = new[] { "Green" },
                datasets = new[]
                {
                    new {
                        data = new[] { "80" }
                    }
                }
            };

            return data.ToJSON();
        }
    }

    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}