namespace TouchPOS.MASTER
{
    partial class PosMaster
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_add1 = new System.Windows.Forms.TextBox();
            this.txt_tips1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmb_taxtype3 = new System.Windows.Forms.ComboBox();
            this.cmb_taxtype2 = new System.Windows.Forms.ComboBox();
            this.cmb_taxtype = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Cmb_freeze = new System.Windows.Forms.ComboBox();
            this.CMB_ADD = new System.Windows.Forms.ComboBox();
            this.CMB_TIPS = new System.Windows.Forms.ComboBox();
            this.CMB_SURCHR = new System.Windows.Forms.ComboBox();
            this.cmb_cost = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Txt_gstin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TXT_CATRER = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_surcharge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_posdesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_poscode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_st = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_add = new System.Windows.Forms.MaskedTextBox();
            this.txt_tips = new System.Windows.Forms.MaskedTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.POSCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Posdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cmd_Print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(27, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(852, 68);
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
            this.btn_new.Location = new System.Drawing.Point(17, 7);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(191, 54);
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
            this.btn_edit.Location = new System.Drawing.Point(228, 7);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(191, 54);
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
            this.btn_save.Location = new System.Drawing.Point(441, 7);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(191, 54);
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
            this.Btn_exit.Location = new System.Drawing.Point(649, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(191, 54);
            this.Btn_exit.TabIndex = 36;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(910, 34);
            this.label1.TabIndex = 39;
            this.label1.Text = "Pos Master";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(28, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(851, 448);
            this.groupBox2.TabIndex = 479;
            this.groupBox2.TabStop = false;
            // 
            // txt_add1
            // 
            this.txt_add1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_add1.Location = new System.Drawing.Point(250, 75);
            this.txt_add1.MaxLength = 11;
            this.txt_add1.Name = "txt_add1";
            this.txt_add1.Size = new System.Drawing.Size(96, 26);
            this.txt_add1.TabIndex = 488;
            this.txt_add1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_add1_KeyPress);
            // 
            // txt_tips1
            // 
            this.txt_tips1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tips1.Location = new System.Drawing.Point(250, 40);
            this.txt_tips1.MaxLength = 11;
            this.txt_tips1.Name = "txt_tips1";
            this.txt_tips1.Size = new System.Drawing.Size(96, 26);
            this.txt_tips1.TabIndex = 487;
            this.txt_tips1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_tips1_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(363, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 20);
            this.label17.TabIndex = 486;
            this.label17.Text = "Tax";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(363, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 20);
            this.label16.TabIndex = 485;
            this.label16.Text = "Tax";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(363, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 20);
            this.label15.TabIndex = 484;
            this.label15.Text = "Tax";
            // 
            // cmb_taxtype3
            // 
            this.cmb_taxtype3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taxtype3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_taxtype3.FormattingEnabled = true;
            this.cmb_taxtype3.Location = new System.Drawing.Point(409, 76);
            this.cmb_taxtype3.Name = "cmb_taxtype3";
            this.cmb_taxtype3.Size = new System.Drawing.Size(112, 28);
            this.cmb_taxtype3.TabIndex = 483;
            this.cmb_taxtype3.SelectedIndexChanged += new System.EventHandler(this.cmb_taxtype3_SelectedIndexChanged);
            // 
            // cmb_taxtype2
            // 
            this.cmb_taxtype2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taxtype2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_taxtype2.FormattingEnabled = true;
            this.cmb_taxtype2.Location = new System.Drawing.Point(409, 42);
            this.cmb_taxtype2.Name = "cmb_taxtype2";
            this.cmb_taxtype2.Size = new System.Drawing.Size(112, 28);
            this.cmb_taxtype2.TabIndex = 482;
            this.cmb_taxtype2.SelectedIndexChanged += new System.EventHandler(this.cmb_taxtype2_SelectedIndexChanged);
            // 
            // cmb_taxtype
            // 
            this.cmb_taxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taxtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_taxtype.FormattingEnabled = true;
            this.cmb_taxtype.Location = new System.Drawing.Point(409, 7);
            this.cmb_taxtype.Name = "cmb_taxtype";
            this.cmb_taxtype.Size = new System.Drawing.Size(112, 28);
            this.cmb_taxtype.TabIndex = 481;
            this.cmb_taxtype.SelectedIndexChanged += new System.EventHandler(this.cmb_taxtype_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 20);
            this.label14.TabIndex = 40;
            this.label14.Text = "Freeze";
            // 
            // Cmb_freeze
            // 
            this.Cmb_freeze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_freeze.FormattingEnabled = true;
            this.Cmb_freeze.Items.AddRange(new object[] {
            "NO",
            "YES"});
            this.Cmb_freeze.Location = new System.Drawing.Point(115, 109);
            this.Cmb_freeze.Name = "Cmb_freeze";
            this.Cmb_freeze.Size = new System.Drawing.Size(154, 28);
            this.Cmb_freeze.TabIndex = 39;
            // 
            // CMB_ADD
            // 
            this.CMB_ADD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMB_ADD.FormattingEnabled = true;
            this.CMB_ADD.Location = new System.Drawing.Point(116, 73);
            this.CMB_ADD.Name = "CMB_ADD";
            this.CMB_ADD.Size = new System.Drawing.Size(153, 28);
            this.CMB_ADD.TabIndex = 28;
            this.CMB_ADD.SelectedIndexChanged += new System.EventHandler(this.CMB_ADD_SelectedIndexChanged);
            // 
            // CMB_TIPS
            // 
            this.CMB_TIPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMB_TIPS.FormattingEnabled = true;
            this.CMB_TIPS.Location = new System.Drawing.Point(117, 41);
            this.CMB_TIPS.Name = "CMB_TIPS";
            this.CMB_TIPS.Size = new System.Drawing.Size(153, 28);
            this.CMB_TIPS.TabIndex = 27;
            this.CMB_TIPS.SelectedIndexChanged += new System.EventHandler(this.CMB_TIPS_SelectedIndexChanged);
            // 
            // CMB_SURCHR
            // 
            this.CMB_SURCHR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMB_SURCHR.FormattingEnabled = true;
            this.CMB_SURCHR.Location = new System.Drawing.Point(116, 9);
            this.CMB_SURCHR.Name = "CMB_SURCHR";
            this.CMB_SURCHR.Size = new System.Drawing.Size(153, 28);
            this.CMB_SURCHR.TabIndex = 26;
            this.CMB_SURCHR.SelectedIndexChanged += new System.EventHandler(this.CMB_SURCHR_SelectedIndexChanged);
            // 
            // cmb_cost
            // 
            this.cmb_cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_cost.FormattingEnabled = true;
            this.cmb_cost.Location = new System.Drawing.Point(249, 113);
            this.cmb_cost.Name = "cmb_cost";
            this.cmb_cost.Size = new System.Drawing.Size(273, 28);
            this.cmb_cost.TabIndex = 25;
            this.cmb_cost.SelectedIndexChanged += new System.EventHandler(this.cmb_cost_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 20);
            this.label13.TabIndex = 23;
            this.label13.Text = "Account In";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 20);
            this.label12.TabIndex = 21;
            this.label12.Text = "Account In";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "Account In";
            // 
            // Txt_gstin
            // 
            this.Txt_gstin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_gstin.Location = new System.Drawing.Point(147, 45);
            this.Txt_gstin.MaxLength = 20;
            this.Txt_gstin.Name = "Txt_gstin";
            this.Txt_gstin.Size = new System.Drawing.Size(127, 26);
            this.Txt_gstin.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "Caterer GSTIN";
            // 
            // TXT_CATRER
            // 
            this.TXT_CATRER.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_CATRER.Location = new System.Drawing.Point(148, 6);
            this.TXT_CATRER.MaxLength = 60;
            this.TXT_CATRER.Name = "TXT_CATRER";
            this.TXT_CATRER.Size = new System.Drawing.Size(126, 26);
            this.TXT_CATRER.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Caterer Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Cost Center ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Additonal Charges";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Service Charge/Tips";
            // 
            // txt_surcharge
            // 
            this.txt_surcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_surcharge.Location = new System.Drawing.Point(250, 6);
            this.txt_surcharge.MaxLength = 11;
            this.txt_surcharge.Name = "txt_surcharge";
            this.txt_surcharge.Size = new System.Drawing.Size(96, 26);
            this.txt_surcharge.TabIndex = 8;
            this.txt_surcharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_surcharge_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(236, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Packaging/Serv./Surcharge";
            // 
            // txt_posdesc
            // 
            this.txt_posdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_posdesc.Location = new System.Drawing.Point(338, 41);
            this.txt_posdesc.MaxLength = 60;
            this.txt_posdesc.Name = "txt_posdesc";
            this.txt_posdesc.Size = new System.Drawing.Size(183, 26);
            this.txt_posdesc.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(221, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Description";
            // 
            // txt_poscode
            // 
            this.txt_poscode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_poscode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_poscode.Location = new System.Drawing.Point(125, 41);
            this.txt_poscode.MaxLength = 10;
            this.txt_poscode.Name = "txt_poscode";
            this.txt_poscode.ReadOnly = true;
            this.txt_poscode.Size = new System.Drawing.Size(90, 26);
            this.txt_poscode.TabIndex = 3;
            this.txt_poscode.TextChanged += new System.EventHandler(this.txt_poscode_TextChanged);
            this.txt_poscode.Validated += new System.EventHandler(this.txt_poscode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "POS Code";
            // 
            // cmb_st
            // 
            this.cmb_st.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_st.FormattingEnabled = true;
            this.cmb_st.Items.AddRange(new object[] {
            "FACILITY",
            "POS"});
            this.cmb_st.Location = new System.Drawing.Point(125, 7);
            this.cmb_st.Name = "cmb_st";
            this.cmb_st.Size = new System.Drawing.Size(90, 28);
            this.cmb_st.TabIndex = 1;
            this.cmb_st.SelectedIndexChanged += new System.EventHandler(this.cmb_st_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Type";
            // 
            // txt_add
            // 
            this.txt_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_add.Location = new System.Drawing.Point(44, 603);
            this.txt_add.Name = "txt_add";
            this.txt_add.Size = new System.Drawing.Size(23, 29);
            this.txt_add.TabIndex = 12;
            this.txt_add.Visible = false;
            this.txt_add.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_add_KeyPress);
            // 
            // txt_tips
            // 
            this.txt_tips.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tips.Location = new System.Drawing.Point(73, 603);
            this.txt_tips.Name = "txt_tips";
            this.txt_tips.Size = new System.Drawing.Size(23, 29);
            this.txt_tips.TabIndex = 10;
            this.txt_tips.Visible = false;
            this.txt_tips.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_tips_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.POSCODE,
            this.Posdesc,
            this.Status,
            this.Type});
            this.dataGridView1.Location = new System.Drawing.Point(15, 263);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(736, 170);
            this.dataGridView1.TabIndex = 480;
            // 
            // POSCODE
            // 
            this.POSCODE.HeaderText = "Pos Code";
            this.POSCODE.Name = "POSCODE";
            // 
            // Posdesc
            // 
            this.Posdesc.HeaderText = "Pos Name";
            this.Posdesc.Name = "Posdesc";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Cmd_Print
            // 
            this.Cmd_Print.BackgroundImage = global::TouchPOS.Properties.Resources.printFile;
            this.Cmd_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Print.Location = new System.Drawing.Point(791, 524);
            this.Cmd_Print.Name = "Cmd_Print";
            this.Cmd_Print.Size = new System.Drawing.Size(70, 50);
            this.Cmd_Print.TabIndex = 481;
            this.Cmd_Print.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_posdesc);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_poscode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmb_st);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(14, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 81);
            this.panel1.TabIndex = 489;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Txt_gstin);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.TXT_CATRER);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(552, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(282, 81);
            this.panel2.TabIndex = 490;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txt_add1);
            this.panel3.Controls.Add(this.txt_tips1);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.cmb_taxtype3);
            this.panel3.Controls.Add(this.cmb_taxtype2);
            this.panel3.Controls.Add(this.cmb_taxtype);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cmb_cost);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txt_surcharge);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(14, 107);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(531, 150);
            this.panel3.TabIndex = 491;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.Cmb_freeze);
            this.panel4.Controls.Add(this.CMB_ADD);
            this.panel4.Controls.Add(this.CMB_TIPS);
            this.panel4.Controls.Add(this.CMB_SURCHR);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Location = new System.Drawing.Point(553, 107);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(280, 150);
            this.panel4.TabIndex = 492;
            // 
            // PosMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(913, 615);
            this.Controls.Add(this.Cmd_Print);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_tips);
            this.Controls.Add(this.txt_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PosMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PettycashMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_poscode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_st;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_posdesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_surcharge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txt_tips;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txt_add;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Txt_gstin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TXT_CATRER;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_cost;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox CMB_ADD;
        private System.Windows.Forms.ComboBox CMB_TIPS;
        private System.Windows.Forms.ComboBox CMB_SURCHR;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox Cmb_freeze;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Posdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.ComboBox cmb_taxtype3;
        private System.Windows.Forms.ComboBox cmb_taxtype2;
        private System.Windows.Forms.ComboBox cmb_taxtype;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_add1;
        private System.Windows.Forms.TextBox txt_tips1;
        private System.Windows.Forms.Button Cmd_Print;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}