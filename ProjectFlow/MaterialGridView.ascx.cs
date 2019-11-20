using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class MaterialGridView : System.Web.UI.UserControl
    {
        public const string NURMEIC = "mdc-data-table_header-cell--numeric";

        protected void Page_Load(object sender, EventArgs e)
        {
            AddColumn("Hi ALson", "");
            AddColumn("Hi ALson", "");
            AddColumn("Hi ALson", "");
        }

        private void AddColumn(string headerName, string type)
        {
            string containerId = Container.ClientID;
            Response.Write(@"
                <script language='javascript'>
                    window.addEventListener('load', function(){
                        var header = $('#" + containerId + @" .mdc-data-table__header-row')
                        header.append('<th class=\'mdc-data-table__header-cell\' role=\'columnheader\' scope=\'col\'>" + headerName + @"</th>')
                    });
                </script>");
        }

    }
}