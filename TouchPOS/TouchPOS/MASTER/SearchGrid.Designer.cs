namespace TouchPOS.MASTER
{
    partial class SearchGrid
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
            this.Txt_SearchBox = new System.Windows.Forms.TextBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Txt_SearchBox
            // 
            this.Txt_SearchBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_SearchBox.Location = new System.Drawing.Point(12, 14);
            this.Txt_SearchBox.Name = "Txt_SearchBox";
            this.Txt_SearchBox.Size = new System.Drawing.Size(219, 29);
            this.Txt_SearchBox.TabIndex = 0;
            // 
            // Button_Search
            // 
            this.Button_Search.BackColor = System.Drawing.Color.Lime;
            this.Button_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Search.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Search.ForeColor = System.Drawing.Color.White;
            this.Button_Search.Location = new System.Drawing.Point(237, 11);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(108, 36);
            this.Button_Search.TabIndex = 5;
            this.Button_Search.Text = "Search";
            this.Button_Search.UseVisualStyleBackColor = false;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(350, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SearchGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 54);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button_Search);
            this.Controls.Add(this.Txt_SearchBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SearchGrid";
            this.Load += new System.EventHandler(this.SearchGrid_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_SearchBox;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.Button button1;
    }
}