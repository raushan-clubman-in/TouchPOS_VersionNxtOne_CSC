namespace TouchPOS.MASTER
{
    partial class ItemPosAccountTagging
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PosCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcountDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sez_ChargeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cmb_PosLocation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Cmb_OnSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmd_Search = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(827, 43);
            this.label1.TabIndex = 99;
            this.label1.Text = "Item Account / Tax Setup";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(24, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(807, 63);
            this.groupBox1.TabIndex = 100;
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
            this.btn_new.Location = new System.Drawing.Point(53, 3);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(145, 54);
            this.btn_new.TabIndex = 37;
            this.btn_new.Text = "Refresh";
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_save.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(323, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(145, 54);
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
            this.Btn_exit.Location = new System.Drawing.Point(615, 3);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(145, 54);
            this.Btn_exit.TabIndex = 36;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(24, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(809, 327);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PosCode,
            this.PosDesc,
            this.ItemCode,
            this.ItemDesc,
            this.ChargeType,
            this.AcountCode,
            this.AcountDesc,
            this.Sez_ChargeType});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(803, 308);
            this.dataGridView1.TabIndex = 102;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // PosCode
            // 
            this.PosCode.HeaderText = "PosCode";
            this.PosCode.Name = "PosCode";
            this.PosCode.ReadOnly = true;
            this.PosCode.Visible = false;
            // 
            // PosDesc
            // 
            this.PosDesc.HeaderText = "PosDesc";
            this.PosDesc.Name = "PosDesc";
            this.PosDesc.ReadOnly = true;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.Visible = false;
            // 
            // ItemDesc
            // 
            this.ItemDesc.HeaderText = "ItemDesc";
            this.ItemDesc.Name = "ItemDesc";
            this.ItemDesc.ReadOnly = true;
            // 
            // ChargeType
            // 
            this.ChargeType.HeaderText = "ChargeType";
            this.ChargeType.Name = "ChargeType";
            // 
            // AcountCode
            // 
            this.AcountCode.HeaderText = "AcountCode";
            this.AcountCode.Name = "AcountCode";
            this.AcountCode.Visible = false;
            // 
            // AcountDesc
            // 
            this.AcountDesc.HeaderText = "AcountDesc";
            this.AcountDesc.Name = "AcountDesc";
            // 
            // Sez_ChargeType
            // 
            this.Sez_ChargeType.HeaderText = "Sez_ChargeType";
            this.Sez_ChargeType.Name = "Sez_ChargeType";
            // 
            // Cmb_PosLocation
            // 
            this.Cmb_PosLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PosLocation.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_PosLocation.FormattingEnabled = true;
            this.Cmb_PosLocation.Location = new System.Drawing.Point(103, 9);
            this.Cmb_PosLocation.Name = "Cmb_PosLocation";
            this.Cmb_PosLocation.Size = new System.Drawing.Size(190, 27);
            this.Cmb_PosLocation.TabIndex = 104;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(58, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 19);
            this.label7.TabIndex = 103;
            this.label7.Text = "POS";
            // 
            // Cmb_OnSelect
            // 
            this.Cmb_OnSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_OnSelect.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_OnSelect.FormattingEnabled = true;
            this.Cmb_OnSelect.Items.AddRange(new object[] {
            "CATEGORY",
            "GROUP",
            "SUBGROUP"});
            this.Cmb_OnSelect.Location = new System.Drawing.Point(378, 9);
            this.Cmb_OnSelect.Name = "Cmb_OnSelect";
            this.Cmb_OnSelect.Size = new System.Drawing.Size(231, 27);
            this.Cmb_OnSelect.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(299, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 105;
            this.label2.Text = "OnSelect";
            // 
            // Cmd_Search
            // 
            this.Cmd_Search.BackColor = System.Drawing.Color.Transparent;
            this.Cmd_Search.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Search.Image = global::TouchPOS.Properties.Resources._1__6_;
            this.Cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_Search.Location = new System.Drawing.Point(615, 4);
            this.Cmd_Search.Name = "Cmd_Search";
            this.Cmd_Search.Size = new System.Drawing.Size(127, 34);
            this.Cmd_Search.TabIndex = 107;
            this.Cmd_Search.Text = "Search";
            this.Cmd_Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Search.UseVisualStyleBackColor = false;
            this.Cmd_Search.Click += new System.EventHandler(this.Cmd_Search_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Cmd_Search);
            this.panel1.Controls.Add(this.Cmb_OnSelect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Cmb_PosLocation);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(23, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 45);
            this.panel1.TabIndex = 108;
            // 
            // ItemPosAccountTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(861, 539);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemPosAccountTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ItemPosAccountTagging";
            this.Load += new System.EventHandler(this.ItemPosAccountTagging_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox Cmb_PosLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Cmb_OnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cmd_Search;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChargeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcountDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sez_ChargeType;
        private System.Windows.Forms.Panel panel1;
    }
}