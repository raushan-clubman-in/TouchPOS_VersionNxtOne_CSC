namespace TouchPOS.REPORTS
{
    partial class ITEMWISESALESREPORT
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
            this.chk_his = new System.Windows.Forms.CheckBox();
            this.chk_top10 = new System.Windows.Forms.CheckBox();
            this.CHK_GROUP = new System.Windows.Forms.CheckBox();
            this.CHK_CATEGORY = new System.Windows.Forms.CheckBox();
            this.chk_subgroupwise = new System.Windows.Forms.CheckBox();
            this.chk_itemwise = new System.Windows.Forms.CheckBox();
            this.chk_poswise = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chklist_POSlocation = new System.Windows.Forms.CheckedListBox();
            this.lstsubgroup = new System.Windows.Forms.CheckedListBox();
            this.Chk_SelectAllGroup = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LstGroup = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Chk_SelectAllsubgroup = new System.Windows.Forms.CheckBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Chk_SelectAllCategory = new System.Windows.Forms.CheckBox();
            this.lstcategory = new System.Windows.Forms.CheckedListBox();
            this.Chk_POSlocation = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Chk_Taxtype = new System.Windows.Forms.CheckBox();
            this.chklist_Type = new System.Windows.Forms.CheckedListBox();
            this.CHKLIST_PAYMENTMODE = new System.Windows.Forms.CheckedListBox();
            this.Chk_PAYMENTMODE = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.mskTodate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.mskFromdate = new System.Windows.Forms.DateTimePicker();
            this.Cmb_Order = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.label1.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1040, 43);
            this.label1.TabIndex = 43;
            this.label1.Text = "Item Wise Sales Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(29, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1009, 71);
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
            this.btn_new.Location = new System.Drawing.Point(15, 9);
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
            this.btn_save.Location = new System.Drawing.Point(419, 9);
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
            this.Btn_exit.Location = new System.Drawing.Point(802, 9);
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
            this.checkBox1.Location = new System.Drawing.Point(87, 736);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 427;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // chk_his
            // 
            this.chk_his.BackColor = System.Drawing.Color.Transparent;
            this.chk_his.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_his.Location = new System.Drawing.Point(974, 10);
            this.chk_his.Name = "chk_his";
            this.chk_his.Size = new System.Drawing.Size(16, 22);
            this.chk_his.TabIndex = 435;
            this.chk_his.Text = "Sales History";
            this.chk_his.UseVisualStyleBackColor = false;
            this.chk_his.Visible = false;
            this.chk_his.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chk_top10
            // 
            this.chk_top10.BackColor = System.Drawing.Color.Transparent;
            this.chk_top10.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_top10.Location = new System.Drawing.Point(843, 10);
            this.chk_top10.Name = "chk_top10";
            this.chk_top10.Size = new System.Drawing.Size(143, 22);
            this.chk_top10.TabIndex = 434;
            this.chk_top10.Text = "Top 10 Sale Item";
            this.chk_top10.UseVisualStyleBackColor = false;
            this.chk_top10.CheckedChanged += new System.EventHandler(this.chk_top10_CheckedChanged);
            // 
            // CHK_GROUP
            // 
            this.CHK_GROUP.BackColor = System.Drawing.Color.Transparent;
            this.CHK_GROUP.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_GROUP.Location = new System.Drawing.Point(498, 9);
            this.CHK_GROUP.Name = "CHK_GROUP";
            this.CHK_GROUP.Size = new System.Drawing.Size(119, 23);
            this.CHK_GROUP.TabIndex = 433;
            this.CHK_GROUP.Text = "Group Wise";
            this.CHK_GROUP.UseVisualStyleBackColor = false;
            this.CHK_GROUP.CheckedChanged += new System.EventHandler(this.CHK_GROUP_CheckedChanged);
            // 
            // CHK_CATEGORY
            // 
            this.CHK_CATEGORY.BackColor = System.Drawing.Color.Transparent;
            this.CHK_CATEGORY.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_CATEGORY.Location = new System.Drawing.Point(317, 9);
            this.CHK_CATEGORY.Name = "CHK_CATEGORY";
            this.CHK_CATEGORY.Size = new System.Drawing.Size(136, 23);
            this.CHK_CATEGORY.TabIndex = 432;
            this.CHK_CATEGORY.Text = "Category Wise";
            this.CHK_CATEGORY.UseVisualStyleBackColor = false;
            this.CHK_CATEGORY.CheckedChanged += new System.EventHandler(this.CHK_CATEGORY_CheckedChanged);
            // 
            // chk_subgroupwise
            // 
            this.chk_subgroupwise.BackColor = System.Drawing.Color.Transparent;
            this.chk_subgroupwise.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_subgroupwise.Location = new System.Drawing.Point(662, 7);
            this.chk_subgroupwise.Name = "chk_subgroupwise";
            this.chk_subgroupwise.Size = new System.Drawing.Size(136, 27);
            this.chk_subgroupwise.TabIndex = 431;
            this.chk_subgroupwise.Text = "Subgroup Wise";
            this.chk_subgroupwise.UseVisualStyleBackColor = false;
            this.chk_subgroupwise.CheckedChanged += new System.EventHandler(this.chk_subgroupwise_CheckedChanged);
            // 
            // chk_itemwise
            // 
            this.chk_itemwise.BackColor = System.Drawing.Color.Transparent;
            this.chk_itemwise.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_itemwise.Location = new System.Drawing.Point(16, 10);
            this.chk_itemwise.Name = "chk_itemwise";
            this.chk_itemwise.Size = new System.Drawing.Size(98, 22);
            this.chk_itemwise.TabIndex = 429;
            this.chk_itemwise.Text = "Item Wise";
            this.chk_itemwise.UseVisualStyleBackColor = false;
            this.chk_itemwise.CheckedChanged += new System.EventHandler(this.chk_itemwise_CheckedChanged);
            // 
            // chk_poswise
            // 
            this.chk_poswise.BackColor = System.Drawing.Color.Transparent;
            this.chk_poswise.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_poswise.Location = new System.Drawing.Point(165, 8);
            this.chk_poswise.Name = "chk_poswise";
            this.chk_poswise.Size = new System.Drawing.Size(94, 26);
            this.chk_poswise.TabIndex = 430;
            this.chk_poswise.Text = "Pos Wise";
            this.chk_poswise.UseVisualStyleBackColor = false;
            this.chk_poswise.CheckedChanged += new System.EventHandler(this.chk_poswise_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chklist_POSlocation);
            this.panel1.Controls.Add(this.Chk_POSlocation);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(796, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 445);
            this.panel1.TabIndex = 427;
            // 
            // chklist_POSlocation
            // 
            this.chklist_POSlocation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_POSlocation.Location = new System.Drawing.Point(12, 82);
            this.chklist_POSlocation.Name = "chklist_POSlocation";
            this.chklist_POSlocation.Size = new System.Drawing.Size(224, 349);
            this.chklist_POSlocation.TabIndex = 638;
            // 
            // lstsubgroup
            // 
            this.lstsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstsubgroup.Location = new System.Drawing.Point(10, 76);
            this.lstsubgroup.Name = "lstsubgroup";
            this.lstsubgroup.Size = new System.Drawing.Size(226, 356);
            this.lstsubgroup.TabIndex = 651;
            // 
            // Chk_SelectAllGroup
            // 
            this.Chk_SelectAllGroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllGroup.Location = new System.Drawing.Point(9, 13);
            this.Chk_SelectAllGroup.Name = "Chk_SelectAllGroup";
            this.Chk_SelectAllGroup.Size = new System.Drawing.Size(151, 16);
            this.Chk_SelectAllGroup.TabIndex = 655;
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
            this.label7.Location = new System.Drawing.Point(9, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(226, 24);
            this.label7.TabIndex = 654;
            this.label7.Text = "Group Description :";
            // 
            // LstGroup
            // 
            this.LstGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstGroup.Location = new System.Drawing.Point(9, 63);
            this.LstGroup.Name = "LstGroup";
            this.LstGroup.Size = new System.Drawing.Size(226, 148);
            this.LstGroup.TabIndex = 653;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(11, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(224, 28);
            this.label8.TabIndex = 652;
            this.label8.Text = "Subgroup Description :";
            // 
            // Chk_SelectAllsubgroup
            // 
            this.Chk_SelectAllsubgroup.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllsubgroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllsubgroup.Location = new System.Drawing.Point(13, 14);
            this.Chk_SelectAllsubgroup.Name = "Chk_SelectAllsubgroup";
            this.Chk_SelectAllsubgroup.Size = new System.Drawing.Size(169, 24);
            this.Chk_SelectAllsubgroup.TabIndex = 650;
            this.Chk_SelectAllsubgroup.Text = "Select All";
            this.Chk_SelectAllsubgroup.UseVisualStyleBackColor = false;
            this.Chk_SelectAllsubgroup.CheckedChanged += new System.EventHandler(this.Chk_SelectAllsubgroup_CheckedChanged);
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.White;
            this.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(12, 42);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(224, 27);
            this.Label12.TabIndex = 648;
            this.Label12.Text = "Category :";
            // 
            // Chk_SelectAllCategory
            // 
            this.Chk_SelectAllCategory.BackColor = System.Drawing.Color.Transparent;
            this.Chk_SelectAllCategory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SelectAllCategory.Location = new System.Drawing.Point(14, 12);
            this.Chk_SelectAllCategory.Name = "Chk_SelectAllCategory";
            this.Chk_SelectAllCategory.Size = new System.Drawing.Size(203, 24);
            this.Chk_SelectAllCategory.TabIndex = 646;
            this.Chk_SelectAllCategory.Text = "Select All";
            this.Chk_SelectAllCategory.UseVisualStyleBackColor = false;
            this.Chk_SelectAllCategory.CheckedChanged += new System.EventHandler(this.Chk_SelectAllCategory_CheckedChanged);
            // 
            // lstcategory
            // 
            this.lstcategory.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstcategory.Location = new System.Drawing.Point(12, 72);
            this.lstcategory.Name = "lstcategory";
            this.lstcategory.Size = new System.Drawing.Size(224, 139);
            this.lstcategory.TabIndex = 647;
            // 
            // Chk_POSlocation
            // 
            this.Chk_POSlocation.BackColor = System.Drawing.Color.Transparent;
            this.Chk_POSlocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_POSlocation.Location = new System.Drawing.Point(12, 11);
            this.Chk_POSlocation.Name = "Chk_POSlocation";
            this.Chk_POSlocation.Size = new System.Drawing.Size(178, 24);
            this.Chk_POSlocation.TabIndex = 640;
            this.Chk_POSlocation.Text = "Select All";
            this.Chk_POSlocation.UseVisualStyleBackColor = false;
            this.Chk_POSlocation.CheckedChanged += new System.EventHandler(this.Chk_POSlocation_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(12, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(222, 33);
            this.label11.TabIndex = 639;
            this.label11.Text = "POS Description :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(10, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(225, 31);
            this.label13.TabIndex = 637;
            this.label13.Text = "Items   :";
            // 
            // Chk_Taxtype
            // 
            this.Chk_Taxtype.BackColor = System.Drawing.Color.Transparent;
            this.Chk_Taxtype.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_Taxtype.Location = new System.Drawing.Point(12, 10);
            this.Chk_Taxtype.Name = "Chk_Taxtype";
            this.Chk_Taxtype.Size = new System.Drawing.Size(193, 24);
            this.Chk_Taxtype.TabIndex = 636;
            this.Chk_Taxtype.Text = "Select All";
            this.Chk_Taxtype.UseVisualStyleBackColor = false;
            this.Chk_Taxtype.CheckedChanged += new System.EventHandler(this.Chk_Taxtype_CheckedChanged);
            // 
            // chklist_Type
            // 
            this.chklist_Type.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklist_Type.HorizontalScrollbar = true;
            this.chklist_Type.Location = new System.Drawing.Point(10, 78);
            this.chklist_Type.Name = "chklist_Type";
            this.chklist_Type.Size = new System.Drawing.Size(226, 349);
            this.chklist_Type.TabIndex = 635;
            // 
            // CHKLIST_PAYMENTMODE
            // 
            this.CHKLIST_PAYMENTMODE.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKLIST_PAYMENTMODE.Location = new System.Drawing.Point(37, 749);
            this.CHKLIST_PAYMENTMODE.Name = "CHKLIST_PAYMENTMODE";
            this.CHKLIST_PAYMENTMODE.Size = new System.Drawing.Size(44, 4);
            this.CHKLIST_PAYMENTMODE.TabIndex = 649;
            this.CHKLIST_PAYMENTMODE.Visible = false;
            // 
            // Chk_PAYMENTMODE
            // 
            this.Chk_PAYMENTMODE.BackColor = System.Drawing.Color.Transparent;
            this.Chk_PAYMENTMODE.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_PAYMENTMODE.Location = new System.Drawing.Point(39, 738);
            this.Chk_PAYMENTMODE.Name = "Chk_PAYMENTMODE";
            this.Chk_PAYMENTMODE.Size = new System.Drawing.Size(28, 10);
            this.Chk_PAYMENTMODE.TabIndex = 645;
            this.Chk_PAYMENTMODE.Text = "SELECT ALL";
            this.Chk_PAYMENTMODE.UseVisualStyleBackColor = false;
            this.Chk_PAYMENTMODE.Visible = false;
            this.Chk_PAYMENTMODE.CheckedChanged += new System.EventHandler(this.Chk_PAYMENTMODE_CheckedChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(34, 736);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 10);
            this.label9.TabIndex = 644;
            this.label9.Text = "PAYMENTMODE:";
            this.label9.Visible = false;
            // 
            // mskTodate
            // 
            this.mskTodate.CustomFormat = "dd/MM/yyyy";
            this.mskTodate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mskTodate.Location = new System.Drawing.Point(397, 8);
            this.mskTodate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.mskTodate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.mskTodate.Name = "mskTodate";
            this.mskTodate.Size = new System.Drawing.Size(176, 21);
            this.mskTodate.TabIndex = 19;
            this.mskTodate.Value = new System.DateTime(2019, 11, 13, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "FROM DATE :";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(307, 11);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(79, 15);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "TO DATE       :";
            // 
            // mskFromdate
            // 
            this.mskFromdate.CustomFormat = "dd/MM/yyyy";
            this.mskFromdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mskFromdate.Location = new System.Drawing.Point(104, 9);
            this.mskFromdate.MaxDate = new System.DateTime(9998, 8, 14, 0, 0, 0, 0);
            this.mskFromdate.MinDate = new System.DateTime(2005, 8, 14, 0, 0, 0, 0);
            this.mskFromdate.Name = "mskFromdate";
            this.mskFromdate.Size = new System.Drawing.Size(167, 21);
            this.mskFromdate.TabIndex = 18;
            this.mskFromdate.Value = new System.DateTime(2019, 11, 12, 0, 0, 0, 0);
            // 
            // Cmb_Order
            // 
            this.Cmb_Order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Order.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Order.FormattingEnabled = true;
            this.Cmb_Order.Items.AddRange(new object[] {
            "ITEMCODE",
            "ITEMDESC"});
            this.Cmb_Order.Location = new System.Drawing.Point(845, 6);
            this.Cmb_Order.Name = "Cmb_Order";
            this.Cmb_Order.Size = new System.Drawing.Size(145, 24);
            this.Cmb_Order.TabIndex = 636;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(770, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 650;
            this.label2.Text = "Order By";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lstcategory);
            this.panel2.Controls.Add(this.Chk_SelectAllCategory);
            this.panel2.Controls.Add(this.Label12);
            this.panel2.Location = new System.Drawing.Point(37, 176);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 220);
            this.panel2.TabIndex = 651;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Chk_SelectAllGroup);
            this.panel3.Controls.Add(this.LstGroup);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(37, 403);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 220);
            this.panel3.TabIndex = 652;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lstsubgroup);
            this.panel4.Controls.Add(this.Chk_SelectAllsubgroup);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(293, 176);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 445);
            this.panel4.TabIndex = 653;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.chklist_Type);
            this.panel5.Controls.Add(this.Chk_Taxtype);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Location = new System.Drawing.Point(546, 177);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 445);
            this.panel5.TabIndex = 654;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.chk_his);
            this.panel6.Controls.Add(this.chk_itemwise);
            this.panel6.Controls.Add(this.chk_top10);
            this.panel6.Controls.Add(this.chk_poswise);
            this.panel6.Controls.Add(this.chk_subgroupwise);
            this.panel6.Controls.Add(this.CHK_GROUP);
            this.panel6.Controls.Add(this.CHK_CATEGORY);
            this.panel6.Location = new System.Drawing.Point(37, 633);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1002, 42);
            this.panel6.TabIndex = 655;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.mskTodate);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.mskFromdate);
            this.panel7.Controls.Add(this.Label5);
            this.panel7.Controls.Add(this.Cmb_Order);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Location = new System.Drawing.Point(37, 681);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1003, 37);
            this.panel7.TabIndex = 656;
            // 
            // ITEMWISESALESREPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1076, 744);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CHKLIST_PAYMENTMODE);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Chk_PAYMENTMODE);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ITEMWISESALESREPORT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSWISE";
            this.Load += new System.EventHandler(this.ITEMWISESALESREPORT_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
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
        internal System.Windows.Forms.CheckBox chk_itemwise;
        internal System.Windows.Forms.CheckBox chk_poswise;
        internal System.Windows.Forms.CheckBox chk_subgroupwise;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckedListBox chklist_POSlocation;
        internal System.Windows.Forms.CheckedListBox lstsubgroup;
        internal System.Windows.Forms.CheckBox Chk_SelectAllGroup;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckedListBox LstGroup;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox Chk_SelectAllsubgroup;
        internal System.Windows.Forms.CheckedListBox CHKLIST_PAYMENTMODE;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox Chk_SelectAllCategory;
        internal System.Windows.Forms.CheckedListBox lstcategory;
        internal System.Windows.Forms.CheckBox Chk_PAYMENTMODE;
        internal System.Windows.Forms.Label label9;
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
        internal System.Windows.Forms.CheckBox CHK_GROUP;
        internal System.Windows.Forms.CheckBox CHK_CATEGORY;
        internal System.Windows.Forms.CheckBox chk_top10;
        internal System.Windows.Forms.CheckBox chk_his;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}