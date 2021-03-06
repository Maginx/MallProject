﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using TracePatient.SQL;

namespace TracePatient
{
    class DatabaseBusiness
    {
        /// <summary>
        /// His database connection string
        /// </summary>
        private static string HisDbConnectionString = @"Data Source={0};Initial Catalog = {1};User ID = {2}; Password={3};";

        /// <summary>
        /// Mall endoscope data trace database connection string
        /// </summary>
        private static string MallDbConnectionString = @"Data Source={0};Initial Catalog = {1};User ID = {2}; Password={3};";

        private DbConnection _histConnection = null;
        private DbConnection _mallConnection = null;

        public DatabaseBusiness(IniInformation iniFile)
        {
            HisDbConnectionString = string.Format(HisDbConnectionString, iniFile.DbIp, iniFile.DbName, iniFile.UserName, iniFile.UserPassword);
            MallDbConnectionString = string.Format(MallDbConnectionString, iniFile.MallDbIp, iniFile.MallDbName, iniFile.MallDbUserName, iniFile.MallDbUserPassword);
        }

        /// <summary>
        /// Verify whether database can connect
        /// </summary>
        /// <returns></returns>
        protected bool VerifyDatabaseConnection(DbConnection connection, string dataBaseString)
        {
            if (connection == null)
            {
                connection = new SqlConnection(dataBaseString);
            }

            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }

