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
                string subject = dataMap.GetString("Subject");
                string textBody = dataMap.GetString("TextBody");

                // Create and Send Email
                EmailHelper emailHelper = new EmailHelper();
                bool result = emailHelper.SendEmail(recivers, subject, textBody);

                // Print Email Result
                if (result)
                {
                    await Console.Out.WriteLineAsync("Email Sent Successfully");
                }
                else
                {
                    await Console.Out.WriteLineAsync("Email Failed to Send");
                }
            }
            catch (JobExecutionException e)
            {
                await Console.Out.WriteLineAsync($"Email Job Failed: {e.Message}");
            }

        }

    }
}