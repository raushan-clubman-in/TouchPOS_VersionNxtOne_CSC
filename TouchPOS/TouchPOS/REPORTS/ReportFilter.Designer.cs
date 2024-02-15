namespace TouchPOS.REPORTS
{
    partial class ReportFilter
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
            this.ReportName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_view = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Cmd_Export = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportName
            // 
            this.ReportName.BackColor = System.Drawing.Color.Transparent;
            this.ReportName.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportName.ForeColor = System.Drawing.Color.White;
            this.ReportName.Location = new System.Drawing.Point(12, 11);
            this.ReportName.Name = "ReportName";
            this.ReportName.Size = new System.Drawing.Size(614, 37);
            this.ReportName.TabIndex = 44;
            this.ReportName.Text = "Report";
            this.ReportName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(256, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 24);
            this.label6.TabIndex = 434;
            this.label6.Text = "To Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 24);
            this.label5.TabIndex = 433;
            this.label5.Text = "From Date";
            // 
            // dtp1
            // 
            this.dtp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp1.Location = new System.Drawing.Point(120, 12);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(129, 29);
            this.dtp1.TabIndex = 431;
            // 
            // dtp2
            // 
            this.dtp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp2.Location = new System.Drawing.Point(343, 13);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(127, 29);
            this.dtp2.TabIndex = 432;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_view);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(18, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 67);
            this.groupBox1.TabIndex = 458;
            this.groupBox1.TabStop = false;
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_new.BackgroundImage = global::TouchPOS.Properties.Resources.BluebuttonMaster;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.ForeColor = System.Drawing.Color.White;
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(22, 5);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(164, 54);
            this.btn_new.TabIndex = 33;
            this.btn_new.Text = "Clear";
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_view
            // 
            this.btn_view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_view.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.btn_view.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_view.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_view.ForeColor = System.Drawing.Color.White;
            this.btn_view.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_view.Location = new System.Drawing.Point(215, 5);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(164, 54);
            this.btn_view.TabIndex = 31;
            this.btn_view.Text = "View";
            this.btn_view.UseVisualStyleBackColor = false;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.BackColor = System.Drawing.Color.Red;
            this.Btn_exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(413, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(164, 54);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(20, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(595, 310);
            this.groupBox3.TabIndex = 460;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(589, 291);
            this.dataGridView1.TabIndex = 460;
            // 
            // Cmd_Export
            // 
            this.Cmd_Export.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_Export.BackgroundImage = global::TouchPOS.Properties.Resources.GreyBar;
            this.Cmd_Export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Export.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Export.Location = new System.Drawing.Point(3, 4);
            this.Cmd_Export.Name = "Cmd_Export";
            this.Cmd_Export.Size = new System.Drawing.Size(100, 47);
            this.Cmd_Export.TabIndex = 461;
            this.Cmd_Export.Text = "Export";
            this.Cmd_Export.UseVisualStyleBackColor = false;
            this.Cmd_Export.Click += new System.EventHandler(this.Cmd_Export_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtp2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtp1);
            this.panel1.Location = new System.Drawing.Point(23, 459);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 54);
            this.panel1.TabIndex = 462;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Cmd_Export);
            this.panel2.Location = new System.Drawing.Point(509, 459);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 54);
            this.panel2.TabIndex = 463;
            // 
            // ReportFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 532);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReportName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportFilter";
            this.Load += new System.EventHandler(this.ReportFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ReportName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Cmd_Export;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}