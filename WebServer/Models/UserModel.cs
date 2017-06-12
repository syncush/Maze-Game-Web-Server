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
        private static DataTable dataTable;
        private static Boolean isInit = false;
        private static DataSet ds;
        private static OleDbCommand dCommand;
        private static OleDbConnection con;
        private static OleDbDataAdapter adapter;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel() {
            if(!isInit)
            {
                try
                {
                    ds = new DataSet();
                    dataTable = new DataTable("Users");

                    con = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\sync\Desktop\WebServer\WebServer\MazeGameDB.mdb");
                    dCommand = new OleDbCommand("Select * from Users", con);
                    adapter = new OleDbDataAdapter(dCommand);

                    adapter.Fill(dataTable);
                    ds.Tables.Add(dataTable);
                    isInit = true;
                    // con.Close();
                }
                catch (Exception e)
                {

                }

            }
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public Boolean Register(NewUser user) {
            if(Exist(user.UserName)) {
                return false;
            } else {
                DataRow newRow = dataTable.NewRow();
                newRow["Username"] = user.UserName;
                newRow["Password"] = user.Password;
                newRow["Email"] = user.Email;
                newRow["Wins"] = 0;
                newRow["Loses"] = 0;
                dataTable.Rows.Add(newRow);
                adapter.Update(ds, dataTable.TableName);
                return true;
            }
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Boolean Login(UserLoginData user) {
            foreach(DataRow dr in dataTable.Rows)
            {
                if(dr["Username"].ToString() == user.UserName &&
                    dr["Password"].ToString() == user.Password)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean Exist(string userName)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr["Username"].ToString() == userName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
