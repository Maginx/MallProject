using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Management;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 输入验证
    /// </summary>
    public partial class EnterLicense : KryptonForm
    {
        public EnterLicense()
        {
            InitializeComponent();
        }
        string tempStr = "";
        private void EnterLicense_Load(object sender, EventArgs e)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            string temp = Des.EncryptDES(disk.GetPropertyValue("VolumeSerialNumber").ToString(), Des.ConstKey);
            tempStr = Des.EncryptDES(Des.EncryptDES(disk.GetPropertyValue("VolumeSerialNumber").ToString(), Des.ConstKey), Des.ConstKey);
            this.txtOnlyMsg.Text = temp.Trim();
            this.txtLicense.Text = DealXml.ReadSysConfig("appSettings", "license");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (tempStr == this.txtLicense.Text.Trim())
            {
                this.DialogResult = DialogResult.OK;
                DealXml.ModifySysConfig("license", tempStr);
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.No;
                DealXml.ModifySysConfig("license", string.Empty);
                Environment.Exit(0);
            }
        }
    }
}
