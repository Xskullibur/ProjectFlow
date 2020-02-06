using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ProjectFlow;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using Newtonsoft.Json.Linq;

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
                    //Store currently joined clients inside redis
                    Global.Redis.GetDatabase().SetAdd(sessionId.ToString() + "-connected", student.UserId.ToString());

                    //Response to the client when it has successfully joined the session
                    var listOfPoints = Global.Redis.GetDatabase().ListRange(sessionIdAsString);

                    if(listOfPoints.Length == 0)
                    {
                        //If there is no any value take from file instead
                        string path = HostingEnvironment.MapPath("~/Services/Whiteboard/StrokesPath/" + sessionIdAsString + ".json");

                        if (File.Exists(path))
                        {
                            string json = File.ReadAllText(path);
                            JObject dataPoints = JObject.Parse(json);
                            foreach (var points in dataPoints.Value<JArray>("data-points"))
                            {
                                Global.Redis.GetDatabase().ListRightPush(sessionIdAsString, points.ToString());
                            }
                            //Retrieve the data from redis again
                            listOfPoints = Global.Redis.GetDatabase().ListRange(sessionIdAsString);
                        }

                    }


                    this.Clients.Caller.JoinWhiteboardSessionComplete(listOfPoints);

                    this.Clients.Group(sessionIdAsString).AlertUserJoin($"{student.firstName} {student.lastName}");
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
                try
                {
                    //Create a new session
                    WhiteboardSessionBLL whiteboardSessionBLL = new WhiteboardSessionBLL();

                    WhiteboardSession whiteboardSession = new WhiteboardSession
                    {
                        groupName = groupName,
                        creationDateTime = DateTime.Now,
                        teamID = teamID,
                        sessionID = Guid.NewGuid(),
                        strokesJsonPath = null
                    };

                    whiteboardSessionBLL.CreateNewSession(whiteboardSession);

                    //Get newly generated session id
                    Guid sessionId = whiteboardSession.sessionID;

                    //Store currently joined clients inside redis
                    Global.Redis.GetDatabase().SetAdd(sessionId.ToString() + "-connected", student.UserId.ToString());

                    //Response to the client when it has successfully created the session
                    this.Clients.Caller.CreatedWhiteboardSessionComplete(sessionId.ToString());
                }
                catch (Exception e)
                {

                    Console.WriteLine();
                }
                
            }
            else
            {
                this.Clients.Caller.IllegalAccess();
            }

            

        }

        public void Save(string sessionIdAsString)
        {
            Student student = (Context.User.Identity as ProjectFlowIdentity).Student;
            try
            {
                Guid sessionId = Guid.Parse(sessionIdAsString);

                //Check if session exist and have access
                if (WhiteboardHubUtils.CheckHaveAccess(sessionId, student))
                {
                    //Get the list of points inside redis
                    var listOfPoints = Global.Redis.GetDatabase().ListRange(sessionIdAsString);
                    //Save to file located in ~/StrokePath
                    string points = $"{{ \"data-points\": [{string.Join(",", listOfPoints)}] }}";


                    WhiteboardSessionBLL whiteboardSessionBLL = new WhiteboardSessionBLL();
                    WhiteboardSession whiteboardSession = whiteboardSessionBLL.GetWhiteboardSessionFromSessonId(sessionId);

                    string path = HostingEnvironment.MapPath("~/Services/Whiteboard/StrokesPath/" + sessionIdAsString + ".json");

                    File.WriteAllText(path, points);

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


        public void DrawMove(string sessionIdAsString, string strokeColor, float[] p)
        {
            Student student = (Context.User.Identity as ProjectFlowIdentity).Student;
            try
            {
                Guid sessionId = Guid.Parse(sessionIdAsString);
                //Check if session exist and have access
                if (WhiteboardHubUtils.CheckHaveAccessFromRedis(sessionId, student))
                {
                    //Get the list of points inside redis
                    Global.Redis.GetDatabase().ListRightPush(sessionIdAsString, $"{{\"points\": [{string.Join(",", p)}], \"strokeColor\": \"{strokeColor}\" }}");

                    Clients.OthersInGroup(sessionIdAsString).DrawMove(p, strokeColor);
                }
                else
                {
                    this.Clients.Caller.IllegalAccess();
                }


            }
            catch (Exception e)
            {
                this.Clients.Caller.UnableToReadSessionId();
            }

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

        public static bool CheckHaveAccessFromRedis(Guid sessionId, Student student)
        {
            //Sets contains is O(1) checking
            return Global.Redis.GetDatabase().SetContains(sessionId.ToString() + "-connected", student.UserId.ToString());
        }

    }

}