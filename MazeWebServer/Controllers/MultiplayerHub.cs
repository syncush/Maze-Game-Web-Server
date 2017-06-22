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
        /// <summary>
        /// The connected users
        /// </summary>
        public static ConcurrentDictionary<String, GameInfo> connectedUsers = new ConcurrentDictionary<string, GameInfo>();
        /// <summary>
        /// The mp model
        /// </summary>
        public static MultiplayerModel mpModel = new MultiplayerModel();


        /// <summary>
        /// Starts the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
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

        /// <summary>
        /// Joins the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
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

        /// <summary>
        /// Moves the action.
        /// </summary>
        /// <param name="direction">The direction.</param>
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


        /// <summary>
        /// Closes the specified winner player.
        /// </summary>
        /// <param name="winnerPlayer">The winner player.</param>
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
                mpModel.Close(gameToBeDeleted);
                this.Clients.Client(otherPlayer).close();
            }
        }
    }
}
