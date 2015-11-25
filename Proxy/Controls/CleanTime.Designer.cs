namespace ProxyClient.Controls
{
    partial class CleanTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSpecAdd = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecDelete = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridStepTime = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStepTime)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripAdd,
            this.toolStripMenuRemove});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // save
            // 
            this.save.Image = global::ProxyClient.Properties.Resources._221;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(59, 20);
            this.save.Text = "保存";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripAdd
            // 
            this.toolStripAdd.Image = global::ProxyClient.Properties.Resources._20130512102237769_easyicon_net_16;
            this.toolStripAdd.Name = "toolStripAdd";
            this.toolStripAdd.Size = new System.Drawing.Size(59, 20);
            this.toolStripAdd.Text = "添加";
            this.toolStripAdd.ToolTipText = "添加新节点";
            this.toolStripAdd.Visible = false;
            this.toolStripAdd.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuRemove
            // 
            this.toolStripMenuRemove.Image = global::ProxyClient.Properties.Resources._2013051210224445_easyicon_net_16;
            this.toolStripMenuRemove.Name = "toolStripMenuRemove";
            this.toolStripMenuRemove.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuRemove.Text = "删除";
            this.toolStripMenuRemove.Visible = false;
            this.toolStripMenuRemove.Click += new System.EventHandler(this.toolStripMenuRemove_Click);
            // 
            // buttonSpecAdd
            // 
            this.buttonSpecAdd.Image = global::ProxyClient.Properties.Resources.Add;
            this.buttonSpecAdd.UniqueName = "55215230BA484E3E60BF8CCAA64A74E3";
            // 
            // buttonSpecDelete
            // 
            this.buttonSpecDelete.Image = global::ProxyClient.Properties.Resources.delete;
            this.buttonSpecDelete.UniqueName = "4A8C17720E1A4BDC7D98AA23C5602AA1";
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Text = "加1";
            this.buttonSpecAny1.UniqueName = "8159E41116224DC2F09323E6B921618B";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dataGridStepTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 233);
            this.panel1.TabIndex = 2;
            // 
            // dataGridStepTime
            // 
            this.dataGridStepTime.AllowUserToAddRows = false;
            this.dataGridStepTime.AllowUserToDeleteRows = false;
            this.dataGridStepTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridStepTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.time,
            this.remark});
            this.dataGridStepTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridStepTime.Location = new System.Drawing.Point(0, 0);
            this.dataGridStepTime.Name = "dataGridStepTime";
            this.dataGridStepTime.RowTemplate.Height = 23;
            this.dataGridStepTime.Size = new System.Drawing.Size(380, 229);
            this.dataGridStepTime.TabIndex = 3;
            this.dataGridStepTime.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridStepTime_RowPostPaint);
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.FillWeight = 28.60161F;
            this.name.HeaderText = "清洗步骤";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // time
            // 
            this.time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.time.FillWeight = 27.74357F;
            this.time.HeaderText = "清洗时间";
            this.time.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.time.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.time.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.time.Name = "time";
            this.time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.time.Width = 167;
            // 
            // remark
            // 
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Visible = false;
            // 
            // CleanTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 257);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MaximumSize = new System.Drawing.Size(400, 295);
            this.MinimumSize = new System.Drawing.Size(400, 295);
            this.Name = "CleanTime";
            this.Text = "清洗步骤时间设置";
            this.Load += new System.EventHandler(this.CleanTime_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStepTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem save;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAdd;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecDelete;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridStepTime;
        private System.Windows.Forms.ToolStripMenuItem toolStripAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}