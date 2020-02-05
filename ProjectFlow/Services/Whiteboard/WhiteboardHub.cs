using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ProjectFlow.Services.Whiteboard
{
    [HubName("Whiteboard")]
    public class WhiteboardHub : Hub
    {
        public void StartUp(string msg)
        {
            Trace.WriteLine(msg);
        }

        public void DrawMove(int[] p)
        {
            Clients.Others.DrawMove(p);
        }
    }
}