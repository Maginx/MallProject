﻿using System;
using System.Collections.Generic;
using System.Configuration;
using MallHost.Data;
using System.IO;

namespace MallHost
{
    /// <summary>
    /// 全局参数
    /// </summary>
    internal sealed class Global
    {
        #region 私有字段
        /// <summary>
        /// 日志路径
        /// </summary>
        private static string logFile = "log.txt";
        #endregion

        #region 构造方法
        /// <summary>
        /// Initializes the <see cref="Global"/> class.
        /// </summary>
        static Global()
        {
            DBProcess = new DataBusiProcess();
        }
        #endregion

        #region  公有属性
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnStr { get; private set; }

        /// <summary>
        /// 工作量表
        /// </summary>
        public static string AmountWork { get; private set; }

        /// <summary>
        /// 内镜信息表
        /// </summary>
        public static string EndoscopeInfo { get; private set; }

        /// <summary>
        /// 内镜清洗表
        /// </summary>
        public static string EndoscopeRecord { get; private set; }

        /// <summary>
        /// 内镜清洗详细信息表
        /// </summary>
        public static string EndoscopeRecordInfo { get; private set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public static string UserInfo { get; private set; }

        /// <summary>
        /// 临时记录
        /// </summary>
        public static string EndoscopeTemp { get; private set; }

        /// <summary>
        /// 业务逻辑处理对象
        /// </summary>
        public static DataBusiProcess DBProcess { get; private set; }
        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        public static void InitialAgrs()
        {
            try
            {
                ConnStr = ReadXML.Xmls["SQL"].ToString().Trim();
                AmountWork = ReadXML.Xmls["amountWork"].ToString().Trim();
                EndoscopeInfo = ReadXML.Xmls["endoscopeInfo"].ToString().Trim();
                EndoscopeRecord = ReadXML.Xmls["endoscopeRecord"].ToString().Trim();
                EndoscopeRecordInfo = ReadXML.Xmls["endoscopeRecordInfo"].ToString().Trim();
                UserInfo = ReadXML.Xmls["user"].ToString().Trim();
                EndoscopeTemp = ReadXML.Xmls["endoscopeTemp"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void Log(Exception ex)
        {
            using (var writer = new StreamWriter(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString() + " : ");
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine(ex.Source);
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void Log(string msg, Exception ex)
        {
            using (var writer = new StreamWriter(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString() + " : ");
                writer.WriteLine(msg);
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine(ex.Source);
            }
        }
        #endregion
    }
}
