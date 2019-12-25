using ProjectFlow.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Tasks
{
    public class TaskVerification
    {
        public List<string> TNameErrors { get; set; }
        public List<string> TDescErrors { get; set; }
        public List<string> TMilestoneErrors { get; set; }
        public List<string> TStartDateErrors { get; set; }
        public List<string> TEndDateErrors { get; set; }
        public List<string> TStatusErrors { get; set; }
        public List<string> StartEndDateErrors { get; set; }

        public TaskVerification()
        {
            TNameErrors = new List<string>();
            TDescErrors = new List<string>();
            TMilestoneErrors = new List<string>();
            TStartDateErrors = new List<string>();
            TEndDateErrors = new List<string>();
            TStatusErrors = new List<string>();
            StartEndDateErrors = new List<string>();
        }

        // Verify Add Task
        public bool Verify(string taskName, string taskDesc, int milestoneIndex, string startDate, string endDate, int statusIndex)
        {
            bool verified = true;
            TNameErrors.Clear();
            TDescErrors.Clear();
            TMilestoneErrors.Clear();
            TStartDateErrors.Clear();
            TEndDateErrors.Clear();
            TStatusErrors.Clear();
            StartEndDateErrors.Clear();

            /**
             * Check For Errors
             **/ 

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
            if (milestoneIndex == -1)
            {
                TMilestoneErrors.Add("Milestone Field is Required!");
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
                if (DateTime.Compare(DateTime.Parse(startDate), DateTime.Parse(endDate)) >= 0)
                {
                    StartEndDateErrors.Add("Start Date cannot be later than End Date!");
                    verified = false;
                }
            }

            // Status
            if (statusIndex == -1)
            {
                TStatusErrors.Add("Status Field is Required!");
                verified = false;
            }

            return verified;

            ///**
            // * Show Error Messages
            // **/

            //if (!verified)
            //{
            //    // Name
            //    if (TNameErrors.Count > 0)
            //    {
            //        tNameErrorLbl.Visible = true;
            //        tNameErrorLbl.Text = string.Join("<br>", TNameErrors);
            //    }

            //    // Description
            //    if (tDescErrors.Count > 0)
            //    {
            //        tDescErrorLbl.Visible = true;
            //        tDescErrorLbl.Text = string.Join("<br>", tDescErrors);
            //    }

            //    // Milestone
            //    if (tMilestoneErrors.Count > 0)
            //    {
            //        tMilestoneErrorLbl.Visible = true;
            //        tMilestoneErrorLbl.Text = string.Join("<br>", tMilestoneErrors);
            //    }

            //    // Start Date
            //    if (tStartDateErrors.Count > 0)
            //    {
            //        tStartDateErrorLbl.Visible = true;
            //        tStartDateErrorLbl.Text = string.Join("<br>", tStartDateErrors);
            //    }

            //    // End Date
            //    if (tEndDateErrors.Count > 0)
            //    {
            //        tEndDateErrorLbl.Visible = true;
            //        tEndDateErrorLbl.Text = string.Join("<br>", tEndDateErrors);
            //    }

            //    // Start End Date
            //    if (startEndDateErrors.Count > 0)
            //    {
            //        startEndDateErrorLbl.Visible = true;
            //        startEndDateErrorLbl.Text = string.Join("<br>", startEndDateErrors);
            //    }

            //    // Status
            //    if (tStatusErrors.Count > 0)
            //    {
            //        statusErrorLbl.Visible = true;
            //        statusErrorLbl.Text = string.Join("<br>", tStatusErrors);
            //    }
            //}
        }

    }
}