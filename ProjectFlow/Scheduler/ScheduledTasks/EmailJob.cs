using ProjectFlow.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectFlow.ScheduledTasks
{
    public class EmailJob : IJob
    {

        public async System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {

            try
            {
                // Get Values from Data Map
                JobDataMap dataMap = context.MergedJobDataMap;
                List<string> recivers = (List<string>)dataMap["Recivers"];
                string cc = dataMap.GetString("CC");
                string subject = dataMap.GetString("Subject");
                string textBody = dataMap.GetString("TextBody");

                // Create and Send Email
                EmailHelper emailHelper = new EmailHelper();
                bool result = emailHelper.SendEmail(recivers, subject, textBody, cc);

                Console.Out.WriteLine("Sending Email!");

                // Print Email Result
                if (result)
                {
                    Console.Out.WriteLine("Email Sent Successfully");
                }
                else
                {
                    Console.Out.WriteLine("Email Failed to Send");
                }
            }
            catch (JobExecutionException e)
            {
                Console.Out.WriteLine($"Email Job Failed: {e.Message}");
            }

        }

    }
}