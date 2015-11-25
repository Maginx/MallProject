using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using ProxyClient.Controls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Management;
using SpeechLib;
using ClassLibrary;
using System.Threading;

namespace ProxyClient
{
    /// <summary>
    /// Form class
    /// </summary>
    public static class FormClient
    {
        #region public object
        /// <summary>
        /// control namesapce
        /// </summary>
        public static string CleanTempMsg;

        /// <summary>
        /// control name
        /// </summary>
        public static string PortSet;

        /// <summary>
        /// control time
        /// </summary>
        public static string CleanTime;

        /// <summary>
        /// clean state object
        /// </summary>
        public static string EndoscopeCleanState;

        /// <summary>
        /// basic setting
        /// </summary>
        public static string BasicSetting;

        /// <summary>
        /// user manager 
        /// </summary>
        public static string UserManager;

        /// <summary>
        /// endpscope manager
        /// </summary>
        public static string EndoscopeManager;

        /// <summary>
        /// data trace
        /// </summary>
        public static string DataTrace;

        /// <summary>
        /// enter license
        /// </summary>
        public static string EnterLicense;

        /// <summary>
        /// main form
        /// </summary>
        public static string MainFormText { get; private set; }

        /// <summary>
        /// temp bashborad
        /// </summary>
        public static string TempRecord { get; private set; }
        #endregion

        #region constructor
        static FormClient()
        {
            CleanTempMsg = "CleanTempMsg";
            PortSet = "PortSet";
            CleanTime = "CleanTime";
            EndoscopeCleanState = "EndoscopeCleanState";
            BasicSetting = "BasicSetting";
            UserManager = "UserManager";
            EndoscopeManager = "EndoscopeManager";
            DataTrace = "DataTrace";
            EnterLicense = "EnterLicense";
            MainFormText = "MainForm";
            TempRecord = "TempRecord";
        }
        #endregion
    }

    /// <summary>
    /// Form Helper class
    /// </summary>
    public static class FormHelper
    {
        #region private objects
        /// <summary>
        /// SIM card number regex
        /// </summary>
        private static Regex simNo;

        /// <summary>
        /// normal no.
        /// </summary>
        private static Regex commonText;

        private static object lockObj = new object();
        /// <summary>
        /// voice flag
        /// </summary>
        private static SpeechVoiceSpeakFlags spFlags;

        /// <summary>
        /// voice object
        /// </summary>
        private static SpVoice voice;

        private static int number = -1;
        private static string temp = string.Empty;
        private static SpVoice sureVoice;
        /// <summary>
        /// main form
        /// </summary>
        private static MainForm mainForm;

        /// <summary>
        /// proxy form
        /// </summary>
        private static TempRecord tempRecord;

        /// <summary>
        /// font setting object
        /// </summary>
        private static Font font = new Font("宋体", 11, FontStyle.Bold);

        /// <summary>
        /// writter object
        /// </summary>
        private static StreamWriter writer = null;

        /// <summary>
        /// log path
        /// </summary>
        private static string SocketLogPath;
        #endregion

        #region  public objects
        /// <summary>
        /// voice flag
        /// </summary>
        public static bool voiceFlag = true;

        /// <summary>
        /// print rich text
        /// </summary>
        /// <param name="text">message</param>
        /// <param name="color">font color</param>
        public delegate void PrintRichText(string text, Color color);

        /// <summary>
        /// print doc
        /// </summary>
        /// <param name="text">message</param>
        /// <param name="colr">font color</param>
        public delegate void PrintText(string text, Color colr);

        /// <summary>
        /// the latest clean step
        /// </summary>
        /// <param name="state">TextBox object</param>
        /// <param name="str">message</param>
        public delegate void ShowCleanStepHandler(Label state, string str);

        /// <summary>
        /// rich text show
        /// </summary>
        public static PrintRichText RichTextBoxMethod = new PrintRichText(AppendRichText);

        /// <summary>
        /// test show
        /// </summary>
        public static PrintText TextBoxMethod = new PrintText(AppendText);

        /// <summary>
        /// show clean step event
        /// </summary>
        public static ShowCleanStepHandler ShowCleanStepEvent = new ShowCleanStepHandler(ShowCleanStep);

        #endregion

        #region constructor
        static FormHelper()
        {
            simNo = new Regex(@"\d{10,12}");
            commonText = new Regex(@"\w{0,100}");
            spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            voice = new SpVoice();

            sureVoice = new SpVoice();
            sureVoice.Priority = SpeechVoicePriority.SVPOver;

            mainForm = GSetting.GetFormInstance<MainForm>();
            tempRecord = GSetting.GetFormInstance<TempRecord>();
            SocketLogPath = Path.Combine(Application.StartupPath, "log.mp");
        }


        #endregion

        #region public function

