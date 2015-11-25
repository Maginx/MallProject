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
    /// 初洗
    /// </summary>
    internal sealed class FirstWash : Wash
    {
        #region 公有方法
        /// <summary>
        /// 数据处理程序
        /// </summary>
        /// <param name="data">数据值</param>
        public override void DatagramProcess(MonitorData data)
        {
            // 执行父类操作
            base.DatagramProcess(data);
            // 端口状态
            if (string.Equals(this.WorkState, "FF"))
            {
                this.SIMIdentify(this.Sim);
            }

            if (!this.NullOfStrs())
            {
                // 已经打过员工卡
                if (this.WasherChecked())
                {
                    FormHelper.VoiceSpeech("员工打卡完成，请打内镜卡");
                    this.PrintTempRecordMsg(this.WasherNo + "员工打卡完成", Color.Empty);
                }

                // 没有打过卡，直接返回
                return;
            }

            // 刷洗结束初洗开始 
            if (string.Equals(this.WorkState, "01"))
            {
                GSetting.EndoscStates[this.EndoscopeSN].BrushWashEnd = DateTime.Now;
                GSetting.EndoscStates[this.EndoscopeSN].FirstWashBegin = DateTime.Now;
                GSetting.EndoscStates[this.EndoscopeSN].BrushWashTime = (GSetting.EndoscStates[this.EndoscopeSN].BrushWashEnd.Value - GSetting.EndoscStates[this.EndoscopeSN].BrushWashBegin.Value).TotalSeconds;


                if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.FirstWashBeginStep, DateTime.Now, this.EndoscopeSN))
                {
                    var tmp = DateTime.Now.AddSeconds(Convert.ToDouble(GSetting.StepTime["FirstWash"]));
                    GSetting.EndoscStates[this.EndoscopeSN].FirstWashTime = Convert.ToDouble(GSetting.StepTime["FirstWash"]);
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.FirstWashEndStep, tmp, this.EndoscopeSN);
                    GSetting.EndoscStates[this.EndoscopeSN].FirstWashBegin = DateTime.Now;
                    FormHelper.VoiceSpeech(this.EndoscopeSN + "初洗开始");
                    this.ShowCleanStep("刷洗结束，初洗开始", Color.Empty);
                }
                else
                {
                    this.ShowCleanStep("内镜初洗开始失败", Color.Red);
                    GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeState.Text = "内镜初洗开始失败";
                }
            }

            // 初洗结束，进入酶洗
            if (string.Equals(this.WorkState, "02"))
            {
                if (GSetting.DataServ.RecordEveryCleanStep(DataService.Step.FirstWashEndStep, DateTime.Now, this.EndoscopeSN))
                {
                    GSetting.EndoscStates[this.EndoscopeSN].FirstWashEnd = DateTime.Now;
                    GSetting.EndoscStates[this.EndoscopeSN].FirstWashTime = (GSetting.EndoscStates[this.EndoscopeSN].FirstWashEnd.Value - GSetting.EndoscStates[this.EndoscopeSN].FirstWashBegin.Value).TotalSeconds;

                    FormHelper.VoiceSpeech(this.EndoscopeSN + "初洗结束，请进入酶洗");
                    this.ShowCleanStep("初洗结束，请进入酶洗", Color.Empty);
                }
                else
                {
                    this.ShowCleanStep("内镜初洗结束失败", Color.Red);
                    GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeState.Text = "内镜初洗结束失败";
                }

                this.CleanAllStr();
            }
        }

        /// <summary>
        /// 获取内镜绑定的相关信息
        /// </summary>
        /// <param name="sim">SIM卡号</param>
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
                // 清洗工编号
                this.WasherNo = sresult[1];

                // 清洗工姓名
                this.WasherName = sresult[2];
                FormHelper.VoiceSpeech("请打内镜卡");
                this.ShowCleanStep("初洗员工打卡完成", Color.Empty);
                this.ShowStatusTips("员工编号：" + this.WasherNo + "； 员工名称：" + this.WasherName, Color.Empty);
            }
            else if (string.Equals(result, "endoscope"))
            {
                if (string.IsNullOrEmpty(this.WasherNo))
                {
                    FormHelper.VoiceSpeech("请打员工卡");
                }

                // 内镜编号
                this.EndoscopeSN = sresult[1];

                // 内镜姓名
                this.EndoscopeSIM = sim;
                this.ShowCleanStep("初洗内镜打卡完成", Color.Empty);

                if (this.NullOfStrs())
                {
                    this.ShowStatusTips("内镜编号：" + this.EndoscopeSN + "；内镜卡号：" + this.EndoscopeSIM, Color.Empty);
                    this.ResetEndoscope();
                }
            }
            else
            {
                // 发送错误信息
                FormHelper.VoiceSpeech("无效数据卡号");
                this.ShowStatusTips("无效数据卡号：" + sim, Color.Red);
                this.CleanAllStr();
                this.SendSocketMsg("02");
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 重置内镜信息
        /// </summary>
        private void ResetEndoscope()
        {
            // 重置内镜信息,并且开始
            bool isrest = GSetting.DataServ.ResetEndoscopeMsg(this.EndoscopeSN, this.EndoscopeSIM, this.WasherNo, this.WasherName, "1", "1", this.PortNo);

            if (isrest)
            {
                FormHelper.VoiceSpeech("刷洗开始");

                try
                {
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.ManualWashBeginStep, DateTime.Now, this.EndoscopeSN);
                    GSetting.DataServ.RecordEveryCleanStep(DataService.Step.RecordTime, DateTime.Now, this.EndoscopeSN);
                    GSetting.EndoscStates[this.EndoscopeSN].BrushWashBegin = DateTime.Now;
                }
                catch (Exception)
                {
                }

                var prepatient = GSetting.DataServ.GetPrePatient(this.EndoscopeSN);
                if (!GSetting.EndoscStates.Keys.Contains(this.EndoscopeSN))
                {
                    GSetting.EndoscStates.Add(this.EndoscopeSN, new EndoscopeState(this.EndoscopeSN));
                    GSetting.EndoscStates[this.EndoscopeSN].txtWasherSN.Text = this.WasherNo;
                    GSetting.EndoscStates[this.EndoscopeSN].txtWasherSIM.Text = this.WasherName;
                    GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeID.Text = this.EndoscopeSN;
                    GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeSIM.Text = this.EndoscopeSIM;
                    GSetting.EndoscStates[this.EndoscopeSN].txtEndoscopeState.Text = "内镜清洗状态正常";
                    GSetting.EndoscStates[this.EndoscopeSN].txtCleanStep.Text = "正在刷洗";
                    GSetting.EndoscStates[this.EndoscopeSN].txtPrepatient.Text = prepatient;
                    GSetting.ShowState(this.EndoscopeSN);
                    GSetting.EndoscStates[this.EndoscopeSN].BrushWashBegin = DateTime.Now;
                }

                this.PrintTempRecordMsg(this.EndoscopeSN + "刷洗开始", Color.Empty);
            }
        }
        #endregion
    }
}
