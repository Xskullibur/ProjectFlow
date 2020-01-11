using ProjectFlow.ScheduledTasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ProjectFlow.Scheduler
{
    public static class JobScheduler
    {

        public static async void StartAsync()
        {
            try
            {
                // Construct and Start Scheduler
                NameValueCollection props = new NameValueCollection
                {
                { "quartz.serializer.type", "binary" }
                };

                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Starting Scheduler: {e.Message}");
            }
        }

        /// <summary>
        /// Create an Email Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="emailRecivers"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailBody"></param>
        /// <returns></returns>
        public static IJobDetail CreateEmailJob(string jobName, List<string> emailRecivers, string emailSubject, string emailBody, string cc = null)
        {
            IJobDetail job = JobBuilder.Create<EmailJob>()
                .WithIdentity(jobName, "EmailJob")
                .UsingJobData("Subject", emailSubject)
                .UsingJobData("TextBody", emailBody)
                .Build();

            job.JobDataMap.Put("Recivers", emailRecivers);
            job.JobDataMap.Put("CC", cc);

            return job;
        }


        public static  ISimpleTrigger CreateSimpleTrigger(IJobDetail job, DateTime triggerDate)
        {
            string jobName = job.Key.Name;
            string triggerGrp = $"{jobName}_trigger";
            string triggerName = $"{triggerGrp}_{triggerDate.ToShortDateString()}";

            // Create Job Trigger
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGrp)
                .StartAt(triggerDate.ToUniversalTime())
                .Build();

            return trigger;
        }


        /// <summary>
        /// Add a Job for sending email
        /// </summary>
        /// <param name="jobName">Must Be Unique!</param>
        /// <param name="triggerDate">When the job will trigger</param>
        /// <param name="recivers">People whom will recive the email</param>
        /// <param name="subject">Email's Subject</param>
        /// <param name="textBody">Email's Contents (In HTML)</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task AddEmailJobAsync(IJobDetail job, ISimpleTrigger trigger)
        {
            try
            {
                // Get Scheduler
                IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
                await scheduler.Start();

                // Schedule Job with Trigger
                await scheduler.ScheduleJob(job, trigger);
                System.Diagnostics.Debug.WriteLine($"\n\n{job.Key.Name} Scheduled for {trigger.StartTimeUtc.ToLocalTime().ToString()} !\n\n");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Scheduling Job: {e.Message}");
            }
        }

    }
}