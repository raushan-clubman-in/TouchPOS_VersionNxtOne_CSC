namespace TouchPOS.REPORTS
{
    partial class DayEndSummary
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmd_ViewRpt = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_view = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.Cmd_Export = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.Cmd_Mail = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(641, 45);
            this.label1.TabIndex = 102;
            this.label1.Text = "Day End Summary";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Cmd_ViewRpt);
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_view);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(30, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 67);
            this.groupBox1.TabIndex = 459;
            this.groupBox1.TabStop = false;
            // 
            // Cmd_ViewRpt
            // 
            this.Cmd_ViewRpt.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_ViewRpt.BackgroundImage = global::TouchPOS.Properties.Resources.GreyBar;
            this.Cmd_ViewRpt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_ViewRpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_ViewRpt.ForeColor = System.Drawing.Color.Black;
            this.Cmd_ViewRpt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_ViewRpt.Location = new System.Drawing.Point(310, 5);
            this.Cmd_ViewRpt.Name = "Cmd_ViewRpt";
            this.Cmd_ViewRpt.Size = new System.Drawing.Size(132, 54);
            this.Cmd_ViewRpt.TabIndex = 463;
            this.Cmd_ViewRpt.Text = "Print";
            this.Cmd_ViewRpt.UseVisualStyleBackColor = false;
            this.Cmd_ViewRpt.Click += new System.EventHandler(this.Cmd_ViewRpt_Click);
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_new.BackgroundImage = global::TouchPOS.Properties.Resources.BluebuttonMaster;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.ForeColor = System.Drawing.Color.White;
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(6, 5);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(132, 54);
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
            this.btn_view.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_view.ForeColor = System.Drawing.Color.White;
            this.btn_view.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_view.Location = new System.Drawing.Point(161, 5);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(132, 54);
            this.btn_view.TabIndex = 31;
            this.btn_view.Text = "Get Details";
            this.btn_view.UseVisualStyleBackColor = false;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.BackColor = System.Drawing.Color.Red;
            this.Btn_exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(453, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(132, 54);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(32, 205);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(595, 290);
            this.groupBox3.TabIndex = 461;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Desc,
            this.Amout});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(589, 271);
            this.dataGridView1.TabIndex = 460;
            // 
            // Desc
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Desc.DefaultCellStyle = dataGridViewCellStyle3;
            this.Desc.HeaderText = "Desc";
            this.Desc.Name = "Desc";
            // 
            // Amout
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amout.DefaultCellStyle = dataGridViewCellStyle4;
            this.Amout.HeaderText = "Amout";
            this.Amout.Name = "Amout";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtp2);
            this.panel1.Controls.Add(this.Cmd_Export);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtp1);
            this.panel1.Location = new System.Drawing.Point(30, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 54);
            this.panel1.TabIndex = 463;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(256, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 464;
            this.label2.Text = "To Date";
            // 
            // dtp2
            // 
            this.dtp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp2.Location = new System.Drawing.Point(334, 11);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(129, 26);
            this.dtp2.TabIndex = 463;
            // 
            // Cmd_Export
            // 
            this.Cmd_Export.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_Export.BackgroundImage = global::TouchPOS.Properties.Resources.GreyBar;
            this.Cmd_Export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Export.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Export.Location = new System.Drawing.Point(488, 2);
            this.Cmd_Export.Name = "Cmd_Export";
            this.Cmd_Export.Size = new System.Drawing.Size(100, 47);
            this.Cmd_Export.TabIndex = 462;
            this.Cmd_Export.Text = "Export";
            this.Cmd_Export.UseVisualStyleBackColor = false;
            this.Cmd_Export.Click += new System.EventHandler(this.Cmd_Export_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 433;
            this.label5.Text = "From Date";
            // 
            // dtp1
            // 
            this.dtp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp1.Location = new System.Drawing.Point(108, 11);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(129, 26);
            this.dtp1.TabIndex = 431;
            // 
            // Cmd_Mail
            // 
            this.Cmd_Mail.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_Mail.BackgroundImage = global::TouchPOS.Properties.Resources.GreyBar;
            this.Cmd_Mail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Mail.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Mail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Mail.Location = new System.Drawing.Point(483, 501);
            this.Cmd_Mail.Name = "Cmd_Mail";
            this.Cmd_Mail.Size = new System.Drawing.Size(136, 47);
            this.Cmd_Mail.TabIndex = 464;
            this.Cmd_Mail.Text = "Send Email";
            this.Cmd_Mail.UseVisualStyleBackColor = false;
            this.Cmd_Mail.Click += new System.EventHandler(this.Cmd_Mail_Click);
            // 
            // DayEndSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(665, 566);
            this.Controls.Add(this.Cmd_Mail);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DayEndSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DayEndSummary";
            this.Load += new System.EventHandler(this.DayEndSummary_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.Button Cmd_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amout;
        private System.Windows.Forms.Button Cmd_ViewRpt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.Button Cmd_Mail;
    }
}