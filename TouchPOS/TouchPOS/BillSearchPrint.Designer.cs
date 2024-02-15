namespace TouchPOS
{
    partial class BillSearchPrint
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
            this.ServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmd_PrintDuplicate = new System.Windows.Forms.Button();
            this.Cmd_BPOS = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(710, 34);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bill Print Duplicate ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Cmd_Search);
            this.groupBox2.Controls.Add(this.Dtp_FromDate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(145, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 63);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // Cmd_Search
            // 
            this.Cmd_Search.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_Search.BackgroundImage = global::TouchPOS.Properties.Resources.NumericPad;
            this.Cmd_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Search.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Search.Image = global::TouchPOS.Properties.Resources._1__6_;
            this.Cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_Search.Location = new System.Drawing.Point(276, 10);
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
            this.Dtp_FromDate.Location = new System.Drawing.Point(134, 17);
            this.Dtp_FromDate.Name = "Dtp_FromDate";
            this.Dtp_FromDate.Size = new System.Drawing.Size(136, 29);
            this.Dtp_FromDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(22, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(27, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(682, 297);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillNo,
            this.BillDate,
            this.TotalAmount,
            this.ServiceType,
            this.Location,
            this.TableNo});
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
            // ServiceType
            // 
            this.ServiceType.HeaderText = "ServiceType";
            this.ServiceType.Name = "ServiceType";
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            // 
            // TableNo
            // 
            this.TableNo.HeaderText = "TableNo";
            this.TableNo.Name = "TableNo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cmd_PrintDuplicate);
            this.groupBox1.Controls.Add(this.Cmd_BPOS);
            this.groupBox1.Location = new System.Drawing.Point(217, 454);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 65);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // Cmd_PrintDuplicate
            // 
            this.Cmd_PrintDuplicate.BackColor = System.Drawing.Color.Green;
            this.Cmd_PrintDuplicate.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.Cmd_PrintDuplicate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_PrintDuplicate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_PrintDuplicate.ForeColor = System.Drawing.Color.White;
            this.Cmd_PrintDuplicate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_PrintDuplicate.Location = new System.Drawing.Point(12, 7);
            this.Cmd_PrintDuplicate.Name = "Cmd_PrintDuplicate";
            this.Cmd_PrintDuplicate.Size = new System.Drawing.Size(139, 50);
            this.Cmd_PrintDuplicate.TabIndex = 7;
            this.Cmd_PrintDuplicate.Text = "Print";
            this.Cmd_PrintDuplicate.UseVisualStyleBackColor = false;
            this.Cmd_PrintDuplicate.Click += new System.EventHandler(this.Cmd_PrintDuplicate_Click);
            // 
            // Cmd_BPOS
            // 
            this.Cmd_BPOS.BackColor = System.Drawing.Color.Red;
            this.Cmd_BPOS.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_BPOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_BPOS.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_BPOS.ForeColor = System.Drawing.Color.White;
            this.Cmd_BPOS.Location = new System.Drawing.Point(157, 7);
            this.Cmd_BPOS.Name = "Cmd_BPOS";
            this.Cmd_BPOS.Size = new System.Drawing.Size(117, 50);
            this.Cmd_BPOS.TabIndex = 5;
            this.Cmd_BPOS.Text = "Exit";
            this.Cmd_BPOS.UseVisualStyleBackColor = false;
            this.Cmd_BPOS.Click += new System.EventHandler(this.Cmd_BPOS_Click);
            // 
            // BillSearchPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(736, 538);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BillSearchPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BillSearchPrint";
            this.Load += new System.EventHandler(this.BillSearchPrint_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cmd_PrintDuplicate;
        private System.Windows.Forms.Button Cmd_BPOS;
    }
}