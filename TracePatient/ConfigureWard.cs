using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TracePatient
{
    public partial class ConfigureWardForm : KryptonForm
    {
        private string wardName;
        public string NewWardName
        {
            get { return wardName; }
        }

        public ConfigureWardForm()
        {
            InitializeComponent();
        }

        public ConfigureWardForm(string wardName) : this()
        {
            this.txtWardNo.Text = wardName;
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            this.wardName = this.txtWardNo.Text;
            this.Close();
        }
    }
}
