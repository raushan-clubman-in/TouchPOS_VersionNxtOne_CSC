using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace TouchPOS.MASTER
{
    public partial class Tablemater : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        public Tablemater(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        private void Tablemater_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            Txt_Tablecode.Focus();
            fillGRID();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;

            fillGRID();
            fillservicelocation();
            cmb_servicelocation.SelectedIndex = -1;
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;

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

        String Servicelocationcode;
        public void fillservicelocation()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct loccode,LocName from ServiceLocation_Hdr where isnull(void,'')<>'Y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Servicelocationcode = dt.Rows[0]["loccode"].ToString();
                cmb_servicelocation.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_servicelocation.Items.Add(dt.Rows[i]["LocName"].ToString());
                }
                cmb_servicelocation.SelectedIndex = 0;
            }
        }

        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT tableno,POSDESC,Freeze,Isnull(TableOrder,0) as TableOrder FROM TableMaster ";

            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                this.dataGridView1.ReadOnly = true;
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = PosCate.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string loccode;
        private void cmb_servicelocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_servicelocation.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "select distinct loccode,LocName from ServiceLocation_Hdr where locname='" + cmb_servicelocation.Text + "' AND ISNULL(void,'') <> 'Y'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    loccode = dt.Rows[0]["loccode"].ToString();

                }

            }
        }
        string FREEZE;
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Txt_Tablecode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cmb_servicelocation.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Txt_TOrder.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            FREEZE = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();

            if (FREEZE == "N")
            {
                Cmb_freeze.Text = "NO";
            }
            else
            {
                Cmb_freeze.Text = "YES";
            }

            Cmb_freeze.Enabled = true;

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            fillGRID();
            fillservicelocation();
            cmb_servicelocation.SelectedIndex = -1;
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            Txt_Tablecode.Text = "";
            Txt_TOrder.Text = "";
            Txt_Tablecode.Focus();
        }


        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_Tablecode.Text == "")
            {
                MessageBox.Show(" Table  no. cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Tablecode.Focus();
                MeValidate = true;
                return;
            }
            if (cmb_servicelocation.Text == "")
            {
                MessageBox.Show(" Service Location cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_servicelocation.Focus();
                MeValidate = true;
                return;
            }

           
            MeValidate = false;
        }




        string vseqno;
        private void btn_save_Click(object sender, EventArgs e)
        {
            string Fr = "N";
            DataTable dt = new DataTable();

            checkvalidate();

            if (MeValidate == true)
            {
                return;
            }

            sql = "select * from TableMaster  where tableno = '" + Txt_Tablecode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  TableMaster set pos = '" + loccode + "',";
                sql = sql + "Updateuser='" + GlobalVariable.gUserName + "',";
                sql = sql + "updatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',";
                sql = sql + "posdesc='" + cmb_servicelocation.Text + "',";
                sql = sql + "TableOrder=" + Convert.ToInt32(Txt_TOrder.Text) + ",";

                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N'";
                    Fr = "N";
                }
                else
                {
                    sql = sql + "freeze='Y'";
                    Fr = "Y";
                }
                sql = sql + " where tableno = '" + Txt_Tablecode.Text + "'";
                dt = GCon.getDataSet(sql);
                sql = "Update  ServiceLocation_Tables set LocCode = '" + loccode + "',TableOrder=" + Convert.ToInt32(Txt_TOrder.Text) + ",Void = '" + Fr + "' where TableNo = '" + Txt_Tablecode.Text + "' ";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);
            }
            else
            {

                sql = "select [dbo].[GetSeqno]('" + Txt_Tablecode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sqlstring = "INSERT INTO TableMaster (TableNo,TableSeqNo,POS,POSDESC,Freeze,AddUserId,Adddatetime,TableOrder) ";
                sqlstring = sqlstring + " Values ('" + Txt_Tablecode.Text + "','" + vseqno + "','" + loccode + "','" + cmb_servicelocation.Text + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sqlstring = sqlstring + "'N',";
                }
                else
                {
                    sqlstring = sqlstring + "'Y',";
                }
                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "'," + Convert.ToInt32(Txt_TOrder.Text) + ")";
                dt = GCon.getDataSet(sqlstring);

                sqlstring = "Insert into ServiceLocation_Tables (LocCode,TableNo,TableName,TableOrder,Void,AddUserId,AddDateTime,OpenStatus)";
                sqlstring = sqlstring + " Values ('" + loccode + "','" + Txt_Tablecode.Text + "','" + Txt_Tablecode.Text + "'," + Convert.ToInt32(Txt_TOrder.Text) + ",";
                sqlstring = sqlstring + "'N','" + GlobalVariable.gUserName + "',getdate(),'')";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchGrid SG = new SearchGrid(this, dataGridView1, "SELECT tableno,POSDESC,Freeze,Isnull(TableOrder,0) as TableOrder FROM TableMaster", "POSDESC");
            SG.GridCol = dataGridView1.ColumnCount;
            SG.ShowDialog();
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

        private void Txt_TOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            } 
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
    

