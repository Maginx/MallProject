using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MallHost
{
    /// <summary>
    /// 程序入口
    /// </summary>
    internal sealed class Program
    {
        #region 私有静态字段
        /// <summary>
        /// 输入线程
        /// </summary>
        private static Thread inputThread;
        #endregion

        #region 公有属性
        /// <summary>
        /// 退出标志
        /// </summary>
        public static bool ExitFlag { get; set; }
        #endregion

        #region 禁用关闭按钮

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpclassName, string lpwindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, IntPtr brevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern IntPtr RemoveMenu(IntPtr hmenu, uint uposition, uint uflags);
        #endregion

        #region 入口方法
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            try
            {
                // 初始化控制台
                InitialConsole();

                // 启动控制辅助线程
                inputThread = new Thread(new ThreadStart(InputControl));
                inputThread.IsBackground = true;
                inputThread.Start();

                // 开启WCF服务 
                ProxyServ.StartProxyService();

                // 程序退出操作
                while (true)
                {
                    Application.DoEvents();

                    if (ExitFlag)
                    {
                        DialogResult result = MessageBox.Show("确实要退出迈尔数据服务吗？退出后将无法向客户端提供服务", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.Yes)
                        {
                            break;
                        }
                        else
                        {
                            ExitFlag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (FileStream stream = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 输出控制
        /// </summary>
        private static void InputControl()
        {
            // 服务介绍
            ConsoleHelper.PrintText("--------------------------------------------------------------------------------");
            ConsoleHelper.PrintText("本服务程序主要为迈尔客户端组件程序提供数据支持，非技术人员请勿操作", ConsoleColor.Red);
            ConsoleHelper.PrintText("--------------------------------------------------------------------------------");
            ConsoleHelper.PrintText("1.启动Mall数据代理程序服务请输入OpenProxy\r\n");
            ConsoleHelper.PrintText("2.关闭Mall数据代理程序服务请输入CloseProxy\r\n");
            ConsoleHelper.PrintText("3.设置Mall数据代理程序服务主机地址请输入EnterProxy\r\n");
            ConsoleHelper.PrintText("4.设置Mall数据代理程序服务元数据地址请输入EnterMetaData\r\n");
            ConsoleHelper.PrintText("5.设置Mall数据服务数据库连接字符串请输入DataBase\r\n");
            ConsoleHelper.PrintText("6.清屏请输入Clear\r\n");
            ConsoleHelper.PrintText("7.退出服务请输入Exit\r\n");
            ConsoleHelper.PrintText("--------------------------------------------------------------------------------");

            while (true)
            {
                string input = Console.ReadLine();
                Type type = Type.GetType(string.Format("MallHost.{0}Command", input));
                System.Reflection.MethodInfo method = type.GetMethod("ExcuteCommand");
                method.Invoke(Activator.CreateInstance(type), null);
            }
        }

        /// <summary>
        /// 初始化控制台
        /// </summary>
        private static void InitialConsole()
        {
            Console.Title = "数据中心服务";
            Console.CursorVisible = true;

            // 显示托盘程序
            ConsoleHelper.ShowNotifyIcon();

            // 显示隐藏窗体
            ConsoleHelper.ShowHideWindow();

            // 禁用关闭按钮
            DisableCloseButton(Console.Title);

            // 读取配置文件  
            ReadXML.ReadXMLService();

            // 初始化配置信息
            Global.InitialAgrs();
        }

        /// <summary>
        /// 禁用关闭按钮
        /// </summary>
        /// <param name="title">The title.</param>
        private static void DisableCloseButton(string title)
        {
            // 线程睡眠，确保closebtn中能够正常FindWindow，否则有时会Find失败。。
            Thread.Sleep(100);

            IntPtr windowHandle = FindWindow(null, title);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint sc_close = 0xF060;
            RemoveMenu(closeMenu, sc_close, 0x0);
        }

        /// <summary>
        /// Determines whether [is exists console] [the specified title].
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>退出True，否则False</returns>
        private static bool IsExistsConsole(string title)
        {
            IntPtr windowHandle = FindWindow(null, title);

            if (windowHandle.Equals(IntPtr.Zero))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
