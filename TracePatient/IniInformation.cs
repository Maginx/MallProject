namespace TracePatient
{
    sealed class IniInformation : IniFileHandler
    {
        private string mallDbIp;
        private string mallDbName;
        private string mallDbUserName;
        private string mallDbUserPassword;
        private string dbIp;
        private string dbName;
        private string tableName;
        private string userName;
        private string userPassword;
        private string wardName;
        private Field tableField;

        #region proeprties

        public string MallDbIp
        {
            get { return mallDbIp; }
        }

        public string MallDbName
        {
            get { return mallDbName; }
        }

        public string MallDbUserName
        {
            get { return mallDbUserName; }
        }

        public string MallDbUserPassword
        {
            get { return mallDbUserPassword; }
        }

        public string DbIp
        {
            get { return dbIp; }
        }

        public string DbName
        {
            get { return dbName; }
        }

        public string TableName
        {
            get { return tableName; }
        }

        public string UserName
        {
            get { return userName; }
        }

        public string UserPassword
        {
            get { return userPassword; }
        }

        public string WardName
        {
            get { return wardName; }
            set { wardName = value; }
        }

        public Field TableField
        {
            get { return tableField; }
        }

        #endregion

        public IniInformation(string path) : base(path) { }

        /// <summary>
        /// Read *.ini file as a db config
        /// </summary>
        public void ReadIniFile()
        {
            wardName = ReadIniValue("config", "ward_name");
            mallDbIp = ReadIniValue("config", "mall_db_ip");
            dbIp = ReadIniValue("config", "db_ip");
            dbName = ReadIniValue("config", "db_name");
            tableName = ReadIniValue("config", "table_name");
            userName = ReadIniValue("config", "db_user_name");
            userPassword = ReadIniValue("config", "db_user_password");
            mallDbName = ReadIniValue("config","mall_db_name");
            mallDbUserName = ReadIniValue("config","mall_db_user_name");
            mallDbUserPassword = ReadIniValue("config", "mall_db_user_password");

            tableField = new Field();
            tableField.patientCode = ReadIniValue("table", "patient_code");
            tableField.patientName = ReadIniValue("table", "patient_name");
            tableField.patientAge = ReadIniValue("table", "patient_age");
            tableField.patientSex = ReadIniValue("table", "patient_sex");
            tableField.doctorName = ReadIniValue("table", "doctor_name");
            tableField.nurseName = ReadIniValue("table", "nurse_name");
        }

        /// <summary>
        /// Reconfig ini file
        /// </summary>
        public void ReconfigureIniFile()
        {
            WriteIniValue("config", "ward_name", WardName);
            WriteIniValue("config", "mall_db_ip", mallDbIp);
            WriteIniValue("config", "db_ip", DbIp);
            WriteIniValue("config", "db_name", DbName);
            WriteIniValue("config", "table_name", TableName);
            WriteIniValue("config", "user_name", UserName);
            WriteIniValue("config", "user_password", UserPassword);
            ReadIniFile();
        }

        public override string ToString()
        {
            return string.Format("MALL IP:{0}; HIS IP:{1}, view table = {2}, view user = {3};", this.mallDbIp, this.dbIp, this.tableName, this.userName);
        }

        /// <summary>
        /// His table field details
        /// </summary>
        public struct Field
        {
            public string patientCode;
            public string patientName;
            public string patientAge;
            public string patientSex;
            public string doctorName;
            public string nurseName;
        }
    }
}
