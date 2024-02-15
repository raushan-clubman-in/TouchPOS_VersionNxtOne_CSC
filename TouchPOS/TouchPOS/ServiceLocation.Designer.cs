namespace TouchPOS
{
    partial class ServiceLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceLocation));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_PreBalCheck = new System.Windows.Forms.Button();
            this.Cmd_Transfer = new System.Windows.Forms.Button();
            this.Cmd_BPOS = new System.Windows.Forms.Button();
            this.Cmd_MergeTransfer = new System.Windows.Forms.Button();
            this.Cmd_AddChair = new System.Windows.Forms.Button();
            this.Cmd_Split = new System.Windows.Forms.Button();
            this.Lbl_BusinessDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(191, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Cmd_PreBalCheck);
            this.panel1.Controls.Add(this.Cmd_Transfer);
            this.panel1.Controls.Add(this.Cmd_BPOS);
            this.panel1.Controls.Add(this.Cmd_MergeTransfer);
            this.panel1.Controls.Add(this.Cmd_AddChair);
            this.panel1.Controls.Add(this.Cmd_Split);
            this.panel1.Location = new System.Drawing.Point(422, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 82);
            this.panel1.TabIndex = 5;
            // 
            // Cmd_PreBalCheck
            // 
            this.Cmd_PreBalCheck.BackgroundImage = global::TouchPOS.Properties.Resources.Blue;
            this.Cmd_PreBalCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_PreBalCheck.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_PreBalCheck.ForeColor = System.Drawing.Color.White;
            this.Cmd_PreBalCheck.Location = new System.Drawing.Point(3, 3);
            this.Cmd_PreBalCheck.Name = "Cmd_PreBalCheck";
            this.Cmd_PreBalCheck.Size = new System.Drawing.Size(131, 66);
            this.Cmd_PreBalCheck.TabIndex = 7;
            this.Cmd_PreBalCheck.Text = "Prepaid Balance";
            this.Cmd_PreBalCheck.UseVisualStyleBackColor = true;
            this.Cmd_PreBalCheck.Click += new System.EventHandler(this.Cmd_PreBalCheck_Click);
            // 
            // Cmd_Transfer
            // 
            this.Cmd_Transfer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Transfer.BackgroundImage")));
            this.Cmd_Transfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Transfer.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Transfer.ForeColor = System.Drawing.Color.White;
            this.Cmd_Transfer.Location = new System.Drawing.Point(449, 3);
            this.Cmd_Transfer.Name = "Cmd_Transfer";
            this.Cmd_Transfer.Size = new System.Drawing.Size(138, 66);
            this.Cmd_Transfer.TabIndex = 6;
            this.Cmd_Transfer.Text = "Transfer";
            this.Cmd_Transfer.UseVisualStyleBackColor = true;
            this.Cmd_Transfer.Click += new System.EventHandler(this.Cmd_Transfer_Click);
            // 
            // Cmd_BPOS
            // 
            this.Cmd_BPOS.BackColor = System.Drawing.Color.Red;
            this.Cmd_BPOS.BackgroundImage = global::TouchPOS.Properties.Resources.Red;
            this.Cmd_BPOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_BPOS.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_BPOS.ForeColor = System.Drawing.Color.White;
            this.Cmd_BPOS.Location = new System.Drawing.Point(751, 3);
            this.Cmd_BPOS.Name = "Cmd_BPOS";
            this.Cmd_BPOS.Size = new System.Drawing.Size(158, 66);
            this.Cmd_BPOS.TabIndex = 4;
            this.Cmd_BPOS.Text = "Back To POS";
            this.Cmd_BPOS.UseVisualStyleBackColor = false;
            this.Cmd_BPOS.Click += new System.EventHandler(this.Cmd_BPOS_Click);
            // 
            // Cmd_MergeTransfer
            // 
            this.Cmd_MergeTransfer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_MergeTransfer.BackgroundImage")));
            this.Cmd_MergeTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_MergeTransfer.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_MergeTransfer.ForeColor = System.Drawing.Color.White;
            this.Cmd_MergeTransfer.Location = new System.Drawing.Point(591, 3);
            this.Cmd_MergeTransfer.Name = "Cmd_MergeTransfer";
            this.Cmd_MergeTransfer.Size = new System.Drawing.Size(158, 66);
            this.Cmd_MergeTransfer.TabIndex = 3;
            this.Cmd_MergeTransfer.Text = "Merge";
            this.Cmd_MergeTransfer.UseVisualStyleBackColor = true;
            this.Cmd_MergeTransfer.Click += new System.EventHandler(this.Cmd_MergeTransfer_Click);
            // 
            // Cmd_AddChair
            // 
            this.Cmd_AddChair.BackgroundImage = global::TouchPOS.Properties.Resources.Blue;
            this.Cmd_AddChair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_AddChair.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_AddChair.ForeColor = System.Drawing.Color.White;
            this.Cmd_AddChair.Location = new System.Drawing.Point(138, 3);
            this.Cmd_AddChair.Name = "Cmd_AddChair";
            this.Cmd_AddChair.Size = new System.Drawing.Size(147, 66);
            this.Cmd_AddChair.TabIndex = 0;
            this.Cmd_AddChair.Text = "Add Chair";
            this.Cmd_AddChair.UseVisualStyleBackColor = true;
            this.Cmd_AddChair.Click += new System.EventHandler(this.Cmd_AddChair_Click);
            // 
            // Cmd_Split
            // 
            this.Cmd_Split.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Split.BackgroundImage")));
            this.Cmd_Split.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Split.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Split.ForeColor = System.Drawing.Color.White;
            this.Cmd_Split.Location = new System.Drawing.Point(288, 3);
            this.Cmd_Split.Name = "Cmd_Split";
            this.Cmd_Split.Size = new System.Drawing.Size(158, 66);
            this.Cmd_Split.TabIndex = 2;
            this.Cmd_Split.Text = "Split Order";
            this.Cmd_Split.UseVisualStyleBackColor = true;
            this.Cmd_Split.Click += new System.EventHandler(this.Cmd_Split_Click);
            // 
            // Lbl_BusinessDate
            // 
            this.Lbl_BusinessDate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BusinessDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Lbl_BusinessDate.Location = new System.Drawing.Point(233, 50);
            this.Lbl_BusinessDate.Name = "Lbl_BusinessDate";
            this.Lbl_BusinessDate.Size = new System.Drawing.Size(158, 23);
            this.Lbl_BusinessDate.TabIndex = 7;
            this.Lbl_BusinessDate.Text = "label3";
            this.Lbl_BusinessDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(16, 94);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1331, 659);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1323, 622);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button5.Location = new System.Drawing.Point(1229, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1323, 622);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.Lbl_BusinessDate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(16, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 83);
            this.panel2.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TouchPOS.Properties.Resources.chs;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(8, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 40);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // ServiceLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServiceLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceLocation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ServiceLocation_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cmd_BPOS;
        private System.Windows.Forms.Button Cmd_MergeTransfer;
        private System.Windows.Forms.Button Cmd_Split;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cmd_AddChair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cmd_Transfer;
        private System.Windows.Forms.Label Lbl_BusinessDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Cmd_PreBalCheck;
        private System.Windows.Forms.TextBox textBox1;
    }
}