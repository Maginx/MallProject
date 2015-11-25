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
using ProxyClient.Controls;
using ClassLibrary;
using ComponentFactory.Krypton.Toolkit;

namespace ProxyClient
{
    public partial class UpdateEndoscope : KryptonForm
    {
        private string selectedId = null;
        public UpdateEndoscope()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtPatientSN.Text.Trim()) || string.IsNullOrEmpty(this.cmbDoctor.Text.Trim()) || string.IsNullOrEmpty(this.cmbEndoscope.Text.Trim()) || string.IsNullOrEmpty(this.cmbNurses.Text.Trim()) || string.IsNullOrEmpty(this.txtPatientSN.Text.Trim()))
                {
                    KryptonMessageBox.Show("所有内容都不可以为空！");
                    return;
                }

            }
            catch { }

            bool result = false;
            result = GSetting.DataServ.UpdateTracData(this.cmbEndoscope.Text.Trim(), this.cmbWareNos.Text.Trim(), this.cmbDoctor.Text.Trim(), this.txtPatientSN.Text.Trim(), this.txtPatientName.Text.Trim(), this.cmbNurses.Text.Trim(), this.selectedId);

            if (result)
            {
                KryptonMessageBox.Show("更新成功");
                this.txtPatientName.Text = string.Empty;
                this.txtPatientSN.Text = string.Empty;
                this.txtEndoscope.Text = string.Empty;

            }
            else
            {
                KryptonMessageBox.Show("更新失败");
            }

            this.btnPatientList_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the btnPatientList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPatientList_Click(object sender, EventArgs e)
        {
            if (this.Patients != null)
            {
                Patients.Clear();
            }

            if (this.lsbPatient.Items != null && this.lsbPatient.Items.Count > 0)
            {
                this.lsbPatient.Items.Clear();
            }

            this.UpdateEndoscope_Load(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cmbDoctor.SelectedIndex = 0;
            this.cmbEndoscope.Text = string.Empty;
            this.cmbNurses.SelectedIndex = 0;
            this.txtPatientName.Text = string.Empty;
            this.txtPatientSN.Text = string.Empty;
            this.cmbWareNos.SelectedIndex = 0;
            this.txtAge.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.txtEndoscope.Text = string.Empty;
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItem2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var w = new WareSetting();
            w.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lsbPatient.SelectedItem == null)
            {
                KryptonMessageBox.Show("请选择删除列");
                return;
            }
            {
                try
                {
                    this.Patients.ForEach(p =>
                    {
                        var temp = this.lsbPatient.SelectedItem.ToString().Split('[').Last().Trim(']');
                        if (!string.Equals(p.PatientSN, temp, StringComparison.OrdinalIgnoreCase))
                        {
                            return;
                        }

                        if (this.DeletePatient(temp))
                        {
                            this.UpdateEndoscope_Load(sender, e);
                           KryptonMessageBox.Show("病人移除成功");
                            return;
                        }
                        else
                        {
                            KryptonMessageBox.Show("该病人已经被删除");
                        }

                    });

                }
                catch { }
            }
        }

        /// <summary>
        /// Handles the Load event of the UpdateEndoscope control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UpdateEndoscope_Load(object sender, EventArgs e)
        {
            this.mainGroup.ValuesPrimary.Description = Global.LoginedUser + "(" + Global.LoginedUserRole.GetAttributeName<UserRole>() + ")";
            this.cmbDoctor.Text = Global.LoginedUser;
            timer.Start();

            // 加载病人信息到界面
            this.LoadPatientList();

            // 加载医生护士
            this.LoadDoctsNursList();

            // 加载检查室
            this.LoadWareInfos();

            // 加载内镜
            this.LoadEndoscopeInfo();
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the lsbPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lsbPatient_SelectedValueChanged(object sender, EventArgs e)
        {
            var temp = this.lsbPatient.SelectedItems;
            if (temp == null || temp.Count == 0) return;
            var obj = temp[0];

            if (obj == null) return;
            try
            {
                var ts = obj.ToString().Split('[').Last().Split(']').FirstOrDefault().Trim();

                this.Patients.ForEach(p =>
                {
                    if (p.PatientSN == ts)
                    {
                        this.selectedId = p.PatientId;
                        this.txtPatientSN.Text = p.PatientSN;
                        this.txtPatientName.Text = p.PatientName;
                        this.txtAge.Text = p.Age;
                        this.txtEndoscope.Text = p.Endoscope;
                        this.cbkIsPositive.Checked = p.IsPositive;
                        this.txtRemark.Text = p.Remark;
                    }
                });
            }
            catch (Exception ex)
            {
                Global.Log(ex);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the UpdateEndoscope control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void UpdateEndoscope_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Environment.Exit(0);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 验证内镜使用是否合格
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            var tmp = this.cmbEndoscope.Text.Trim();

            if (string.IsNullOrEmpty(tmp))
            {
                return;
            }

            var result = GSetting.DataServ.EndoscopeIsQuality(tmp);

            if (result)
            {
                this.labQuality.ForeColor = Color.Green;
                this.labQuality.Text = "合格";
            }
            else
            {
                this.labQuality.ForeColor = Color.Red;
                this.labQuality.Text = "不合格";
            }
        }

        private void cmbEndoscope_Enter(object sender, EventArgs e)
        {
            KryptonComboBox cb = sender as KryptonComboBox;

            if (cb != null)
            {
                cb.StateActive.ComboBox.Border.Color1 = Color.LimeGreen;
            }
        }

        private void cmbEndoscope_Leave(object sender, EventArgs e)
        {
            KryptonComboBox cb = sender as KryptonComboBox;

            if (cb != null)
            {
                cb.StateActive.ComboBox.Border.Color1 = Color.Silver;
            }
        }

        private void txtPatientSN_Enter(object sender, EventArgs e)
        {
            KryptonTextBox cb = sender as KryptonTextBox;

            if (cb != null)
            {
                cb.StateActive.Border.Color1 = Color.LimeGreen;
            }
        }

        private void txtPatientSN_Leave(object sender, EventArgs e)
        {
            KryptonTextBox cb = sender as KryptonTextBox;

            if (cb != null)
            {
                cb.StateActive.Border.Color1 = Color.Silver;
            }
        }

    }
}
