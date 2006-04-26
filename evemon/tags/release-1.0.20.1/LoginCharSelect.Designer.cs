namespace EVEMon
{
    partial class LoginCharSelect
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
            this.btnCharSelect = new System.Windows.Forms.Button();
            this.tbCharName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCharacterType = new System.Windows.Forms.ComboBox();
            this.gbEveLogin = new System.Windows.Forms.GroupBox();
            this.gbSavedXML = new System.Windows.Forms.GroupBox();
            this.cbMonitorFile = new System.Windows.Forms.CheckBox();
            this.ofdOpenXml = new System.Windows.Forms.OpenFileDialog();
            this.gbEveLogin.SuspendLayout();
            this.gbSavedXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCharSelect
            // 
            this.btnCharSelect.Location = new System.Drawing.Point(234, 109);
            this.btnCharSelect.Name = "btnCharSelect";
            this.btnCharSelect.Size = new System.Drawing.Size(30, 23);
            this.btnCharSelect.TabIndex = 2;
            this.btnCharSelect.Text = "...";
            this.btnCharSelect.UseVisualStyleBackColor = true;
            this.btnCharSelect.Click += new System.EventHandler(this.btnCharSelect_Click);
            // 
            // tbCharName
            // 
            this.tbCharName.Location = new System.Drawing.Point(128, 109);
            this.tbCharName.Name = "tbCharName";
            this.tbCharName.ReadOnly = true;
            this.tbCharName.Size = new System.Drawing.Size(100, 21);
            this.tbCharName.TabIndex = 3;
            this.tbCharName.TabStop = false;
            this.tbCharName.Text = "(None)";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(128, 82);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(100, 21);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(128, 55);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(100, 21);
            this.tbUsername.TabIndex = 0;
            this.tbUsername.TextChanged += new System.EventHandler(this.tbUsername_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Character:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Enter your EVE Online login and choose a character:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(581, 204);
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
            this.btnCancel.Location = new System.Drawing.Point(500, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Specify a saved Character XML file to use:";
            // 
            // tbFilename
            // 
            this.tbFilename.Location = new System.Drawing.Point(18, 55);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.ReadOnly = true;
            this.tbFilename.Size = new System.Drawing.Size(200, 21);
            this.tbFilename.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(224, 55);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Add character using:";
            // 
            // cbCharacterType
            // 
            this.cbCharacterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharacterType.FormattingEnabled = true;
            this.cbCharacterType.Items.AddRange(new object[] {
            "EVE Online Login",
            "Saved Character XML"});
            this.cbCharacterType.Location = new System.Drawing.Point(125, 12);
            this.cbCharacterType.Name = "cbCharacterType";
            this.cbCharacterType.Size = new System.Drawing.Size(144, 21);
            this.cbCharacterType.TabIndex = 6;
            this.cbCharacterType.SelectedIndexChanged += new System.EventHandler(this.cbCharacterType_SelectedIndexChanged);
            // 
            // gbEveLogin
            // 
            this.gbEveLogin.Controls.Add(this.btnCharSelect);
            this.gbEveLogin.Controls.Add(this.label4);
            this.gbEveLogin.Controls.Add(this.tbCharName);
            this.gbEveLogin.Controls.Add(this.label1);
            this.gbEveLogin.Controls.Add(this.label3);
            this.gbEveLogin.Controls.Add(this.tbPassword);
            this.gbEveLogin.Controls.Add(this.tbUsername);
            this.gbEveLogin.Controls.Add(this.label2);
            this.gbEveLogin.Location = new System.Drawing.Point(12, 39);
            this.gbEveLogin.Name = "gbEveLogin";
            this.gbEveLogin.Size = new System.Drawing.Size(318, 154);
            this.gbEveLogin.TabIndex = 7;
            this.gbEveLogin.TabStop = false;
            this.gbEveLogin.Text = "EVE Online Login";
            // 
            // gbSavedXML
            // 
            this.gbSavedXML.Controls.Add(this.cbMonitorFile);
            this.gbSavedXML.Controls.Add(this.btnBrowse);
            this.gbSavedXML.Controls.Add(this.label5);
            this.gbSavedXML.Controls.Add(this.tbFilename);
            this.gbSavedXML.Location = new System.Drawing.Point(336, 39);
            this.gbSavedXML.Name = "gbSavedXML";
            this.gbSavedXML.Size = new System.Drawing.Size(318, 154);
            this.gbSavedXML.TabIndex = 8;
            this.gbSavedXML.TabStop = false;
            this.gbSavedXML.Text = "Saved Character XML";
            // 
            // cbMonitorFile
            // 
            this.cbMonitorFile.AutoSize = true;
            this.cbMonitorFile.Location = new System.Drawing.Point(18, 81);
            this.cbMonitorFile.Name = "cbMonitorFile";
            this.cbMonitorFile.Size = new System.Drawing.Size(224, 17);
            this.cbMonitorFile.TabIndex = 3;
            this.cbMonitorFile.Text = "Monitor this file for updates automatically";
            this.cbMonitorFile.UseVisualStyleBackColor = true;
            // 
            // ofdOpenXml
            // 
            this.ofdOpenXml.Filter = "Character XML File (*.xml)|*.xml";
            this.ofdOpenXml.Title = "Open Character XML";
            // 
            // LoginCharSelect
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(668, 239);
            this.ControlBox = false;
            this.Controls.Add(this.gbSavedXML);
            this.Controls.Add(this.gbEveLogin);
            this.Controls.Add(this.cbCharacterType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginCharSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Character";
            this.Load += new System.EventHandler(this.LoginCharSelect_Load);
            this.gbEveLogin.ResumeLayout(false);
            this.gbEveLogin.PerformLayout();
            this.gbSavedXML.ResumeLayout(false);
            this.gbSavedXML.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbCharName;
        private System.Windows.Forms.Button btnCharSelect;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCharacterType;
        private System.Windows.Forms.GroupBox gbEveLogin;
        private System.Windows.Forms.GroupBox gbSavedXML;
        private System.Windows.Forms.CheckBox cbMonitorFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenXml;
    }
}