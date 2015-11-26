namespace TracePatient
{
    partial class TracePatientForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.splitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.patientListDataView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientListHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.splitPanel = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.detailsPanel = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.labelLaout = new System.Windows.Forms.TableLayoutPanel();
            this.labWarName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labNurse = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labDoctor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtNurse = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDoctor = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.labPatientName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labEndoscope = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labPatient = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtEndoscopeNO = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtPatientname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtWardNO = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtPatientNo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtResult = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.labresults = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtGetPatient = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.records = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.lstRecords = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.menuList = new System.Windows.Forms.MenuStrip();
            this.verifyConnectionDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.setWardName = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel.Panel)).BeginInit();
            this.mainPanel.Panel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientListDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel.Panel1)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel.Panel2)).BeginInit();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailsPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailsPanel.Panel)).BeginInit();
            this.detailsPanel.Panel.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            this.labelLaout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.records)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.records.Panel)).BeginInit();
            this.records.Panel.SuspendLayout();
            this.records.SuspendLayout();
            this.menuList.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.HeaderVisiblePrimary = false;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Name = "mainPanel";
            // 
            // mainPanel.Panel
            // 
            this.mainPanel.Panel.Controls.Add(this.splitContainer);
            this.mainPanel.Size = new System.Drawing.Size(724, 538);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.ValuesPrimary.Heading = "迈尔数据采集";
            this.mainPanel.ValuesPrimary.Image = null;
            // 
            // splitContainer
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.patientListDataView);
            this.splitContainer.Panel1.Controls.Add(this.patientListHeader);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitPanel);
            this.splitContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.splitContainer.Size = new System.Drawing.Size(722, 515);
            this.splitContainer.SplitterDistance = 256;
            this.splitContainer.TabIndex = 0;
            // 
            // patientListDataView
            // 
            this.patientListDataView.AllowUserToDeleteRows = false;
            this.patientListDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientListDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name});
            this.patientListDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientListDataView.Location = new System.Drawing.Point(0, 31);
            this.patientListDataView.Name = "patientListDataView";
            this.patientListDataView.Size = new System.Drawing.Size(256, 484);
            this.patientListDataView.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.patientListDataView.StateCommon.DataCell.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.patientListDataView.StateCommon.DataCell.Border.Rounding = 3;
            this.patientListDataView.StateCommon.DataCell.Border.Width = 3;
            this.patientListDataView.TabIndex = 7;
            this.patientListDataView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.patientListDataView_RowPostPaint);
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.HeaderText = "病人名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // patientListHeader
            // 
            this.patientListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.patientListHeader.Location = new System.Drawing.Point(0, 0);
            this.patientListHeader.Name = "patientListHeader";
            this.patientListHeader.Size = new System.Drawing.Size(256, 31);
            this.patientListHeader.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.patientListHeader.StateCommon.Border.Rounding = 3;
            this.patientListHeader.StateCommon.Border.Width = 3;
            this.patientListHeader.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientListHeader.TabIndex = 8;
            this.patientListHeader.Values.Description = "序号 | 名字";
            this.patientListHeader.Values.Heading = "病人列表";
            this.patientListHeader.Values.Image = null;
            // 
            // splitPanel
            // 
            this.splitPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPanel.Location = new System.Drawing.Point(0, 0);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.detailsPanel);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.records);
            this.splitPanel.Size = new System.Drawing.Size(461, 515);
            this.splitPanel.SplitterDistance = 292;
            this.splitPanel.TabIndex = 0;
            // 
            // detailsPanel
            // 
            this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsPanel.Location = new System.Drawing.Point(0, 0);
            this.detailsPanel.Name = "detailsPanel";
            // 
            // detailsPanel.Panel
            // 
            this.detailsPanel.Panel.Controls.Add(this.labelLaout);
            this.detailsPanel.Size = new System.Drawing.Size(461, 292);
            this.detailsPanel.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.detailsPanel.StateCommon.Border.Rounding = 5;
            this.detailsPanel.StateCommon.Border.Width = 2;
            this.detailsPanel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsPanel.TabIndex = 1;
            this.detailsPanel.Text = "详细信息";
            this.detailsPanel.Values.Heading = "详细信息";
            // 
            // labelLaout
            // 
            this.labelLaout.BackColor = System.Drawing.Color.Transparent;
            this.labelLaout.ColumnCount = 2;
            this.labelLaout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.labelLaout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.labelLaout.Controls.Add(this.labWarName, 0, 5);
            this.labelLaout.Controls.Add(this.labNurse, 0, 4);
            this.labelLaout.Controls.Add(this.labDoctor, 0, 3);
            this.labelLaout.Controls.Add(this.txtNurse, 1, 4);
            this.labelLaout.Controls.Add(this.txtDoctor, 1, 3);
            this.labelLaout.Controls.Add(this.labPatientName, 0, 0);
            this.labelLaout.Controls.Add(this.labEndoscope, 0, 2);
            this.labelLaout.Controls.Add(this.labPatient, 0, 1);
            this.labelLaout.Controls.Add(this.txtEndoscopeNO, 1, 2);
            this.labelLaout.Controls.Add(this.txtPatientname, 1, 1);
            this.labelLaout.Controls.Add(this.txtWardNO, 1, 5);
            this.labelLaout.Controls.Add(this.txtPatientNo, 1, 0);
            this.labelLaout.Controls.Add(this.txtResult, 1, 6);
            this.labelLaout.Controls.Add(this.labresults, 0, 6);
            this.labelLaout.Controls.Add(this.txtGetPatient, 1, 7);
            this.labelLaout.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLaout.Location = new System.Drawing.Point(0, 0);
            this.labelLaout.Name = "labelLaout";
            this.labelLaout.RowCount = 8;
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.26829F));
            this.labelLaout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.19512F));
            this.labelLaout.Size = new System.Drawing.Size(453, 269);
            this.labelLaout.TabIndex = 1;
            // 
            // labWarName
            // 
            this.labWarName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labWarName.Location = new System.Drawing.Point(3, 133);
            this.labWarName.Name = "labWarName";
            this.labWarName.Size = new System.Drawing.Size(63, 19);
            this.labWarName.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labWarName.TabIndex = 14;
            this.labWarName.Values.Text = "检查室:";
            // 
            // labNurse
            // 
            this.labNurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNurse.Location = new System.Drawing.Point(3, 107);
            this.labNurse.Name = "labNurse";
            this.labNurse.Size = new System.Drawing.Size(48, 19);
            this.labNurse.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNurse.TabIndex = 12;
            this.labNurse.Values.Text = "护士:";
            // 
            // labDoctor
            // 
            this.labDoctor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDoctor.Location = new System.Drawing.Point(3, 81);
            this.labDoctor.Name = "labDoctor";
            this.labDoctor.Size = new System.Drawing.Size(48, 19);
            this.labDoctor.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDoctor.TabIndex = 11;
            this.labDoctor.Values.Text = "医生:";
            // 
            // txtNurse
            // 
            this.txtNurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNurse.Location = new System.Drawing.Point(103, 107);
            this.txtNurse.Name = "txtNurse";
            this.txtNurse.Size = new System.Drawing.Size(175, 21);
            this.txtNurse.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtNurse.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNurse.StateCommon.Border.Rounding = 4;
            this.txtNurse.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNurse.TabIndex = 9;
            // 
            // txtDoctor
            // 
            this.txtDoctor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDoctor.Location = new System.Drawing.Point(103, 81);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(175, 21);
            this.txtDoctor.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtDoctor.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtDoctor.StateCommon.Border.Rounding = 4;
            this.txtDoctor.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctor.TabIndex = 7;
            // 
            // labPatientName
            // 
            this.labPatientName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPatientName.Location = new System.Drawing.Point(3, 3);
            this.labPatientName.Name = "labPatientName";
            this.labPatientName.Size = new System.Drawing.Size(84, 20);
            this.labPatientName.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatientName.StateCommon.ShortText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Switch33;
            this.labPatientName.StateCommon.ShortText.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatientName.StateNormal.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.labPatientName.TabIndex = 0;
            this.labPatientName.Values.Text = "病人编码：";
            // 
            // labEndoscope
            // 
            this.labEndoscope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEndoscope.ImeMode = System.Windows.Forms.ImeMode.HangulFull;
            this.labEndoscope.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.labEndoscope.Location = new System.Drawing.Point(3, 55);
            this.labEndoscope.Name = "labEndoscope";
            this.labEndoscope.Size = new System.Drawing.Size(78, 19);
            this.labEndoscope.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEndoscope.TabIndex = 2;
            this.labEndoscope.Values.Text = "镜子编码:";
            // 
            // labPatient
            // 
            this.labPatient.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPatient.Location = new System.Drawing.Point(3, 29);
            this.labPatient.Name = "labPatient";
            this.labPatient.Size = new System.Drawing.Size(63, 19);
            this.labPatient.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatient.TabIndex = 10;
            this.labPatient.Values.Text = "病人名:";
            // 
            // txtEndoscopeNO
            // 
            this.txtEndoscopeNO.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEndoscopeNO.Location = new System.Drawing.Point(103, 55);
            this.txtEndoscopeNO.Name = "txtEndoscopeNO";
            this.txtEndoscopeNO.Size = new System.Drawing.Size(175, 21);
            this.txtEndoscopeNO.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtEndoscopeNO.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtEndoscopeNO.StateCommon.Border.Rounding = 4;
            this.txtEndoscopeNO.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndoscopeNO.TabIndex = 5;
            // 
            // txtPatientname
            // 
            this.txtPatientname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPatientname.Location = new System.Drawing.Point(103, 29);
            this.txtPatientname.Name = "txtPatientname";
            this.txtPatientname.Size = new System.Drawing.Size(175, 21);
            this.txtPatientname.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtPatientname.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtPatientname.StateCommon.Border.Rounding = 4;
            this.txtPatientname.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientname.TabIndex = 15;
            // 
            // txtWardNO
            // 
            this.txtWardNO.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtWardNO.Location = new System.Drawing.Point(103, 133);
            this.txtWardNO.Name = "txtWardNO";
            this.txtWardNO.Size = new System.Drawing.Size(175, 21);
            this.txtWardNO.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtWardNO.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtWardNO.StateCommon.Border.Rounding = 4;
            this.txtWardNO.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWardNO.TabIndex = 13;
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPatientNo.Location = new System.Drawing.Point(103, 3);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.Size = new System.Drawing.Size(175, 21);
            this.txtPatientNo.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtPatientNo.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtPatientNo.StateCommon.Border.Rounding = 4;
            this.txtPatientNo.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientNo.TabIndex = 1;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtResult.Location = new System.Drawing.Point(103, 159);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(347, 71);
            this.txtResult.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtResult.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtResult.StateCommon.Border.Rounding = 4;
            this.txtResult.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.TabIndex = 19;
            // 
            // labresults
            // 
            this.labresults.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labresults.Location = new System.Drawing.Point(3, 185);
            this.labresults.Name = "labresults";
            this.labresults.Size = new System.Drawing.Size(78, 19);
            this.labresults.StateCommon.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labresults.TabIndex = 18;
            this.labresults.Values.Text = "诊断结果:";
            // 
            // txtGetPatient
            // 
            this.txtGetPatient.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtGetPatient.Location = new System.Drawing.Point(103, 237);
            this.txtGetPatient.Name = "txtGetPatient";
            this.txtGetPatient.Size = new System.Drawing.Size(132, 29);
            this.txtGetPatient.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetPatient.TabIndex = 20;
            this.txtGetPatient.Values.Text = "获取病人信息";
            // 
            // records
            // 
            this.records.Dock = System.Windows.Forms.DockStyle.Fill;
            this.records.Location = new System.Drawing.Point(0, 0);
            this.records.Name = "records";
            // 
            // records.Panel
            // 
            this.records.Panel.Controls.Add(this.lstRecords);
            this.records.Size = new System.Drawing.Size(461, 218);
            this.records.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.records.TabIndex = 0;
            this.records.Text = "操作记录";
            this.records.Values.Heading = "操作记录";
            // 
            // lstRecords
            // 
            this.lstRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecords.Items.AddRange(new object[] {
            "1q23",
            "WQEQW"});
            this.lstRecords.Location = new System.Drawing.Point(0, 0);
            this.lstRecords.Name = "lstRecords";
            this.lstRecords.Size = new System.Drawing.Size(457, 195);
            this.lstRecords.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.Green;
            this.lstRecords.TabIndex = 1;
            // 
            // menuList
            // 
            this.menuList.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.menuList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verifyConnectionDataBase,
            this.setWardName});
            this.menuList.Location = new System.Drawing.Point(0, 0);
            this.menuList.Name = "menuList";
            this.menuList.Size = new System.Drawing.Size(724, 24);
            this.menuList.TabIndex = 1;
            this.menuList.Text = "menuStrip1";
            // 
            // verifyConnectionDataBase
            // 
            this.verifyConnectionDataBase.Name = "verifyConnectionDataBase";
            this.verifyConnectionDataBase.Size = new System.Drawing.Size(103, 20);
            this.verifyConnectionDataBase.Text = "验证数据库连接";
            this.verifyConnectionDataBase.Click += new System.EventHandler(this.verifyConnectionDataBase_Click);
            // 
            // setWardName
            // 
            this.setWardName.Name = "setWardName";
            this.setWardName.Size = new System.Drawing.Size(91, 20);
            this.setWardName.Text = "设置当前诊室";
            // 
            // TracePatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 562);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuList;
            this.Name = "TracePatientForm";
            this.Text = "病人数据采集";
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel.Panel)).EndInit();
            this.mainPanel.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientListDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel.Panel1)).EndInit();
            this.splitPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel.Panel2)).EndInit();
            this.splitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detailsPanel.Panel)).EndInit();
            this.detailsPanel.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detailsPanel)).EndInit();
            this.detailsPanel.ResumeLayout(false);
            this.labelLaout.ResumeLayout(false);
            this.labelLaout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.records.Panel)).EndInit();
            this.records.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.records)).EndInit();
            this.records.ResumeLayout(false);
            this.menuList.ResumeLayout(false);
            this.menuList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup mainPanel;
        private System.Windows.Forms.MenuStrip menuList;
        private System.Windows.Forms.ToolStripMenuItem verifyConnectionDataBase;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem setWardName;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer splitPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox records;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lstRecords;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox detailsPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView patientListDataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader patientListHeader;
        private System.Windows.Forms.TableLayoutPanel labelLaout;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labWarName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labNurse;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labDoctor;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtNurse;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDoctor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labPatientName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labEndoscope;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labPatient;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEndoscopeNO;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPatientname;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtWardNO;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPatientNo;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtResult;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labresults;
        private ComponentFactory.Krypton.Toolkit.KryptonButton txtGetPatient;
    }
}

