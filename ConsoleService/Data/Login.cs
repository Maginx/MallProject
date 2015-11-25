using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MallHost
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DialogResult d = ((Button)sender).DialogResult;
            if (txtUser.Text.Trim() == ReadXML.Xmls["username"].ToString() && txtPass.Text.Trim() == ReadXML.Xmls["password"].ToString())
            {
                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
            }
        }
    }
}
