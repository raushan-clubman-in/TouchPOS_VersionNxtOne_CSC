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
    public partial class UomMaster : Form
    {
        
public readonly MastersForm _form1;

public UomMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        
         GlobalClass GCon = new GlobalClass();
        string sql = "";
        string sqlstring = "";


        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_uomdesc.Text == "")
            {
                MessageBox.Show("  Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_uomdesc.Focus();
                MeValidate = true;
                return;
            }

            if (Cmb_freeze.Text == "")
            {
                Cmb_freeze.Focus();
                Cmb_freeze.Text = "NO";
                MeValidate = false;
            }

            MeValidate = false;
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            checkvalidate();

            if (MeValidate == true)
            {
                return;
         
            }

            sql = "select * from UOMMASTER where UOMDESC = '" + Txt_uomdesc.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  UOMMASTER set UOMDESC = '" + Txt_uomdesc.Text + "',";
                 sql = sql + "UOMCODE = '" + Txt_uomdesc.Text + "',";
                sql = sql + "Updateuser='" + GlobalVariable.gUserName + "',";
                sql = sql + "updatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',";
                
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N'";
                }
                else
                {
                    sql = sql + "freeze='Y'";
                }
                sql = sql + " where UOMDESC = '" + Txt_uomdesc.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);
            }
            else
            {

                sqlstring = "INSERT INTO UOMMASTER (UOMCODE,UOMDESC,Freeze,AddUser,AddDateTime) ";
                sqlstring = sqlstring + " Values ('" + Txt_uomdesc.Text + "','" + Txt_uomdesc.Text + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sqlstring = sqlstring + "'N',";
                }
                else
                {
                    sqlstring = sqlstring + "'Y',";
                }
                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);
            }
        }


        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT uomdesc,Freeze FROM Uommaster ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;             
                this.dataGridView1.ReadOnly = true;
               
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }
        private void UomMaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            fillGRID();
            Txt_uomdesc.Focus();
            dataGridView1.ReadOnly = true;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            //GCon.OpenConnection();

            Cmb_freeze.SelectedIndex = -1;
            Maxid();

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

        public void Maxid()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();


            sql = "Select isnull(MAX(CAST(UOMCODE AS INT)),0)+1 as UOMCODE  FROM UOMMASTER where isnumeric(UOMCODE) = 1 ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                Txt_uomcode.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            fillGRID();
            Txt_uomdesc.Text = "";
            Txt_uomdesc.Focus();
            Txt_uomdesc.Focus();

            dataGridView1.ReadOnly = true;
            DataTable dtS = new DataTable();

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            Maxid();
        }
        string freeze;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Txt_disccode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            //Txt_uomcode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_uomdesc.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();


            freeze = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();

            if (freeze == "N")
            {
                Cmb_freeze.Text = "NO";
            }
            else
            {
                Cmb_freeze.Text = "YES";
            }
            Cmb_freeze.Enabled = true;
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
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
