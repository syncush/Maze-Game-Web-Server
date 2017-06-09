using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.Entitys;
using WebServer.Models;

namespace WebServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/User")]
    public class UserController : Controller {
        
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
        [HttpGet("Register")]
        public IActionResult Register(NewUser user) {
            UserModel.users.Add(new Tuple<string, string, string>(user.UserName, user.Password, user.Email));
            return Ok("{}");
        }
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpGet("Login")]
        public IActionResult Login(UserLoginData user)
        {
            if (this.userModel.Login(user)) {
                return Ok();
            }
            else {
                return BadRequest();
            }
            
        }
    }
}
