using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ComponentFactory.Krypton.Toolkit;
using ProxyClient.Controls;
using System.IO.Ports;
using ProxyClient.Steps;
using System.ComponentModel;

namespace ProxyClient
{




    /// <summary>
    /// Accept data from arm
    /// </summary>
    internal class AcceptSocket
    {
        /// <summary>
        /// com object
        /// </summary>
        private static CommonCom serial = null;
        /// <summary>
        /// temp byte array
        /// </summary>
        private static byte[] tmpBytes = null;
        /// <summary>
        /// temp ports name
        /// </summary>
        private static List<string> tmpPorts = null;

        /// <summary>
        /// hex replace array
        /// </summary>
        private static char[] hexDigits = {
        '0', '1', '2', '3', '4', '5', '6', '7',
        '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        #region  private filed
        /// <summary>
        /// msg length
        /// </summary>
        private static int recv;

        /// <summary>
        /// ip and ip port
        /// </summary>
        private static IPEndPoint ipend;

        /// <summary>
        /// local server socket object
        /// </summary>
        private static Socket serverSocket;

        /// <summary>
        /// accept socket object
        /// </summary>
        private static Socket acceptSocket;
        #endregion

        #region public function
        /// <summary>
        /// begin to monitor Socket
        /// </summary>
        public static void BegintSocket()
        {
            try
            {
                IPHostEntry iphostinfo = Dns.GetHostEntry(Dns.GetHostName());
                ipend = new IPEndPoint(
                    IPAddress.Parse(DealXml.ReadSysConfig("appSettings", "selfAddress")),
                    Convert.ToInt32(DealXml.ReadSysConfig("appSettings", "selfPort")));

                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // bind
                serverSocket.Bind(ipend);

                // listen
                serverSocket.Listen(10);

                // accept asyn
                AsynAccept(serverSocket);
            }
            catch (Exception ex)
            {
                // exception log
                FormHelper.SocketLog(ex.Message);
            }
        }

        /// <summary>
        /// begin monitor COM
        /// </summary>
        public static void BeginCom()
        {
            var name = SerialPort.GetPortNames().FirstOrDefault();

            if (string.IsNullOrWhiteSpace(name))
            {
                KryptonMessageBox.Show("no com port to use,please check com port again");
                return;
            }

            serial = new CommonCom();
            serial.PortNum = "COM2";
            serial.BaudRate = 9600;
            serial.ByteSize = 8;
            serial.Parity = 0;
            serial.StopBits = 1;
            serial.ReadTimeout = 50;

            try
            {
                serial.Open();
                //clean the port buffer data
                serial.ClearPortData();
            }
            catch (Exception ex)
            {
                Global.Log(ex);
                KryptonMessageBox.Show(string.Format("com port open failed. port name:{0}", name));
            }

            // some initialization
            tmpBytes = new byte[1];
            tmpPorts = GSetting.PortMapDict.Keys.ToList();

            // write data to 485 
            DoWrite();


        }

        /// <summary>
        /// Foreach write
        /// </summary>
        private static void DoWrite()
        {
            while (true)
            {
                tmpPorts.ForEach(t =>
                {
                    // back work process
                    BackWorker();
                    tmpBytes[0] = (Convert.ToByte(t, 16));
                    serial.Write(tmpBytes);
                    System.Threading.Thread.Sleep(80);
                });
            }
        }

        /// <summary>
        /// Back work process
        /// </summary>
        private static void BackWorker()
        {
            byte[] buffer = serial.Read(32);

            if (buffer.Length == 0)
            {
                return;
            }

            var tmp = ToHexString(buffer);
            FormHelper.AppendText(tmp, Color.Green);

            if (tmp.Length == 16)
            {
                GSetting.WashObj.ShowStatusTips("工作站连接成功！", Color.Green);
                FormHelper.VoiceSpeech("工作站连接成功");
            }
            else if (tmp.Length == 0)
            {
                GSetting.WashObj.ShowStatusTips("工作站已经断开", Color.Red);
                FormHelper.VoiceSpeech("工作站已经断开");
                acceptSocket.Close();
            }
            else
            {
                // get the data
                GSetting.MData.AccepData = tmp;
            }
        }

        static void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //byte[] buffer = new byte[16];

            //var serial = sender as SerialPort;

            //if (serial == null)
            //{
            //    KryptonMessageBox.Show("串口对象为NULL");
            //    return;
            //}

            //var result = serial.Read(buffer, 0, 16);

            //if (result == 0)
            //{
            //    return;
            //}

            //var tmp = ToHexString(buffer);

            //if (tmp.Length == 16)
            //{
            //    GSetting.WashObj.ShowStatusTips("工作站连接成功！", Color.Green);
            //    FormHelper.VoiceSpeech("工作站连接成功");
            //}
            //else if (tmp.Length == 0)
            //{
            //    GSetting.WashObj.ShowStatusTips("工作站已经断开", Color.Red);
            //    FormHelper.VoiceSpeech("工作站已经断开");
            //    acceptSocket.Close();
            //}
            //else
            //{
            //    // 获取数据
            //    GSetting.MData.AccepData = tmp;
            //    System.Diagnostics.Debug.WriteLine(tmp);
            //}
        }

