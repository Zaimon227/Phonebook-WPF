using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Registration
{
    public class DatabaseControl
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-S6G2PFTV\\MSSQLSERVER2022;Initial Catalog=phonebookCRUD;Integrated Security=True; Pooling=False");
        SqlCommand cmd;
        public SqlDataAdapter da;
        List<SqlParameter> Params = new List<SqlParameter>();
        public DataTable dt;
        public int recordcount;
        public string exception;

        public void query(string query)
        {
            recordcount = 0;
            exception = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                Params.ForEach(p => cmd.Parameters.Add(p));
                Params.Clear();
                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                recordcount = da.Fill(dt);
            }
            catch (Exception e) 
            {
                exception = "Problem : " + e.Message;
            }
            finally 
            { 
                if (conn.State==ConnectionState.Open)
                {
                    conn.Close();
                }
            }   
        }

        public void Param(string name, object value) 
        { 
            SqlParameter newparam = new SqlParameter(name, value);
            Params.Add(newparam);
        }

        public bool CheckForError(bool err = false)
        {
            if (string.IsNullOrEmpty(exception))
            {
                return false;
            }
            if (err==true)
            {
                MessageBox.Show(exception, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }
    }
}
