using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ProjectFlow.BLL;

public class EventDAO
{
    private static string connectionString = ConfigurationManager.AppSettings["DBConnString"];

    const string LOW_PRIORITY_COLOR = "rgba(0, 153, 0, 0.3)";
    const string MEDIUM_PRIORITY_COLOR = "rgba(255, 127, 0, 0.3)";
    const string HIGH_PRIORITY_COLOR = "rgba(255, 0, 0, 0.3)";

	//retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();

        TaskBLL taskBLL = new TaskBLL();
        var taskList = taskBLL.GetOngoingTasksBetween(start, end);

        foreach (ProjectFlow.Task task in taskList)
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

	//this method updates the event title and description
    public static void updateEvent(int id, String title, String description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE Event SET title=@title, description=@description WHERE event_id=@event_id", con);
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value= description;
        cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
    public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE Event SET event_start=@event_start, event_end=@event_end, all_day=@all_day WHERE event_id=@event_id", con);
        cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = start;
        cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = end;
        cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
        cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = allDay;

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DELETE FROM Event WHERE (event_id = @event_id)", con);
        cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this method adds events to the database
    public static int addEvent(CalendarEvent cevent)
    {
        //add event to the database and return the primary key of the added event row

        //insert
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO Event(title, description, event_start, event_end, all_day) VALUES(@title, @description, @event_start, @event_end, @all_day)", con);
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
        cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
        cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
        cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

        int key = 0;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();

            //get primary key of inserted row
            cmd = new SqlCommand("SELECT max(event_id) FROM Event where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end AND all_day=@all_day", con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
            cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
            cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
            cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

            key = (int)cmd.ExecuteScalar();
        }

        return key;
    }
}
