using System.Windows.Forms;
namespace TouchPOS
{
    partial class EntryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Autoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CanItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Promo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HappyFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCharges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmd_TransferItem = new System.Windows.Forms.Button();
            this.Cmd_OptionalPrint = new System.Windows.Forms.Button();
            this.Cmd_ChangePT = new System.Windows.Forms.Button();
            this.Cmd_AddPax = new System.Windows.Forms.Button();
            this.Cmd_Less = new System.Windows.Forms.Button();
            this.Cmd_Add = new System.Windows.Forms.Button();
            this.Cmd_ClearOne = new System.Windows.Forms.Button();
            this.Cmd_Pay = new System.Windows.Forms.Button();
            this.Cmd_Exit = new System.Windows.Forms.Button();
            this.Cmd_Modifier = new System.Windows.Forms.Button();
            this.Cmd_Save = new System.Windows.Forms.Button();
            this.Cmd_ClearAll = new System.Windows.Forms.Button();
            this.Cmd_SubGroup = new System.Windows.Forms.Button();
            this.Cmd_Group = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flpItem = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.text_auto_nw = new System.Windows.Forms.TextBox();
            this.Cmd_BackSpace = new System.Windows.Forms.Button();
            this.Txt_Item = new System.Windows.Forms.TextBox();
            this.Lbl_TotAmt = new System.Windows.Forms.Label();
            this.Lbl_Qty = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Button_Qty = new System.Windows.Forms.Button();
            this.Button_Dot = new System.Windows.Forms.Button();
            this.Button_0 = new System.Windows.Forms.Button();
            this.Button_3 = new System.Windows.Forms.Button();
            this.Button_2 = new System.Windows.Forms.Button();
            this.Button_1 = new System.Windows.Forms.Button();
            this.Button_6 = new System.Windows.Forms.Button();
            this.Button_5 = new System.Windows.Forms.Button();
            this.Button_4 = new System.Windows.Forms.Button();
            this.Button_9 = new System.Windows.Forms.Button();
            this.Button_8 = new System.Windows.Forms.Button();
            this.Button_7 = new System.Windows.Forms.Button();
            this.Lbl_Modifier = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Txt_Remarks = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.Chk_NCApply = new System.Windows.Forms.CheckBox();
            this.Cmb_NCCategory = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.Cmb_Steward = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Cmb_BSource = new System.Windows.Forms.ComboBox();
            this.Txt_BarCode = new System.Windows.Forms.TextBox();
            this.Lbl_BarCode = new System.Windows.Forms.Label();
            this.Chk_SearchByCode = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.Pnl_DirectB = new System.Windows.Forms.Panel();
            this.Cmd_D3_DBilling = new System.Windows.Forms.Button();
            this.Cmd_D2_DBilling = new System.Windows.Forms.Button();
            this.Cmd_D1_DBilling = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox_Items = new System.Windows.Forms.ListBox();
            this.Chk_Contain = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.Pnl_DirectB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 299);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemDesc,
            this.Qty,
            this.Rate,
            this.Amount,
            this.POSCode,
            this.UOM,
            this.Modifier,
            this.ChargeCode,
            this.Slno,
            this.Autoid,
            this.OrderNo,
            this.CanItem,
            this.Promo,
            this.Qty2,
            this.HappyFlag,
            this.SOrder,
            this.MCharges});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(427, 297);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentChanged);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Visible = false;
            // 
            // ItemDesc
            // 
            this.ItemDesc.HeaderText = "Item";
            this.ItemDesc.Name = "ItemDesc";
            this.ItemDesc.ReadOnly = true;
            this.ItemDesc.Width = 200;
            // 
            // Qty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 50;
            // 
            // Rate
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle6;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 50;
            // 
            // Amount
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // POSCode
            // 
            this.POSCode.HeaderText = "POSCode";
            this.POSCode.Name = "POSCode";
            this.POSCode.Visible = false;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.Visible = false;
            // 
            // Modifier
            // 
            this.Modifier.HeaderText = "Modifier";
            this.Modifier.Name = "Modifier";
            this.Modifier.Visible = false;
            // 
            // ChargeCode
            // 
            this.ChargeCode.HeaderText = "ChargeCode";
            this.ChargeCode.Name = "ChargeCode";
            this.ChargeCode.ReadOnly = true;
            this.ChargeCode.Visible = false;
            // 
            // Slno
            // 
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.Slno.DefaultCellStyle = dataGridViewCellStyle8;
            this.Slno.HeaderText = "Slno";
            this.Slno.Name = "Slno";
            this.Slno.ReadOnly = true;
            this.Slno.Visible = false;
            // 
            // Autoid
            // 
            this.Autoid.HeaderText = "Autoid";
            this.Autoid.Name = "Autoid";
            this.Autoid.Visible = false;
            // 
            // OrderNo
            // 
            this.OrderNo.HeaderText = "OrderNo";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.Visible = false;
            // 
            // CanItem
            // 
            this.CanItem.HeaderText = "CanItem";
            this.CanItem.Name = "CanItem";
            this.CanItem.Visible = false;
            // 
            // Promo
            // 
            this.Promo.HeaderText = "Promo";
            this.Promo.Name = "Promo";
            this.Promo.Visible = false;
            // 
            // Qty2
            // 
            this.Qty2.HeaderText = "Qty2";
            this.Qty2.Name = "Qty2";
            this.Qty2.Visible = false;
            // 
            // HappyFlag
            // 
            this.HappyFlag.HeaderText = "HappyFlag";
            this.HappyFlag.Name = "HappyFlag";
            this.HappyFlag.Visible = false;
            // 
            // SOrder
            // 
            this.SOrder.HeaderText = "SOrder";
            this.SOrder.Name = "SOrder";
            // 
            // MCharges
            // 
            this.MCharges.HeaderText = "MCharges";
            this.MCharges.Name = "MCharges";
            this.MCharges.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Cmd_TransferItem);
            this.panel2.Controls.Add(this.Cmd_OptionalPrint);
            this.panel2.Controls.Add(this.Cmd_ChangePT);
            this.panel2.Controls.Add(this.Cmd_AddPax);
            this.panel2.Controls.Add(this.Cmd_Less);
            this.panel2.Controls.Add(this.Cmd_Add);
            this.panel2.Controls.Add(this.Cmd_ClearOne);
            this.panel2.Controls.Add(this.Cmd_Pay);
            this.panel2.Controls.Add(this.Cmd_Exit);
            this.panel2.Controls.Add(this.Cmd_Modifier);
            this.panel2.Controls.Add(this.Cmd_Save);
            this.panel2.Controls.Add(this.Cmd_ClearAll);
            this.panel2.Controls.Add(this.Cmd_SubGroup);
            this.panel2.Controls.Add(this.Cmd_Group);
            this.panel2.Location = new System.Drawing.Point(445, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 710);
            this.panel2.TabIndex = 1;
            // 
            // Cmd_TransferItem
            // 
            this.Cmd_TransferItem.BackColor = System.Drawing.Color.LightGreen;
            this.Cmd_TransferItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_TransferItem.BackgroundImage")));
            this.Cmd_TransferItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_TransferItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_TransferItem.Location = new System.Drawing.Point(3, 583);
            this.Cmd_TransferItem.Name = "Cmd_TransferItem";
            this.Cmd_TransferItem.Size = new System.Drawing.Size(130, 45);
            this.Cmd_TransferItem.TabIndex = 12;
            this.Cmd_TransferItem.Text = "Transfer Item";
            this.Cmd_TransferItem.UseVisualStyleBackColor = false;
            this.Cmd_TransferItem.Click += new System.EventHandler(this.Cmd_TransferItem_Click);
            // 
            // Cmd_OptionalPrint
            // 
            this.Cmd_OptionalPrint.BackColor = System.Drawing.Color.Blue;
            this.Cmd_OptionalPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_OptionalPrint.BackgroundImage")));
            this.Cmd_OptionalPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_OptionalPrint.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_OptionalPrint.ForeColor = System.Drawing.Color.Black;
            this.Cmd_OptionalPrint.Location = new System.Drawing.Point(3, 531);
            this.Cmd_OptionalPrint.Name = "Cmd_OptionalPrint";
            this.Cmd_OptionalPrint.Size = new System.Drawing.Size(130, 50);
            this.Cmd_OptionalPrint.TabIndex = 11;
            this.Cmd_OptionalPrint.Text = "Optional Print";
            this.Cmd_OptionalPrint.UseVisualStyleBackColor = false;
            this.Cmd_OptionalPrint.Click += new System.EventHandler(this.Cmd_OptionalPrint_Click);
            // 
            // Cmd_ChangePT
            // 
            this.Cmd_ChangePT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_ChangePT.Location = new System.Drawing.Point(3, 592);
            this.Cmd_ChangePT.Name = "Cmd_ChangePT";
            this.Cmd_ChangePT.Size = new System.Drawing.Size(36, 32);
            this.Cmd_ChangePT.TabIndex = 10;
            this.Cmd_ChangePT.Text = "Change Payment Types";
            this.Cmd_ChangePT.UseVisualStyleBackColor = true;
            this.Cmd_ChangePT.Visible = false;
            this.Cmd_ChangePT.Click += new System.EventHandler(this.Cmd_ChangePT_Click);
            // 
            // Cmd_AddPax
            // 
            this.Cmd_AddPax.BackColor = System.Drawing.Color.LightGreen;
            this.Cmd_AddPax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_AddPax.BackgroundImage")));
            this.Cmd_AddPax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_AddPax.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_AddPax.Location = new System.Drawing.Point(3, 484);
            this.Cmd_AddPax.Name = "Cmd_AddPax";
            this.Cmd_AddPax.Size = new System.Drawing.Size(130, 45);
            this.Cmd_AddPax.TabIndex = 9;
            this.Cmd_AddPax.Text = "Add Pax";
            this.Cmd_AddPax.UseVisualStyleBackColor = false;
            this.Cmd_AddPax.Click += new System.EventHandler(this.Cmd_AddPax_Click);
            // 
            // Cmd_Less
            // 
            this.Cmd_Less.BackColor = System.Drawing.Color.Orange;
            this.Cmd_Less.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Less.BackgroundImage")));
            this.Cmd_Less.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Less.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Less.Location = new System.Drawing.Point(3, 436);
            this.Cmd_Less.Name = "Cmd_Less";
            this.Cmd_Less.Size = new System.Drawing.Size(130, 45);
            this.Cmd_Less.TabIndex = 8;
            this.Cmd_Less.Text = "Less Qty";
            this.Cmd_Less.UseVisualStyleBackColor = false;
            this.Cmd_Less.Click += new System.EventHandler(this.Cmd_Less_Click);
            // 
            // Cmd_Add
            // 
            this.Cmd_Add.BackColor = System.Drawing.Color.Orange;
            this.Cmd_Add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Add.BackgroundImage")));
            this.Cmd_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Add.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Add.Location = new System.Drawing.Point(3, 388);
            this.Cmd_Add.Name = "Cmd_Add";
            this.Cmd_Add.Size = new System.Drawing.Size(130, 45);
            this.Cmd_Add.TabIndex = 7;
            this.Cmd_Add.Text = "Add Qty";
            this.Cmd_Add.UseVisualStyleBackColor = false;
            this.Cmd_Add.Click += new System.EventHandler(this.Cmd_Add_Click);
            // 
            // Cmd_ClearOne
            // 
            this.Cmd_ClearOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Cmd_ClearOne.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_ClearOne.BackgroundImage")));
            this.Cmd_ClearOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_ClearOne.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_ClearOne.Location = new System.Drawing.Point(3, 340);
            this.Cmd_ClearOne.Name = "Cmd_ClearOne";
            this.Cmd_ClearOne.Size = new System.Drawing.Size(130, 45);
            this.Cmd_ClearOne.TabIndex = 6;
            this.Cmd_ClearOne.Text = "Clear One";
            this.Cmd_ClearOne.UseVisualStyleBackColor = false;
            this.Cmd_ClearOne.Click += new System.EventHandler(this.Cmd_ClearOne_Click);
            // 
            // Cmd_Pay
            // 
            this.Cmd_Pay.BackColor = System.Drawing.Color.LimeGreen;
            this.Cmd_Pay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Pay.BackgroundImage")));
            this.Cmd_Pay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Pay.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Pay.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Pay.Location = new System.Drawing.Point(3, 257);
            this.Cmd_Pay.Name = "Cmd_Pay";
            this.Cmd_Pay.Size = new System.Drawing.Size(130, 79);
            this.Cmd_Pay.TabIndex = 5;
            this.Cmd_Pay.Text = "Pay";
            this.Cmd_Pay.UseVisualStyleBackColor = false;
            this.Cmd_Pay.Click += new System.EventHandler(this.Cmd_Pay_Click);
            // 
            // Cmd_Exit
            // 
            this.Cmd_Exit.BackColor = System.Drawing.Color.Red;
            this.Cmd_Exit.BackgroundImage = global::TouchPOS.Properties.Resources.Red;
            this.Cmd_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Exit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Exit.ForeColor = System.Drawing.Color.White;
            this.Cmd_Exit.Location = new System.Drawing.Point(3, 629);
            this.Cmd_Exit.Name = "Cmd_Exit";
            this.Cmd_Exit.Size = new System.Drawing.Size(129, 76);
            this.Cmd_Exit.TabIndex = 4;
            this.Cmd_Exit.Text = "Exit";
            this.Cmd_Exit.UseVisualStyleBackColor = false;
            this.Cmd_Exit.Click += new System.EventHandler(this.Cmd_Exit_Click);
            // 
            // Cmd_Modifier
            // 
            this.Cmd_Modifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Cmd_Modifier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Modifier.BackgroundImage")));
            this.Cmd_Modifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Modifier.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Modifier.Location = new System.Drawing.Point(3, 207);
            this.Cmd_Modifier.Name = "Cmd_Modifier";
            this.Cmd_Modifier.Size = new System.Drawing.Size(130, 45);
            this.Cmd_Modifier.TabIndex = 4;
            this.Cmd_Modifier.Text = "Modifier";
            this.Cmd_Modifier.UseVisualStyleBackColor = false;
            this.Cmd_Modifier.Click += new System.EventHandler(this.Cmd_Modifier_Click);
            // 
            // Cmd_Save
            // 
            this.Cmd_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Cmd_Save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Save.BackgroundImage")));
            this.Cmd_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Save.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Save.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Save.Location = new System.Drawing.Point(3, 158);
            this.Cmd_Save.Name = "Cmd_Save";
            this.Cmd_Save.Size = new System.Drawing.Size(130, 45);
            this.Cmd_Save.TabIndex = 3;
            this.Cmd_Save.Text = "Save";
            this.Cmd_Save.UseVisualStyleBackColor = false;
            this.Cmd_Save.Click += new System.EventHandler(this.Cmd_Save_Click);
            // 
            // Cmd_ClearAll
            // 
            this.Cmd_ClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Cmd_ClearAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_ClearAll.BackgroundImage")));
            this.Cmd_ClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_ClearAll.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_ClearAll.Location = new System.Drawing.Point(3, 108);
            this.Cmd_ClearAll.Name = "Cmd_ClearAll";
            this.Cmd_ClearAll.Size = new System.Drawing.Size(130, 45);
            this.Cmd_ClearAll.TabIndex = 2;
            this.Cmd_ClearAll.Text = "Clear All";
            this.Cmd_ClearAll.UseVisualStyleBackColor = false;
            this.Cmd_ClearAll.Click += new System.EventHandler(this.Cmd_ClearAll_Click);
            // 
            // Cmd_SubGroup
            // 
            this.Cmd_SubGroup.BackColor = System.Drawing.Color.Blue;
            this.Cmd_SubGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_SubGroup.BackgroundImage")));
            this.Cmd_SubGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_SubGroup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_SubGroup.ForeColor = System.Drawing.Color.Black;
            this.Cmd_SubGroup.Location = new System.Drawing.Point(3, 58);
            this.Cmd_SubGroup.Name = "Cmd_SubGroup";
            this.Cmd_SubGroup.Size = new System.Drawing.Size(130, 45);
            this.Cmd_SubGroup.TabIndex = 1;
            this.Cmd_SubGroup.Text = "Sub Groups";
            this.Cmd_SubGroup.UseVisualStyleBackColor = false;
            this.Cmd_SubGroup.Click += new System.EventHandler(this.Cmd_SubGroup_Click);
            // 
            // Cmd_Group
            // 
            this.Cmd_Group.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Cmd_Group.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cmd_Group.BackgroundImage")));
            this.Cmd_Group.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cmd_Group.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_Group.ForeColor = System.Drawing.Color.Black;
            this.Cmd_Group.Location = new System.Drawing.Point(3, 9);
            this.Cmd_Group.Name = "Cmd_Group";
            this.Cmd_Group.Size = new System.Drawing.Size(130, 45);
            this.Cmd_Group.TabIndex = 0;
            this.Cmd_Group.Text = "Groups";
            this.Cmd_Group.UseVisualStyleBackColor = false;
            this.Cmd_Group.Click += new System.EventHandler(this.Cmd_Group_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(12, 725);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1342, 37);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(768, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(566, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(565, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpItem
            // 
            this.flpItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpItem.Location = new System.Drawing.Point(589, 12);
            this.flpItem.Name = "flpItem";
            this.flpItem.Size = new System.Drawing.Size(766, 705);
            this.flpItem.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(285, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 43);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(312, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.text_auto_nw);
            this.panel4.Controls.Add(this.Cmd_BackSpace);
            this.panel4.Controls.Add(this.Txt_Item);
            this.panel4.Controls.Add(this.Lbl_TotAmt);
            this.panel4.Controls.Add(this.Lbl_Qty);
            this.panel4.Location = new System.Drawing.Point(12, 419);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(425, 61);
            this.panel4.TabIndex = 9;
            // 
            // text_auto_nw
            // 
            this.text_auto_nw.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_auto_nw.Location = new System.Drawing.Point(9, 30);
            this.text_auto_nw.Name = "text_auto_nw";
            this.text_auto_nw.Size = new System.Drawing.Size(226, 26);
            this.text_auto_nw.TabIndex = 4;
            this.text_auto_nw.TextChanged += new System.EventHandler(this.text_auto_nw_TextChanged);
            this.text_auto_nw.Leave += new System.EventHandler(this.text_auto_nw_Leave);
            // 
            // Cmd_BackSpace
            // 
            this.Cmd_BackSpace.BackColor = System.Drawing.Color.MistyRose;
            this.Cmd_BackSpace.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_BackSpace.ForeColor = System.Drawing.Color.Red;
            this.Cmd_BackSpace.Location = new System.Drawing.Point(241, 23);
            this.Cmd_BackSpace.Name = "Cmd_BackSpace";
            this.Cmd_BackSpace.Size = new System.Drawing.Size(168, 34);
            this.Cmd_BackSpace.TabIndex = 3;
            this.Cmd_BackSpace.Text = "<<< BackSpace";
            this.Cmd_BackSpace.UseVisualStyleBackColor = false;
            this.Cmd_BackSpace.Click += new System.EventHandler(this.Cmd_BackSpace_Click);
            // 
            // Txt_Item
            // 
            this.Txt_Item.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Item.Location = new System.Drawing.Point(224, -1);
            this.Txt_Item.Name = "Txt_Item";
            this.Txt_Item.Size = new System.Drawing.Size(11, 29);
            this.Txt_Item.TabIndex = 2;
            this.Txt_Item.Visible = false;
            this.Txt_Item.TextChanged += new System.EventHandler(this.Txt_Item_TextChanged);
            this.Txt_Item.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Item_KeyDown);
            this.Txt_Item.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Item_KeyPress);
            // 
            // Lbl_TotAmt
            // 
            this.Lbl_TotAmt.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TotAmt.Location = new System.Drawing.Point(185, 5);
            this.Lbl_TotAmt.Name = "Lbl_TotAmt";
            this.Lbl_TotAmt.Size = new System.Drawing.Size(224, 16);
            this.Lbl_TotAmt.TabIndex = 1;
            this.Lbl_TotAmt.Text = "Lbl_TotAmt";
            this.Lbl_TotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Lbl_Qty
            // 
            this.Lbl_Qty.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Qty.Location = new System.Drawing.Point(6, 4);
            this.Lbl_Qty.Name = "Lbl_Qty";
            this.Lbl_Qty.Size = new System.Drawing.Size(141, 17);
            this.Lbl_Qty.TabIndex = 0;
            this.Lbl_Qty.Text = "Lbl_Qty";
            this.Lbl_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.Button_Qty);
            this.panel5.Controls.Add(this.Button_Dot);
            this.panel5.Controls.Add(this.Button_0);
            this.panel5.Controls.Add(this.Button_3);
            this.panel5.Controls.Add(this.Button_2);
            this.panel5.Controls.Add(this.Button_1);
            this.panel5.Controls.Add(this.Button_6);
            this.panel5.Controls.Add(this.Button_5);
            this.panel5.Controls.Add(this.Button_4);
            this.panel5.Controls.Add(this.Button_9);
            this.panel5.Controls.Add(this.Button_8);
            this.panel5.Controls.Add(this.Button_7);
            this.panel5.Location = new System.Drawing.Point(13, 483);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(279, 204);
            this.panel5.TabIndex = 10;
            // 
            // Button_Qty
            // 
            this.Button_Qty.BackColor = System.Drawing.Color.Goldenrod;
            this.Button_Qty.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Qty.Location = new System.Drawing.Point(188, 151);
            this.Button_Qty.Name = "Button_Qty";
            this.Button_Qty.Size = new System.Drawing.Size(86, 50);
            this.Button_Qty.TabIndex = 26;
            this.Button_Qty.Text = "Qty";
            this.Button_Qty.UseVisualStyleBackColor = false;
            this.Button_Qty.Click += new System.EventHandler(this.Button_Qty_Click);
            // 
            // Button_Dot
            // 
            this.Button_Dot.BackColor = System.Drawing.Color.White;
            this.Button_Dot.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Dot.Location = new System.Drawing.Point(4, 151);
            this.Button_Dot.Name = "Button_Dot";
            this.Button_Dot.Size = new System.Drawing.Size(86, 50);
            this.Button_Dot.TabIndex = 25;
            this.Button_Dot.Text = ".";
            this.Button_Dot.UseVisualStyleBackColor = false;
            // 
            // Button_0
            // 
            this.Button_0.BackColor = System.Drawing.Color.White;
            this.Button_0.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_0.Location = new System.Drawing.Point(96, 151);
            this.Button_0.Name = "Button_0";
            this.Button_0.Size = new System.Drawing.Size(86, 50);
            this.Button_0.TabIndex = 24;
            this.Button_0.Text = "0";
            this.Button_0.UseVisualStyleBackColor = false;
            this.Button_0.Click += new System.EventHandler(this.Button_0_Click);
            // 
            // Button_3
            // 
            this.Button_3.BackColor = System.Drawing.Color.White;
            this.Button_3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_3.Location = new System.Drawing.Point(188, 103);
            this.Button_3.Name = "Button_3";
            this.Button_3.Size = new System.Drawing.Size(86, 50);
            this.Button_3.TabIndex = 23;
            this.Button_3.Text = "3";
            this.Button_3.UseVisualStyleBackColor = false;
            this.Button_3.Click += new System.EventHandler(this.Button_3_Click);
            // 
            // Button_2
            // 
            this.Button_2.BackColor = System.Drawing.Color.White;
            this.Button_2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_2.Location = new System.Drawing.Point(96, 103);
            this.Button_2.Name = "Button_2";
            this.Button_2.Size = new System.Drawing.Size(86, 50);
            this.Button_2.TabIndex = 22;
            this.Button_2.Text = "2";
            this.Button_2.UseVisualStyleBackColor = false;
            this.Button_2.Click += new System.EventHandler(this.Button_2_Click);
            // 
            // Button_1
            // 
            this.Button_1.BackColor = System.Drawing.Color.White;
            this.Button_1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_1.Location = new System.Drawing.Point(4, 103);
            this.Button_1.Name = "Button_1";
            this.Button_1.Size = new System.Drawing.Size(86, 50);
            this.Button_1.TabIndex = 21;
            this.Button_1.Text = "1";
            this.Button_1.UseVisualStyleBackColor = false;
            this.Button_1.Click += new System.EventHandler(this.Button_1_Click);
            // 
            // Button_6
            // 
            this.Button_6.BackColor = System.Drawing.Color.White;
            this.Button_6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_6.Location = new System.Drawing.Point(188, 54);
            this.Button_6.Name = "Button_6";
            this.Button_6.Size = new System.Drawing.Size(86, 50);
            this.Button_6.TabIndex = 20;
            this.Button_6.Text = "6";
            this.Button_6.UseVisualStyleBackColor = false;
            this.Button_6.Click += new System.EventHandler(this.Button_6_Click);
            // 
            // Button_5
            // 
            this.Button_5.BackColor = System.Drawing.Color.White;
            this.Button_5.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_5.Location = new System.Drawing.Point(96, 54);
            this.Button_5.Name = "Button_5";
            this.Button_5.Size = new System.Drawing.Size(86, 50);
            this.Button_5.TabIndex = 19;
            this.Button_5.Text = "5";
            this.Button_5.UseVisualStyleBackColor = false;
            this.Button_5.Click += new System.EventHandler(this.Button_5_Click);
            // 
            // Button_4
            // 
            this.Button_4.BackColor = System.Drawing.Color.White;
            this.Button_4.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_4.Location = new System.Drawing.Point(4, 54);
            this.Button_4.Name = "Button_4";
            this.Button_4.Size = new System.Drawing.Size(86, 50);
            this.Button_4.TabIndex = 18;
            this.Button_4.Text = "4";
            this.Button_4.UseVisualStyleBackColor = false;
            this.Button_4.Click += new System.EventHandler(this.Button_4_Click);
            // 
            // Button_9
            // 
            this.Button_9.BackColor = System.Drawing.Color.White;
            this.Button_9.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_9.Location = new System.Drawing.Point(188, 4);
            this.Button_9.Name = "Button_9";
            this.Button_9.Size = new System.Drawing.Size(86, 50);
            this.Button_9.TabIndex = 17;
            this.Button_9.Text = "9";
            this.Button_9.UseVisualStyleBackColor = false;
            this.Button_9.Click += new System.EventHandler(this.Button_9_Click);
            // 
            // Button_8
            // 
            this.Button_8.BackColor = System.Drawing.Color.White;
            this.Button_8.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_8.Location = new System.Drawing.Point(96, 4);
            this.Button_8.Name = "Button_8";
            this.Button_8.Size = new System.Drawing.Size(86, 50);
            this.Button_8.TabIndex = 16;
            this.Button_8.Text = "8";
            this.Button_8.UseVisualStyleBackColor = false;
            this.Button_8.Click += new System.EventHandler(this.Button_8_Click);
            // 
            // Button_7
            // 
            this.Button_7.BackColor = System.Drawing.Color.White;
            this.Button_7.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_7.Location = new System.Drawing.Point(4, 4);
            this.Button_7.Name = "Button_7";
            this.Button_7.Size = new System.Drawing.Size(86, 50);
            this.Button_7.TabIndex = 15;
            this.Button_7.Text = "7";
            this.Button_7.UseVisualStyleBackColor = false;
            this.Button_7.Click += new System.EventHandler(this.Button_7_Click);
            // 
            // Lbl_Modifier
            // 
            this.Lbl_Modifier.AutoSize = true;
            this.Lbl_Modifier.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Modifier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Lbl_Modifier.Location = new System.Drawing.Point(17, 398);
            this.Lbl_Modifier.Name = "Lbl_Modifier";
            this.Lbl_Modifier.Size = new System.Drawing.Size(40, 15);
            this.Lbl_Modifier.TabIndex = 11;
            this.Lbl_Modifier.Text = "label6";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 693);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Remarks";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_Remarks
            // 
            this.Txt_Remarks.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Remarks.Location = new System.Drawing.Point(86, 691);
            this.Txt_Remarks.Name = "Txt_Remarks";
            this.Txt_Remarks.Size = new System.Drawing.Size(205, 26);
            this.Txt_Remarks.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Location = new System.Drawing.Point(298, 483);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(139, 203);
            this.panel6.TabIndex = 12;
            // 
            // panel10
            // 
            this.panel10.BackgroundImage = global::TouchPOS.Properties.Resources.Gold;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.Chk_NCApply);
            this.panel10.Controls.Add(this.Cmb_NCCategory);
            this.panel10.Location = new System.Drawing.Point(2, 139);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(132, 60);
            this.panel10.TabIndex = 10;
            // 
            // Chk_NCApply
            // 
            this.Chk_NCApply.AutoSize = true;
            this.Chk_NCApply.BackColor = System.Drawing.Color.Transparent;
            this.Chk_NCApply.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_NCApply.Location = new System.Drawing.Point(10, 7);
            this.Chk_NCApply.Name = "Chk_NCApply";
            this.Chk_NCApply.Size = new System.Drawing.Size(106, 19);
            this.Chk_NCApply.TabIndex = 7;
            this.Chk_NCApply.Text = "N C Applicable";
            this.Chk_NCApply.UseVisualStyleBackColor = false;
            this.Chk_NCApply.CheckedChanged += new System.EventHandler(this.Chk_NCApply_CheckedChanged);
            // 
            // Cmb_NCCategory
            // 
            this.Cmb_NCCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_NCCategory.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_NCCategory.FormattingEnabled = true;
            this.Cmb_NCCategory.Location = new System.Drawing.Point(6, 27);
            this.Cmb_NCCategory.Name = "Cmb_NCCategory";
            this.Cmb_NCCategory.Size = new System.Drawing.Size(119, 27);
            this.Cmb_NCCategory.TabIndex = 6;
            this.Cmb_NCCategory.SelectedIndexChanged += new System.EventHandler(this.Cmb_NCCategory_SelectedIndexChanged);
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::TouchPOS.Properties.Resources.Gold;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.Cmb_Steward);
            this.panel9.Location = new System.Drawing.Point(2, 71);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(132, 60);
            this.panel9.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "Steward";
            // 
            // Cmb_Steward
            // 
            this.Cmb_Steward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Steward.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Steward.FormattingEnabled = true;
            this.Cmb_Steward.Location = new System.Drawing.Point(4, 30);
            this.Cmb_Steward.Name = "Cmb_Steward";
            this.Cmb_Steward.Size = new System.Drawing.Size(122, 27);
            this.Cmb_Steward.TabIndex = 4;
            this.Cmb_Steward.SelectedIndexChanged += new System.EventHandler(this.Cmb_Steward_SelectedindexChaged);
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::TouchPOS.Properties.Resources.Gold;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.Cmb_BSource);
            this.panel8.Location = new System.Drawing.Point(3, 6);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(132, 60);
            this.panel8.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Business Source";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmb_BSource
            // 
            this.Cmb_BSource.BackColor = System.Drawing.Color.Goldenrod;
            this.Cmb_BSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_BSource.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_BSource.FormattingEnabled = true;
            this.Cmb_BSource.Location = new System.Drawing.Point(6, 26);
            this.Cmb_BSource.Name = "Cmb_BSource";
            this.Cmb_BSource.Size = new System.Drawing.Size(118, 27);
            this.Cmb_BSource.TabIndex = 2;
            // 
            // Txt_BarCode
            // 
            this.Txt_BarCode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_BarCode.Location = new System.Drawing.Point(425, 661);
            this.Txt_BarCode.Name = "Txt_BarCode";
            this.Txt_BarCode.Size = new System.Drawing.Size(19, 26);
            this.Txt_BarCode.TabIndex = 9;
            // 
            // Lbl_BarCode
            // 
            this.Lbl_BarCode.AutoSize = true;
            this.Lbl_BarCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BarCode.Location = new System.Drawing.Point(365, 672);
            this.Lbl_BarCode.Name = "Lbl_BarCode";
            this.Lbl_BarCode.Size = new System.Drawing.Size(58, 15);
            this.Lbl_BarCode.TabIndex = 8;
            this.Lbl_BarCode.Text = "Bar Code";
            // 
            // Chk_SearchByCode
            // 
            this.Chk_SearchByCode.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_SearchByCode.Location = new System.Drawing.Point(314, 397);
            this.Chk_SearchByCode.Name = "Chk_SearchByCode";
            this.Chk_SearchByCode.Size = new System.Drawing.Size(123, 17);
            this.Chk_SearchByCode.TabIndex = 15;
            this.Chk_SearchByCode.Text = "Search By Code";
            this.Chk_SearchByCode.UseVisualStyleBackColor = true;
            this.Chk_SearchByCode.CheckedChanged += new System.EventHandler(this.Chk_SearchByCode_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.Pnl_DirectB);
            this.panel7.Controls.Add(this.pictureBox1);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Location = new System.Drawing.Point(12, 10);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(429, 77);
            this.panel7.TabIndex = 16;
            // 
            // Pnl_DirectB
            // 
            this.Pnl_DirectB.Controls.Add(this.Cmd_D3_DBilling);
            this.Pnl_DirectB.Controls.Add(this.Cmd_D2_DBilling);
            this.Pnl_DirectB.Controls.Add(this.Cmd_D1_DBilling);
            this.Pnl_DirectB.Location = new System.Drawing.Point(112, 3);
            this.Pnl_DirectB.Name = "Pnl_DirectB";
            this.Pnl_DirectB.Size = new System.Drawing.Size(168, 47);
            this.Pnl_DirectB.TabIndex = 9;
            this.Pnl_DirectB.Visible = false;
            // 
            // Cmd_D3_DBilling
            // 
            this.Cmd_D3_DBilling.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_D3_DBilling.Location = new System.Drawing.Point(115, 3);
            this.Cmd_D3_DBilling.Name = "Cmd_D3_DBilling";
            this.Cmd_D3_DBilling.Size = new System.Drawing.Size(48, 38);
            this.Cmd_D3_DBilling.TabIndex = 2;
            this.Cmd_D3_DBilling.Text = "V3";
            this.Cmd_D3_DBilling.UseVisualStyleBackColor = true;
            this.Cmd_D3_DBilling.Click += new System.EventHandler(this.Cmd_D3_DBilling_Click);
            // 
            // Cmd_D2_DBilling
            // 
            this.Cmd_D2_DBilling.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_D2_DBilling.Location = new System.Drawing.Point(60, 3);
            this.Cmd_D2_DBilling.Name = "Cmd_D2_DBilling";
            this.Cmd_D2_DBilling.Size = new System.Drawing.Size(48, 38);
            this.Cmd_D2_DBilling.TabIndex = 1;
            this.Cmd_D2_DBilling.Text = "V2";
            this.Cmd_D2_DBilling.UseVisualStyleBackColor = true;
            this.Cmd_D2_DBilling.Click += new System.EventHandler(this.Cmd_D2_DBilling_Click);
            // 
            // Cmd_D1_DBilling
            // 
            this.Cmd_D1_DBilling.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmd_D1_DBilling.Location = new System.Drawing.Point(6, 3);
            this.Cmd_D1_DBilling.Name = "Cmd_D1_DBilling";
            this.Cmd_D1_DBilling.Size = new System.Drawing.Size(48, 38);
            this.Cmd_D1_DBilling.TabIndex = 0;
            this.Cmd_D1_DBilling.Text = "V1";
            this.Cmd_D1_DBilling.UseVisualStyleBackColor = true;
            this.Cmd_D1_DBilling.Click += new System.EventHandler(this.Cmd_D1_DBilling_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TouchPOS.Properties.Resources.chs;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // listBox_Items
            // 
            this.listBox_Items.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Items.FormattingEnabled = true;
            this.listBox_Items.ItemHeight = 22;
            this.listBox_Items.Location = new System.Drawing.Point(22, 172);
            this.listBox_Items.Name = "listBox_Items";
            this.listBox_Items.Size = new System.Drawing.Size(226, 268);
            this.listBox_Items.TabIndex = 1;
            this.listBox_Items.Visible = false;
            this.listBox_Items.SelectedIndexChanged += new System.EventHandler(this.listBox_Items_SelectedIndexChanged);
            // 
            // Chk_Contain
            // 
            this.Chk_Contain.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_Contain.Location = new System.Drawing.Point(301, 694);
            this.Chk_Contain.Name = "Chk_Contain";
            this.Chk_Contain.Size = new System.Drawing.Size(123, 21);
            this.Chk_Contain.TabIndex = 17;
            this.Chk_Contain.Text = "Contains";
            this.Chk_Contain.UseVisualStyleBackColor = true;
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.Chk_Contain);
            this.Controls.Add(this.listBox_Items);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.Chk_SearchByCode);
            this.Controls.Add(this.Txt_Remarks);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.Lbl_Modifier);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.flpItem);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Txt_BarCode);
            this.Controls.Add(this.Lbl_BarCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EntryForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EntryForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.Pnl_DirectB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel flpItem;
        private System.Windows.Forms.Button Cmd_Exit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Cmd_Group;
        private System.Windows.Forms.Button Cmd_ChangePT;
        private System.Windows.Forms.Button Cmd_AddPax;
        private System.Windows.Forms.Button Cmd_Less;
        private System.Windows.Forms.Button Cmd_Add;
        private System.Windows.Forms.Button Cmd_ClearOne;
        private System.Windows.Forms.Button Cmd_Pay;
        private System.Windows.Forms.Button Cmd_Modifier;
        private System.Windows.Forms.Button Cmd_Save;
        private System.Windows.Forms.Button Cmd_ClearAll;
        private System.Windows.Forms.Button Cmd_SubGroup;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox Txt_Item;
        private System.Windows.Forms.Label Lbl_TotAmt;
        private System.Windows.Forms.Label Lbl_Qty;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button Cmd_BackSpace;
        private System.Windows.Forms.Button Button_Qty;
        private System.Windows.Forms.Button Button_Dot;
        private System.Windows.Forms.Button Button_0;
        private System.Windows.Forms.Button Button_3;
        private System.Windows.Forms.Button Button_2;
        private System.Windows.Forms.Button Button_1;
        private System.Windows.Forms.Button Button_6;
        private System.Windows.Forms.Button Button_5;
        private System.Windows.Forms.Button Button_4;
        private System.Windows.Forms.Button Button_9;
        private System.Windows.Forms.Button Button_8;
        private System.Windows.Forms.Button Button_7;
        private Label Lbl_Modifier;
        private Label label8;
        private TextBox Txt_Remarks;
        private DataGridViewTextBoxColumn ItemCode;
        private DataGridViewTextBoxColumn ItemDesc;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Rate;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn POSCode;
        private DataGridViewTextBoxColumn UOM;
        private DataGridViewTextBoxColumn Modifier;
        private DataGridViewTextBoxColumn ChargeCode;
        private DataGridViewTextBoxColumn Slno;
        private DataGridViewTextBoxColumn Autoid;
        private DataGridViewTextBoxColumn OrderNo;
        private DataGridViewTextBoxColumn CanItem;
        private DataGridViewTextBoxColumn Promo;
        private DataGridViewTextBoxColumn Qty2;
        private DataGridViewTextBoxColumn HappyFlag;
        private DataGridViewTextBoxColumn SOrder;
        private DataGridViewTextBoxColumn MCharges;
        private Button Cmd_OptionalPrint;
        private Button Cmd_TransferItem;
        private Label label6;
        private ComboBox Cmb_BSource;
        private Label label7;
        private ComboBox Cmb_Steward;
        private ComboBox Cmb_NCCategory;
        private CheckBox Chk_NCApply;
        private Panel panel6;
        private TextBox Txt_BarCode;
        private Label Lbl_BarCode;
        private CheckBox Chk_SearchByCode;
        private Panel panel7;
        private Panel panel8;
        private Panel panel10;
        private Panel panel9;
        private TextBox text_auto_nw;
        private ListBox listBox_Items;
        private Panel Pnl_DirectB;
        private Button Cmd_D3_DBilling;
        private Button Cmd_D2_DBilling;
        private Button Cmd_D1_DBilling;
        private CheckBox Chk_Contain;
    }
}