using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Entitys;
using System.Data.OleDb;

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
        private static DataTable dataTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel() {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\sync\Desktop\WebServer\WebServer\MazeGameDB.mdb");
                OleDbCommand dCommand = new OleDbCommand("Select * from Users", con);
                OleDbDataAdapter adapter = new OleDbDataAdapter(dCommand);
                dataTable = new DataTable("Users");
                adapter.Fill(dataTable);
                con.Close();
            } catch(Exception e)
            {

            }
           

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
