﻿namespace TouchPOS.MASTER
{
    partial class Tablemater
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
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Txt_TOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_servicelocation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Cmb_freeze = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_Tablecode = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
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
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(653, 37);
            this.label1.TabIndex = 96;
            this.label1.Text = "Table Master";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.Btn_exit);
            this.groupBox1.Location = new System.Drawing.Point(25, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(630, 69);
            this.groupBox1.TabIndex = 97;
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
            this.btn_new.Location = new System.Drawing.Point(15, 7);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(145, 54);
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
            this.btn_edit.Location = new System.Drawing.Point(167, 7);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(145, 54);
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
            this.btn_save.Location = new System.Drawing.Point(318, 7);
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
            this.Btn_exit.Location = new System.Drawing.Point(469, 7);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(145, 54);
            this.Btn_exit.TabIndex = 36;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Txt_TOrder);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmb_servicelocation);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Cmb_freeze);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Txt_Tablecode);
            this.groupBox2.Location = new System.Drawing.Point(20, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 156);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            // 
            // Txt_TOrder
            // 
            this.Txt_TOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_TOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_TOrder.Location = new System.Drawing.Point(287, 120);
            this.Txt_TOrder.MaxLength = 30;
            this.Txt_TOrder.Name = "Txt_TOrder";
            this.Txt_TOrder.Size = new System.Drawing.Size(215, 26);
            this.Txt_TOrder.TabIndex = 35;
            this.Txt_TOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_TOrder_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(81, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 20);
            this.label5.TabIndex = 34;
            this.label5.Text = "Table Order No.";
            // 
            // cmb_servicelocation
            // 
            this.cmb_servicelocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_servicelocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_servicelocation.FormattingEnabled = true;
            this.cmb_servicelocation.Location = new System.Drawing.Point(287, 46);
            this.cmb_servicelocation.Name = "cmb_servicelocation";
            this.cmb_servicelocation.Size = new System.Drawing.Size(215, 28);
            this.cmb_servicelocation.TabIndex = 33;
            this.cmb_servicelocation.SelectedIndexChanged += new System.EventHandler(this.cmb_servicelocation_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(81, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "Freeze";
            // 
            // Cmb_freeze
            // 
            this.Cmb_freeze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_freeze.FormattingEnabled = true;
            this.Cmb_freeze.Items.AddRange(new object[] {
            "NO",
            "YES"});
            this.Cmb_freeze.Location = new System.Drawing.Point(287, 83);
            this.Cmb_freeze.Name = "Cmb_freeze";
            this.Cmb_freeze.Size = new System.Drawing.Size(215, 28);
            this.Cmb_freeze.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Table Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Location Description";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Txt_Tablecode
            // 
            this.Txt_Tablecode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_Tablecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Tablecode.Location = new System.Drawing.Point(287, 11);
            this.Txt_Tablecode.MaxLength = 30;
            this.Txt_Tablecode.Name = "Txt_Tablecode";
            this.Txt_Tablecode.Size = new System.Drawing.Size(215, 26);
            this.Txt_Tablecode.TabIndex = 29;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Name,
            this.Status,
            this.TableOrder});
            this.dataGridView1.Location = new System.Drawing.Point(20, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(588, 152);
            this.dataGridView1.TabIndex = 98;
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
            // Status
            // 
            this.Status.HeaderText = "Freeze";
            this.Status.Name = "Status";
            // 
            // TableOrder
            // 
            this.TableOrder.HeaderText = "TableOrder";
            this.TableOrder.Name = "TableOrder";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::TouchPOS.Properties.Resources._1__6_;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(559, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 37);
            this.button1.TabIndex = 100;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(25, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 376);
            this.panel1.TabIndex = 101;
            // 
            // Tablemater
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(680, 534);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ////this.Name = "Tablemater";
            this.Text = "Tablemater";
            this.Load += new System.EventHandler(this.Tablemater_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Cmb_freeze;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Txt_Tablecode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmb_servicelocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableOrder;
        private System.Windows.Forms.TextBox Txt_TOrder;
        private System.Windows.Forms.Label label5;
    }
}