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
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error While Starting Scheduler: {e.Message}");
            }
        }

        public static async System.Threading.Tasks.Task PauseJob(string jobName, string jobGrp)
        {
            // Get Scheduler
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();
            await scheduler.PauseJob(new JobKey(jobName, jobGrp));

            System.Diagnostics.Debug.WriteLine($"\nPausing Job: {jobName}, {jobGrp}\n");
        }


        public static async System.Threading.Tasks.Task ResumeJob(string jobName, string jobGrp)
        {
            // Get Scheduler
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();
            await scheduler.ResumeJob(new JobKey(jobName, jobGrp));
        }

        /// <summary>
        /// Create an Email Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="emailRecivers"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailBody"></param>
        /// <returns></returns>
        public static IJobDetail CreateEmailJob(string jobName, string jobGrp, List<string> emailRecivers, string emailSubject, string emailBody, string cc = null)
        {
            IJobDetail job = JobBuilder.Create<EmailJob>()
                .WithIdentity(jobName, jobGrp)
                .UsingJobData("Subject", emailSubject)
                .UsingJobData("TextBody", emailBody)
                .Build();

            job.JobDataMap.Put("Recivers", emailRecivers);
            job.JobDataMap.Put("CC", cc);

            return job;
        }


        public static ISimpleTrigger CreateSimpleTrigger(IJobDetail job, DateTime triggerDate)
        {
            string jobName = job.Key.Name;
            string triggerGrp = $"{jobName}_email_trigger";
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