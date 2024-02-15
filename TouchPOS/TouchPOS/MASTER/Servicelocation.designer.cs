namespace TouchPOS.MASTER
{
    partial class Servicelocation
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
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rdb_dinein = new System.Windows.Forms.RadioButton();
            this.rdb_hd = new System.Windows.Forms.RadioButton();
            this.rdb_takeaway = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CMB_S = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_finalbill = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_kot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cmb_Freeze = new System.Windows.Forms.ComboBox();
            this.cmb_scv = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Txt_slname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_slcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LocCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERVICEFLAG1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Svalidate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freeze1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DB2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScardValidate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(28, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 67);
            this.groupBox1.TabIndex = 95;
            this.groupBox1.TabStop = false;
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_new.BackgroundImage = global::TouchPOS.Properties.Resources.BluebuttonMaster;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.ForeColor = System.Drawing.Color.White;
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(17, 5);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(175, 54);
            this.btn_new.TabIndex = 37;
            this.btn_new.Text = "New";
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btn_edit.BackgroundImage = global::TouchPOS.Properties.Resources.OrangebuttonMaster;
            this.btn_edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_edit.ForeColor = System.Drawing.Color.White;
            this.btn_edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_edit.Location = new System.Drawing.Point(214, 5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(175, 54);
            this.btn_edit.TabIndex = 38;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_save.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(411, 5);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(175, 54);
            this.btn_save.TabIndex = 35;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.BackColor = System.Drawing.Color.Red;
            this.Btn_exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(608, 5);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(175, 54);
            this.Btn_exit.TabIndex = 36;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(824, 37);
            this.label1.TabIndex = 39;
            this.label1.Text = "Service Location Master";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.DB2);
            this.groupBox2.Location = new System.Drawing.Point(30, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(794, 476);
            this.groupBox2.TabIndex = 96;
            this.groupBox2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rdb_dinein);
            this.panel5.Controls.Add(this.rdb_hd);
            this.panel5.Controls.Add(this.rdb_takeaway);
            this.panel5.Location = new System.Drawing.Point(395, 265);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(383, 46);
            this.panel5.TabIndex = 493;
            // 
            // rdb_dinein
            // 
            this.rdb_dinein.AutoSize = true;
            this.rdb_dinein.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_dinein.Location = new System.Drawing.Point(5, 9);
            this.rdb_dinein.Name = "rdb_dinein";
            this.rdb_dinein.Size = new System.Drawing.Size(87, 24);
            this.rdb_dinein.TabIndex = 481;
            this.rdb_dinein.TabStop = true;
            this.rdb_dinein.Text = "Dine In";
            this.rdb_dinein.UseVisualStyleBackColor = true;
            this.rdb_dinein.CheckedChanged += new System.EventHandler(this.rdb_dinein_CheckedChanged);
            // 
            // rdb_hd
            // 
            this.rdb_hd.AutoSize = true;
            this.rdb_hd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_hd.Location = new System.Drawing.Point(225, 9);
            this.rdb_hd.Name = "rdb_hd";
            this.rdb_hd.Size = new System.Drawing.Size(151, 24);
            this.rdb_hd.TabIndex = 478;
            this.rdb_hd.TabStop = true;
            this.rdb_hd.Text = "Home Delivery";
            this.rdb_hd.UseVisualStyleBackColor = true;
            this.rdb_hd.CheckedChanged += new System.EventHandler(this.rdb_hd_CheckedChanged);
            // 
            // rdb_takeaway
            // 
            this.rdb_takeaway.AutoSize = true;
            this.rdb_takeaway.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_takeaway.Location = new System.Drawing.Point(99, 9);
            this.rdb_takeaway.Name = "rdb_takeaway";
            this.rdb_takeaway.Size = new System.Drawing.Size(117, 24);
            this.rdb_takeaway.TabIndex = 477;
            this.rdb_takeaway.TabStop = true;
            this.rdb_takeaway.Text = "Take Away";
            this.rdb_takeaway.UseVisualStyleBackColor = true;
            this.rdb_takeaway.CheckedChanged += new System.EventHandler(this.rdb_takeaway_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.CMB_S);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(16, 265);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(370, 48);
            this.panel4.TabIndex = 492;
            // 
            // CMB_S
            // 
            this.CMB_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMB_S.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMB_S.FormattingEnabled = true;
            this.CMB_S.Items.AddRange(new object[] {
            "BOTH",
            "MEMBER",
            "WALK-IN"});
            this.CMB_S.Location = new System.Drawing.Point(201, 10);
            this.CMB_S.Name = "CMB_S";
            this.CMB_S.Size = new System.Drawing.Size(149, 28);
            this.CMB_S.TabIndex = 488;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 20);
            this.label8.TabIndex = 487;
            this.label8.Text = "Service Applied For";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txt_finalbill);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txt_kot);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(16, 184);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 73);
            this.panel3.TabIndex = 491;
            // 
            // txt_finalbill
            // 
            this.txt_finalbill.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_finalbill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_finalbill.Location = new System.Drawing.Point(203, 40);
            this.txt_finalbill.MaxLength = 5;
            this.txt_finalbill.Name = "txt_finalbill";
            this.txt_finalbill.Size = new System.Drawing.Size(150, 26);
            this.txt_finalbill.TabIndex = 485;
            this.txt_finalbill.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_finalbill_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 486;
            this.label7.Text = "Bill Prefix";
            // 
            // txt_kot
            // 
            this.txt_kot.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_kot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_kot.Location = new System.Drawing.Point(204, 8);
            this.txt_kot.MaxLength = 5;
            this.txt_kot.Name = "txt_kot";
            this.txt_kot.Size = new System.Drawing.Size(150, 26);
            this.txt_kot.TabIndex = 483;
            this.txt_kot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_kot_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 484;
            this.label5.Text = "Kot Prefix";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Cmb_Freeze);
            this.panel2.Controls.Add(this.cmb_scv);
            this.panel2.Location = new System.Drawing.Point(15, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 83);
            this.panel2.TabIndex = 490;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 479;
            this.label6.Text = "Freeze";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 20);
            this.label4.TabIndex = 53;
            this.label4.Text = "S Card Validate";
            // 
            // Cmb_Freeze
            // 
            this.Cmb_Freeze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Freeze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Freeze.FormattingEnabled = true;
            this.Cmb_Freeze.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.Cmb_Freeze.Location = new System.Drawing.Point(208, 46);
            this.Cmb_Freeze.Name = "Cmb_Freeze";
            this.Cmb_Freeze.Size = new System.Drawing.Size(148, 28);
            this.Cmb_Freeze.TabIndex = 480;
            // 
            // cmb_scv
            // 
            this.cmb_scv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_scv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_scv.FormattingEnabled = true;
            this.cmb_scv.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.cmb_scv.Location = new System.Drawing.Point(208, 10);
            this.cmb_scv.Name = "cmb_scv";
            this.cmb_scv.Size = new System.Drawing.Size(148, 28);
            this.cmb_scv.TabIndex = 474;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Txt_slname);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Txt_slcode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(14, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 78);
            this.panel1.TabIndex = 489;
            // 
            // Txt_slname
            // 
            this.Txt_slname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_slname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_slname.Location = new System.Drawing.Point(208, 42);
            this.Txt_slname.MaxLength = 60;
            this.Txt_slname.Name = "Txt_slname";
            this.Txt_slname.Size = new System.Drawing.Size(151, 26);
            this.Txt_slname.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "Location Code";
            // 
            // Txt_slcode
            // 
            this.Txt_slcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_slcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_slcode.Location = new System.Drawing.Point(208, 10);
            this.Txt_slcode.Name = "Txt_slcode";
            this.Txt_slcode.ReadOnly = true;
            this.Txt_slcode.Size = new System.Drawing.Size(151, 26);
            this.Txt_slcode.TabIndex = 51;
            this.Txt_slcode.TextChanged += new System.EventHandler(this.Txt_slcode_TextChanged);
            this.Txt_slcode.Validated += new System.EventHandler(this.Txt_slcode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 20);
            this.label3.TabIndex = 52;
            this.label3.Text = "Location Description";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocCode,
            this.LocName,
            this.SERVICEFLAG1,
            this.Svalidate,
            this.Freeze1,
            this.Kot,
            this.Bill,
            this.SAF});
            this.dataGridView1.Location = new System.Drawing.Point(16, 321);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(763, 144);
            this.dataGridView1.TabIndex = 97;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // LocCode
            // 
            this.LocCode.HeaderText = "LocCode";
            this.LocCode.Name = "LocCode";
            // 
            // LocName
            // 
            this.LocName.HeaderText = "LocName";
            this.LocName.Name = "LocName";
            // 
            // SERVICEFLAG1
            // 
            this.SERVICEFLAG1.HeaderText = "Service Flag";
            this.SERVICEFLAG1.Name = "SERVICEFLAG1";
            // 
            // Svalidate
            // 
            this.Svalidate.HeaderText = "Svalidate";
            this.Svalidate.Name = "Svalidate";
            // 
            // Freeze1
            // 
            this.Freeze1.HeaderText = "Status";
            this.Freeze1.Name = "Freeze1";
            // 
            // Kot
            // 
            this.Kot.HeaderText = "Kot";
            this.Kot.Name = "Kot";
            // 
            // Bill
            // 
            this.Bill.HeaderText = "Bill";
            this.Bill.Name = "Bill";
            // 
            // SAF
            // 
            this.SAF.HeaderText = "SAF";
            this.SAF.Name = "SAF";
            // 
            // DB2
            // 
            this.DB2.AllowUserToAddRows = false;
            this.DB2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DB2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn2});
            this.DB2.Location = new System.Drawing.Point(398, 9);
            this.DB2.Name = "DB2";
            this.DB2.Size = new System.Drawing.Size(381, 247);
            this.DB2.TabIndex = 483;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Pos";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            // 
            // ServiceFlag
            // 
            this.ServiceFlag.HeaderText = "ServiceFlag";
            this.ServiceFlag.Name = "ServiceFlag";
            // 
            // ScardValidate
            // 
            this.ScardValidate.HeaderText = "ScardValidate";
            this.ScardValidate.Name = "ScardValidate";
            // 
            // Servicelocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(857, 644);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
           
            this.Text = "Servicelocation";
            this.Load += new System.EventHandler(this.Servicelocation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Txt_slname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_slcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_scv;
        private System.Windows.Forms.ComboBox Cmb_Freeze;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdb_hd;
        private System.Windows.Forms.RadioButton rdb_takeaway;
        private System.Windows.Forms.RadioButton rdb_dinein;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_finalbill;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_kot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DB2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.ComboBox CMB_S;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScardValidate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERVICEFLAG1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Svalidate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Freeze1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAF;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
    }
}