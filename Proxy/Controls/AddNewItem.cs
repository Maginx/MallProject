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
    public partial class AddNewItem : KryptonForm
    {
        PortSet portSet = null;
        public AddNewItem(PortSet set)
        {
            portSet = set;
            InitializeComponent();
            foreach (var c in portSet.NodeDics.Values)
            {
                this.txtCleanStep.Items.Add(c);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                portSet.DgvPortMapping.Rows.Add(new DataGridViewRow());
                bool isAdd = true;

                foreach (DataGridViewRow row in portSet.DgvPortMapping.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString() == this.txtPortCode.Text.Trim())
                        {
                            isAdd = false;
                        }
                    }
                }

                if (isAdd)
                {
                    portSet.DgvPortMapping.Rows[portSet.DgvPortMapping.RowCount - 1].Cells[0].Value = this.txtPortCode.Text.Trim();
                    portSet.DgvPortMapping.Rows[portSet.DgvPortMapping.RowCount - 1].Cells[1].Value = this.txtCleanStep.SelectedItem.ToString();
                    foreach (XElement node in DealXml.GetNodes("portName","port"))
                    {
                        if (node.Attribute("remark").Value != null)
                        {
                            if (node.Attribute("remark").Value.Trim() == this.txtCleanStep.SelectedItem.ToString())
                            {
                                portSet.DgvPortMapping.Rows[portSet.DgvPortMapping.RowCount - 1].Cells[2].Value = node.Attribute("stepName").Value.ToString();
                            }
                        }
                    }
                    this.Close();
                }
                else
                {
                    portSet.DgvPortMapping.Rows.RemoveAt(portSet.DgvPortMapping.Rows.Count - 1);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);
            }
        }
    }
}
