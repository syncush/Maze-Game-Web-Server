using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class MazeParams
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows { get; set; }
        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols { get; set; }

    }
}