        /// <summary>
        /// Dec to Hex
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>hex number</returns>
        private static string ToHexString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }

            return new string(chars);
        }
        #endregion

        #region  私有方法
        /// <summary>
        /// asyn accept data
        /// </summary>
        /// <param name="serverSocket">server socket object</param>
        private static void AsynAccept(Socket serverSocket)
        {
            if (!serverSocket.IsBound)
            {
                return;
            }

            var e = new SocketAsyncEventArgs();
            e.Completed += e_Completed;
            serverSocket.AcceptAsync(e);
        }

        /// <summary>
        /// SocketAccept operate asyn
        /// </summary>
        private static void e_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket server = sender as Socket;
            if (server != null)
            {
                AsynAccept(server);
            }

            var tmp = e.AcceptSocket;

            if (e.SocketError != SocketError.Success)
            {
                tmp.Dispose();
                return;
            }

            var buffer = new byte[16];

            while (true)
            {
                try
                {
                    recv = tmp.Receive(buffer, 16, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    tmp.Dispose();
                    FormHelper.SocketLog(ex.ErrorCode + "-" + ex.Message);
                    return;
                }

                var temp = new StringBuilder();

                for (int i = 0; i < recv; i++)
                {
                    temp.Append(GetNum((byte)(buffer[i] / 16)) + GetNum((byte)(buffer[i] % 16)));
                }

                try
                {
                    // arm power on connect self
                    if (temp.Length == 16)
                    {
                        // verify send msg
                        if (SocketDataProcessValid.SocketConnection(temp.ToSafeString()))
                        {
                            GSetting.WashObj.ShowStatusTips("工作站连接成功！", Color.Green);
                            FormHelper.VoiceSpeech("工作站连接成功");
                        }
                        else
                        {
                            GSetting.WashObj.ShowStatusTips("工作站连接失败，请重启工作站后，再重启程序！", Color.Red);
                            FormHelper.VoiceSpeech("工作站连接失败");
                        }
                    }
                    else if (temp.Length == 0)
                    {
                        GSetting.WashObj.ShowStatusTips("工作站已经断开", Color.Red);
                        FormHelper.VoiceSpeech("工作站已经断开");
                        tmp.Dispose();
                    }
                    else
                    {
                        // get data
                        GSetting.MData.AccepData = temp.ToSafeString();
                        //GSetting.WashObj.ShowStatusTips(GSetting.MData.AccepData, Color.Red);
                        //Global.Log(GSetting.MData.AccepData);
                    }
                }
                catch (Exception ex)
                {
                    FormHelper.RecordLogMsg(GSetting.MData.AccepData);
                    FormHelper.RecordLogMsg(ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <param name="num">The number.</param>
        /// <returns>number to string</returns>
        private static string GetNum(byte num)
        {
            if (num >= 10)
            {
                switch (num % 10)
                {
                    case 0:
                        return "A";
                    case 1:
                        return "B";
                    case 2:
                        return "C";
                    case 3:
                        return "D";
                    case 4:
                        return "E";
                    case 5:
                        return "F";
                    default:
                        {
                            Console.WriteLine("数据出错");
                            return "-1";
                        }
                }
            }
            else
            {
                return num.ToString();
            }
        }
        #endregion
    }
}
