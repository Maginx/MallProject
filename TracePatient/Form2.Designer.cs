namespace TracePatient
{
    partial class Form2
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
            this.patientListDataView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientListHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            ((System.ComponentModel.ISupportInitialize)(this.patientListDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // patientListDataView
            // 
            this.patientListDataView.AllowUserToAddRows = false;
            this.patientListDataView.AllowUserToDeleteRows = false;
            this.patientListDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientListDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name});
            this.patientListDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientListDataView.Location = new System.Drawing.Point(0, 31);
            this.patientListDataView.Name = "patientListDataView";
            this.patientListDataView.ReadOnly = true;
            this.patientListDataView.Size = new System.Drawing.Size(284, 231);
            this.patientListDataView.TabIndex = 3;
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
            this.patientListHeader.Size = new System.Drawing.Size(284, 31);
            this.patientListHeader.TabIndex = 4;
            this.patientListHeader.Values.Description = "序号 | 名字";
            this.patientListHeader.Values.Heading = "病人列表";
            this.patientListHeader.Values.Image = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.patientListDataView);
            this.Controls.Add(this.patientListHeader);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.patientListDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView patientListDataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader patientListHeader;
    }
}