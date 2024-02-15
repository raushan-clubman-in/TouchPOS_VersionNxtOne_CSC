namespace TouchPOS.REPORTS
{
    partial class SETTLEMENT
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
            this.Label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chk_userlist = new System.Windows.Forms.CheckBox();
            this.POS_LISTBOX = new System.Windows.Forms.CheckedListBox();
            this.USER_LISTBOX = new System.Windows.Forms.CheckedListBox();
            this.PAYMENT_LISTBOX = new System.Windows.Forms.CheckedListBox();
            this.dtp_fromdate = new System.Windows.Forms.DateTimePicker();
            this.dtp_todate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkbox_withdaybrkup = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Chk_SETTLEMENT = new System.Windows.Forms.CheckBox();
            this.PAYMENTWISE = new System.Windows.Forms.CheckBox();
            this.USERWISE = new System.Windows.Forms.CheckBox();
            this.chk_PAYMENT = new System.Windows.Forms.CheckBox();
            this.chk_poslist = new System.Windows.Forms.CheckBox();
            this.Chk_WithoutNC = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(784, 35);
            this.label1.TabIndex = 43;
            this.label1.Text = "Settlement Register";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(24, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(761, 69);
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
            this.btn_new.Location = new System.Drawing.Point(14, 7);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(191, 53);
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
            this.btn_save.Location = new System.Drawing.Point(299, 7);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(191, 53);
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
            this.Btn_exit.Location = new System.Drawing.Point(557, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(191, 53);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(12, 38);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(220, 24);
            this.Label2.TabIndex = 422;
            this.Label2.Text = "POS Location :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 25);
            this.label3.TabIndex = 424;
            this.label3.Text = "User Name List :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 24);
            this.label4.TabIndex = 426;
            this.label4.Text = "Payment Mode List:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // chk_userlist
            // 
            this.chk_userlist.AutoSize = true;
            this.chk_userlist.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_userlist.Location = new System.Drawing.Point(17, 9);
            this.chk_userlist.Name = "chk_userlist";
            this.chk_userlist.Size = new System.Drawing.Size(117, 22);
            this.chk_userlist.TabIndex = 427;
            this.chk_userlist.Text = "SELECT ALL";
            this.chk_userlist.UseVisualStyleBackColor = true;
            this.chk_userlist.CheckedChanged += new System.EventHandler(this.chk_userlist_CheckedChanged);
            // 
            // POS_LISTBOX
            // 
            this.POS_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.POS_LISTBOX.FormattingEnabled = true;
            this.POS_LISTBOX.Location = new System.Drawing.Point(12, 67);
            this.POS_LISTBOX.Name = "POS_LISTBOX";
            this.POS_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.POS_LISTBOX.TabIndex = 428;
            // 
            // USER_LISTBOX
            // 
            this.USER_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.USER_LISTBOX.FormattingEnabled = true;
            this.USER_LISTBOX.Location = new System.Drawing.Point(8, 69);
            this.USER_LISTBOX.Name = "USER_LISTBOX";
            this.USER_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.USER_LISTBOX.TabIndex = 429;
            // 
            // PAYMENT_LISTBOX
            // 
            this.PAYMENT_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PAYMENT_LISTBOX.FormattingEnabled = true;
            this.PAYMENT_LISTBOX.Location = new System.Drawing.Point(7, 66);
            this.PAYMENT_LISTBOX.Name = "PAYMENT_LISTBOX";
            this.PAYMENT_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.PAYMENT_LISTBOX.TabIndex = 430;
            // 
            // dtp_fromdate
            // 
            this.dtp_fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fromdate.Location = new System.Drawing.Point(127, 6);
            this.dtp_fromdate.Name = "dtp_fromdate";
            this.dtp_fromdate.Size = new System.Drawing.Size(128, 29);
            this.dtp_fromdate.TabIndex = 431;
            // 
            // dtp_todate
            // 
            this.dtp_todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_todate.Location = new System.Drawing.Point(361, 5);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(119, 29);
            this.dtp_todate.TabIndex = 432;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(266, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 24);
            this.label6.TabIndex = 434;
            this.label6.Text = "To Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 24);
            this.label5.TabIndex = 433;
            this.label5.Text = "From Date";
            // 
            // chkbox_withdaybrkup
            // 
            this.chkbox_withdaybrkup.AutoSize = true;
            this.chkbox_withdaybrkup.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_withdaybrkup.Location = new System.Drawing.Point(10, 10);
            this.chkbox_withdaybrkup.Name = "chkbox_withdaybrkup";
            this.chkbox_withdaybrkup.Size = new System.Drawing.Size(152, 22);
            this.chkbox_withdaybrkup.TabIndex = 0;
            this.chkbox_withdaybrkup.Text = "With Day Breakup";
            this.chkbox_withdaybrkup.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.Chk_SETTLEMENT);
            this.groupBox3.Location = new System.Drawing.Point(732, 574);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(51, 51);
            this.groupBox3.TabIndex = 453;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // Chk_SETTLEMENT
            // 
            this.Chk_SETTLEMENT.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SETTLEMENT.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SETTLEMENT.Location = new System.Drawing.Point(9, 17);
            this.Chk_SETTLEMENT.Name = "Chk_SETTLEMENT";
            this.Chk_SETTLEMENT.Size = new System.Drawing.Size(16, 22);
            this.Chk_SETTLEMENT.TabIndex = 429;
            this.Chk_SETTLEMENT.Text = "SETTLEMENT";
            this.Chk_SETTLEMENT.UseVisualStyleBackColor = false;
            this.Chk_SETTLEMENT.Visible = false;
            this.Chk_SETTLEMENT.CheckedChanged += new System.EventHandler(this.Chk_SUMM_CheckedChanged);
            // 
            // PAYMENTWISE
            // 
            this.PAYMENTWISE.BackColor = System.Drawing.Color.Transparent;
            this.PAYMENTWISE.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PAYMENTWISE.Location = new System.Drawing.Point(263, 7);
            this.PAYMENTWISE.Name = "PAYMENTWISE";
            this.PAYMENTWISE.Size = new System.Drawing.Size(196, 27);
            this.PAYMENTWISE.TabIndex = 432;
            this.PAYMENTWISE.Text = "Payment Mode";
            this.PAYMENTWISE.UseVisualStyleBackColor = false;
            this.PAYMENTWISE.CheckedChanged += new System.EventHandler(this.BEARERWISE_CheckedChanged);
            // 
            // USERWISE
            // 
            this.USERWISE.BackColor = System.Drawing.Color.Transparent;
            this.USERWISE.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.USERWISE.Location = new System.Drawing.Point(26, 3);
            this.USERWISE.Name = "USERWISE";
            this.USERWISE.Size = new System.Drawing.Size(206, 35);
            this.USERWISE.TabIndex = 431;
            this.USERWISE.Text = "User Wise";
            this.USERWISE.UseVisualStyleBackColor = false;
            this.USERWISE.CheckedChanged += new System.EventHandler(this.USERWISE_CheckedChanged);
            // 
            // chk_PAYMENT
            // 
            this.chk_PAYMENT.AutoSize = true;
            this.chk_PAYMENT.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_PAYMENT.Location = new System.Drawing.Point(6, 8);
            this.chk_PAYMENT.Name = "chk_PAYMENT";
            this.chk_PAYMENT.Size = new System.Drawing.Size(117, 22);
            this.chk_PAYMENT.TabIndex = 432;
            this.chk_PAYMENT.Text = "SELECT ALL";
            this.chk_PAYMENT.UseVisualStyleBackColor = true;
            this.chk_PAYMENT.CheckedChanged += new System.EventHandler(this.chk_bearer_CheckedChanged);
            // 
            // chk_poslist
            // 
            this.chk_poslist.AutoSize = true;
            this.chk_poslist.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_poslist.Location = new System.Drawing.Point(14, 8);
            this.chk_poslist.Name = "chk_poslist";
            this.chk_poslist.Size = new System.Drawing.Size(117, 22);
            this.chk_poslist.TabIndex = 431;
            this.chk_poslist.Text = "SELECT ALL";
            this.chk_poslist.UseVisualStyleBackColor = true;
            this.chk_poslist.CheckedChanged += new System.EventHandler(this.chk_poslist_CheckedChanged);
            // 
            // Chk_WithoutNC
            // 
            this.Chk_WithoutNC.AutoSize = true;
            this.Chk_WithoutNC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_WithoutNC.Location = new System.Drawing.Point(11, 10);
            this.Chk_WithoutNC.Name = "Chk_WithoutNC";
            this.Chk_WithoutNC.Size = new System.Drawing.Size(92, 19);
            this.Chk_WithoutNC.TabIndex = 652;
            this.Chk_WithoutNC.Text = "Without NC";
            this.Chk_WithoutNC.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.POS_LISTBOX);
            this.panel1.Controls.Add(this.chk_poslist);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(25, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 321);
            this.panel1.TabIndex = 653;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.USER_LISTBOX);
            this.panel2.Controls.Add(this.chk_userlist);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(281, 149);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 321);
            this.panel2.TabIndex = 654;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chk_PAYMENT);
            this.panel3.Controls.Add(this.PAYMENT_LISTBOX);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(539, 150);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 321);
            this.panel3.TabIndex = 655;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.PAYMENTWISE);
            this.panel4.Controls.Add(this.USERWISE);
            this.panel4.Location = new System.Drawing.Point(28, 482);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(497, 40);
            this.panel4.TabIndex = 656;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.chkbox_withdaybrkup);
            this.panel5.Location = new System.Drawing.Point(537, 483);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(247, 40);
            this.panel5.TabIndex = 657;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.dtp_fromdate);
            this.panel6.Controls.Add(this.dtp_todate);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(29, 529);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(496, 40);
            this.panel6.TabIndex = 658;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.Chk_WithoutNC);
            this.panel7.Location = new System.Drawing.Point(536, 530);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(247, 38);
            this.panel7.TabIndex = 659;
            // 
            // SETTLEMENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(814, 593);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SETTLEMENT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SETTLEMENT";
            this.Load += new System.EventHandler(this.SETTLEMENT_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chk_userlist;
        private System.Windows.Forms.CheckedListBox POS_LISTBOX;
        private System.Windows.Forms.CheckedListBox USER_LISTBOX;
        private System.Windows.Forms.CheckedListBox PAYMENT_LISTBOX;
        private System.Windows.Forms.DateTimePicker dtp_fromdate;
        private System.Windows.Forms.DateTimePicker dtp_todate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox chkbox_withdaybrkup;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox Chk_SETTLEMENT;
        internal System.Windows.Forms.CheckBox PAYMENTWISE;
        internal System.Windows.Forms.CheckBox USERWISE;
        private System.Windows.Forms.CheckBox chk_PAYMENT;
        private System.Windows.Forms.CheckBox chk_poslist;
        private System.Windows.Forms.CheckBox Chk_WithoutNC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}