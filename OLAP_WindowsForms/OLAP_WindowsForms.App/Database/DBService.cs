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
                NpgsqlConnection connection = getConnection();
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
                NpgsqlConnection connection = getConnection();
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
                NpgsqlConnection connection = getConnection();
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
                NpgsqlConnection connection = getConnection();

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

        //insert into table
        public void insertInto(String table, String columnPK, String[] column = null, String[] input = null)
        {
            try
            {
                // get new ID
                DataTable dt = DBContext.Service().GetData(
              "SELECT MAX(" + columnPK + ") FROM " + table);

                DataTable dt2 = dt.Copy();
                DataRow[] dr = dt2.Select();

                String id = dr[0].ItemArray[0].ToString();
                int ID = Int32.Parse(id) + 1;
                Console.WriteLine(ID);
                
                // insert
                // build command string
                StringBuilder cmds = new StringBuilder();
                cmds.Append("INSERT INTO "+table+" ("+columnPK);
                if (column != null)
                {
                    for (int i = 0; i < column.Length; i++)
                    {
                        cmds.Append(", " + column[i]);
                    }
                }
                cmds.Append(") VALUES(:pk");
                if (input != null)
                {
                    for (int j = 0; j < input.Length; j++)
                    {
                        cmds.Append(", :c" + j);
                    }
                }
                cmds.Append(")");

                NpgsqlConnection conn = getConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(cmds.ToString(), conn);

                // add primary key
                cmd.Parameters.Add(new NpgsqlParameter(":pk",ID));

                // add rest string
                if (input != null)
                {
                    for (int k = 0; k < input.Length; k++)
                    {
                        cmd.Parameters.Add(new NpgsqlParameter(":c" + k, input[k]));
                        Console.WriteLine(":c" + k +" "+input[k]);
                    }
                }

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();         
                conn.Close();

            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }

        public void delete(String table, String pk_column, String pk_sid)
        {
            try
            {
                NpgsqlConnection conn = getConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + table + " WHERE " + pk_column + " = " + pk_sid, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }

        public NpgsqlConnection getConnection()
        {
            return new NpgsqlConnection(String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                        DBHostname, DBPort, DBUsername, DBPassword, DBName));
        }
    }  
    }

