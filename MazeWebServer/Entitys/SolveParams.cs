using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class SolveParams
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }
        /// <summary>
        /// Gets or sets the algo selector.
        /// </summary>
        /// <value>
        /// The algo selector.
        /// </value>
        public int AlgoSelector { get; set; }
    }
}