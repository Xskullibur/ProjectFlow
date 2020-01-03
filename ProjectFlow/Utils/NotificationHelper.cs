using ProjectFlow.BLL;
using ProjectFlow.Scheduler;
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

        public NotificationHelper(Task task)
        {

        }

        public static void Default_AddTask_Notification_Setup(int taskID)
        {
            // Check Task Status
            TaskBLL taskBLL = new TaskBLL();
            StatusBLL statusBLL = new StatusBLL();

            // Get Task
            Task task = taskBLL.GetTaskById(taskID);
            Status task_status = statusBLL.GetStatusByID(task.statusID);

            // Check Status
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
                // TODO: Task Reminder Job
                Send_Notification_Reminder_ONEDAY(task);
            }

            // TODO: Task Overdue Job
        }

        public static void Send_Notification_Reminder_ONEDAY(Task task)
        {
            StudentBLL studentBLL = new StudentBLL();

            // Get Students Involved  (Allocated Team Members / Team Leader)
            List<Student> allocatedStudents = studentBLL.GetAllocationsByTaskID(task.taskID);
            Student leader = studentBLL.GetLeaderByTaskID(task.taskID);

            // Names of those allocated
            List<string> Allocated_Names = allocatedStudents.Select(x => x.firstName + " " + x.lastName).ToList();

            // TODO: Check Notification Preference

                /**
                 * EMAIL PREFERENCE
                 **/
                List<Student> Email_AllocatedStudents = allocatedStudents; // TODO: Filter allocated students by those who prefer email
                List<string> recivers = Email_AllocatedStudents.Select(x => x.aspnet_Users.aspnet_Membership.Email).ToList();

                // Check if Leader in Email_AllocatedStudents
                if (!Email_AllocatedStudents.Contains(leader))
                {
                    string leader_email = leader.aspnet_Users.aspnet_Membership.Email;
                    Email_TaskReminder_ONEDAY(task, Allocated_Names, recivers, leader_email);
                }
                else
                {
                    Email_TaskReminder_ONEDAY(task, Allocated_Names, recivers);
                }

                /**
                 * SMS PREFERENCE
                 **/
                List<string> HP_AllocatedStudents = allocatedStudents.Select(x => x.aspnet_Users.aspnet_Membership.MobilePIN).ToList();

            // END OF TODO
        }

        /// <summary>
        /// Add Email Task Reminder
        /// </summary>
        /// <param name="task">Task to notify</param>
        /// <param name="allocatorsNames">Names of team members allocated</param>
        /// <param name="recivers">Emails of team members allocated</param>
        public static void Email_TaskReminder_ONEDAY(Task task, List<string> allocatorsNames, List<string> recivers, string cc =  null)
        {
            string jobName = $"{task.taskID}_Email_OneDayReminder";
            string emailSubject = $"{task.taskName} Due in ONE DAY";
            string emailBody = EmailHelper.GetTaskNotificationTemplate(1, task, allocatorsNames);

            DateTime triggerDate = task.endDate.Date.AddDays(-1);

            if (DateTime.Compare(triggerDate.Date, DateTime.Now.Date) >= 0)
            {
                IJobDetail job = JobScheduler.CreateEmailJob(jobName, recivers, emailSubject, emailBody, cc);
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