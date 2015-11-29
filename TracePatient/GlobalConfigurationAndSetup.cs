using ComponentFactory.Krypton.Toolkit;
using System;
using System.Windows.Forms;

namespace TracePatient
{
    sealed class GlobalConfigurationAndSetup
    {
        /// <summary>
        /// TracePatient form instance reference
        /// </summary>
        public static KryptonListBox recordListControl = null;

        /// <summary>
        /// Log instance
        /// </summary>
        public static Logging TraceLogger = null;

        /// <summary>
        /// Database operation instance
        /// </summary>
        public static DatabaseBusiness DatabaseHandler = null;

        /// <summary>
        /// Load initialization configure files
        /// 1. initialize Logger instance;
        /// 2. read *.ini file as instance;
        /// </summary>
        /// <param name="path">configure file path.</param>
        public static IniInformation LoadIniFileAndStaticInstance(string path = null)
        {
            if (path == null)
            {
                path = Application.StartupPath + @"\config.ini";
            }

            // initialize logger instance
            TraceLogger = new Logging();
            TraceLogger.InitializeConfiguration();


            // get *.ini file as a instance.
            var iniFile = GetIniInstance(path);

            // inistialize database handler instance.
            DatabaseHandler = new DatabaseBusiness(iniFile);

            return iniFile;
        }

        /// <summary>
        /// Reconfig *.ini file, if existed one inifile instance, can pass it as parameter to reuse
        /// </summary>
        /// <param name="key">section key</param>
        /// <param name="value">section value</param>
        /// <param name="iniInfo">iniInfo instance</param>
        /// <param name="section">section </param>
        /// <param name="path">*.ini file path</param>
        /// <returns>inifile</returns>
        public static void ReconfigIniFile(IniInformation iniInfo, string section = "config", string path = null)
        {
            if (path == null)
            {
                path = Application.StartupPath + @"\config.ini";
            }

            if (iniInfo == null)
            {
                throw new ArgumentNullException("iniInfo instance is null");
            }

            iniInfo.ReconfigureIniFile();
        }

        /// <summary>
        /// Get ini instnce ,if already existed one instance, can reread ini file into this instance
        /// </summary>
        /// <param name="path">*.ini file path</param>
        /// <param name="iniInfo">already exist instance</param>
        /// <returns>inifile</returns>
        public static IniInformation GetIniInstance(string path, IniInformation iniInfo = null)
        {
            if (iniInfo == null)
            {
                iniInfo = new IniInformation(path);
            }
            iniInfo.ReadIniFile();
            return iniInfo;
        }
    }
}

