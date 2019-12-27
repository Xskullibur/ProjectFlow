using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace ProjectFlow.Utils
{
    public class EmailHelper
    {
        string sender = ConfigurationManager.AppSettings["Email"].ToString();
        string senderPW= ConfigurationManager.AppSettings["EmailPW"].ToString();
        string smtp= ConfigurationManager.AppSettings["SMTP"].ToString();
        int portNo = Convert.ToInt32(ConfigurationManager.AppSettings["PortNo"].ToString());

        public string GetTaskNotificationTemplate(int dueDays, string taskName, string taskDesc, DateTime startDate, DateTime endDate, string milestone, string status, string allocations)
        {

            try
            {
                string templateDir = HttpContext.Current.Server.MapPath("~/Utils/EmailTemplates/TaskNotification.html");

                using (StreamReader streamReader = new StreamReader(templateDir))
                {
                    string textBody = streamReader.ReadToEnd();

                    //Update Template Values
                    textBody = textBody.Replace("[DueDays]", dueDays.ToString());
                    textBody = textBody.Replace("[TaskName]", taskName);
                    textBody = textBody.Replace("[TaskDesc]", taskDesc);
                    textBody = textBody.Replace("[StartDate]", startDate.ToString());
                    textBody = textBody.Replace("[EndDate]", endDate.ToString());
                    textBody = textBody.Replace("[Milestone]", milestone);
                    textBody = textBody.Replace("[Status]", status);
                    textBody = textBody.Replace("[Allocations]", allocations);

                    return textBody;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }

        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="recivers">List of Reciver's Emails</param>
        /// <param name="subject">Subject of Email</param>
        /// <param name="textBody">Body of the Email</param>
        /// <returns>Boolean</returns>
        public bool SendEmail(List<string> recivers, string subject, string textBody)
        {
            try
            {
                MailMessage _mailMessage = new MailMessage();
                _mailMessage.IsBodyHtml = true;
                _mailMessage.From = new MailAddress(sender);

                foreach (string reciver in recivers)
                {
                    _mailMessage.To.Add(reciver);
                }

                _mailMessage.Subject = subject;
                _mailMessage.Body = textBody;

                SmtpClient _smtp = new SmtpClient();
                _smtp.Host = smtp;
                _smtp.Port = portNo;
                _smtp.EnableSsl = true;

                NetworkCredential _network = new NetworkCredential(sender, senderPW);
                _smtp.Credentials = _network;

                _smtp.Send(_mailMessage);

                return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

    }
}