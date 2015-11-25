using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProxyClient
{
    public partial class WareSetting : Form
    {
        public WareSetting()
        {
            InitializeComponent();
        }

        private void WareSetting_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Global.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from wareInfo";
                var reader = cmd.ExecuteReader();
                if (reader == null) return;
                while (reader.Read())
                {
                    try
                    {
                        this.wareList.Items.Add(reader["wareName"].ToString());
                    }
                    catch { }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.wareList.Items != null && this.wareList.Items.Count > 0)
            {
                try
                {
                    var t = this.wareList.Items[0].ToString();
                    if (this.Delete(t))
                    {
                        MessageBox.Show("已经删除！");
                        this.wareList.Items.Remove(t);
                    }
                }
                catch { }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.txtContent.Text.Trim().Length <= 0) return;
            if (this.Insert(this.txtContent.Text.Trim()))
            {
                MessageBox.Show("新增成功");
                this.wareList.Items.Insert(0, this.txtContent.Text.Trim());
            }
        }
    }
}
