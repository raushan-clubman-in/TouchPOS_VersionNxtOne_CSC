namespace TouchPOS
{
    partial class RoomAccTagging
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmd_Clear = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Txt_GuestName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_CheckNo = new System.Windows.Forms.TextBox();
            this.Txt_RoomNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Room Tagging";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.BackColor = System.Drawing.Color.Silver;
            this.Cmd_Clear.BackgroundImage = global::TouchPOS.Properties.Resources.Clearbutton;
            this.Cmd_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Clear.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Clear.ForeColor = System.Drawing.Color.White;
            this.Cmd_Clear.Location = new System.Drawing.Point(131, 6);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(122, 53);
            this.Cmd_Clear.TabIndex = 28;
            this.Cmd_Clear.Text = "Clear";
            this.Cmd_Clear.UseVisualStyleBackColor = false;
            this.Cmd_Clear.Click += new System.EventHandler(this.Cmd_Clear_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.BackColor = System.Drawing.Color.Red;
            this.Cmd_Cancel.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Cancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Cancel.ForeColor = System.Drawing.Color.White;
            this.Cmd_Cancel.Location = new System.Drawing.Point(7, 6);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(120, 53);
            this.Cmd_Cancel.TabIndex = 27;
            this.Cmd_Cancel.Text = "Cancel";
            this.Cmd_Cancel.UseVisualStyleBackColor = false;
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // Cmd_Processed
            // 
            this.Cmd_Processed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Processed.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.Cmd_Processed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Processed.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Processed.ForeColor = System.Drawing.Color.White;
            this.Cmd_Processed.Location = new System.Drawing.Point(257, 6);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(123, 53);
            this.Cmd_Processed.TabIndex = 26;
            this.Cmd_Processed.Text = "OK";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.Txt_GuestName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Txt_CheckNo);
            this.groupBox1.Controls.Add(this.Txt_RoomNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(18, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 329);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Room Tagging For Bill";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(104, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "double click on Grid Cell for selecction";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 23);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(377, 159);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // Txt_GuestName
            // 
            this.Txt_GuestName.Enabled = false;
            this.Txt_GuestName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_GuestName.ForeColor = System.Drawing.Color.Black;
            this.Txt_GuestName.Location = new System.Drawing.Point(104, 290);
            this.Txt_GuestName.MaxLength = 50;
            this.Txt_GuestName.Name = "Txt_GuestName";
            this.Txt_GuestName.Size = new System.Drawing.Size(274, 29);
            this.Txt_GuestName.TabIndex = 6;
            this.Txt_GuestName.TextChanged += new System.EventHandler(this.Txt_GuestName_TextChanged);
            this.Txt_GuestName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_GuestName_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Guest Name";
            // 
            // Txt_CheckNo
            // 
            this.Txt_CheckNo.Enabled = false;
            this.Txt_CheckNo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_CheckNo.ForeColor = System.Drawing.Color.Black;
            this.Txt_CheckNo.Location = new System.Drawing.Point(104, 208);
            this.Txt_CheckNo.MaxLength = 50;
            this.Txt_CheckNo.Name = "Txt_CheckNo";
            this.Txt_CheckNo.Size = new System.Drawing.Size(274, 29);
            this.Txt_CheckNo.TabIndex = 4;
            this.Txt_CheckNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_CheckNo_KeyDown);
            // 
            // Txt_RoomNo
            // 
            this.Txt_RoomNo.Enabled = false;
            this.Txt_RoomNo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_RoomNo.ForeColor = System.Drawing.Color.Black;
            this.Txt_RoomNo.Location = new System.Drawing.Point(104, 249);
            this.Txt_RoomNo.MaxLength = 50;
            this.Txt_RoomNo.Name = "Txt_RoomNo";
            this.Txt_RoomNo.Size = new System.Drawing.Size(274, 29);
            this.Txt_RoomNo.TabIndex = 3;
            this.Txt_RoomNo.TextChanged += new System.EventHandler(this.Txt_RoomNo_TextChanged);
            this.Txt_RoomNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_RoomNo_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Room No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "CheckIn No";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Cmd_Clear);
            this.panel1.Controls.Add(this.Cmd_Cancel);
            this.panel1.Controls.Add(this.Cmd_Processed);
            this.panel1.Location = new System.Drawing.Point(17, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 66);
            this.panel1.TabIndex = 29;
            // 
            // RoomAccTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(424, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RoomAccTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RoomAccTagging";
            this.Load += new System.EventHandler(this.RoomAccTagging_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cmd_Clear;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.Button Cmd_Processed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Txt_CheckNo;
        private System.Windows.Forms.TextBox Txt_RoomNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_GuestName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
    }
}