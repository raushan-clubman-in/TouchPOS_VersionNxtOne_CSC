namespace TouchPOS.REPORTS
{
    partial class MEMBERWISE
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
            this.btn_view = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.MEMBER_LIST = new System.Windows.Forms.CheckedListBox();
            this.POS_LIST = new System.Windows.Forms.CheckedListBox();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Chk_SUMM = new System.Windows.Forms.CheckBox();
            this.CHK_DET = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.CHK_BOTH = new System.Windows.Forms.CheckBox();
            this.CHK_MEM = new System.Windows.Forms.CheckBox();
            this.CHK_WALK = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(726, 31);
            this.label1.TabIndex = 43;
            this.label1.Text = "Memberwise Sales Register";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_view);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(26, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 65);
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
            this.btn_new.Location = new System.Drawing.Point(15, 8);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(191, 47);
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
            this.btn_view.Location = new System.Drawing.Point(256, 8);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(191, 47);
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
            this.Btn_exit.Location = new System.Drawing.Point(493, 8);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(191, 47);
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
            this.Label2.Location = new System.Drawing.Point(14, 38);
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
            this.label3.Text = "Member List :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(17, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 20);
            this.checkBox1.TabIndex = 427;
            this.checkBox1.Text = "Select All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MEMBER_LIST
            // 
            this.MEMBER_LIST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MEMBER_LIST.FormattingEnabled = true;
            this.MEMBER_LIST.Location = new System.Drawing.Point(12, 65);
            this.MEMBER_LIST.Name = "MEMBER_LIST";
            this.MEMBER_LIST.Size = new System.Drawing.Size(220, 229);
            this.MEMBER_LIST.TabIndex = 428;
            // 
            // POS_LIST
            // 
            this.POS_LIST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.POS_LIST.FormattingEnabled = true;
            this.POS_LIST.Location = new System.Drawing.Point(14, 64);
            this.POS_LIST.Name = "POS_LIST";
            this.POS_LIST.Size = new System.Drawing.Size(220, 229);
            this.POS_LIST.TabIndex = 429;
            // 
            // dtp1
            // 
            this.dtp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp1.Location = new System.Drawing.Point(142, 12);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(200, 29);
            this.dtp1.TabIndex = 431;
            // 
            // dtp2
            // 
            this.dtp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp2.Location = new System.Drawing.Point(472, 11);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(200, 29);
            this.dtp2.TabIndex = 432;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(367, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 24);
            this.label6.TabIndex = 434;
            this.label6.Text = "To Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 24);
            this.label5.TabIndex = 433;
            this.label5.Text = "From Date";
            // 
            // Chk_SUMM
            // 
            this.Chk_SUMM.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SUMM.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SUMM.Location = new System.Drawing.Point(12, 10);
            this.Chk_SUMM.Name = "Chk_SUMM";
            this.Chk_SUMM.Size = new System.Drawing.Size(115, 22);
            this.Chk_SUMM.TabIndex = 429;
            this.Chk_SUMM.Text = "Summary";
            this.Chk_SUMM.UseVisualStyleBackColor = false;
            this.Chk_SUMM.CheckedChanged += new System.EventHandler(this.Chk_SUMM_CheckedChanged);
            // 
            // CHK_DET
            // 
            this.CHK_DET.BackColor = System.Drawing.Color.Transparent;
            this.CHK_DET.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_DET.Location = new System.Drawing.Point(12, 38);
            this.CHK_DET.Name = "CHK_DET";
            this.CHK_DET.Size = new System.Drawing.Size(92, 26);
            this.CHK_DET.TabIndex = 430;
            this.CHK_DET.Text = "Details";
            this.CHK_DET.UseVisualStyleBackColor = false;
            this.CHK_DET.CheckedChanged += new System.EventHandler(this.CHK_DET_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(15, 12);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(93, 20);
            this.checkBox2.TabIndex = 430;
            this.checkBox2.Text = "Select All";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // CHK_BOTH
            // 
            this.CHK_BOTH.BackColor = System.Drawing.Color.Transparent;
            this.CHK_BOTH.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_BOTH.Location = new System.Drawing.Point(9, 81);
            this.CHK_BOTH.Name = "CHK_BOTH";
            this.CHK_BOTH.Size = new System.Drawing.Size(92, 26);
            this.CHK_BOTH.TabIndex = 433;
            this.CHK_BOTH.Text = "Both";
            this.CHK_BOTH.UseVisualStyleBackColor = false;
            this.CHK_BOTH.CheckedChanged += new System.EventHandler(this.CHK_BOTH_CheckedChanged);
            // 
            // CHK_MEM
            // 
            this.CHK_MEM.BackColor = System.Drawing.Color.Transparent;
            this.CHK_MEM.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_MEM.Location = new System.Drawing.Point(9, 19);
            this.CHK_MEM.Name = "CHK_MEM";
            this.CHK_MEM.Size = new System.Drawing.Size(115, 22);
            this.CHK_MEM.TabIndex = 431;
            this.CHK_MEM.Text = "Member";
            this.CHK_MEM.UseVisualStyleBackColor = false;
            this.CHK_MEM.CheckedChanged += new System.EventHandler(this.CHK_MEM_CheckedChanged);
            // 
            // CHK_WALK
            // 
            this.CHK_WALK.BackColor = System.Drawing.Color.Transparent;
            this.CHK_WALK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_WALK.Location = new System.Drawing.Point(9, 48);
            this.CHK_WALK.Name = "CHK_WALK";
            this.CHK_WALK.Size = new System.Drawing.Size(92, 26);
            this.CHK_WALK.TabIndex = 432;
            this.CHK_WALK.Text = "Walk-In";
            this.CHK_WALK.UseVisualStyleBackColor = false;
            this.CHK_WALK.CheckedChanged += new System.EventHandler(this.CHK_WALK_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.POS_LIST);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(28, 146);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 307);
            this.panel1.TabIndex = 457;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.MEMBER_LIST);
            this.panel2.Location = new System.Drawing.Point(288, 145);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 307);
            this.panel2.TabIndex = 458;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.CHK_DET);
            this.panel3.Controls.Add(this.Chk_SUMM);
            this.panel3.Location = new System.Drawing.Point(547, 144);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(174, 307);
            this.panel3.TabIndex = 459;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.CHK_BOTH);
            this.panel4.Controls.Add(this.CHK_WALK);
            this.panel4.Controls.Add(this.CHK_MEM);
            this.panel4.Location = new System.Drawing.Point(10, 72);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(151, 222);
            this.panel4.TabIndex = 431;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.dtp2);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.dtp1);
            this.panel5.Location = new System.Drawing.Point(28, 465);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(692, 48);
            this.panel5.TabIndex = 460;
            // 
            // MEMBERWISE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(751, 537);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MEMBERWISE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSWISE";
            this.Load += new System.EventHandler(this.MEMBERWISE_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.Button Btn_exit;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckedListBox MEMBER_LIST;
        private System.Windows.Forms.CheckedListBox POS_LIST;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox Chk_SUMM;
        internal System.Windows.Forms.CheckBox CHK_DET;
        private System.Windows.Forms.CheckBox checkBox2;
        internal System.Windows.Forms.CheckBox CHK_BOTH;
        internal System.Windows.Forms.CheckBox CHK_MEM;
        internal System.Windows.Forms.CheckBox CHK_WALK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}