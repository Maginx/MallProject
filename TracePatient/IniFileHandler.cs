using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TracePatient
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    class IniFileHandler
    {
        private readonly string __path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        protected IniFileHandler(string INIPath)
        {
            if (!File.Exists(INIPath))
            {
                File.Create(INIPath);
            }
            __path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        protected void WriteIniValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.__path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        protected string ReadIniValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, String.Empty, temp, 255, this.__path);
            if (i == 0)
            {
                GlobalConfigurationAndSetup.TraceLogger.TraceConsole(string.Format("Read the ini file error, the key [{0}]", Key));
            }
            return temp.ToString();
        }
    }

}
