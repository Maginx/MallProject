using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ProxyClient
{
    /// <summary>
    /// 全局对象
    /// </summary>
    internal sealed partial class Global
    {
        #region 公有对象
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static string LoginedUser;

        /// <summary>
        /// 当前登录用户角色
        /// </summary>
        public static ClassLibrary.UserRole LoginedUserRole;

        /// <summary>
        /// 数据库对象
        /// </summary>
        public static string DataBase;

        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public static string Server;

        /// <summary>
        /// 数据库用户名
        /// </summary>
        public static string User;

        /// <summary>
        /// 数据库用户密码
        /// </summary>
        public static string Pass;

        /// <summary>
        /// 日志路径
        /// </summary>
        public static readonly string LogPath;

        /// <summary>
        /// 数据库连接字符串
        /// Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        /// </summary>
        public static string ConnectionStr = null;

        private static object lockObj = new object();
        #endregion

        #region 构造方法
        static Global()
        {
            var s = DealXml.ReadSysConfig("appSettings", "wcfService").Split(':')[1].Trim("//".ToArray());
            Server = string.Empty;
            //Server = ConfigurationManager.AppSettings["server"];
            //DataBase = ConfigurationManager.AppSettings["database"];
            //User = ConfigurationManager.AppSettings["user"];
            //Pass = ConfigurationManager.AppSettings["pass"];
            LogPath = "log.txt";
            if (Server.Length > 0)
            {
                ConnectionStr = string.Format(ConnectionStr, Server, DataBase, User, Pass);
            }
            else
            {
                ConnectionStr = string.Format("Data Source={0};Initial Catalog=EndoscopeArm;User ID=sa;password=s@123456", s);
            }
        }
        #endregion

        #region 日志
        public static void Log(string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Global.LogPath, true))
                {
                    writer.WriteLine(DateTime.Now.ToSafeString() + ":" + text);
                }
            }
            finally
            {
            }
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void Log(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Global.LogPath, true))
                {
                    writer.WriteLine(string.Format("错误信息{0}", ex.Message));
                    writer.WriteLine(string.Format("错误应用程序或者对象名{0}", ex.Source));
                    writer.WriteLine(string.Format("错误堆栈信息", ex.StackTrace));
                }
            }
            finally
            {
            }
        }
        #endregion

        /// <summary>
        /// 安全读取
        /// </summary>
        /// <param name="obj">reader对象</param>
        /// <returns>读取结果</returns>
        public static string SafeReader(object obj)
        {
            string result = string.Empty;

            if (obj == null)
            {
                return result;
            }

            if (Convert.IsDBNull(obj))
            {
                return result;
            }

            try
            {
                result = obj.ToString();
            }
            catch { }

            return result;
        }
    }
}
