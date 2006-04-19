namespace EVEMon
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpEmailSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMailServer = new System.Windows.Forms.TextBox();
            this.tbFromAddress = new System.Windows.Forms.TextBox();
            this.tbToAddress = new System.Windows.Forms.TextBox();
            this.cbEmailServerRequireSsl = new System.Windows.Forms.CheckBox();
            this.cbEmailAuthRequired = new System.Windows.Forms.CheckBox();
            this.tlpEmailAuthTable = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEmailUsername = new System.Windows.Forms.TextBox();
            this.tbEmailPassword = new System.Windows.Forms.TextBox();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.cbSendEmail = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTitleToTime = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpEmailSettings.SuspendLayout();
            this.tlpEmailAuthTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(16, 19);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(102, 17);
            this.cbMinimizeToTray.TabIndex = 0;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(287, 372);
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
            this.btnCancel.Location = new System.Drawing.Point(206, 372);
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
            this.groupBox1.Controls.Add(this.cbTitleToTime);
            this.groupBox1.Controls.Add(this.cbMinimizeToTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 71);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Controls.Add(this.btnTestEmail);
            this.groupBox2.Controls.Add(this.cbSendEmail);
            this.groupBox2.Location = new System.Drawing.Point(12, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 272);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alert Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpEmailSettings, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 42);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(338, 192);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tlpEmailSettings
            // 
            this.tlpEmailSettings.AutoSize = true;
            this.tlpEmailSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmailSettings.ColumnCount = 2;
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.Controls.Add(this.label1, 0, 0);
            this.tlpEmailSettings.Controls.Add(this.label2, 0, 4);
            this.tlpEmailSettings.Controls.Add(this.label3, 0, 5);
            this.tlpEmailSettings.Controls.Add(this.tbMailServer, 1, 0);
            this.tlpEmailSettings.Controls.Add(this.tbFromAddress, 1, 4);
            this.tlpEmailSettings.Controls.Add(this.tbToAddress, 1, 5);
            this.tlpEmailSettings.Controls.Add(this.cbEmailServerRequireSsl, 1, 1);
            this.tlpEmailSettings.Controls.Add(this.cbEmailAuthRequired, 1, 2);
            this.tlpEmailSettings.Controls.Add(this.tlpEmailAuthTable, 1, 3);
            this.tlpEmailSettings.Location = new System.Drawing.Point(23, 3);
            this.tlpEmailSettings.Name = "tlpEmailSettings";
            this.tlpEmailSettings.RowCount = 6;
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.Size = new System.Drawing.Size(291, 184);
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
            this.label2.Location = new System.Drawing.Point(3, 132);
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
            this.label3.Location = new System.Drawing.Point(13, 158);
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
            this.tbFromAddress.Location = new System.Drawing.Point(82, 135);
            this.tbFromAddress.Name = "tbFromAddress";
            this.tbFromAddress.Size = new System.Drawing.Size(206, 20);
            this.tbFromAddress.TabIndex = 4;
            // 
            // tbToAddress
            // 
            this.tbToAddress.Location = new System.Drawing.Point(82, 161);
            this.tbToAddress.Name = "tbToAddress";
            this.tbToAddress.Size = new System.Drawing.Size(206, 20);
            this.tbToAddress.TabIndex = 5;
            // 
            // cbEmailServerRequireSsl
            // 
            this.cbEmailServerRequireSsl.AutoSize = true;
            this.cbEmailServerRequireSsl.Location = new System.Drawing.Point(82, 29);
            this.cbEmailServerRequireSsl.Name = "cbEmailServerRequireSsl";
            this.cbEmailServerRequireSsl.Size = new System.Drawing.Size(117, 17);
            this.cbEmailServerRequireSsl.TabIndex = 6;
            this.cbEmailServerRequireSsl.Text = "Connect using SSL";
            this.cbEmailServerRequireSsl.UseVisualStyleBackColor = true;
            // 
            // cbEmailAuthRequired
            // 
            this.cbEmailAuthRequired.AutoSize = true;
            this.cbEmailAuthRequired.Location = new System.Drawing.Point(82, 52);
            this.cbEmailAuthRequired.Name = "cbEmailAuthRequired";
            this.cbEmailAuthRequired.Size = new System.Drawing.Size(122, 17);
            this.cbEmailAuthRequired.TabIndex = 7;
            this.cbEmailAuthRequired.Text = "Server requires login";
            this.cbEmailAuthRequired.UseVisualStyleBackColor = true;
            this.cbEmailAuthRequired.CheckedChanged += new System.EventHandler(this.cbEmailAuthRequired_CheckedChanged);
            // 
            // tlpEmailAuthTable
            // 
            this.tlpEmailAuthTable.AutoSize = true;
            this.tlpEmailAuthTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmailAuthTable.ColumnCount = 2;
            this.tlpEmailAuthTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailAuthTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailAuthTable.Controls.Add(this.label5, 0, 1);
            this.tlpEmailAuthTable.Controls.Add(this.label4, 0, 0);
            this.tlpEmailAuthTable.Controls.Add(this.tbEmailUsername, 1, 0);
            this.tlpEmailAuthTable.Controls.Add(this.tbEmailPassword, 1, 1);
            this.tlpEmailAuthTable.Location = new System.Drawing.Point(82, 75);
            this.tlpEmailAuthTable.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.tlpEmailAuthTable.Name = "tlpEmailAuthTable";
            this.tlpEmailAuthTable.RowCount = 2;
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.Size = new System.Drawing.Size(199, 52);
            this.tlpEmailAuthTable.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbEmailUsername
            // 
            this.tbEmailUsername.Location = new System.Drawing.Point(67, 3);
            this.tbEmailUsername.Name = "tbEmailUsername";
            this.tbEmailUsername.Size = new System.Drawing.Size(129, 20);
            this.tbEmailUsername.TabIndex = 9;
            // 
            // tbEmailPassword
            // 
            this.tbEmailPassword.Location = new System.Drawing.Point(67, 29);
            this.tbEmailPassword.Name = "tbEmailPassword";
            this.tbEmailPassword.PasswordChar = '*';
            this.tbEmailPassword.Size = new System.Drawing.Size(129, 20);
            this.tbEmailPassword.TabIndex = 10;
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestEmail.Location = new System.Drawing.Point(234, 243);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(110, 23);
            this.btnTestEmail.TabIndex = 3;
            this.btnTestEmail.Text = "Send Test Email";
            this.btnTestEmail.UseVisualStyleBackColor = true;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // cbSendEmail
            // 
            this.cbSendEmail.AutoSize = true;
            this.cbSendEmail.Location = new System.Drawing.Point(16, 19);
            this.cbSendEmail.Name = "cbSendEmail";
            this.cbSendEmail.Size = new System.Drawing.Size(215, 17);
            this.cbSendEmail.TabIndex = 0;
            this.cbSendEmail.Text = "Send email when skill training completes";
            this.cbSendEmail.UseVisualStyleBackColor = true;
            this.cbSendEmail.CheckedChanged += new System.EventHandler(this.cbSendEmail_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 40);
            this.label6.TabIndex = 8;
            this.label6.Text = "Server Password:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Email Server:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 26);
            this.label8.TabIndex = 1;
            this.label8.Text = "From address:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTitleToTime
            // 
            this.cbTitleToTime.AutoSize = true;
            this.cbTitleToTime.Location = new System.Drawing.Point(16, 43);
            this.cbTitleToTime.Name = "cbTitleToTime";
            this.cbTitleToTime.Size = new System.Drawing.Size(171, 17);
            this.cbTitleToTime.TabIndex = 1;
            this.cbTitleToTime.Text = "Set window title to training time";
            this.cbTitleToTime.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 407);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpEmailSettings.ResumeLayout(false);
            this.tlpEmailSettings.PerformLayout();
            this.tlpEmailAuthTable.ResumeLayout(false);
            this.tlpEmailAuthTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbEmailServerRequireSsl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbEmailPassword;
        private System.Windows.Forms.TextBox tbEmailUsername;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cbEmailAuthRequired;
        private System.Windows.Forms.TableLayoutPanel tlpEmailAuthTable;
        private System.Windows.Forms.CheckBox cbTitleToTime;
    }
}