        public static void VoiceSpeechSure(string text)
        {
            voiceFlag = false;
            sureVoice.Speak("", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            sureVoice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
            Global.Log(text);
        }

        static void sureVoice_EndStream(int StreamNumber, object StreamPosition)
        {
            voice.Speak("", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }
        /// <summary>
        /// voice function
        /// </summary>
        /// <param name="voiceText">message</param>
        public static void VoiceSpeech(string voiceText)
        {

            voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            if (voiceText == "清洗信息已记录" || voiceText == "清洗信息记录失败")
            {
                voice.Speak(voiceText, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                return;
            }

            //if (voiceFlag)
            {
                voice.Speak(voiceText, SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }



        /// <summary>
        /// show clean step
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="step">step</param>
        public static void ShowCleanStep(Label state, string step)
        {
            state.Text = step;
        }

        /// <summary>
        /// print tips message
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="color">The color.</param>
        public static void AppendText(string msg, Color color)
        {
            try
            {
                mainForm.labTips.Font = font;
                mainForm.labTips.ForeColor = color;
                mainForm.labTips.Text = msg;
            }
            catch (Exception ex)
            {
                FormHelper.RecordLogMsg(ex.Message);
            }
        }

        /// <summary>
        /// load rich text 
        /// </summary>
        /// <param name="text">cotent</param>
        /// <param name="color">color</param>
        public static void AppendRichText(string text, Color color)
        {
            RichTextBox box = tempRecord.richTxtTempRecord;
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionFont = font;
            box.SelectionColor = color;
            box.AppendText(DateTime.Now.ToString());
            box.AppendText("    [");
            box.AppendText(text);
            box.AppendText("]");
            box.AppendText("\r\n");
            box.SelectionColor = box.ForeColor;
            box.SelectionStart = box.TextLength;
            box.ScrollToCaret();
        }

        /// <summary>
        /// record log
        /// </summary>
        /// <param name="msg">message</param>
        public static void RecordLogMsg(string msg)
        {
            using (writer = new StreamWriter(GSetting.LogFilePath, true))
            {
                string tempM = DateTime.Now.ToString() + "  " + msg;
                writer.WriteLine(tempM);
            }
        }

        /// <summary>
        /// verify card no
        /// </summary>
        /// <param name="no">SIM卡号</param>
        /// <returns>验证结果</returns>
        public static bool VerifySimNo(string no)
        {
            bool result = false;
            if (simNo.IsMatch(no))
            {
                result = true;
            }
            return true;
        }

        /// <summary>
        /// verify normal text
        /// </summary>
        /// <param name="text">message</param>
        /// <returns>
        /// the result
        /// </returns>
        public static bool VerifyCommonText(string text)
        {
            bool result = false;
            if (commonText.IsMatch(text))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// encryption disk no
        /// </summary>
        public static string DiskNo()
        {
            string temp = Des.ConstKey;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                disk.Get();
                return Des.EncryptDES(Des.EncryptDES(disk.GetPropertyValue("VolumeSerialNumber").ToString(), temp), temp);
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// socket log
        /// </summary>
        /// <returns></returns>
        public static void SocketLog(string msg)
        {
            if (!File.Exists(SocketLogPath))
            {
                File.Create(SocketLogPath);
            }

            bool result = false;
            Monitor.TryEnter(lockObj, 1000, ref result);

            if (!result)
            {
                Monitor.Exit(lockObj);
                return;
            }

            try
            {
                lock (mainForm)
                {
                    using (FileStream stream = File.OpenWrite(SocketLogPath))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("[SocketError-" + DateTime.Now.ToString() + " : " + msg + "]");
                            writer.Flush();
                        }
                    }
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// Processes the SQL string.
        /// </summary>
        /// <param name="Str">The string.</param>
        /// <returns>verify</returns>
        public static bool ProcessSqlStr(string Str)
        {
            string SqlStr;
            SqlStr = "'|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare";
            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

        /// <summary>
        /// verify list
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">object</param>
        /// <returns>true,false</returns>
        public static bool IsValid<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified ts is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts">The ts.</param>
        /// <returns></returns>
        public static bool IsValid<T>(this T[] ts)
        {
            if (ts == null || ts.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Automatics the safe string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return obj.ToString().Trim();
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="t">The attribute.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">this object is null</exception>
        public static string GetAttributeName<T1>(this T1 t)
        {

            if (t == null)
            {
                throw new ArgumentNullException("this object is null");
            }

            Type type1 = typeof(T1);
            var memeber = type1.GetMember(t.ToSafeString()).FirstOrDefault();


            if (memeber.Name == t.ToSafeString())
            {
                EnumDescriptionAttribute attr = Attribute.GetCustomAttribute(memeber, typeof(EnumDescriptionAttribute)) as EnumDescriptionAttribute;
                if (attr == null)
                {
                    return string.Empty;
                }
                return attr.Description;
            }
            return string.Empty;
        }

        public static string GetValue(this XElement element)
        {
            if (element == null)
            {
                return string.Empty;
            }

            if (element.Value == null)
            {
                return string.Empty;
            }

            return element.Value;
        }
        #endregion
    }
}
