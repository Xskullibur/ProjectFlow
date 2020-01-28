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


        public static async System.Threading.Tasks.Task DeleteJobs(string jobName, string jobGrp)
        {
            // Get Scheduler
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            await scheduler.DeleteJob(new JobKey(jobName, jobGrp));
        }

        /// <summary>
        /// Update job details based on Job Name and Job Group
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="jobGrp"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailBody"></param>
        /// <param name="emailRecivers"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task UpdateJobDataMap_Email(string jobName, string jobGrp, string emailSubject, string emailBody, List<string> emailRecivers, string cc = null)
        {
            // Get Scheduler
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            // Get Job Details
            IJobDetail jobDetail = await scheduler.GetJobDetail(new JobKey(jobName, jobGrp));

            // Update Datamap
            jobDetail.JobDataMap.Put("Subject", emailSubject);
            jobDetail.JobDataMap.Put("TextBody", emailBody);
            jobDetail.JobDataMap.Put("Recivers", emailRecivers);
            jobDetail.JobDataMap.Put("CC", cc);

            await scheduler.AddJob(jobDetail, true);
        }

        /// <summary>
        /// Update old trigger with new trigger
        /// </summary>
        /// <param name="oldTrigger"></param>
        /// <param name="newTrigger"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task UpdateTrigger(ISimpleTrigger oldTrigger, ISimpleTrigger newTrigger)
        {
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            await scheduler.RescheduleJob(oldTrigger.Key, newTrigger);
        }


        /// <summary>
        /// Pause specified job based on Job Name and Job Group
        /// </summary>
        /// <param name="jobName">string</param>
        /// <param name="jobGrp">string</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task PauseJob(string jobName, string jobGrp)
        {
            // Get Scheduler
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();
            await scheduler.PauseJob(new JobKey(jobName, jobGrp));

            System.Diagnostics.Debug.WriteLine($"\nPausing Job: {jobName}, {jobGrp}\n");
        }


        /// <summary>
        /// Resume specifiec job based ono Job Name and Job Group
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="jobGrp"></param>
        /// <returns></returns>
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
                .Build();

            job.JobDataMap.Put("Subject", emailSubject);
            job.JobDataMap.Put("TextBody", emailBody);
            job.JobDataMap.Put("Recivers", emailRecivers);
            job.JobDataMap.Put("CC", cc);

            return job;
        }


        /// <summary>
        /// Create a trigger for specified Job
        /// </summary>
        /// <param name="job"></param>
        /// <param name="triggerDate"></param>
        /// <returns></returns>
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
        /// Schedule Specified Job
        /// </summary>
        /// <param name="jobName">Must Be Unique!</param>
        /// <param name="triggerDate">When the job will trigger</param>
        /// <param name="recivers">People whom will recive the email</param>
        /// <param name="subject">Email's Subject</param>
        /// <param name="textBody">Email's Contents (In HTML)</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task ScheduleJobWithTrigger(IJobDetail job, ISimpleTrigger trigger)
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