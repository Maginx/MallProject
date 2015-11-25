using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 正在清洗内镜状态
    /// </summary>
    sealed partial class EndoscopeCleanState : KryptonForm
    {
        #region 构造方法
        /// <summary>
        /// Initializes a new instance of the <see cref="EndoscopeCleanState"/> class.
        /// </summary>
        public EndoscopeCleanState()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 初始化事件
        /// </summary>
        public void InitialEndoscopeCLeanState()
        {
            this.menue.ItemClicked += menue_ItemClicked;
            GSetting.ShowStateEvnet += GlobalSetting_ShowUserControlEvnet;
            GSetting.AddListEvent += GlobalSetting_AddEndoscopeToQueueEvent;
            GSetting.RemoveListEvent += GlobalSetting_RemoveWaitQueueEvent;
            GSetting.CleanPanelEvent += GlobalSetting_RemoveCleanedEndoscopeEvent;
        }

        /// <summary>
        /// 删除待确认内镜数据
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        public void GlobalSetting_RemoveWaitQueueEvent(string endoscopeSN)
        {
            Action action = delegate()
            {
                foreach (KryptonListItem item in sureList.Items)
                {
                    if (item.ShortText.Trim().Contains(endoscopeSN))
                    {
                        this.sureList.Items.Remove(item);
                        break;
                    }
                }
            };

            //MessageBox.Show(endoscopeSN + "正在移除ing");
            // 移除队列
            this.sureList.BeginInvoke(action);
            //this.sureList.Invoke(action);
        }

        /// <summary>
        /// Handles the ItemClicked event of the menue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void menue_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sureList.SelectedItems.Count > 0)
            {
                ToolStripItem item = e.ClickedItem;
                this.SureClean(item.Name, ((KryptonListItem)sureList.SelectedItems[0]).Tag.ToString());
            }
        }

        /// <summary>
        /// Sures the clean.
        /// </summary>
        /// <param name="e">The decimal.</param>
        /// <param name="endoscopeSN">The endoscope sn.</param>
        public void SureClean(string e, string endoscopeSN)
        {
            if (string.IsNullOrEmpty(e))
            {
                return;
            }

            // 右键选项
            if (string.Equals(e, "endoscopeInfoMsg", StringComparison.OrdinalIgnoreCase))
            {
                // 显示内镜清洗详情
                MainForm.ShowEndoscopeInfoMsg(sureList.SelectedItems[0].ToString());
            }
            else
            {
                // 确认内镜信息
                CleanTempMsg proxy = GSetting.GetFormInstance<CleanTempMsg>();
                proxy.GetEndoscopeAndSureCleanMsg(endoscopeSN);
            }

        }

        #region 删除清洗数据
        /// <summary>
        /// Globals the setting_ remove cleaned endoscope event.
        /// </summary>
        /// <param name="state">The state.</param>
        private void GlobalSetting_RemoveCleanedEndoscopeEvent(EndoscopeState state)
        {
            Action action = delegate()
            {

                this.flowLayoutUserControl.Controls.Remove(state);
                string temp = "内镜";

                foreach (EndoscopeState es in flowLayoutUserControl.Controls)
                {
                    temp += es.txtEndoscopeID.Text + ",";
                }

                if (temp.Trim(',').Length > 2)
                {
                    this.labTips.Text = temp.Trim(',') + "正在清洗";
                }
                else
                {
                    this.labTips.Text = "欢迎使用迈尔内镜清洗工作站";
                }
            };

            this.Invoke(action);
        }
        #endregion

        #region 添加待确认信息
        /// <summary>
        /// Globals the setting_ add endoscope automatic queue event.
        /// </summary>
        /// <param name="endoscope">The endoscope.</param>
        private void GlobalSetting_AddEndoscopeToQueueEvent(EndoscopeState endoscope)
        {
            Action action = delegate()
            {
                var ListItem = new KryptonListItem();
                ListItem.Tag = endoscope.txtEndoscopeID.Text;
                ListItem.Image = ProxyClient.Properties.Resources.ok;
                ListItem.ShortText = "内镜" + endoscope.txtEndoscopeID.Text + "等待确认";
                ListItem.LongText = "清洗员编号：" + endoscope.txtWasherSN.Text;

                if (!string.IsNullOrEmpty(endoscope.EndoscopeCleanStandarData))
                {
                    ListItem.LongText += " 清洗不合格信息：[" + endoscope.EndoscopeCleanStandarData + "]";
                    ListItem.Image = ProxyClient.Properties.Resources.no;
                }

                this.sureList.Items.Add(ListItem);
            };

            // 添加队列
            this.sureList.Invoke(action);
        }


        #endregion

        #region 显示内镜清洗状态到面板上
        /// <summary>
        /// Globals the setting_ show user control evnet.
        /// </summary>
        /// <param name="endopscopeSN">The endopscope sn.</param>
        private void GlobalSetting_ShowUserControlEvnet(string endopscopeSN)
        {
            Action action = delegate()
            {
                flowLayoutUserControl.Controls.Add(GSetting.EndoscStates[endopscopeSN]);
                string temp = "内镜";

                foreach (EndoscopeState es in flowLayoutUserControl.Controls)
                {
                    temp += es.txtEndoscopeID.Text + ",";
                }

                if (temp.Trim(',').Length > 2)
                {
                    this.labTips.Text = temp.Trim(',') + "正在清洗";
                }
                else
                {
                    this.labTips.Text = "欢迎使用迈尔内镜清洗工作站";
                }

                this.tipsPanel.Text = temp.Trim(',') + "正在清洗";
            };
            this.Invoke(action);
        }
        #endregion
    }
}
