using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Gets or sets the host identifier.
        /// </summary>
        /// <value>
        /// The host identifier.
        /// </value>
        public string HostID { get; set; }
        /// <summary>
        /// Gets or sets the guest identifier.
        /// </summary>
        /// <value>
        /// The guest identifier.
        /// </value>
        public string GuestID { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo"/> class.
        /// </summary>
        public GameInfo()
        {

        }
    }
}