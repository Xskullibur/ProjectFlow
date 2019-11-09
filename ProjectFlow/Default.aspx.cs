using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.Login;

namespace ProjectFlow
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BasicLoginEvent(object sender, AuthenticateEventArgs e)
        {

            //Generate login credentials
            var credential = new LoginCredential(LoginForm.UserName, LoginForm.Password);

            var loginService = GetBasicLoginService();

            e.Authenticated = loginService.Authenticate(this, credential);

        }

        private LoginService GetBasicLoginService() => new LoginService(new BasicLoginManager(), new SessionAuthCallback(), new RedirectAuthCallback("/Services/Tasks.aspx"));

    }
}