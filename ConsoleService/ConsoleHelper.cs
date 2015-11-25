using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace MallHost
{
    /// <summary>
    /// 输出 辅助类
    /// </summary>
    internal sealed class ConsoleHelper
    {
        #region 构造方法
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static ConsoleHelper()
        {
            Username = string.Empty;
            IconPath = Path.Combine(Application.StartupPath, @"database.ico");
            LoginOn = new Login();
        }
        #endregion

        #region 私有对象
        /// <summary>
        /// 托盘对象
        /// </summary>
        public static NotifyIcon NotifyIconImg { get; private set; }

        /// <summary>
        /// 图标路径
        /// </summary>
        public static string IconPath { get; private set; }

        /// <summary>
        /// 右键菜单对象
        /// </summary>
        public static ContextMenu Menu { get; private set; }

        /// <summary>
        /// 登录窗口对象
        /// </summary>
        public static Login LoginOn { get; private set; }
        #endregion

        #region 公有对象
        /// <summary>
        /// 标志位
        /// </summary>
        public static bool Flag { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string Username { get; set; }
        #endregion

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nmdShow);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string className, string windowName);

        #region 公有方法
        /// <summary>
        /// 显示托盘
        /// </summary>
        public static void ShowNotifyIcon()
        {
            if (NotifyIconImg == null)
            {
                NotifyIconImg = new NotifyIcon();
            }

            NotifyIconImg.Icon = new Icon(IconPath);
            NotifyIconImg.Visible = true;
            NotifyIconImg.Text = "迈尔数据服务";
            NotifyIconImg.ShowBalloonTip(6000, "迈尔后台数据服务", "为迈尔客户端提供数据服务支持，切勿退出，若要查看详细内容请先登录", ToolTipIcon.None);
            SetMenu();
        }

        /// <summary>
        /// 登录
        /// </summary>
        public static void Login()
        {
            LoginOn = new Login();
            LoginOn.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - LoginOn.Height) / 2);

            DialogResult r = LoginOn.ShowDialog();
            if (r != DialogResult.Yes)
            {
                LoginOn.Close();
                ConsoleHelper.Flag = false;
                Menu.MenuItems[2].Text = "登录";
            }
            else
            {
                NotifyIconImg.ShowBalloonTip(5000, "迈尔后台数据服务", "用户已经登录，可以查看服务器详细内容", ToolTipIcon.None);
                ConsoleHelper.Username = "admin";
                Menu.MenuItems[2].Text = "注销";
            }
        }

        /// <summary>
        /// 显示或者隐藏控制台
        /// </summary>
        public static void ShowHideWindow()
        {
            IntPtr intptr = FindWindow(null, Console.Title);

            // 当前用户可以观看显示屏
            if (Username.Length > 0)
            {
                if (intptr != IntPtr.Zero)
                {
                    if (!Flag)
                    {
                        Menu.MenuItems[1].Text = "显示";
                        ShowWindow(intptr, 0);
                    }
                    else
                    {
                        Menu.MenuItems[1].Text = "隐藏";
                        ShowWindow(intptr, 1);
                    }
                }
            }
            else
            {
                NotifyIconImg.ShowBalloonTip(6000, "迈尔后台数据服务", "为迈尔客户端提供数据服务支持，切勿退出，若要查看详细内容请先登录", ToolTipIcon.Info);
                Menu.MenuItems[1].Text = "显示";
                ShowWindow(intptr, 0);
            }
        }

        /// <summary>
        /// 封装控制台输出
        /// </summary>
        /// <param name="text">信息</param>
        /// <param name="color">颜色</param>
        public static void PrintText(string text = "", ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }

        /// <summary>
        /// 控制台信息
        /// </summary>
        /// <param name="text">信息</param>
        /// <param name="color">颜色</param>
        public static void Message(string text, ConsoleColor color = ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now.ToString() + "    " + text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 设置菜单
        /// </summary>
        private static void SetMenu()
        {
            if (Menu == null)
            {
                Menu = new ContextMenu();
            }

            // 菜单
            MenuItem exitItem = new MenuItem();
            exitItem.Text = "退出";
            exitItem.Index = 1;
            exitItem.Click += delegate
            {
                // 程序退出
                Program.ExitFlag = true;
            };

            MenuItem loginItem = new MenuItem();
            loginItem.Text = "登录";
            loginItem.Index = 2;
            loginItem.Click += delegate
            {
                if (Username.Length <= 0)
                {
                    Login();
                }
                else
                {
                    loginItem.Text = "登录";
                    Username = string.Empty;
                }
            };

            MenuItem hideItem = new MenuItem();
            hideItem.Index = 0;
            hideItem.Click += delegate
            {
                ShowHideWindow();
                Flag = !Flag;
            };
            Menu.MenuItems.Add(exitItem);
            Menu.MenuItems.Add(hideItem);
            Menu.MenuItems.Add(loginItem);

            NotifyIconImg.ContextMenu = Menu;
            NotifyIconImg.MouseDoubleClick += delegate
            {
                ShowHideWindow();
                Flag = !Flag;
            };
        }
        #endregion
    }
}
