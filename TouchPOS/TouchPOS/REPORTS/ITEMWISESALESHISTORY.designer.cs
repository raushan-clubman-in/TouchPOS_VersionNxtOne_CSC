namespace TouchPOS.REPORTS
{
    partial class ITEMWISESALESHISTORY
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chklist_POSlocation = new System.Windows.Forms.CheckedListBox();
            this.lstsubgroup = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Chk_SelectAllsubgroup = new System.Windows.Forms.CheckBox();
            this.Chk_POSlocation = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Chk_Taxtype = new System.Windows.Forms.CheckBox();
            this.chklist_Type = new System.Windows.Forms.CheckedListBox();
            this.mskTodate = new System.Windows.Forms.DateTimePicker();
            this.Label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mskFromdate = new System.Windows.Forms.DateTimePicker();
            this.Cmb_Order = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(771, 36);
            this.label1.TabIndex = 43;
            this.label1.Text = "Item Wise Sales History";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(25, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 70);
            this.groupBox1.TabIndex = 45;
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
            this.btn_new.Location = new System.Drawing.Point(16, 7);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(191, 54);
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
            this.btn_save.Location = new System.Drawing.Point(286, 7);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(191, 54);
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
            this.Btn_exit.Location = new System.Drawing.Point(545, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(191, 54);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(703, 592);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 427;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // chklist_POSlocation
            // 
            this.chklist_POSlocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chklist_POSlocation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_POSlocation.Location = new System.Drawing.Point(12, 72);
            this.chklist_POSlocation.Name = "chklist_POSlocation";
            this.chklist_POSlocation.Size = new System.Drawing.Size(220, 302);
            this.chklist_POSlocation.TabIndex = 638;
            // 
            // lstsubgroup
            // 
            this.lstsubgroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstsubgroup.Location = new System.Drawing.Point(10, 68);
            this.lstsubgroup.Name = "lstsubgroup";
            this.lstsubgroup.Size = new System.Drawing.Size(220, 306);
            this.lstsubgroup.TabIndex = 651;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(10, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 29);
            this.label8.TabIndex = 652;
            this.label8.Text = "Subgroup Description :";
            // 
            // Chk_SelectAllsubgroup
            // 
            this.Chk_SelectAllsubgroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllsubgroup.Location = new System.Drawing.Point(10, 8);
            this.Chk_SelectAllsubgroup.Name = "Chk_SelectAllsubgroup";
            this.Chk_SelectAllsubgroup.Size = new System.Drawing.Size(169, 24);
            this.Chk_SelectAllsubgroup.TabIndex = 650;
            this.Chk_SelectAllsubgroup.Text = "SELECT ALL ";
            this.Chk_SelectAllsubgroup.UseVisualStyleBackColor = false;
            this.Chk_SelectAllsubgroup.CheckedChanged += new System.EventHandler(this.Chk_SelectAllsubgroup_CheckedChanged);
            // 
            // Chk_POSlocation
            // 
            this.Chk_POSlocation.BackColor = System.Drawing.Color.Transparent;
            this.Chk_POSlocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_POSlocation.Location = new System.Drawing.Point(12, 11);
            this.Chk_POSlocation.Name = "Chk_POSlocation";
            this.Chk_POSlocation.Size = new System.Drawing.Size(178, 24);
            this.Chk_POSlocation.TabIndex = 640;
            this.Chk_POSlocation.Text = "SELECT ALL";
            this.Chk_POSlocation.UseVisualStyleBackColor = false;
            this.Chk_POSlocation.CheckedChanged += new System.EventHandler(this.Chk_POSlocation_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(12, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(220, 29);
            this.label11.TabIndex = 639;
            this.label11.Text = "POS Description :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(12, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(220, 29);
            this.label13.TabIndex = 637;
            this.label13.Text = "Items :";
            // 
            // Chk_Taxtype
            // 
            this.Chk_Taxtype.BackColor = System.Drawing.Color.Transparent;
            this.Chk_Taxtype.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_Taxtype.Location = new System.Drawing.Point(12, 9);
            this.Chk_Taxtype.Name = "Chk_Taxtype";
            this.Chk_Taxtype.Size = new System.Drawing.Size(193, 24);
            this.Chk_Taxtype.TabIndex = 636;
            this.Chk_Taxtype.Text = "SELECT ALL";
            this.Chk_Taxtype.UseVisualStyleBackColor = false;
            this.Chk_Taxtype.CheckedChanged += new System.EventHandler(this.Chk_Taxtype_CheckedChanged);
            // 
            // chklist_Type
            // 
            this.chklist_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chklist_Type.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_Type.HorizontalScrollbar = true;
            this.chklist_Type.Location = new System.Drawing.Point(12, 70);
            this.chklist_Type.Name = "chklist_Type";
            this.chklist_Type.Size = new System.Drawing.Size(220, 302);
            this.chklist_Type.TabIndex = 635;
            // 
            // mskTodate
            // 
            this.mskTodate.CustomFormat = "dd/MM/yyyy";
            this.mskTodate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mskTodate.Location = new System.Drawing.Point(120, 9);
            this.mskTodate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.mskTodate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.mskTodate.Name = "mskTodate";
            this.mskTodate.Size = new System.Drawing.Size(112, 21);
            this.mskTodate.TabIndex = 19;
            this.mskTodate.Value = new System.DateTime(2019, 11, 13, 0, 0, 0, 0);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(13, 12);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(100, 15);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "As On  DATE       :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "FROM DATE :";
            this.label3.Visible = false;
            // 
            // mskFromdate
            // 
            this.mskFromdate.CustomFormat = "dd/MM/yyyy";
            this.mskFromdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mskFromdate.Location = new System.Drawing.Point(109, 561);
            this.mskFromdate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.mskFromdate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.mskFromdate.Name = "mskFromdate";
            this.mskFromdate.Size = new System.Drawing.Size(109, 21);
            this.mskFromdate.TabIndex = 18;
            this.mskFromdate.Value = new System.DateTime(2019, 11, 12, 0, 0, 0, 0);
            this.mskFromdate.Visible = false;
            // 
            // Cmb_Order
            // 
            this.Cmb_Order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Order.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Order.FormattingEnabled = true;
            this.Cmb_Order.Items.AddRange(new object[] {
            "ITEMCODE",
            "ITEMDESC"});
            this.Cmb_Order.Location = new System.Drawing.Point(638, 562);
            this.Cmb_Order.Name = "Cmb_Order";
            this.Cmb_Order.Size = new System.Drawing.Size(145, 24);
            this.Cmb_Order.TabIndex = 636;
            this.Cmb_Order.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(565, 566);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 650;
            this.label2.Text = "Order By";
            this.label2.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chklist_POSlocation);
            this.panel2.Controls.Add(this.Chk_POSlocation);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(27, 160);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 388);
            this.panel2.TabIndex = 651;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lstsubgroup);
            this.panel3.Controls.Add(this.Chk_SelectAllsubgroup);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(283, 162);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 385);
            this.panel3.TabIndex = 652;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Chk_Taxtype);
            this.panel4.Controls.Add(this.chklist_Type);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Location = new System.Drawing.Point(539, 160);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(245, 386);
            this.panel4.TabIndex = 653;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mskTodate);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Location = new System.Drawing.Point(282, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 38);
            this.panel1.TabIndex = 654;
            // 
            // ITEMWISESALESHISTORY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(814, 619);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cmb_Order);
            this.Controls.Add(this.mskFromdate);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ITEMWISESALESHISTORY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSWISE";
            this.Load += new System.EventHandler(this.ITEMWISESALESREPORT_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.CheckedListBox chklist_POSlocation;
        internal System.Windows.Forms.CheckedListBox lstsubgroup;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox Chk_SelectAllsubgroup;
        internal System.Windows.Forms.CheckBox Chk_POSlocation;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.CheckBox Chk_Taxtype;
        internal System.Windows.Forms.CheckedListBox chklist_Type;
        internal System.Windows.Forms.DateTimePicker mskTodate;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.DateTimePicker mskFromdate;
        internal System.Windows.Forms.ComboBox Cmb_Order;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
    }
}