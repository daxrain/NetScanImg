namespace Client
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_test_connection = new System.Windows.Forms.Button();
            this.testImmagine = new System.Windows.Forms.PictureBox();
            this.btn_scan = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_save_conf = new System.Windows.Forms.Button();
            this.server_port_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.server_ip_TextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.testImmagine)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_test_connection
            // 
            this.btn_test_connection.Location = new System.Drawing.Point(497, 35);
            this.btn_test_connection.Name = "btn_test_connection";
            this.btn_test_connection.Size = new System.Drawing.Size(123, 23);
            this.btn_test_connection.TabIndex = 0;
            this.btn_test_connection.Text = "Test Connessione";
            this.btn_test_connection.UseVisualStyleBackColor = true;
            this.btn_test_connection.Click += new System.EventHandler(this.btn_test_connection_Click);
            // 
            // testImmagine
            // 
            this.testImmagine.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.testImmagine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.testImmagine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.testImmagine.Location = new System.Drawing.Point(43, 23);
            this.testImmagine.Name = "testImmagine";
            this.testImmagine.Size = new System.Drawing.Size(443, 581);
            this.testImmagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.testImmagine.TabIndex = 1;
            this.testImmagine.TabStop = false;
            // 
            // btn_scan
            // 
            this.btn_scan.Location = new System.Drawing.Point(527, 581);
            this.btn_scan.Name = "btn_scan";
            this.btn_scan.Size = new System.Drawing.Size(123, 23);
            this.btn_scan.TabIndex = 2;
            this.btn_scan.Text = "SCANSIONA";
            this.btn_scan.UseVisualStyleBackColor = true;
            this.btn_scan.Click += new System.EventHandler(this.btn_scan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(701, 659);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.testImmagine);
            this.tabPage1.Controls.Add(this.btn_scan);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(693, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SCANSIONE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_save_conf);
            this.tabPage2.Controls.Add(this.server_port_TextBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.server_ip_TextBox);
            this.tabPage2.Controls.Add(this.btn_test_connection);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(693, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IMPOSTAZIONI";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_save_conf
            // 
            this.btn_save_conf.Location = new System.Drawing.Point(33, 95);
            this.btn_save_conf.Name = "btn_save_conf";
            this.btn_save_conf.Size = new System.Drawing.Size(75, 23);
            this.btn_save_conf.TabIndex = 5;
            this.btn_save_conf.Text = "SALVA CONFIGURAZIONE";
            this.btn_save_conf.UseVisualStyleBackColor = true;
            this.btn_save_conf.Click += new System.EventHandler(this.btn_save_conf_Click);
            // 
            // server_port_TextBox
            // 
            this.server_port_TextBox.Location = new System.Drawing.Point(213, 39);
            this.server_port_TextBox.Name = "server_port_TextBox";
            this.server_port_TextBox.Size = new System.Drawing.Size(78, 23);
            this.server_port_TextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Porta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Indirizzo IP del server";
            // 
            // server_ip_TextBox
            // 
            this.server_ip_TextBox.Location = new System.Drawing.Point(29, 39);
            this.server_ip_TextBox.Name = "server_ip_TextBox";
            this.server_ip_TextBox.Size = new System.Drawing.Size(155, 23);
            this.server_ip_TextBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(917, 655);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "NetScanImg";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.testImmagine)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_test_connection;
        private System.Windows.Forms.PictureBox testImmagine;
        private System.Windows.Forms.Button btn_scan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_save_conf;
        private System.Windows.Forms.TextBox server_port_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server_ip_TextBox;
    }
}
