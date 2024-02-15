namespace TouchPOS
{
    partial class EInvoiceingStatus
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Cmd_Search = new System.Windows.Forms.Button();
            this.Dtp_FromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinancialYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmd_DelEInvoice = new System.Windows.Forms.Button();
            this.Cmd_Process = new System.Windows.Forms.Button();
            this.Cmd_BPOS = new System.Windows.Forms.Button();
            this.Cmd_PrintBill = new System.Windows.Forms.Button();
            this.Pic_QR = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_QR)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(738, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "e-Invoicing Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Cmd_Search);
            this.groupBox2.Controls.Add(this.Dtp_FromDate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(153, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 66);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // Cmd_Search
            // 
            this.Cmd_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Search.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Search.Image = global::TouchPOS.Properties.Resources._1__6_;
            this.Cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_Search.Location = new System.Drawing.Point(276, 14);
            this.Cmd_Search.Name = "Cmd_Search";
            this.Cmd_Search.Size = new System.Drawing.Size(134, 43);
            this.Cmd_Search.TabIndex = 5;
            this.Cmd_Search.Text = "Search";
            this.Cmd_Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Search.UseVisualStyleBackColor = false;
            this.Cmd_Search.Click += new System.EventHandler(this.Cmd_Search_Click);
            // 
            // Dtp_FromDate
            // 
            this.Dtp_FromDate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dtp_FromDate.Location = new System.Drawing.Point(134, 24);
            this.Dtp_FromDate.Name = "Dtp_FromDate";
            this.Dtp_FromDate.Size = new System.Drawing.Size(136, 29);
            this.Dtp_FromDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(29, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(682, 297);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillNo,
            this.BillDate,
            this.TotalAmount,
            this.Status,
            this.Message,
            this.FinancialYear});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(676, 278);
            this.dataGridView1.TabIndex = 1;
            // 
            // BillNo
            // 
            this.BillNo.HeaderText = "BillNo";
            this.BillNo.Name = "BillNo";
            // 
            // BillDate
            // 
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.BillDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.BillDate.HeaderText = "BillDate";
            this.BillDate.Name = "BillDate";
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.TotalAmount.HeaderText = "TotalAmount";
            this.TotalAmount.Name = "TotalAmount";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // FinancialYear
            // 
            this.FinancialYear.HeaderText = "FinancialYear";
            this.FinancialYear.Name = "FinancialYear";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cmd_PrintBill);
            this.groupBox1.Controls.Add(this.Cmd_DelEInvoice);
            this.groupBox1.Controls.Add(this.Cmd_Process);
            this.groupBox1.Controls.Add(this.Cmd_BPOS);
            this.groupBox1.Location = new System.Drawing.Point(32, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 72);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // Cmd_DelEInvoice
            // 
            this.Cmd_DelEInvoice.BackColor = System.Drawing.Color.Red;
            this.Cmd_DelEInvoice.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_DelEInvoice.ForeColor = System.Drawing.Color.White;
            this.Cmd_DelEInvoice.Location = new System.Drawing.Point(421, 13);
            this.Cmd_DelEInvoice.Name = "Cmd_DelEInvoice";
            this.Cmd_DelEInvoice.Size = new System.Drawing.Size(117, 50);
            this.Cmd_DelEInvoice.TabIndex = 8;
            this.Cmd_DelEInvoice.Text = "Delete";
            this.Cmd_DelEInvoice.UseVisualStyleBackColor = false;
            this.Cmd_DelEInvoice.Click += new System.EventHandler(this.Cmd_DelEInvoice_Click);
            // 
            // Cmd_Process
            // 
            this.Cmd_Process.BackColor = System.Drawing.Color.Green;
            this.Cmd_Process.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Process.ForeColor = System.Drawing.Color.White;
            this.Cmd_Process.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_Process.Location = new System.Drawing.Point(12, 14);
            this.Cmd_Process.Name = "Cmd_Process";
            this.Cmd_Process.Size = new System.Drawing.Size(139, 50);
            this.Cmd_Process.TabIndex = 7;
            this.Cmd_Process.Text = "Process";
            this.Cmd_Process.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Process.UseVisualStyleBackColor = false;
            this.Cmd_Process.Click += new System.EventHandler(this.Cmd_Process_Click);
            // 
            // Cmd_BPOS
            // 
            this.Cmd_BPOS.BackColor = System.Drawing.Color.Red;
            this.Cmd_BPOS.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_BPOS.ForeColor = System.Drawing.Color.White;
            this.Cmd_BPOS.Location = new System.Drawing.Point(544, 14);
            this.Cmd_BPOS.Name = "Cmd_BPOS";
            this.Cmd_BPOS.Size = new System.Drawing.Size(117, 50);
            this.Cmd_BPOS.TabIndex = 5;
            this.Cmd_BPOS.Text = "Exit";
            this.Cmd_BPOS.UseVisualStyleBackColor = false;
            this.Cmd_BPOS.Click += new System.EventHandler(this.Cmd_BPOS_Click);
            // 
            // Cmd_PrintBill
            // 
            this.Cmd_PrintBill.BackColor = System.Drawing.Color.Green;
            this.Cmd_PrintBill.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_PrintBill.ForeColor = System.Drawing.Color.White;
            this.Cmd_PrintBill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_PrintBill.Location = new System.Drawing.Point(176, 14);
            this.Cmd_PrintBill.Name = "Cmd_PrintBill";
            this.Cmd_PrintBill.Size = new System.Drawing.Size(139, 50);
            this.Cmd_PrintBill.TabIndex = 9;
            this.Cmd_PrintBill.Text = "Print Bill";
            this.Cmd_PrintBill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_PrintBill.UseVisualStyleBackColor = false;
            this.Cmd_PrintBill.Click += new System.EventHandler(this.Cmd_PrintBill_Click);
            // 
            // Pic_QR
            // 
            this.Pic_QR.Location = new System.Drawing.Point(621, 56);
            this.Pic_QR.Name = "Pic_QR";
            this.Pic_QR.Size = new System.Drawing.Size(102, 49);
            this.Pic_QR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_QR.TabIndex = 13;
            this.Pic_QR.TabStop = false;
            this.Pic_QR.Visible = false;
            // 
            // EInvoiceingStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(740, 502);
            this.Controls.Add(this.Pic_QR);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EInvoiceingStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EInvoiceingStatus";
            this.Load += new System.EventHandler(this.EInvoiceingStatus_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_QR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Cmd_Search;
        private System.Windows.Forms.DateTimePicker Dtp_FromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cmd_Process;
        private System.Windows.Forms.Button Cmd_BPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinancialYear;
        private System.Windows.Forms.Button Cmd_DelEInvoice;
        private System.Windows.Forms.Button Cmd_PrintBill;
        private System.Windows.Forms.PictureBox Pic_QR;
    }
}