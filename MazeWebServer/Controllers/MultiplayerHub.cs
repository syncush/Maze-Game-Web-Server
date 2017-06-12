using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MazeWebServer.Controllers
{
    public class MultiplayerHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}