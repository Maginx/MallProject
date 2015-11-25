using System;
using System.Net.Sockets;

namespace ProxyClient
{
    /// <summary>
    /// 发送Socket
    /// </summary>
    internal sealed class SendSocket
    {
        /// <summary>
        /// 发送端口对象
        /// </summary>
        private static Socket sendSocket;

        /// <summary>
        /// 缓存
        /// </summary>
        private static byte[] bytes = null;

        #region 构造方法
        /// <summary>
        /// Initializes the <see cref="SendSocket"/> class.
        /// </summary>
        static SendSocket()
        {
            SendMsg = new SendMsgAsyn(SendMsgToArm);
            CallBack = new AsyncCallback(CallBackMethod);
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bytes = new byte[16];
            ProtocalFormat = "1656{0}0008000000ECEC{1}{2}00000000";
        }
        #endregion

        #region 公有对象
        /// <summary>
        /// 异步发送委托
        /// </summary>
        /// <param name="ms">The parameters.</param>
        public delegate void SendMsgAsyn(string ms);

        /// <summary>
        /// 发送对象
        /// </summary>
        public static SendMsgAsyn SendMsg { get; private set; }

        /// <summary>
        /// 回调对象
        /// </summary>
        public static AsyncCallback CallBack { get; private set; }

        /// <summary>
        /// 发送数据格式
        /// </summary>
        public static string ProtocalFormat { get; private set; }
        #endregion

        /// <summary>
        /// 通过Socket发送信息
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void SendMsgToArm(string msg)
        {
            try
            {
                string armIP = DealXml.ReadSysConfig("appSettings", "armAddress");
                string armPort = DealXml.ReadSysConfig("appSettings", "armPort");

                if (!sendSocket.Connected)
                {
                    sendSocket.Connect(armIP, int.Parse(armPort));
                }

                StringToByte(msg);
                sendSocket.Send(bytes);
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);
            }
        }

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="result">The result.</param>
        public static void CallBackMethod(IAsyncResult result)
        {
            SendMsgAsyn s = result.AsyncState as SendMsgAsyn;
            s.EndInvoke(result);
        }

        /// <summary>
        /// String To Byte转换
        /// </summary>
        /// <param name="msg">内镜信息</param>
        private static void StringToByte(string msg)
        {
            for (int i = 0; i < 16; i++)
            {
                bytes[i] = byte.Parse(msg.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
        }
    }
}
