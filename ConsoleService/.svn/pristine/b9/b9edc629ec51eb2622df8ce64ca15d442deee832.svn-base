using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ClassLibrary;
using MallHost;

namespace MallHost.Data
{
    /// <summary>
    /// 数据接口
    /// </summary>
    internal interface IDataBase
    {
        /// <summary>
        /// 更新endoscopeCleanTemp记录
        /// </summary>
        /// <param name="time">清洗时间（开始或结束时间）</param>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <param name="endoscopeSIM">内镜卡号</param>
        /// <param name="step">清洗工序</param>
        /// <param name="wahserNo">员工编号（默认为空）</param>
        /// <param name="washerName">员工名称（默认为空）</param>
        /// <returns>更新结果（true 表示更新成功，false表示更新失败）</returns>
        bool RecordCleanTime(string time, string endoscopeSN, string step);

        /// <summary>
        /// 根据SIM获取相关信息
        /// </summary>
        /// <param name="endoscope">内镜编号</param>
        /// <returns>内镜信息</returns>
        EndoscopeInfo GetEndoscopeMsgBySIM(string endoscope, string tablename);

        /// <summary>
        /// 根据SIM获取用户
        /// </summary>
        /// <param name="userSIM">用户卡号</param>
        /// <returns>用户信息</returns>
        UserInfo GetUserInfo(string userSIM, string tablename);

        /// <summary>
        /// 重置endoscope
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <returns>true 重 置成功 否则返还false</returns>
        bool ResetEndoscope(string endoscopeSN, string endoscopeSIM, string washerNo, string washerRealName, string cleanType, string washDate, string disinfection, string autoClean);

        /// <summary>
        /// 获得内镜信息
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        /// <returns>内镜信息</returns>
        EndoscopeTemp GetNewCleanMsg(string endoscope);

        /// <summary>
        /// 将临时记录写入终表
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="endocscopeSN">内镜编号</param>
        /// <param name="recordTime">The record time.</param>
        /// <returns>
        /// 写入是否成功
        /// </returns>
        bool RecordTraceData(string tableName, string endocscopeSN, string recordTime);

        /// <summary>
        /// 内镜是否绑定员工
        /// </summary>
        /// <param name="endoscopeSN">内镜名称</param>
        /// <returns>绑定结果</returns>
        bool IsBindingWaher(string endoscopeSN);

        /// <summary>
        /// 是否存在具体的一条数据
        /// </summary>
        /// <param name="endoscopeSN">内镜编码</param>
        /// <param name="table">表名</param>
        /// <param name="col">列名</param>
        /// <returns>查找结果</returns>
        bool IsExitOneColumn(string endoscopeSN, string table, string col);

        /// <summary>
        /// 设置清洗合格
        /// </summary>
        /// <param name="endoscopeSn">内镜编号</param>
        /// <param name="qulified">合格</param>
        /// <returns>设置结果</returns>
        bool SetOneTable(string endoscopeSn, string qulified, string col);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>删除结果</returns>
        bool DeleteUser(string condition);

        /// <summary>
        /// 删除内镜
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>删除结果</returns>
        bool DeleteEndoscope(string condition);

        /// <summary>
        /// 插入内镜信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="endoscopeInfo">内镜信息</param>
        /// <returns>是否成功</returns>
        bool ModifyEndoscope(string tableName, EndoscopeInfo endoscopeInfo);

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="user">用户</param>
        /// <returns>是否成功</returns>
        bool ModifyUser(string tableName, UserInfo user);

        /// <summary>
        /// 加载用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        List<UserInfo> LoadUsers();

        /// <summary>
        /// 加载内镜信息
        /// </summary>
        /// <returns><内镜信息/returns>
        List<EndoscopeInfo> LoadEndoscopes();

        /// <summary>
        /// 查询追溯结果
        /// </summary>
        /// <returns>XML结果</returns>
        string SearchResult(string sql);

        /// <summary>
        /// 初次清洗
        /// </summary>
        /// <returns>一次清洗结果</returns>
        string SearchFirstWash(string beginTime, string endTime);

        /// <summary>
        /// 二次清洗结果查询
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        string SearchSecondWash(string beginTime, string endTime);

        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>查询结果集合</returns>
        string GetSearchResultByName(string condition, string value);

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <returns>登录结果</returns>
        string LoginUser(string userName, string passWord);

        /// <summary>
        /// Registers the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        bool RegisterPatient(Patient patient);

        /// <summary>
        /// Loads the patients.
        /// </summary>
        /// <returns></returns>
        List<Patient> LoadPatients();

        /// <summary>
        /// Deletes the patients.
        /// </summary>
        /// <param name="sn">The sn.</param>
        /// <returns></returns>
        bool DeletePatients(string sn);

        /// <summary>
        /// Updates the trac data.
        /// </summary>
        /// <param name="wareNo">The ware no.</param>
        /// <param name="doctorName">Name of the doctor.</param>
        /// <param name="patientNO">The patient no.</param>
        /// <param name="nurseNO">The nurse no.</param>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <returns></returns>
        bool UpdateTracData(string endoscop, string wareNo, string doctorName, string patientNO, string patientName, string nurseNO, string patientId);

        /// <summary>
        /// Loads the user infos.
        /// </summary>
        /// <param name="userRole">The user role.</param>
        /// <returns></returns>
        List<string> LoadUserInfos(UserRole userRole);

        /// <summary>
        /// Loads the wareinfos.
        /// </summary>
        /// <returns></returns>
        List<string> LoadWareinfos();

        /// <summary>
        /// Loads the endsocope names.
        /// </summary>
        /// <returns></returns>
        List<string> LoadEndsocopeNames();

        /// <summary>
        /// Determines whether the specified length is quality.
        /// </summary>
        /// <param name="en">The length.</param>
        /// <returns></returns>
        bool EndoscopeIsQuality(string en);
    }
}
