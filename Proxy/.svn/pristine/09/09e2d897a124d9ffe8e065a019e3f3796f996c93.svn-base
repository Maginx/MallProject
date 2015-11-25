using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Xml.Linq;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 设置清洗时间
    /// </summary>
    public partial class CleanTime : KryptonForm
    {
        public CleanTime()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (DealXml.Remove("stepTime"))
            {
                List<CleanTimeStruct> structs = new List<CleanTimeStruct>();

                foreach (DataGridViewRow row in this.dataGridStepTime.Rows)
                {
                    var cleanTime = new CleanTimeStruct();
                    cleanTime.remark = row.Cells[0].Value.ToString().Trim();
                    cleanTime.time = row.Cells[1].Value.ToString().Trim();
                    cleanTime.stepName = row.Cells[2].Value.ToSafeString();
                    structs.Add(cleanTime);
                }

                if (DealXml.WriteCleanTimeNodesToXml(structs))
                {
                    KryptonMessageBox.Show("保存成功");
                }
            }
        }

        private void CleanTime_Load(object sender, EventArgs e)
        {
            IList<XElement> elements = DealXml.GetNodes("stepTime", "step");

            foreach (XElement element in elements)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridStepTime);
                row.Cells[0].Value = element.Attribute("remark").Value.ToString();
                row.Cells[1].Value = element.Attribute("time").Value.ToString();
                row.Cells[2].Value = element.Attribute("stepName").Value.ToString();
                this.dataGridStepTime.Rows.Add(row);
            }
        }


        /// <summary>
        /// Handles the RowPostPaint event of the dataGridStepTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowPostPaintEventArgs"/> instance containing the event data.</param>
        private void dataGridStepTime_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
             e.RowBounds.Location.Y,
             dataGridStepTime.RowHeadersWidth - 4,
             e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridStepTime.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridStepTime.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var row = new DataGridViewRow();
            row.CreateCells(this.dataGridStepTime);
            this.dataGridStepTime.Rows.Add(row);
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuRemove_Click(object sender, EventArgs e)
        {
            if (this.dataGridStepTime.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow c in this.dataGridStepTime.SelectedRows)
                {
                    this.dataGridStepTime.Rows.Remove(c);
                }
            }
        }


    }
    /// <summary>
    /// 清洗时间设置结构体
    /// </summary>
    public struct CleanTimeStruct
    {
        public string stepName;
        public string time;
        public string remark;
    }
}
