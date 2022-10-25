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
            this.scanned_images_PictureBox = new System.Windows.Forms.PictureBox();
            this.btn_scan = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.color_comboBox = new System.Windows.Forms.ComboBox();
            this.dpi_comboBox = new System.Windows.Forms.ComboBox();
            this.scanner_comboBox = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_save_conf = new System.Windows.Forms.Button();
            this.server_port_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.server_ip_TextBox = new System.Windows.Forms.TextBox();
            this.nextImage_button = new System.Windows.Forms.Button();
            this.previousImage_button = new System.Windows.Forms.Button();
            this.deleteImage_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scanned_images_PictureBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_test_connection
            // 
            this.btn_test_connection.Location = new System.Drawing.Point(338, 39);
            this.btn_test_connection.Name = "btn_test_connection";
            this.btn_test_connection.Size = new System.Drawing.Size(123, 23);
            this.btn_test_connection.TabIndex = 0;
            this.btn_test_connection.Text = "Test Connessione";
            this.btn_test_connection.UseVisualStyleBackColor = true;
            this.btn_test_connection.Click += new System.EventHandler(this.btn_test_connection_Click);
            // 
            // scanned_images_PictureBox
            // 
            this.scanned_images_PictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scanned_images_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.scanned_images_PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scanned_images_PictureBox.Location = new System.Drawing.Point(43, 58);
            this.scanned_images_PictureBox.Name = "scanned_images_PictureBox";
            this.scanned_images_PictureBox.Size = new System.Drawing.Size(393, 526);
            this.scanned_images_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.scanned_images_PictureBox.TabIndex = 1;
            this.scanned_images_PictureBox.TabStop = false;
            // 
            // btn_scan
            // 
            this.btn_scan.Location = new System.Drawing.Point(504, 18);
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
            this.tabPage1.Controls.Add(this.deleteImage_button);
            this.tabPage1.Controls.Add(this.previousImage_button);
            this.tabPage1.Controls.Add(this.nextImage_button);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.scanner_comboBox);
            this.tabPage1.Controls.Add(this.scanned_images_PictureBox);
            this.tabPage1.Controls.Add(this.btn_scan);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(693, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SCANSIONE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.color_comboBox);
            this.groupBox1.Controls.Add(this.dpi_comboBox);
            this.groupBox1.Location = new System.Drawing.Point(466, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 329);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opzioni";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Contrasto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Luminosità";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(26, 253);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(146, 45);
            this.trackBar2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Modalità colore";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Risoluzione DPI";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(26, 170);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(146, 45);
            this.trackBar1.TabIndex = 4;
            // 
            // color_comboBox
            // 
            this.color_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.color_comboBox.FormattingEnabled = true;
            this.color_comboBox.Items.AddRange(new object[] {
            "Bianco e Nero",
            "Colori",
            "Scala di Grigi"});
            this.color_comboBox.Location = new System.Drawing.Point(26, 113);
            this.color_comboBox.Name = "color_comboBox";
            this.color_comboBox.Size = new System.Drawing.Size(146, 23);
            this.color_comboBox.TabIndex = 7;
            // 
            // dpi_comboBox
            // 
            this.dpi_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpi_comboBox.FormattingEnabled = true;
            this.dpi_comboBox.Items.AddRange(new object[] {
            "150",
            "300",
            "600"});
            this.dpi_comboBox.Location = new System.Drawing.Point(26, 52);
            this.dpi_comboBox.Name = "dpi_comboBox";
            this.dpi_comboBox.Size = new System.Drawing.Size(146, 23);
            this.dpi_comboBox.TabIndex = 6;
            // 
            // scanner_comboBox
            // 
            this.scanner_comboBox.FormattingEnabled = true;
            this.scanner_comboBox.Location = new System.Drawing.Point(43, 18);
            this.scanner_comboBox.Name = "scanner_comboBox";
            this.scanner_comboBox.Size = new System.Drawing.Size(219, 23);
            this.scanner_comboBox.TabIndex = 3;
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
            // nextImage_button
            // 
            this.nextImage_button.Location = new System.Drawing.Point(361, 598);
            this.nextImage_button.Name = "nextImage_button";
            this.nextImage_button.Size = new System.Drawing.Size(75, 23);
            this.nextImage_button.TabIndex = 9;
            this.nextImage_button.Text = "AVANTI";
            this.nextImage_button.UseVisualStyleBackColor = true;
            // 
            // previousImage_button
            // 
            this.previousImage_button.Location = new System.Drawing.Point(43, 598);
            this.previousImage_button.Name = "previousImage_button";
            this.previousImage_button.Size = new System.Drawing.Size(75, 23);
            this.previousImage_button.TabIndex = 10;
            this.previousImage_button.Text = "INDIETRO";
            this.previousImage_button.UseVisualStyleBackColor = true;
            // 
            // deleteImage_button
            // 
            this.deleteImage_button.Location = new System.Drawing.Point(199, 598);
            this.deleteImage_button.Name = "deleteImage_button";
            this.deleteImage_button.Size = new System.Drawing.Size(75, 23);
            this.deleteImage_button.TabIndex = 11;
            this.deleteImage_button.Text = "CANCELLA";
            this.deleteImage_button.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(696, 663);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "NetScanImg";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.scanned_images_PictureBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_test_connection;
        private System.Windows.Forms.PictureBox scanned_images_PictureBox;
        private System.Windows.Forms.Button btn_scan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_save_conf;
        private System.Windows.Forms.TextBox server_port_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server_ip_TextBox;
        private System.Windows.Forms.ComboBox scanner_comboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox color_comboBox;
        private System.Windows.Forms.ComboBox dpi_comboBox;
        private System.Windows.Forms.Button deleteImage_button;
        private System.Windows.Forms.Button previousImage_button;
        private System.Windows.Forms.Button nextImage_button;
    }
}
