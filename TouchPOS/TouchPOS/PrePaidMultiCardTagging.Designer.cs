namespace TouchPOS
{
    partial class PrePaidMultiCardTagging
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lbl_Balance = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Lbl_BillAmount = new System.Windows.Forms.Label();
            this.Lbl_CardBal = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_CardHolderName = new System.Windows.Forms.Label();
            this.Lbl_CardCode = new System.Windows.Forms.Label();
            this.Cmd_OK = new System.Windows.Forms.Button();
            this.Txt_Cardid = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Cmd_RemoveRow = new System.Windows.Forms.Button();
            this.Cmd_AddRow = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(485, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "PrePaid Card Tagging";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Lbl_Balance);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Lbl_BillAmount);
            this.panel1.Controls.Add(this.Lbl_CardBal);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Cmd_OK);
            this.panel1.Controls.Add(this.Txt_Cardid);
            this.panel1.Location = new System.Drawing.Point(17, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 222);
            this.panel1.TabIndex = 11;
            // 
            // Lbl_Balance
            // 
            this.Lbl_Balance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Balance.ForeColor = System.Drawing.Color.Red;
            this.Lbl_Balance.Location = new System.Drawing.Point(382, 189);
            this.Lbl_Balance.Name = "Lbl_Balance";
            this.Lbl_Balance.Size = new System.Drawing.Size(65, 23);
            this.Lbl_Balance.TabIndex = 13;
            this.Lbl_Balance.Text = "0";
            this.Lbl_Balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(314, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Balance";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(16, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bill Amount";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_BillAmount
            // 
            this.Lbl_BillAmount.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BillAmount.ForeColor = System.Drawing.Color.Red;
            this.Lbl_BillAmount.Location = new System.Drawing.Point(112, 189);
            this.Lbl_BillAmount.Name = "Lbl_BillAmount";
            this.Lbl_BillAmount.Size = new System.Drawing.Size(90, 23);
            this.Lbl_BillAmount.TabIndex = 10;
            this.Lbl_BillAmount.Text = "0";
            this.Lbl_BillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_CardBal
            // 
            this.Lbl_CardBal.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardBal.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardBal.Location = new System.Drawing.Point(112, 169);
            this.Lbl_CardBal.Name = "Lbl_CardBal";
            this.Lbl_CardBal.Size = new System.Drawing.Size(123, 23);
            this.Lbl_CardBal.TabIndex = 9;
            this.Lbl_CardBal.Text = "0";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Lbl_CardHolderName);
            this.panel2.Controls.Add(this.Lbl_CardCode);
            this.panel2.Location = new System.Drawing.Point(13, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(443, 111);
            this.panel2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Holder Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Holder Code :";
            // 
            // Lbl_CardHolderName
            // 
            this.Lbl_CardHolderName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardHolderName.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardHolderName.Location = new System.Drawing.Point(14, 77);
            this.Lbl_CardHolderName.Name = "Lbl_CardHolderName";
            this.Lbl_CardHolderName.Size = new System.Drawing.Size(307, 23);
            this.Lbl_CardHolderName.TabIndex = 5;
            this.Lbl_CardHolderName.Text = "Name";
            // 
            // Lbl_CardCode
            // 
            this.Lbl_CardCode.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardCode.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardCode.Location = new System.Drawing.Point(12, 27);
            this.Lbl_CardCode.Name = "Lbl_CardCode";
            this.Lbl_CardCode.Size = new System.Drawing.Size(307, 23);
            this.Lbl_CardCode.TabIndex = 4;
            this.Lbl_CardCode.Text = "Name";
            // 
            // Cmd_OK
            // 
            this.Cmd_OK.BackgroundImage = global::TouchPOS.Properties.Resources.White;
            this.Cmd_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_OK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_OK.Location = new System.Drawing.Point(262, 4);
            this.Cmd_OK.Name = "Cmd_OK";
            this.Cmd_OK.Size = new System.Drawing.Size(94, 39);
            this.Cmd_OK.TabIndex = 7;
            this.Cmd_OK.Text = "Search";
            this.Cmd_OK.UseVisualStyleBackColor = true;
            this.Cmd_OK.Click += new System.EventHandler(this.Cmd_OK_Click);
            // 
            // Txt_Cardid
            // 
            this.Txt_Cardid.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Cardid.Location = new System.Drawing.Point(13, 8);
            this.Txt_Cardid.MaxLength = 30;
            this.Txt_Cardid.Name = "Txt_Cardid";
            this.Txt_Cardid.PasswordChar = '*';
            this.Txt_Cardid.Size = new System.Drawing.Size(244, 29);
            this.Txt_Cardid.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Cmd_RemoveRow);
            this.panel3.Controls.Add(this.Cmd_AddRow);
            this.panel3.Location = new System.Drawing.Point(17, 286);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(469, 44);
            this.panel3.TabIndex = 106;
            // 
            // Cmd_RemoveRow
            // 
            this.Cmd_RemoveRow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cmd_RemoveRow.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_RemoveRow.Location = new System.Drawing.Point(224, 6);
            this.Cmd_RemoveRow.Name = "Cmd_RemoveRow";
            this.Cmd_RemoveRow.Size = new System.Drawing.Size(86, 30);
            this.Cmd_RemoveRow.TabIndex = 104;
            this.Cmd_RemoveRow.Text = "Remove Row";
            this.Cmd_RemoveRow.UseVisualStyleBackColor = true;
            this.Cmd_RemoveRow.Click += new System.EventHandler(this.Cmd_RemoveRow_Click);
            // 
            // Cmd_AddRow
            // 
            this.Cmd_AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cmd_AddRow.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_AddRow.Location = new System.Drawing.Point(140, 6);
            this.Cmd_AddRow.Name = "Cmd_AddRow";
            this.Cmd_AddRow.Size = new System.Drawing.Size(86, 30);
            this.Cmd_AddRow.TabIndex = 103;
            this.Cmd_AddRow.Text = "Add Row";
            this.Cmd_AddRow.UseVisualStyleBackColor = true;
            this.Cmd_AddRow.Click += new System.EventHandler(this.Cmd_AddRow_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Location = new System.Drawing.Point(18, 337);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(469, 117);
            this.panel4.TabIndex = 107;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardID,
            this.CardCode,
            this.HolderName,
            this.Amount});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(467, 115);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.Cmd_Clear);
            this.panel5.Controls.Add(this.Cmd_Cancel);
            this.panel5.Controls.Add(this.Cmd_Processed);
            this.panel5.Location = new System.Drawing.Point(16, 461);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(469, 66);
            this.panel5.TabIndex = 108;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.BackColor = System.Drawing.Color.Silver;
            this.Cmd_Clear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Clear.Location = new System.Drawing.Point(172, 7);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(126, 50);
            this.Cmd_Clear.TabIndex = 27;
            this.Cmd_Clear.Text = "Clear";
            this.Cmd_Clear.UseVisualStyleBackColor = false;
            this.Cmd_Clear.Click += new System.EventHandler(this.Cmd_Clear_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.BackColor = System.Drawing.Color.Red;
            this.Cmd_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Cancel.Location = new System.Drawing.Point(7, 7);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(126, 50);
            this.Cmd_Cancel.TabIndex = 26;
            this.Cmd_Cancel.Text = "Cancel";
            this.Cmd_Cancel.UseVisualStyleBackColor = false;
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // Cmd_Processed
            // 
            this.Cmd_Processed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Processed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Processed.Location = new System.Drawing.Point(328, 7);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(126, 50);
            this.Cmd_Processed.TabIndex = 25;
            this.Cmd_Processed.Text = "OK";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(14, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 23);
            this.label6.TabIndex = 14;
            this.label6.Text = "Card Balance";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CardID
            // 
            this.CardID.HeaderText = "CardID";
            this.CardID.Name = "CardID";
            this.CardID.ReadOnly = true;
            this.CardID.Visible = false;
            // 
            // CardCode
            // 
            this.CardCode.HeaderText = "CardCode";
            this.CardCode.Name = "CardCode";
            this.CardCode.ReadOnly = true;
            // 
            // HolderName
            // 
            this.HolderName.HeaderText = "HolderName";
            this.HolderName.Name = "HolderName";
            this.HolderName.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // PrePaidMultiCardTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(507, 544);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrePaidMultiCardTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrePaidMultiCardTagging";
            this.Load += new System.EventHandler(this.PrePaidMultiCardTagging_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Lbl_BillAmount;
        private System.Windows.Forms.Label Lbl_CardBal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lbl_CardHolderName;
        private System.Windows.Forms.Label Lbl_CardCode;
        private System.Windows.Forms.Button Cmd_OK;
        private System.Windows.Forms.TextBox Txt_Cardid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Cmd_RemoveRow;
        private System.Windows.Forms.Button Cmd_AddRow;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button Cmd_Clear;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.Button Cmd_Processed;
        private System.Windows.Forms.Label Lbl_Balance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}