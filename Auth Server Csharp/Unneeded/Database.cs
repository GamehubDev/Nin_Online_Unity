using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AuthServer
{
    public sealed class Database
    {
        static readonly Database inst = new Database();
        private AuthenticationServer ui;
        static Database() { }
        Database(){}

        public static Database Instance
        {
            get
            {
                return inst;
            }
        }
        public void SetUI(AuthenticationServer ui)
        {
            this.ui = ui;
        }

        public bool CheckConnection()
        {
            int retries = 0;
            while (retries < Constants.MAX_RETRIES) // 5
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection(Constants.DB_SERVER + Constants.DB_PORT + Constants.DB_NAME + Constants.DB_UID + Constants.DB_PWD))
                    {
                        cnn.Open();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is MySqlException)
                    {
                        switch (((MySqlException)ex).Number)
                        {
                            case 0:
                                ui.appendLog("Cannot connect to database server. Contact administrator");
                                break;

                            case 1045:
                                ui.appendLog("Invalid username/password (database), please try again");
                                break;
                                return false;
                        }
                    }
                    retries++;
                    ui.appendLog("Retrying for " + retries + " time...");

                }
            }
            return false;
        }
        //Select statement
        public bool SelectPlayerData(out List<string>[] list, string loginName)
        {
            string query = "SELECT members_pass_salt, members_pass_hash, member_group_id, members_display_name FROM nin_members WHERE members_l_username = '" + loginName + "';";
            //int columnsCounter = what.ToCharArray().Count(x => x == ','); 

            list = new List<string>[4];

            int retries = 0;
            while (retries < Constants.MAX_RETRIES) // 5
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection(Constants.DB_SERVER + Constants.DB_PORT + Constants.DB_NAME + Constants.DB_UID + Constants.DB_PWD))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                        {
                            cnn.Open();
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                list[0] = new List<string>();
                                list[1] = new List<string>();
                                list[2] = new List<string>();
                                list[3] = new List<string>();
                                while (dataReader.Read())
                                {
                                    list[0].Add(dataReader["members_pass_salt"] + "");
                                    list[1].Add(dataReader["members_pass_hash"] + "");
                                    list[2].Add(dataReader["member_group_id"] + "");
                                    list[3].Add(dataReader["members_display_name"] + "");
                                }
                            }
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is MySqlException)
                    {
                        switch (((MySqlException)ex).Number)
                        {
                            case 0:
                                ui.appendLog("Cannot connect to database server...");
                                break;

                            case 1045:
                                ui.appendLog("Invalid username/password (database), please try again");
                                break;
                        }
                    }
                    else
                    {
                        ui.appendLog("There was some problem while checking data for this login: " + loginName);
                    }

                    retries++;
                    ui.appendLog("Retrying for " + retries + " time(+loginName+)...");
                }
            }

            return false;
        }
    }
}
