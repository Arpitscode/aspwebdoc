using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webdoc.Models
{
    public class ConnectionManager
    {
        SqlConnection dbcon=null;
        public ConnectionManager()
        {
            dbcon = new SqlConnection(@"Data Source=DESKTOP-TDMDE5Q;Initial Catalog=webdoc;Integrated Security=True;");
        }
        public bool crud(string sql)
        {
            SqlCommand command=new SqlCommand(sql,dbcon);
            if(ConnectionState.Closed==dbcon.State)
                dbcon.Open();
            int n=command.ExecuteNonQuery();
            dbcon.Close();
            return n>0?true:false;
        }
        public DataTable getData(string sql)
        {
            SqlDataAdapter sqd = new SqlDataAdapter(sql, dbcon);
            DataTable dt = new DataTable();
            sqd.Fill(dt);
            return dt;
        }
    }
}