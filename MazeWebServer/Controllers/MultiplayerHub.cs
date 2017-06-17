using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using MazeWebServer.Models;
using Microsoft.AspNet.SignalR.Hubs;
using MazeWebServer.Models;
using MazeLib;

namespace MazeWebServer.Controllers
{
    public struct PlayerIDInfo
    {
        public string hostID;
        public string guestID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    [HubName("MultiplayerHub")]
    public class MultiplayerHub : Hub
    {
        private ConcurrentDictionary<String, PlayerIDInfo> connectedUsers = new ConcurrentDictionary<string, PlayerIDInfo>();
        private static MultiplayerModel mpModel = new MultiplayerModel();
        public void Start(string mazeName, int rows, int cols)
        {
            Maze maze = mpModel.Start(mazeName, rows, cols);
            if (maze != null)
            {
                PlayerIDInfo pIDInfo = new PlayerIDInfo();
                pIDInfo.hostID = this.Context.ConnectionId;
                pIDInfo.guestID = "";
                Boolean sucess = connectedUsers.TryAdd(mazeName, pIDInfo);
            }
        }

        public void Join(string mazeName)
        {
            Maze maze = mpModel.Join(mazeName);
            if (maze != null)
            {
                PlayerIDInfo pIDInfo = connectedUsers[mazeName];
                pIDInfo.guestID = this.Context.ConnectionId;
                //TODO ACTIVATE GAME STARTED.
            }
        }

        public void MoveAction(string direction)
        {
            string otherPlayer = "";
            string currPlayer = this.Context.ConnectionId;
            foreach (string game in connectedUsers.Keys)
            {
                if (this.connectedUsers[game].hostID == currPlayer)
                {
                    otherPlayer = this.connectedUsers[game].guestID;
                    break;
                }
                if (this.connectedUsers[game].guestID == currPlayer)
                {
                    otherPlayer = this.connectedUsers[game].hostID;
                    break;
                }
            }
            if (otherPlayer != "")
            {
                this.Clients.Client(otherPlayer).move(direction);
            }

        }
    }
}
