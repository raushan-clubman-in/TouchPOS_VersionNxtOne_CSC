namespace TouchPOS
{
    partial class DiscItemUpdation
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Rdb_Group = new System.Windows.Forms.RadioButton();
            this.Rdb_Item = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.Cmd_Processed = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Rdb_Group);
            this.groupBox1.Controls.Add(this.Rdb_Item);
            this.groupBox1.Location = new System.Drawing.Point(22, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Rdb_Group
            // 
            this.Rdb_Group.AutoSize = true;
            this.Rdb_Group.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_Group.Location = new System.Drawing.Point(240, 19);
            this.Rdb_Group.Name = "Rdb_Group";
            this.Rdb_Group.Size = new System.Drawing.Size(110, 23);
            this.Rdb_Group.TabIndex = 1;
            this.Rdb_Group.TabStop = true;
            this.Rdb_Group.Text = "Group Basis";
            this.Rdb_Group.UseVisualStyleBackColor = true;
            this.Rdb_Group.CheckedChanged += new System.EventHandler(this.Rdb_Group_CheckedChanged);
            // 
            // Rdb_Item
            // 
            this.Rdb_Item.AutoSize = true;
            this.Rdb_Item.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rdb_Item.Location = new System.Drawing.Point(21, 19);
            this.Rdb_Item.Name = "Rdb_Item";
            this.Rdb_Item.Size = new System.Drawing.Size(99, 23);
            this.Rdb_Item.TabIndex = 0;
            this.Rdb_Item.TabStop = true;
            this.Rdb_Item.Text = "Item Basis";
            this.Rdb_Item.UseVisualStyleBackColor = true;
            this.Rdb_Item.CheckedChanged += new System.EventHandler(this.Rdb_Item_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(22, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 357);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(369, 335);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.BackColor = System.Drawing.Color.Red;
            this.Cmd_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Cancel.Location = new System.Drawing.Point(22, 436);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(189, 69);
            this.Cmd_Cancel.TabIndex = 19;
            this.Cmd_Cancel.Text = "Cancel";
            this.Cmd_Cancel.UseVisualStyleBackColor = false;
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // Cmd_Processed
            // 
            this.Cmd_Processed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Processed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Processed.Location = new System.Drawing.Point(214, 436);
            this.Cmd_Processed.Name = "Cmd_Processed";
            this.Cmd_Processed.Size = new System.Drawing.Size(185, 69);
            this.Cmd_Processed.TabIndex = 20;
            this.Cmd_Processed.Text = "Processed";
            this.Cmd_Processed.UseVisualStyleBackColor = false;
            this.Cmd_Processed.Click += new System.EventHandler(this.Cmd_Processed_Click);
            // 
            // DiscItemUpdation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(415, 525);
            this.Controls.Add(this.Cmd_Processed);
            this.Controls.Add(this.Cmd_Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DiscItemUpdation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DiscItemUpdation";
            this.Load += new System.EventHandler(this.DiscItemUpdation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Rdb_Group;
        private System.Windows.Forms.RadioButton Rdb_Item;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Cmd_Cancel;
        private System.Windows.Forms.Button Cmd_Processed;
    }
}