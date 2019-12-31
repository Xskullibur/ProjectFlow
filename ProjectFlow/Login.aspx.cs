using ProjectFlow.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using ProjectFlow.BLL;
using ProjectFlow.Utils;

namespace ProjectFlow
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginValidateAction(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            LoginBLL loginService = new LoginBLL();

            if(Membership.ValidateUser(username, password))
            {
                //Found student or tutor, login success 

                var authenticatedUser = Membership.GetUser(username);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, 
                    DateTime.Now, DateTime.Now.AddMinutes(2880), rememberMeCheckBox.Checked, LoginUtil.ConvertAuthenticatedUserToRole(authenticatedUser), FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                string url = FormsAuthentication.GetRedirectUrl(username, rememberMeCheckBox.Checked);

                if (url.Equals("/"))
                {
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                }
                else
                {
                    Response.Redirect(url);
                }
            }
            else
            {
                //Student not found, login failed
                this.ShowAlertWithTiming("Login Failed", BootstrapAlertTypes.DANGER, 3000);
            }

        }
    }
}