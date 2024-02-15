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
    public partial class Kitchenmaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        public Kitchenmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        private void Tablemater_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
             Maxid();
             txt_kitchendesc.Focus();
            fillGRID();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            fillGRID();
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

        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT Kitchencode,Kitchenname,Freeze FROM Kitchenmaster ";

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
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        string FREEZE;
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Txt_Kitchencode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_kitchendesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            


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
        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, kitchencode)),0)+1 as kitchencode from kitchenmaster where isnumeric(kitchencode) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                Txt_Kitchencode.Text = dt.Rows[0].ItemArray[0].ToString();
                Txt_Kitchencode.ReadOnly = true;
            }
        }
        private void btn_new_Click(object sender, EventArgs e)
        {
            fillGRID();
            txt_kitchendesc.Text = "";
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            Txt_Kitchencode.Text = "";
            Txt_Kitchencode.Focus();
            Maxid();
        }


        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_Kitchencode.Text == "")
            {
                MessageBox.Show(" Kitchen Code can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Kitchencode.Focus();
                MeValidate = true;
                return;
            }
            
            if (txt_kitchendesc.Text == "")
            {
                MessageBox.Show("Kitchen Name can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_kitchendesc.Focus();
                MeValidate = true;
                return;
            }

           
            MeValidate = false;
        }




        string vseqno;
        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            checkvalidate();

            if (MeValidate == true)
            {
                return;
            }

            sql = "select * from kitchenmaster  where kitchencode = '" + Txt_Kitchencode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  kitchenmaster set kitchenName = '" + txt_kitchendesc.Text + "',";
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
                sql = sql + " where kitchencode = '" + Txt_Kitchencode.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                btn_new_Click(sender, e);

            }
            else
            {

                sql = "select upper(isnull(kitchenName,''))as kitchenName  from kitchenmaster ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = txt_kitchendesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("kitchen  Already Exist ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }
               
                sql = "select [dbo].[GetSeqno]('" + Txt_Kitchencode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sqlstring = "INSERT INTO kitchenmaster (kitchencode,kitchenSeqno,kitchenName,Freeze,AddUser,Adddatetime) ";
                sqlstring = sqlstring + " Values ('" + Txt_Kitchencode.Text + "','" + vseqno + "','" + txt_kitchendesc.Text + "',";
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
    

