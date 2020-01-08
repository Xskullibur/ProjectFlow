using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services.Christina
{
    public partial class Christina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Create speaker on the client side so all the speaker are displayed
        /// 
        /// Called during Page_Load
        /// 
        /// </summary>
        /// <param name="speakers"></param>
        private void InjectSpeaker(Student[] speakers)
        {
            
            string createSpeakers = @"
                <script type=""text/javascript"">
                    
                </script>
            ";

            foreach(var speaker in speakers)
            {
                createSpeakers += $"create_speaker({speaker.aspnet_Users});";
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "create_speakers", createSpeakers, false);
        }

    }
}