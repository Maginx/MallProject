using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using ProxyClient.Controls;
using ClassLibrary;
using ComponentFactory.Krypton.Toolkit;

namespace ProxyClient
{
    /// <summary>
    /// 病人登记
    /// </summary>
    public partial class PatientRegister : KryptonForm
    {
        public PatientRegister()
        {
            InitializeComponent();
            this.timer = new Timer();
            this.timer.Interval = 10;
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// 登记信息
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPatientNo.Text.Trim()) || string.IsNullOrEmpty(this.txtPatientName.Text.Trim()))
            {
                return;
            }

            var pat = new Patient
            {
                PatientSN = this.txtPatientNo.Text.Trim(),
                Age = this.txtAge.Text.Trim(),
                IsPositive = this.cbkIsPositive.Checked ? true : false,
                Sex = this.chbMale.Checked ? "男" : "女",
                Endoscope = this.txtEndoscope.Text.Trim(),
                PatientAddr = this.txtAddress.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                PatientName = this.txtPatientName.Text.Trim(),
                PatientPhoneNo = this.txtPhone.Text.Trim()
            };

            try
            {
                if (this.RegisterPatient(pat))
                {
                    MessageBox.Show("注册成功！");
                    this.ClearTextMsg();
                    this.PatientRegister_Load(sender, e);
                }
                else
                {
                    KryptonMessageBox.Show("注册失败！");
                }
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("注册失败！");
            }
        }

        /// <summary>
        /// 清空TextBox
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            // 清空信息
            this.ClearTextMsg();
        }

        private void chbFemale_Click(object sender, EventArgs e)
        {
            //this.chbFemale.Checked = true;
            if (this.chbFemale.Checked)
            {
                this.chbMale.Checked = false;
            }
        }

        private void chbMale_Click(object sender, EventArgs e)
        {
            //this.chbMale.Checked = true;
            if (this.chbMale.Checked)
            {
                this.chbFemale.Checked = false;
            }
        }

        /// <summary>
        /// Handles the Load event of the PatientRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PatientRegister_Load(object sender, EventArgs e)
        {
            this.timer.Start();
            this.txtPatientNo.Text = this.GetPatientNo();
            this.mainGroup.ValuesPrimary.Description = Global.LoginedUser + "(" + Global.LoginedUserRole.GetAttributeName<UserRole>() + ")";

            // 加载病人信息
            this.LoadPatients();

            // 绑定数据到界面
            this.BingingToList();
        }

        /// <summary>
        /// Gets the patient no.(获取随机编号)
        /// </summary>
        /// <returns>编号</returns>
        private string GetPatientNo()
        {
            var str = "P{0}";
            var time = Math.Abs((DateTime.Now.ToString().GetHashCode())).ToString();
            return string.Format(str, time);
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.PatientRegister_Load(sender, e);
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
                return;
            }

            try
            {
                this.Patients.ForEach(p =>
                 {
                     var temp = this.lsbPatient.SelectedItem.ToString().Split('[').Last().Split(']').FirstOrDefault().Trim();

                     if (!string.Equals(p.PatientSN, temp, StringComparison.OrdinalIgnoreCase))
                     {
                         return;
                     }
                     if (this.DeletePatient(temp))
                     {
                         this.PatientRegister_Load(sender, e);
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

        /// <summary>
        /// Handles the FormClosing event of the PatientRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void PatientRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtPatientNo_Leave(object sender, EventArgs e)
        {
            KryptonTextBox txt = sender as KryptonTextBox;

            if (txt == null)
            {
                return;
            }

            txt.StateActive.Border.Color1 = Color.Silver;
        }

        private void txtPatientNo_Enter(object sender, EventArgs e)
        {
            KryptonTextBox txt = sender as KryptonTextBox;

            if (txt == null)
            {
                return;
            }

            txt.StateActive.Border.Color1 = Color.LimeGreen;
        }

        private void txtAge_Enter(object sender, EventArgs e)
        {
            KryptonComboBox cb = sender as KryptonComboBox;

            if (cb == null)
            {
                return;
            }

            cb.StateActive.ComboBox.Border.Color1 = Color.LimeGreen;
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            KryptonComboBox cb = sender as KryptonComboBox;

            if (cb == null)
            {
                return;
            }

            cb.StateActive.ComboBox.Border.Color1 = Color.Silver;
        }
    }
}
