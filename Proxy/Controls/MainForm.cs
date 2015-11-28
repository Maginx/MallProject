using System;
using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Runtime.InteropServices;
using ClassLibrary;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 主窗体
    /// </summary>
    internal sealed partial class MainForm : KryptonForm
    {
        [DllImport("MasterRDHID.dll")]
        static extern int rf_init_com(short icdev, int nVID, int nPID);
        [DllImport("MasterRDHID.dll")]
        static extern int rf_request(short icdev, byte mode, ref ushort pTagType);
        [DllImport("MasterRDHID.dll")]
        static extern int rf_anticoll(short icdev, byte bcnt, ref byte pSnr, ref byte pRLength);
        [DllImport("MasterRDHID.dll")]
        static extern int rf_select(short icdev, ref byte pSnr, byte srcLen, ref sbyte Size);
        [DllImport("MasterRDHID.dll")]
        static extern int rf_ClosePort();

        #region 显示内镜信息委托、事件
        public delegate void ShowEndoscInfoHandler(string endoscopeSN);
        public static event ShowEndoscInfoHandler ShowEndoscInfoEvent;
        #endregion

        private bool isComConnected = false;
        private string oldData = string.Empty;
        private System.Windows.Forms.Timer timerClean = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerPort = new System.Windows.Forms.Timer();

        #region 构造方法
        public MainForm()
        {
            InitializeComponent();

            ShowEndoscInfoEvent += new ShowEndoscInfoHandler(MainForm_ShowEndoscopeInfoMsgEvent);

            this.Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height + 20);
            EndoscTempTSMenuItem_Click(null, null);
            EndopscInfoTSMenuItem_Click(null, null);
            cleanMoniterMenueItem_Click(null, null);

            GSetting.AutoClean = DealXml.ReadSysConfig("appSettings", "cleanType");
            this.SetCleanType(GSetting.AutoClean);

        }
        #endregion

        /// <summary>
        /// 设置清洗类型
        /// </summary>
        /// <param name="type"></param>
        private void SetCleanType(string type)
        {
            if (type == "1")//手洗
            {
                this.cleanChoice2.Checked = true;
                this.cleanChoice.Checked = false;
            }
            else//机洗
            {
                this.cleanChoice.Checked = true;
                this.cleanChoice2.Checked = false;
            }
        }

        /// <summary>
        /// 显示内镜详细信息
        /// </summary>
        /// <param name="endoscopeSN">内镜编号</param>
        public static void ShowEndoscopeInfoMsg(string endoscopeSN)
        {
            ShowEndoscInfoEvent(endoscopeSN);
        }

        private void MainForm_ShowEndoscopeInfoMsgEvent(string endoscopeSN)
        {
            CleanTempMsg tempForm = GSetting.GetFormInstance<CleanTempMsg>();
            tempForm.MdiParent = this;
            this.contentPanel.Controls.Clear();
            tempForm.Size = this.contentPanel.Size;
            tempForm.Location = new Point(0, 0);
            tempForm.WindowState = FormWindowState.Maximized;
            this.contentPanel.Controls.Add(tempForm);
            this.statusStripTips.Visible = true;
            tempForm.Show();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = KryptonMessageBox.Show("确定退出程序吗？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                GSetting.ContentForms.Clear();
                Environment.Exit(0);
                //GlobalSetting.AnimateWindow(this.Handle, 500, GlobalSetting.AW_BLEND | GlobalSetting.AW_HIDE);
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 主窗体加载
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化信息
            GSetting.InitialbasicData();

            //this.ConnectRW208AH();
            timerPort.Interval = 300;
            timerPort.Tick += new EventHandler(timerPort_Tick);
            timerPort.Start();
            timerClean.Interval = 600;
            timerClean.Tick += new EventHandler(timerClean_Tick);
            timerClean.Start();
            // 用户登录
            //this.LoginUser();

            //this.mainHeader.ValuesPrimary.Description = string.Format("{0}[{1}]", Global.LoginedUser, Global.LoginedUserRole.GetAttributeName<ClassLibrary.UserRole>());

            // 导航到主页
            cleanMoniterMenueItem_Click(null, null);
        }

        void timerClean_Tick(object sender, EventArgs e)
        {
            this.oldData = string.Empty;
        }

        void timerPort_Tick(object sender, EventArgs e)
        {
            this.OpenRW208AH();
        }

        /// <summary>
        /// 打开OpenRW208AH
        /// </summary>
        private void OpenRW208AH()
        {
            short icdev = 0x0000;
            int status;
            byte bcnt = 0x04;
            byte[] pSnr = new byte[10];
            ushort tagtype = 0;
            byte pRLength = 0;
            byte srcLen = 0;
            sbyte Size = 0;
            if (!isComConnected)
            {
                //MessageBox.Show("串口未连接成功");
                return;
            }

            status = rf_request(icdev, 0x52, ref tagtype);
            if (0 != status)
            {
                //FormHelper.AppendText("寻卡失败", Color.Red);
                return;
            }
            status = rf_anticoll(icdev, bcnt, ref pSnr[0], ref pRLength);
            if (0 != status)
            {
                //FormHelper.AppendText("防冲突失败", Color.Red);
                return;
            }
            else
            {
                string cardno = string.Empty;
                string temp = string.Empty;
                for (int i = 0; i < pRLength; i++)
                {
                    temp = pSnr[i].ToString("X");
                    temp = temp.Length == 1 ? "0" + temp : temp;        //如果返回的是1字节的1个位，前面需要补0
                    cardno = cardno + temp;
                }
                if (oldData == cardno)
                {
                    return;
                }
                // 获取数据
                cardno += "00";
                GSetting.MData.AccepData = cardno.ToSafeString();
                oldData = cardno;
                FormHelper.AppendText(string.Format("卡号：{0}", cardno), Color.Green);
                FormHelper.AppendRichText(string.Format("卡号：{0}", cardno), Color.Green);
            }
            status = rf_select(icdev, ref pSnr[0], srcLen, ref Size);
            if (0 != status)
            {
                //FormHelper.AppendText("选卡失败", Color.Red);
                return;
            }
            System.Threading.Thread.Sleep(400);

        }
        /// <summary>
        /// 连接RW208AH打卡器
        /// </summary>
        private void ConnectRW208AH()
        {
            if (isComConnected)
            {
                isComConnected = false;
                rf_ClosePort();
                return;
            }

            int status;
            //注：对于USB免驱读卡器，port和baud这两个参数，可以是任意值
            //所以原来的程序不用更改。 这两个数字是固定的
            status = rf_init_com(0, 6790, 57360);
            if (0 == status)
            {
                isComConnected = true;
                FormHelper.AppendText("打卡器断开连接", Color.Red);
                FormHelper.AppendText("打卡器端口连接成功", Color.Green);
            }
            else
            {
                //FormHelper.AppendText("打卡器端口连接失败", Color.Red);
                isComConnected = false;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void LoginUser()
        {
            var login = new Login();
            DialogResult result = login.ShowDialog();

            if (result != DialogResult.OK)
            {
                KryptonMessageBox.Show("登录取消，退出程序");
                Environment.Exit(0);
            }

            login.Close();

            // 根据权限设置菜单
            this.mainHeader.ValuesPrimary.Description = string.Format("{0}[{1}]", Global.LoginedUser, Global.LoginedUserRole.GetAttributeName<ClassLibrary.UserRole>());

            this.toolStripDoctor.Visible = true;
            this.toolStripNurse.Visible = true;

            switch (Global.LoginedUserRole)
            {
                case UserRole.Washer:
                    this.toolStripDoctor.Visible = false;
                    this.toolStripNurse.Visible = false;
                    break;
                case UserRole.Nurse:
                    this.toolStripDoctor.Visible = false;
                    break;
                case UserRole.Dcotor:
                    this.toolStripNurse.Visible = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 端口设置
        /// </summary>
        private void portSetting_Click(object sender, EventArgs e)
        {
            Form porSet = GSetting.GetFormInstance<PortSet>();
            porSet.StartPosition = FormStartPosition.Manual;
            porSet.WindowState = FormWindowState.Normal;
            porSet.ShowDialog();
        }

        /// <summary>
        /// 服务地址设置
        /// </summary>
        private void serverBasicSetting_Click(object sender, EventArgs e)
        {
            Form settingForm = GSetting.GetFormInstance<BasicSetting>();
            settingForm.StartPosition = FormStartPosition.Manual;
            settingForm.WindowState = FormWindowState.Normal;
            settingForm.ShowDialog();
        }

        /// <summary>
        /// 保存临时信息面板
        /// </summary>
        private void CheckLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "文本(*.txt)|*.txt";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = fileDialog.FileName;
                this.SaveTemp(name);
            }
        }

        /// <summary>
        /// 清洗时间设置
        /// </summary>
        private void cleanTimeSetMenueItem_Click(object sender, EventArgs e)
        {
            Form CleanTime = GSetting.GetFormInstance<CleanTime>();
            CleanTime.StartPosition = FormStartPosition.Manual;
            CleanTime.WindowState = FormWindowState.Normal;
            CleanTime.ShowDialog();
        }

        /// <summary>
        /// 内镜清洗临时记录
        /// </summary>
        private void EndoscTempTSMenuItem_Click(object sender, EventArgs e)
        {
            labMark.Text = "内镜清洗临时记录";
            Form tempForm = GSetting.GetFormInstance<TempRecord>();
            tempForm.MdiParent = this;
            this.contentPanel.Controls.Clear();
            tempForm.StartPosition = FormStartPosition.Manual;
            tempForm.Location = new Point(0, 0);
            tempForm.Size = this.contentPanel.Size;
            tempForm.WindowState = FormWindowState.Maximized;
            this.contentPanel.Controls.Add(tempForm);
            this.statusStripTips.Visible = true;
            tempForm.Show();
            endoscopeCleanInfoMsg.Checked = false;
            tempReacord.Checked = true;
            cleanMoniterMenueItem.Checked = false;
        }

        /// <summary>
        /// 内镜清洗信息
        /// </summary>
        private void EndopscInfoTSMenuItem_Click(object sender, EventArgs e)
        {
            labMark.Text = "内镜清洗信息";
            this.ChangeContentControl<CleanTempMsg>();

            endoscopeCleanInfoMsg.Checked = true;
            tempReacord.Checked = false;
            cleanMoniterMenueItem.Checked = false;
        }

        /// <summary>
        /// 内镜清洗状态
        /// </summary>
        private void cleanMoniterMenueItem_Click(object sender, EventArgs e)
        {
            labMark.Text = "内镜清洗状态";
            ChangeContentControl<EndoscopeCleanState>();
            endoscopeCleanInfoMsg.Checked = false;
            tempReacord.Checked = false;
            cleanMoniterMenueItem.Checked = true;
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 最大化
        /// </summary>
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 自动清洗
        /// </summary>
        private void cleanChoice_Click(object sender, EventArgs e)
        {
            GSetting.AutoClean = "2";//自动清洗
            this.cleanChoice2.Checked = false;
            this.cleanChoice.Checked = true;
            this.mainHeader.ValuesPrimary.Heading = "自动清洗机";
            DealXml.ModifySysConfig("cleanType", "2");
        }

        /// <summary>
        /// 手动清洗
        /// </summary>
        private void cleanChocie2_Click(object sender, EventArgs e)
        {
            GSetting.AutoClean = "1";//手动清洗
            this.cleanChoice2.Checked = true;
            this.cleanChoice.Checked = false;
            this.mainHeader.ValuesPrimary.Heading = "手动清洗工作站";
            DealXml.ModifySysConfig("cleanType", "1");
        }

        /// <summary>
        /// 内镜管理
        /// </summary>
        private void toolstripMIEndoscope_Click(object sender, EventArgs e)
        {
            labMark.Text = "内镜管理";
            this.ChangeContentControl<EndoscopeManager>();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        private void toolStripMIUser_Click(object sender, EventArgs e)
        {
            labMark.Text = "用户管理";
            this.ChangeContentControl<UserManager>();
        }

        /// <summary>
        /// 追溯查询
        /// </summary>
        private void toolStripCleanSearch_Click(object sender, EventArgs e)
        {
            labMark.Text = "追溯查询";
            this.ChangeContentControl<DataTrace>();
        }


        /// <summary>
        ///  追溯查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTrace_Click(object sender, EventArgs e)
        {
            toolStripCleanSearch_Click(sender, e);
        }

        /// <summary>
        /// 测试连接服务器
        /// </summary>
        private void toolStripContact_Click(object sender, EventArgs e)
        {
            GSetting.ContactServer();
        }

        /// <summary>
        /// 密钥处理
        /// </summary>
        private void toolStripVerify_Click(object sender, EventArgs e)
        {
            EnterLicense enter = new EnterLicense();
            enter.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        #region Themes
        private void blackToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.PaletteMode = PaletteMode.Office2010Black;
        }

        private void blueToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.PaletteMode = PaletteMode.Office2010Blue;
        }

        private void silverToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.PaletteMode = PaletteMode.Office2010Silver;
        }

        private void toolStripWares_Click(object sender, EventArgs e)
        {
            var ws = new WareSetting();
            ws.ShowDialog();
        }
        #endregion

        #region 用户角色切换
        private void toolStripDoctor_Click(object sender, EventArgs e)
        {
            this.Hide();

            var up = GSetting.GetFormInstance<UpdateEndoscope>();

            if (DialogResult.OK == up.ShowDialog())
            {
                this.Show();
            }
        }

        private void toolStripNurse_Click(object sender, EventArgs e)
        {
            this.Hide();
            var pr = GSetting.GetFormInstance<PatientRegister>();

            if (DialogResult.OK == pr.ShowDialog())
            {
                this.Show();
            }
        }

        private void loginOut_Click(object sender, EventArgs e)
        {
            // 注销
            Global.LoginedUser = string.Empty;
            Global.LoginedUserRole = 0;
            this.mainHeader.ValuesPrimary.Description = string.Empty;
            this.LoginUser();
        }
        #endregion
    }
}
