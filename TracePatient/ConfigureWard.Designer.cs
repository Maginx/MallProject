namespace TracePatient
{
    partial class ConfigureWardForm
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnConfigure = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.labWardName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtWardNo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnConfigure);
            this.kryptonPanel1.Controls.Add(this.labWardName);
            this.kryptonPanel1.Controls.Add(this.txtWardNo);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(220, 91);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnConfigure
            // 
            this.btnConfigure.Location = new System.Drawing.Point(23, 44);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(172, 34);
            this.btnConfigure.TabIndex = 2;
            this.btnConfigure.Values.Text = "设置";
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // labWardName
            // 
            this.labWardName.Location = new System.Drawing.Point(23, 16);
            this.labWardName.Name = "labWardName";
            this.labWardName.Size = new System.Drawing.Size(70, 19);
            this.labWardName.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labWardName.TabIndex = 1;
            this.labWardName.Values.Text = "检查室：";
            // 
            // txtWardNo
            // 
            this.txtWardNo.Location = new System.Drawing.Point(89, 12);
            this.txtWardNo.Name = "txtWardNo";
            this.txtWardNo.Size = new System.Drawing.Size(106, 26);
            this.txtWardNo.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtWardNo.StateCommon.Border.Rounding = 3;
            this.txtWardNo.StateCommon.Border.Width = 3;
            this.txtWardNo.TabIndex = 0;
            // 
            // ConfigureWardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 91);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ConfigureWardForm";
            this.Text = "设置检查室";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnConfigure;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labWardName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtWardNo;
    }
}