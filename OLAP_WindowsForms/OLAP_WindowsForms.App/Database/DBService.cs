using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLAP_WindowsForms.App.View;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OLAP_WindowsForms.App
{
    public class DBService
    {
        private string DBHostname;
        private string DBPort;
        private string DBUsername;
        private string DBPassword;
        private string DBName;

        private bool newTransaction = true;
        private int nass_dq_sid;
        private int nass_sc_sid;

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

        // START --- COMBO ITEM HELPER METHODS
        public DataTable GetData(string table, string column1, string column2)
        {
            try
            {
                NpgsqlConnection connection = GetConnection();
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

        public DataTable GetData(string table, string column)
        {

            try
            {
                NpgsqlConnection connection = GetConnection();
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

        public DataTable GetData(string sqlStmt)
        {

            try
            {
                NpgsqlConnection connection = GetConnection();

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
                Console.WriteLine("An error occurred: '{0}'", ex.Message);
                Environment.Exit(0);
                return null;
            }
        }
        
        public string GetStringFromStmt(string stmt, int row, int column)
        {
            return GetData(stmt).Copy().Select()[row].ItemArray[column].ToString();
        }
        // END --- COMBO ITEM HELPER METHODS

        // get new ID
        public int GetLatestID(string columnPK, string table)
        {
            DataTable dt = DBContext.Service().GetData("SELECT MAX(" + columnPK + ") FROM " + table);

            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();

            String index = dr[0].ItemArray[0].ToString();
            if (index.Equals("") || index == null)
            {
                return 1;
            }
            else
            {
                return Int32.Parse(index);
            }
        }

        // Start ------------------------------ LOAD -----------------------------------------------------------------
        public int GetKeyfromTable(string table, int keyValue, string keyColumn, string targetColum)
        {
            try
            {
                DataTable dt = DBContext.Service().GetData(
                  "SELECT " + targetColum +
                  " FROM " + table +
                  " WHERE " + keyColumn + " = " + keyValue);

                DataTable dt2 = dt.Copy();
                DataRow[] dr = dt2.Select();

                String index = dr[0].ItemArray[0].ToString();
                return Int32.Parse(index);
            }catch (Exception e)
            {
                MessageBox.Show("The loaded Analysis-Schema is empty. A new one will be loaded", "Empty Schema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
        }
        public string GetSKeyfromTable(string table, int keyValue, string keyColumn, string targetColum)
        {
            try
            {
                DataTable dt = DBContext.Service().GetData(
                  "SELECT " + targetColum +
                  " FROM " + table +
                  " WHERE " + keyColumn + " = " + keyValue);

                DataTable dt2 = dt.Copy();
                DataRow[] dr = dt2.Select();

                return dr[0].ItemArray[0].ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("The loaded Analysis-Schema is empty. A new one will be loaded", "Empty Schema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
        }
        // END ------------------------------- LOAD -----------------------------------------------------------------

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                        DBHostname, DBPort, DBUsername, DBPassword, DBName));
        }

        //----------------------------------START-INSERTS------------------------------------------------
        //insert into table
        public void InsertInto(NpgsqlConnection connection, NpgsqlTransaction transaction, string table, string columnPK, LinkedList<Insert_item> list)
        {

            NpgsqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;

            // get new ID
            int id = GetLatestID(columnPK, table) + 1;

            // insert
            // build command string
            StringBuilder cmds = new StringBuilder();
            StringBuilder cmds2 = new StringBuilder();

            cmds.Append("INSERT INTO " + table + " (" + columnPK);
            cmds2.Append(") VALUES(:pk");

            int cnt = 1;

            foreach (Insert_item i in list)
            {
                cmds.Append(", " + i.GetColumnName());
                cmds2.Append(", :c" + cnt);
                cnt++;
            }

            cmds.Append(cmds2);
            cmds.Append(")");

            Console.WriteLine(cmds.ToString());

            command.CommandText = cmds.ToString();

            // add primary key
            command.Parameters.Add(new NpgsqlParameter(":pk", id));
            Console.WriteLine(":pk "+id);
            // add rest string
            cnt = 1;
            foreach (Insert_item i in list)
            {
                if (i.IsNull())
                {
                    Console.WriteLine("null value recognized");
                    command.Parameters.Add(new NpgsqlParameter(":c" + cnt, DBNull.Value));
                    Console.WriteLine("null value inserted");
                }
                else
                {
                    if (i.IntValue())
                    {
                        Console.WriteLine(":c" + cnt + " " + i.GetIValue());
                        command.Parameters.Add(new NpgsqlParameter(":c" + cnt, i.GetIValue()));
                    }
                    else
                    {
                        Console.WriteLine(":c" + cnt + " " + i.GetSValue());
                        command.Parameters.Add(new NpgsqlParameter(":c" + cnt, i.GetSValue()));
                    }
                }
                cnt++;
            }
            command.ExecuteNonQuery();
            Console.WriteLine("Command sucessful");
        }
        
        //insert into table -> PK provided as first item in list
        public void InsertWithoutPK(NpgsqlConnection connection, NpgsqlTransaction transaction, string table, LinkedList<Insert_item> list)
        {
            NpgsqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;

            StringBuilder cmds = new StringBuilder();
            StringBuilder cmds2 = new StringBuilder();

            cmds.Append("INSERT INTO " + table + " (");
            cmds2.Append(") VALUES(");

            int cnt = 1;

            foreach (Insert_item i in list)
            {
                cmds.Append(i.GetColumnName() + " ,");
                cmds2.Append(":c" + cnt + " ,");
                cnt++;
            }

            cmds.Remove(cmds.Length - 1, 1);
            cmds2.Remove(cmds2.Length - 1, 1);

            cmds.Append(cmds2);
            cmds.Append(")");

            Console.WriteLine(cmds.ToString());

            command.CommandText = cmds.ToString();

            // add rest string
            cnt = 1;
            foreach (Insert_item i in list)
            {
                if (i.IsNull())
                {
                    Console.WriteLine("null value recognized");
                    command.Parameters.Add(new NpgsqlParameter(":c" + cnt, DBNull.Value));
                    Console.WriteLine("null value inserted");
                }
                else
                {
                    if (i.IntValue())
                    {
                        Console.WriteLine("Int :c" + cnt + i.GetIValue());
                        command.Parameters.Add(new NpgsqlParameter(":c" + cnt, i.GetIValue()));
                    }
                    else
                    {
                        Console.WriteLine("String :c" + cnt + i.GetSValue());
                        command.Parameters.Add(new NpgsqlParameter(":c" + cnt, i.GetSValue()));
                    }

                }
                cnt++;
            }
            command.ExecuteNonQuery();
            Console.WriteLine("Command sucessful");
        }

        // insert into ags_nass_dim_qual and ags_nass_dim_qual_slice from schema
        public void InsertDimQual(NpgsqlConnection connection, NpgsqlTransaction transaction, ComboBox cdw, ComboBox cdw_gl, TextBox tdw, ListBox ldw, int ass_sid, int dim_sid, String defSC, CheckBox dl, CheckBox dn, CheckBox sc, CheckBox gl)
        {
            if (newTransaction)
            {
                // get max IDs
                this.nass_dq_sid = GetLatestID("Nass_DQ_SID", "AGS_NASS_DIM_QUAL") + 1;
                this.nass_sc_sid = GetLatestID("NASS_SC_SID", "AGS_NASS_DIM_QUAL_SLICE_COND") + 1;
                this.newTransaction = false;
            }
            else
            {
                this.nass_dq_sid += 1; // raise pk from table
                this.nass_sc_sid += 1;
            }

            LinkedList<Insert_item> list = new LinkedList<Insert_item>();
            list.AddFirst(new Insert_item("Nass_DQ_SID", nass_dq_sid));
            list.AddLast(new Insert_item("ASS_SID_NASS", ass_sid));
            list.AddLast(new Insert_item("DIM_SID", dim_sid));

            if (dl.Checked)
            {
                DataTable vdt = DBContext.Service().GetData(
                "select lvl_sid from dw_level where dim_sid = " + dim_sid + " and lvl_sid < 0");

                DataTable vdt2 = vdt.Copy();
                DataRow[] vdr = vdt2.Select();
                String sdl = vdr[0].ItemArray[0].ToString();
                int idl = Int32.Parse(sdl);

                list.AddLast(new Insert_item("LVL_SID_DICELVL", idl));
            }
            else if (cdw.SelectedValue != null)
            {
                list.AddLast(new Insert_item("LVL_SID_DICELVL", Int32.Parse(cdw.SelectedValue.ToString())));
            }
            else
            {
                list.AddLast(new Insert_item("LVL_SID_DICELVL", null));
            }

            if (dn.Checked)
            {
                list.AddLast(new Insert_item("NASS_DQ_DICE_NODE", ""));
            }
            else if (tdw.Text.Length > 0)
            {
                list.AddLast(new Insert_item("NASS_DQ_DICE_NODE", tdw.Text));
            }
            else
            {
                list.AddLast(new Insert_item("NASS_DQ_DICE_NODE", null));
            }

            if (gl.Checked)
            {
                DataTable vdt = DBContext.Service().GetData(
                "select lvl_sid from dw_level where dim_sid = " + dim_sid + " and lvl_sid < 0");

                DataTable vdt2 = vdt.Copy();
                DataRow[] vdr = vdt2.Select();
                String sdl = vdr[0].ItemArray[0].ToString();
                int idl = Int32.Parse(sdl);

                list.AddLast(new Insert_item("LVL_SID_GRANLVL", idl));
            }
            else if (cdw_gl.SelectedValue != null)
            {
                list.AddLast(new Insert_item("LVL_SID_GRANLVL", Int32.Parse(cdw_gl.SelectedValue.ToString())));
            }
            else
            {
                list.AddLast(new Insert_item("LVL_SID_GRANLVL", null));
            }

            DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_DIM_QUAL", list);

            // insert slice cond -> ags_nass_dim_qual_slice
            list.Clear();
            if (sc.Checked)
            {
                Console.WriteLine("entered_sc_variable -> Var");

                DataTable sdt = DBContext.Service().GetData(
                "select * from dw_dim_predicate " +
                "where dim_pred_name = '" + defSC + "Var'");

                //Console.WriteLine("query sucess");
                DataTable sdt2 = sdt.Copy();
                DataRow[] sdr = sdt2.Select();
                String pred = sdr[0].ItemArray[0].ToString();
                //Console.WriteLine("query sucess: -> " + pred);
                int dim_pred = Int32.Parse(pred);

                list.AddFirst(new Insert_item("NASS_SC_SID", nass_sc_sid));
                list.AddLast(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                list.AddLast(new Insert_item("DIM_PRED_SID", dim_pred));
                DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_DIM_QUAL_SLICE_COND", list);

            }
            else if (ldw.SelectedValue != null)
            {
                Console.WriteLine("entered slice: ");
                Console.WriteLine(ldw.SelectedValue.ToString());

                foreach (Object sel in ldw.SelectedItems)
                {
                    Console.WriteLine((sel as DataRowView)["DIM_PRED_SID"]);
                    list.AddFirst(new Insert_item("NASS_SC_SID", nass_sc_sid));
                    list.AddLast(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                    list.AddLast(new Insert_item("DIM_PRED_SID", Int32.Parse((sel as DataRowView)["DIM_PRED_SID"].ToString())));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_DIM_QUAL_SLICE_COND", list);
                    this.nass_sc_sid += 1;// raise pk 
                    list.Clear();
                }
            }
            else
            {
                Console.WriteLine("entered slice empty --- def: " + defSC);

                DataTable sdt = DBContext.Service().GetData(
              "select dim_pred_sid from dw_dim_predicate where dim_pred_name like '%true" + defSC + "%'");

                //Console.WriteLine("query sucess");
                DataTable sdt2 = sdt.Copy();
                DataRow[] sdr = sdt2.Select();
                String pred = sdr[0].ItemArray[0].ToString();
                //Console.WriteLine("query sucess: -> " + pred);
                int dim_pred = Int32.Parse(pred);


                //Console.WriteLine("dim_pred_empty: " + dim_pred);

                // insert
                list.AddFirst(new Insert_item("NASS_SC_SID", nass_sc_sid));
                list.AddLast(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                list.AddLast(new Insert_item("DIM_PRED_SID", dim_pred));
                DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_DIM_QUAL_SLICE_COND", list);

            }
        }

        public void Delete(string table, string pk_column, string pk_sid)
        {
            try
            {
                NpgsqlConnection conn = GetConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + table + " WHERE " + pk_column + " = " + pk_sid, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("An Error occured while trying to delete the schema! \n"+e.Message, "Deletion error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TransactionComplete()
        {
            this.newTransaction = true;
        }
    }
}

