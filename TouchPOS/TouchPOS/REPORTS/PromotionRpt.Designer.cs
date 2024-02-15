namespace TouchPOS.REPORTS
{
    partial class PromotionRpt
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.Chk_Taxtype = new System.Windows.Forms.CheckBox();
            this.chklist_Type = new System.Windows.Forms.CheckedListBox();
            this.Chk_SelectAllGroup = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LstGroup = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmb_PType = new System.Windows.Forms.ComboBox();
            this.Rdb_Details = new System.Windows.Forms.RadioButton();
            this.Rdb_Summary = new System.Windows.Forms.RadioButton();
            this.Dtp_ToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Dtp_FromDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(612, 38);
            this.label1.TabIndex = 44;
            this.label1.Text = "Promotional Sale Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(20, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 69);
            this.groupBox1.TabIndex = 46;
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
            this.btn_new.Location = new System.Drawing.Point(10, 6);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(185, 54);
            this.btn_new.TabIndex = 33;
            this.btn_new.Text = "Clear";
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_save.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(202, 6);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(185, 54);
            this.btn_save.TabIndex = 31;
            this.btn_save.Text = "View";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.BackColor = System.Drawing.Color.Red;
            this.Btn_exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(395, 6);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(185, 54);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(13, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(220, 24);
            this.label13.TabIndex = 661;
            this.label13.Text = "Items :";
            // 
            // Chk_Taxtype
            // 
            this.Chk_Taxtype.BackColor = System.Drawing.Color.Transparent;
            this.Chk_Taxtype.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_Taxtype.Location = new System.Drawing.Point(15, 11);
            this.Chk_Taxtype.Name = "Chk_Taxtype";
            this.Chk_Taxtype.Size = new System.Drawing.Size(193, 20);
            this.Chk_Taxtype.TabIndex = 660;
            this.Chk_Taxtype.Text = "Select All";
            this.Chk_Taxtype.UseVisualStyleBackColor = false;
            this.Chk_Taxtype.CheckedChanged += new System.EventHandler(this.Chk_Taxtype_CheckedChanged);
            // 
            // chklist_Type
            // 
            this.chklist_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chklist_Type.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_Type.HorizontalScrollbar = true;
            this.chklist_Type.Location = new System.Drawing.Point(13, 62);
            this.chklist_Type.Name = "chklist_Type";
            this.chklist_Type.Size = new System.Drawing.Size(220, 257);
            this.chklist_Type.TabIndex = 659;
            // 
            // Chk_SelectAllGroup
            // 
            this.Chk_SelectAllGroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllGroup.Location = new System.Drawing.Point(15, 10);
            this.Chk_SelectAllGroup.Name = "Chk_SelectAllGroup";
            this.Chk_SelectAllGroup.Size = new System.Drawing.Size(151, 16);
            this.Chk_SelectAllGroup.TabIndex = 658;
            this.Chk_SelectAllGroup.Text = "Select All";
            this.Chk_SelectAllGroup.UseVisualStyleBackColor = false;
            this.Chk_SelectAllGroup.CheckedChanged += new System.EventHandler(this.Chk_SelectAllGroup_CheckedChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(13, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 24);
            this.label7.TabIndex = 657;
            this.label7.Text = "Group Description :";
            // 
            // LstGroup
            // 
            this.LstGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstGroup.Location = new System.Drawing.Point(13, 60);
            this.LstGroup.Name = "LstGroup";
            this.LstGroup.Size = new System.Drawing.Size(220, 258);
            this.LstGroup.TabIndex = 656;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 16);
            this.label2.TabIndex = 652;
            this.label2.Text = "Type of Promotion";
            // 
            // Cmb_PType
            // 
            this.Cmb_PType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_PType.FormattingEnabled = true;
            this.Cmb_PType.Items.AddRange(new object[] {
            "Qty",
            "Fixed Rate",
            "Discount Rate"});
            this.Cmb_PType.Location = new System.Drawing.Point(150, 11);
            this.Cmb_PType.Name = "Cmb_PType";
            this.Cmb_PType.Size = new System.Drawing.Size(118, 24);
            this.Cmb_PType.TabIndex = 651;
            // 
            // Rdb_Details
            // 
            this.Rdb_Details.AutoSize = true;
            this.Rdb_Details.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_Details.Location = new System.Drawing.Point(424, 15);
            this.Rdb_Details.Name = "Rdb_Details";
            this.Rdb_Details.Size = new System.Drawing.Size(64, 19);
            this.Rdb_Details.TabIndex = 1;
            this.Rdb_Details.TabStop = true;
            this.Rdb_Details.Text = "Details";
            this.Rdb_Details.UseVisualStyleBackColor = true;
            // 
            // Rdb_Summary
            // 
            this.Rdb_Summary.AutoSize = true;
            this.Rdb_Summary.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_Summary.Location = new System.Drawing.Point(333, 14);
            this.Rdb_Summary.Name = "Rdb_Summary";
            this.Rdb_Summary.Size = new System.Drawing.Size(78, 19);
            this.Rdb_Summary.TabIndex = 0;
            this.Rdb_Summary.TabStop = true;
            this.Rdb_Summary.Text = "Summary";
            this.Rdb_Summary.UseVisualStyleBackColor = true;
            // 
            // Dtp_ToDate
            // 
            this.Dtp_ToDate.CustomFormat = "dd/MM/yyyy";
            this.Dtp_ToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_ToDate.Location = new System.Drawing.Point(374, 11);
            this.Dtp_ToDate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.Dtp_ToDate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.Dtp_ToDate.Name = "Dtp_ToDate";
            this.Dtp_ToDate.Size = new System.Drawing.Size(112, 23);
            this.Dtp_ToDate.TabIndex = 19;
            this.Dtp_ToDate.Value = new System.DateTime(2020, 2, 29, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "From Date";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(292, 15);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(60, 16);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "To Date";
            // 
            // Dtp_FromDate
            // 
            this.Dtp_FromDate.CustomFormat = "dd/MM/yyyy";
            this.Dtp_FromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_FromDate.Location = new System.Drawing.Point(116, 10);
            this.Dtp_FromDate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.Dtp_FromDate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.Dtp_FromDate.Name = "Dtp_FromDate";
            this.Dtp_FromDate.Size = new System.Drawing.Size(109, 23);
            this.Dtp_FromDate.TabIndex = 18;
            this.Dtp_FromDate.Value = new System.DateTime(2020, 2, 29, 0, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LstGroup);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Chk_SelectAllGroup);
            this.panel1.Location = new System.Drawing.Point(65, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 332);
            this.panel1.TabIndex = 655;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.chklist_Type);
            this.panel2.Controls.Add(this.Chk_Taxtype);
            this.panel2.Location = new System.Drawing.Point(323, 158);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 330);
            this.panel2.TabIndex = 656;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Dtp_ToDate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.Label5);
            this.panel3.Controls.Add(this.Dtp_FromDate);
            this.panel3.Location = new System.Drawing.Point(64, 552);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(505, 43);
            this.panel3.TabIndex = 657;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Rdb_Details);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.Rdb_Summary);
            this.panel4.Controls.Add(this.Cmb_PType);
            this.panel4.Location = new System.Drawing.Point(65, 498);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(504, 48);
            this.panel4.TabIndex = 658;
            // 
            // PromotionRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(635, 620);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PromotionRpt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PromotionRpt";
            this.Load += new System.EventHandler(this.PromotionRpt_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        internal System.Windows.Forms.CheckBox Chk_SelectAllGroup;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckedListBox LstGroup;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.CheckBox Chk_Taxtype;
        internal System.Windows.Forms.CheckedListBox chklist_Type;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ComboBox Cmb_PType;
        private System.Windows.Forms.RadioButton Rdb_Details;
        private System.Windows.Forms.RadioButton Rdb_Summary;
        internal System.Windows.Forms.DateTimePicker Dtp_ToDate;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.DateTimePicker Dtp_FromDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}