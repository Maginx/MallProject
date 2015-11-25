using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ClassLibrary;
using MallHost.Data;
using MallHost;

namespace MallHost
{
    /// <summary>
    /// 业务处理程序
    /// </summary>
    internal sealed class DataBusiProcess
    {
        #region  私有对象
        /// <summary>
        /// 数据对象
        /// </summary>
        private static IDataBase dataObject = new DataBase();
        #endregion

        #region 静态方法
        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 验证结果
        /// </returns>
        public static bool ValidID(string username, string password)
        {
            if (string.Equals(username, ReadXML.Xmls["username"].ToString()) && string.Equals(password, ReadXML.Xmls["password"].ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 记录清洗数据
        /// </summary>
        /// <param name="step">清洗步骤</param>
        /// <param name="endoscopeSN">内镜卡号</param>
        /// <param name="time">时间</param>
        /// <returns>
        /// 记录结果，true为记录成功，false为失败
        /// </returns>
        public bool RecordStep(MallHost.Step step, string endoscopeSN, DateTime time)
        {
            if (string.IsNullOrEmpty(endoscopeSN))
            {
                return false;
            }

            bool markResult = false;

            try
            {
                // 是否内镜与清洗工已经绑定
                if (this.IsBindingWasher(endoscopeSN))
                {
                    switch (step)
                    {
                        case Step.RecordTime:
                            markResult = dataObject.RecordCleanTime(time.ToString(), endoscopeSN, "recordTime");
                            break;
                        case Step.ManualWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "brushWashBegin");
                            break;
                        case Step.ManualWashEndStep:
                        case Step.FirstWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "brushWashEnd");
                            markResult = markResult & dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "firstWashBegin");
                            break;
                        case Step.FirstWashEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "firstWashEnd");
                            break;
                        case Step.EnzymeWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "enzymeWashBegin");
                            break;
                        case Step.EnzymeWashEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "enzymeWashEnd");
                            break;
                        case Step.CleanOutWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "cleanOutWashBegin");
                            break;
                        case Step.CleanOutWashEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "cleanOutWashEnd");
                            break;
                        case Step.DipInWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "dipInWashBegin");
                            break;
                        case Step.DipInWashEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "dipInwashEnd");
                            break;
                        case Step.DipInWashSecBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "dipInWashSecBegin");
                            break;
                        case Step.DipInWashSecEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "dipInWashSecEnd");
                            break;
                        case Step.LastWashSecBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "lastWashSecBegin");
                            break;
                        case Step.LastWashSecEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "lastWashSecEnd");
                            break;
                        case Step.LastWashBeginStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "lastWashBegin");
                            break;
                        case Step.LastWashEndStep:
                            markResult = dataObject.RecordCleanTime(time.ToLongTimeString(), endoscopeSN, "lastWashEnd");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return markResult;
        }

        /// <summary>
        /// 根据SIM卡号获取User或者Endoscope的信息
        /// </summary>
        /// <param name="sim">磁卡号</param>
        /// <returns>
        /// 用户/内镜信息
        /// </returns>
        public string[] GetUserEndoscopeMsg(string sim)
        {
            if (string.IsNullOrEmpty(sim))
            {
                return new string[0];
            }

            var tempStr = new string[3];
            var en = new EndoscopeInfo();

            try
            {
                en = dataObject.GetEndoscopeMsgBySIM(sim, Global.EndoscopeInfo);
            }
            catch (Exception)
            {
                return new string[0];
            }

            if (en != null)
            {
                tempStr[0] = "endoscope";
                tempStr[1] = en.EndoscopeSn;
            }
            else
            {
                var u = new UserInfo();

                try
                {
                    u = dataObject.GetUserInfo(sim, Global.UserInfo);
                }
                catch (Exception)
                {
                    return new string[0];
                }

                if (u.UserName.Length > 0)
                {
                    tempStr[0] = "user";
                    tempStr[1] = u.UserSn;
                    tempStr[2] = u.UserName;
                }
                else
                {
                    tempStr[0] = "no";
                }
            }

            return tempStr;
        }

        /// <summary>
        /// 重置内镜信息
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <param name="endoscopeSIM">内镜卡号</param>
        /// <param name="washerNo">清洗员编号</param>
        /// <param name="washerRealName">清洗员名</param>
        /// <param name="cleanType">清洗类型</param>
        /// <param name="washDate">清洗日期</param>
        /// <param name="disinfection">消毒液</param>
        /// <param name="autoClean">自动清洗机编号</param>
        /// <returns>
        /// 返回重置结果
        /// </returns>
        public bool ResetEndoscope(string endoscopeSN, string endoscopeSIM, string washerNo, string washerRealName, string cleanType, string washDate, string disinfection, string autoClean)
        {
            bool result = false;

            try
            {
                result = dataObject.ResetEndoscope(endoscopeSN, endoscopeSIM, washerNo, washerRealName, cleanType, washDate, disinfection, autoClean);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 获取最新的一条记录
        /// </summary>
        /// <param name="endoscope">内镜编号</param>
        /// <returns>
        /// 返回内镜信息
        /// </returns>
        public EndoscopeTemp GetLatestCleanMSG(string endoscope)
        {
            var tmp = new EndoscopeTemp();

            try
            {
                tmp = dataObject.GetNewCleanMsg(endoscope);
            }
            catch (Exception)
            {
                return tmp;
            }

            return tmp;
        }

        /// <summary>
        /// 确认清洗信息，并入库（业务逻辑层）
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <param name="totalTime">总清洗时间</param>
        /// <param name="checkTime">检查时间</param>
        /// <param name="checkNo">检查员工编号</param>
        /// <param name="checkRealName">检查员工姓名</param>
        /// <returns>
        /// 确认结果
        /// </returns>
        public bool SureCleanRecord(string endoscopeSN, string totalTime, string checkTime, string checkNo = "", string checkRealName = "")
        {
            if (string.IsNullOrEmpty(endoscopeSN) || string.IsNullOrEmpty(totalTime) || string.IsNullOrEmpty(checkTime))
            {
                return false;
            }

            bool reuslt = false;

            try
            {
                reuslt = dataObject.RecordTraceData(Global.EndoscopeRecord, endoscopeSN, checkTime);
            }
            catch (Exception)
            {
                return false;
            }

            // 写入终表，写入是否成功
            if (reuslt)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否存在某一数据
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <param name="table">表名</param>
        /// <param name="col">列名</param>
        /// <returns>
        /// 返回查找结果，true存在
        /// </returns>
        public bool CheckIsExit(string endoscopeSN, string table, string col)
        {
            if (string.IsNullOrEmpty(endoscopeSN) || string.IsNullOrEmpty(table) || string.IsNullOrEmpty(col))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.IsExitOneColumn(endoscopeSN, table, col);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 是否绑定内镜
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <returns>
        /// 是否绑定
        /// </returns>
        public bool IsBindingWasher(string endoscopeSN)
        {
            if (string.IsNullOrEmpty(endoscopeSN))
            {
                return false;
            }

            bool result = false;

            try
            {

                result = dataObject.IsBindingWaher(endoscopeSN);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 设置清洗是否合格
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <param name="qulified">合格</param>
        /// <returns>
        /// 返回值
        /// </returns>
        public bool SetQulified(string endoscopeSN, string qulified)
        {
            if (string.IsNullOrEmpty(endoscopeSN) || string.IsNullOrEmpty(qulified))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.SetOneTable(endoscopeSN, qulified, "qualified");
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 设置清洗机编号
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <param name="cleanNo">清洗编码</param>
        /// <returns>设置结果</returns>
        public bool SetAutoCleanNo(string endoscopeSn, string cleanNo)
        {
            if (string.IsNullOrEmpty(endoscopeSn) || string.IsNullOrEmpty(cleanNo))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.SetOneTable(endoscopeSn, cleanNo, "autoCleanNo");
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="userInfo">用户对象</param>
        /// <returns>
        /// 修改结果
        /// </returns>
        public bool ModifyUser(string tableName, UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(tableName) || userInfo == null)
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.ModifyUser(tableName, userInfo);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 修改内镜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="endoscopeInfo">内镜对象</param>
        /// <returns>
        /// 修改结果
        /// </returns>
        public bool ModifyEndoscope(string tableName, EndoscopeInfo endoscopeInfo)
        {
            if (string.IsNullOrEmpty(tableName) || endoscopeInfo == null)
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.ModifyEndoscope(tableName, endoscopeInfo);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 删除内镜
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>
        /// 删除结果
        /// </returns>
        public bool DeleteEndoscope(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.DeleteEndoscope(condition);
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>
        /// 删除结果
        /// </returns>
        public bool DeleteUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.DeleteUser(condition);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// 加载用户信息
        /// </summary>
        /// <returns>用户列表</returns>
        public List<UserInfo> LoadUsers()
        {
            return dataObject.LoadUsers();
        }

        /// <summary>
        /// 加载内镜
        /// </summary>
        /// <returns>内镜列表</returns>
        public List<EndoscopeInfo> LoadEndoscopes()
        {
            return dataObject.LoadEndoscopes();
        }

        /// <summary>
        /// 查询追溯
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public string SearchResult(string sql)
        {
            return dataObject.SearchResult(sql);
        }

        /// <summary>
        /// 一次清洗
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>
        /// 一次清洗结果集合
        /// </returns>
        public string SearchFirstWash(string begin, string end)
        {
            return dataObject.SearchFirstWash(begin, end);
        }

        /// <summary>
        /// 二次清洗
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>
        /// 清洗结果集合
        /// </returns>
        public string SearchSecondWash(string begin, string end)
        {
            return dataObject.SearchSecondWash(begin, end);
        }

        /// <summary>
        /// 获取内镜条件查询信息
        /// </summary>
        /// <param name="condition">条件查询</param>
        /// <param name="value">条件值</param>
        /// <returns>
        /// 查询结果集合
        /// </returns>
        public string GetSearchResultByName(string condition, string value)
        {
            if (string.IsNullOrEmpty(condition) || string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string result = string.Empty;

            try
            {
                result = dataObject.GetSearchResultByName(condition, value);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return result;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pass">The pass.</param>
        /// <returns></returns>
        public string LoginUser(string username, string pass)
        {
            return dataObject.LoginUser(username, pass);
        }

        /// <summary>
        /// Loads the patients.
        /// </summary>
        /// <returns></returns>
        public List<Patient> LoadPatients()
        {
            return dataObject.LoadPatients();
        }

        /// <summary>
        /// Registers the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public bool RegisterPatient(Patient patient)
        {
            return dataObject.RegisterPatient(patient);
        }

        /// <summary>
        /// Deletes the patient.
        /// </summary>
        /// <param name="sn">The sn.</param>
        /// <returns></returns>
        public bool DeletePatient(string sn)
        {
            return dataObject.DeletePatients(sn);
        }

        /// <summary>
        /// Updates the trac data.
        /// </summary>
        /// <param name="wareNo">The ware no.</param>
        /// <param name="doctorName">Name of the doctor.</param>
        /// <param name="patientNO">The patient no.</param>
        /// <param name="nurseNO">The nurse no.</param>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <returns></returns>
        public bool UpdateTracData(string endoscop, string wareNo, string doctorName, string patientNO, string patientName, string nurseNO, string patientId)
        {
            if (string.IsNullOrEmpty(endoscop) || string.IsNullOrEmpty(patientId))
            {
                return false;
            }

            bool result = false;

            try
            {
                result = dataObject.UpdateTracData(endoscop, wareNo, doctorName, patientNO, patientName, nurseNO, patientId);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// Loads the endoscope names.
        /// </summary>
        /// <returns></returns>
        public List<string> LoadEndoscopeNames()
        {
            return dataObject.LoadEndsocopeNames();
        }

        /// <summary>
        /// Loads the ware infos.
        /// </summary>
        /// <returns></returns>
        public List<string> LoadWareInfos()
        {
            return dataObject.LoadWareinfos();
        }

        /// <summary>
        /// Loads the name of the user.
        /// </summary>
        /// <param name="userRole">The user role.</param>
        /// <returns></returns>
        public List<string> LoadUserName(UserRole userRole)
        {
            return dataObject.LoadUserInfos(userRole);
        }

        /// <summary>
        /// Endoscopes the is quality.
        /// </summary>
        /// <param name="en">The length.</param>
        /// <returns>合格true；不合格false</returns>
        public bool EndoscopeIsQuality(string en)
        {
            return dataObject.EndoscopeIsQuality(en);
        }
        #endregion
    }
}
