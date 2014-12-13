namespace LinkedInRecommendation
{
    partial class FormMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLogin = new System.Windows.Forms.TabPage();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pictureBoxLogin = new System.Windows.Forms.PictureBox();
            this.tabVerify = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVerificationCode = new System.Windows.Forms.TextBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabWait = new System.Windows.Forms.TabPage();
            this.pictureBoxWait = new System.Windows.Forms.PictureBox();
            this.tabAnalytic = new System.Windows.Forms.TabPage();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblColorExplanation = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).BeginInit();
            this.tabVerify.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).BeginInit();
            this.tabAnalytic.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabLogin);
            this.tabControl1.Controls.Add(this.tabVerify);
            this.tabControl1.Controls.Add(this.tabWait);
            this.tabControl1.Controls.Add(this.tabAnalytic);
            this.tabControl1.Location = new System.Drawing.Point(0, -2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 563);
            this.tabControl1.TabIndex = 0;
            // 
            // tabLogin
            // 
            this.tabLogin.Controls.Add(this.btnExit);
            this.tabLogin.Controls.Add(this.btnLogin);
            this.tabLogin.Controls.Add(this.pictureBoxLogin);
            this.tabLogin.Location = new System.Drawing.Point(4, 4);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogin.Size = new System.Drawing.Size(776, 537);
            this.tabLogin.TabIndex = 0;
            this.tabLogin.Text = "Login";
            this.tabLogin.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(478, 382);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(154, 71);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(144, 382);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(154, 71);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pictureBoxLogin
            // 
            this.pictureBoxLogin.BackgroundImage = global::LinkedInRecommendation.Properties.Resources.pizap_com14154921157121;
            this.pictureBoxLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLogin.InitialImage = null;
            this.pictureBoxLogin.Location = new System.Drawing.Point(-4, 0);
            this.pictureBoxLogin.Name = "pictureBoxLogin";
            this.pictureBoxLogin.Size = new System.Drawing.Size(780, 534);
            this.pictureBoxLogin.TabIndex = 2;
            this.pictureBoxLogin.TabStop = false;
            // 
            // tabVerify
            // 
            this.tabVerify.Controls.Add(this.groupBox1);
            this.tabVerify.Controls.Add(this.webBrowser1);
            this.tabVerify.Location = new System.Drawing.Point(4, 4);
            this.tabVerify.Name = "tabVerify";
            this.tabVerify.Padding = new System.Windows.Forms.Padding(3);
            this.tabVerify.Size = new System.Drawing.Size(776, 537);
            this.tabVerify.TabIndex = 1;
            this.tabVerify.Text = "Verify";
            this.tabVerify.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVerificationCode);
            this.groupBox1.Controls.Add(this.btnVerify);
            this.groupBox1.Location = new System.Drawing.Point(482, 433);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 64);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Verification Code Here:";
            // 
            // txtVerificationCode
            // 
            this.txtVerificationCode.Location = new System.Drawing.Point(25, 27);
            this.txtVerificationCode.Name = "txtVerificationCode";
            this.txtVerificationCode.Size = new System.Drawing.Size(115, 20);
            this.txtVerificationCode.TabIndex = 1;
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(168, 25);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 2;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(770, 531);
            this.webBrowser1.TabIndex = 0;
            // 
            // tabWait
            // 
            this.tabWait.Controls.Add(this.pictureBoxWait);
            this.tabWait.Location = new System.Drawing.Point(4, 4);
            this.tabWait.Name = "tabWait";
            this.tabWait.Padding = new System.Windows.Forms.Padding(3);
            this.tabWait.Size = new System.Drawing.Size(776, 537);
            this.tabWait.TabIndex = 3;
            this.tabWait.Text = "Wait";
            this.tabWait.UseVisualStyleBackColor = true;
            // 
            // pictureBoxWait
            // 
            this.pictureBoxWait.BackgroundImage = global::LinkedInRecommendation.Properties.Resources.pizap_com14154971798191;
            this.pictureBoxWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxWait.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxWait.Name = "pictureBoxWait";
            this.pictureBoxWait.Size = new System.Drawing.Size(776, 534);
            this.pictureBoxWait.TabIndex = 0;
            this.pictureBoxWait.TabStop = false;
            // 
            // tabAnalytic
            // 
            this.tabAnalytic.Controls.Add(this.lblColorExplanation);
            this.tabAnalytic.Controls.Add(this.lblExplanation);
            this.tabAnalytic.Location = new System.Drawing.Point(4, 4);
            this.tabAnalytic.Name = "tabAnalytic";
            this.tabAnalytic.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnalytic.Size = new System.Drawing.Size(776, 537);
            this.tabAnalytic.TabIndex = 2;
            this.tabAnalytic.Text = "Analytic";
            this.tabAnalytic.UseVisualStyleBackColor = true;
            // 
            // lblExplanation
            // 
            this.lblExplanation.AutoSize = true;
            this.lblExplanation.Location = new System.Drawing.Point(9, 11);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(35, 13);
            this.lblExplanation.TabIndex = 0;
            this.lblExplanation.Text = "label1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // lblColorExplanation
            // 
            this.lblColorExplanation.AutoSize = true;
            this.lblColorExplanation.Location = new System.Drawing.Point(9, 33);
            this.lblColorExplanation.Name = "lblColorExplanation";
            this.lblColorExplanation.Size = new System.Drawing.Size(35, 13);
            this.lblColorExplanation.TabIndex = 1;
            this.lblColorExplanation.Text = "label1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).EndInit();
            this.tabVerify.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabWait.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).EndInit();
            this.tabAnalytic.ResumeLayout(false);
            this.tabAnalytic.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLogin;
        private System.Windows.Forms.TabPage tabVerify;
        private System.Windows.Forms.TabPage tabAnalytic;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtVerificationCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxLogin;
        private System.Windows.Forms.TabPage tabWait;
        private System.Windows.Forms.PictureBox pictureBoxWait;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.Label lblColorExplanation;

    }
}