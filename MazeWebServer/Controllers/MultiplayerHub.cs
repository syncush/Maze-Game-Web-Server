using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR.Hubs;

namespace MazeWebServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    [HubName("MultiplayerHub")]
    public class MultiplayerHub : Hub
    {
        private ConcurrentDictionary<String, Tuple<string,string>> connectedUsers = new ConcurrentDictionary<string, Tuple<string, string>>();

        /// <summary>
        /// Helloes this instance.
        /// </summary>
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Connect(String name)
        {

            //connectedUsers[name] = Context.ConnectionId;
            String n = Context.ConnectionId;
            Clients.Client(n).GetMaze("nnn");
        }

        public void SendMaze(String Sender, String message, String reciver)
        {
            //String recipientId = connectedUsers[reciver];
            //if (recipientId == null)
            //{
            //    return; //todo handle
           // }
            //Clients.Client(recipientId).GetMaze(message);
        }

        public void GetMoveFromPlayer(string move)
        {
            string senderId = Context.ConnectionId;
            string secondPlayerId = "";
            //todo get othe player id from model
            Clients.Client(secondPlayerId).GetOtherMove(move);
        }

        public void StartMulti(String data)
        {
            string senderId = Context.ConnectionId;
            //todo parse data,and sned it to model with my id
        }

        public void JoinMulti(String name)
        {
            string senderId = Context.ConnectionId;
            //todo parse data,and sned it to model with my id,
            //todo find the game,notify both player,and start the game
        }
        public void notifyMyWin()
        {
            string senderId = Context.ConnectionId;
            string secondPlayerId = "";
            //todo get othe player id from model
            Clients.Client(secondPlayerId).NotifyOtherWin();
        }
    }
}