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
    public partial class Categotymaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;

        public Categotymaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        string vseqno;

        public void fillgrid()
        {
            dataGridView1.ReadOnly = true;

            DataTable PosCate = new DataTable();
            PosCate = new DataTable();
            sql = " SELECT CATEGORYCODE,CategoryName,Freeze FROM poscategorymaster ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              
                this.dataGridView1.ReadOnly = true;
               
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                }
            }
        }



        private void Categotymaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            fillgrid();
            
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            //GCon.OpenConnection();
            Txt_Categorydesc.Focus();
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
        sql = "select Isnull(max(Convert(int, CATEGORYCODE)),0)+1 as CATEGORYCODE  from poscategorymaster where isnumeric(CATEGORYCODE) = 1";

        dt = GCon.getDataSet(sql);
        if (dt.Rows.Count > 0)
        {
            Txt_Categorycode.Text = dt.Rows[0].ItemArray[0].ToString();
        }

    }


        public void fillcategory()
        {
       
            DataTable PosCate = new DataTable();
            sql = " SELECT CATEGORYCODE,CategoryName,Freeze FROM poscategorymaster ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }


        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (Txt_Categorydesc.Text == "")
            {
                MessageBox.Show(" Group Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Categorydesc.Focus();
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

            checkvalidate();
            if (MeValidate == true)
            {
                return;
            } 
            DataTable dt = new DataTable();
           

            sql = "select * from poscategorymaster where Categorycode = '" + Txt_Categorycode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  poscategorymaster set CategoryName = '" + Txt_Categorydesc.Text + "',Updateuser='" + GlobalVariable.gUserName + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N',";
                }
                else
                {
                    sql = sql + "freeze='Y',";
                }
                
             sql = sql +   " updatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' where categorycode = '" + Txt_Categorycode.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                btn_new_Click(sender, e);
            }
            else
            {

                sql = "select upper(isnull(CategoryName,''))as CategoryName  from poscategorymaster ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = Txt_Categorydesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("Category Already Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }

                sql = "select [dbo].[GetSeqno]('" + Txt_Categorycode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }
                sqlstring = "INSERT INTO poscategorymaster (categoryCode,categoryName,categorySeqno,Freeze,Adduser,Adddatetime) ";
                sqlstring = sqlstring + " Values ('" + Txt_Categorycode.Text + "','" + Txt_Categorydesc.Text + "','" + vseqno + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sqlstring = sqlstring + "'N',";
                }
                else
                {
                    sqlstring = sqlstring + "'Y',";
                }
                  sqlstring = sqlstring + "  '" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);

            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
        //    dataGridView1.TabIndex = 0;
            Txt_Categorycode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_Categorydesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
            fillcategory();
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
          
            Txt_Categorycode.Text = "";
            Txt_Categorydesc.Text = "";
            Txt_Categorydesc.Focus();
            dataGridView1.ReadOnly = true;
            Maxid();
        }
        String FREEZE;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
