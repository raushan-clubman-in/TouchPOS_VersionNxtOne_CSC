namespace TouchPOS.MASTER
{
    partial class UserLogRelease
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_Close = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 34);
            this.label1.TabIndex = 6;
            this.label1.Text = "Release Logged User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Mname
            // 
            this.Lbl_Mname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Mname.ForeColor = System.Drawing.Color.Red;
            this.Lbl_Mname.Location = new System.Drawing.Point(18, 56);
            this.Lbl_Mname.Name = "Lbl_Mname";
            this.Lbl_Mname.Size = new System.Drawing.Size(258, 23);
            this.Lbl_Mname.TabIndex = 18;
            this.Lbl_Mname.Text = "Logged In User List";
            // 
            // FromListBox
            // 
            this.FromListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FromListBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromListBox.FormattingEnabled = true;
            this.FromListBox.ItemHeight = 22;
            this.FromListBox.Location = new System.Drawing.Point(15, 82);
            this.FromListBox.Name = "FromListBox";
            this.FromListBox.Size = new System.Drawing.Size(261, 398);
            this.FromListBox.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Cmd_Close);
            this.panel1.Controls.Add(this.Cmd_Processed);
            this.panel1.Location = new System.Drawing.Point(286, 323);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 155);
            this.panel1.TabIndex = 22;
            // 
            // Cmd_Close
            // 
            this.Cmd_Close.BackColor = System.Drawing.Color.Red;
            this.Cmd_Close.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Close.ForeColor = System.Drawing.Color.White;
            this.Cmd_Close.Location = new System.Drawing.Point(6, 80);
            this.Cmd_Close.Name = "Cmd_Close";
            this.Cmd_Close.Size = new System.Drawing.Size(154, 69);
            this.Cmd_Close.TabIndex = 20;
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
            this.Cmd_Processed.Location = new System.Drawing.Point(6, 5);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(154, 69);
            this.Cmd_Processed.TabIndex = 19;
            this.Cmd_Processed.Text = "Release";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // UserLogRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(476, 494);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Lbl_Mname);
            this.Controls.Add(this.FromListBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserLogRelease";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UserLogRelease";
            this.Load += new System.EventHandler(this.UserLogRelease_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lbl_Mname;
        private System.Windows.Forms.ListBox FromListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Cmd_Close;
        private System.Windows.Forms.Button Cmd_Processed;
    }
}