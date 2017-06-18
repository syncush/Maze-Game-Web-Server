using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using MazeWebServer.Models;
using Microsoft.AspNet.SignalR.Hubs;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;
using MazeWebServer.Entitys;
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
    [HubName("multiplayerHub")]
    public class MultiplayerHub : Hub
    {
        public static ConcurrentDictionary<String, GameInfo> connectedUsers = new ConcurrentDictionary<string, GameInfo>();
        public static MultiplayerModel mpModel = new MultiplayerModel();

        public void Start(string mazeName, int rows, int cols)
        {
            Maze maze = mpModel.Start(mazeName, rows, cols);
            if (maze != null)
            {
                GameInfo pIDInfo = new GameInfo();
                pIDInfo.HostID = this.Context.ConnectionId;
                pIDInfo.GuestID = "";
                Boolean sucess = connectedUsers.TryAdd(mazeName, pIDInfo);
            }
        }

        public void Join(string mazeName)
        {
            Maze maze = mpModel.Join(mazeName);
            if (maze != null)
            {
                connectedUsers[mazeName].GuestID = this.Context.ConnectionId;
                JObject mazeJSON = JObject.Parse(maze.ToJSON());
                this.Clients.Client(this.Context.ConnectionId).gameStarted(mazeJSON);
                this.Clients.Client(connectedUsers[mazeName].HostID).gameStarted(mazeJSON);
            }
        }

        public void MoveAction(string direction)
        {
            string otherPlayer = "";
            string currPlayer = this.Context.ConnectionId;
            foreach (string game in connectedUsers.Keys)
            {
                if (connectedUsers[game].HostID == currPlayer)
                {
                    otherPlayer = connectedUsers[game].GuestID;
                    break;
                }
                if (connectedUsers[game].GuestID == currPlayer)
                {
                    otherPlayer = connectedUsers[game].HostID;
                    break;
                }
            }
            if (otherPlayer != "")
            {
                this.Clients.Client(otherPlayer).gotMessage(direction);
            }

        }

        public void Close(string winnerPlayer)
        {
            string otherPlayer = "";
            GameInfo temp;
            string gameToBeDeleted = "";
            string currPlayer = this.Context.ConnectionId;
            foreach (string game in connectedUsers.Keys)
            {
                if (connectedUsers[game].HostID == currPlayer)
                {
                    otherPlayer = connectedUsers[game].GuestID;
                    gameToBeDeleted = game;
                    break;
                }
                if (connectedUsers[game].GuestID == currPlayer)
                {
                    otherPlayer = connectedUsers[game].HostID;
                    gameToBeDeleted = game;
                    break;
                }
            }
            if (otherPlayer != "")
            {
                //UPDATE WINNER
                connectedUsers.TryRemove(gameToBeDeleted, out temp);
                this.Clients.Client(otherPlayer).close();
            }
        }
    }
}
