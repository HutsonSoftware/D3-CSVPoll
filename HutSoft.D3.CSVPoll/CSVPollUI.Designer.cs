namespace HutSoft.D3.CSVPoll
{
    partial class CSVPollUI
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
            this.txtCsvFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerInstance = new System.Windows.Forms.TextBox();
            this.btnBrowseCsvFile = new System.Windows.Forms.Button();
            this.btnRefreshDatabases = new System.Windows.Forms.Button();
            this.cboAvailableDatabases = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPreview = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lstNewCsvEntries = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstExistingCsvEntries = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCsvFile
            // 
            this.txtCsvFile.Location = new System.Drawing.Point(11, 43);
            this.txtCsvFile.Name = "txtCsvFile";
            this.txtCsvFile.Size = new System.Drawing.Size(407, 20);
            this.txtCsvFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CSV File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server\\Instance:";
            // 
            // txtServerInstance
            // 
            this.txtServerInstance.Location = new System.Drawing.Point(11, 93);
            this.txtServerInstance.Name = "txtServerInstance";
            this.txtServerInstance.Size = new System.Drawing.Size(369, 20);
            this.txtServerInstance.TabIndex = 3;
            // 
            // btnBrowseCsvFile
            // 
            this.btnBrowseCsvFile.Location = new System.Drawing.Point(424, 41);
            this.btnBrowseCsvFile.Name = "btnBrowseCsvFile";
            this.btnBrowseCsvFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCsvFile.TabIndex = 4;
            this.btnBrowseCsvFile.Text = "Browse";
            this.btnBrowseCsvFile.UseVisualStyleBackColor = true;
            this.btnBrowseCsvFile.Click += new System.EventHandler(this.btnBrowseCsvFile_Click);
            // 
            // btnRefreshDatabases
            // 
            this.btnRefreshDatabases.Location = new System.Drawing.Point(386, 91);
            this.btnRefreshDatabases.Name = "btnRefreshDatabases";
            this.btnRefreshDatabases.Size = new System.Drawing.Size(113, 23);
            this.btnRefreshDatabases.TabIndex = 5;
            this.btnRefreshDatabases.Text = "Refresh Databases";
            this.btnRefreshDatabases.UseVisualStyleBackColor = true;
            this.btnRefreshDatabases.Click += new System.EventHandler(this.btnRefreshDatabases_Click);
            // 
            // cboAvailableDatabases
            // 
            this.cboAvailableDatabases.FormattingEnabled = true;
            this.cboAvailableDatabases.Location = new System.Drawing.Point(11, 145);
            this.cboAvailableDatabases.Name = "cboAvailableDatabases";
            this.cboAvailableDatabases.Size = new System.Drawing.Size(369, 21);
            this.cboAvailableDatabases.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Available Databases:";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(359, 354);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(140, 23);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "Import New CSV Entries";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(521, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.settingsToolStripMenuItem.Text = "Config";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(386, 143);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(113, 23);
            this.btnPreview.TabIndex = 17;
            this.btnPreview.Text = "Preview Changes";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lstNewCsvEntries);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lstExistingCsvEntries);
            this.groupBox1.Location = new System.Drawing.Point(12, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 149);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview Only";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "New CSV Entries:";
            // 
            // lstNewCsvEntries
            // 
            this.lstNewCsvEntries.FormattingEnabled = true;
            this.lstNewCsvEntries.Location = new System.Drawing.Point(252, 35);
            this.lstNewCsvEntries.Name = "lstNewCsvEntries";
            this.lstNewCsvEntries.Size = new System.Drawing.Size(229, 108);
            this.lstNewCsvEntries.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Existing CSV Entries:";
            // 
            // lstExistingCsvEntries
            // 
            this.lstExistingCsvEntries.FormattingEnabled = true;
            this.lstExistingCsvEntries.Location = new System.Drawing.Point(10, 35);
            this.lstExistingCsvEntries.Name = "lstExistingCsvEntries";
            this.lstExistingCsvEntries.Size = new System.Drawing.Size(229, 108);
            this.lstExistingCsvEntries.TabIndex = 20;
            // 
            // CSVPoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 393);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboAvailableDatabases);
            this.Controls.Add(this.btnRefreshDatabases);
            this.Controls.Add(this.btnBrowseCsvFile);
            this.Controls.Add(this.txtServerInstance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCsvFile);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CSVPoll";
            this.Text = "CSV Poll";
            this.Load += new System.EventHandler(this.CSVPoll_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCsvFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerInstance;
        private System.Windows.Forms.Button btnBrowseCsvFile;
        private System.Windows.Forms.Button btnRefreshDatabases;
        private System.Windows.Forms.ComboBox cboAvailableDatabases;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstNewCsvEntries;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstExistingCsvEntries;
    }
}

