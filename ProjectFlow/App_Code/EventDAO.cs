using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ProjectFlow.BLL;
using ProjectFlow;
using System.Web;
using System.Linq;

public class EventDAO
{
    private struct FilterType
    {
        public static string KEYWORD = "filterTaskName";
        public static string PRIORITY = "filterPriority";
        public static string STATUS = "filterStatus";
        public static string ALLOCATION = "filterAllocation";
    }

    const string LOW_PRIORITY_COLOR = "rgba(0, 153, 0, 0.3)";
    const string MEDIUM_PRIORITY_COLOR = "rgba(255, 127, 0, 0.3)";
    const string HIGH_PRIORITY_COLOR = "rgba(255, 0, 0, 0.3)";

	//retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();

        TaskBLL taskBLL = new TaskBLL();
        var taskList = taskBLL.GetOngoingTasksBetween(start, end);

        taskList = ApplyFilters(taskList);

        foreach (Task task in taskList)
        {
            string color;

            switch (task.priorityID)
            {

                case 1:
                    color = LOW_PRIORITY_COLOR;
                    break;

                case 2:
                    color = MEDIUM_PRIORITY_COLOR;
                    break;

                case 3:
                    color = HIGH_PRIORITY_COLOR;
                    break;

                default:
                    color = LOW_PRIORITY_COLOR;
                    break;
            }

            events.Add(new CalendarEvent()
            {
                id = task.taskID,
                title = task.taskName,
                description = task.taskDescription,
                start = task.startDate,
                end = task.endDate,
                allDay = false,
                color = color
            });
        }

        return events;
    }

    public static List<Task> ApplyFilters(List<Task> data)
    {
        HttpContext context = HttpContext.Current;

        if (context.Session[FilterType.KEYWORD] != null)
        {
            string filterKeyword = context.Session[FilterType.KEYWORD].ToString();
            data = data.Where(x => x.taskName.ToLower().Contains(filterKeyword.ToLower()))
                .ToList();
        }

        if (context.Session[FilterType.PRIORITY] != null)
        {
            Dictionary<int, string> priorityDict = (context.Session[FilterType.PRIORITY] as Dictionary<int, string>);

            foreach (var item in priorityDict)
            {
                data = data.Where(x => x.priorityID == item.Key)
                    .ToList();
            }
        }

        if (context.Session[FilterType.STATUS] != null)
        {
            Dictionary<int, string> statusDict = (context.Session[FilterType.STATUS] as Dictionary<int, string>);

            foreach (var item in statusDict)
            {
                data = data.Where(x => x.statusID == item.Key)
                    .ToList();
            }
        }

        if (context.Session[FilterType.ALLOCATION] != null)
        {
            Dictionary<int, string> allocationDict = (context.Session[FilterType.ALLOCATION] as Dictionary<int, string>);

            foreach (var item in allocationDict)
            {
                data = data.Where(x => x.TaskAllocations.Select(allocation => allocation.TeamMember.memberID).Contains(item.Key))
                    .ToList();
            }

        }

        return data;
    }

}
