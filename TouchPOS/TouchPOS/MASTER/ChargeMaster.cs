using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.MASTER
{
    public partial class ChargeMaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;

        public ChargeMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(22, 10);
        }

        string sql = "";
        string sqlstring = "";
        string vseqno;
        Boolean MeValidate = false;

        private void ChargeMaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillGrid2();
            FillTaxType();
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void FillGrid2()
        {
            DataTable ItemType = new DataTable();
            sql = " select CHARGECODE,CHARGEDESC,TAXTYPECODE,TAXTYPEDESC from CHARGEMASTER ";
            ItemType = GCon.getDataSet(sql);
            if (ItemType.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.ReadOnly = true;
                for (int i = 0; i < ItemType.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = ItemType.Rows[i].ItemArray[0];
                    dataGridView2.Rows[i].Cells[1].Value = ItemType.Rows[i].ItemArray[1];
                    dataGridView2.Rows[i].Cells[2].Value = ItemType.Rows[i].ItemArray[2];
                    dataGridView2.Rows[i].Cells[3].Value = ItemType.Rows[i].ItemArray[3];
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        public void FillTaxType()
        {
            DataTable dt = new DataTable();
            sql = "select DISTINCT ItemTypeCode,ItemTypeDesc from ITEMTYPEMASTER Where isnull(Freeze,'') <> 'Y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_Type.Items.Clear();
                Cmb_Type.DisplayMember = "ItemTypeDesc";
                Cmb_Type.ValueMember = "ItemTypeCode";
                Cmb_Type.DataSource = dt;
                Cmb_Type.SelectedIndex = 0;
            }
        }

        private void Cmb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Typeid, TypeName;
            DataRowView drv = (DataRowView)Cmb_Type.SelectedItem;
            Typeid = drv["ItemTypeCode"].ToString();
            TypeName = drv["ItemTypeDesc"].ToString();

            DataTable TaxCode = new DataTable();
            sql = " select i.TaxCode,a.taxdesc,i.TaxPercentage from ITEMTYPEMASTER I,accountstaxmaster A Where I.TaxCode = a.taxcode And ItemTypeCode = '" + Typeid + "'  ";
            TaxCode = GCon.getDataSet(sql);
            if (TaxCode.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.ReadOnly = true;
                for (int i = 0; i < TaxCode.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = TaxCode.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = TaxCode.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = TaxCode.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataTable ChargeMaster = new DataTable();
            Txt_Code.Text = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
            sqlstring = "select CHARGECODE,CHARGEDESC,TAXTYPECODE,TAXTYPEDESC from CHARGEMASTER Where CHARGECODE = '" + Txt_Code.Text + "'";
            ChargeMaster = GCon.getDataSet(sqlstring);
            if (ChargeMaster.Rows.Count > 0)
            {
                Txt_Code.Text = Convert.ToString(ChargeMaster.Rows[0].ItemArray[0]);
                Txt_Name.Text = Convert.ToString(ChargeMaster.Rows[0].ItemArray[1]);
                Cmb_Type.Text = Convert.ToString(ChargeMaster.Rows[0].ItemArray[3]);
                Txt_Code.Enabled = false;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Txt_Code.Text = "";
            Txt_Code.Enabled = true;
            Txt_Name.Text = "";
            Cmb_Type.SelectedIndex = 0;
            FillGrid2();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            checkvalidate();

            if (MeValidate == true)
            { return; }

            string Typeid, TypeName;
            DataRowView drv = (DataRowView)Cmb_Type.SelectedItem;
            Typeid = drv["ItemTypeCode"].ToString();
            TypeName = drv["ItemTypeDesc"].ToString();

            sql = "Select * from CHARGEMASTER  where CHARGECODE = '" + Txt_Code.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  CHARGEMASTER Set CHARGEDESC = '" + Txt_Name.Text + "',TAXTYPECODE = '" + Typeid + "',TAXTYPEDESC = '" + TypeName + "',";
                sql = sql + "UPDATEUSER='" + GlobalVariable.gUserName + "',";
                sql = sql + "UPDATEDATETIME=getdate() ";
                sql = sql + " where CHARGECODE = '" + Txt_Code.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
            else 
            {
                sqlstring = "INSERT INTO CHARGEMASTER (CHARGECODE,CHARGEDESC,RATE,TAXTYPECODE,TAXTYPEDESC,ADDUSER,ADDDATETIME) ";
                sqlstring = sqlstring + " Values ('" + Txt_Code.Text + "','" + Txt_Name.Text + "',0,'" + Typeid + "','" + TypeName + "',";
                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "',getdate())";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
        }

        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_Code.Text == "")
            {
                MessageBox.Show(" Code can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Code.Focus();
                MeValidate = true;
                return;
            }
            if (Txt_Name.Text == "")
            {
                MessageBox.Show(" Name can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Name.Focus();
                MeValidate = true;
                return;
            }
            MeValidate = false;
        }

        public class myGroupBox : GroupBox
        {

            private Color borderColor;

            public Color BorderColor
            {

                get { return this.borderColor; }

                set { this.borderColor = value; }

            }



            public myGroupBox()
            {

                this.borderColor = Color.Black;

            }



            protected override void OnPaint(PaintEventArgs e)
            {

                Size tSize = TextRenderer.MeasureText(this.Text, this.Font);



                Rectangle borderRect = e.ClipRectangle;

                borderRect.Y += tSize.Height / 2;

                borderRect.Height -= tSize.Height / 2;

                ControlPaint.DrawBorder(e.Graphics, borderRect, this.borderColor, ButtonBorderStyle.Solid);



                Rectangle textRect = e.ClipRectangle;

                textRect.X += 6;

                textRect.Width = tSize.Width;

                textRect.Height = tSize.Height;

                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);

                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);

            }

        }
    }
}
