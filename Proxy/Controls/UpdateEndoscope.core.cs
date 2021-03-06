﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ClassLibrary;
using ComponentFactory.Krypton.Toolkit;

namespace ProxyClient
{
    public partial class UpdateEndoscope
    {
        #region 私有对象
        /// <summary>
        /// Gets the patients.
        /// </summary>
        /// <value>
        /// The patients.
        /// </value>
        public List<Patient> Patients { get; private set; }

        private Timer timer = null;
        #endregion

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            this.mainGroup.ValuesSecondary.Description = DateTime.Now.ToString();
        }

        #region 加载病人列表
        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <returns>病人列表</returns>
        public void LoadPatientList()
        {
            this.Patients = GSetting.DataServ.LoadPatients();
            this.BingingToList(this.Patients);
        }
        #endregion

        #region  加载护士和医生信息
        /// <summary>
        /// 加载医生护士信息
        /// </summary>
        private void LoadDoctsNursList()
        {
            var doctors = GSetting.DataServ.LoadUserInfos(UserRole.Dcotor);
            this.BindingToCombobox(this.cmbDoctor, doctors);

            var nurses = GSetting.DataServ.LoadUserInfos(UserRole.Nurse);
            this.BindingToCombobox(this.cmbNurses, nurses);
        }
        #endregion

        #region 加载检查室信息
        /// <summary>
        /// Loads the ware infos.
        /// </summary>
        private void LoadWareInfos()
        {
            var wares = GSetting.DataServ.LoadWareinfos();
            this.BindingToCombobox(this.cmbWareNos, wares);
        }
        #endregion

        #region 加载镜子信息
        /// <summary>
        /// Loads the endoscope information.
        /// </summary>
        private void LoadEndoscopeInfo()
        {
            var endos = GSetting.DataServ.LoadEndsocopeNames();
            this.BindingToCombobox(this.cmbEndoscope, endos);
        }
        #endregion

        #region 删除病人信息
        /// <summary>
        /// Deletes the patient.
        /// </summary>
        /// <param name="sn">The sn.</param>
        /// <returns></returns>
        public bool DeletePatient(string sn)
        {
            return GSetting.DataServ.DeletePatients(sn);
        }
        #endregion

        #region  读取病人信息
        /// <summary>
        /// 读取病人信息
        /// </summary>
        /// <param name="reader">reader对象</param>
        /// <returns>病人列表</returns>
        private List<Patient> SqlReaderPatient(SqlDataReader reader)
        {
            List<Patient> patients = null;
            if (reader == null) return null;
            patients = new List<Patient>();
            while (reader.Read())
            {
                var patient = new Patient();
                patient.PatientId = Global.SafeReader(reader["patientId"]);
                patient.PatientSN = Global.SafeReader(reader["patientSN"]);
                patient.PatientSex = Global.SafeReader(reader["sex"]);
                patient.PatientName = Global.SafeReader(reader["patientName"]);
                patient.PatientPhoneNo = Global.SafeReader(reader["patientPhoneNo"]);
                patient.PatientAddr = Global.SafeReader(reader["patientAddress"]);
                patient.Remark = Global.SafeReader(reader["remark"]);
                patient.Endoscope = Global.SafeReader(reader["endoscope"]);
                patient.Age = Global.SafeReader(reader["age"]);
                patient.Sex = Global.SafeReader(reader["sex"]);
                patient.IsPositive = Convert.ToBoolean(Global.SafeReader(reader["positive"]));
                patients.Add(patient);
            }
            return patients;
        }
        #endregion

        #region  绑定数据到界面
        /// <summary>
        /// 绑定链表到界面
        /// </summary>
        /// <param name="patients">列表集合</param>
        /// <param name="listBox">The list box.</param>
        public void BingingToList(List<Patient> patients, ListBox listBox = null)
        {
            if (!this.IsValid<Patient>(patients))
            {
                return;
            }

            string template = "({0}).{1}——[{2}]";

            if (this.lsbPatient.Items == null)
            {
                return;
            }

            this.lsbPatient.Items.Clear();
            var i = 1;

            this.Patients.ForEach(p =>
            {
                this.lsbPatient.Items.Add(string.Format(template, i++, p.PatientName, p.PatientSN));
            });
        }

        /// <summary>
        /// Bingings the automatic combobox.
        /// </summary>
        /// <param name="cmb">The CMB.</param>
        /// <param name="docts">The docts.</param>
        private void BindingToCombobox(KryptonComboBox cmb, List<string> docts)
        {
            if (cmb.Items != null)
            {
                cmb.Items.Clear();
            }

            if (!this.IsValid<string>(docts)) return;

            try
            {
                docts.ForEach(d =>
                {
                    cmb.Items.Add(d);
                });
                if (cmb.Items != null && cmb.Items.Count > 0)
                {
                    cmb.Text = cmb.Items[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Global.Log(ex);
            }
        }
        #endregion




        /// <summary>
        /// 验证是否集合是否合法
        /// </summary>
        /// <typeparam name="T">集合复合类型</typeparam>
        /// <param name="enums">集合对象</param>
        /// <returns>验证结果，合法=true；不合法=false</returns>
        public bool IsValid<T>(IEnumerable<T> enums)
        {
            bool result = false;

            if (enums == null || enums.Count() == 0)
            {
                return result;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
