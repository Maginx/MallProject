﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrary;

namespace MallHost.Data
{
    /// <summary>
    /// 数据对象
    /// </summary>
    public class DataBase : IDataBase
    {
        #region Connect to databases
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns>
        /// 连接结果
        /// </returns>
        public static bool ConnectDataBase()
        {
            using (var connection = new SqlConnection(Global.ConnStr))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    Global.Log("数据库连接失败", ex);
                    throw new Exception("数据库连接失败");
                }
                catch (Exception ex)
                {
                    Global.Log("数据库连接失败", ex);
                    throw new Exception("数据库连接失败");
                }
            }

            return false;
        }
        #endregion

        #region  Update endoscopeCleanTemp record
        /// <summary>
        /// 更新endoscopeCleanTemp记录
        /// </summary>
        /// <param name="time">清洗时间（开始或结束时间）</param>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <param name="step">清洗工序</param>
        /// <returns>
        /// 更新结果（true 表示更新成功，false表示更新失败）
        /// </returns>
        public bool RecordCleanTime(string time, string endoscopeSn, string step)
        {
            if (string.IsNullOrEmpty(endoscopeSn))
            {
                return false;
            }

            if (string.IsNullOrEmpty(step))
            {
                return false;
            }

            return this.Update("endoscopeTemp", step, time, "endoscopeSn", endoscopeSn);
        }
        #endregion

        #region Get endoscope information by SIM
        /// <summary>
        /// 根据SIM获取相关信息
        /// </summary>
        /// <param name="endoscopeSim">内镜卡号</param>
        /// <param name="tablename">表名</param>
        /// <returns>
        /// 内镜详细信息
        /// </returns>
        /// <exception cref="System.Exception">SqlException</exception>
        public EndoscopeInfo GetEndoscopeMsgBySIM(string endoscopeSim, string tablename)
        {
            EndoscopeInfo endoscope = null;
            var selectsql = "select * from EndoscopeInfo where endoscopeSim=@endoscope";

            SqlParameter[] paras = 
            {
                this.GetSqlParameter("@endoscope", endoscopeSim, DbType.String)
            };

            try
            {
                // reader读取内镜卡号
                using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, selectsql, paras))
                {
                    if (reader == null || !reader.HasRows)
                    {
                        return endoscope;
                    }

                    while (reader.Read())
                    {
                        endoscope = new EndoscopeInfo { EndoscopeSn = reader.SafeRead<string>("endoscopeSn") };
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("根据Sim卡号查找内镜出错", ex);
                throw new Exception("根据Sim卡号查找内镜出错");
            }

            return endoscope;
        }
        #endregion

        #region  Get user information by SIM
        /// <summary>
        /// 根据SIM获取用户
        /// </summary>
        /// <param name="userSim">The user sim.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        /// 用户信息
        /// </returns>
        public UserInfo GetUserInfo(string userSim, string table)
        {
            UserInfo userinfo = null;
            var tempstr = "select * from userInfo where userSim=@userSim";
            SqlParameter[] paras =
            {
                this.GetSqlParameter("@userSim",userSim,DbType.String)
            };

            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, tempstr, paras))
                {
                    if (reader == null || !reader.HasRows)
                    {
                        return null;
                    }

                    while (reader.Read())
                    {
                        userinfo = new UserInfo
                        {
                            UserSn = reader.SafeRead<string>("userSn"),
                            UserName = reader.SafeRead<string>("userName")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("根据sim获取用户信息失败", ex);
                throw new Exception("根据sim获取用户信息失败");
            }

            return userinfo;
        }
        #endregion

        #region Reset endoscope information
        /// <summary>
        /// 重置内镜信息
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <param name="endoscopeSim">内镜卡号</param>
        /// <param name="washerNo">清洗工编号</param>
        /// <param name="washerRealName">清洗工名</param>
        /// <param name="cleanType">清洗类型</param>
        /// <param name="washDate">清洗日期</param>
        /// <param name="disinfection">消毒液</param>
        /// <param name="autoClean">自动清洗机编号</param>
        /// <returns>
        /// 重置结果
        /// </returns>
        public bool ResetEndoscope(string endoscopeSn, string endoscopeSim, string washerNo, string washerRealName, string cleanType, string washDate, string disinfection, string autoClean)
        {
            SqlConnection connection = null;
            var deletesql = "delete  from endoscopeTemp where endoscopeSn=@endoscopeSn";
            SqlParameter[] deleteparas =
            {
                this.GetSqlParameter("@endoscopeSn",endoscopeSn,DbType.String)
            };

            using (connection = new SqlConnection(Global.ConnStr))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    int result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, deletesql, deleteparas);

                    if (result <= 0)
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Global.Log("删除endoscopeTemp信息失败", ex);
                    tran.Rollback();
                    throw new Exception("删除endoscopeTemp信息失败");
                }

                try
                {
                    var insertsql = @"insert into EndoscopeTemp (endoscopeSn,endoscopeSim,washerNo,washerName,cleanType,disinfection,washDate,autoCleanNo,isRecord)
                                    values(@endoscopeSn,@endoscopeSim,@washerNo,@washerName,@cleanType,@disinfection,@washDate,@autoClean,null)";
                    SqlParameter[] insertparas =
                    {
                        this.GetSqlParameter("@endoscopeSn", endoscopeSn, DbType.String),
                        this.GetSqlParameter("@endoscopeSim",endoscopeSim,DbType.String),
                        this.GetSqlParameter("@washerNo",washerNo,DbType.String),
                        this.GetSqlParameter("@washerName",washerRealName,DbType.String),
                        this.GetSqlParameter("@cleanType",cleanType,DbType.String),
                        this.GetSqlParameter("@disinfection",disinfection,DbType.String),
                        this.GetSqlParameter("@washDate",washDate,DbType.DateTime),
                        this.GetSqlParameter("@autoClean",autoClean,DbType.String)
                    };

                    int result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, insertsql, insertparas);

                    if (result > 0)
                    {
                        tran.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Global.Log("插入endoscopeTemop信息失败", ex);
                    tran.Rollback();
                    throw new Exception("插入endoscopeTemop信息失败");
                }
            }

            return false;
        }
        #endregion

        #region Get endoscope information  by  endoscpoeSN
        /// <summary>
        /// 获得内镜信息
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <returns>
        /// 内镜信息
        /// </returns>
        public EndoscopeTemp GetNewCleanMsg(string endoscopeSn)
        {
            EndoscopeTemp endoscopeclean = null;
            var selectsql = "select * from endoscopeTemp where endoscopeSn=@endoscopeSn";
            SqlParameter[] paras =
            {
                 this.GetSqlParameter("@endoscopeSn",endoscopeSn,DbType.String)
            };

            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, selectsql, paras))
                {
                    if (!reader.HasRows)
                    {
                        return endoscopeclean;
                    }

                    while (reader.Read())
                    {

                        endoscopeclean = new EndoscopeTemp
                        {
                            EndoscopeSn = reader.SafeRead<string>("endoscopeSn"),
                            EndoscopeSim = reader.SafeRead<string>("endoscopeSim"),
                            CleanType = reader.SafeRead<string>("cleanType"),
                            WasherNo = reader.SafeRead<string>("washerNo"),
                            WasherName = reader.SafeRead<string>("washerName"),
                            AutoCleanNo = reader.SafeRead<string>("autoCleanNo")
                        };

                        endoscopeclean.WashDate = reader.SafeRead<DateTime>("washDate");

                        if (string.Equals(endoscopeclean.CleanType.Trim(), "1"))
                        {
                            endoscopeclean.BrushWashBegin = reader.SafeRead<TimeSpan>("brushWashBegin");
                            endoscopeclean.BrushWashEnd = reader.SafeRead<TimeSpan>("brushWashEnd");
                            endoscopeclean.FirstWashBegin = reader.SafeRead<TimeSpan>("firstWashBegin");
                            endoscopeclean.FirstWashEnd = reader.SafeRead<TimeSpan>("firstWashEnd");
                            endoscopeclean.EnzymeWashBegin = reader.SafeRead<TimeSpan>("enzymeWashBegin");
                            endoscopeclean.EnzymeWashEnd = reader.SafeRead<TimeSpan>("enzymeWashEnd");
                            endoscopeclean.CleanOutBegin = reader.SafeRead<TimeSpan>("cleanOutWashBegin");
                            endoscopeclean.CleanOutEnd = reader.SafeRead<TimeSpan>("cleanOutWashEnd");
                            endoscopeclean.DipInBegin = reader.SafeRead<TimeSpan>("dipInWashBegin");
                            endoscopeclean.DipInEnd = reader.SafeRead<TimeSpan>("dipInwashEnd");
                            endoscopeclean.LastWashBegin = reader.SafeRead<TimeSpan>("lastWashBegin");
                            endoscopeclean.LastWashEnd = reader.SafeRead<TimeSpan>("lastWashEnd");
                        }

                        if (string.Equals(endoscopeclean.CleanType.Trim(), "2"))
                        {
                            endoscopeclean.DipInSecBegin = reader.SafeRead<TimeSpan>("dipInWashSecBegin");
                            endoscopeclean.DipInSecEnd = reader.SafeRead<TimeSpan>("dipInWashSecEnd");
                            endoscopeclean.LastWashSecBegin = reader.SafeRead<TimeSpan>("lastWashSecBegin");
                            endoscopeclean.LastWashSecEnd = reader.SafeRead<TimeSpan>("lastWashSecEnd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("获取最近清洗信息失败", ex);
                throw new Exception("获取最近清洗信息失败");
            }

            return endoscopeclean;
        }
        #endregion

        #region  Insert trace data records
        /// <summary>
        /// 将临时记录写入终表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="endocscopeSn">内镜编号</param>
        /// <param name="recordTime">The record time.</param>
        /// <returns>
        /// 写入是否成功
        /// </returns>
        public bool RecordTraceData(string tableName, string endocscopeSn, string recordTime)
        {
            var endoTemp = new EndoscopeTemp();
            SqlTransaction tran = null;
            object tempobj = null;

            // 验证isRead是否为null 如果为null说明数据是新数据，如果非null则说明该数据已经记录过
            var sql = string.Format("select isRecord from endoscopeTemp where endoscopeSn='{0}'", endocscopeSn);
            tempobj = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, sql);

            // 保证返回数据不为空
            if (tempobj == null)
            {
                return false;
            }

            // 验证isRead是否为null，不为null记录数据失败
            if (!string.IsNullOrEmpty(tempobj.ToString()))
            {
                return false;
            }

            try
            {
                // 读取endoscopeTemp数据
                endoTemp = this.ReadRecordTemp(endocscopeSn);
            }
            catch (Exception ex)
            {
                // ReadRecordTemp内部已经记录异常，所以直接抛出
                throw ex;
            }

            if (endoTemp == null)
            {
                return false;
            }

            using (var conn = new SqlConnection(Global.ConnStr))
            {
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    string insertsql;
                    int result = 0;
                    // 更新endoscopeTemp 的isRead 字段为非null
                    sql = string.Format(@"update endoscopeTemp set isRecord='1' where endoscopeSn='{0}'", endocscopeSn);

                    if (SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, sql) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Global.Log("设置内镜已经记录标识失败", ex);
                    tran.Rollback();
                    throw new Exception("设置内镜已经记录标识失败");
                }

                int tmp = 0;

                try
                {
                    // 插入endoscopeRecordInfo
                    tmp = this.InsertToRecordInfo(endoTemp, tran);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }

                if (tmp <= 0)
                {
                    tran.Rollback();
                }

                // 查询endoscopeRecordInfo的id
                sql = string.Format("select  MAX(id) from endoscopeRecordInfo where endoscopeSn='{0}'", endoTemp.EndoscopeSn);
                tempobj = SqlHelper.ExecuteScalar(tran, CommandType.Text, sql);
                int recordiinfoid = 0;

                if (!int.TryParse(tempobj.ToString(), out recordiinfoid))
                {
                    tran.Rollback();
                    return false;
                }

                // 查询上一个使用该镜子的病人
                sql = string.Format("select top 1 patientNo from endoscopeRecord where endoscopeSn='{0}' order by id desc", endoTemp.EndoscopeSn);
                var patientnum = SqlHelper.ExecuteScalar(tran, CommandType.Text, sql);

                if (patientnum == null)
                {
                    patientnum = string.Empty;
                }



                try
                {
                    // 插入endoscopeRecord
                    tmp = this.InsertToRecord(endoTemp, tran, recordiinfoid, patientnum);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (tmp >= 0)
                {
                    tran.Commit();
                    return true;
                }

                tran.Rollback();
            }

            return false;
        }
        #endregion

        #region Is bind to a cleaner
        /// <summary>
        /// 是否绑定员工
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <returns>是否绑定</returns>
        public bool IsBindingWaher(string endoscopeSn)
        {
            var selectsql = string.Format("select washerNo from endoscopeTemp where endoscopeSn='{0}'", endoscopeSn);
            object temp = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, selectsql);

            if (temp == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Is exist a data
        /// <summary>
        /// 是否存在具体的一条数据
        /// </summary>
        /// <param name="endoscopeSn">内镜编码</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="col">列名</param>
        /// <returns>
        /// 查找结果
        /// </returns>
        public bool IsExitOneColumn(string endoscopeSn, string tableName, string col)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = "endoscopeTemp";
            }

            if (string.IsNullOrEmpty(col))
            {
                return false;
            }

            var selectsql = string.Format("select {0} from {1} where endoscopeSn='{2}'", col, tableName, endoscopeSn);
            object temp = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, selectsql);

            if (temp == null || string.IsNullOrEmpty(temp.ToString()))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region  Set cleanning qualified
        /// <summary>
        /// 设置清洗是否合格
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <param name="qulified">合格</param>
        /// <param name="col">设置的列</param>
        /// <returns>
        /// 设置结果
        /// </returns>
        public bool SetOneTable(string endoscopeSn, string qulified, string col)
        {
            if (string.IsNullOrEmpty(col))
            {
                return false;
            }

            return this.Update("endoscopeTemp", col, qulified, "endoscopeSn", endoscopeSn);
        }
        #endregion

        #region Insert a endoscope information into table
        /// <summary>
        /// 插入内镜信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="endoscopeInfo">内镜信息</param>
        /// <returns>是否成功</returns>
        public bool ModifyEndoscope(string tableName, EndoscopeInfo endoscopeInfo)
        {
            int result = 0;
            var selectsql = string.Format("select id from endoscopeInfo where endoscopeSn='{0}'", endoscopeInfo.EndoscopeSn);
            object temp = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, selectsql);
            SqlParameter[] paras = 
            {
                this.GetSqlParameter("endoSn",endoscopeInfo.EndoscopeSn,DbType.String),
                this.GetSqlParameter("endoSim",endoscopeInfo.EndoscopeSim,DbType.String),
                this.GetSqlParameter("endoType",endoscopeInfo.EndoscopeType,DbType.String),
                this.GetSqlParameter("endoTypeName",endoscopeInfo.EndoscopeName,DbType.String),
                this.GetSqlParameter("endoSerYear",endoscopeInfo.EndoscopeServYear,DbType.Int32),
                this.GetSqlParameter("endoUseTime",endoscopeInfo.EndoscopeUseTime,DbType.DateTime),
                this.GetSqlParameter("endoSeal",endoscopeInfo.EndoscopeSeal,DbType.String),
                this.GetSqlParameter("remark",endoscopeInfo.Remark,DbType.String)
            };

            using (var conn = new SqlConnection(Global.ConnStr))
            {
                conn.Open();

                if (temp == null)
                {
                    var tran = conn.BeginTransaction();
                    var insertsql = @"insert into endoscopeInfo (
                                    endoscopeSn,endoscopeSim,endoscopeType,endoscopeName,endoscopeServYear,
                                    endoscopeUseTime,endoscopeSeal,remark) values(
                                    @endoSn,@endoSim,@endoType,@endoTypeName,@endoSerYear,@endoUseTime,@endoSeal,@remark)";
                    try
                    {
                        result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, insertsql, paras);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("插入endoscopeInfo数据失败", ex);
                        tran.Rollback();
                        throw new Exception("插入endoscopeInfo数据失败");
                    }

                    if (result <= 0)
                    {
                        tran.Rollback();
                    }

                    insertsql = string.Format("insert into endoscopeTemp (endoscopeSn,endoscopeSim,cleanType) values('{0}','{1}','{2}')", endoscopeInfo.EndoscopeSn, endoscopeInfo.EndoscopeSim, 1);

                    try
                    {
                        result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, insertsql);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("插入endoscopeTemp数据失败", ex);
                        tran.Rollback();
                        throw new Exception("插入endoscopeInfo数据失败");
                    }

                    if (result > 0)
                    {
                        tran.Commit();
                        return true;
                    }

                    tran.Rollback();
                }
                else
                {
                    var updatesql = string.Format(@"update endoscopeInfo set endoscopeSn=@endoSn,endoscopeSim=@endoSim,endoscopeType=@endoType,
                                                    endoscopeName=@endoTypeName,endoscopeServYear=@endoSerYear,endoscopeUseTime=@endoUseTime,
                                                    endoscopeSeal=@endoSeal,remark=@remark where endoscopeSn='{0}'",
                                                    endoscopeInfo.EndoscopeSn);
                    try
                    {
                        result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, updatesql, paras);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("更新endoscopeInfo信息失败", ex);
                        throw new Exception("更新endoscopeInfo信息失败");
                    }

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
        #endregion

        #region Insert users into user table
        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="user">用户</param>
        /// <returns>是否成功</returns>
        public bool ModifyUser(string tableName, UserInfo user)
        {
            var selectsql = string.Format("select * from userInfo where userSn='{0}'", user.UserSn);
            object temp = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, selectsql);
            SqlParameter[] paras =
            {
                    this.GetSqlParameter("@userSn",user.UserSn,DbType.String),
                    this.GetSqlParameter("@userSIM",user.UserSim,DbType.String),
                    this.GetSqlParameter("@userPass",user.UserPass,DbType.String),
                    this.GetSqlParameter("@userName",user.UserName,DbType.String),
                    this.GetSqlParameter("@userRole",user.UserRole,DbType.Int32),
                    this.GetSqlParameter("@userTime",DateTime.Now,DbType.DateTime),
                    this.GetSqlParameter("@userType",((int)user.UserType) == 1 ? 9 : 1,DbType.Int32),
                    this.GetSqlParameter("@remark",user.Remark,DbType.String)
            };

            try
            {
                if (temp == null)
                {
                    var insertsql = @"insert into userInfo (userSn,userSim,userPass,userName,userTime,userRole,userType,remark)
                                values (@userSn,@userSim,@userPass,@userName,@userTime,@userRole,@userType,@remark)";
                    int result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, insertsql, paras);

                    if (result > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    var updateSql = string.Format(@"update userInfo set userSn=@userSn,userSim=@userSim , userPass=@userPass,userName=@userName,
                                    userTime=@userTime,userRole=@userRole,userType=@userType,remark=@remark where userSn='{0}'",
                                                                                                                                   user.UserSn);
                    int result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, updateSql, paras);

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("更新用户数据失败", ex);
                throw new Exception("更新用户数据失败");
            }

            return false;
        }
        #endregion

        #region Delete user
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>
        /// 执行结果
        /// </returns>
        public bool DeleteUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return false;
            }
            return this.Delete("userInfo", "userSn", condition);
        }
        #endregion

        #region Delete endoscope
        /// <summary>
        /// 删除内镜
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>删除结果</returns>
        public bool DeleteEndoscope(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return false;
            }

            using (var conn = new SqlConnection(Global.ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                if (!this.Delete(tran, "endoscopeInfo", "endoscopeSn", condition))
                {
                    tran.Rollback();
                    return false;
                }

                if (!this.Delete(tran, "endoscopeTemp", "endoscopeSn", condition))
                {
                    tran.Rollback();
                    return false;
                }

                tran.Commit();
            }

            return true;
        }
        #endregion

        #region load user information
        /// <summary>
        /// 加载用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public List<UserInfo> LoadUsers()
        {
            var seletsql = "select * from userInfo";
            List<UserInfo> userinfos = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, seletsql))
            {
                if (!reader.HasRows)
                {
                    return userinfos;
                }

                userinfos = new List<UserInfo>();

                while (reader.Read())
                {
                    try
                    {
                        var userInfo = new UserInfo
                        {
                            UserSn = reader.SafeRead<string>("userSn"),
                            Id = reader.SafeRead<int>("id"),
                            UserName = reader.SafeRead<string>("userName"),
                            UserPass = reader.SafeRead<string>("userPass"),
                            UserSim = reader.SafeRead<string>("userSim"),
                            UserRole = reader.SafeRead<ClassLibrary.UserRole>("userRole"),
                            UserTime = reader.SafeRead<DateTime>("userTime"),
                            UserType = reader.SafeRead<ClassLibrary.UserType>("userType"),
                            Remark = reader.SafeRead<string>("remark")
                        };

                        userinfos.Add(userInfo);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("加载用户信息失败", ex);
                    }
                }
            }

            return userinfos;
        }
        #endregion

        #region  Load endoscope information
        /// <summary>
        /// Loads the endoscopes.
        /// </summary>
        /// <returns>
        /// 内镜信息
        /// </returns>
        public List<EndoscopeInfo> LoadEndoscopes()
        {
            var selectsql = "select * from endoscopeInfo";
            List<EndoscopeInfo> endoscopes = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, selectsql))
            {
                if (!reader.HasRows)
                {
                    return endoscopes;
                }

                endoscopes = new List<EndoscopeInfo>();
                while (reader.Read())
                {
                    var endoscopeInfo = new EndoscopeInfo
                    {
                        EndoscopeSn = reader.SafeRead<string>("endoscopeSn"),
                        Id = reader.SafeRead<int>("id"),
                        EndoscopeSim = reader.SafeRead<string>("endoscopeSim"),
                        EndoscopeSeal = reader.SafeRead<string>("endoscopeSeal"),
                        EndoscopeServYear = reader.SafeRead<int>("endoscopeServYear"),
                        EndoscopeType = reader.SafeRead<string>("endoscopeType"),
                        EndoscopeName = reader.SafeRead<string>("endoscopeName"),
                        EndoscopeUseTime = reader.SafeRead<DateTime>("endoscopeUseTime"),
                        Remark = reader.SafeRead<string>("remark")
                    };

                    endoscopes.Add(endoscopeInfo);
                }
            }

            return endoscopes;
        }
        #endregion

        #region Trace the cleanning  data
        /// <summary>
        /// 查询追溯结果
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>
        /// XML结果
        /// </returns>
        public string SearchResult(string sql)
        {
            DataSet dataSet = SqlHelper.ExecuteDataset(Global.ConnStr, CommandType.Text, sql);

            if (dataSet == null)
            {
                return string.Empty;
            }

            return dataSet.GetXml();
        }
        #endregion

        #region First clean
        /// <summary>
        /// 一次清洗
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>
        /// 一次清洗结果
        /// </returns>
        public string SearchFirstWash(string beginTime, string endTime)
        {
            var select = string.Format(@"select a.*,b.* from endoscopeRecord as a left join endoscopeRecordInfo as b
                                        on a.recordInfoId=b.id where a.cleanType='1' and a.recordTime between '{0}' and '{1}' 
                                        order by a.recordTime desc",
                                                                   beginTime,
                                                                   endTime);
            DataTable table = this.CreateNewDataTable("1");

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, select))
            {
                if (!reader.HasRows)
                {
                    return string.Empty;
                }

                while (reader.Read())
                {
                    try
                    {

                        DataRow row = table.NewRow();
                        row[0] = reader.SafeRead<string>("endoscopeSn");
                        row[1] = reader.SafeRead<string>("washerName");
                        row[2] = reader.SafeRead<DateTime>("washDate").ToLongDateString();
                        row[3] = reader.SafeRead<string>("washTotalTime");
                        row[4] = reader.SafeRead<TimeSpan>("washBeginTime");
                        row[5] = reader.SafeRead<TimeSpan>("washEndTime");
                        row[6] = reader.SafeRead<string>("autoCleanNo");
                        row[7] = this.VerifyData(reader.SafeRead<TimeSpan>("brushWashEnd"), reader.SafeRead<TimeSpan>("brushWashBegin"));
                        row[8] = this.VerifyData(reader.SafeRead<TimeSpan>("firstWashEnd"), reader.SafeRead<TimeSpan>("firstWashBegin"));
                        row[9] = this.VerifyData(reader.SafeRead<TimeSpan>("enzymeWashEnd"), reader.SafeRead<TimeSpan>("enzymeWashBegin"));
                        row[10] = this.VerifyData(reader.SafeRead<TimeSpan>("cleanOutWashEnd"), reader.SafeRead<TimeSpan>("cleanOutWashBegin"));
                        row[11] = this.VerifyData(reader.SafeRead<TimeSpan>("dipInWashEnd"), reader.SafeRead<TimeSpan>("dipInWashBegin"));
                        row[12] = this.VerifyData(reader.SafeRead<TimeSpan>("lastWashEnd"), reader.SafeRead<TimeSpan>("lastWashBegin"));
                        row[13] = reader.SafeRead<string>("wareNo");
                        row[14] = reader.SafeRead<string>("doctorName");
                        row[15] = reader.SafeRead<string>("patientNo");
                        row[16] = reader.SafeRead<string>("prePatientNo");
                        row[17] = reader.SafeRead<short>("qualified");
                        row[18] = reader.SafeRead<string>("nurseNo");
                        table.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("一次清洗查找错误", ex);
                        throw new Exception("一次清洗查找错误");
                    }
                }
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(table);
            return dataSet.GetXml();
        }
        #endregion

        #region Second clean
        /// <summary>
        /// 二次清洗
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>
        /// 二次清洗结果
        /// </returns>
        public string SearchSecondWash(string beginTime, string endTime)
        {
            var select = string.Format(@"select a.*,b.* from endoscopeRecord as a left join endoscopeRecordInfo as b 
                                        on a.recordInfoId=b.id where a.cleanType='2' and a.recordTime between '{0}' and '{1}' 
                                        order by a.recordTime desc",
                                beginTime,
                                endTime);
            DataTable table = this.CreateNewDataTable("2");

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, select))
            {
                if (!reader.HasRows)
                {
                    return string.Empty;
                }

                while (reader.Read())
                {
                    try
                    {
                        DataRow row = table.NewRow();
                        row[0] = reader.SafeRead<string>("endoscopeSn");
                        row[1] = reader.SafeRead<string>("washerName");
                        row[2] = reader.SafeRead<DateTime>("washDate").ToLongDateString();
                        row[3] = reader.SafeRead<string>("washTotalTime");
                        row[4] = reader.SafeRead<TimeSpan>("washBeginTime");
                        row[5] = reader.SafeRead<TimeSpan>("washEndTime");
                        row[6] = reader.SafeRead<string>("autoCleanNo");
                        row[7] = this.VerifyData(reader.SafeRead<TimeSpan>("dipInWashSecEnd"), reader.SafeRead<TimeSpan>("dipInWashSecBegin"));
                        row[8] = this.VerifyData(reader.SafeRead<TimeSpan>("lastWashSecEnd"), reader.SafeRead<TimeSpan>("lastWashSecBegin"));
                        row[9] = reader.SafeRead<string>("wareNo");
                        row[10] = reader.SafeRead<string>("doctorName");
                        row[11] = reader.SafeRead<string>("patientNo");
                        row[12] = reader.SafeRead<string>("prePatientNo");
                        row[13] = reader.SafeRead<short>("qualified");
                        row[14] = reader.SafeRead<string>("nurseNo");
                        table.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        Global.Log("二次清洗查找错误", ex);
                        throw new Exception("二次清洗查找错误");
                    }
                }
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(table);
            return dataSet.GetXml();
        }
        #endregion

        #region  Get clean result by condition
        /// <summary>
        /// 根据搜索条件获取内容
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="value">值</param>
        /// <returns>查询结果集</returns>
        public string GetSearchResultByName(string condition, string value)
        {
            var select = string.Format(@"select a.*,b.* from endoscopeRecord as a left join endoscopeRecordInfo as b 
                                        on a.recordInfoId=b.id where {0} {1} order by a.recordTime desc",
                                        condition,
                                        value);
            DataTable t1 = this.CreateNewDataTable("1");
            DataTable t2 = this.CreateNewDataTable("2");
            var set = new DataSet();

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, select))
            {
                if (!reader.HasRows)
                {
                    return string.Empty;
                }

                while (reader.Read())
                {
                    if (string.Equals(reader.SafeRead<string>("cleanType"), "1"))
                    {
                        try
                        {
                            DataRow row = t1.NewRow();
                            row[0] = reader.SafeRead<string>("endoscopeSn");
                            row[1] = reader.SafeRead<string>("washerName");
                            row[2] = reader.SafeRead<DateTime>("washDate").ToLongDateString();
                            row[3] = reader.SafeRead<string>("washTotalTime");
                            row[4] = reader.SafeRead<TimeSpan>("washBeginTime");
                            row[5] = reader.SafeRead<TimeSpan>("washEndTime");
                            row[6] = reader.SafeRead<string>("autoCleanNo");
                            row[7] = this.VerifyData(reader.SafeRead<TimeSpan>("brushWashEnd"), reader.SafeRead<TimeSpan>("brushWashBegin"));
                            row[8] = this.VerifyData(reader.SafeRead<TimeSpan>("firstWashEnd"), reader.SafeRead<TimeSpan>("firstWashBegin"));
                            row[9] = this.VerifyData(reader.SafeRead<TimeSpan>("enzymeWashEnd"), reader.SafeRead<TimeSpan>("enzymeWashBegin"));
                            row[10] = this.VerifyData(reader.SafeRead<TimeSpan>("cleanOutWashEnd"), reader.SafeRead<TimeSpan>("cleanOutWashBegin"));
                            row[11] = this.VerifyData(reader.SafeRead<TimeSpan>("dipInWashEnd"), reader.SafeRead<TimeSpan>("dipInWashBegin"));
                            row[12] = this.VerifyData(reader.SafeRead<TimeSpan>("lastWashEnd"), reader.SafeRead<TimeSpan>("lastWashBegin"));
                            row[13] = reader.SafeRead<string>("wareNo");
                            row[14] = reader.SafeRead<string>("doctorName");
                            row[15] = reader.SafeRead<string>("patientNo");
                            row[16] = reader.SafeRead<string>("prePatientNo");
                            row[17] = reader.SafeRead<short>("qualified");
                            row[18] = reader.SafeRead<string>("nurseNo");
                            t1.Rows.Add(row);
                        }
                        catch (Exception ex)
                        {
                            Global.Log("一次清洗查找错误", ex);
                            throw new Exception("一次清洗查找错误");
                        }
                    }
                    else
                    {
                        try
                        {
                            DataRow row = t2.NewRow();
                            row[0] = reader.SafeRead<string>("endoscopeSn");
                            row[1] = reader.SafeRead<string>("washerName");
                            row[2] = reader.SafeRead<DateTime>("washDate").ToLongDateString();
                            row[3] = reader.SafeRead<string>("washTotalTime");
                            row[4] = reader.SafeRead<TimeSpan>("washBeginTime");
                            row[5] = reader.SafeRead<TimeSpan>("washEndTime");
                            row[6] = reader.SafeRead<string>("autoCleanNo");
                            row[7] = this.VerifyData(reader.SafeRead<TimeSpan>("dipInWashSecEnd"), reader.SafeRead<TimeSpan>("dipInWashSecBegin"));
                            row[8] = this.VerifyData(reader.SafeRead<TimeSpan>("lastWashSecEnd"), reader.SafeRead<TimeSpan>("lastWashSecBegin"));
                            row[9] = reader.SafeRead<string>("wareNo");
                            row[10] = reader.SafeRead<string>("doctorName");
                            row[11] = reader.SafeRead<string>("patientNo");
                            row[12] = reader.SafeRead<string>("prePatientNo");
                            row[13] = reader.SafeRead<short>("qualified");
                            row[14] = reader.SafeRead<string>("nurseNo");
                            t2.Rows.Add(row);
                        }
                        catch (Exception ex)
                        {
                            Global.Log("二次清洗查找错误", ex);
                            throw new Exception("二次清洗查找错误");
                        }
                    }
                }
            }

            set.Tables.Add(t1);
            set.Tables.Add(t2);
            return set.GetXml();
        }
        #endregion

        #region Login
        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <returns></returns>
        public string LoginUser(string userName, string passWord)
        {
            string sql = string.Format("select * from userInfo where (userSn='{0}' or userSim='{1}') and userPass='{2}'", userName, userName, passWord);

            try
            {
                using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
                {
                    if (!reader.HasRows)
                    {
                        return string.Empty;
                    }

                    while (reader.Read())
                    {
                        string role = reader.SafeRead<int>("userRole").ToString();
                        string name = reader.SafeRead<string>("userName").ToString();

                        return string.Format("{0}|{1}", name, role);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("用户登录失败", ex);
                throw new Exception("用户登录失败");
            }

            return string.Empty;
        }
        #endregion

        #region Register　patient info
        /// <summary>
        /// Registers the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns>登记结果</returns>
        public bool RegisterPatient(Patient patient)
        {
            string sql = string.Format(@"select patientName from patientRecord where patientId='{0}'", patient.PatientId);
            var temp = SqlHelper.ExecuteScalar(Global.ConnStr, CommandType.Text, sql);
            var paras = new SqlParameter[]
                {
                    this.GetSqlParameter("@patientId",patient.PatientId),
                    this.GetSqlParameter("@patientSN",patient.PatientSN),
                    this.GetSqlParameter("@patientName",patient.PatientName),
                    this.GetSqlParameter("@patientPhoneNo",patient.PatientPhoneNo),
                    this.GetSqlParameter("@patientAddress",patient.PatientAddr),
                    this.GetSqlParameter("@remark",patient.Remark),
                    this.GetSqlParameter("@sex",patient.Sex),
                    this.GetSqlParameter("@time",DateTime.Now.ToString()),
                    this.GetSqlParameter("@positive",patient.IsPositive),
                    this.GetSqlParameter("@age",patient.Age),
                    this.GetSqlParameter("@endoscope",patient.Endoscope)
                };

            try
            {
                // 插入
                if (temp == null)
                {

                    sql = @"insert into patientRecord (patientSn,patientName,patientPhoneNo,patientAddress,remark,endoscope,sex,time,positive,age) 
                        values(@patientSN,@patientName,@patientPhoneNo,@patientAddress,@remark,@endoscope,@sex,@time,@positive,@age)";
                    int result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, sql, paras);

                    if (result > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    sql = @"update patientRecord set  patientName=@patientName,patientPhoneNo=@patientPhoneNo,patientAddress=@patientAddress,remark=@remark,endoscope=@endoscope,sex=@sex,time=@time,positive=@positive,age=@age where patientId=@patientId";
                    int result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, sql, paras);

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("注册病人信息失败", ex);
                throw new Exception("注册病人信息失败");
            }

            return false;
        }
        #endregion

        #region Load patients
        /// <summary>
        /// Loads the patients.
        /// </summary>
        /// <returns>病人列表</returns>
        public List<Patient> LoadPatients()
        {
            var sql = "select * from patientRecord where examined is null";
            List<Patient> patients = null;

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return new List<Patient>();
                }

                patients = new List<Patient>();

                try
                {
                    while (reader.Read())
                    {
                        var patient = new Patient();
                        patient.PatientId = reader.SafeRead<int>("patientId").ToString();
                        patient.PatientSN = reader.SafeRead<string>("patientSn");
                        patient.PatientSex = reader.SafeRead<string>("sex");
                        patient.PatientName = reader.SafeRead<string>("patientName");
                        patient.PatientPhoneNo = reader.SafeRead<string>("patientPhoneNo");
                        patient.PatientAddr = reader.SafeRead<string>("patientAddress");
                        patient.Remark = reader.SafeRead<string>("remark");
                        patient.Endoscope = reader.SafeRead<string>("endoscope");
                        patient.Age = reader.SafeRead<string>("age");
                        patient.Sex = reader.SafeRead<string>("sex");
                        patient.IsPositive = reader.SafeRead<bool>("positive");
                        patients.Add(patient);
                    }
                }
                catch (Exception ex)
                {
                    Global.Log("加载病人信息失败", ex);
                    throw new Exception("加载病人信息失败", ex);
                }
            }

            return patients;
        }
        #endregion

        #region Delete patients
        /// <summary>
        /// Deletes the patients.
        /// </summary>
        /// <param name="sn">The sn.</param>
        /// <returns></returns>
        public bool DeletePatients(string sn)
        {
            return this.Update("patientRecord", "examined", "0", "patientSn", sn);
        }
        #endregion

        #region Update trace record
        /// <summary>
        /// Updates the trac data.
        /// </summary>
        /// <returns>更新结果</returns>
        public bool UpdateTracData(string endoscop, string wareNo, string doctorName, string patientNO, string patientname, string nurseNO, string patientId)
        {
            bool result = false;
            string sql = string.Format("select id from endoscopeRecord where (endoscopeSn='{0}' or endoscopeSim='{1}') and  (len(ltrim(rtrim(wareNo)))=0 or wareNo is null) order by id desc", endoscop, endoscop);
            SqlTransaction tran = null;

            using (SqlConnection con = new SqlConnection(Global.ConnStr))
            {
                con.Open();
                tran = con.BeginTransaction();
                var obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, sql);

                if (obj == null)
                {
                    tran.Rollback();
                    return result;
                }

                sql = string.Format("update endoscopeRecord set wareNo='{0}',doctorName='{1}',patientNo='{2}',nurseNo='{3}',isUpdated='1' where id='{4}'", wareNo, doctorName, patientNO + "|" + patientname, nurseNO, obj);
                int tmp = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);

                if (tmp <= 0)
                {
                    tran.Rollback();
                    return result;
                }

                sql = string.Format("update patientRecord set examined='1' where patientId='{0}'", patientId);
                tmp = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);

                if (tmp <= 0)
                {
                    tran.Rollback();
                    return result;
                }

                result = true;
                tran.Commit();
                return result;
            }
        }
        #endregion

        #region Load endoscope names
        /// <summary>
        /// Loads the endsocope names.
        /// </summary>
        /// <returns></returns>
        public List<string> LoadEndsocopeNames()
        {
            var sql = string.Format("select endoscopeSN from endoscopeInfo ");
            var list = new List<string>();

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return list;
                }

                while (reader.Read())
                {
                    var tmp = reader.SafeRead<string>("endoscopeSn");
                    list.Add(tmp);
                }
            }

            return list;
        }
        #endregion

        #region Load ware infos
        /// <summary>
        /// Loads the wareinfos.
        /// </summary>
        /// <returns></returns>
        public List<string> LoadWareinfos()
        {
            var sql = string.Format("select wareName from wareInfo ");
            var list = new List<string>();

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return list;
                }

                while (reader.Read())
                {
                    var tmp = reader.SafeRead<string>("wareName");
                    list.Add(tmp);
                }
            }

            return list;
        }
        #endregion

        #region Load user infos
        /// <summary>
        /// Loads the user infos.
        /// </summary>
        /// <param name="userRole">The user role.</param>
        /// <returns></returns>
        public List<string> LoadUserInfos(UserRole userRole)
        {
            var sql = string.Format("select * from userInfo where userRole='{0}' ", (int)userRole);
            var list = new List<string>();

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return list;
                }

                while (reader.Read())
                {
                    var tmp = reader.SafeRead<string>("userName");
                    list.Add(tmp);
                }
            }

            return list;
        }
        #endregion

        #region Check endoscope clean quality
        /// <summary>
        /// Determines whether the specified length is quality.
        /// </summary>
        /// <param name="en">The length.</param>
        /// <returns></returns>
        public bool EndoscopeIsQuality(string en)
        {
            var sql = string.Format("select * from endoscopeRecord where endoscopeSn='{0}' order by id desc", en);

            using (var reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return false;
                }

                try
                {
                    while (reader.Read())
                    {
                        var updated = reader.SafeRead<int>("isUpdated");
                        var quality = reader.SafeRead<short>("qualified");

                        if (quality <= 0)
                        {
                            return false;
                        }

                        if (updated == 1)
                        {
                            return false;
                        }

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Global.Log("验证内镜是否可用失败", ex);
                    throw new Exception("验证内镜失败");
                }
            }

            return false;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// Deletes the specified table name.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>删除成功true；删除失败false</returns>
        private bool Delete(string tableName, string column, string value)
        {
            int result = 0;
            var deletesql = string.Format("delete from {0} where userSn='{1}'", tableName, column, value);

            try
            {
                result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, deletesql);
            }
            catch (Exception ex)
            {
                Global.Log("删除" + tableName + "数据失败", ex);
                throw new Exception("删除" + tableName + "数据失败");
            }

            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified tran.
        /// </summary>
        /// <param name="tran">The tran.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private bool Delete(SqlTransaction tran, string tableName, string column, string value)
        {
            int result = 0;
            var deletesql = string.Format("delete from {0} where userSn='{1}'", tableName, column, value);

            try
            {
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, deletesql);
            }
            catch (Exception ex)
            {
                Global.Log(string.Format("删除{0}数据失败", tableName), ex);
                tran.Rollback();
                throw new Exception(string.Format("删除{0}数据失败", tableName));
            }

            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="setCol">The set col.</param>
        /// <param name="setValue">The set value.</param>
        /// <param name="conCol">The where col.</param>
        /// <param name="conValue">The where value.</param>
        /// <returns>update result</returns>
        private bool Update(string tableName, string setCol, string setValue, string conCol, string conValue)
        {
            var updatesql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", tableName, setCol, setValue, conCol, conValue);
            int result = 0;

            try
            {
                result = SqlHelper.ExecuteNonQuery(Global.ConnStr, CommandType.Text, updatesql);
            }
            catch (Exception ex)
            {
                Global.Log(string.Format("更新{0}字段失败", tableName), ex);
                throw new Exception(string.Format("更新{0}字段失败", tableName));
            }

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 创建新的DataTable 转化成XML
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns>返回构建好的DataTable</returns>
        private DataTable CreateNewDataTable(string flag)
        {
            DataTable temp = new DataTable();
            DataColumn endoscopeSN = new DataColumn();
            endoscopeSN.ColumnName = "endoscopeSn";
            temp.Columns.Add(endoscopeSN);
            DataColumn washerRealName = new DataColumn();
            washerRealName.ColumnName = "washerName";
            temp.Columns.Add(washerRealName);
            DataColumn washDate = new DataColumn();
            washDate.ColumnName = "washDate";
            temp.Columns.Add(washDate);
            DataColumn washTotalTime = new DataColumn();
            washTotalTime.ColumnName = "washTotalTime";
            temp.Columns.Add(washTotalTime);
            DataColumn washBeginTime = new DataColumn();
            washBeginTime.ColumnName = "washBeginTime";
            temp.Columns.Add(washBeginTime);
            DataColumn washEndTime = new DataColumn();
            washEndTime.ColumnName = "washEndTime";
            temp.Columns.Add(washEndTime);
            DataColumn autoCleanNo = new DataColumn();
            autoCleanNo.ColumnName = "autoCleanNo";
            temp.Columns.Add(autoCleanNo);

            if (flag == "1")
            {
                DataColumn brushWashTime = new DataColumn();
                brushWashTime.ColumnName = "brushWashTime";
                temp.Columns.Add(brushWashTime);
                DataColumn firstWashTime = new DataColumn();
                firstWashTime.ColumnName = "firstWashTime";
                temp.Columns.Add(firstWashTime);
                DataColumn enzymeWashTime = new DataColumn();
                enzymeWashTime.ColumnName = "enzymeWashTime";
                temp.Columns.Add(enzymeWashTime);
                DataColumn cleanOutWashTime = new DataColumn();
                cleanOutWashTime.ColumnName = "cleanOutWashTime";
                temp.Columns.Add(cleanOutWashTime);
                DataColumn dipInWashTime = new DataColumn();
                dipInWashTime.ColumnName = "dipInWashTime";
                temp.Columns.Add(dipInWashTime);

                DataColumn lastWashTime = new DataColumn();
                lastWashTime.ColumnName = "lastWashTime";
                temp.Columns.Add(lastWashTime);
                temp.TableName = "FirstWash";
            }
            else
            {
                DataColumn dipInWashSecTime = new DataColumn();
                dipInWashSecTime.ColumnName = "dipInWashSecTime";
                temp.Columns.Add(dipInWashSecTime);
                DataColumn lastWashSecTime = new DataColumn();
                lastWashSecTime.ColumnName = "lastWashSecTime";
                temp.Columns.Add(lastWashSecTime);
                temp.TableName = "SecondWash";
            }

            DataColumn wareNo = new DataColumn();
            wareNo.ColumnName = "wareNo";
            temp.Columns.Add(wareNo);
            DataColumn doctorName = new DataColumn();
            doctorName.ColumnName = "doctorName";
            temp.Columns.Add(doctorName);
            DataColumn patientNo = new DataColumn();
            patientNo.ColumnName = "patientNo";
            temp.Columns.Add(patientNo);
            DataColumn prePatientNo = new DataColumn();
            prePatientNo.ColumnName = "prePatientNo";
            temp.Columns.Add(prePatientNo);
            DataColumn qualified = new DataColumn();
            qualified.ColumnName = "qualified";
            temp.Columns.Add(qualified);
            DataColumn nurseNo = new DataColumn();
            nurseNo.ColumnName = "nurseNo";
            temp.Columns.Add(nurseNo);
            return temp;
        }

        /// <summary>
        /// 验证时间数据
        /// </summary>
        /// <param name="strEnd">结束时间</param>
        /// <param name="strBegin">开始时间</param>
        /// <returns>
        /// 时间段
        /// </returns>
        private string VerifyData(TimeSpan strEnd, TimeSpan strBegin)
        {
            string result = "00:00:00";

            try
            {
                if (strBegin < strEnd)
                {
                    result = (strEnd - strBegin).ToString();
                }
            }
            catch (Exception ex)
            {
                Global.Log("验证数据失败", ex);
                throw new Exception("验证数据失败");
            }

            return result;
        }

        /// <summary>
        /// Reads the record temporary.
        /// </summary>
        /// <param name="endoTemp">The endo temporary.</param>
        /// <param name="endocscopeSn">The endocscope sn.</param>
        private EndoscopeTemp ReadRecordTemp(string endocscopeSn)
        {
            EndoscopeTemp endoTemp = null;
            string sql = string.Format("select * from endoscopeTemp where endoscopeSn='{0}'", endocscopeSn);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(Global.ConnStr, CommandType.Text, sql))
            {
                if (!reader.HasRows)
                {
                    return endoTemp;
                }

                while (reader.Read())
                {
                    try
                    {
                        endoTemp = new EndoscopeTemp();
                        endoTemp.EndoscopeSn = reader.SafeRead<string>("endoscopeSn");
                        endoTemp.EndoscopeSim = reader.SafeRead<string>("endoscopeSim");
                        endoTemp.RecordTime = DateTime.Now;
                        endoTemp.BrushWashBegin = reader.SafeRead<TimeSpan>("brushWashBegin");
                        endoTemp.BrushWashEnd = reader.SafeRead<TimeSpan>("brushWashEnd");
                        endoTemp.FirstWashBegin = reader.SafeRead<TimeSpan>("firstWashBegin");
                        endoTemp.FirstWashEnd = reader.SafeRead<TimeSpan>("firstWashEnd");
                        endoTemp.EnzymeWashBegin = reader.SafeRead<TimeSpan>("enzymeWashBegin");
                        endoTemp.EnzymeWashEnd = reader.SafeRead<TimeSpan>("enzymeWashEnd");
                        endoTemp.CleanOutBegin = reader.SafeRead<TimeSpan>("cleanOutWashBegin");
                        endoTemp.CleanOutEnd = reader.SafeRead<TimeSpan>("cleanOutWashEnd");
                        endoTemp.DipInBegin = reader.SafeRead<TimeSpan>("dipInWashBegin");
                        endoTemp.DipInEnd = reader.SafeRead<TimeSpan>("dipInwashEnd");
                        endoTemp.LastWashBegin = reader.SafeRead<TimeSpan>("lastWashBegin");
                        endoTemp.LastWashEnd = reader.SafeRead<TimeSpan>("lastWashEnd");
                        endoTemp.DipInSecBegin = reader.SafeRead<TimeSpan>("dipInWashSecBegin");
                        endoTemp.DipInSecEnd = reader.SafeRead<TimeSpan>("dipInWashSecEnd");
                        endoTemp.LastWashSecBegin = reader.SafeRead<TimeSpan>("lastWashSecBegin");
                        endoTemp.LastWashSecEnd = reader.SafeRead<TimeSpan>("lastWashSecEnd");
                        endoTemp.Quality = reader.SafeRead<bool>("qualified");
                        endoTemp.WasherNo = reader.SafeRead<string>("washerNo");
                        endoTemp.WasherName = reader.SafeRead<string>("washerName");
                        endoTemp.CleanType = reader.SafeRead<string>("cleanType").Trim();
                        endoTemp.Disinfection = reader.SafeRead<bool>("disinfection");
                        endoTemp.WashDate = reader.SafeRead<DateTime>("washDate");
                        endoTemp.AutoCleanNo = reader.SafeRead<string>("autoCleanNo");

                        if (endoTemp.CleanType.Equals("1"))
                        {
                            endoTemp.TotalTime = (endoTemp.BrushWashEnd - endoTemp.BrushWashBegin + endoTemp.FirstWashEnd - endoTemp.FirstWashBegin + endoTemp.EnzymeWashEnd - endoTemp.EnzymeWashBegin + endoTemp.CleanOutEnd - endoTemp.CleanOutBegin + endoTemp.DipInEnd - endoTemp.DipInBegin + endoTemp.LastWashEnd - endoTemp.LastWashBegin).ToString();
                        }
                        else
                        {
                            endoTemp.TotalTime = (endoTemp.DipInSecEnd - endoTemp.DipInSecBegin + endoTemp.LastWashSecEnd - endoTemp.LastWashSecBegin).ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Global.Log("读取endoscopeTemp失败", ex);
                        throw new Exception("读取endoscopeTemp失败");
                    }
                }
            }

            return endoTemp;
        }

        /// <summary>
        /// Inserts the automatic record.
        /// </summary>
        /// <param name="endoTemp">The endo temporary.</param>
        /// <param name="tran">The tran.</param>
        /// <param name="recordiinfoid">The recordiinfoid.</param>
        /// <param name="patientnum">The patientnum.</param>
        /// <returns>成功true；失败false</returns>
        private int InsertToRecord(EndoscopeTemp endoTemp, SqlTransaction tran, int recordiinfoid, object patientnum)
        {
            int result = 0;
            string insertsql = @"insert into endoscopeRecord (
                              endoscopeSn,endoscopeSim,washerNo,washerName,recordInfoId,recordTime,
                              cleanType,disinfection,washDate,washTotalTime,washBeginTime,washEndTime,
                              autoCleanNo,prePatientNo,qualified,isUpdated) values(
                              @endoSn,@endoSim,@washerNo,@washerName,@recordInfoId,@recordTime,@cleanType,
                              @disinfection,@washDate,@washTotalTime,@washBeginTime,@washEndTime,@autoCleanNo,
                              @prePatientNo,@qualified,@isUpdated)";
            SqlParameter[] insertparas =
                {
                   this.GetSqlParameter("@endoSn",endoTemp.EndoscopeSn,DbType.String),
                   this.GetSqlParameter("@endoSim",endoTemp.EndoscopeSim,DbType.String),
                   this.GetSqlParameter("@washerNo",endoTemp.WasherNo,DbType.String),
                   this.GetSqlParameter("@washerName",endoTemp.WasherName,DbType.String),
                   this.GetSqlParameter("@recordInfoId",recordiinfoid.ToString(),DbType.String),
                   this.GetSqlParameter("@recordTime",endoTemp.RecordTime,DbType.DateTime),
                   this.GetSqlParameter("@cleanType",endoTemp.CleanType,DbType.String),
                   this.GetSqlParameter("@disinfection",endoTemp.Disinfection,DbType.Boolean),
                   this.GetSqlParameter("@washDate",endoTemp.WashDate,DbType.Date),
                   this.GetSqlParameter("@washTotalTime",endoTemp.TotalTime,DbType.String),
                   this.GetSqlParameter("@washBeginTime",endoTemp.CleanType == "1" ? endoTemp.BrushWashBegin : endoTemp.DipInSecBegin,SqlDbType.Time),
                   this.GetSqlParameter("@washEndTime",endoTemp.CleanType == "1" ? endoTemp.LastWashEnd : endoTemp.LastWashSecEnd,SqlDbType.Time),
                   this.GetSqlParameter("@autoCleanNo",endoTemp.AutoCleanNo,DbType.String),
                   this.GetSqlParameter("@prePatientNo",patientnum,DbType.String),
                   this.GetSqlParameter("@qualified",endoTemp.Quality,DbType.Boolean),
                   this.GetSqlParameter("@isUpdated",0,DbType.Int32),
                };

            try
            {
                // insert into Record with transaction
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, insertsql, insertparas);
            }
            catch (Exception ex)
            {
                Global.Log("插入endoscopeRecord失败", ex);
                throw new Exception("插入endoscopeRecord失败");
            }

            return result;
        }

        /// <summary>
        /// Inserts the automatic record information.
        /// </summary>
        /// <param name="endoTemp">The endo temporary.</param>
        /// <param name="tran">The tran.</param>
        /// <returns>成功true；失败false</returns>
        private int InsertToRecordInfo(EndoscopeTemp endoTemp, SqlTransaction tran)
        {
            int result = 0;

            // 插入endoscopeRecord
            var insertsql = @"insert into endoscopeRecordInfo (
                                endoscopeSn,endoscopeSim,recordTime,brushWashBegin,brushWashEnd,
                                firstWashBegin,firstWashEnd,enzymeWashBegin,enzymeWashEnd,cleanOutWashBegin,
                                cleanOutWashEnd,dipInWashBegin,dipInWashEnd,lastWashBegin,lastWashEnd,
                                dipInWashSecBegin,dipInWashSecEnd,lastWashSecBegin,lastWashSecEnd) 
                                values( @endoSn,@endoSim,@recordTime,@bruWashB,@bruWashE,
                                @firWashB,@firWashE,@enWashB,@enWashE,@cleanWashB,
                                @cleanWashE,@dipWashB,@dipWashE,@lastWashB,@lastWashE,
                                @dipWashSecB,@dipWashSecE,@lastWashSecB,@lastWashSecE)";
            SqlParameter[] paras = new SqlParameter[]
                {
                   this.GetSqlParameter("@endoSn",endoTemp.EndoscopeSn,DbType.String),
                   this.GetSqlParameter("@endoSim",endoTemp.EndoscopeSim,DbType.String),
                   this.GetSqlParameter("@recordTime",endoTemp.RecordTime,DbType.DateTime),
                   this.GetSqlParameter("@bruWashB",endoTemp.BrushWashBegin, SqlDbType.Time),
                   this.GetSqlParameter("@bruWashE",endoTemp.BrushWashEnd,SqlDbType.Time),
                   this.GetSqlParameter("@firWashB",endoTemp.FirstWashBegin,SqlDbType.Time),
                   this.GetSqlParameter("@firWashE",endoTemp.FirstWashEnd,SqlDbType.Time),
                   this.GetSqlParameter("@enWashB",endoTemp.EnzymeWashBegin,SqlDbType.Time),
                   this.GetSqlParameter("@enWashE",endoTemp.EnzymeWashEnd,SqlDbType.Time),
                   this.GetSqlParameter("@cleanWashB",endoTemp.CleanOutBegin,SqlDbType.Time),
                   this.GetSqlParameter("@cleanWashE",endoTemp.CleanOutEnd,SqlDbType.Time),
                   this.GetSqlParameter("@dipWashB",endoTemp.DipInBegin,SqlDbType.Time),
                   this.GetSqlParameter("@dipWashE",endoTemp.DipInEnd,SqlDbType.Time),
                   this.GetSqlParameter("@lastWashB",endoTemp.LastWashBegin,SqlDbType.Time),
                   this.GetSqlParameter("@lastWashE",endoTemp.LastWashEnd,SqlDbType.Time),
                   this.GetSqlParameter("@dipWashSecB",endoTemp.DipInSecBegin,SqlDbType.Time),
                   this.GetSqlParameter("@dipWashSecE",endoTemp.DipInSecEnd,SqlDbType.Time),
                   this.GetSqlParameter("@lastWashSecB",endoTemp.LastWashSecBegin,SqlDbType.Time),
                   this.GetSqlParameter("@lastWashSecE",endoTemp.LastWashSecEnd,SqlDbType.Time)
                };
            try
            {
                // Insert record into RecordInfo with transaction
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, insertsql, paras);
            }
            catch (Exception ex)
            {
                Global.Log("插入endoscopeRecordInfo失败", ex);
                throw new Exception("插入endoscopeRecordInfo失败");
            }

            return result;
        }
        #endregion
    }
}