using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ProxyClient.Controls;

namespace ProxyClient
{
    /// <summary>
    /// XML文档处理类
    /// </summary>
    internal sealed class DealXml
    {
        /// <summary>
        /// Initializes the <see cref="DealXml"/> class.
        /// </summary>
        static DealXml()
        {
            // 设置xml读取路径
            ConfigSettingPath = Application.StartupPath + "\\system.mp";

            // 加载Xml内容到内存中
            ReadDESConfig();
        }

        /// <summary>
        /// 内存临时存储
        /// </summary>
        public static string ConfigContent { get; private set; }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigSettingPath { get; private set; }

        /// <summary>
        /// 读取XML节点
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 读取节点值
        /// </returns>
        public static string ReadSysConfig(string parentNode, string key)
        {
            try
            {
                if (ConfigContent.Length <= 0)
                {
                    ReadDESConfig();
                }

                XElement root = XElement.Parse(ConfigContent);
                XElement temp = root.Element(parentNode);
                IEnumerable<XElement> elements = temp.Elements("add");

                foreach (XElement e in elements)
                {
                    if (e.Attribute("key").Value == key)
                    {
                        return e.Attribute("value").Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// 修改XML节点
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="value">值</param>
        public static void ModifySysConfig(string key, string value)
        {
            XElement root = XElement.Parse(DealXml.ConfigContent);
            IEnumerable<XElement> elements = from ele in root.Element("appSettings").Elements("add") select ele;

            foreach (XElement ex in elements)
            {
                if (ex.Attribute("key").Value == key)
                {
                    // 机洗
                    ex.Attribute("value").Value = value;
                }
            }

            File.SetAttributes(DealXml.ConfigSettingPath, FileAttributes.Normal);
            DealXml.Save(root.ToString());
            File.SetAttributes(DealXml.ConfigSettingPath, FileAttributes.Hidden);
        }

        /// <summary>
        /// 添加XML节点 PortMapping
        /// </summary>
        /// <param name="nodes">文件节点</param>
        /// <returns>成功true，错误false</returns>
        public static bool WritePortMappingNodesToXml(List<PortMapping> nodes)
        {
            try
            {
                if (ConfigContent.Length <= 0)
                {
                    ReadDESConfig();
                }

                XElement root = XElement.Parse(ConfigContent);
                XElement mapping = root.Element("portMapping");

                foreach (PortMapping n in nodes)
                {
                    XElement temp = new XElement("port", new XAttribute("portCode", n.PortNo), new XAttribute("stepName", n.StepName), new XAttribute("remark", n.MarkText));
                    mapping.Add(temp);
                }

                File.SetAttributes(ConfigSettingPath, FileAttributes.Normal);
                Save(root.ToString());
                File.SetAttributes(ConfigSettingPath, FileAttributes.Hidden);
                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// 添加XML节点 CleanTime
        /// </summary>
        /// <param name="nodes">文件节点</param>
        /// <returns>成功true；失败false</returns>
        public static bool WriteCleanTimeNodesToXml(List<CleanTimeStruct> nodes)
        {
            try
            {
                if (ConfigContent.Length <= 0)
                {
                    ReadDESConfig();
                }

                XElement root = XElement.Parse(ConfigContent);
                XElement mapping = root.Element("stepTime");

                foreach (CleanTimeStruct n in nodes)
                {
                    XElement temp = new XElement("step", new XAttribute("stepName", n.stepName), new XAttribute("time", n.time), new XAttribute("remark", n.remark));
                    mapping.Add(temp);
                }

                File.SetAttributes(ConfigSettingPath, FileAttributes.Normal);
                Save(root.ToString());
                File.SetAttributes(ConfigSettingPath, FileAttributes.Hidden);
                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node">移除的节点对象</param>
        /// <returns>成功true；失败false</returns>
        public static bool Remove(string node)
        {
            if (ConfigContent.Length <= 0)
            {
                ReadDESConfig();
            }

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(ConfigContent);
                XmlNode tempNode = doc.SelectSingleNode("configuration").SelectSingleNode(node);
                tempNode.RemoveAll();
                Save(doc.InnerXml);
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 获取节点链表
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childeNode">子节点</param>
        /// <returns>节点列表</returns>
        public static IList<XElement> GetNodes(string parentNode, string childeNode = "")
        {
            try
            {
                if (ConfigContent.Length <= 0)
                {
                    ReadDESConfig();
                }

                XElement root = XElement.Parse(ConfigContent);
                IEnumerable<XElement> elements = from ele in root.Elements(parentNode).Elements(childeNode) select ele;
                return elements.ToList();
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        /// <summary>
        /// 创建模板文件
        /// </summary>
        public static void CreateXML()
        {
            try
            {
                ConfigContent = @"<?xml version='1.0' encoding='utf-8'?>
                                            <configuration>
                                                <appSettings>
                                                        <add key='wcfService' value='http://localhost:8887/Service' />
                                                        <add key='selfAddress' value='http://192.168.0.2' />
                                                        <add key='selfPort' value='10000'/>
                                                        <add key='armAddress' value='192.168.0.1' />
                                                        <add key='armPort' value='8888' />
                                                        <add key='cleanType' value='1'/>
                                                        <add key='license' value=''/>
                                                </appSettings>
                                                <portMapping>
                                                        <port portCode='01' stepName='FirstWash' remark='刷洗槽' />
                                                        <port portCode='02' stepName='EnzymeWash' remark='酶洗槽' />
                                                        <port portCode='03' stepName='Cleanout' remark='次洗槽' />
                                                        <port portCode='04' stepName='DipIn' remark='消毒槽' />
                                                        <port portCode='05' stepName='LastWash'  remark='末洗槽' />
                                                        <port portCode='20' stepName='OtherAutoMachine'  remark='自动清洗机' />
                                                </portMapping>
                                                <portName>
                                                        <port stepName='FirstWash' remark='刷洗槽'></port>
                                                        <port stepName='EnzymeWash' remark='酶洗槽'></port>
                                                        <port stepName='Cleanout' remark='次洗槽'></port>
                                                        <port stepName='DipIn' remark='消毒槽'></port>
                                                        <port stepName='LastWash' remark='末洗槽'></port>
                                                        <port stepName='MallAutoMachine' remark='迈尔清洗机'></port>
                                                        <port stepName='OtherAutoMachine' remark='清洗机'></port>
                                                </portName>
                                                <stepTime>
                                                        <step stepName='ManualWash' time='0' remark='刷洗'></step>
                                                        <step stepName='FirstWash' time='0' remark='初洗'></step>
                                                        <step stepName='EnzymeWash' time='0' remark='酶洗'></step>
                                                        <step stepName='Cleanout' time='0' remark='次洗'></step>
                                                        <step stepName='DipIn' time='0' remark='消毒'></step>
                                                        <step stepName='LastWash' time='0' remark='末洗'></step>
                                                        <step stepName='DipInSec' time='0' remark='二次消毒'></step>
                                                        <step stepName='LastWashSec' time='0' remark='二次末洗'></step>
                                                        <step stepName='EnzymeWashAuto' time='0' remark='酶洗（机洗）'></step>
                                                        <step stepName='CleanoutAuto' time='0' remark='次洗（机洗）'></step>
                                                        <step stepName='DipInAuto' time='0' remark='消毒（机洗）'></step>
                                                        <step stepName='LastWashAuto' time='0' remark='末洗（机洗）'></step>
                                                        <step stepName='DipInSecAuto' time='0' remark='二次消毒（机洗）'></step>
                                                        <step stepName='LastWashSecAuto' time='0' remark='二次末洗（机洗）'></step>
                                                </stepTime>
                                            
                                            </configuration>";
                using (FileStream stream = new FileStream(DealXml.ConfigSettingPath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(ConfigContent);
                        writer.Flush();
                    }
                }

                File.SetAttributes(DealXml.ConfigSettingPath, FileAttributes.Hidden);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 读取配置数据
        /// </summary>
        public static void ReadDESConfig()
        {
            try
            {
                if (File.Exists(DealXml.ConfigSettingPath))
                {
                    using (FileStream stream = new FileStream(DealXml.ConfigSettingPath, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            ConfigContent = reader.ReadToEnd();
                            XElement.Parse(ConfigContent);
                        }
                    }
                }
                else
                {
                    CreateXML();
                }
            }
            catch (Exception)
            {
                File.Delete(DealXml.ConfigSettingPath);
                ReadDESConfig();
            }
        }

        /// <summary>
        /// 对更改进行保存
        /// </summary>
        /// <param name="content">更改后的内容</param>
        /// <returns>成功true；失败false</returns>
        public static bool Save(string content)
        {
            try
            {
                ConfigContent = content;

                if (File.Exists(DealXml.ConfigSettingPath))
                {
                    // 删除原来旧文件
                    File.Delete(DealXml.ConfigSettingPath);
                }

                using (StreamWriter writer = new StreamWriter(DealXml.ConfigSettingPath))
                {
                    string t = ConfigContent;

                    // 写入新配置信息
                    writer.WriteLine(t);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}