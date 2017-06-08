using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Entitys;

namespace WebServer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The users
        /// </summary>
        public static List<Tuple<string, string, string>> users = new List<Tuple<string, string, string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel() {
            
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Register(NewUser user) {
            users.Add(new Tuple<string, string, string>(user.UserName, user.Password, user.Email));
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Boolean Login(UserLoginData user) {
            foreach (Tuple<string, string, string> tup in users) {
                if (tup.Item1 == user.UserName && tup.Item2 == user.Password) {
                    return true;
                }
                
            }
            return false;
        }
    }
}