            try
            {
                connection.Open();
                return true;
            }
            catch (InvalidOperationException invaidOperationException)
            {
                GlobalConfigurationAndSetup.TraceLogger.TraceConsole(invaidOperationException.Message);
                return false;
            }
            catch (SqlException sqlException)
            {
                GlobalConfigurationAndSetup.TraceLogger.TraceConsole(sqlException.Message);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// Verify the his databse connnection status
        /// </summary>
        /// <returns></returns>
        public bool VerifyHistDatabaseConnection()
        {
            return VerifyDatabaseConnection(_histConnection, HisDbConnectionString);
        }

        /// <summary>
        /// Verify the endoscope trace database connnection status
        /// </summary>
        /// <returns></returns>
        public bool VerifyMallDatabaseConnection()
        {
            return VerifyDatabaseConnection(_mallConnection, MallDbConnectionString);
        }

        /// <summary>
        /// Load doctors names
        /// </summary>
        /// <returns>unique doctors names</returns>
        public ISet<string> LoadDoctorsNames()
        {
            // user role type 4 : doctor
            string sqlText = "select * from userInfo where userRole='4' ";
            return ReadDataBySqlReader(sqlText, "userName");
        }

        /// <summary>
        /// Load nurse names
        /// </summary>
        /// <returns>unique nurses names</returns>
        public ISet<string> LoadNursesNames()
        {
            // user role type 2 : nurse
            string sqlText = "select * from userInfo where userRole='2'";
            return ReadDataBySqlReader(sqlText, "userName");
        }

        /// <summary>
        /// Load endoscope informations
        /// </summary>
        /// <returns>unique endoscope cutomized code. e.g:C01,W01</returns>
        public ISet<string> LoadEndoscopeCustomizedCode()
        {
            string sqlText = "select * from endoscopeInfo";
            return ReadDataBySqlReader(sqlText, "endoscopeSn");
        }

        /// <summary>
        /// Get patient details from his database
        /// </summary>
        /// <param name="iniInfo">iniIfo instance</param>
        /// <param name="patientCode">patient unique code</param>
        /// <returns>details</returns>
        public IDictionary<string, string> GetPatientDetailsFromHis(IniInformation iniInfo, string patientCode)
        {
            string sqlText = string.Format("select * from {0} where {1} = '{2}'", iniInfo.TableName, iniInfo.TableField.patientCode, patientCode);
            var sets = ReadDataBySqlReader(sqlText, iniInfo, patientCode);
            if (sets == null || sets.Count == 0)
            {
                throw new Exception("read the patient error");
            }

            return sets;
        }


        /// <summary>
        /// Get endoscope id by endoscope code(endoscopeSn) and clean time
        /// </summary>
        /// <returns>endsocope information instance</returns>
        public EndoscopeInformation GetAvailableEndoscopeDetails(string endoscopeCutomizedCode)
        {
            string sqlTextWithoutPatientInformation = string.Format("select top 1 id,washBeginTime,washEndTime,washDate from endoscopeRecord where endoscopeSn='{0}' and patientNo is null and doctorName is null order by id desc", endoscopeCutomizedCode);
            string sqlTextWithPatientInformation = string.Format("select top 1 id,washBeginTime,washEndTime,washDate from endoscopeRecord where endoscopeSn='{0}' and patientNo is not null and doctorName is not null order by id desc", endoscopeCutomizedCode);
            var endoscopeWithPatient = ReadDataBySqlReader(sqlTextWithPatientInformation);
            var endoscopeWithoutPatient = ReadDataBySqlReader(sqlTextWithoutPatientInformation);

            if (endoscopeWithPatient.Id > endoscopeWithoutPatient.Id)
            {
                throw new ArgumentNullException("doesn't get a available and cleaned endoscope");
            }

            return endoscopeWithoutPatient;
        }

        public bool CompletePatientDetials(string endoscopeId, string wareNo, string doctorName, string patientNo, string patientName, string patientResult, string sex, string time, string age, string nurseNo)
        {
            string sqlText = string.Format("update endoscopeRecord set wareNo='{0}',doctorName='{1}',patientNo='{2}',nurseNo='{3}' where id='{4}'", wareNo, doctorName, patientNo, nurseNo, endoscopeId);
            string addNewPatientSql = string.Format("insert into patientRecord (patientSn,patientName,patientResult,sex,time,age)valuse({0},{1},{2},{3},{5})", patientNo, patientName, patientResult, sex, time, age);
            SqlConnection connection = new SqlConnection(MallDbConnectionString);
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                var index = SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, sqlText);
                if (index <= 0)
                {
                    throw new ArgumentNullException("update patient details failed");
                }
                index = SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, addNewPatientSql);
                if (index <= 0)
                {
                    throw new ArgumentException("add new record into patient failed");
                }
                return true;
            }

        }

        /// <summary>
        /// Get endoscope details information 
        /// </summary>
        /// <param name="sqlCommand">sql commond</param>
        /// <returns>endoscope information instance</returns>
        private EndoscopeInformation ReadDataBySqlReader(string sqlCommand)
        {
            using (var dataSet = SqlHelper.ExecuteDataset(MallDbConnectionString, System.Data.CommandType.Text, sqlCommand))
            {
                var table = dataSet.Tables[dataSet.Tables.Count - 1];
                if (table.Rows.Count == 0)
                {
                    throw new ArgumentNullException("doesn't get a clean endoscope record");
                }

                var id = table.Rows[0]["id"];
                var beginTime = table.Rows[0]["washBeginTime"];
                var endTime = table.Rows[0]["washEndTime"];
                var date = table.Rows[0]["washDate"];

                if (id == null || beginTime == null || endTime == null || date == null)
                {
                    throw new ArgumentNullException("endsocope necessary message is empty.");
                }

                return new EndoscopeInformation(Int32.Parse(id.ToString()), beginTime.ToString(), endTime.ToString(), date.ToString());
            }
        }

        /// <summary>
        /// Read patient details from his database by patient unique code(maybe Outpatient No)
        /// </summary>
        /// <param name="sqlCommand">sql query text</param>
        /// <param name="iniInfo">IniInformation instance</param>
        /// <param name="patientCode">patient unique code</param>
        /// <returns>Dict for all of patient details include(name,age,sex)</returns>
        private IDictionary<string, string> ReadDataBySqlReader(string sqlCommand, IniInformation iniInfo, string patientCode)
        {
            IDictionary<string, string> patientDetails;
            string sqlText = string.Format("select * from {0} where {1} = '{2}'", iniInfo.TableName, iniInfo.TableField.patientCode, patientCode);
            using (var reader = SqlHelper.ExecuteReader(HisDbConnectionString, System.Data.CommandType.Text, sqlText))
            {
                patientDetails = new Dictionary<string, string>();
                patientDetails.Add(iniInfo.TableField.patientName, SafeReader(iniInfo.TableField.patientName));
                patientDetails.Add(iniInfo.TableField.patientAge, SafeReader(iniInfo.TableField.patientAge));
                patientDetails.Add(iniInfo.TableField.patientSex, SafeReader(iniInfo.TableField.patientSex));
            }

            return patientDetails;
        }

        /// <summary>
        /// Read database data from mall database by SqlDataReader instance.
        /// </summary>
        /// <param name="sqlCommond">sql text</param>
        /// <returns>list type values</returns>
        private ISet<string> ReadDataBySqlReader(string sqlCommond, string fieldName)
        {
            ISet<string> sets = null;
            using (var reader = SqlHelper.ExecuteReader(MallDbConnectionString, System.Data.CommandType.Text, sqlCommond))
            {
                try
                {
                    sets = SqlReaderList(reader, fieldName);
                }
                catch (ArgumentNullException e)
                {
                    GlobalConfigurationAndSetup.TraceLogger.TraceLog("reader data failed", this.GetType(), e);
                }

                return sets;
            }
        }

        /// <summary>
        /// SqlDataReader read all of table data by line and put the values into a list.
        /// </summary>
        /// <param name="reader">instance</param>
        /// <returns>values list</returns>
        private ISet<string> SqlReaderList(SqlDataReader reader, string fieldName)
        {
            ISet<string> list = null;
            if (reader == null)
            {
                throw new ArgumentNullException("SqlDataReader is null");
            }

            list = new HashSet<string>();
            while (reader.Read())
            {
                var obj = SafeReader(reader[fieldName]);
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// Reader database field data
        /// </summary>
        /// <param name="obj">field name</param>
        /// <returns>field corresponding value</returns>
        private string SafeReader(object obj)
        {
            string result = string.Empty;

            if (obj == null || Convert.IsDBNull(obj))
            {
                return result;
            }

            try
            {
                result = obj.ToString();
            }
            catch (System.IndexOutOfRangeException exception)
            {
                GlobalConfigurationAndSetup.TraceLogger.TraceLog("reader table field value error", this.GetType(), exception);
            }

            return result;
        }
    }
}
