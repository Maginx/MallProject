using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;

namespace ProxyClient.Controls
{
    public partial class Login : KryptonForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var tmpStr = GSetting.DataServ.LoginUser(this.txtuserName.Text.Trim(), this.txtPassword.Text.Trim());

                if (string.IsNullOrEmpty(tmpStr))
                {
                    KryptonMessageBox.Show("用户登录错误，请核对账户名和密码");
                    return;
                }

                Global.LoginedUser = tmpStr.Split('|').FirstOrDefault();
                int role = 0;
                int.TryParse(tmpStr.Split('|').LastOrDefault(), out role);
                Global.LoginedUserRole = (ClassLibrary.UserRole)role;

            }
            catch (Exception)
            {
            }

            switch (Global.LoginedUserRole)
            {
                case ClassLibrary.UserRole.Washer:
                case ClassLibrary.UserRole.Nurse:
                case ClassLibrary.UserRole.Dcotor:
                    this.DialogResult = DialogResult.OK;
                    break;
                default:
                    break;
            }

            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtuserName_Leave(object sender, EventArgs e)
        {
            KryptonTextBox txt = sender as KryptonTextBox;

            if (txt == null)
            {
                return;
            }

            txt.StateActive.Border.Color1 = Color.Silver;
        }

        private void txtuserName_Enter(object sender, EventArgs e)
        {
            KryptonTextBox txt = sender as KryptonTextBox;

            if (txt == null)
            {
                return;
            }

            txt.StateActive.Border.Color1 = Color.LimeGreen;
        }
    }
}
