using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.Entitys;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/User")]
    public class UserController : Controller {
        
        private UserModel userModel;
        public UserController() {
            this.userModel = new UserModel();
        }

        [HttpPost("Register")]
        public IActionResult Register(NewUser user) {
           
            UserModel.users.Add(new Tuple<string, string, string>(user.UserName, user.Password, user.Email));
            return Ok();
        }
        [HttpPost("Login")]
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
