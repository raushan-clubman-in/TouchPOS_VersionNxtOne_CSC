namespace TouchPOS
{
    partial class TransferTable
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
            this.FromListBox = new System.Windows.Forms.ListBox();
            this.ToListBox = new System.Windows.Forms.ListBox();
            this.Cmd_Close = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.Lbl_Mname = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_BusinessDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1317, 40);
            this.label1.TabIndex = 3;
            this.label1.Text = "Transfer Table";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FromListBox
            // 
            this.FromListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FromListBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromListBox.FormattingEnabled = true;
            this.FromListBox.ItemHeight = 22;
            this.FromListBox.Location = new System.Drawing.Point(0, 0);
            this.FromListBox.Name = "FromListBox";
            this.FromListBox.Size = new System.Drawing.Size(365, 531);
            this.FromListBox.TabIndex = 4;
            // 
            // ToListBox
            // 
            this.ToListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToListBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToListBox.FormattingEnabled = true;
            this.ToListBox.ItemHeight = 22;
            this.ToListBox.Location = new System.Drawing.Point(0, 0);
            this.ToListBox.Name = "ToListBox";
            this.ToListBox.Size = new System.Drawing.Size(365, 532);
            this.ToListBox.TabIndex = 5;
            // 
            // Cmd_Close
            // 
            this.Cmd_Close.BackColor = System.Drawing.Color.Red;
            this.Cmd_Close.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Close.ForeColor = System.Drawing.Color.White;
            this.Cmd_Close.Location = new System.Drawing.Point(3, 81);
            this.Cmd_Close.Name = "Cmd_Close";
            this.Cmd_Close.Size = new System.Drawing.Size(251, 69);
            this.Cmd_Close.TabIndex = 11;
            this.Cmd_Close.Text = "Close";
            this.Cmd_Close.UseVisualStyleBackColor = false;
            this.Cmd_Close.Click += new System.EventHandler(this.Cmd_Close_Click);
            // 
            // Cmd_Processed
            // 
            this.Cmd_Processed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Processed.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.Cmd_Processed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Processed.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Processed.ForeColor = System.Drawing.Color.White;
            this.Cmd_Processed.Location = new System.Drawing.Point(3, 6);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(251, 69);
            this.Cmd_Processed.TabIndex = 10;
            this.Cmd_Processed.Text = "Processed";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // Lbl_Mname
            // 
            this.Lbl_Mname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Mname.ForeColor = System.Drawing.Color.Red;
            this.Lbl_Mname.Location = new System.Drawing.Point(184, 92);
            this.Lbl_Mname.Name = "Lbl_Mname";
            this.Lbl_Mname.Size = new System.Drawing.Size(365, 23);
            this.Lbl_Mname.TabIndex = 12;
            this.Lbl_Mname.Text = "Occupied Tables List";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(820, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Vacant Tables List";
            // 
            // Lbl_BusinessDate
            // 
            this.Lbl_BusinessDate.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_BusinessDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_BusinessDate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BusinessDate.ForeColor = System.Drawing.Color.Black;
            this.Lbl_BusinessDate.Location = new System.Drawing.Point(24, 723);
            this.Lbl_BusinessDate.Name = "Lbl_BusinessDate";
            this.Lbl_BusinessDate.Size = new System.Drawing.Size(1317, 35);
            this.Lbl_BusinessDate.TabIndex = 14;
            this.Lbl_BusinessDate.Text = "label3";
            this.Lbl_BusinessDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.FromListBox);
            this.panel1.Location = new System.Drawing.Point(185, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 533);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ToListBox);
            this.panel2.Location = new System.Drawing.Point(822, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(367, 534);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Cmd_Close);
            this.panel3.Controls.Add(this.Cmd_Processed);
            this.panel3.Location = new System.Drawing.Point(559, 500);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(259, 156);
            this.panel3.TabIndex = 17;
            // 
            // TransferTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Lbl_BusinessDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lbl_Mname);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransferTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferTable";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferTable_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox FromListBox;
        private System.Windows.Forms.ListBox ToListBox;
        private System.Windows.Forms.Button Cmd_Close;
        private System.Windows.Forms.Button Cmd_Processed;
        private System.Windows.Forms.Label Lbl_Mname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lbl_BusinessDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}