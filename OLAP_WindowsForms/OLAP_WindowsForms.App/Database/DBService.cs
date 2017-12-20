using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLAP_WindowsForms.App
{
    public class DBService
    {
        private string DBHostname;
        private string DBPort;
        private string DBUsername;
        private string DBPassword;
        private string DBName;

        private DataSet ds = new DataSet(); // current data set
        private DataTable dt = new DataTable(); // copy of data table

        public void LogIn(string hostname, string port, string username, string password, string dbName)
        {
            DBHostname = hostname;
            DBPort = port;
            DBUsername = username;
            DBPassword = password;
            DBName = dbName;
        }




        public DataTable GetAllDataFromTable(string table)
        {

            try
            {
                string connectionString =
                    String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                    DBHostname, DBPort, DBUsername, DBPassword, DBName);

                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string sqlStmt = "SELECT * FROM " + table;
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sqlStmt, connection); // adapter for select statements

                // refresh data set
                ds.Reset();
                dataAdapter.Fill(ds);

                // refresh data table
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
                Environment.Exit(0);
                return null;
            }
        }

        public DataTable GetData(string table, String column1, String column2)
        {

            try
            {
                string connectionString =
                    String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                    DBHostname, DBPort, DBUsername, DBPassword, DBName);

                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string sqlStmt = "SELECT " + column1 + "," + column2 + " FROM " + table;
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sqlStmt, connection); // adapter for select statements

                // refresh data set
                ds.Reset();
                dataAdapter.Fill(ds);

                // refresh data table
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
                Environment.Exit(0);
                return null;
            }
        }

        public DataTable GetData(string table, String column)
        {

            try
            {
                string connectionString =
                    String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                    DBHostname, DBPort, DBUsername, DBPassword, DBName);

                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string sqlStmt = "SELECT " + column + " FROM " + table;
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sqlStmt, connection); // adapter for select statements

                // refresh data set
                ds.Reset();
                dataAdapter.Fill(ds);

                // refresh data table
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
                Environment.Exit(0);
                return null;
            }
        }

        public DataTable GetData(String sqlStmt)
        {

            try
            {
                string connectionString =
                    String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                    DBHostname, DBPort, DBUsername, DBPassword, DBName);

                NpgsqlConnection connection = new NpgsqlConnection(connectionString);

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sqlStmt, connection); // adapter for select statements

                // refresh data set
                ds.Reset();
                dataAdapter.Fill(ds);


                // refresh data table
                dt = ds.Tables[0];
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
                Environment.Exit(0);
                return null;
            }
        }
    }
}
