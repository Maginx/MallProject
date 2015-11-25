using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 数据追溯
    /// </summary>
    internal partial class DataTrace
    {
        #region 私有方法

        /// <summary>
        /// 选择显示数据列
        /// </summary>
        private void ChoiceShowData()
        {
            if (toolStripTraceOnly.Checked)
            {
                foreach (DataGridViewTextBoxColumn c in this.dataGridViewTrace.Columns)
                {
                    if (c.HeaderText == "检查室号")
                    {
                        c.Visible = true;
                    }

                    if (c.HeaderText == "前个病人")
                    {
                        c.Visible = true;
                    }

                    if (c.HeaderText == "病人名")
                    {
                        c.Visible = true;
                    }

                    if (c.HeaderText == "医生名")
                    {
                        c.Visible = true;
                    }

                    if (c.HeaderText == "护士名")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (DataGridViewTextBoxColumn c in this.dataGridViewTrace.Columns)
                {
                    if (c.HeaderText == "检查室号")
                    {
                        c.Visible = false;
                    }

                    if (c.HeaderText == "前个病人")
                    {
                        c.Visible = false;
                    }

                    if (c.HeaderText == "病人名")
                    {
                        c.Visible = false;
                    }

                    if (c.HeaderText == "医生名")
                    {
                        c.Visible = false;
                    }

                    if (c.HeaderText == "护士名")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 根据清洗类型设置数据列
        /// </summary>
        /// <param name="flag">清洗类型</param>
        private void SetDataGridViewColums(int flag)
        {
            this.dataGridViewTrace.Columns.Clear();
            var endoscopeSN = new DataGridViewTextBoxColumn();
            endoscopeSN.HeaderText = "内镜编号";
            this.dataGridViewTrace.Columns.Add(endoscopeSN);

            var washerRealName = new DataGridViewTextBoxColumn();
            washerRealName.HeaderText = "清洗工";
            this.dataGridViewTrace.Columns.Add(washerRealName);

            var washDate = new DataGridViewTextBoxColumn();
            washDate.HeaderText = "日期";
            this.dataGridViewTrace.Columns.Add(washDate);

            var autoCleanNo = new DataGridViewTextBoxColumn();
            autoCleanNo.HeaderText = "清洗机编号";
            this.dataGridViewTrace.Columns.Add(autoCleanNo);

            var wareNo = new DataGridViewTextBoxColumn();
            wareNo.HeaderText = "检查室号";
            this.dataGridViewTrace.Columns.Add(wareNo);

            var doctorName = new DataGridViewTextBoxColumn();
            doctorName.HeaderText = "医生名";
            this.dataGridViewTrace.Columns.Add(doctorName);

            var patientNo = new DataGridViewTextBoxColumn();
            patientNo.HeaderText = "病人名";
            this.dataGridViewTrace.Columns.Add(patientNo);

            var qualified = new DataGridViewTextBoxColumn();
            qualified.HeaderText = "清洗是否合格";
            qualified.Visible = false;
            this.dataGridViewTrace.Columns.Add(qualified);

            var washTotalTime = new DataGridViewTextBoxColumn();
            washTotalTime.HeaderText = "总时间";
            this.dataGridViewTrace.Columns.Add(washTotalTime);

            var washBeginTime = new DataGridViewTextBoxColumn();
            washBeginTime.HeaderText = "开始";
            this.dataGridViewTrace.Columns.Add(washBeginTime);

            var washEndTime = new DataGridViewTextBoxColumn();
            washEndTime.HeaderText = "结束";
            this.dataGridViewTrace.Columns.Add(washEndTime);

            if (flag == 1)
            {
                var brushWashTime = new DataGridViewTextBoxColumn();
                brushWashTime.HeaderText = "刷洗";
                this.dataGridViewTrace.Columns.Add(brushWashTime);

                var firstWashTime = new DataGridViewTextBoxColumn();
                firstWashTime.HeaderText = "初洗";
                this.dataGridViewTrace.Columns.Add(firstWashTime);

                var enzymeWashTime = new DataGridViewTextBoxColumn();
                enzymeWashTime.HeaderText = "酶洗";
                this.dataGridViewTrace.Columns.Add(enzymeWashTime);

                var cleanOutWashTime = new DataGridViewTextBoxColumn();
                cleanOutWashTime.HeaderText = "清洗";
                this.dataGridViewTrace.Columns.Add(cleanOutWashTime);

                var dipInWashTime = new DataGridViewTextBoxColumn();
                dipInWashTime.HeaderText = "消毒";
                this.dataGridViewTrace.Columns.Add(dipInWashTime);

                var lastWashTime = new DataGridViewTextBoxColumn();
                lastWashTime.HeaderText = "末洗";
                this.dataGridViewTrace.Columns.Add(lastWashTime);
            }
            else
            {
                var dipInWashTimeSec = new DataGridViewTextBoxColumn();
                dipInWashTimeSec.HeaderText = "消毒";
                this.dataGridViewTrace.Columns.Add(dipInWashTimeSec);

                var lastWashTimeSec = new DataGridViewTextBoxColumn();
                lastWashTimeSec.HeaderText = "末洗";
                this.dataGridViewTrace.Columns.Add(lastWashTimeSec);
            }

            var nurseNo = new DataGridViewTextBoxColumn();
            nurseNo.HeaderText = "护士名";
            this.dataGridViewTrace.Columns.Add(nurseNo);

            var prePatientNo = new DataGridViewTextBoxColumn();
            prePatientNo.HeaderText = "前个病人";
            this.dataGridViewTrace.Columns.Add(prePatientNo);
        }

        /// <summary>
        /// 组装成DataTable
        /// </summary>
        /// <param name="xml">xml内容</param>
        /// <param name="flag">清洗类型</param>
        private void GetTable(string xml, int flag)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                List<XElement> elements = null;

                // 一次清洗
                if (flag == 1)
                {
                    elements = doc.Root.Elements("NewDataSet").Elements("FirstWash").ToList();
                }
                else
                {
                    elements = doc.Root.Elements("NewDataSet").Elements("SecondWash").ToList();
                }

                // 将XML文档转换成DataTable
                this.XmlToDataTable(flag, elements);
            }
            catch
            {
            }

            if (flag == 1)
            {
                this.groupBoxTrace.Values.Description = "一次清洗：共查询" + this.dataGridViewTrace.Rows.Count + "行数据，其中" + this.wrongData + "行清洗不合格";
                this.wrongData = 0;
            }
            else
            {
                this.groupBoxTrace.Values.Description = "二次清洗：共查询" + this.dataGridViewTrace.Rows.Count + "行数据，其中" + this.wrongData + "行清洗不合格";
                this.wrongData = 0;
            }
        }

        /// <summary>
        /// XMLs the automatic data table.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <param name="elements">The elements.</param>
        private void XmlToDataTable(int flag, List<XElement> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                try
                {
                    XElement e = elements[i];
                    this.dataGridViewTrace.Rows.Add();
                    int index = this.dataGridViewTrace.Rows.Count - 1;
                    this.dataGridViewTrace.Rows[index].Cells[0].Value = e.Element("endoscopeSn").Value.ToSafeString();
                    this.dataGridViewTrace.Rows[index].Cells[1].Value = e.Element("washerName").Value.ToSafeString();
                    this.dataGridViewTrace.Rows[index].Cells[2].Value = e.Element("washDate").Value;
                    this.dataGridViewTrace.Rows[index].Cells[3].Value = this.CheckCleanAutoNo(e.Element("autoCleanNo").Value.ToSafeString());
                    this.dataGridViewTrace.Rows[index].Cells[4].Value = e.Element("wareNo").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[5].Value = e.Element("doctorName").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[6].Value = e.Element("patientNo").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[7].Value = e.Element("qualified").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[8].Value = e.Element("washTotalTime").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[9].Value = e.Element("washBeginTime").GetValue();
                    this.dataGridViewTrace.Rows[index].Cells[10].Value = e.Element("washEndTime").GetValue();

                    // 不合格的数据显示色调
                    if (this.dataGridViewTrace.Rows[index].Cells[7].Value.ToString().Trim() == "0")
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.LightPink;
                        this.dataGridViewTrace.Rows[index].DefaultCellStyle = style;
                        this.wrongData++;
                    }

                    // 一次清洗
                    if (flag == 1)
                    {
                        this.dataGridViewTrace.Rows[index].Cells[11].Value = e.Element("brushWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[12].Value = e.Element("firstWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[13].Value = e.Element("enzymeWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[14].Value = e.Element("cleanOutWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[15].Value = e.Element("dipInWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[16].Value = e.Element("lastWashTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[17].Value = e.Element("nurseNo").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[18].Value = e.Element("prePatientNo").GetValue();
                    }
                    else
                    {
                        this.dataGridViewTrace.Rows[index].Cells[11].Value = e.Element("dipInWashSecTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[12].Value = e.Element("lastWashSecTime").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[13].Value = e.Element("nurseNo").GetValue();
                        this.dataGridViewTrace.Rows[index].Cells[14].Value = e.Element("prePatientNo").GetValue();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 检查机洗或者手洗
        /// </summary>
        /// <param name="no">编号</param>
        /// <returns>
        /// 清洗编号
        /// </returns>
        private string CheckCleanAutoNo(string no)
        {
            string temp = "手洗";

            try
            {
                IList<XElement> listPortMapping = DealXml.GetNodes("portMapping", "port");

                if (listPortMapping == null)
                {
                    return temp;
                }

                foreach (XElement node in listPortMapping)
                {
                    if (!node.Attribute("portCode").Value.Trim().Equals(no))
                    {
                        continue;
                    }

                    if (node.Attribute("remark").Value.Trim().Equals("自动清洗机"))
                    {
                        temp = no + "号清洗机";
                    }
                }
            }
            catch (Exception)
            {
            }

            return temp;
        }

        /// <summary>
        /// 清洗SQL语句
        /// </summary>
        /// <returns>
        /// null
        /// </returns>
        private string CleanSQL()
        {
            string temp = string.Empty;
            var stringBuilder = new StringBuilder("select ");
            var stringBuilderSec = new StringBuilder();
            var stringBuilderFirst = new StringBuilder();

            try
            {
                var doc = XDocument.Load(ColumnSet.path);
                XElement root = doc.Root;

                foreach (XElement e in root.Elements())
                {
                    if (e.Attribute("key").Value.Trim() == "true" && (e.Attribute("type").Value.Trim() == "0"))
                    {
                        stringBuilder.Append("A." + e.Attribute("col").Value.Trim() + ",");
                    }

                    if (e.Attribute("key").Value.Trim() == "true" && (e.Attribute("type").Value.Trim() == "2"))
                    {
                        stringBuilderSec.Append("B." + e.Attribute("col").Value.Trim() + ",");
                    }

                    if (e.Attribute("key").Value.Trim() == "true" && (e.Attribute("type").Value.Trim() == "1"))
                    {
                        stringBuilderFirst.Append("B." + e.Attribute("col").Value.Trim() + ",");
                    }
                }
            }
            catch (Exception)
            {
            }

            return temp;
        }
        #endregion

        #region 格式改变（暂未使用）
        /// <summary>
        /// Handles the Click event of the ToolStripSetColum control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ToolStripSetColum_Click(object sender, EventArgs e)
        {
            ColumnSet set = new ColumnSet();

            if (set.ShowDialog() != DialogResult.None)
            {
                this.dataGridViewTrace.Columns.Clear();
                this.ReadColumnSet();
            }
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        private void ReadColumnSet()
        {
            if (!File.Exists(ColumnSet.path))
            {
                return;
            }

            try
            {
                // 加载XML
                var doc = XDocument.Load(ColumnSet.path);
                XElement root = doc.Root;

                foreach (XElement e in root.Elements())
                {
                    // 二次清洗
                    if (radioBtnSec.Checked)
                    {
                        if (e.Attribute("key").Value.Trim().Equals("true") && (e.Attribute("type").Value.Trim().Equals("2") || e.Attribute("type").Value.Trim().Equals("0")))
                        {
                            DataGridViewColumn col = new DataGridViewColumn();
                            col.HeaderText = e.Value;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            this.dataGridViewTrace.Columns.Add(col);
                        }
                    }
                    else
                    {
                        if (e.Attribute("key").Value.Trim().Equals("true") && (e.Attribute("type").Value.Trim().Equals("1") || e.Attribute("type").Value.Trim().Equals("0")))
                        {
                            DataGridViewColumn col = new DataGridViewColumn();
                            col.HeaderText = e.Value;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            this.dataGridViewTrace.Columns.Add(col);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
