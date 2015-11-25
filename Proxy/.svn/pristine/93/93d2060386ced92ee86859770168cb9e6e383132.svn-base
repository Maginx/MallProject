using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
using System.IO;

namespace ProxyClient.Controls
{
    partial class MainForm
    {
        #region 私有方法
        /// <summary>
        /// 保存临时数据
        /// </summary>
        /// <param name="name">文件名</param>
        private void SaveTemp(string name)
        {
            try
            {
                using (var reader = new StreamReader(GSetting.LogFilePath))
                {
                    using (var writer = new StreamWriter(name, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            string temp = reader.ReadLine();
                            writer.WriteLine(temp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);
            }
        }

        /// <summary>
        /// 选项更改(子项切换)
        /// </summary>
        private void ChangeContentControl<T>() where T : System.Windows.Forms.Form
        {
            Form tempForm = GSetting.GetFormInstance<T>();
            tempForm.MdiParent = this;
            this.contentPanel.Controls.Clear();
            tempForm.StartPosition = FormStartPosition.Manual;
            tempForm.Location = new Point(0, 0);
            tempForm.Size = this.contentPanel.Size;
            tempForm.WindowState = FormWindowState.Maximized;
            this.contentPanel.Controls.Add(tempForm);
            this.statusStripTips.Visible = true;
            tempForm.Show();
        }

        #region 主题设置
        private void yaheiFontMenu_Click(object sender, EventArgs e)
        {
            Font f = new Font("微软雅黑", 9);
            this.Font = f;
        }

        private void blackFontMenu_Click(object sender, EventArgs e)
        {
            Font f = new Font("黑体", 9);
            this.Font = f;
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void systemThemeMune_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void silverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }
        #endregion
        #endregion
    }
}
