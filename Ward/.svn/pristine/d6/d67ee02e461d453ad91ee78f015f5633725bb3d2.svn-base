using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProxyClient
{
    public partial class WareSetting
    {
        private bool Delete(string condition)
        {
            using (SqlConnection con = new SqlConnection(Global.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("delete from wareInfo where wareName='{0}'", condition);
                var num = cmd.ExecuteNonQuery();
                if (num > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Insert(string name)
        {
            using (SqlConnection con = new SqlConnection(Global.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("insert into  wareInfo (wareName) values('{0}')", name);
                var num = cmd.ExecuteNonQuery();
                if (num > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
