namespace TextVerteilerClient
{
    partial class FormEinstellungen
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbServerIps = new System.Windows.Forms.ComboBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numOpcaity = new System.Windows.Forms.NumericUpDown();
            this.btnResetPosition = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numAutomatischVerstecken = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numHistoryStack = new System.Windows.Forms.NumericUpDown();
            this.numReconnectionTries = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnStopReconnecting = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOpcaity)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutomatischVerstecken)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHistoryStack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReconnectionTries)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbServerIps);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dns Einstellungen";
            // 
            // cbServerIps
            // 
            this.cbServerIps.FormattingEnabled = true;
            this.cbServerIps.Location = new System.Drawing.Point(74, 19);
            this.cbServerIps.Name = "cbServerIps";
            this.cbServerIps.Size = new System.Drawing.Size(101, 21);
            this.cbServerIps.TabIndex = 1;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(88, 46);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(77, 33);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(14, 46);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(68, 33);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Dns Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numOpcaity);
            this.groupBox2.Location = new System.Drawing.Point(202, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 49);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Durchsichtigkeit";
            // 
            // numOpcaity
            // 
            this.numOpcaity.DecimalPlaces = 2;
            this.numOpcaity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numOpcaity.Location = new System.Drawing.Point(14, 19);
            this.numOpcaity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOpcaity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numOpcaity.Name = "numOpcaity";
            this.numOpcaity.Size = new System.Drawing.Size(62, 20);
            this.numOpcaity.TabIndex = 0;
            this.numOpcaity.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numOpcaity.ValueChanged += new System.EventHandler(this.numOpcaity_ValueChanged);
            // 
            // btnResetPosition
            // 
            this.btnResetPosition.Location = new System.Drawing.Point(202, 67);
            this.btnResetPosition.Name = "btnResetPosition";
            this.btnResetPosition.Size = new System.Drawing.Size(97, 33);
            this.btnResetPosition.TabIndex = 7;
            this.btnResetPosition.Text = "Reset Position";
            this.btnResetPosition.UseVisualStyleBackColor = true;
            this.btnResetPosition.Click += new System.EventHandler(this.btnResetPosition_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numAutomatischVerstecken);
            this.groupBox3.Location = new System.Drawing.Point(12, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 58);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "automatisch Verstecken nach ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sekunden (0 = nie, \r\nmit enter bestätigen)";
            // 
            // numAutomatischVerstecken
            // 
            this.numAutomatischVerstecken.Location = new System.Drawing.Point(13, 25);
            this.numAutomatischVerstecken.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numAutomatischVerstecken.Name = "numAutomatischVerstecken";
            this.numAutomatischVerstecken.Size = new System.Drawing.Size(57, 20);
            this.numAutomatischVerstecken.TabIndex = 7;
            this.numAutomatischVerstecken.ValueChanged += new System.EventHandler(this.numAutomatischVerstecken_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numHistoryStack);
            this.groupBox4.Location = new System.Drawing.Point(202, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(97, 50);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "History Stack";
            // 
            // numHistoryStack
            // 
            this.numHistoryStack.Location = new System.Drawing.Point(21, 19);
            this.numHistoryStack.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHistoryStack.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHistoryStack.Name = "numHistoryStack";
            this.numHistoryStack.Size = new System.Drawing.Size(62, 20);
            this.numHistoryStack.TabIndex = 0;
            this.numHistoryStack.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numHistoryStack.ValueChanged += new System.EventHandler(this.numHistoryStack_ValueChanged);
            // 
            // numReconnectionTries
            // 
            this.numReconnectionTries.Location = new System.Drawing.Point(211, 14);
            this.numReconnectionTries.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numReconnectionTries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReconnectionTries.Name = "numReconnectionTries";
            this.numReconnectionTries.Size = new System.Drawing.Size(62, 20);
            this.numReconnectionTries.TabIndex = 11;
            this.numReconnectionTries.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numReconnectionTries.ValueChanged += new System.EventHandler(this.numReconnectionTries_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Anzahl erneuter Verbindungsversuche";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnStopReconnecting);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.numReconnectionTries);
            this.groupBox5.Location = new System.Drawing.Point(12, 170);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(287, 67);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Wiederverbinden";
            // 
            // btnStopReconnecting
            // 
            this.btnStopReconnecting.Location = new System.Drawing.Point(14, 40);
            this.btnStopReconnecting.Name = "btnStopReconnecting";
            this.btnStopReconnecting.Size = new System.Drawing.Size(259, 23);
            this.btnStopReconnecting.TabIndex = 13;
            this.btnStopReconnecting.Text = "Stoppe Verbindungsversuche";
            this.btnStopReconnecting.UseVisualStyleBackColor = true;
            this.btnStopReconnecting.Click += new System.EventHandler(this.btnStopReconnecting_Click);
            // 
            // FormEinstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 249);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnResetPosition);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormEinstellungen";
            this.ShowInTaskbar = false;
            this.Text = "Einstellungen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEinstellungen_FormClosing);
            this.Load += new System.EventHandler(this.FormEinstellungen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOpcaity)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutomatischVerstecken)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numHistoryStack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReconnectionTries)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numOpcaity;
        private System.Windows.Forms.Button btnResetPosition;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAutomatischVerstecken;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numHistoryStack;
        internal System.Windows.Forms.ComboBox cbServerIps;
        private System.Windows.Forms.NumericUpDown numReconnectionTries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnStopReconnecting;
    }
}