namespace EveCharacterMonitor
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.tlpEmailSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMailServer = new System.Windows.Forms.TextBox();
            this.tbFromAddress = new System.Windows.Forms.TextBox();
            this.tbToAddress = new System.Windows.Forms.TextBox();
            this.cbSendEmail = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tlpEmailSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(17, 28);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(102, 17);
            this.cbMinimizeToTray.TabIndex = 0;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(287, 246);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(206, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbMinimizeToTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 61);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnTestEmail);
            this.groupBox2.Controls.Add(this.tlpEmailSettings);
            this.groupBox2.Controls.Add(this.cbSendEmail);
            this.groupBox2.Location = new System.Drawing.Point(12, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 157);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alert Settings";
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestEmail.Location = new System.Drawing.Point(234, 128);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(110, 23);
            this.btnTestEmail.TabIndex = 3;
            this.btnTestEmail.Text = "Send Test Email";
            this.btnTestEmail.UseVisualStyleBackColor = true;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // tlpEmailSettings
            // 
            this.tlpEmailSettings.AutoSize = true;
            this.tlpEmailSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmailSettings.ColumnCount = 2;
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.Controls.Add(this.label1, 0, 0);
            this.tlpEmailSettings.Controls.Add(this.label2, 0, 1);
            this.tlpEmailSettings.Controls.Add(this.label3, 0, 2);
            this.tlpEmailSettings.Controls.Add(this.tbMailServer, 1, 0);
            this.tlpEmailSettings.Controls.Add(this.tbFromAddress, 1, 1);
            this.tlpEmailSettings.Controls.Add(this.tbToAddress, 1, 2);
            this.tlpEmailSettings.Location = new System.Drawing.Point(30, 43);
            this.tlpEmailSettings.Name = "tlpEmailSettings";
            this.tlpEmailSettings.RowCount = 3;
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.Size = new System.Drawing.Size(291, 78);
            this.tlpEmailSettings.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Server:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "From address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "To address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMailServer
            // 
            this.tbMailServer.Location = new System.Drawing.Point(82, 3);
            this.tbMailServer.Name = "tbMailServer";
            this.tbMailServer.Size = new System.Drawing.Size(152, 20);
            this.tbMailServer.TabIndex = 3;
            // 
            // tbFromAddress
            // 
            this.tbFromAddress.Location = new System.Drawing.Point(82, 29);
            this.tbFromAddress.Name = "tbFromAddress";
            this.tbFromAddress.Size = new System.Drawing.Size(206, 20);
            this.tbFromAddress.TabIndex = 4;
            // 
            // tbToAddress
            // 
            this.tbToAddress.Location = new System.Drawing.Point(82, 55);
            this.tbToAddress.Name = "tbToAddress";
            this.tbToAddress.Size = new System.Drawing.Size(206, 20);
            this.tbToAddress.TabIndex = 5;
            // 
            // cbSendEmail
            // 
            this.cbSendEmail.AutoSize = true;
            this.cbSendEmail.Location = new System.Drawing.Point(17, 20);
            this.cbSendEmail.Name = "cbSendEmail";
            this.cbSendEmail.Size = new System.Drawing.Size(215, 17);
            this.cbSendEmail.TabIndex = 0;
            this.cbSendEmail.Text = "Send email when skill training completes";
            this.cbSendEmail.UseVisualStyleBackColor = true;
            this.cbSendEmail.CheckedChanged += new System.EventHandler(this.cbSendEmail_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 281);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tlpEmailSettings.ResumeLayout(false);
            this.tlpEmailSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMinimizeToTray;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbSendEmail;
        private System.Windows.Forms.TableLayoutPanel tlpEmailSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMailServer;
        private System.Windows.Forms.TextBox tbFromAddress;
        private System.Windows.Forms.TextBox tbToAddress;
        private System.Windows.Forms.Button btnTestEmail;
    }
}