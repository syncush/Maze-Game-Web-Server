using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeWebServer.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLoginData {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}
