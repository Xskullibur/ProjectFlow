using ProjectFlow.Login;
using ProjectFlow.Login.AuthCallbacks;
using ProjectFlow.Login.LoginManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BasicLoginEvent(object sender, EventArgs e)
        {

            //Generate login credentials
            var credential = new LoginCredential(usernameTxtBox.Text, passwordTxtBox.Text);

            var loginService = GetBasicLoginService();

            loginService.Authenticate(this, credential);

            

        }

        private LoginService GetBasicLoginService() => new LoginService(new BasicLoginManager(), new RedirectAuthCallback("/MainContent.aspx"));

    }
}