namespace TouchPOS
{
    partial class SmartCardTagging
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
            this.Cmd_OK = new System.Windows.Forms.Button();
            this.Txt_Cardid = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Lbl_MCode = new System.Windows.Forms.Label();
            this.Lbl_Mname = new System.Windows.Forms.Label();
            this.Lbl_CardCode = new System.Windows.Forms.Label();
            this.Lbl_CardHolderName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lbl_Address = new System.Windows.Forms.Label();
            this.Lbl_CardBal = new System.Windows.Forms.Label();
            this.Lbl_OutStanding = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Cmd_Clear = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "SmartCard Tagging";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmd_OK
            // 
            this.Cmd_OK.BackgroundImage = global::TouchPOS.Properties.Resources.White;
            this.Cmd_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_OK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_OK.Location = new System.Drawing.Point(309, 48);
            this.Cmd_OK.Name = "Cmd_OK";
            this.Cmd_OK.Size = new System.Drawing.Size(111, 44);
            this.Cmd_OK.TabIndex = 7;
            this.Cmd_OK.Text = "Read Card";
            this.Cmd_OK.UseVisualStyleBackColor = true;
            this.Cmd_OK.Click += new System.EventHandler(this.Cmd_OK_Click);
            // 
            // Txt_Cardid
            // 
            this.Txt_Cardid.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Cardid.Location = new System.Drawing.Point(12, 53);
            this.Txt_Cardid.Multiline = true;
            this.Txt_Cardid.Name = "Txt_Cardid";
            this.Txt_Cardid.PasswordChar = '*';
            this.Txt_Cardid.Size = new System.Drawing.Size(294, 37);
            this.Txt_Cardid.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.Lbl_CardBal);
            this.groupBox2.Controls.Add(this.Lbl_OutStanding);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(14, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 326);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Lbl_MCode);
            this.groupBox3.Controls.Add(this.Lbl_Mname);
            this.groupBox3.Controls.Add(this.Lbl_CardCode);
            this.groupBox3.Controls.Add(this.Lbl_CardHolderName);
            this.groupBox3.Location = new System.Drawing.Point(6, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 148);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // Lbl_MCode
            // 
            this.Lbl_MCode.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_MCode.ForeColor = System.Drawing.Color.White;
            this.Lbl_MCode.Location = new System.Drawing.Point(8, 16);
            this.Lbl_MCode.Name = "Lbl_MCode";
            this.Lbl_MCode.Size = new System.Drawing.Size(161, 23);
            this.Lbl_MCode.TabIndex = 1;
            // 
            // Lbl_Mname
            // 
            this.Lbl_Mname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Mname.ForeColor = System.Drawing.Color.White;
            this.Lbl_Mname.Location = new System.Drawing.Point(6, 48);
            this.Lbl_Mname.Name = "Lbl_Mname";
            this.Lbl_Mname.Size = new System.Drawing.Size(214, 23);
            this.Lbl_Mname.TabIndex = 2;
            this.Lbl_Mname.Text = "Name";
            // 
            // Lbl_CardCode
            // 
            this.Lbl_CardCode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardCode.ForeColor = System.Drawing.Color.White;
            this.Lbl_CardCode.Location = new System.Drawing.Point(6, 82);
            this.Lbl_CardCode.Name = "Lbl_CardCode";
            this.Lbl_CardCode.Size = new System.Drawing.Size(214, 23);
            this.Lbl_CardCode.TabIndex = 4;
            this.Lbl_CardCode.Text = "Name";
            // 
            // Lbl_CardHolderName
            // 
            this.Lbl_CardHolderName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardHolderName.ForeColor = System.Drawing.Color.White;
            this.Lbl_CardHolderName.Location = new System.Drawing.Point(6, 115);
            this.Lbl_CardHolderName.Name = "Lbl_CardHolderName";
            this.Lbl_CardHolderName.Size = new System.Drawing.Size(214, 23);
            this.Lbl_CardHolderName.TabIndex = 5;
            this.Lbl_CardHolderName.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lbl_Address);
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(5, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 115);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // Lbl_Address
            // 
            this.Lbl_Address.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Address.ForeColor = System.Drawing.Color.White;
            this.Lbl_Address.Location = new System.Drawing.Point(7, 14);
            this.Lbl_Address.Name = "Lbl_Address";
            this.Lbl_Address.Size = new System.Drawing.Size(365, 90);
            this.Lbl_Address.TabIndex = 3;
            this.Lbl_Address.Text = "Name";
            // 
            // Lbl_CardBal
            // 
            this.Lbl_CardBal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_CardBal.ForeColor = System.Drawing.Color.White;
            this.Lbl_CardBal.Location = new System.Drawing.Point(198, 289);
            this.Lbl_CardBal.Name = "Lbl_CardBal";
            this.Lbl_CardBal.Size = new System.Drawing.Size(189, 23);
            this.Lbl_CardBal.TabIndex = 7;
            this.Lbl_CardBal.Text = "Name";
            // 
            // Lbl_OutStanding
            // 
            this.Lbl_OutStanding.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_OutStanding.ForeColor = System.Drawing.Color.Yellow;
            this.Lbl_OutStanding.Location = new System.Drawing.Point(7, 289);
            this.Lbl_OutStanding.Name = "Lbl_OutStanding";
            this.Lbl_OutStanding.Size = new System.Drawing.Size(188, 23);
            this.Lbl_OutStanding.TabIndex = 6;
            this.Lbl_OutStanding.Text = "Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(262, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.BackColor = System.Drawing.Color.Silver;
            this.Cmd_Clear.BackgroundImage = global::TouchPOS.Properties.Resources.White;
            this.Cmd_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Clear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Clear.Location = new System.Drawing.Point(146, 426);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(138, 69);
            this.Cmd_Clear.TabIndex = 13;
            this.Cmd_Clear.Text = "Clear";
            this.Cmd_Clear.UseVisualStyleBackColor = false;
            this.Cmd_Clear.Click += new System.EventHandler(this.Cmd_Clear_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.BackColor = System.Drawing.Color.Red;
            this.Cmd_Cancel.BackgroundImage = global::TouchPOS.Properties.Resources.Red;
            this.Cmd_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Cancel.Location = new System.Drawing.Point(10, 426);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(126, 69);
            this.Cmd_Cancel.TabIndex = 12;
            this.Cmd_Cancel.Text = "Cancel";
            this.Cmd_Cancel.UseVisualStyleBackColor = false;
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // Cmd_Processed
            // 
            this.Cmd_Processed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Processed.BackgroundImage = global::TouchPOS.Properties.Resources.Green;
            this.Cmd_Processed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Processed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Processed.Location = new System.Drawing.Point(292, 426);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(128, 69);
            this.Cmd_Processed.TabIndex = 11;
            this.Cmd_Processed.Text = "OK";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // SmartCardTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(430, 504);
            this.Controls.Add(this.Cmd_Clear);
            this.Controls.Add(this.Cmd_Cancel);
            this.Controls.Add(this.Cmd_Processed);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cmd_OK);
            this.Controls.Add(this.Txt_Cardid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SmartCardTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SmartCardTagging";
            this.Load += new System.EventHandler(this.SmartCardTagging_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cmd_OK;
        private System.Windows.Forms.TextBox Txt_Cardid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label Lbl_MCode;
        private System.Windows.Forms.Label Lbl_Mname;
        private System.Windows.Forms.Label Lbl_CardCode;
        private System.Windows.Forms.Label Lbl_CardHolderName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Lbl_Address;
        private System.Windows.Forms.Label Lbl_CardBal;
        private System.Windows.Forms.Label Lbl_OutStanding;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Cmd_Clear;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.Button Cmd_Processed;
    }
}