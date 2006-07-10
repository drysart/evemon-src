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
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbRunIGBServer = new System.Windows.Forms.CheckBox();
            this.cbWorksafeMode = new System.Windows.Forms.CheckBox();
            this.cbTitleToTime = new System.Windows.Forms.CheckBox();
            this.cbPlaySoundOnSkillComplete = new System.Windows.Forms.CheckBox();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbRelocateEveWindow = new System.Windows.Forms.CheckBox();
            this.flpScreenSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbScreenList = new System.Windows.Forms.ComboBox();
            this.btnIdentifyScreens = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.verticalFlowPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.vfpCustomProxySettings = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tbProxyHttpHost = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnProxyHttpAuth = new System.Windows.Forms.Button();
            this.tbProxyHttpPort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rbCustomProxy = new System.Windows.Forms.RadioButton();
            this.rbDefaultProxy = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpEmailSettings.SuspendLayout();
            this.tlpEmailAuthTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.verticalFlowPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpScreenSelect.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.verticalFlowPanel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.verticalFlowPanel4.SuspendLayout();
            this.vfpCustomProxySettings.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.verticalFlowPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(12, 3);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(103, 17);
            this.cbMinimizeToTray.TabIndex = 0;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(84, 3);
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
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbRunIGBServer
            // 
            this.cbRunIGBServer.AutoSize = true;
            this.cbRunIGBServer.Location = new System.Drawing.Point(12, 3);
            this.cbRunIGBServer.Name = "cbRunIGBServer";
            this.cbRunIGBServer.Size = new System.Drawing.Size(217, 17);
            this.cbRunIGBServer.TabIndex = 3;
            this.cbRunIGBServer.Text = "Run IGB Mini-server on http://localhost/";
            this.cbRunIGBServer.UseVisualStyleBackColor = true;
            // 
            // cbWorksafeMode
            // 
            this.cbWorksafeMode.AutoSize = true;
            this.cbWorksafeMode.Location = new System.Drawing.Point(12, 49);
            this.cbWorksafeMode.Name = "cbWorksafeMode";
            this.cbWorksafeMode.Size = new System.Drawing.Size(271, 17);
            this.cbWorksafeMode.TabIndex = 2;
            this.cbWorksafeMode.Text = "Run in \"safe for work\" mode (no portraits or colors)";
            this.cbWorksafeMode.UseVisualStyleBackColor = true;
            // 
            // cbTitleToTime
            // 
            this.cbTitleToTime.AutoSize = true;
            this.cbTitleToTime.Location = new System.Drawing.Point(12, 26);
            this.cbTitleToTime.Name = "cbTitleToTime";
            this.cbTitleToTime.Size = new System.Drawing.Size(177, 17);
            this.cbTitleToTime.TabIndex = 1;
            this.cbTitleToTime.Text = "Set window title to training time";
            this.cbTitleToTime.UseVisualStyleBackColor = true;
            // 
            // cbPlaySoundOnSkillComplete
            // 
            this.cbPlaySoundOnSkillComplete.AutoSize = true;
            this.cbPlaySoundOnSkillComplete.Location = new System.Drawing.Point(12, 3);
            this.cbPlaySoundOnSkillComplete.Name = "cbPlaySoundOnSkillComplete";
            this.cbPlaySoundOnSkillComplete.Size = new System.Drawing.Size(216, 17);
            this.cbPlaySoundOnSkillComplete.TabIndex = 5;
            this.cbPlaySoundOnSkillComplete.Text = "Play sound when skill training completes";
            this.cbPlaySoundOnSkillComplete.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpEmailSettings, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 26);
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
            this.tlpEmailSettings.Location = new System.Drawing.Point(22, 3);
            this.tlpEmailSettings.Name = "tlpEmailSettings";
            this.tlpEmailSettings.RowCount = 6;
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.Size = new System.Drawing.Size(294, 189);
            this.tlpEmailSettings.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Server:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "From address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "To address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMailServer
            // 
            this.tbMailServer.Location = new System.Drawing.Point(85, 3);
            this.tbMailServer.Name = "tbMailServer";
            this.tbMailServer.Size = new System.Drawing.Size(152, 21);
            this.tbMailServer.TabIndex = 3;
            // 
            // tbFromAddress
            // 
            this.tbFromAddress.Location = new System.Drawing.Point(85, 138);
            this.tbFromAddress.Name = "tbFromAddress";
            this.tbFromAddress.Size = new System.Drawing.Size(206, 21);
            this.tbFromAddress.TabIndex = 4;
            // 
            // tbToAddress
            // 
            this.tbToAddress.Location = new System.Drawing.Point(85, 165);
            this.tbToAddress.Name = "tbToAddress";
            this.tbToAddress.Size = new System.Drawing.Size(206, 21);
            this.tbToAddress.TabIndex = 5;
            // 
            // cbEmailServerRequireSsl
            // 
            this.cbEmailServerRequireSsl.AutoSize = true;
            this.cbEmailServerRequireSsl.Location = new System.Drawing.Point(85, 30);
            this.cbEmailServerRequireSsl.Name = "cbEmailServerRequireSsl";
            this.cbEmailServerRequireSsl.Size = new System.Drawing.Size(114, 17);
            this.cbEmailServerRequireSsl.TabIndex = 6;
            this.cbEmailServerRequireSsl.Text = "Connect using SSL";
            this.cbEmailServerRequireSsl.UseVisualStyleBackColor = true;
            // 
            // cbEmailAuthRequired
            // 
            this.cbEmailAuthRequired.AutoSize = true;
            this.cbEmailAuthRequired.Location = new System.Drawing.Point(85, 53);
            this.cbEmailAuthRequired.Name = "cbEmailAuthRequired";
            this.cbEmailAuthRequired.Size = new System.Drawing.Size(125, 17);
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
            this.tlpEmailAuthTable.Location = new System.Drawing.Point(85, 76);
            this.tlpEmailAuthTable.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.tlpEmailAuthTable.Name = "tlpEmailAuthTable";
            this.tlpEmailAuthTable.RowCount = 2;
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.Size = new System.Drawing.Size(200, 54);
            this.tlpEmailAuthTable.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 27);
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
            this.label4.Size = new System.Drawing.Size(59, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbEmailUsername
            // 
            this.tbEmailUsername.Location = new System.Drawing.Point(68, 3);
            this.tbEmailUsername.Name = "tbEmailUsername";
            this.tbEmailUsername.Size = new System.Drawing.Size(129, 21);
            this.tbEmailUsername.TabIndex = 9;
            // 
            // tbEmailPassword
            // 
            this.tbEmailPassword.Location = new System.Drawing.Point(68, 30);
            this.tbEmailPassword.Name = "tbEmailPassword";
            this.tbEmailPassword.PasswordChar = '*';
            this.tbEmailPassword.Size = new System.Drawing.Size(129, 21);
            this.tbEmailPassword.TabIndex = 10;
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestEmail.Location = new System.Drawing.Point(240, 224);
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
            this.cbSendEmail.Location = new System.Drawing.Point(12, 3);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 492);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.verticalFlowPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(472, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel1
            // 
            this.verticalFlowPanel1.AutoSize = true;
            this.verticalFlowPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel1.Controls.Add(this.groupBox1);
            this.verticalFlowPanel1.Controls.Add(this.groupBox2);
            this.verticalFlowPanel1.Controls.Add(this.groupBox5);
            this.verticalFlowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel1.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel1.Name = "verticalFlowPanel1";
            this.verticalFlowPanel1.Size = new System.Drawing.Size(466, 460);
            this.verticalFlowPanel1.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.flowLayoutPanel1);
            this.groupBox5.Location = new System.Drawing.Point(3, 147);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(460, 72);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "EVE Window Relocation";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cbRelocateEveWindow);
            this.flowLayoutPanel1.Controls.Add(this.flpScreenSelect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(454, 52);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // cbRelocateEveWindow
            // 
            this.cbRelocateEveWindow.AutoSize = true;
            this.cbRelocateEveWindow.Location = new System.Drawing.Point(12, 3);
            this.cbRelocateEveWindow.Name = "cbRelocateEveWindow";
            this.cbRelocateEveWindow.Size = new System.Drawing.Size(214, 17);
            this.cbRelocateEveWindow.TabIndex = 0;
            this.cbRelocateEveWindow.Text = "Relocate windowed EVE to fill a monitor";
            this.cbRelocateEveWindow.UseVisualStyleBackColor = true;
            this.cbRelocateEveWindow.CheckedChanged += new System.EventHandler(this.cbRelocateEveWindow_CheckedChanged);
            // 
            // flpScreenSelect
            // 
            this.flpScreenSelect.AutoSize = true;
            this.flpScreenSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpScreenSelect.Controls.Add(this.flowLayoutPanel4);
            this.flpScreenSelect.Controls.Add(this.btnIdentifyScreens);
            this.flpScreenSelect.Enabled = false;
            this.flpScreenSelect.Location = new System.Drawing.Point(34, 23);
            this.flpScreenSelect.Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.flpScreenSelect.Name = "flpScreenSelect";
            this.flpScreenSelect.Size = new System.Drawing.Size(288, 29);
            this.flpScreenSelect.TabIndex = 7;
            this.flpScreenSelect.WrapContents = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.label9);
            this.flowLayoutPanel4.Controls.Add(this.cbScreenList);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(180, 27);
            this.flowLayoutPanel4.TabIndex = 2;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 27);
            this.label9.TabIndex = 2;
            this.label9.Text = "Monitor:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbScreenList
            // 
            this.cbScreenList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenList.FormattingEnabled = true;
            this.cbScreenList.Location = new System.Drawing.Point(56, 3);
            this.cbScreenList.Name = "cbScreenList";
            this.cbScreenList.Size = new System.Drawing.Size(121, 21);
            this.cbScreenList.TabIndex = 1;
            // 
            // btnIdentifyScreens
            // 
            this.btnIdentifyScreens.AutoSize = true;
            this.btnIdentifyScreens.Location = new System.Drawing.Point(183, 3);
            this.btnIdentifyScreens.Name = "btnIdentifyScreens";
            this.btnIdentifyScreens.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnIdentifyScreens.Size = new System.Drawing.Size(102, 23);
            this.btnIdentifyScreens.TabIndex = 3;
            this.btnIdentifyScreens.Text = "Identify Screens";
            this.btnIdentifyScreens.UseVisualStyleBackColor = true;
            this.btnIdentifyScreens.Click += new System.EventHandler(this.btnIdentifyScreens_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.flowLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(3, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 43);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IGB Mini-server";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.cbRunIGBServer);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(454, 23);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Settings";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.cbMinimizeToTray);
            this.flowLayoutPanel2.Controls.Add(this.cbTitleToTime);
            this.flowLayoutPanel2.Controls.Add(this.cbWorksafeMode);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(454, 69);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.verticalFlowPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(472, 466);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Network";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel3
            // 
            this.verticalFlowPanel3.Controls.Add(this.groupBox6);
            this.verticalFlowPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel3.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel3.Name = "verticalFlowPanel3";
            this.verticalFlowPanel3.Size = new System.Drawing.Size(466, 460);
            this.verticalFlowPanel3.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.AutoSize = true;
            this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox6.Controls.Add(this.verticalFlowPanel4);
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(460, 141);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Proxy Server Settings";
            // 
            // verticalFlowPanel4
            // 
            this.verticalFlowPanel4.AutoSize = true;
            this.verticalFlowPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel4.Controls.Add(this.rbDefaultProxy);
            this.verticalFlowPanel4.Controls.Add(this.rbCustomProxy);
            this.verticalFlowPanel4.Controls.Add(this.vfpCustomProxySettings);
            this.verticalFlowPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel4.Location = new System.Drawing.Point(3, 17);
            this.verticalFlowPanel4.Name = "verticalFlowPanel4";
            this.verticalFlowPanel4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.verticalFlowPanel4.Size = new System.Drawing.Size(454, 121);
            this.verticalFlowPanel4.TabIndex = 1;
            // 
            // vfpCustomProxySettings
            // 
            this.vfpCustomProxySettings.AutoSize = true;
            this.vfpCustomProxySettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vfpCustomProxySettings.Controls.Add(this.label13);
            this.vfpCustomProxySettings.Controls.Add(this.tableLayoutPanel3);
            this.vfpCustomProxySettings.Enabled = false;
            this.vfpCustomProxySettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.vfpCustomProxySettings.Location = new System.Drawing.Point(35, 49);
            this.vfpCustomProxySettings.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.vfpCustomProxySettings.Name = "vfpCustomProxySettings";
            this.vfpCustomProxySettings.Size = new System.Drawing.Size(406, 69);
            this.vfpCustomProxySettings.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbProxyHttpHost, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnProxyHttpAuth, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbProxyHttpPort, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(400, 50);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(51, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 27);
            this.label10.TabIndex = 0;
            this.label10.Text = "HTTP:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbProxyHttpHost
            // 
            this.tbProxyHttpHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbProxyHttpHost.Location = new System.Drawing.Point(93, 26);
            this.tbProxyHttpHost.Name = "tbProxyHttpHost";
            this.tbProxyHttpHost.Size = new System.Drawing.Size(107, 21);
            this.tbProxyHttpHost.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(206, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Port";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(93, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Host/IP Address";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnProxyHttpAuth
            // 
            this.btnProxyHttpAuth.AutoSize = true;
            this.btnProxyHttpAuth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProxyHttpAuth.Location = new System.Drawing.Point(250, 26);
            this.btnProxyHttpAuth.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnProxyHttpAuth.Name = "btnProxyHttpAuth";
            this.btnProxyHttpAuth.Size = new System.Drawing.Size(99, 23);
            this.btnProxyHttpAuth.TabIndex = 5;
            this.btnProxyHttpAuth.Text = "Authentication...";
            this.btnProxyHttpAuth.UseVisualStyleBackColor = true;
            this.btnProxyHttpAuth.Click += new System.EventHandler(this.btnProxyHttpAuth_Click);
            // 
            // tbProxyHttpPort
            // 
            this.tbProxyHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbProxyHttpPort.Location = new System.Drawing.Point(206, 26);
            this.tbProxyHttpPort.Name = "tbProxyHttpPort";
            this.tbProxyHttpPort.Size = new System.Drawing.Size(38, 21);
            this.tbProxyHttpPort.TabIndex = 2;
            this.tbProxyHttpPort.TextChanged += new System.EventHandler(this.tbProxyHttpPort_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(400, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Please enter the server and port to use as your proxy server:";
            // 
            // rbCustomProxy
            // 
            this.rbCustomProxy.AutoSize = true;
            this.rbCustomProxy.Location = new System.Drawing.Point(13, 26);
            this.rbCustomProxy.Name = "rbCustomProxy";
            this.rbCustomProxy.Size = new System.Drawing.Size(428, 17);
            this.rbCustomProxy.TabIndex = 3;
            this.rbCustomProxy.TabStop = true;
            this.rbCustomProxy.Text = "Use custom proxy settings:";
            this.rbCustomProxy.UseVisualStyleBackColor = true;
            this.rbCustomProxy.CheckedChanged += new System.EventHandler(this.rbCustomProxy_CheckedChanged);
            // 
            // rbDefaultProxy
            // 
            this.rbDefaultProxy.AutoSize = true;
            this.rbDefaultProxy.Location = new System.Drawing.Point(13, 3);
            this.rbDefaultProxy.Name = "rbDefaultProxy";
            this.rbDefaultProxy.Size = new System.Drawing.Size(428, 17);
            this.rbDefaultProxy.TabIndex = 2;
            this.rbDefaultProxy.TabStop = true;
            this.rbDefaultProxy.Text = "Use system default proxy (from Control Panel)";
            this.rbDefaultProxy.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.verticalFlowPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(472, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Alerts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel2
            // 
            this.verticalFlowPanel2.AutoSize = true;
            this.verticalFlowPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel2.Controls.Add(this.groupBox4);
            this.verticalFlowPanel2.Controls.Add(this.groupBox3);
            this.verticalFlowPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel2.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel2.Name = "verticalFlowPanel2";
            this.verticalFlowPanel2.Size = new System.Drawing.Size(466, 460);
            this.verticalFlowPanel2.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.flowLayoutPanel6);
            this.groupBox3.Location = new System.Drawing.Point(3, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 269);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Email Alert";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.cbSendEmail);
            this.flowLayoutPanel6.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel6.Controls.Add(this.btnTestEmail);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel6.Size = new System.Drawing.Size(454, 249);
            this.flowLayoutPanel6.TabIndex = 5;
            this.flowLayoutPanel6.WrapContents = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSize = true;
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.flowLayoutPanel5);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 42);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sound Alert";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.cbPlaySoundOnSkillComplete);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel5.Size = new System.Drawing.Size(454, 22);
            this.flowLayoutPanel5.TabIndex = 6;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel7.Controls.Add(this.tabControl1);
            this.flowLayoutPanel7.Controls.Add(this.flowLayoutPanel8);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(597, 596);
            this.flowLayoutPanel7.TabIndex = 6;
            this.flowLayoutPanel7.WrapContents = false;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel8.AutoSize = true;
            this.flowLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel8.Controls.Add(this.btnOk);
            this.flowLayoutPanel8.Controls.Add(this.btnCancel);
            this.flowLayoutPanel8.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(321, 498);
            this.flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel8.TabIndex = 6;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(597, 596);
            this.Controls.Add(this.flowLayoutPanel7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpEmailSettings.ResumeLayout(false);
            this.tlpEmailSettings.PerformLayout();
            this.tlpEmailAuthTable.ResumeLayout(false);
            this.tlpEmailAuthTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.verticalFlowPanel1.ResumeLayout(false);
            this.verticalFlowPanel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flpScreenSelect.ResumeLayout(false);
            this.flpScreenSelect.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.verticalFlowPanel3.ResumeLayout(false);
            this.verticalFlowPanel3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.verticalFlowPanel4.ResumeLayout(false);
            this.verticalFlowPanel4.PerformLayout();
            this.vfpCustomProxySettings.ResumeLayout(false);
            this.vfpCustomProxySettings.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.verticalFlowPanel2.ResumeLayout(false);
            this.verticalFlowPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMinimizeToTray;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
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
        private System.Windows.Forms.CheckBox cbWorksafeMode;
        private System.Windows.Forms.CheckBox cbPlaySoundOnSkillComplete;
        private System.Windows.Forms.CheckBox cbRunIGBServer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel1;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbRelocateEveWindow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbScreenList;
        private System.Windows.Forms.Button btnIdentifyScreens;
        private System.Windows.Forms.FlowLayoutPanel flpScreenSelect;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel4;
        private System.Windows.Forms.FlowLayoutPanel vfpCustomProxySettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbProxyHttpPort;
        private System.Windows.Forms.TextBox tbProxyHttpHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnProxyHttpAuth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rbCustomProxy;
        private System.Windows.Forms.RadioButton rbDefaultProxy;
    }
}
