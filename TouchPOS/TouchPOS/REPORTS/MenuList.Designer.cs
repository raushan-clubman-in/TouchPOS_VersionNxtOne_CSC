namespace TouchPOS.REPORTS
{
    partial class MenuList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_view = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.Chk_POSlocation = new System.Windows.Forms.CheckBox();
            this.chklist_POSlocation = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label12 = new System.Windows.Forms.Label();
            this.Chk_SelectAllCategory = new System.Windows.Forms.CheckBox();
            this.lstcategory = new System.Windows.Forms.CheckedListBox();
            this.lstsubgroup = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Chk_SelectAllsubgroup = new System.Windows.Forms.CheckBox();
            this.Chk_SelectAllGroup = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LstGroup = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_view);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(27, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(878, 66);
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
            this.btn_new.Location = new System.Drawing.Point(19, 6);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(191, 54);
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
            this.btn_view.Location = new System.Drawing.Point(360, 6);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(191, 54);
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
            this.Btn_exit.Location = new System.Drawing.Point(671, 6);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(191, 54);
            this.Btn_exit.TabIndex = 32;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(10, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 22);
            this.label11.TabIndex = 642;
            this.label11.Text = "POS Description :";
            // 
            // Chk_POSlocation
            // 
            this.Chk_POSlocation.BackColor = System.Drawing.Color.Transparent;
            this.Chk_POSlocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_POSlocation.Location = new System.Drawing.Point(10, 9);
            this.Chk_POSlocation.Name = "Chk_POSlocation";
            this.Chk_POSlocation.Size = new System.Drawing.Size(178, 24);
            this.Chk_POSlocation.TabIndex = 643;
            this.Chk_POSlocation.Text = "SELECT ALL";
            this.Chk_POSlocation.UseVisualStyleBackColor = false;
            this.Chk_POSlocation.CheckedChanged += new System.EventHandler(this.Chk_POSlocation_CheckedChanged);
            // 
            // chklist_POSlocation
            // 
            this.chklist_POSlocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chklist_POSlocation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_POSlocation.Location = new System.Drawing.Point(10, 60);
            this.chklist_POSlocation.Name = "chklist_POSlocation";
            this.chklist_POSlocation.Size = new System.Drawing.Size(193, 272);
            this.chklist_POSlocation.TabIndex = 641;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Label12);
            this.panel1.Controls.Add(this.Chk_SelectAllCategory);
            this.panel1.Controls.Add(this.lstcategory);
            this.panel1.Location = new System.Drawing.Point(696, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 343);
            this.panel1.TabIndex = 47;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.White;
            this.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(10, 32);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(193, 24);
            this.Label12.TabIndex = 664;
            this.Label12.Text = "Category";
            // 
            // Chk_SelectAllCategory
            // 
            this.Chk_SelectAllCategory.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllCategory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllCategory.Location = new System.Drawing.Point(11, 8);
            this.Chk_SelectAllCategory.Name = "Chk_SelectAllCategory";
            this.Chk_SelectAllCategory.Size = new System.Drawing.Size(130, 24);
            this.Chk_SelectAllCategory.TabIndex = 662;
            this.Chk_SelectAllCategory.Text = "SELECT ALL ";
            this.Chk_SelectAllCategory.UseVisualStyleBackColor = false;
            this.Chk_SelectAllCategory.CheckedChanged += new System.EventHandler(this.Chk_SelectAllCategory_CheckedChanged);
            // 
            // lstcategory
            // 
            this.lstcategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstcategory.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstcategory.Location = new System.Drawing.Point(9, 58);
            this.lstcategory.Name = "lstcategory";
            this.lstcategory.Size = new System.Drawing.Size(193, 272);
            this.lstcategory.TabIndex = 663;
            // 
            // lstsubgroup
            // 
            this.lstsubgroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstsubgroup.Location = new System.Drawing.Point(10, 57);
            this.lstsubgroup.Name = "lstsubgroup";
            this.lstsubgroup.Size = new System.Drawing.Size(193, 274);
            this.lstsubgroup.TabIndex = 660;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(10, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 23);
            this.label8.TabIndex = 661;
            this.label8.Text = "Subgroup Description :";
            // 
            // Chk_SelectAllsubgroup
            // 
            this.Chk_SelectAllsubgroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllsubgroup.Location = new System.Drawing.Point(11, 8);
            this.Chk_SelectAllsubgroup.Name = "Chk_SelectAllsubgroup";
            this.Chk_SelectAllsubgroup.Size = new System.Drawing.Size(169, 21);
            this.Chk_SelectAllsubgroup.TabIndex = 659;
            this.Chk_SelectAllsubgroup.Text = "SELECT ALL ";
            this.Chk_SelectAllsubgroup.UseVisualStyleBackColor = false;
            this.Chk_SelectAllsubgroup.CheckedChanged += new System.EventHandler(this.Chk_SelectAllsubgroup_CheckedChanged);
            // 
            // Chk_SelectAllGroup
            // 
            this.Chk_SelectAllGroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllGroup.Location = new System.Drawing.Point(10, 13);
            this.Chk_SelectAllGroup.Name = "Chk_SelectAllGroup";
            this.Chk_SelectAllGroup.Size = new System.Drawing.Size(151, 16);
            this.Chk_SelectAllGroup.TabIndex = 658;
            this.Chk_SelectAllGroup.Text = "SELECT ALL ";
            this.Chk_SelectAllGroup.UseVisualStyleBackColor = false;
            this.Chk_SelectAllGroup.CheckedChanged += new System.EventHandler(this.Chk_SelectAllGroup_CheckedChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 23);
            this.label7.TabIndex = 657;
            this.label7.Text = "Group Description :";
            // 
            // LstGroup
            // 
            this.LstGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstGroup.Location = new System.Drawing.Point(9, 60);
            this.LstGroup.Name = "LstGroup";
            this.LstGroup.Size = new System.Drawing.Size(193, 274);
            this.LstGroup.TabIndex = 656;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(906, 41);
            this.label1.TabIndex = 100;
            this.label1.Text = "Menu List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chklist_POSlocation);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.Chk_POSlocation);
            this.panel2.Location = new System.Drawing.Point(28, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 343);
            this.panel2.TabIndex = 101;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.LstGroup);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.Chk_SelectAllGroup);
            this.panel3.Location = new System.Drawing.Point(250, 144);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(213, 343);
            this.panel3.TabIndex = 102;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lstsubgroup);
            this.panel4.Controls.Add(this.Chk_SelectAllsubgroup);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(473, 144);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(213, 343);
            this.panel4.TabIndex = 103;
            // 
            // MenuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(940, 512);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuList";
            this.Load += new System.EventHandler(this.MenuList_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.Button Btn_exit;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.CheckBox Chk_POSlocation;
        internal System.Windows.Forms.CheckedListBox chklist_POSlocation;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckBox Chk_SelectAllGroup;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckedListBox LstGroup;
        internal System.Windows.Forms.CheckedListBox lstsubgroup;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox Chk_SelectAllsubgroup;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox Chk_SelectAllCategory;
        internal System.Windows.Forms.CheckedListBox lstcategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}