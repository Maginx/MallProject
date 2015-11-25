using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lisence
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string temp = DES.ConstKey;
            this.txtAfter.Text = DES.EncryptDES(this.txtBefore.Text.Trim(), temp);
        }
    }
}
