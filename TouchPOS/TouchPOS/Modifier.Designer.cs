namespace TouchPOS
{
    partial class Modifier
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Txt_Modifier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmd_Exit = new System.Windows.Forms.Button();
            this.Cmd_Ok = new System.Windows.Forms.Button();
            this.Lbl_Qty = new System.Windows.Forms.Label();
            this.Txt_ModiCharges = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(682, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modifier";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 188);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(644, 184);
            this.dataGridView1.TabIndex = 0;
            // 
            // Txt_Modifier
            // 
            this.Txt_Modifier.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Modifier.Location = new System.Drawing.Point(142, 9);
            this.Txt_Modifier.Name = "Txt_Modifier";
            this.Txt_Modifier.Size = new System.Drawing.Size(488, 26);
            this.Txt_Modifier.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Manual Modifier";
            // 
            // Cmd_Exit
            // 
            this.Cmd_Exit.BackColor = System.Drawing.Color.Red;
            this.Cmd_Exit.BackgroundImage = global::TouchPOS.Properties.Resources.RedbuttonMaster;
            this.Cmd_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Exit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Exit.ForeColor = System.Drawing.Color.White;
            this.Cmd_Exit.Location = new System.Drawing.Point(471, 42);
            this.Cmd_Exit.Name = "Cmd_Exit";
            this.Cmd_Exit.Size = new System.Drawing.Size(160, 50);
            this.Cmd_Exit.TabIndex = 5;
            this.Cmd_Exit.Text = "Exit";
            this.Cmd_Exit.UseVisualStyleBackColor = false;
            this.Cmd_Exit.Click += new System.EventHandler(this.Cmd_Exit_Click);
            // 
            // Cmd_Ok
            // 
            this.Cmd_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Ok.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
            this.Cmd_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Ok.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Ok.ForeColor = System.Drawing.Color.White;
            this.Cmd_Ok.Location = new System.Drawing.Point(305, 42);
            this.Cmd_Ok.Name = "Cmd_Ok";
            this.Cmd_Ok.Size = new System.Drawing.Size(160, 50);
            this.Cmd_Ok.TabIndex = 6;
            this.Cmd_Ok.Text = "OK";
            this.Cmd_Ok.UseVisualStyleBackColor = false;
            this.Cmd_Ok.Click += new System.EventHandler(this.Cmd_Ok_Click);
            // 
            // Lbl_Qty
            // 
            this.Lbl_Qty.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Qty.Location = new System.Drawing.Point(8, 47);
            this.Lbl_Qty.Name = "Lbl_Qty";
            this.Lbl_Qty.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Qty.TabIndex = 7;
            this.Lbl_Qty.Text = "Charges";
            this.Lbl_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_ModiCharges
            // 
            this.Txt_ModiCharges.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_ModiCharges.Location = new System.Drawing.Point(142, 49);
            this.Txt_ModiCharges.Name = "Txt_ModiCharges";
            this.Txt_ModiCharges.Size = new System.Drawing.Size(103, 29);
            this.Txt_ModiCharges.TabIndex = 8;
            this.Txt_ModiCharges.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_ModiCharges_KeyPress);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(21, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(664, 315);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Txt_ModiCharges);
            this.panel3.Controls.Add(this.Lbl_Qty);
            this.panel3.Controls.Add(this.Cmd_Ok);
            this.panel3.Controls.Add(this.Cmd_Exit);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.Txt_Modifier);
            this.panel3.Location = new System.Drawing.Point(7, 202);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 101);
            this.panel3.TabIndex = 9;
            // 
            // Modifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TouchPOS.Properties.Resources.Chs_Background_form_new;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(708, 377);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Modifier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifier";
            this.Load += new System.EventHandler(this.Modifier_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Txt_Modifier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cmd_Exit;
        private System.Windows.Forms.Button Cmd_Ok;
        private System.Windows.Forms.Label Lbl_Qty;
        private System.Windows.Forms.TextBox Txt_ModiCharges;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}