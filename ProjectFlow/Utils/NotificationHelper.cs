using ProjectFlow.Scheduler;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Utils
{
    public class NotificationHelper
    {

        /// <summary>
        /// Add Email Task Reminder
        /// </summary>
        /// <param name="task">Task to notify</param>
        /// <param name="allocators">Names of team members allocated</param>
        /// <param name="recivers">Emails of team members allocated</param>
        public static void AddEmail_TaskReminder_ONEDAY(Task task, List<string> allocators, List<string> recivers)
        {
            string jobName = $"{task.taskID}_Email_OneDayReminder";
            string emailSubject = $"{task.taskName} Due in ONE DAY";
            string emailBody = EmailHelper.GetTaskNotificationTemplate(1, task, allocators);

            DateTime triggerDate = task.endDate.Date.AddDays(-1);

            if (DateTime.Compare(triggerDate, DateTime.Now) <= 0)
            {
                IJobDetail job = JobScheduler.CreateEmailJob(jobName, recivers, emailSubject, emailBody);
                ISimpleTrigger trigger = JobScheduler.CreateSimpleTrigger(job, triggerDate);
                JobScheduler.AddEmailJobAsync(job, trigger);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Job Passed Date");
            }

        }

    }
}