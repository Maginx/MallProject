using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyClient.Steps;

namespace ProxyClient
{
    /// <summary>
    /// 端口映射对象
    /// </summary>
    internal sealed class PortMapping
    {
        #region 私有对象
        /// <summary>
        /// 初洗对象
        /// </summary>
        private FirstWash firstWash;

        /// <summary>
        /// 酶洗对象
        /// </summary>
        private EnzymeWash enzymeWash;

        /// <summary>
        /// 清洗对象
        /// </summary>
        private Cleanout cleanOut;

        /// <summary>
        /// 浸泡消毒对象
        /// </summary>
        private DipIn dipIn;

        /// <summary>
        /// 末洗对象
        /// </summary>
        private LastWash lastWash;

        /// <summary>
        /// 机洗
        /// </summary>
        private OtherAutoMachine otherAutoMachine;
        #endregion

        #region 构造方法
        /// <summary>
        /// Initializes a new instance of the <see cref="PortMapping"/> class.
        /// </summary>
        public PortMapping()
        {
            this.firstWash = new FirstWash();
            this.enzymeWash = new EnzymeWash();
            this.cleanOut = new Cleanout();
            this.dipIn = new DipIn();
            this.lastWash = new LastWash();
            this.otherAutoMachine = new OtherAutoMachine();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 端口号
        /// </summary>
        public string PortNo { get; set; }

        /// <summary>
        /// 清洗步骤
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 备注文本
        /// </summary>
        public string MarkText { get; set; }
        #endregion

        /// <summary>
        /// 选择清洗步骤
        /// </summary>
        /// <param name="data">socket数据</param>
        public void DatagramProcess(MonitorData data)
        {
            switch (this.StepName)
            {
                case "FirstWash":
                    this.firstWash.DatagramProcess(data);
                    break;
                case "EnzymeWash":
                    this.enzymeWash.DatagramProcess(data);
                    break;
                case "Cleanout":
                    this.cleanOut.DatagramProcess(data);
                    break;
                case "DipIn":
                    this.dipIn.DatagramProcess(data);
                    break;
                case "LastWash":
                    this.lastWash.DatagramProcess(data);
                    break;
                case "OtherAutoMachine":
                    this.otherAutoMachine.DatagramProcess(data);
                    break;
                default:
                    break;
            }
        }
    }
}
