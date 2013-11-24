using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace AuthServer.Handler
{
    class SQLCommandHandler
    {

        SqlConnection sqlController = new SqlConnection();

        // This will be called on server load, this will just make sure the connection is established.
        public bool sqlConnect(string user, string pass, string hostname, bool trusted, string dbase, int timeout)
        {
            string trustedBool = "no";

            //The SQL client API does not accepted bool for the "Trusted_Connection" arg, this converts the bool
            if (trusted == true)
            {
                trustedBool = "yes";
            }
            else
            {
                trustedBool = "no";
            }

            // Clea SQL command string for the base of the SQL object.
            string sqlCommandString = "user id=" + user + ";" +
                                       "password=" + pass + ";" +
                                        "server=" + hostname + ";" +
                                       "Trusted_Connection=" + trustedBool + ";" +
                                       "database=" + dbase + ";" +
                                       "connection timeout=" + timeout;

            // Declaring that controller!
            sqlController.ConnectionString = sqlCommandString;

            try
            {
                sqlController.Open();
            }
            catch (Exception err)
            {
                // Just thought you should know this shit failed. 
                MessageBox.Show("SQL - ERROR - Connect :: " + err, "SERVER ERROR::SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public int getIntTable(string TableID, string ColumnID)
        {

            // Temporary value holder
            int returnInt = 0;

            try
            {
                // Starting the SQL controllers to read selected Data.
                SqlDataReader sqlDataController = null;
                SqlCommand sqlCommandController = new SqlCommand("select * from " + TableID, sqlController);

                // Begin to read the data pulled.
                sqlDataController = sqlCommandController.ExecuteReader();

                // While the Controller is still reading
                while (sqlDataController.Read())
                {
                    try
                    {
                        // Convert the selected info into a string to be returned
                        returnInt = Int32.Parse(sqlDataController["ColumnID"].ToString());
                    }
                    catch (Exception err)
                    {
                        // Well Shit!
                        MessageBox.Show("SQL - ERROR - ParseInt :: " + err, "SERVER ERROR::SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            catch (Exception err)
            {
                // Double shit!
                MessageBox.Show("SQL - ERROR - SQLDATACONTROLLER :: " + err, "SERVER ERROR::SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            // Everything worked, here is your value! 
            return returnInt;
        }


        public string getStringTable(string TableID, string ColumnID)
        {
            string dataColumn = ""; 

            try
            {
                // Starting the SQL controllers to read selected Data.
                SqlDataReader sqlDataController = null;
                SqlCommand sqlCommandController = new SqlCommand("select * from " + TableID, sqlController);

                // Begin to read the data pulled.
                sqlDataController = sqlCommandController.ExecuteReader();

                // While the Controller is still reading
                while (sqlDataController.Read())
                {
                        dataColumn = sqlDataController["ColumnID"].ToString();
                }
            }
            catch (Exception err)
            {
                // I hope this doesn't happen, if so, shit!
                MessageBox.Show("SQL - ERROR - SQLDATACONTROLLER :: " + err, "SERVER ERROR::SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "FAIL";
            }

            // No column of data should ever return null. 
            if (dataColumn == null)
            {
                return "FAIL";
            }

            return dataColumn;
        }

        public bool setIntTable(string TableID, string ColumnID, int setValue)
        {
            try
            {
                // Setting up that command for execution. 
                SqlCommand sqlCommandController = new SqlCommand("INSERT INTO "
                    + TableID + "(" + ColumnID + ")"
                    + "Values ('" + setValue + "')"
                    , sqlController);

                // Execution!
                sqlCommandController.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                // Shit x3
                MessageBox.Show("SQL - ERROR - SQLDATACONTROLLER :: " + err, "SERVER ERROR::SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
