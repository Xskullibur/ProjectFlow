using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using ProjectFlow.BLL;

namespace ProjectFlow.Utils
{
    public class EmailHelper
    {
        string sender = ConfigurationManager.AppSettings["Email"].ToString();
        string senderPW= ConfigurationManager.AppSettings["EmailPW"].ToString();
        string smtp= ConfigurationManager.AppSettings["SMTP"].ToString();
        int portNo = Convert.ToInt32(ConfigurationManager.AppSettings["PortNo"].ToString());

        public static string GetTaskNotificationTemplate(string templateDir, string url, string title, Task task)
        {
            try
            {
                StatusBLL statusBLL = new StatusBLL();
                MilestoneBLL milestoneBLL = new MilestoneBLL();

                string status = statusBLL.GetStatusByID(task.statusID).status1;
                string milestone = "-";

                List<string> allocations = task.TaskAllocations.Select(x => x.TeamMember.Student.firstName + " " + x.TeamMember.Student.lastName).ToList();
                milestone = milestoneBLL.GetMilestoneByID(task.milestoneID).milestoneName;

                using (StreamReader streamReader = new StreamReader(templateDir))
                {
                    string textBody = streamReader.ReadToEnd();

                    //Update Template Values
                    textBody = textBody.Replace("[Title]", title);
                    textBody = textBody.Replace("[TaskDesc]", task.taskDescription);
                    textBody = textBody.Replace("[StartDate]", task.startDate.ToString());
                    textBody = textBody.Replace("[EndDate]", task.endDate.ToString());
                    textBody = textBody.Replace("[Milestone]", milestone);
                    textBody = textBody.Replace("[Priority]", task.Priority.priority1);
                    textBody = textBody.Replace("[Status]", status);
                    textBody = textBody.Replace("[TaskDirectory]", url);

                    if (allocations == null)
                    {
                        textBody = textBody.Replace("[Allocations]", "-");
                    }
                    else
                    {
                        textBody = textBody.Replace("[Allocations]", string.Join(", ", allocations));
                    }

                    return textBody;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static string GetTaskNotificationTemplate(string title, Task task)
        {

            try
            {
                string templateDir = HttpContext.Current.Server.MapPath("~/Utils/EmailTemplates/TaskNotification.html");

                StatusBLL statusBLL = new StatusBLL();
                MilestoneBLL milestoneBLL = new MilestoneBLL();

                string status = statusBLL.GetStatusByID(task.statusID).status1;
                string milestone = "-";

                List<string> allocations = task.TaskAllocations.Select(x => x.TeamMember.Student.firstName + " " + x.TeamMember.Student.lastName).ToList();
                milestone = milestoneBLL.GetMilestoneByID(task.milestoneID).milestoneName;

                using (StreamReader streamReader = new StreamReader(templateDir))
                {
                    string textBody = streamReader.ReadToEnd();

                    //Update Template Values
                    textBody = textBody.Replace("[Title]", title);
                    textBody = textBody.Replace("[TaskDesc]", task.taskDescription);
                    textBody = textBody.Replace("[StartDate]", task.startDate.ToString());
                    textBody = textBody.Replace("[EndDate]", task.endDate.ToString());
                    textBody = textBody.Replace("[Milestone]", milestone);
                    textBody = textBody.Replace("[Priority]", task.Priority.priority1);
                    textBody = textBody.Replace("[Status]", status);
                    textBody = textBody.Replace("[TaskDirectory]", HttpContext.Current.Request.Url.AbsoluteUri);

                    if (allocations == null)
                    {
                        textBody = textBody.Replace("[Allocations]", "-");
                    }
                    else
                    {
                        textBody = textBody.Replace("[Allocations]", string.Join(", ", allocations));
                    }

                    return textBody;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Email Template Error: \n{e.Message}");
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
        public bool SendEmail(List<string> recivers, string subject, string textBody, string cc = null)
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

                if (cc != null)
                {
                    _mailMessage.CC.Add(cc);
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