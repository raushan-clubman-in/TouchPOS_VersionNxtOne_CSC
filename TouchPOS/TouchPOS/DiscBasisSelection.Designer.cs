namespace TouchPOS
{
    partial class DiscBasisSelection
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
            this.Rdb_Bill = new System.Windows.Forms.RadioButton();
            this.Rdb_ItemGroup = new System.Windows.Forms.RadioButton();
            this.Cmd_OK = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmb_DiscCategory = new System.Windows.Forms.ComboBox();
            this.Txt_DiscPerc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Amount = new System.Windows.Forms.TextBox();
            this.Lbl_BillValue = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Rdb_Bill
            // 
            this.Rdb_Bill.AutoSize = true;
            this.Rdb_Bill.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_Bill.Location = new System.Drawing.Point(17, 15);
            this.Rdb_Bill.Name = "Rdb_Bill";
            this.Rdb_Bill.Size = new System.Drawing.Size(105, 30);
            this.Rdb_Bill.TabIndex = 0;
            this.Rdb_Bill.TabStop = true;
            this.Rdb_Bill.Text = "On Bill";
            this.Rdb_Bill.UseVisualStyleBackColor = true;
            // 
            // Rdb_ItemGroup
            // 
            this.Rdb_ItemGroup.AutoSize = true;
            this.Rdb_ItemGroup.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_ItemGroup.Location = new System.Drawing.Point(17, 121);
            this.Rdb_ItemGroup.Name = "Rdb_ItemGroup";
            this.Rdb_ItemGroup.Size = new System.Drawing.Size(217, 30);
            this.Rdb_ItemGroup.TabIndex = 1;
            this.Rdb_ItemGroup.TabStop = true;
            this.Rdb_ItemGroup.Text = "On Item or Group";
            this.Rdb_ItemGroup.UseVisualStyleBackColor = true;
            // 
            // Cmd_OK
            // 
            this.Cmd_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_OK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_OK.Location = new System.Drawing.Point(141, 239);
            this.Cmd_OK.Name = "Cmd_OK";
            this.Cmd_OK.Size = new System.Drawing.Size(129, 60);
            this.Cmd_OK.TabIndex = 20;
            this.Cmd_OK.Text = "OK";
            this.Cmd_OK.UseVisualStyleBackColor = false;
            this.Cmd_OK.Click += new System.EventHandler(this.Cmd_OK_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.BackColor = System.Drawing.Color.Red;
            this.Cmd_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Cancel.Location = new System.Drawing.Point(12, 239);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(123, 60);
            this.Cmd_Cancel.TabIndex = 21;
            this.Cmd_Cancel.Text = "Cancel";
            this.Cmd_Cancel.UseVisualStyleBackColor = false;
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cmb_DiscCategory);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 73);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Discount Category";
            // 
            // Cmb_DiscCategory
            // 
            this.Cmb_DiscCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_DiscCategory.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_DiscCategory.FormattingEnabled = true;
            this.Cmb_DiscCategory.Location = new System.Drawing.Point(6, 29);
            this.Cmb_DiscCategory.Name = "Cmb_DiscCategory";
            this.Cmb_DiscCategory.Size = new System.Drawing.Size(239, 31);
            this.Cmb_DiscCategory.TabIndex = 6;
            // 
            // Txt_DiscPerc
            // 
            this.Txt_DiscPerc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_DiscPerc.Location = new System.Drawing.Point(85, 51);
            this.Txt_DiscPerc.Name = "Txt_DiscPerc";
            this.Txt_DiscPerc.Size = new System.Drawing.Size(104, 26);
            this.Txt_DiscPerc.TabIndex = 23;
            this.Txt_DiscPerc.TextChanged += new System.EventHandler(this.Txt_DiscPerc_TextChanged);
            this.Txt_DiscPerc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_DiscPerc_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "Perc %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "Amount";
            // 
            // Txt_Amount
            // 
            this.Txt_Amount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Amount.Location = new System.Drawing.Point(85, 83);
            this.Txt_Amount.Name = "Txt_Amount";
            this.Txt_Amount.Size = new System.Drawing.Size(104, 26);
            this.Txt_Amount.TabIndex = 25;
            this.Txt_Amount.TextChanged += new System.EventHandler(this.Txt_Amount_TextChanged);
            this.Txt_Amount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Amount_KeyPress);
            // 
            // Lbl_BillValue
            // 
            this.Lbl_BillValue.AutoSize = true;
            this.Lbl_BillValue.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BillValue.Location = new System.Drawing.Point(195, 92);
            this.Lbl_BillValue.Name = "Lbl_BillValue";
            this.Lbl_BillValue.Size = new System.Drawing.Size(39, 13);
            this.Lbl_BillValue.TabIndex = 27;
            this.Lbl_BillValue.Text = "label3";
            // 
            // DiscBasisSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 307);
            this.Controls.Add(this.Lbl_BillValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_Amount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_DiscPerc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cmd_Cancel);
            this.Controls.Add(this.Cmd_OK);
            this.Controls.Add(this.Rdb_ItemGroup);
            this.Controls.Add(this.Rdb_Bill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DiscBasisSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DiscBasisSelection";
            this.Load += new System.EventHandler(this.DiscBasisSelection_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Rdb_Bill;
        private System.Windows.Forms.RadioButton Rdb_ItemGroup;
        private System.Windows.Forms.Button Cmd_OK;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Cmb_DiscCategory;
        private System.Windows.Forms.TextBox Txt_DiscPerc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_Amount;
        private System.Windows.Forms.Label Lbl_BillValue;
    }
}