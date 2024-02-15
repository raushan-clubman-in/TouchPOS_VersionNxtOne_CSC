namespace TouchPOS
{
    partial class PrePaidCardTagging
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
            this.Cmd_OK = new System.Windows.Forms.Button();
            this.Txt_Cardid = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_CardCode = new System.Windows.Forms.Label();
            this.Lbl_CardHolderName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lbl_CardBal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cmd_OK
            // 
            this.Cmd_OK.BackgroundImage = global::TouchPOS.Properties.Resources.White;
            this.Cmd_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_OK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_OK.Location = new System.Drawing.Point(262, 9);
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
            this.Txt_Cardid.Location = new System.Drawing.Point(13, 13);
            this.Txt_Cardid.MaxLength = 30;
            this.Txt_Cardid.Name = "Txt_Cardid";
            this.Txt_Cardid.PasswordChar = '*';
            this.Txt_Cardid.Size = new System.Drawing.Size(244, 29);
            this.Txt_Cardid.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Lbl_CardBal);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Cmd_OK);
            this.panel1.Controls.Add(this.Txt_Cardid);
            this.panel1.Location = new System.Drawing.Point(17, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 244);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(423, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "PrePaid Card Tagging";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_CardCode
            // 
            this.Lbl_CardCode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardCode.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardCode.Location = new System.Drawing.Point(12, 38);
            this.Lbl_CardCode.Name = "Lbl_CardCode";
            this.Lbl_CardCode.Size = new System.Drawing.Size(307, 23);
            this.Lbl_CardCode.TabIndex = 4;
            this.Lbl_CardCode.Text = "Name";
            // 
            // Lbl_CardHolderName
            // 
            this.Lbl_CardHolderName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardHolderName.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardHolderName.Location = new System.Drawing.Point(14, 96);
            this.Lbl_CardHolderName.Name = "Lbl_CardHolderName";
            this.Lbl_CardHolderName.Size = new System.Drawing.Size(307, 23);
            this.Lbl_CardHolderName.TabIndex = 5;
            this.Lbl_CardHolderName.Text = "Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Lbl_CardHolderName);
            this.panel2.Controls.Add(this.Lbl_CardCode);
            this.panel2.Location = new System.Drawing.Point(13, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 135);
            this.panel2.TabIndex = 8;
            // 
            // Lbl_CardBal
            // 
            this.Lbl_CardBal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardBal.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CardBal.Location = new System.Drawing.Point(15, 201);
            this.Lbl_CardBal.Name = "Lbl_CardBal";
            this.Lbl_CardBal.Size = new System.Drawing.Size(380, 23);
            this.Lbl_CardBal.TabIndex = 9;
            this.Lbl_CardBal.Text = "Name";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Cmd_Clear);
            this.panel3.Controls.Add(this.Cmd_Cancel);
            this.panel3.Controls.Add(this.Cmd_Processed);
            this.panel3.Location = new System.Drawing.Point(17, 314);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(409, 66);
            this.panel3.TabIndex = 29;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.BackColor = System.Drawing.Color.Silver;
            this.Cmd_Clear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Clear.Location = new System.Drawing.Point(139, 7);
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
            this.Cmd_Processed.Location = new System.Drawing.Point(271, 7);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(126, 50);
            this.Cmd_Processed.TabIndex = 25;
            this.Cmd_Processed.Text = "OK";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Holder Code :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Holder Name :";
            // 
            // PrePaidCardTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(442, 399);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrePaidCardTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrePaidCardTagging";
            this.Load += new System.EventHandler(this.PrePaidCardTagging_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cmd_OK;
        private System.Windows.Forms.TextBox Txt_Cardid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Lbl_CardHolderName;
        private System.Windows.Forms.Label Lbl_CardCode;
        private System.Windows.Forms.Label Lbl_CardBal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Cmd_Clear;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.Button Cmd_Processed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}