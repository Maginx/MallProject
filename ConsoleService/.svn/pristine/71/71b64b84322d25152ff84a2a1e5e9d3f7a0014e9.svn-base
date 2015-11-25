using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace MallHost
{
    /// <summary>
    /// XML操作
    /// </summary>
    internal sealed class ReadXML
    {
        #region 私有属性
        /// <summary>
        /// Config 设置路径
        /// </summary>
        private static string ConfigSettingPath;

        /// <summary>
        /// Config 内容
        /// </summary>
        private static string ConfigContent;

        #endregion

        #region 共有属性
        /// <summary>
        /// 设置 参数键值对
        /// </summary>
        public static IDictionary<string, string> Xmls { get; set; }
        #endregion

        #region 构造方法
        static ReadXML()
        {
            Xmls = new Dictionary<string, string>();
            ConfigSettingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.mp");//获取上两级目录
            ConfigContent = ReadFile();
        }
        #endregion

        #region 共有方法
        /// <summary>
        /// 读取XML文件
        /// </summary>
        public static void ReadXMLService()
        {
            try
            {
                XElement root = XElement.Parse(ConfigContent);
                foreach (var e in root.Elements())
                {
                    if (!Xmls.Keys.Contains(e.Name.ToString()))
                    {
                        Xmls.Add(e.Name.ToString(), (e.Attribute("value").Value.ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)//文件出现异常 则将其删除，并且初始化
            {
                ConsoleHelper.Message(ex.Message);
                File.Delete(ConfigSettingPath);
                ConfigContent = ReadFile();
                ReadXMLService();
            }
        }

        /// <summary>
        /// 写入XML文件
        /// </summary>
        /// <returns>写入结果</returns>
        public static bool WriteXMLService(string nodeKey, string value)
        {
            bool result = false;
            try
            {
                XElement root = XElement.Parse(ConfigContent);
                foreach (var e in root.Elements())
                {
                    if (e.Name.ToString().Trim() == nodeKey)
                    {
                        e.SetAttributeValue("value", value);
                        return Save(root.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Message(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns>保存结果</returns>
        public static bool Save(string content)
        {
            try
            {
                ConfigContent = content;

                if (File.Exists(ConfigSettingPath))
                {
                    File.Delete(ConfigSettingPath);//删除原来旧文件
                }

                using (var writer = new StreamWriter(ConfigSettingPath))
                {
                    writer.WriteLine(ConfigContent);//写入新配置信息
                    return true;
                }
            }
            catch (Exception ex)
            {
                Global.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// 初始化Config文件
        /// </summary>
        public static void InitialConfig()
        {
            string temp = @"<?xml version='1.0' encoding='utf-8'?>
                            <configuration>
		                        <mallProxy value='http://localhost:8887/Service' />
		                        <mallPManager value='http://localhost:8886/Manager' />
		                        <username value='admin'/>
		                        <password value='mall'/>
		                        <SQL value='Data Source=OTTER-PC\OTTER_PC;Initial Catalog=endoscopeARM;Persist Security Info=True;User ID=sa;Password=s@123456'/>
                                <endoscopeInfo value='endoscopeInfo'/>		
                                <endoscopeRecord value='endoscopeRecord'/>
		                        <endoscopeRecordInfo value='endoscopeRecordInfo'/>
		                        <endoscopeTemp value='endoscopeTemp'/>
		                        <user value='userInfo'/>
		                        <amountWork  value='amountWork'/>
                            </configuration>";


            if (!File.Exists(ConfigSettingPath))
            {
                using (var writer = new StreamWriter(ConfigSettingPath))
                {
                    writer.WriteLine(temp);
                }
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <returns>配置文件</returns>
        public static string ReadFile()
        {
            StreamReader stream = null;
            string tempStr = string.Empty;
            try
            {
                //文件不存在
                if (!File.Exists(ConfigSettingPath))
                {
                    //重新创建文件，并初始化
                    InitialConfig();
                }
                stream = new StreamReader(ConfigSettingPath);
                tempStr = stream.ReadToEnd();
                stream.Close();
            }
            catch (Exception ex)
            {
                Global.Log(ex);
            }
            return tempStr;
        }
        #endregion
    }
}
