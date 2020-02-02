using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Profile
{
    /// <summary>
    /// Display user profile informations and updating
    /// </summary>
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetHeader("Profile informations");
            RefreshProfile();
        }

        private void RefreshProfile()
        {
            var identity = this.User.Identity as ProjectFlowIdentity;

            if (identity.IsStudent)
            {
                var student = identity.Student;

                DisplayProfile(student);

            }
            else if (identity.IsTutor)
            {
                var tutor = identity.Tutor;

                DisplayProfile(tutor);

            }
        }

        /// <summary>
        /// Display student profile informations 
        /// </summary>
        /// <param name="student"></param>
        private void DisplayProfile(Student student)
        {
            UsernameLbl.Text = student.aspnet_Users.UserName;
            EmailLbl.Text = student.aspnet_Users.aspnet_Membership.Email;
            AdminNoLbl.Text = student.studentID;
            NameLbl.Text = student.firstName + " " + student.lastName;

            DisplayProfileImage(student.aspnet_Users);
        }

        /// <summary>
        /// Display tutor profile informations 
        /// </summary>
        /// <param name="tutor"></param>
        private void DisplayProfile(Tutor tutor)
        {
            UsernameLbl.Text = tutor.aspnet_Users.UserName;
            EmailLbl.Text = tutor.aspnet_Users.aspnet_Membership.Email;
            AdminNoLbl.Text = "--not applicable--";
            NameLbl.Text = tutor.firstName + " " + tutor.lastName;

            DisplayProfileImage(tutor.aspnet_Users);

        }
        /// <summary>
        /// Display a image of the user from aspnet_Users
        /// </summary>
        /// <param name="user"></param>
        private void DisplayProfileImage(aspnet_Users user)
        {
            if(user.ProfileImagePath != null)
            {
                ProfileImg.ImageUrl = "ProfileImages/" + user.ProfileImagePath;
            }
            else
            {
                ProfileImg.ImageUrl = "ProfileImages/default-picture.png";
            }
        }

        protected void UpdatePasswordEvent(object sender, EventArgs e)
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            MembershipUser user = null;

            if (identity.IsStudent)
            {
                user = Membership.GetUser(identity.Student.aspnet_Users.UserName);
            }else if (identity.IsTutor)
            {
                user = Membership.GetUser(identity.Tutor.aspnet_Users.UserName);
            }
            try
            {
                string newPassword = PasswordTextBox.Text;
                if ((newPassword.Length >= Membership.MinRequiredPasswordLength) &&
                (newPassword.ToCharArray().Count(c => !Char.IsLetterOrDigit(c)) >=
                     Membership.MinRequiredNonAlphanumericCharacters) &&
                ((Membership.PasswordStrengthRegularExpression.Length == 0) ||
                     Regex.IsMatch(newPassword, Membership.PasswordStrengthRegularExpression)))
                {
                    if (user != null && user.ChangePassword(user.ResetPassword(), PasswordTextBox.Text))
                    {
                        (Master as IAlert).ShowAlert("Password successfully updated", BootstrapAlertTypes.SUCCESS);
                        DisplayPasswordPanelEvent(null, null);
                    }
                }
                else
                {
                    (Master as IAlert).ShowAlert($"Password must have at least <b>{Membership.MinRequiredPasswordLength}</b> required " +
                        $"length and at least <b>{Membership.MinRequiredNonAlphanumericCharacters}</b> non-alphanumeric characters ", BootstrapAlertTypes.DANGER, false);
                }

                
            }
            catch(Exception ex)
            {
                (Master as IAlert).ShowAlert("Password not successfully updated", BootstrapAlertTypes.DANGER);
            }
        }
        protected void DisplayPasswordPanelChangeEvent(object sender, EventArgs e)
        {
            PasswordPanel.Visible = false;
            PasswordPanelChange.Visible = true;
        }
        protected void DisplayPasswordPanelEvent(object sender, EventArgs e)
        {
            PasswordPanel.Visible = true;
            PasswordPanelChange.Visible = false;
        }

        protected void ChangeProfileImageEvent(object sender, EventArgs e)
        {
            var identity = this.User.Identity as ProjectFlowIdentity;
            if (ImageFileUploadControl.HasFile)
            {
                try
                {
                    if(ImageFileUploadControl.PostedFile.ContentType == "image/jpeg" || ImageFileUploadControl.PostedFile.ContentType == "image/png")
                    {
                        string filename = Path.GetFileName(ImageFileUploadControl.FileName);
                        string savedFilename = identity.aspnet_Users.UserId.ToString() + Path.GetExtension(filename);
                        //Delete old file
                        if (File.Exists(Server.MapPath("ProfileImages/") + identity.aspnet_Users.ProfileImagePath))
                        {
                            File.Delete(Server.MapPath("ProfileImages/") + identity.aspnet_Users.ProfileImagePath);
                        }
                        ImageFileUploadControl.SaveAs(Server.MapPath("ProfileImages/") + savedFilename);

                        //Successfully uploaded the file
                        this.ShowAlertWithTiming("Profile is successfully uploaded", BootstrapAlertTypes.SUCCESS, 3000);

                        //Change profile picture in database
                        var aspnet_UsersBLL = new aspnet_UsersBLL();
                        aspnet_UsersBLL.UpdateProfilePicture(identity.aspnet_Users, savedFilename);

                        //Refresh page
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        this.ShowAlertWithTiming("Supported files type are only; JPG, PNG", BootstrapAlertTypes.DANGER, 3000);
                    }
                }
                catch (Exception ex)
                {
                    //Unsccessfully uploaded the file

                    this.ShowAlert("Profile is not successfully uploaded", BootstrapAlertTypes.DANGER);
                }
            }
        }
    }
}