using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.ServiceModel.Configuration;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 基础设置
    /// </summary>
    public partial class BasicSetting : KryptonForm
    {
        #region 私有对象
        /// <summary>
        /// IP正则验证
        /// </summary>
        private Regex regexIp;

        /// <summary>
        /// IP字符串
        /// </summary>
        private static string ip;
        #endregion

        public BasicSetting()
        {
            InitializeComponent();
            ip = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            regexIp = new Regex(ip);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SetError(this.serviceIP) && SetError(this.armSocketServer) && SetError(this.selfAddress))
            {
                try
                {
                    var root = XElement.Parse(DealXml.ConfigContent);
                    IEnumerable<XElement> elements = from ele in root.Element("appSettings").Elements("add") select ele;
                    foreach (XElement n in elements)
                    {
                        switch (n.Attribute("key").Value.ToUpper())
                        {
                            case "WCFSERVICE":
                                {
                                    n.Attribute("value").Value = this.serviceIP.Text.Trim();
                                    //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                    //ConfigurationSectionGroup sct = config.SectionGroups["system.serviceModel"];
                                    //ServiceModelSectionGroup serviceModelSectionGroup = sct as ServiceModelSectionGroup;
                                    //ClientSection clientSection = serviceModelSectionGroup.Client;
                                    //foreach (ChannelEndpointElement item in clientSection.Endpoints)
                                    //{
                                    //    item.Address = new Uri(this.serviceIP.Text.Trim());
                                    //}
                                    //config.Save(ConfigurationSaveMode.Modified);
                                    //ConfigurationManager.RefreshSection("system.serviceModel");
                                    break;
                                }
                            case "ARMADDRESS":
                                {
                                    n.Attribute("value").Value = this.armSocketServer.Text.Trim();

                                    break;
                                }
                            case "ARMPORT":
                                {
                                    n.Attribute("value").Value = this.armSocketPort.Text.Trim();
                                    break;
                                }
                            case "SELFADDRESS":
                                {
                                    n.Attribute("value").Value = this.selfAddress.Text.Trim();
                                    break;
                                }
                            case "SELFPORT":
                                {
                                    n.Attribute("value").Value = this.selfPort.Text.Trim();
                                    break;
                                }
                            default: break;
                        }
                    }
                    File.SetAttributes(DealXml.ConfigSettingPath, FileAttributes.Normal);
                    DealXml.Save(root.ToString());
                    File.SetAttributes(DealXml.ConfigSettingPath, FileAttributes.Hidden);
                    KryptonMessageBox.Show("已保存设置");
                }
                catch (Exception ex)
                {
                    FormHelper.RecordLogMsg(ex.Message);
                }
            }
            else
            {
                KryptonMessageBox.Show("输入格式错误！");

            }
        }

        /// <summary>
        /// 设置错误
        /// </summary>
        /// <param name="textBox">TxtBox对象</param>
        /// <returns>错误返回false；否则为true</returns>
        private bool SetError(KryptonMaskedTextBox textBox)
        {
            if (!regexIp.IsMatch(textBox.Text.Trim()))
            {
                textBox.StateCommon.Border.Color1 = Color.Red;
                return false;
            }
            textBox.StateCommon.Border.Color1 = Color.LimeGreen;
            return true;
        }

        /// <summary>
        /// 加载窗体
        /// </summary>am>
        /// <param name="e"></param>
        private void BasicSetting_Load(object sender, EventArgs e)
        {
            try
            {
                var root = XElement.Parse(DealXml.ConfigContent);
                IEnumerable<XElement> appSettings = from el in root.Elements("appSettings").Elements("add") select el;
                foreach (XElement n in appSettings)
                {
                    switch (n.Attribute("key").Value.ToUpper())
                    {
                        case "WCFSERVICE":
                            {
                                this.serviceIP.Text = n.Attribute("value").Value.ToString().Trim();
                                break;
                            }
                        case "ARMADDRESS":
                            {
                                this.armSocketServer.Text = n.Attribute("value").Value.ToString().Trim();
                                break;
                            }
                        case "ARMPORT":
                            {
                                this.armSocketPort.Text = n.Attribute("value").Value.ToString().Trim();
                                break;
                            }
                        case "SELFADDRESS":
                            {
                                this.selfAddress.Text = n.Attribute("value").Value.ToString().Trim();
                                break;
                            }
                        case "SELFPORT":
                            {
                                this.selfPort.Text = n.Attribute("value").Value.ToString().Trim();
                                break;
                            }
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void BasicSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GSetting.ContentForms.Keys.Contains(FormClient.BasicSetting))
            {
                GSetting.ContentForms.Remove(FormClient.BasicSetting);
            }
        }

        /// <summary>
        /// 标记改变
        /// </summary>
        private void MarkedTextChanged(object sender, EventArgs e)
        {
            SetError(sender as KryptonMaskedTextBox);
        }
    }
}