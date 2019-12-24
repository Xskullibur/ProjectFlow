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

            StudentDAO dao = new StudentDAO();

            Student student = dao.LoginValidate(email, password);

            if(student != null)
            {
                //Found student, login success 
                FormsAuthentication.RedirectFromLoginPage(email, rememberMeCheckBox.Checked);
            }
            else
            {
                //Student not found, login failed
                this.ShowAlertWithTiming("Login Failed", BootstrapAlertTypes.DANGER, 3000);
            }

        }
    }
}