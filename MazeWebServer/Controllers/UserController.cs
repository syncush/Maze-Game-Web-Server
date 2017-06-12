using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazeWebServer.Entitys;
using MazeWebServer.Models;
using System.Web.Http;

namespace MazeWebServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso/>
    public class UserController : ApiController {
        
        private UserModel userModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController() {
            this.userModel = new UserModel();
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("api/User/Register")]
        public IHttpActionResult Register(NewUser user) {
            if (this.userModel.Register(user)) {
                return Ok("{}");
            } else
            {
                return BadRequest("{}");
            }
                
        }
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/Login")]
        public IHttpActionResult Login(UserLoginData user)
        {
            if (this.userModel.Login(user)) {
                return Ok("{}");
            }
            else {
                return BadRequest("{}");
            }
        }
    }
}
