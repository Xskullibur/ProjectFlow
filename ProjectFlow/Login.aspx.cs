﻿using ProjectFlow.DAO;
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

namespace ProjectFlow
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginValidateAction(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            LoginBLL loginService = new LoginBLL();

            Student student = loginService.LoginValidate(email, password);

            if(student != null)
            {
                //Found student, login success 

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, email, 
                    DateTime.Now, DateTime.Now.AddMinutes(2880), rememberMeCheckBox.Checked, "Student", FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(email, rememberMeCheckBox.Checked));
            }
            else
            {
                //Student not found, login failed
                this.ShowAlertWithTiming("Login Failed", BootstrapAlertTypes.DANGER, 3000);
            }

        }
    }
}