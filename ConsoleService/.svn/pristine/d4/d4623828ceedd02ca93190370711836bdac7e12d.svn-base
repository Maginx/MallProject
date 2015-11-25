using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MallHost
{
    /// <summary>
    /// 代理服务
    /// </summary>
    internal sealed class ProxyServ
    {
        #region 私有属性
        /// <summary>
        /// 代理对象
        /// </summary>
        private static ServiceHost mallProxyHost;
        #endregion

        #region 共有方法
        /// <summary>
        /// 打开MallProx服务
        /// </summary>
        public static void StartProxyService()
        {
            try
            {
                if (mallProxyHost == null)
                {
                    Uri uri = new Uri(ReadXML.Xmls["mallProxy"]);
                    mallProxyHost = new ServiceHost(typeof(MallHost.MallProxy), uri);

                    // mex协议终结点
                    var smb = mallProxyHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                    if (smb == null)
                    {
                        smb = new ServiceMetadataBehavior();
                    }
                    smb.HttpGetEnabled = true;//ServiceMetadataBehavior.MexContractName
                    //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    mallProxyHost.Description.Behaviors.Add(smb);
                    mallProxyHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                    // WSHttpBinding协议终结点 
                    var wsbinding = new WSHttpBinding(SecurityMode.None);
                    wsbinding.MaxBufferPoolSize = 2147483647;
                    wsbinding.MaxReceivedMessageSize = 2147483647;
                    mallProxyHost.AddServiceEndpoint(typeof(MallHost.IMallProxy), wsbinding, uri);

                    foreach (var endpont in mallProxyHost.Description.Endpoints)
                    {
                        endpont.Behaviors.Add(new MyEndPointBehavior());
                    }

                    mallProxyHost.Opened += delegate
                    {
                        ConsoleHelper.Message("迈尔数据代理服务（MallProxy）已经启动", ConsoleColor.Green);

                        foreach (var e in mallProxyHost.Description.Endpoints)
                        {
                            ConsoleHelper.PrintText("主机地址：" + e.Address);
                            ConsoleHelper.PrintText("终结点名称：" + e.Name);
                            ConsoleHelper.PrintText("契约：" + e.Contract.Name);
                            ConsoleHelper.PrintText("绑定类型：" + e.Binding.Name);
                            ConsoleHelper.PrintText("\r\n");
                        }

                        ConsoleHelper.PrintText("数据库连接字符串：" + Global.ConnStr);
                        ConsoleHelper.PrintText("数据库是否已经连接成功：" + MallHost.Data.DataBase.ConnectDataBase());
                        ConsoleHelper.PrintText("\r\n");
                    };

                    if (mallProxyHost.State != CommunicationState.Opened)
                    {
                        mallProxyHost.Open();
                    }
                }
                else
                {
                    ConsoleHelper.PrintText("服务已经启动");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Message(ex.Message, ConsoleColor.Red);
            }
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        public static void CloseProxyService()
        {
            if (mallProxyHost != null)
            {
                if (mallProxyHost.State != CommunicationState.Closed)
                {
                    mallProxyHost.Close();
                    ConsoleHelper.PrintText("已经关闭MallProxy服务");
                }
            }
            else
            {
                ConsoleHelper.PrintText("您还没有启动过该服务！");
            }
        }
        #endregion
    }
}
