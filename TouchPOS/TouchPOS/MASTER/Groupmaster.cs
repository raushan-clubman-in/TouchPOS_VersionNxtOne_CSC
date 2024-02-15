

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TouchPOS.MASTER
{
    public partial class Groupmaster : Form
    {
       
        public readonly MastersForm _form1;

        public Groupmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }


         GlobalClass GCon = new GlobalClass();
        string vseqno;
        string sql = "";
        string sqlstring = "";
        private void Groupmaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            dataGridView1.ReadOnly = true;
            fillGRID();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;

            Maxid();
            Cmb_freeze.SelectedIndex = -1;
            Cmb_category.SelectedIndex = -1;
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct categoryName from poscategoryMaster where isnull(Freeze,'')<>'y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_category.Items.Clear();
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    Cmb_category.Items.Add(dt.Rows[i]["categoryName"].ToString());
                }
                Cmb_category.SelectedIndex = 0;
            }

            txt_groupdesc.Focus();
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
            sql = " SELECT GroupCode,GroupDesc,Category,Freeze FROM GROUPMASTER ";
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            Maxid();
            fillGRID();
            Cmb_category.Text = "";
            txt_groupdesc.Text = "";
            txt_groupdesc.Focus();

            Cmb_category.SelectedIndex = -1;

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;

        }



        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (txt_groupdesc.Text == "")
            {
                MessageBox.Show(" Group Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_groupdesc.Focus();
                MeValidate = true;
                return;
            }
            if (Cmb_category.Text == "")
            {
                MessageBox.Show(" Category cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_category.Focus();
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

            sql = "select * from groupmaster where groupcode = '" + Txt_groupcode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  groupmaster set groupdesc = '" + txt_groupdesc.Text + "',Category='" + Cmb_category.Text + "', Updateuser='" + GlobalVariable.gUserName + "',updatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N'";
                }
                else
                {
                    sql = sql + "freeze='Y'";
                }

               sql = sql +" where groupcode = '" + Txt_groupcode.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
                
            }
            else
            {

                sql = "select isnull(groupdesc,'')as groupdesc  from groupmaster ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = txt_groupdesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("Group Already Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }

                sql = "select [dbo].[GetSeqno]('" + Txt_groupcode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }


                sqlstring = "Insert Into groupmaster (Groupcode,Groupseqno,Groupdesc,ShortName,Category,Freeze,AddUserId,AddDateTime) ";
                sqlstring = sqlstring + " Values ('" + Txt_groupcode.Text + "','"+ vseqno +"','" + txt_groupdesc.Text + "','','" + Cmb_category.Text + "',";

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


        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, groupCode)),0)+1  as groupCode from GROUPMASTER where isnumeric(groupCode) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Txt_groupcode.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        { 

        }

        private void btn_edit_Click(object sender, System.EventArgs e)
        {
            Txt_groupcode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_groupdesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();

            Cmb_category.Text = "";
            Cmb_category.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            FREEZE = dataGridView1.CurrentRow.Cells[3].Value.ToString();
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
        String FREEZE;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Txt_groupcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchGrid SG = new SearchGrid(this, dataGridView1, "SELECT GroupCode,GroupDesc,Category,Freeze FROM GROUPMASTER", "GroupDesc,Category");
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
       
    }
}
