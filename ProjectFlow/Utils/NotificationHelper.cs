using ProjectFlow.BLL;
using ProjectFlow.Scheduler;
using ProjectFlow.Tasks;
using Quartz;
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
            DAY_7, DAY_1, DAY_0, OVERDUE
        }

        private static bool LeaderInAllocation(List<Student> allocatedStudents, Student leader)
        {
            return allocatedStudents.Select(x => x.studentID).Contains(leader.studentID);
        }

        /// <summary>
        /// Send Status Reminders (Completed / Verification / Due Dates) based on Task ID
        /// </summary>
        /// <param name="taskID">int</param>
        public static void Default_AddTask_Notification_Setup(int taskID)
        {
            // Get Task
            TaskBLL taskBLL = new TaskBLL();
            Task task = taskBLL.GetTaskByID(taskID);

            // Check Status
            StatusBLL statusBLL = new StatusBLL();
            Status task_status = statusBLL.GetStatusByID(task.statusID);

            if (task_status.status1 == StatusBLL.COMPLETED)
            {
                // TODO: Task Completed Job
            }
            else if (task_status.status1 == StatusBLL.VERIFICATON)
            {
                // TODO: Task Verification Job
            }
            else
            {
                // Task Reminder Job
                int days_remaining = TaskHelper.VerifyDaysLeft(task.endDate);

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

                    // TODO: TODAY Reminder
                }

                // TODO: Overdue Reminder
            }

        }

        /// <summary>
        /// Send Email Reminder Based on Task Details (Allocated Students / Leader)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="task"></param>
        public static void GetDetailsAndSendReminderEmail(REMINDER_TYPE type, Task task)
        {
            StudentBLL studentBLL = new StudentBLL();

            // Get Students Involved  (Allocated Team Members / Team Leader)
            List<Student> allocatedStudents = studentBLL.GetAllocationsByTaskID(task.taskID);
            Student leader = studentBLL.GetLeaderByTaskID(task.taskID);
            bool students_allocated_exist = allocatedStudents.Count > 0 ? true : false;

            // Default Values
            List<string> allocated_names = new List<string>();
            List<string> email_recivers = new List<string>();
            string leader_email = leader.aspnet_Users.aspnet_Membership.Email;

            // Send Notification to Leader if No Allocation
            if (allocatedStudents.Count > 0)
            {
                allocated_names = allocatedStudents.Select(x => x.firstName + " " + x.lastName).ToList();
                email_recivers = allocatedStudents.Select(x => x.aspnet_Users.aspnet_Membership.Email).ToList();
            }

            if (students_allocated_exist)
            {
                // Send Allocated Students
                if (!LeaderInAllocation(allocatedStudents, leader))
                {
                    // Leader Not in Allocation
                    Email_TaskReminder(type, task, allocated_names, email_recivers);
                }
                else
                {
                    // Leader in Allocation
                    Email_TaskReminder(type, task, allocated_names, email_recivers, leader_email);
                }
            }
            else
            {
                // Send Leader
                Email_TaskReminder(type, task, null, new List<string> { leader_email });
            }
        }

        /// <summary>
        /// Create an Email Task Reminder Job
        /// </summary>
        /// <param name="type">Reminder Type</param>
        /// <param name="task">Task to notify</param>
        /// <param name="allocatedNames">Names of allocated team members</param>
        /// <param name="reciversEmail">Email of allocated team members</param>
        /// <param name="cc"></param>
        private static void Email_TaskReminder(REMINDER_TYPE type, Task task, List<string> allocatedNames, List<string> reciversEmail, string cc = null)
        {
            string jobName = task.taskID.ToString();
            string subject = null;
            string emailBody = null;
            DateTime triggerDate = new DateTime();

            // Setup Email Based on Type
            switch (type)
            {
                case REMINDER_TYPE.DAY_7:
                    jobName += "_EMAIL_REMINDER_7DAYS";
                    subject = $"{task.taskName} is Due in 7 Days";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, 7, task, allocatedNames);
                    triggerDate = task.endDate.AddDays(-7);
                    break;
                case REMINDER_TYPE.DAY_1:
                    jobName += "_EMAIL_REMINDER_1DAYS";
                    subject = $"{task.taskName} is Due Tomorrow";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, 7, task, allocatedNames);
                    triggerDate = task.endDate.AddDays(1);
                    break;
                case REMINDER_TYPE.DAY_0:
                    jobName += "_EMAIL_REMINDER_0DAYS";
                    subject = $"{task.taskName} is Due Today";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, 7, task, allocatedNames);
                    triggerDate = task.endDate;
                    break;
                case REMINDER_TYPE.OVERDUE:
                    jobName += "_EMAIL_REMINDER_OVERDUE";
                    subject = $"{task.taskName} is Overdue";
                    emailBody = EmailHelper.GetTaskNotificationTemplate(subject, 7, task, allocatedNames);
                    triggerDate = task.endDate.AddDays(1);
                    break;
            }

            // Check if setup successful
            if (subject != null && emailBody != null && triggerDate != DateTime.MinValue)
            {
                // Schedule Email Job
                IJobDetail job = JobScheduler.CreateEmailJob(jobName, reciversEmail, subject, emailBody, cc);
                ISimpleTrigger trigger = JobScheduler.CreateSimpleTrigger(job, triggerDate);
                JobScheduler.AddEmailJobAsync(job, trigger);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"\nError in Scheduling EMAIL REMINDER\n");
            }
        }

    }
}