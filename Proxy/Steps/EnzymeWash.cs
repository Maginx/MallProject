using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ProxyClient.Controls;

namespace ProxyClient.Steps
{
    /// <summary>
    /// 酶洗
    /// </summary>
    internal sealed class EnzymeWash : Wash
    {
        /// <summary>
        /// The temporary date time
        /// </summary>
        private DateTime? tempDateTime = null;

        #region 公有方法
        /// <summary>
        /// 数据处理程序
        /// </summary>
        /// <param name="data">数据值</param>
        public override void DatagramProcess(MonitorData data)
        {
            base.DatagramProcess(data);
            FormHelper.voiceFlag = true;
            if (string.Equals(this.WorkState, "FF"))
            {
                // 验证身份
                this.SIMIdentify(this.Sim);
            }

            if (string.IsNullOrEmpty(this.EndoscopeSN) || string.IsNullOrEmpty(this.EndoscopeSIM))
            {
                // 发送错误信息
                FormHelper.VoiceSpeech("请打内镜卡");
                this.ShowCleanStep("酶洗已打内镜卡", Color.Empty);

                //if (string.IsNullOrEmpty(this.EndoscopeSIM))
                //{
                //    this.tempDateTime = DateTime.Now;
                //}

                // 没有打过卡，则返回
                return;
            }

            // 开始酶洗
            if (string.Equals(this.WorkState, "01"))
            {
                if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.EnzymeWashBeginStep, DateTime.Now, this.EndoscopeSN))
                {
                    var tmp = DateTime.Now.AddSeconds(Convert.ToDouble(GSetting.StepTime["EnzymeWash"]));
                    GSetting.EndoscStates[this.EndoscopeSN].EzymeWashTime = Convert.ToDouble(GSetting.StepTime["EnzymeWash"]);
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.EnzymeWashEndStep, tmp, this.EndoscopeSN);
                    FormHelper.VoiceSpeech(this.EndoscopeSN + "酶洗开始");
                    this.ShowCleanStep("酶洗开始", Color.Empty);
                    GSetting.EndoscStates[this.EndoscopeSN].EzymeWashBegin = DateTime.Now;
                }
                else
                {
                    FormHelper.VoiceSpeech("请先打员工卡");
                    this.PrintTempRecordMsg("酶洗记录错误，请先打员工卡", Color.Red);
                }
            }

            // 酶洗结束进入清洗
            if (string.Equals(this.WorkState, "02"))
            {
                // 如果忘记开始清洗则自动填充
                if (this.tempDateTime != null)
                {
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.EnzymeWashBeginStep, this.tempDateTime.Value, this.EndoscopeSN);
                    GSetting.EndoscStates[this.EndoscopeSN].EzymeWashBegin = this.tempDateTime;
                }

                if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.EnzymeWashEndStep, DateTime.Now, this.EndoscopeSN))
                {
                    GSetting.EndoscStates[this.EndoscopeSN].EzymeWashEnd = DateTime.Now;
                    GSetting.EndoscStates[this.EndoscopeSN].EzymeWashTime = (GSetting.EndoscStates[this.EndoscopeSN].EzymeWashEnd.Value - GSetting.EndoscStates[this.EndoscopeSN].EzymeWashBegin.Value).TotalSeconds;

                    FormHelper.VoiceSpeech(this.EndoscopeSN + "酶洗结束，请进入清洗");
                    this.ShowCleanStep("酶洗结束，请进入清洗", Color.Empty);
                }
                else
                {
                    this.ShowCleanStep("酶洗记录错误，酶洗数据未更新", Color.Red);
                }

                this.CleanAllStr();
                this.tempDateTime = null;
            }
        }

        /// <summary>
        /// 验证SIM卡号
        /// </summary>
        /// <param name="sim">sim卡号</param>
        public override void SIMIdentify(string sim)
        {
            // 获取打卡信息sResult[0]=endoscope或者user
            var sresult = GSetting.DataServ.GetUserEndoscopeMsgBySIM(sim);

            if (!sresult.IsValid())
            {
                return;
            }

            var result = sresult.FirstOrDefault();

            if (string.IsNullOrEmpty(result))
            {
                return;
            }

            if (string.Equals(result, "user"))
            {
                FormHelper.VoiceSpeech("请打内镜卡");
            }
            else if (string.Equals(result, "endoscope"))
            {
                this.EndoscopeSN = sresult[1];
                this.EndoscopeSIM = sim;
                FormHelper.VoiceSpeech(this.EndoscopeSN + "酶洗打卡完成");
                this.ShowCleanStep("酶洗打卡完成", Color.Empty);
                this.PrintTempRecordMsg(this.EndoscopeSN + "酶洗打卡完成", Color.Empty);
            }
            else
            {
                this.EndoscopeSIM = string.Empty;
                this.EndoscopeSN = string.Empty;
                FormHelper.VoiceSpeech("无效数据卡号");
                this.ShowStatusTips("无效数据卡号：" + sim, Color.Red);
                //this.SendSocketMsg("02");
            }
        }
        #endregion
    }
}
