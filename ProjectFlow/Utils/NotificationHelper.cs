using ProjectFlow.BLL;
using ProjectFlow.Scheduler;
using ProjectFlow.Tasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace ProjectFlow.Utils
{
    public class NotificationHelper
    {
        public enum REMINDER_TYPE
        {
            COMPLETED, VERIFICATION, DAY_7, DAY_1, DAY_0, OVERDUE
        }

        private static bool LeaderInAllocation(List<Student> allocatedStudents, Student leader)
        {
            return allocatedStudents.Select(x => x.studentID).Contains(leader.studentID);
        }


        // Status Update

        /// <summary>
        /// Email Notification When Task Updated to Verification
        /// </summary>
        /// <param name="taskID"></param>
        public static void Send_Verification_Email(int taskID)
        {
            TaskBLL taskBLL = new TaskBLL();
            Task task = taskBLL.GetTaskByID(taskID);

            string title = $"{task.taskName} is up for verification";
            string subject = "Task Verification";
            string emailTemplate = EmailHelper.GetTaskNotificationTemplate(title, task);

            StudentBLL studentBLL = new StudentBLL();
            List<string> leaderEmail = new List<string> {
                studentBLL.GetLeaderByTaskID(task.taskID)
                .aspnet_Users
                .aspnet_Membership
                .Email
            };

            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmail(leaderEmail, subject, emailTemplate);
        }

        /// <summary>
        /// Email Notification When Task Updated to Completed
        /// </summary>
        /// <param name="taskID"></param>
        public static void Send_Complete_Email(int taskID)
        {
            TaskBLL taskBLL = new TaskBLL();
            Task task = taskBLL.GetTaskByID(taskID);

            string title = $"{task.taskName} Completed";
            string subject = "Task Completed";
            string emailTemplate = EmailHelper.GetTaskNotificationTemplate(title, task);

            StudentBLL studentBLL = new StudentBLL();
            List<string> allocatorsEmail = studentBLL.GetAllocationsByTaskID(task.taskID)
                .Select(x => x.aspnet_Users.aspnet_Membership.Email)
                .ToList();

            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmail(allocatorsEmail, subject, emailTemplate);
        }


        // Task Updated

        /// <summary>
        /// Remove all existing jobs related to the task and recreate
        /// </summary>
        /// <param name="taskID"></param>
        public static void Default_TaskUpdate_Setup(int taskID)
        {
            TaskBLL taskBLL = new TaskBLL();
            Task updated_task = taskBLL.GetTaskByID(taskID);

            JobDetailsBLL jobDetailsBLL = new JobDetailsBLL();
            List<QRTZ_JOB_DETAILS> jobDetails = jobDetailsBLL.GetJobDetailsByTaskID(taskID);

            // Remove all jobs related to task
            jobDetails.ForEach(async job => 
                await JobScheduler.DeleteJobs(job.JOB_NAME, job.JOB_GROUP)
            );

            Default_AddTask_Setup(taskID);
        }


        //  Task Drop and Restore

        /// <summary>
        /// Pause all jobs assigned to task when task is dropped
        /// </summary>
        /// <param name="taskID">Int</param>
        public static void Task_Drop_Setup(int taskID)
        {
            JobDetailsBLL jobDetailsBLL = new JobDetailsBLL();
            List<QRTZ_JOB_DETAILS> jobs = jobDetailsBLL.GetJobDetailsByTaskID(taskID);

            if (jobs.Count > 0)
            {
                jobs.ForEach(async x => await JobScheduler.PauseJob(x.JOB_NAME, x.JOB_GROUP));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"\nNo Jobs Found (Pause Job)\n");
            }
        }


        /// <summary>
        /// Resume all jobs assigned to task when task is restored
        /// </summary>
        /// <param name="taskID"></param>
        internal static void Task_Restore_Setup(int taskID)
        {
            JobDetailsBLL jobDetailsBLL = new JobDetailsBLL();
            List<QRTZ_JOB_DETAILS> jobs = jobDetailsBLL.GetJobDetailsByTaskID(taskID);

            if (jobs.Count > 0)
            {
                jobs.ForEach(async x => await JobScheduler.ResumeJob(x.JOB_NAME, x.JOB_GROUP));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"\nNo Jobs Found (Resume Job)\n");
            }
        }


        // Task Added

        /// <summary>
        /// Send Status Reminders (Completed / Verification / Due Dates) based on Task ID
        /// </summary>
        /// <param name="taskID">int</param>
        public static void Default_AddTask_Setup(int taskID)
        {
            // Get Task
            TaskBLL taskBLL = new TaskBLL();
            Task task = taskBLL.GetTaskByID(taskID);

            // Check Status
            StatusBLL statusBLL = new StatusBLL();
            Status task_status = statusBLL.GetStatusByID(task.statusID);

            // Task Reminder Job
            int days_remaining = TaskHelper.GetDaysLeft(task.endDate);

            // In-progress Reminder
            if (days_remaining >= 0)
            {
                //  7 DAYS
                if (days_remaining >= 7)
                {
                    GetDetailsAndSendReminderEmail(REMINDER_TYPE.DAY_7, task);
                }

                // TOMORROW
                if (days_remaining >= 1)
                {
                    GetDetailsAndSendReminderEmail(REMINDER_TYPE.DAY_1, task);
                }

                // TODAY
                GetDetailsAndSendReminderEmail(REMINDER_TYPE.DAY_0, task);
            }

            // Overdue
            GetDetailsAndSendReminderEmail(REMINDER_TYPE.OVERDUE, task);

        }


        // Private methods

        /// <summary>
        /// Send Email Reminder Based on Task Details (Allocated Students / Leader)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="task"></param>
        private static void GetDetailsAndSendReminderEmail(REMINDER_TYPE type, Task task)
        {
            StudentBLL studentBLL = new StudentBLL();

            // Get Students Involved  (Allocated Team Members / Team Leader)
            List<Student> allocatedStudents = studentBLL.GetAllocationsByTaskID(task.taskID);
            Student leader = studentBLL.GetLeaderByTaskID(task.taskID);
            bool students_allocated_exist = allocatedStudents.Count > 0 ? true : false;

            // Default Values
            List<string> email_recivers = new List<string>();
            string leader_email = leader.aspnet_Users.aspnet_Membership.Email;

            // Send Notification to Leader if No Allocation
            if (allocatedStudents.Count > 0)
            {
                email_recivers = allocatedStudents.Select(x => x.aspnet_Users.aspnet_Membership.Email).ToList();
            }

            if (students_allocated_exist)
            {
                // Send Allocated Students
                if (!LeaderInAllocation(allocatedStudents, leader))
                {
                    // Leader Not in Allocation
                    Schedule_Email_TaskReminder(type, task, email_recivers, leader_email);
                }
                else
                {
                    // Leader in Allocation
                    Schedule_Email_TaskReminder(type, task, email_recivers);
                }
            }
            else
            {
                // Send Leader
                Schedule_Email_TaskReminder(type, task, new List<string> { leader_email });
            }
        }


        /// <summary>
        /// Schedule an Email Task Reminder Job
        /// </summary>
        /// <param name="type">Reminder Type</param>
        /// <param name="task">Task to notify</param>
        /// <param name="reciversEmail">Email of allocated team members</param>
        /// <param name="cc"></param>
        private static void Schedule_Email_TaskReminder(REMINDER_TYPE type, Task task, List<string> reciversEmail, string cc = null)
        {
            // Task Details
            string taskID = task.taskID.ToString();
            string taskName = task.taskName;

            //Job  Details
            string jobName = null;
            string jobNamePrefix = "_EMAIL_REMINDER";
            string jobGroup = $"{taskID}_EMAIL_REMINDER";

            //Trigger Details
            DateTime triggerDate = new DateTime();

            //Email Details
            string subject = null;
            string emailBody = null;

            // Setup Email Based on Type
            switch (type)
            {
                case REMINDER_TYPE.DAY_7:
                    jobName += $"{taskID}_7DAYS{jobNamePrefix}";
                    subject = $"{taskName} is Due in 7 Days";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, task);
                    triggerDate = task.endDate.AddDays(-7);
                    break;
                case REMINDER_TYPE.DAY_1:
                    jobName += $"{taskID}_1DAY{jobNamePrefix}";
                    subject = $"{taskName} is Due Tomorrow";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, task);
                    triggerDate = task.endDate.AddDays(1);
                    break;
                case REMINDER_TYPE.DAY_0:
                    jobName += $"{taskID}_0DAY{jobNamePrefix}";
                    subject = $"{taskName} is Due Today";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, task);
                    triggerDate = task.endDate;
                    break;
                case REMINDER_TYPE.OVERDUE:
                    jobName += $"{taskID}_OVERDUE{jobNamePrefix}";
                    subject = $"{taskName} is Overdue";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, task);
                    triggerDate = task.endDate.AddDays(1);
                    break;
            }

            // Check if setup successful
            if (jobName != null && subject != null && emailBody != null && triggerDate != DateTime.MinValue)
            {
                // Schedule Email Job
                IJobDetail job = JobScheduler.CreateEmailJob(jobName, jobGroup, reciversEmail, subject, emailBody, cc);
                ISimpleTrigger trigger = JobScheduler.CreateSimpleTrigger(job, triggerDate);
                JobScheduler.ScheduleJobWithTrigger(job, trigger);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"\nError in Scheduling EMAIL REMINDER\n");
            }
        }

    }
}