using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// Class desribes a info container for a Player game.
    /// </summary>
    class MultiPlayerInfoPackage {
        /// <summary>
        /// The host socket;
        /// The guest socket;
        /// The maze they host and guest are playing on.
        /// </summary>
        private string host;

        /// <summary>
        /// The guest
        /// </summary>
        private string guest;
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host {
            get { return this.host; }
            set {
                this.host = value;

            }
        }

        /// <summary>
        /// Gets or sets the guest.
        /// </summary>
        /// <value>
        /// The guest.
        /// </value>
        public string Guest {
            get { return this.guest; }
            set {
                this.guest = value;

            }
        }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze {
            get { return this.maze; }
            set { this.maze = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerInfoPackage"/> class.
        /// </summary>
        /// <param name="host">The host. The host socket</param>
        /// <param name="maze">The maze.the maze the Player game is on </param>
        public MultiPlayerInfoPackage(string host, Maze maze) {
            this.Host = host;
            this.maze = maze;
        }
    }
}