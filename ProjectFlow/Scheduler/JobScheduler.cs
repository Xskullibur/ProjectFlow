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
        /// Add a Job for sending email
        /// </summary>
        /// <param name="jobName">Must Be Unique!</param>
        /// <param name="triggerDate">When the job will trigger</param>
        /// <param name="recivers">People whom will recive the email</param>
        /// <param name="subject">Email's Subject</param>
        /// <param name="textBody">Email's Contents (In HTML)</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task AddEmailJobAsync(string jobName, DateTime triggerDate, List<string> recivers, string subject, string textBody)
        {
            try
            {
                // Get Scheduler
                IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
                await scheduler.Start();

                // Create Email Job
                IJobDetail job = JobBuilder.Create<EmailJob>()
                    .WithIdentity(jobName, "EmailJob")
                    .UsingJobData("Subject", subject)
                    .UsingJobData("TextBody", textBody)
                    .Build();

                job.JobDataMap.Put("Recivers", recivers);

                // Create Job Trigger
                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                    .WithIdentity(jobName + "_trigger", jobName + "_trigger")
                    .StartAt(triggerDate.ToUniversalTime())
                    .ForJob(job)
                    .Build();

                // Schedule Job with Trigger
                await scheduler.ScheduleJob(job, trigger);
                System.Diagnostics.Debug.WriteLine($"\n\n{jobName} Scheduled for {triggerDate.ToString()} !\n\n");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Scheduling Job: {e.Message}");
            }
        }

    }
}