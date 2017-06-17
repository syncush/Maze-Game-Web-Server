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
    class MultiPlayerInfoPackage
    {
        /// <summary>
        /// The maze
        /// </summary>
        public Maze Maze { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is started; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsStarted { get; set; }

    }
}
