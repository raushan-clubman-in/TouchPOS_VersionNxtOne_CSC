namespace TouchPOS.REPORTS
{
    partial class POSWISE
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
            this.BEARER_LISTBOX = new System.Windows.Forms.CheckedListBox();
            this.dtp_fromdate = new System.Windows.Forms.DateTimePicker();
            this.dtp_todate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkbox_withdaybrkup = new System.Windows.Forms.CheckBox();
            this.Chk_SUMM = new System.Windows.Forms.CheckBox();
            this.CHK_DET = new System.Windows.Forms.CheckBox();
            this.CHK_CATEGORY = new System.Windows.Forms.CheckBox();
            this.Chk_PosWise = new System.Windows.Forms.CheckBox();
            this.BEARERWISE = new System.Windows.Forms.CheckBox();
            this.USERWISE = new System.Windows.Forms.CheckBox();
            this.chk_bearer = new System.Windows.Forms.CheckBox();
            this.chk_poslist = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
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
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(786, 34);
            this.label1.TabIndex = 43;
            this.label1.Text = "POS Wise Sales Register";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(30, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 64);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_new.BackgroundImage = global::TouchPOS.Properties.Resources.BluebuttonMaster;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.ForeColor = System.Drawing.Color.White;
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(16, 9);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(202, 50);
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
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(280, 9);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(202, 50);
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
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(540, 9);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(202, 50);
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
            this.Label2.Location = new System.Drawing.Point(13, 38);
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
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 24);
            this.label3.TabIndex = 424;
            this.label3.Text = "User Name List :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(9, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 24);
            this.label4.TabIndex = 426;
            this.label4.Text = "Bearer Name List :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // chk_userlist
            // 
            this.chk_userlist.AutoSize = true;
            this.chk_userlist.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_userlist.Location = new System.Drawing.Point(12, 11);
            this.chk_userlist.Name = "chk_userlist";
            this.chk_userlist.Size = new System.Drawing.Size(92, 22);
            this.chk_userlist.TabIndex = 427;
            this.chk_userlist.Text = "Select All";
            this.chk_userlist.UseVisualStyleBackColor = true;
            this.chk_userlist.CheckedChanged += new System.EventHandler(this.chk_userlist_CheckedChanged);
            // 
            // POS_LISTBOX
            // 
            this.POS_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.POS_LISTBOX.FormattingEnabled = true;
            this.POS_LISTBOX.Location = new System.Drawing.Point(13, 65);
            this.POS_LISTBOX.Name = "POS_LISTBOX";
            this.POS_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.POS_LISTBOX.TabIndex = 428;
            this.POS_LISTBOX.ThreeDCheckBoxes = true;
            // 
            // USER_LISTBOX
            // 
            this.USER_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.USER_LISTBOX.FormattingEnabled = true;
            this.USER_LISTBOX.Location = new System.Drawing.Point(12, 66);
            this.USER_LISTBOX.Name = "USER_LISTBOX";
            this.USER_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.USER_LISTBOX.TabIndex = 429;
            this.USER_LISTBOX.ThreeDCheckBoxes = true;
            // 
            // BEARER_LISTBOX
            // 
            this.BEARER_LISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BEARER_LISTBOX.FormattingEnabled = true;
            this.BEARER_LISTBOX.Location = new System.Drawing.Point(9, 66);
            this.BEARER_LISTBOX.Name = "BEARER_LISTBOX";
            this.BEARER_LISTBOX.Size = new System.Drawing.Size(220, 242);
            this.BEARER_LISTBOX.TabIndex = 430;
            this.BEARER_LISTBOX.ThreeDCheckBoxes = true;
            // 
            // dtp_fromdate
            // 
            this.dtp_fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fromdate.Location = new System.Drawing.Point(117, 6);
            this.dtp_fromdate.Name = "dtp_fromdate";
            this.dtp_fromdate.Size = new System.Drawing.Size(118, 26);
            this.dtp_fromdate.TabIndex = 431;
            // 
            // dtp_todate
            // 
            this.dtp_todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_todate.Location = new System.Drawing.Point(355, 6);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(123, 26);
            this.dtp_todate.TabIndex = 432;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(266, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 434;
            this.label6.Text = "To Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 433;
            this.label5.Text = "FromDate";
            // 
            // chkbox_withdaybrkup
            // 
            this.chkbox_withdaybrkup.AutoSize = true;
            this.chkbox_withdaybrkup.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_withdaybrkup.Location = new System.Drawing.Point(8, 10);
            this.chkbox_withdaybrkup.Name = "chkbox_withdaybrkup";
            this.chkbox_withdaybrkup.Size = new System.Drawing.Size(152, 22);
            this.chkbox_withdaybrkup.TabIndex = 0;
            this.chkbox_withdaybrkup.Text = "With Day Breakup";
            this.chkbox_withdaybrkup.UseVisualStyleBackColor = true;
            // 
            // Chk_SUMM
            // 
            this.Chk_SUMM.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SUMM.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SUMM.Location = new System.Drawing.Point(11, 9);
            this.Chk_SUMM.Name = "Chk_SUMM";
            this.Chk_SUMM.Size = new System.Drawing.Size(104, 22);
            this.Chk_SUMM.TabIndex = 429;
            this.Chk_SUMM.Text = "Summary";
            this.Chk_SUMM.UseVisualStyleBackColor = false;
            this.Chk_SUMM.CheckedChanged += new System.EventHandler(this.Chk_SUMM_CheckedChanged);
            // 
            // CHK_DET
            // 
            this.CHK_DET.BackColor = System.Drawing.Color.Transparent;
            this.CHK_DET.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_DET.Location = new System.Drawing.Point(124, 7);
            this.CHK_DET.Name = "CHK_DET";
            this.CHK_DET.Size = new System.Drawing.Size(108, 26);
            this.CHK_DET.TabIndex = 430;
            this.CHK_DET.Text = "Details";
            this.CHK_DET.UseVisualStyleBackColor = false;
            this.CHK_DET.CheckedChanged += new System.EventHandler(this.CHK_DET_CheckedChanged);
            // 
            // CHK_CATEGORY
            // 
            this.CHK_CATEGORY.BackColor = System.Drawing.Color.Transparent;
            this.CHK_CATEGORY.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_CATEGORY.Location = new System.Drawing.Point(352, 10);
            this.CHK_CATEGORY.Name = "CHK_CATEGORY";
            this.CHK_CATEGORY.Size = new System.Drawing.Size(140, 23);
            this.CHK_CATEGORY.TabIndex = 435;
            this.CHK_CATEGORY.Text = "Category Wise";
            this.CHK_CATEGORY.UseVisualStyleBackColor = false;
            this.CHK_CATEGORY.CheckedChanged += new System.EventHandler(this.CHK_CATEGORY_CheckedChanged);
            // 
            // Chk_PosWise
            // 
            this.Chk_PosWise.BackColor = System.Drawing.Color.Transparent;
            this.Chk_PosWise.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_PosWise.Location = new System.Drawing.Point(252, 11);
            this.Chk_PosWise.Name = "Chk_PosWise";
            this.Chk_PosWise.Size = new System.Drawing.Size(96, 23);
            this.Chk_PosWise.TabIndex = 434;
            this.Chk_PosWise.Text = "POS Wise";
            this.Chk_PosWise.UseVisualStyleBackColor = false;
            this.Chk_PosWise.CheckedChanged += new System.EventHandler(this.Chk_PosWise_CheckedChanged);
            // 
            // BEARERWISE
            // 
            this.BEARERWISE.BackColor = System.Drawing.Color.Transparent;
            this.BEARERWISE.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BEARERWISE.Location = new System.Drawing.Point(117, 9);
            this.BEARERWISE.Name = "BEARERWISE";
            this.BEARERWISE.Size = new System.Drawing.Size(123, 27);
            this.BEARERWISE.TabIndex = 432;
            this.BEARERWISE.Text = "Bearer Wise";
            this.BEARERWISE.UseVisualStyleBackColor = false;
            this.BEARERWISE.CheckedChanged += new System.EventHandler(this.BEARERWISE_CheckedChanged);
            // 
            // USERWISE
            // 
            this.USERWISE.BackColor = System.Drawing.Color.Transparent;
            this.USERWISE.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.USERWISE.Location = new System.Drawing.Point(7, 6);
            this.USERWISE.Name = "USERWISE";
            this.USERWISE.Size = new System.Drawing.Size(106, 32);
            this.USERWISE.TabIndex = 431;
            this.USERWISE.Text = "User Wise";
            this.USERWISE.UseVisualStyleBackColor = false;
            this.USERWISE.CheckedChanged += new System.EventHandler(this.USERWISE_CheckedChanged);
            // 
            // chk_bearer
            // 
            this.chk_bearer.AutoSize = true;
            this.chk_bearer.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_bearer.Location = new System.Drawing.Point(11, 11);
            this.chk_bearer.Name = "chk_bearer";
            this.chk_bearer.Size = new System.Drawing.Size(92, 22);
            this.chk_bearer.TabIndex = 432;
            this.chk_bearer.Text = "Select All";
            this.chk_bearer.UseVisualStyleBackColor = true;
            this.chk_bearer.CheckedChanged += new System.EventHandler(this.chk_bearer_CheckedChanged);
            // 
            // chk_poslist
            // 
            this.chk_poslist.AutoSize = true;
            this.chk_poslist.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_poslist.Location = new System.Drawing.Point(17, 11);
            this.chk_poslist.Name = "chk_poslist";
            this.chk_poslist.Size = new System.Drawing.Size(92, 22);
            this.chk_poslist.TabIndex = 431;
            this.chk_poslist.Text = "Select All";
            this.chk_poslist.UseVisualStyleBackColor = true;
            this.chk_poslist.CheckedChanged += new System.EventHandler(this.chk_poslist_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.POS_LISTBOX);
            this.panel1.Controls.Add(this.chk_poslist);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(31, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 320);
            this.panel1.TabIndex = 456;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.USER_LISTBOX);
            this.panel2.Controls.Add(this.chk_userlist);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(286, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 320);
            this.panel2.TabIndex = 457;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chk_bearer);
            this.panel3.Controls.Add(this.BEARER_LISTBOX);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(543, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 320);
            this.panel3.TabIndex = 458;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.CHK_CATEGORY);
            this.panel4.Controls.Add(this.USERWISE);
            this.panel4.Controls.Add(this.Chk_PosWise);
            this.panel4.Controls.Add(this.BEARERWISE);
            this.panel4.Location = new System.Drawing.Point(32, 483);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(499, 41);
            this.panel4.TabIndex = 459;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.CHK_DET);
            this.panel5.Controls.Add(this.Chk_SUMM);
            this.panel5.Location = new System.Drawing.Point(543, 482);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 42);
            this.panel5.TabIndex = 460;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.dtp_fromdate);
            this.panel6.Controls.Add(this.dtp_todate);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(32, 531);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(498, 41);
            this.panel6.TabIndex = 461;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.chkbox_withdaybrkup);
            this.panel7.Location = new System.Drawing.Point(542, 532);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(245, 40);
            this.panel7.TabIndex = 462;
            // 
            // POSWISE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(821, 594);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSWISE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSWISE";
            this.Load += new System.EventHandler(this.POSWISE_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.CheckedListBox BEARER_LISTBOX;
        private System.Windows.Forms.DateTimePicker dtp_fromdate;
        private System.Windows.Forms.DateTimePicker dtp_todate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox chkbox_withdaybrkup;
        internal System.Windows.Forms.CheckBox Chk_SUMM;
        internal System.Windows.Forms.CheckBox CHK_DET;
        internal System.Windows.Forms.CheckBox Chk_PosWise;
        internal System.Windows.Forms.CheckBox BEARERWISE;
        internal System.Windows.Forms.CheckBox USERWISE;
        private System.Windows.Forms.CheckBox chk_bearer;
        private System.Windows.Forms.CheckBox chk_poslist;
        internal System.Windows.Forms.CheckBox CHK_CATEGORY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}