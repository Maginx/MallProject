using System.ComponentModel;
using ProxyClient.Controls;
using ClassLibrary;
using System;

namespace ProxyClient
{
    /// <summary>
    /// verify data class
    /// </summary>
    internal class MonitorData : INotifyPropertyChanged
    {
        /// <summary>
        /// The serial data
        /// </summary>
        private string serialData;

        /// <summary>
        /// The accept data
        /// </summary>
        private string acceptData;

        #region 构造方法
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorData"/> class.
        /// </summary>
        public MonitorData()
        {
            this.acceptData = "0";
            this.PortNO = string.Empty;
            this.WorkState = string.Empty;
            this.SIM = string.Empty;
        }
        #endregion

        #region event process
        /// <summary>
        /// property changed event object
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [serial data prorperty changed].
        /// </summary>
        public event PropertyChangedEventHandler SerialDataProrpertyChanged;
        #endregion

        #region 公有对象
        /// <summary>
        /// accept-data property
        /// </summary>
        public string AccepData
        {
            get
            {
                return this.acceptData;
            }

            set
            {
                this.acceptData = value;

                // filter socket message and verify message 
                if (this.VerifySocketData(value))
                {
                    this.PortNO = this.acceptData.Substring(4, 2);
                    this.WorkState = this.acceptData.Substring(6, 2);
                    this.SIM = this.acceptData.Substring(12, 10);
                }

                this.NotifyPropertyChanged("AccepData");
            }
        }

        /// <summary>
        /// port number
        /// </summary>
        public string PortNO { get; set; }

        /// <summary>
        /// work state 
        /// </summary>
        public string WorkState { get; set; }

        /// <summary>
        /// endoscope sim number
        /// </summary>
        public string SIM { get; set; }
        #endregion

        #region Notify Property
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Notifies the serial property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifySerialPropertyChanged(string propertyName)
        {
            if (this.SerialDataProrpertyChanged != null)
            {
                this.SerialDataProrpertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        /// valid Socket accept-data
        /// </summary>
        /// <param name="data">data content</param>
        /// <returns>
        /// true:valid；false：invalid
        /// </returns>
        private bool VerifySocketData(string data)
        {
            bool isVerify = true;

            // can bus ensure 
            //if (!data.StartsWith("EEEE") && !data.EndsWith("EDED") && data.Length == 8)
            if (data.Length == 10)
            {
                GSetting.InitialMdata();
                string card = data;
                card = GSetting.DataServ.GetUserEndoscopeMsgBySIM(card)[1];
                // ensure encoscope message
                var proxy = GSetting.GetFormInstance<CleanTempMsg>();
                proxy.GetEndoscopeAndSureCleanMsg(card);
                return false;
            }

            if (data.Length < 32)
            {
                isVerify = false;
            }

            // verify data head
            if (!data.StartsWith("EEEE"))
            {
                GSetting.GetFormInstance<CleanTempMsg>().ProxyGroupPanel.ValuesSecondary.Description = "Data header is wrong";
                isVerify = false;
            }

            // verify data end
            if (!data.EndsWith("EDED"))
            {
                GSetting.GetFormInstance<CleanTempMsg>().ProxyGroupPanel.ValuesSecondary.Description = "Data end is wrong";
                isVerify = false;
            }

            return isVerify;
        }
    }
}
