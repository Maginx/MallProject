using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProxyClient.Controls;

namespace ProxyClient.Steps
{
    /// <summary>
    /// 浸泡消毒
    /// </summary>
    internal sealed class DipIn : Wash
    {
        /// <summary>
        /// 一次清洗还是二次清洗标志
        /// </summary>
        private string flagCleanType = "1";

        /// <summary>
        /// The temporary date time
        /// </summary>
        private DateTime? tempDateTime = null;
        #region 公有方法
        /// <summary>
        /// 数据处理方法
        /// </summary>
        /// <param name="data">The data.</param>
        public override void DatagramProcess(MonitorData data)
        {
            base.DatagramProcess(data);
            if (string.Equals(this.WorkState, "FF"))
            {
                this.SIMIdentify(this.Sim);
            }

            if (string.IsNullOrEmpty(this.EndoscopeSN) || string.IsNullOrEmpty(this.EndoscopeSIM))
            {
                if (!string.IsNullOrEmpty(this.WasherNo))
                {
                    FormHelper.VoiceSpeech("请打内镜卡");
                    //return;
                }


                // 没有打过卡，则返回
                return;
            }

            // 消毒开始
            if (string.Equals(this.WorkState, "01"))
            {
                // 一次清洗
                if (this.flagCleanType == "1")
                {
                    if (string.IsNullOrEmpty(this.EndoscopeSN))
                    {
                        FormHelper.VoiceSpeech("请打内镜卡");
                    }

                    if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashBeginStep, DateTime.Now, this.EndoscopeSN))
                    {
                        var tmp = DateTime.Now.AddSeconds(Convert.ToDouble(GSetting.StepTime["DipIn"]));
                        GSetting.EndoscStates[this.EndoscopeSN].DipInTime = Convert.ToDouble(GSetting.StepTime["DipIn"]);
                        GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashEndStep, tmp, this.EndoscopeSN);
                        FormHelper.VoiceSpeech(this.EndoscopeSN + "消毒已开始");
                        GSetting.EndoscStates[this.EndoscopeSN].DipInBegin = DateTime.Now;
                        this.ShowCleanStep("消毒已开始", Color.Empty);
                    }
                    else
                    {
                        FormHelper.VoiceSpeech("请先打员工卡");
                        this.PrintTempRecordMsg("消毒记录错误，请先打员工卡", Color.Empty);
                    }
                }
                else
                {
                    if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashSecBeginStep, DateTime.Now, this.EndoscopeSN))
                    {
                        var tmp = DateTime.Now.AddSeconds(Convert.ToDouble(GSetting.StepTime["DipInSec"]));
                        GSetting.EndoscStates[this.EndoscopeSN].DipInSecTime = Convert.ToDouble(GSetting.StepTime["DipInSec"]);
                        GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashSecEndStep, tmp, this.EndoscopeSN);
                        GSetting.DataServ.RecordEveryCleanStep(DataService.Step.RecordTime, DateTime.Now, this.EndoscopeSN);
                        FormHelper.VoiceSpeech(this.EndoscopeSN + "（二次）消毒已开始");
                        GSetting.EndoscStates[this.EndoscopeSN].DipInSecBegin = DateTime.Now;
                        this.ShowCleanStep("（二次）消毒已开始", Color.Empty);
                    }
                    else
                    {
                        FormHelper.VoiceSpeech("请先打员工卡");
                        this.PrintTempRecordMsg("消毒记录错误，请先打员工卡", Color.Empty);
                    }
                }

            }

            // 消毒结束
            if (string.Equals(this.WorkState, "02"))
            {
                // 一次
                if (this.flagCleanType == "1")
                {

                    if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashEndStep, DateTime.Now, this.EndoscopeSN))
                    {
                        FormHelper.VoiceSpeech(this.EndoscopeSN + "消毒已结束，进入末洗");
                        GSetting.EndoscStates[this.EndoscopeSN].DipInEnd = DateTime.Now;
                        GSetting.EndoscStates[this.EndoscopeSN].DipInTime = (GSetting.EndoscStates[this.EndoscopeSN].DipInEnd.Value - GSetting.EndoscStates[this.EndoscopeSN].DipInBegin.Value).TotalSeconds;
                        this.ShowCleanStep("消毒已结束，进入末洗", Color.Empty);
                    }
                }
                else
                {
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.RecordTime, DateTime.Now, this.EndoscopeSN);


                    if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.DipInWashSecEndStep, DateTime.Now, this.EndoscopeSN))
                    {
                        FormHelper.VoiceSpeech(this.EndoscopeSN + "（二次）消毒已结束，进入末洗");
                        GSetting.EndoscStates[this.EndoscopeSN].DipInSecEnd = DateTime.Now;
                        GSetting.EndoscStates[this.EndoscopeSN].DipInSecTime = (GSetting.EndoscStates[this.EndoscopeSN].DipInSecEnd.Value - GSetting.EndoscStates[this.EndoscopeSN].DipInSecBegin.Value).TotalSeconds;
                        this.ShowCleanStep("（二次）消毒已结束，进入末洗", Color.Empty);
                    }

                    this.flagCleanType = "1";
                }

                this.CleanAllStr();
                this.tempDateTime = null;
            }
        }

        /// <summary>
        /// 验证SIM卡号
        /// </summary>
        /// <param name="sim">SIM</param>
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
                this.WasherNo = sresult[1];
                this.WasherName = sresult[2];
                this.flagCleanType = "2";
                FormHelper.VoiceSpeech("（二次）消毒已打卡，请打内镜卡");
                this.PrintTempRecordMsg(this.EndoscopeSN + "（二次）消毒已打卡，请打内镜卡", Color.Empty);
                this.ShowStatusTips("（二次清洗）员工编号：" + this.WasherNo + "； 员工名称：" + this.WasherName, Color.Green);
            }
            else if (string.Equals(result, "endoscope"))
            {
                this.EndoscopeSN = sresult[1];
                this.EndoscopeSIM = sim;

                if (this.flagCleanType == "1")
                {
                    FormHelper.VoiceSpeech(this.EndoscopeSN + "消毒打卡完成");
                    this.ShowCleanStep("消毒打卡完成", Color.Empty);
                    this.PrintTempRecordMsg(this.EndoscopeSN + "消毒打卡完成", Color.Empty);
                }
                else
                {
                    // 重置内镜信息,并且开始
                    bool isRest = GSetting.DataServ.ResetEndoscopeMsg(this.EndoscopeSN, this.EndoscopeSIM, WasherNo, WasherName, "2", "1", PortNo);

                    if (isRest)
                    {
                        var prepatient = GSetting.DataServ.GetPrePatient(this.EndoscopeSN);
                        if (!GSetting.EndoscStates.Keys.Contains(this.EndoscopeSN))
                        {
                            GSetting.EndoscStates.Add(this.EndoscopeSN, new EndoscopeState(this.EndoscopeSN));
                            GSetting.EndoscStates[this.EndoscopeSN].cleanType = 2;
                            GSetting.EndoscStates[this.EndoscopeSN].txtWasherSN.Text = this.WasherNo;
                            GSetting.EndoscStates[this.EndoscopeSN].txtWasherSIM.Text = this.WasherName;
                            GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeID.Text = this.EndoscopeSN;
                            GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeSIM.Text = this.EndoscopeSIM;
                            GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeState.Text = "内镜清洗正常";
                            GSetting.EndoscStates[this.EndoscopeSN].txtCleanStep.Text = "（二次）消毒打卡完成";
                            GSetting.EndoscStates[this.EndoscopeSN].txtPrepatient.Text = prepatient;
                            GSetting.ShowState(this.EndoscopeSN);
                        }

                        FormHelper.VoiceSpeech(this.EndoscopeSN + "（二次）消毒打卡完成");
                        this.ShowCleanStep("（二次）消毒打卡完成", Color.Empty);
                        this.PrintTempRecordMsg(this.EndoscopeSN + "（二次）消毒打卡完成", Color.Empty);
                    }
                }
            }
            else
            {
                this.EndoscopeSIM = string.Empty;
                this.EndoscopeSN = string.Empty;
                FormHelper.VoiceSpeech("无效数据卡号");
                this.ShowStatusTips("无效数据卡号：" + sim, Color.Red);
                this.SendSocketMsg("02");
            }
        }
        #endregion
    }
}
