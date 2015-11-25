using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 临时记录
    /// </summary>
    public partial class TempRecord : KryptonForm
    {
        public TempRecord()
        {
            InitializeComponent();
        }

        private void TempRecord_Load(object sender, EventArgs e)
        {

        }


        private void saveTemp_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "文本(*.txt)|*.txt";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = fileDialog.FileName;
                Save(name);
            }
        }

        /// <summary>
        /// 保存临时记录
        /// </summary>
        /// <param name="name">保存名称</param>
        void Save(string name)
        {
            try
            {
                FileStream stream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach (var c in this.richTxtTempRecord.Text)
                    {
                        writer.Write(c);
                    }
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);

            }
        }

        private void kryptonHeaderGroup1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void clearTempRecord_Click(object sender, EventArgs e)
        {
            this.richTxtTempRecord.Text = "";
        }
    }
}
