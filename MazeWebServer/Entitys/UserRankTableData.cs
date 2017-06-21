using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRankTableData
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the wins.
        /// </summary>
        /// <value>
        /// The wins.
        /// </value>
        public int Wins { get; set; }
        /// <summary>
        /// Gets or sets the losses.
        /// </summary>
        /// <value>
        /// The losses.
        /// </value>
        public int Losses { get; set; }
    }
}