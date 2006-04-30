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
            this.verticalFlowPanel1 = new EVEMon.Common.VerticalFlowPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel2 = new EVEMon.Common.VerticalFlowPanel();
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
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
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
            this.verticalFlowPanel1.Controls.Add(this.groupBox2);
            this.verticalFlowPanel1.Controls.Add(this.groupBox1);
            this.verticalFlowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel1.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel1.Name = "verticalFlowPanel1";
            this.verticalFlowPanel1.Size = new System.Drawing.Size(466, 460);
            this.verticalFlowPanel1.TabIndex = 7;
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
            this.verticalFlowPanel2.Controls.Add(this.groupBox3);
            this.verticalFlowPanel2.Controls.Add(this.groupBox4);
            this.verticalFlowPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.groupBox3.Location = new System.Drawing.Point(3, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 270);
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
            this.flowLayoutPanel6.Size = new System.Drawing.Size(454, 250);
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
            this.groupBox4.Size = new System.Drawing.Size(460, 43);
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
            this.flowLayoutPanel5.Size = new System.Drawing.Size(454, 23);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
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
        private EVEMon.Common.VerticalFlowPanel verticalFlowPanel1;
        private EVEMon.Common.VerticalFlowPanel verticalFlowPanel2;
    }
}