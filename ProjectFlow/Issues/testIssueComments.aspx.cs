using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Issues
{
    public partial class testIssueComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            repeaterData();
        }

        public void repeaterData()
        {
            List<string> myList = new List<string>();
            myList.Add("Test1");
            myList.Add("Test2");
            myList.Add("Test3");

            /*var myList = new List<Tuple<string, string>> {
                Tuple.Create("penguin","penguin"),
                Tuple.Create("penguin1","penguin1"),
                Tuple.Create("penguin2","penguin2")
            };*/

            Repeater1.DataSource = myList;
            Repeater1.DataBind();
        }
    }
}