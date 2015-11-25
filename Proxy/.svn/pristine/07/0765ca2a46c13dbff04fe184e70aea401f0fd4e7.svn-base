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
            this.wareList.Items.Clear();
            var tmp = GSetting.DataServ.LoadWareinfos();

            if (!tmp.IsValid())
            {
                return;
            }

            tmp.ForEach(t =>
            {
                this.wareList.Items.Add(t);
            });
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

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                return;
            }

            var result = GSetting.DataServ.InsertWare(this.txtContent.Text.Trim());

            if (result)
            {
                MessageBox.Show("已添加");
            }
            else
            {
                MessageBox.Show("添加出现错误，未能添加");
            }
            this.WareSetting_Load(sender, e);
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                return;
            }

            var result = GSetting.DataServ.DeleteWare("wareName", this.txtContent.Text.Trim());

            if (result)
            {
                MessageBox.Show("已删除");
            }
            else
            {
                MessageBox.Show("删除遇到错误，未能删除");
            }
            this.WareSetting_Load(sender, e);
        }

        private void wareList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.wareList.SelectedItems.Count > 0)
            {
                var tmp = this.wareList.SelectedItems[0];
                this.txtContent.Text = tmp.ToSafeString();
            }
         
        }
    }
}
