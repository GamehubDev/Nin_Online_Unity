namespace AuthServer
{
    partial class AuthenticationServer
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
            this.components = new System.ComponentModel.Container();
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGameServer = new System.Windows.Forms.Label();
            this.grpGame = new System.Windows.Forms.GroupBox();
            this.lblServerList = new System.Windows.Forms.Label();
            this.listServers = new System.Windows.Forms.ListBox();
            this.btnRestartSingle = new System.Windows.Forms.Button();
            this.btnRestartAll = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.btnMassKick = new System.Windows.Forms.Button();
            this.grpAuth = new System.Windows.Forms.GroupBox();
            this.lblVersionStatus = new System.Windows.Forms.Label();
            this.lblRevisionOut = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMinorOut = new System.Windows.Forms.Label();
            this.lblMajorDot = new System.Windows.Forms.Label();
            this.lblMajorOut = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMinor = new System.Windows.Forms.Label();
            this.lblMajor = new System.Windows.Forms.Label();
            this.lblCurrentVer = new System.Windows.Forms.Label();
            this.checkDonors = new System.Windows.Forms.CheckBox();
            this.checkEveryone = new System.Windows.Forms.CheckBox();
            this.checkAdmins = new System.Windows.Forms.CheckBox();
            this.lblAllowed = new System.Windows.Forms.Label();
            this.btnRestartAuth = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tmCleaner = new System.Windows.Forms.Timer(this.components);
            this.txtRevision = new AuthServer.DoubleClickTextBox();
            this.txtMinor = new AuthServer.DoubleClickTextBox();
            this.txtMajor = new AuthServer.DoubleClickTextBox();
            this.grpGame.SuspendLayout();
            this.grpAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // richLog
            // 
            this.richLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richLog.BackColor = System.Drawing.Color.Black;
            this.richLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richLog.ForeColor = System.Drawing.SystemColors.Window;
            this.richLog.Location = new System.Drawing.Point(2, 22);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richLog.Size = new System.Drawing.Size(353, 440);
            this.richLog.TabIndex = 0;
            this.richLog.Text = "";
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(4, 6);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(43, 13);
            this.lblLog.TabIndex = 1;
            this.lblLog.Text = "Logger:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(401, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Functions";
            // 
            // lblGameServer
            // 
            this.lblGameServer.AutoSize = true;
            this.lblGameServer.Location = new System.Drawing.Point(359, 55);
            this.lblGameServer.Name = "lblGameServer";
            this.lblGameServer.Size = new System.Drawing.Size(0, 13);
            this.lblGameServer.TabIndex = 2;
            // 
            // grpGame
            // 
            this.grpGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGame.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grpGame.Controls.Add(this.lblServerList);
            this.grpGame.Controls.Add(this.listServers);
            this.grpGame.Controls.Add(this.btnRestartSingle);
            this.grpGame.Controls.Add(this.btnRestartAll);
            this.grpGame.Controls.Add(this.btnShutdown);
            this.grpGame.Controls.Add(this.btnMassKick);
            this.grpGame.Location = new System.Drawing.Point(361, 30);
            this.grpGame.Name = "grpGame";
            this.grpGame.Size = new System.Drawing.Size(181, 221);
            this.grpGame.TabIndex = 3;
            this.grpGame.TabStop = false;
            this.grpGame.Text = "Game servers";
            // 
            // lblServerList
            // 
            this.lblServerList.AutoSize = true;
            this.lblServerList.Location = new System.Drawing.Point(9, 103);
            this.lblServerList.Name = "lblServerList";
            this.lblServerList.Size = new System.Drawing.Size(63, 13);
            this.lblServerList.TabIndex = 2;
            this.lblServerList.Text = "Server\'s list:";
            // 
            // listServers
            // 
            this.listServers.FormattingEnabled = true;
            this.listServers.Location = new System.Drawing.Point(11, 122);
            this.listServers.Name = "listServers";
            this.listServers.Size = new System.Drawing.Size(141, 69);
            this.listServers.TabIndex = 1;
            // 
            // btnRestartSingle
            // 
            this.btnRestartSingle.Location = new System.Drawing.Point(28, 192);
            this.btnRestartSingle.Name = "btnRestartSingle";
            this.btnRestartSingle.Size = new System.Drawing.Size(107, 23);
            this.btnRestartSingle.TabIndex = 0;
            this.btnRestartSingle.Text = "Restart This";
            this.btnRestartSingle.UseVisualStyleBackColor = true;
            // 
            // btnRestartAll
            // 
            this.btnRestartAll.Location = new System.Drawing.Point(9, 77);
            this.btnRestartAll.Name = "btnRestartAll";
            this.btnRestartAll.Size = new System.Drawing.Size(162, 23);
            this.btnRestartAll.TabIndex = 0;
            this.btnRestartAll.Text = "Restart Servers";
            this.btnRestartAll.UseVisualStyleBackColor = true;
            // 
            // btnShutdown
            // 
            this.btnShutdown.Location = new System.Drawing.Point(9, 48);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(162, 23);
            this.btnShutdown.TabIndex = 0;
            this.btnShutdown.Text = "Shutdown servers";
            this.btnShutdown.UseVisualStyleBackColor = true;
            // 
            // btnMassKick
            // 
            this.btnMassKick.Location = new System.Drawing.Point(9, 19);
            this.btnMassKick.Name = "btnMassKick";
            this.btnMassKick.Size = new System.Drawing.Size(162, 23);
            this.btnMassKick.TabIndex = 0;
            this.btnMassKick.Text = "Masskick everyone";
            this.btnMassKick.UseVisualStyleBackColor = true;
            // 
            // grpAuth
            // 
            this.grpAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAuth.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grpAuth.Controls.Add(this.lblVersionStatus);
            this.grpAuth.Controls.Add(this.lblRevisionOut);
            this.grpAuth.Controls.Add(this.label5);
            this.grpAuth.Controls.Add(this.lblMinorOut);
            this.grpAuth.Controls.Add(this.lblMajorDot);
            this.grpAuth.Controls.Add(this.lblMajorOut);
            this.grpAuth.Controls.Add(this.label3);
            this.grpAuth.Controls.Add(this.label1);
            this.grpAuth.Controls.Add(this.lblMinor);
            this.grpAuth.Controls.Add(this.lblMajor);
            this.grpAuth.Controls.Add(this.txtRevision);
            this.grpAuth.Controls.Add(this.txtMinor);
            this.grpAuth.Controls.Add(this.txtMajor);
            this.grpAuth.Controls.Add(this.lblCurrentVer);
            this.grpAuth.Controls.Add(this.checkDonors);
            this.grpAuth.Controls.Add(this.checkEveryone);
            this.grpAuth.Controls.Add(this.checkAdmins);
            this.grpAuth.Controls.Add(this.lblAllowed);
            this.grpAuth.Controls.Add(this.btnRestartAuth);
            this.grpAuth.Location = new System.Drawing.Point(362, 262);
            this.grpAuth.Name = "grpAuth";
            this.grpAuth.Size = new System.Drawing.Size(181, 200);
            this.grpAuth.TabIndex = 3;
            this.grpAuth.TabStop = false;
            this.grpAuth.Text = "Auth Server";
            // 
            // lblVersionStatus
            // 
            this.lblVersionStatus.AutoSize = true;
            this.lblVersionStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblVersionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVersionStatus.Location = new System.Drawing.Point(119, 175);
            this.lblVersionStatus.Name = "lblVersionStatus";
            this.lblVersionStatus.Size = new System.Drawing.Size(46, 15);
            this.lblVersionStatus.TabIndex = 12;
            this.lblVersionStatus.Text = "Saved";
            this.lblVersionStatus.Click += new System.EventHandler(this.lblVersionStatus_Click);
            // 
            // lblRevisionOut
            // 
            this.lblRevisionOut.AutoSize = true;
            this.lblRevisionOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRevisionOut.Location = new System.Drawing.Point(89, 175);
            this.lblRevisionOut.Name = "lblRevisionOut";
            this.lblRevisionOut.Size = new System.Drawing.Size(15, 15);
            this.lblRevisionOut.TabIndex = 12;
            this.lblRevisionOut.Text = "6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(82, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = ".";
            // 
            // lblMinorOut
            // 
            this.lblMinorOut.AutoSize = true;
            this.lblMinorOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMinorOut.Location = new System.Drawing.Point(71, 175);
            this.lblMinorOut.Name = "lblMinorOut";
            this.lblMinorOut.Size = new System.Drawing.Size(15, 15);
            this.lblMinorOut.TabIndex = 10;
            this.lblMinorOut.Text = "0";
            // 
            // lblMajorDot
            // 
            this.lblMajorDot.AutoSize = true;
            this.lblMajorDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMajorDot.Location = new System.Drawing.Point(64, 175);
            this.lblMajorDot.Name = "lblMajorDot";
            this.lblMajorDot.Size = new System.Drawing.Size(12, 16);
            this.lblMajorDot.TabIndex = 9;
            this.lblMajorDot.Text = ".";
            // 
            // lblMajorOut
            // 
            this.lblMajorOut.AutoSize = true;
            this.lblMajorOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMajorOut.Location = new System.Drawing.Point(55, 175);
            this.lblMajorOut.Name = "lblMajorOut";
            this.lblMajorOut.Size = new System.Drawing.Size(15, 15);
            this.lblMajorOut.TabIndex = 9;
            this.lblMajorOut.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(10, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Current:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(126, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Revision";
            // 
            // lblMinor
            // 
            this.lblMinor.AutoSize = true;
            this.lblMinor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMinor.Location = new System.Drawing.Point(76, 129);
            this.lblMinor.Name = "lblMinor";
            this.lblMinor.Size = new System.Drawing.Size(29, 12);
            this.lblMinor.TabIndex = 7;
            this.lblMinor.Text = "Minor";
            // 
            // lblMajor
            // 
            this.lblMajor.AutoSize = true;
            this.lblMajor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMajor.Location = new System.Drawing.Point(19, 129);
            this.lblMajor.Name = "lblMajor";
            this.lblMajor.Size = new System.Drawing.Size(29, 12);
            this.lblMajor.TabIndex = 7;
            this.lblMajor.Text = "Major";
            // 
            // lblCurrentVer
            // 
            this.lblCurrentVer.AutoSize = true;
            this.lblCurrentVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCurrentVer.Location = new System.Drawing.Point(10, 114);
            this.lblCurrentVer.Name = "lblCurrentVer";
            this.lblCurrentVer.Size = new System.Drawing.Size(112, 13);
            this.lblCurrentVer.TabIndex = 3;
            this.lblCurrentVer.Text = "Check for this version:";
            // 
            // checkDonors
            // 
            this.checkDonors.AutoSize = true;
            this.checkDonors.Checked = true;
            this.checkDonors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDonors.Location = new System.Drawing.Point(13, 94);
            this.checkDonors.Name = "checkDonors";
            this.checkDonors.Size = new System.Drawing.Size(60, 17);
            this.checkDonors.TabIndex = 2;
            this.checkDonors.Text = "Donors";
            this.checkDonors.UseVisualStyleBackColor = true;
            this.checkDonors.CheckedChanged += new System.EventHandler(this.checkDonors_CheckedChanged);
            // 
            // checkEveryone
            // 
            this.checkEveryone.AutoSize = true;
            this.checkEveryone.Location = new System.Drawing.Point(97, 80);
            this.checkEveryone.Name = "checkEveryone";
            this.checkEveryone.Size = new System.Drawing.Size(71, 17);
            this.checkEveryone.TabIndex = 2;
            this.checkEveryone.Text = "Everyone";
            this.checkEveryone.UseVisualStyleBackColor = true;
            this.checkEveryone.CheckedChanged += new System.EventHandler(this.checkEveryone_CheckedChanged);
            // 
            // checkAdmins
            // 
            this.checkAdmins.AutoSize = true;
            this.checkAdmins.Checked = true;
            this.checkAdmins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAdmins.Location = new System.Drawing.Point(13, 70);
            this.checkAdmins.Name = "checkAdmins";
            this.checkAdmins.Size = new System.Drawing.Size(60, 17);
            this.checkAdmins.TabIndex = 2;
            this.checkAdmins.Text = "Admins";
            this.checkAdmins.UseVisualStyleBackColor = true;
            this.checkAdmins.CheckedChanged += new System.EventHandler(this.checkAdmins_CheckedChanged);
            // 
            // lblAllowed
            // 
            this.lblAllowed.AutoSize = true;
            this.lblAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAllowed.Location = new System.Drawing.Point(8, 51);
            this.lblAllowed.Name = "lblAllowed";
            this.lblAllowed.Size = new System.Drawing.Size(94, 13);
            this.lblAllowed.TabIndex = 1;
            this.lblAllowed.Text = "Allowed accounts:";
            // 
            // btnRestartAuth
            // 
            this.btnRestartAuth.Location = new System.Drawing.Point(8, 16);
            this.btnRestartAuth.Name = "btnRestartAuth";
            this.btnRestartAuth.Size = new System.Drawing.Size(167, 25);
            this.btnRestartAuth.TabIndex = 0;
            this.btnRestartAuth.Text = "Restart Auth Server";
            this.btnRestartAuth.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(281, 2);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(73, 20);
            this.btnClearLog.TabIndex = 4;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // tmCleaner
            // 
            this.tmCleaner.Enabled = true;
            this.tmCleaner.Interval = 60000;
            this.tmCleaner.Tick += new System.EventHandler(this.tmCleaner_Tick);
            // 
            // txtRevision
            // 
            this.txtRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRevision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtRevision.Location = new System.Drawing.Point(132, 142);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.Size = new System.Drawing.Size(32, 22);
            this.txtRevision.TabIndex = 6;
            this.txtRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRevision.TextChanged += new System.EventHandler(this.Version_TextChanged);
            // 
            // txtMinor
            // 
            this.txtMinor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMinor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMinor.Location = new System.Drawing.Point(74, 142);
            this.txtMinor.Name = "txtMinor";
            this.txtMinor.Size = new System.Drawing.Size(32, 22);
            this.txtMinor.TabIndex = 5;
            this.txtMinor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinor.TextChanged += new System.EventHandler(this.Version_TextChanged);
            // 
            // txtMajor
            // 
            this.txtMajor.BackColor = System.Drawing.Color.Lime;
            this.txtMajor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMajor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMajor.ForeColor = System.Drawing.Color.Black;
            this.txtMajor.Location = new System.Drawing.Point(17, 142);
            this.txtMajor.Name = "txtMajor";
            this.txtMajor.Size = new System.Drawing.Size(32, 22);
            this.txtMajor.TabIndex = 4;
            this.txtMajor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMajor.TextChanged += new System.EventHandler(this.Version_TextChanged);
            // 
            // AuthenticationServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 469);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.grpAuth);
            this.Controls.Add(this.grpGame);
            this.Controls.Add(this.lblGameServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.richLog);
            this.Name = "AuthenticationServer";
            this.Text = "Authentication Server - Nin Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthServer_FormClosing);
            this.Load += new System.EventHandler(this.AuthServer_Load);
            this.grpGame.ResumeLayout(false);
            this.grpGame.PerformLayout();
            this.grpAuth.ResumeLayout(false);
            this.grpAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGameServer;
        private System.Windows.Forms.GroupBox grpGame;
        private System.Windows.Forms.Label lblServerList;
        private System.Windows.Forms.ListBox listServers;
        private System.Windows.Forms.Button btnRestartSingle;
        private System.Windows.Forms.Button btnRestartAll;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnMassKick;
        private System.Windows.Forms.GroupBox grpAuth;
        private System.Windows.Forms.Button btnRestartAuth;
        private System.Windows.Forms.CheckBox checkDonors;
        private System.Windows.Forms.CheckBox checkAdmins;
        private System.Windows.Forms.Label lblAllowed;
        private System.Windows.Forms.CheckBox checkEveryone;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Timer tmCleaner;
        private System.Windows.Forms.Label lblCurrentVer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMinor;
        private System.Windows.Forms.Label lblMajor;
        private DoubleClickTextBox txtRevision;
        private DoubleClickTextBox txtMinor;
        private DoubleClickTextBox txtMajor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMinorOut;
        private System.Windows.Forms.Label lblMajorDot;
        private System.Windows.Forms.Label lblMajorOut;
        private System.Windows.Forms.Label lblRevisionOut;
        private System.Windows.Forms.Label lblVersionStatus;
    }
}

