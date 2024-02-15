namespace TouchPOS.MASTER
{
    partial class Itemselection
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
            this.Cmd_Ok = new System.Windows.Forms.Button();
            this.Cmd_Exit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shortname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Modifier = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Cmd_Ok
            // 
            this.Cmd_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Ok.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.Cmd_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Ok.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Ok.ForeColor = System.Drawing.Color.White;
            this.Cmd_Ok.Location = new System.Drawing.Point(417, 350);
            this.Cmd_Ok.Name = "Cmd_Ok";
            this.Cmd_Ok.Size = new System.Drawing.Size(94, 76);
            this.Cmd_Ok.TabIndex = 8;
            this.Cmd_Ok.Text = "OK";
            this.Cmd_Ok.UseVisualStyleBackColor = false;
            this.Cmd_Ok.Click += new System.EventHandler(this.Cmd_Ok_Click);
            // 
            // Cmd_Exit
            // 
            this.Cmd_Exit.BackColor = System.Drawing.Color.Red;
            this.Cmd_Exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Exit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Exit.ForeColor = System.Drawing.Color.White;
            this.Cmd_Exit.Location = new System.Drawing.Point(517, 350);
            this.Cmd_Exit.Name = "Cmd_Exit";
            this.Cmd_Exit.Size = new System.Drawing.Size(94, 76);
            this.Cmd_Exit.TabIndex = 7;
            this.Cmd_Exit.Text = "Exit";
            this.Cmd_Exit.UseVisualStyleBackColor = false;
            this.Cmd_Exit.Click += new System.EventHandler(this.Cmd_Exit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Itemcode,
            this.Itemname,
            this.Shortname,
            this.Status});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(591, 332);
            this.dataGridView1.TabIndex = 9;
            // 
            // Itemcode
            // 
            this.Itemcode.HeaderText = "Itemcode";
            this.Itemcode.Name = "Itemcode";
            this.Itemcode.Width = 150;
            // 
            // Itemname
            // 
            this.Itemname.HeaderText = "Itemname";
            this.Itemname.Name = "Itemname";
            this.Itemname.Width = 150;
            // 
            // Shortname
            // 
            this.Shortname.HeaderText = "Shortname";
            this.Shortname.Name = "Shortname";
            this.Shortname.Width = 150;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 397);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Item Search";
            // 
            // Txt_Modifier
            // 
            this.Txt_Modifier.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Modifier.Location = new System.Drawing.Point(113, 397);
            this.Txt_Modifier.Name = "Txt_Modifier";
            this.Txt_Modifier.Size = new System.Drawing.Size(298, 26);
            this.Txt_Modifier.TabIndex = 10;
            this.Txt_Modifier.TextChanged += new System.EventHandler(this.Txt_Modifier_TextChanged);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(27, 362);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 23);
            this.label18.TabIndex = 494;
            this.label18.Text = "Type";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ITEMCODE",
            "ITEMDESC"});
            this.comboBox1.Location = new System.Drawing.Point(113, 364);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(298, 21);
            this.comboBox1.TabIndex = 493;
            // 
            // Itemselection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(624, 438);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_Modifier);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Cmd_Ok);
            this.Controls.Add(this.Cmd_Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Itemselection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Itemselection";
            this.Load += new System.EventHandler(this.Itemselection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cmd_Ok;
        private System.Windows.Forms.Button Cmd_Exit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shortname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_Modifier;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}