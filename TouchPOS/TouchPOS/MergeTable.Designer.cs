﻿namespace TouchPOS
{
    partial class MergeTable
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
            this.Lbl_Mname = new System.Windows.Forms.Label();
            this.FromListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ToListBox = new System.Windows.Forms.ListBox();
            this.Cmd_Close = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.Lbl_BusinessDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1318, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "Merge Table";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Mname
            // 
            this.Lbl_Mname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Mname.ForeColor = System.Drawing.Color.Red;
            this.Lbl_Mname.Location = new System.Drawing.Point(184, 94);
            this.Lbl_Mname.Name = "Lbl_Mname";
            this.Lbl_Mname.Size = new System.Drawing.Size(365, 23);
            this.Lbl_Mname.TabIndex = 14;
            this.Lbl_Mname.Text = "From Occupied Tables List";
            // 
            // FromListBox
            // 
            this.FromListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FromListBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromListBox.FormattingEnabled = true;
            this.FromListBox.ItemHeight = 22;
            this.FromListBox.Location = new System.Drawing.Point(188, 120);
            this.FromListBox.Name = "FromListBox";
            this.FromListBox.Size = new System.Drawing.Size(361, 530);
            this.FromListBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(811, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "To Occupied Tables List";
            // 
            // ToListBox
            // 
            this.ToListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ToListBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToListBox.FormattingEnabled = true;
            this.ToListBox.ItemHeight = 22;
            this.ToListBox.Location = new System.Drawing.Point(815, 120);
            this.ToListBox.Name = "ToListBox";
            this.ToListBox.Size = new System.Drawing.Size(361, 530);
            this.ToListBox.TabIndex = 15;
            // 
            // Cmd_Close
            // 
            this.Cmd_Close.BackColor = System.Drawing.Color.Red;
            this.Cmd_Close.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Close.ForeColor = System.Drawing.Color.White;
            this.Cmd_Close.Location = new System.Drawing.Point(5, 80);
            this.Cmd_Close.Name = "Cmd_Close";
            this.Cmd_Close.Size = new System.Drawing.Size(247, 69);
            this.Cmd_Close.TabIndex = 18;
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
            this.Cmd_Processed.Location = new System.Drawing.Point(5, 8);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(247, 69);
            this.Cmd_Processed.TabIndex = 17;
            this.Cmd_Processed.Text = "Processed";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // Lbl_BusinessDate
            // 
            this.Lbl_BusinessDate.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_BusinessDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_BusinessDate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BusinessDate.ForeColor = System.Drawing.Color.Black;
            this.Lbl_BusinessDate.Location = new System.Drawing.Point(23, 723);
            this.Lbl_BusinessDate.Name = "Lbl_BusinessDate";
            this.Lbl_BusinessDate.Size = new System.Drawing.Size(1318, 35);
            this.Lbl_BusinessDate.TabIndex = 19;
            this.Lbl_BusinessDate.Text = "label3";
            this.Lbl_BusinessDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Cmd_Processed);
            this.panel1.Controls.Add(this.Cmd_Close);
            this.panel1.Location = new System.Drawing.Point(552, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 155);
            this.panel1.TabIndex = 20;
            // 
            // MergeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Lbl_BusinessDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ToListBox);
            this.Controls.Add(this.Lbl_Mname);
            this.Controls.Add(this.FromListBox);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MergeTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MergeTable";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MergeTable_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lbl_Mname;
        private System.Windows.Forms.ListBox FromListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ToListBox;
        private System.Windows.Forms.Button Cmd_Close;
        private System.Windows.Forms.Button Cmd_Processed;
        private System.Windows.Forms.Label Lbl_BusinessDate;
        private System.Windows.Forms.Panel panel1;
    }
}