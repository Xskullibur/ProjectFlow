using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ProjectFlow.BLL;
using ProjectFlow.Login;

namespace ProjectFlow.Services.Whiteboard
{
    [HubName("Whiteboard")]
    public class WhiteboardHub : Hub
    {

        public void JoinWhiteboardGroup(string sessionIdAsString)
        {
            try
            {
                Guid sessionId = Guid.Parse(sessionIdAsString);
                Student student = (Context.User.Identity as ProjectFlowIdentity).Student;
                //Check if session exist and have access
                if (WhiteboardHubUtils.CheckHaveAccess(sessionId, student))
                {
                    //Add the current connection to the group
                    this.Groups.Add(Context.ConnectionId, sessionId.ToString());

                    //Add the new connection to the redis


                    //Response to the client the it has successfully joined the session
                    this.Clients.Caller.JoinWhiteboardGroupComplete();
                }
                else
                {
                    this.Clients.Caller.IllegalAccess();
                }
            }
            catch (Exception)
            {
                this.Clients.Caller.UnableToReadSessionId();
            }
        }

        public void StartNewWhiteboardGroup(string groupName, int teamID)
        {
            Student student = (Context.User.Identity as ProjectFlowIdentity).Student;
            if (WhiteboardHubUtils.CheckIfStudentBelongsToTeam(student, teamID))
            {
                //Create a new session
                WhiteboardSessionBLL whiteboardSessionBLL = new WhiteboardSessionBLL();

                WhiteboardSession whiteboardSession = new WhiteboardSession
                {
                    groupName = groupName,
                    creationDateTime = new DateTime(),
                    teamID = teamID
                };

                whiteboardSessionBLL.CreateNewSession(whiteboardSession);

                //Get newly generated session id
                Guid sessionId = whiteboardSession.sessionID;

                //Store currently joined clients inside redis

            }
            else
            {
                this.Clients.Caller.IllegalAccess();
            }

            

        }

        public void Save(string sessionIdAsString)
        {
            try
            {
                Guid sessionId = Guid.Parse(sessionIdAsString);

                //Check if session exist and have access
                if (WhiteboardHubUtils.CheckHaveAccess(sessionId, student))
                {
                    //Get the list of points inside redis

                    //Save to file located in ~/StrokePath

                    //Tell the client save is successful
                    this.Clients.Caller.WhiteboardSaveSuccessful();
                }
                else
                {
                    this.Clients.Caller.IllegalAccess();
                }


            }
            catch (Exception)
            {
                this.Clients.Caller.UnableToReadSessionId();
            }

        }


        public void DrawMove(int[] p)
        {
            Clients.OthersInGroup("group1").DrawMove(p);
        }
    }
    
    /// <summary>
    /// A utils class for helping to write DRY codes
    /// </summary>
    public class WhiteboardHubUtils
    {
        public static bool CheckHaveAccess(Guid sessionId, Student student)
        {
            WhiteboardSessionBLL whiteboardSessionBLL = new WhiteboardSessionBLL();
            var whiteboardSession = whiteboardSessionBLL.GetWhiteboardSessionFromSessonId(sessionId);
            return whiteboardSessionBLL.CheckIfCanAccessWhiteboardSession(whiteboardSession, student);
        }


        public static bool CheckIfStudentBelongsToTeam(Student student, int teamID)
        {
            //Get ProjectTeam
            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
            ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByTeamID(teamID);

            //Check if student belongs to the team
            StudentBLL studentBLL = new StudentBLL();
            if (!studentBLL.HaveProjectTeam(student, projectTeam))
            {
                //Illegal access
                return false;
            }
            return true;
        }
    }

    public class Point {
        public float X { get; set; }
        public float Y { get; set; }
        public string Color { get; set; }
    }

}