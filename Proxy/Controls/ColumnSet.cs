using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 设置列
    /// </summary>
    public partial class ColumnSet : KryptonForm
    {
        public ColumnSet()
        {
            InitializeComponent();
        }
        string cleanType = "";
        public static string path = Application.StartupPath + @"\colset.xml";

        private void columnList_Click(object sender, EventArgs e)
        {

        }

        private void columnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (columnList.SelectedItems.Count > 0)
            {
                string item = this.columnList.SelectedItems[columnList.SelectedItems.Count - 1].ToString();
                if (item.Trim().Length > 0)
                {
                    txtColumn.Text = item;
                }
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.txtColumn.Text.Trim().Length > 0)
            {
                int index = columnList.Items.IndexOf(this.txtColumn.Text.Trim());
                if (index < 0)
                {
                    columnList.Items.Add(this.txtColumn.Text.Trim());
                    columnList.SetItemChecked(columnList.Items.Count - 1, true);
                    columnList.SetSelected(columnList.Items.Count - 1, true);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (this.txtColumn.Text.Trim().Length > 0)
            {
                int index = columnList.Items.IndexOf(this.txtColumn.Text.Trim());
                if (index >= 0)
                {
                    columnList.Items.RemoveAt(index);
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (columnList.SelectedItems.Count > 0)
            {
                string temp = columnList.SelectedItems[0].ToString();
                int index = columnList.Items.IndexOf(temp);
                if (index > 0)
                {
                    columnList.Items.Remove(columnList.SelectedItems[0]);
                    columnList.Items.Insert(index - 1, temp);
                    columnList.SetItemChecked(index - 1, true);
                    columnList.SetSelected(index - 1, true);
                }

            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (columnList.SelectedItems.Count > 0)
            {
                string temp = columnList.SelectedItems[0].ToString();
                int index = columnList.Items.IndexOf(temp);
                if (index < columnList.Items.Count - 1)
                {
                    columnList.Items.Remove(columnList.SelectedItems[0]);
                    columnList.Items.Insert(index + 1, temp);
                    columnList.SetSelected(index + 1, true);
                    columnList.SetItemChecked(index + 1, true);
                }
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            if (columnList.SelectedItems.Count > 0)
            {
                string temp = columnList.SelectedItems[0].ToString();
                int index = columnList.Items.IndexOf(temp);
                bool result = columnList.GetItemChecked(index);
                columnList.Items.Remove(columnList.SelectedItems[0]);
                columnList.Items.Insert(0, temp);
                columnList.SetSelected(0, true);
                columnList.SetItemChecked(0, result);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            if (columnList.SelectedItems.Count > 0)
            {
                string temp = columnList.SelectedItems[0].ToString();
                int index = columnList.Items.IndexOf(temp);
                bool result = columnList.GetItemChecked(index);
                columnList.Items.Remove(columnList.SelectedItems[0]);
                columnList.Items.Insert(columnList.Items.Count, temp);
                columnList.SetSelected(columnList.Items.Count - 1, true);
                columnList.SetItemChecked(columnList.Items.Count - 1, result);
            }
        }
        private void ColumnSet_Load(object sender, EventArgs e)
        {

            ReadSet();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveSet())
            {
                this.DialogResult = DialogResult.OK;
                ((TempRecord)GSetting.ContentForms[FormClient.TempRecord]).richTxtTempRecord.Invoke(FormHelper.RichTextBoxMethod, DateTime.Now.ToString() + "：列设置保存成功！", Color.Green);
                ((MainForm)GSetting.ContentForms[FormClient.MainFormText]).statusStripTips.Invoke(FormHelper.TextBoxMethod, DateTime.Now.ToString() + "：列设置保存成功！", Color.Green);
            }
            else
            {
                this.DialogResult = DialogResult.No;
                ((TempRecord)GSetting.ContentForms[FormClient.TempRecord]).richTxtTempRecord.Invoke(FormHelper.RichTextBoxMethod, DateTime.Now.ToString() + "：列设置保存失败！", Color.Red);
                ((MainForm)GSetting.ContentForms[FormClient.MainFormText]).statusStripTips.Invoke(FormHelper.TextBoxMethod, DateTime.Now.ToString() + "：列设置保存失败！", Color.Red);
            }
            this.Close();
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns></returns>
        bool SaveSet()
        {
            bool result = false;
            if (!File.Exists(path))
            {
                Reback();
            }
            try
            {
                XDocument doc = XDocument.Load(path);
                XElement root = doc.Root;
                for (int i = 0; i < columnList.Items.Count; i++)
                {
                    foreach (XElement element in root.Elements())
                    {
                        if (element.Value.Trim() == columnList.Items[i].ToString().Trim())
                        {
                            element.Attribute("key").Value = columnList.GetItemChecked(i).ToString().ToLower();
                        }
                    }
                }
                if (File.Exists(path))
                {
                    File.Delete(path);//删除原来旧文件
                }
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(root.ToString());//写入新配置信息
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 读取配置信息
        /// </summary>
        void ReadSet()
        {
            if (!File.Exists(path))
            {
                Reback();
            }
            try
            {
                XDocument doc = XDocument.Load(path);
                XElement root = doc.Root;
                columnList.Items.Add(root.Element("内镜编号").Value, Convert.ToBoolean(root.Element("内镜编号").Attribute("key").Value));
                columnList.Items.Add(root.Element("内镜SIM卡号").Value, Convert.ToBoolean(root.Element("内镜SIM卡号").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗日期").Value, Convert.ToBoolean(root.Element("清洗日期").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗工编号").Value, Convert.ToBoolean(root.Element("清洗工编号").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗工名").Value, Convert.ToBoolean(root.Element("清洗工名").Attribute("key").Value));
                columnList.Items.Add(root.Element("自动清洗机编号").Value, Convert.ToBoolean(root.Element("自动清洗机编号").Attribute("key").Value));
                columnList.Items.Add(root.Element("检查病室号").Value, Convert.ToBoolean(root.Element("检查病室号").Attribute("key").Value));
                columnList.Items.Add(root.Element("病人名").Value, Convert.ToBoolean(root.Element("病人名").Attribute("key").Value));
                columnList.Items.Add(root.Element("前病人名").Value, Convert.ToBoolean(root.Element("前病人名").Attribute("key").Value));
                columnList.Items.Add(root.Element("医生名").Value, Convert.ToBoolean(root.Element("医生名").Attribute("key").Value));
                columnList.Items.Add(root.Element("护士名").Value, Convert.ToBoolean(root.Element("护士名").Attribute("key").Value));
                columnList.Items.Add(root.Element("总时间").Value, Convert.ToBoolean(root.Element("总时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("开始时间").Value, Convert.ToBoolean(root.Element("开始时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("刷洗开始").Value, Convert.ToBoolean(root.Element("刷洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("刷洗结束").Value, Convert.ToBoolean(root.Element("刷洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("刷洗时间").Value, Convert.ToBoolean(root.Element("刷洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("初洗开始").Value, Convert.ToBoolean(root.Element("初洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("初洗结束").Value, Convert.ToBoolean(root.Element("初洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("初洗时间").Value, Convert.ToBoolean(root.Element("初洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("酶洗开始").Value, Convert.ToBoolean(root.Element("酶洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("酶洗结束").Value, Convert.ToBoolean(root.Element("酶洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("酶洗时间").Value, Convert.ToBoolean(root.Element("酶洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗开始").Value, Convert.ToBoolean(root.Element("清洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗结束").Value, Convert.ToBoolean(root.Element("清洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗时间").Value, Convert.ToBoolean(root.Element("清洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("消毒开始").Value, Convert.ToBoolean(root.Element("消毒开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("消毒结束").Value, Convert.ToBoolean(root.Element("消毒结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("消毒时间").Value, Convert.ToBoolean(root.Element("消毒时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("末洗开始").Value, Convert.ToBoolean(root.Element("末洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("末洗结束").Value, Convert.ToBoolean(root.Element("末洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("末洗时间").Value, Convert.ToBoolean(root.Element("末洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次消毒开始").Value, Convert.ToBoolean(root.Element("二次消毒开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次消毒结束").Value, Convert.ToBoolean(root.Element("二次消毒结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次消毒时间").Value, Convert.ToBoolean(root.Element("二次消毒时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次末洗开始").Value, Convert.ToBoolean(root.Element("二次末洗开始").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次末洗结束").Value, Convert.ToBoolean(root.Element("二次末洗结束").Attribute("key").Value));
                columnList.Items.Add(root.Element("二次末洗时间").Value, Convert.ToBoolean(root.Element("二次末洗时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("结束时间").Value, Convert.ToBoolean(root.Element("结束时间").Attribute("key").Value));
                columnList.Items.Add(root.Element("清洗是否合格").Value, Convert.ToBoolean(root.Element("清洗是否合格").Attribute("key").Value));
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        void Reback()
        {
            string temp = @"<?xml version='1.0' encoding='utf-8' ?>
<root>
  <内镜编号 key='true'>内镜编号</内镜编号>
  <内镜SIM卡号 key='true'>内镜SIM卡号</内镜SIM卡号>
  <清洗日期 key='true'>清洗日期</清洗日期>
  <清洗工编号 key='true'>清洗工编号</清洗工编号>
  <清洗工名 key='true'>清洗工名</清洗工名>
  <自动清洗机编号 key='true'>自动清洗机编号</自动清洗机编号>
  <检查病室号 key='true'>检查病室号</检查病室号>
  <病人名 key='true'>病人名</病人名>
  <前病人名 key='true'>前病人名</前病人名>
  <医生名 key='true'>医生名</医生名>
  <护士名 key='true'>护士名</护士名>
  <总时间 key='true'>总时间</总时间>
  <开始时间 key='true'>开始时间</开始时间>
  <刷洗开始 key='true'>刷洗开始</刷洗开始>
  <刷洗结束 key='true'>刷洗结束</刷洗结束>
  <刷洗时间 key='true'>刷洗时间</刷洗时间>
  <初洗开始 key='true'>初洗开始</初洗开始>
  <初洗结束 key='true'>初洗结束</初洗结束>
  <初洗时间 key='true'>初洗时间</初洗时间>
  <酶洗开始 key='true'>酶洗开始</酶洗开始>
  <酶洗结束 key='true'>酶洗结束</酶洗结束>
  <酶洗时间 key='true'>酶洗时间</酶洗时间>
  <清洗开始 key='true'>清洗开始</清洗开始>
  <清洗结束 key='true'>清洗结束</清洗结束>
  <清洗时间 key='true'>清洗时间</清洗时间>
  <消毒开始 key='true'>消毒开始</消毒开始>
  <消毒结束 key='true'>消毒结束</消毒结束>
  <消毒时间 key='true'>消毒时间</消毒时间>
  <末洗开始 key='true'>末洗开始</末洗开始>
  <末洗结束 key='true'>末洗结束</末洗结束>
  <末洗时间 key='true'>末洗时间</末洗时间>
  <二次消毒开始 key='true'>二次消毒开始</二次消毒开始>
  <二次消毒结束 key='true'>二次消毒结束</二次消毒结束>
  <二次消毒时间 key='true'>二次消毒时间</二次消毒时间>
  <二次末洗开始 key='true'>二次末洗开始</二次末洗开始>
  <二次末洗结束 key='true'>二次末洗结束</二次末洗结束>
  <二次末洗时间 key='true'>二次末洗时间</二次末洗时间>
  <结束时间 key='true'>结束时间</结束时间>
  <清洗是否合格 key='true'>清洗是否合格</清洗是否合格>
</root>
";
//            string temp = @"<?xml version='1.0' encoding='utf-8' ?>
//<root>
//  <SN key='true'>内镜编号</SN>
//  <SIM key='true'>内镜SIM卡号</SIM>
//  <Date key='true'>清洗日期</Date>
//  <washerSN key='true'>清洗工编号</washerSN>
//  <washerName key='true'>清洗工名</washerName>
//  <autoCleanNo key='true'>自动清洗机编号</autoCleanNo>
//  <wareNo key='true'>检查病室号</wareNo>
//  <patientNo key='true'>病人名</patientNo>
//  <prePatientNo key='true'>前病人名</prePatientNo>
//  <doctorName key='true'>医生名</doctorName>
//  <nurseNo key='true'>护士名</nurseNo>
//  <totalTime key='true'>总时间</totalTime>
//  <beginTime key='true'>开始时间</beginTime>
//  <brushWashBegin key='true'>刷洗开始</brushWashBegin>
//  <brushWashEnd key='true'>刷洗结束</brushWashEnd>
//  <brushTime key='true'>刷洗时间</brushTime>
//  <firstWashBegin key='true'>初洗开始</firstWashBegin>
//  <firstWashEnd key='true'>初洗结束</firstWashEnd>
//  <firstTime key='true'>初洗时间</firstTime>
//  <enzymeWashBegin key='true'>酶洗开始</enzymeWashBegin>
//  <enzymeWashEnd key='true'>酶洗结束</enzymeWashEnd>
//  <enzymeTime key='true'>酶洗时间</enzymeTime>
//  <cleanOutBegin key='true'>清洗开始</cleanOutBegin>
//  <cleanOutEnd key='true'>清洗结束</cleanOutEnd>
//  <cleanOutTime key='true'>清洗时间</cleanOutTime>
//  <dipInBegin key='true'>消毒开始</dipInBegin>
//  <dipInEnd key='true'>消毒结束</dipInEnd>
//  <dipInTime key='true'>消毒时间</dipInTime>
//  <lastWashBegin key='true'>末洗开始</lastWashBegin>
//  <lastWashEnd key='true'>末洗结束</lastWashEnd>
//  <lastWashTime key='true'>末洗时间</lastWashTime>
//  <dipInSecBegin key='true'>二次消毒开始</dipInSecBegin>
//  <dipInSecEnd key='true'>二次消毒结束</dipInSecEnd>
//  <dipInSecTime key='true'>二次消毒时间</dipInSecTime>
//  <lastWashSecBegin key='true'>二次末洗开始</lastWashSecBegin>
//  <lastWashSecEnd key='true'>二次末洗结束</lastWashSecEnd>
//  <lastWashSecTime key='true'>二次末洗时间</lastWashSecTime>
//  <endTime key='true'>结束时间</endTime>
//  <qulified key='true'>清洗是否合格</qulified>
//
//</root>
//";
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(temp);
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        /// <summary>
        /// 回复默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReback_Click(object sender, EventArgs e)
        {
            this.columnList.Items.Clear();
            Reback();
            ColumnSet_Load(sender, e);
        }

    }
}
