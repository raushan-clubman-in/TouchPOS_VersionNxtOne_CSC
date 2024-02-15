namespace TouchPOS.MASTER
{
    partial class ItemModifierTagging
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_AddRow = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Lbl_ItemName = new System.Windows.Forms.Label();
            this.Lbl_ItemCode = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Chk_SearchByCode = new System.Windows.Forms.CheckBox();
            this.Txt_Item = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Modifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_new = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.Cmd_DeleteRow = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Cmd_DeleteRow);
            this.panel1.Controls.Add(this.Cmd_AddRow);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(20, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 397);
            this.panel1.TabIndex = 0;
            // 
            // Cmd_AddRow
            // 
            this.Cmd_AddRow.BackgroundImage = global::TouchPOS.Properties.Resources.AddSign;
            this.Cmd_AddRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_AddRow.Location = new System.Drawing.Point(478, 127);
            this.Cmd_AddRow.Name = "Cmd_AddRow";
            this.Cmd_AddRow.Size = new System.Drawing.Size(46, 112);
            this.Cmd_AddRow.TabIndex = 21;
            this.Cmd_AddRow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_AddRow.UseVisualStyleBackColor = true;
            this.Cmd_AddRow.Click += new System.EventHandler(this.Cmd_AddRow_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Lbl_ItemName);
            this.panel3.Controls.Add(this.Lbl_ItemCode);
            this.panel3.Location = new System.Drawing.Point(16, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(508, 45);
            this.panel3.TabIndex = 20;
            // 
            // Lbl_ItemName
            // 
            this.Lbl_ItemName.AutoSize = true;
            this.Lbl_ItemName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ItemName.Location = new System.Drawing.Point(14, 12);
            this.Lbl_ItemName.Name = "Lbl_ItemName";
            this.Lbl_ItemName.Size = new System.Drawing.Size(95, 22);
            this.Lbl_ItemName.TabIndex = 5;
            this.Lbl_ItemName.Text = "ItemName";
            this.Lbl_ItemName.Click += new System.EventHandler(this.Lbl_ItemName_Click);
            // 
            // Lbl_ItemCode
            // 
            this.Lbl_ItemCode.AutoSize = true;
            this.Lbl_ItemCode.Location = new System.Drawing.Point(444, 18);
            this.Lbl_ItemCode.Name = "Lbl_ItemCode";
            this.Lbl_ItemCode.Size = new System.Drawing.Size(52, 13);
            this.Lbl_ItemCode.TabIndex = 4;
            this.Lbl_ItemCode.Text = "ItemCode";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Chk_SearchByCode);
            this.panel2.Controls.Add(this.Txt_Item);
            this.panel2.Location = new System.Drawing.Point(16, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(509, 48);
            this.panel2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 21);
            this.label1.TabIndex = 18;
            this.label1.Text = "Search Item";
            // 
            // Chk_SearchByCode
            // 
            this.Chk_SearchByCode.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SearchByCode.Location = new System.Drawing.Point(348, 11);
            this.Chk_SearchByCode.Name = "Chk_SearchByCode";
            this.Chk_SearchByCode.Size = new System.Drawing.Size(126, 30);
            this.Chk_SearchByCode.TabIndex = 16;
            this.Chk_SearchByCode.Text = "Search By Code";
            this.Chk_SearchByCode.UseVisualStyleBackColor = true;
            this.Chk_SearchByCode.CheckedChanged += new System.EventHandler(this.Chk_SearchByCode_CheckedChanged);
            // 
            // Txt_Item
            // 
            this.Txt_Item.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Item.Location = new System.Drawing.Point(119, 11);
            this.Txt_Item.Name = "Txt_Item";
            this.Txt_Item.Size = new System.Drawing.Size(220, 29);
            this.Txt_Item.TabIndex = 3;
            this.Txt_Item.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Item_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modifier});
            this.dataGridView1.Location = new System.Drawing.Point(17, 127);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(455, 227);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Modifier
            // 
            this.Modifier.HeaderText = "Modifier";
            this.Modifier.Name = "Modifier";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btn_new);
            this.panel4.Controls.Add(this.Btn_exit);
            this.panel4.Controls.Add(this.btn_save);
            this.panel4.Location = new System.Drawing.Point(20, 66);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(542, 66);
            this.panel4.TabIndex = 104;
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_new.BackgroundImage = global::TouchPOS.Properties.Resources.BluebuttonMaster;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.ForeColor = System.Drawing.Color.White;
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(10, 5);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(145, 54);
            this.btn_new.TabIndex = 37;
            this.btn_new.Text = "Clear";
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.BackColor = System.Drawing.Color.Red;
            this.Btn_exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_exit.ForeColor = System.Drawing.Color.White;
            this.Btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_exit.Location = new System.Drawing.Point(388, 5);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(145, 54);
            this.Btn_exit.TabIndex = 36;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = false;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_save.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(206, 5);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(145, 54);
            this.btn_save.TabIndex = 35;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Cmd_DeleteRow
            // 
            this.Cmd_DeleteRow.BackgroundImage = global::TouchPOS.Properties.Resources.LessSign;
            this.Cmd_DeleteRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_DeleteRow.Location = new System.Drawing.Point(478, 242);
            this.Cmd_DeleteRow.Name = "Cmd_DeleteRow";
            this.Cmd_DeleteRow.Size = new System.Drawing.Size(46, 112);
            this.Cmd_DeleteRow.TabIndex = 22;
            this.Cmd_DeleteRow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_DeleteRow.UseVisualStyleBackColor = true;
            this.Cmd_DeleteRow.Click += new System.EventHandler(this.Cmd_DeleteRow_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "For Add Row Press Button (+) and for delete Row Press Button (-)";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(562, 45);
            this.label3.TabIndex = 105;
            this.label3.Text = "Item Modifier Tagging";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemModifierTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(585, 563);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemModifierTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemModifierTagging";
            this.Load += new System.EventHandler(this.ItemModifierTagging_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Lbl_ItemName;
        private System.Windows.Forms.Label Lbl_ItemCode;
        private System.Windows.Forms.TextBox Txt_Item;
        private System.Windows.Forms.CheckBox Chk_SearchByCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button Btn_exit;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button Cmd_AddRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modifier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cmd_DeleteRow;
        private System.Windows.Forms.Label label3;
    }
}