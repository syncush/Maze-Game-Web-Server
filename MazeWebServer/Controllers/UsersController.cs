using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;
using MazeWebServer.Entitys;

namespace MazeWebServer.Controllers
{
    public class UsersController : ApiController
    {
        private MazeWebServerContext db = new MazeWebServerContext();

      
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("api/User/Register")]
        public IHttpActionResult Register(NewUser user)
        {
            User temp = new User();
            temp.Email = user.Email;
            temp.Password = user.Password;
            temp.Username = user.UserName;
            temp.Wins = 0;
            temp.Loses = 0;
            if (db.Users.Find(user.UserName) == null)
            {
                db.Users.Add(temp);
                db.SaveChanges();
                return Ok("{}");
            }
            else
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
            User temp = db.Users.Find(user.UserName);
            if (temp != null && user.Password == temp.Password)
            {
                return Ok("{}");
            }
            else
            {
                return BadRequest("{}");
            }
        }

        public void UpdateWin(string winner, string loser) {
            User user = db.Users.Find(winner);
            User loserUser = db.Users.Find(loser);
            if (user != null && loserUser != null) {
                user.Wins += 1;
                loserUser.Loses += 1;
                db.SaveChanges();
            }
        }
       
        [HttpPost]
        [Route("api/User/GetRankTable")]
        public IHttpActionResult GetRankTable()
        {
            List<UserRankTableData> tableData = new List<UserRankTableData>();
            IQueryable<User> qryResult = db.Users.OrderByDescending(user => user.Wins - user.Loses);
            List<User> b = qryResult.ToList<User>();
            JArray array = new JArray();
            foreach(User user in b)
            {
                array.Add(JObject.FromObject(user));
            }
            return Ok(array);
        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Username)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Username }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Username == id) > 0;
        }
    }
}