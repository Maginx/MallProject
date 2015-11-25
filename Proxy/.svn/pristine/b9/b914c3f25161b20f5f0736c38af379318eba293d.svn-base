using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Xml;
using System.Xml.Linq;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 清洗槽端口设置
    /// </summary>
    public partial class PortSet : KryptonForm
    {
        private IDictionary<string, string> nodeDics = new Dictionary<string, string>();//stepname和remark一一对应

        public IDictionary<string, string> NodeDics
        {
            get { return nodeDics; }
            set { nodeDics = value; }
        }

        public PortSet()
        {
            InitializeComponent();
            this.dgvPortMapping.Font = new Font("宋体", 15);
        }

        private void PortSet_Load(object sender, EventArgs e)
        {
            //加载端口名
            IList<XElement> listPortName = DealXml.GetNodes("portName", "port");
            if (listPortName != null)
            {
                foreach (XElement node in listPortName)
                {
                    if (!nodeDics.Keys.Contains(node.Attribute("stepName").Value.ToString().Trim()))
                    {
                        nodeDics.Add(node.Attribute("stepName").Value.ToString().Trim(), node.Attribute("remark").Value.ToString().Trim());
                    }
                }
            }

            //加载端口映射
            IList<XElement> listPortMapping = DealXml.GetNodes("portMapping", "port");
            if (listPortMapping != null)
            {
                foreach (XElement node in listPortMapping)
                {
                    this.dgvPortMapping.Rows.Add(new DataGridViewRow());
                    bool isAdd = true;
                    foreach (DataGridViewRow row in dgvPortMapping.Rows)
                    {
                        if (node.Attribute("portCode") != null && row.Cells[0].Value != null)
                        {
                            if (row.Cells[0].Value.ToString() == node.Attribute("portCode").Value.ToString())
                            {
                                isAdd = false;
                            }
                        }
                    }
                    if (isAdd)
                    {
                        this.dgvPortMapping.Rows[dgvPortMapping.Rows.Count - 1].Cells[0].Value = node.Attribute("portCode").Value.Trim().ToString();
                        this.dgvPortMapping.Rows[dgvPortMapping.Rows.Count - 1].Cells[1].Value = node.Attribute("remark").Value.Trim().ToString();
                        this.dgvPortMapping.Rows[dgvPortMapping.Rows.Count - 1].Cells[2].Value = node.Attribute("stepName").Value.Trim().ToString();
                    }
                    else
                    {
                        this.dgvPortMapping.Rows.RemoveAt(dgvPortMapping.Rows.Count - 1);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewItem newItem = new AddNewItem(this);
            newItem.ShowDialog();
        }

        private void changePortMessage_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.DgvPortMapping.SelectedRows)
            {
                DgvPortMapping.Rows.Remove(row);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult result = KryptonMessageBox.Show(this, "确定全部删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                this.DgvPortMapping.Rows.Clear();
            }
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            List<PortMapping> nodes = new List<PortMapping>();
            if (DealXml.Remove("portMapping"))
            {
                foreach (DataGridViewRow row in dgvPortMapping.Rows)
                {
                    PortMapping tempNode = new PortMapping();
                    tempNode.PortNo = row.Cells[0].Value == null ? "" : row.Cells[0].Value.ToString();
                    tempNode.MarkText = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                    tempNode.StepName = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();
                    nodes.Add(tempNode);
                }
            }
            if (DealXml.WritePortMappingNodesToXml(nodes))
            {
                KryptonMessageBox.Show("保存成功！");
                this.Close();
            }
        }

        private void PortSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            GSetting.ContentForms.Remove(FormClient.PortSet);
        }
    }

}
