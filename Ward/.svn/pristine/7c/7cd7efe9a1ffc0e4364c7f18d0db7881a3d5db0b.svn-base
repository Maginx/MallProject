using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ProxyClient
{
    public partial class UpdateEndoscope : Form
    {
        public UpdateEndoscope()
        {
            InitializeComponent();
            btnPatientList_Click(null, null);
            this.LoadDoctsNursList();
            this.LoadWareInfos();
            this.LoadEndoscopeInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.txtPatientSN.Text.Trim().Length <= 0 || this.cmbDoctor.Text.Length <= 0 || this.cmbEndoscope.Text.Trim().Length <= 0 || this.cmbNurses.Text.Trim().Length <= 0 || this.txtPatientSN.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("所有内容都不可以为空！");
                    return;
                }

            }
            catch { }
            bool result = false;

            try
            {
                result = this.UpdateEndoscopeInfo(this.GetPatientIdBySN(this.txtPatientSN.Text));
            }
            catch (Exception ex)
            {
                this.Log(ex);
            }

            if (result)
            {
                MessageBox.Show("更新成功");
                btnPatientList_Click(null, null);
                //this.btnCancel_Click(null, null);
            }
            else
            {
                MessageBox.Show("更新失败");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (this._connection = new SqlConnection(Global.ConnectionStr))
                {
                    this._connection.Open();
                    MessageBox.Show("数据库连接成功！");
                }
            }
            catch (Exception ex)
            {
                this.Log(ex);
                MessageBox.Show("数据库连接失败！");
            }
        }

        private void btnPatientList_Click(object sender, EventArgs e)
        {
            if (IsValid<Patient>(this._patients))
            {
                this._patients.Clear();
            }

            if (this.lsbPatient.Items != null && this.lsbPatient.Items.Count > 0)
            {
                this.lsbPatient.Items.Clear();
            }
            this.LoadPatientList();
            this.BingingToList(this._patients);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cmbDoctor.SelectedIndex = 0;
            this.cmbEndoscope.Text = string.Empty;
            this.cmbNurses.SelectedIndex = 0;
            this.txtPatientName.Text = string.Empty;
            this.txtPatientSN.Text = string.Empty;
            this.cmbWareNos.SelectedIndex = 0;
        }

        private void lsbPatient_SelectedValueChanged(object sender, EventArgs e)
        {
            var temp = this.lsbPatient.SelectedItems;
            if (temp == null || temp.Count == 0) return;
            var obj = temp[0];
            if (obj == null) return;
            try
            {
                var ts = obj.ToString().Split('[').Last().Trim(']');
                this._patients.ForEach(p =>
                {
                    if (p.PatientSN == ts)
                    {
                        this.txtPatientSN.Text = p.PatientSN;
                        this.txtPatientName.Text = p.PatientName;
                    }
                });
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void txtWareNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            WareSetting w = new WareSetting();
            w.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lsbPatient.SelectedItem != null)
            {
                try
                {
                    this._patients.ForEach(p =>
                    {
                        var temp = this.lsbPatient.SelectedItem.ToString().Split('[').Last().Trim(']');
                        if (p.PatientSN == temp)
                        {
                            if (this.DeletePatient(temp))
                            {
                                this._patients.Remove(p);
                                this.lsbPatient.Items.Remove(this.lsbPatient.SelectedItem);
                                MessageBox.Show("病人移除成功");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("该病人已经被删除");
                            }
                        }
                    });

                }
                catch { }
            }
        }
    }
}
