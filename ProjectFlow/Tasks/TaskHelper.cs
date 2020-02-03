using ProjectFlow.BLL;
using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Tasks
{
    public class TaskHelper
    {
        public List<string> TNameErrors { get; set; }
        public List<string> TDescErrors { get; set; }
        public List<string> TMilestoneErrors { get; set; }
        public List<string> TStartDateErrors { get; set; }
        public List<string> TEndDateErrors { get; set; }
        public List<string> TStatusErrors { get; set; }
        public List<string> StartEndDateErrors { get; set; }
        public List<string> TPriorityErrors { get; private set; }

        public TaskHelper()
        {
            TNameErrors = new List<string>();
            TDescErrors = new List<string>();
            TMilestoneErrors = new List<string>();
            TStartDateErrors = new List<string>();
            TEndDateErrors = new List<string>();
            TStatusErrors = new List<string>();
            StartEndDateErrors = new List<string>();
            TPriorityErrors = new List<string>();
        }

        private void ClearErrorMsgs()
        {
            TNameErrors.Clear();
            TDescErrors.Clear();
            TMilestoneErrors.Clear();
            TStartDateErrors.Clear();
            TEndDateErrors.Clear();
            TStatusErrors.Clear();
            StartEndDateErrors.Clear();
        }


        /// <summary>
        /// Verification for valid task
        /// </summary>
        /// <param name="teamID"></param>
        /// <param name="taskName"></param>
        /// <param name="taskDesc"></param>
        /// <param name="milestoneID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="statusID"></param>
        /// <returns>Booolean</returns>
        public bool VerifyAddTask(int teamID, string taskName, string taskDesc, string milestoneID, string startDate, string endDate, string statusID, string priorityID)
        {
            bool verified = true;
            ClearErrorMsgs();

            // Task Name
            if (string.IsNullOrEmpty(taskName))
            {
                TNameErrors.Add("Task Name Field is Required!");
                verified = false;
            }
            else if (taskName.Length > 255)
            {
                TNameErrors.Add("Maximum Length of 255 Characters!");
                verified = false;
            }

            // Description
            if (string.IsNullOrEmpty(taskDesc))
            {
                TDescErrors.Add("Description Field is Required!");
                verified = false;
            }
            else if (taskDesc.Length > 255)
            {
                TDescErrors.Add("Maximum Length of 255 Characters!");
                verified = false;
            }


            // Milestone
            MilestoneBLL milestoneBLL = new MilestoneBLL();
            List<Milestone> teamMilestones = milestoneBLL.GetMilestonesByTeamID(teamID);

            if (int.TryParse(milestoneID, out int milestoneID_Int))
            {
                if (!teamMilestones.Select(x => x.milestoneID).Contains(milestoneID_Int))
                {
                    TMilestoneErrors.Add("Invalid Milestone Selected!");
                    verified = false;
                }
            }
            else
            {
                TMilestoneErrors.Add("Invalid Milestone Selected!");
                verified = false;
            }

            // Start Date
            if (string.IsNullOrEmpty(startDate))
            {
                TStartDateErrors.Add("Start Date Field is Required!");
                verified = false;
            }
            else if (!DateTime.TryParse(startDate, out DateTime start))
            {
                TStartDateErrors.Add("Invalid Format!");
                verified = false;
            }

            // End Date
            if (string.IsNullOrEmpty(endDate))
            {
                TEndDateErrors.Add("End Date Field is Required!");
                verified = false;
            }
            else if (!DateTime.TryParse(endDate, out DateTime end))
            {
                TEndDateErrors.Add("Invalid Format!");
                verified = false;
            }

            // Compare Start End Date
            if (TStartDateErrors.Count == 0 && TEndDateErrors.Count == 0)
            {
                if (DateTime.Compare(DateTime.Parse(startDate).Date, DateTime.Parse(endDate).Date) > 0)
                {
                    StartEndDateErrors.Add("Start Date cannot be later than End Date!");
                    verified = false;
                }
            }

            // Status
            StatusBLL statusBLL = new StatusBLL();
            Dictionary<int, string> statusDict = statusBLL.Get();

            if (int.TryParse(statusID, out int statusID_int))
            {
                if (!statusDict.Keys.Contains(statusID_int))
                {
                    TStatusErrors.Add("Invalid Status Selected!");
                    verified = false;
                } 
            }
            else
            {
                TStatusErrors.Add("Invalid Status Selected!");
                verified = false;
            }

            // Priority
            PriorityBLL priorityBLL = new PriorityBLL();
            List<Priority> priorities = priorityBLL.Get();

            if (int.TryParse(priorityID, out int priorityID_int))
            {
                if (!priorities.Select(x => x.ID).Contains(priorityID_int))
                {
                    TPriorityErrors.Add("Invalid Priority Selected!");
                    verified = false;
                }
            }
            else
            {
                TPriorityErrors.Add("Invalid Priority Selected!");
                verified = false;
            }

            return verified;
        }

        // Verify Update Status
        public bool VerifyUpdateStatus(string currentStatus, Student leader, Student currentUser)
        {
            if (currentStatus == StatusBLL.VERIFICATON)
            {
                if (leader.studentID != currentUser.studentID)
                {
                    return false;
                }
            }
            else if (currentStatus == StatusBLL.COMPLETED)
            {
                return false;
            }

            return true;
        }


        public static int GetDaysLeft(DateTime endDate)
        {
            return (endDate - DateTime.Now.Date).Days;
        }

    }
}