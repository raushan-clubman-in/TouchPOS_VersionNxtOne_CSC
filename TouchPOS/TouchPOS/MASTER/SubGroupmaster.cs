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
    public partial class SubGroupmaster : Form
    {
        
        
        public readonly MastersForm _form1;

        public SubGroupmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }
        GlobalClass GCon = new GlobalClass();
        string sql = "";
        string sqlstring = "";
        string Groupcode;
        string vseqno;

        private void SubGroupmaster_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tMA1819DataSet2.SUBGROUPMASTER' table. You can move, or remove it, as needed.
            BlackGroupBox();
            fillGRID();
            dataGridView1.ReadOnly = true;
            Txt_Subgroupdesc.Focus();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            //GCon.OpenConnection();

            //Cmb_freeze.SelectedIndex = -1;
            Cmb_group.SelectedIndex = -1;
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct Groupdesc from Groupmaster where isnull(Freeze,'')<>'y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_group.Items.Clear();
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    Cmb_group.Items.Add(dt.Rows[i]["Groupdesc"].ToString());
                }
                Cmb_group.SelectedIndex = 0;
            }

            maxid();

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
            sql = " SELECT subGroupCode,subGroupDesc,GroupDesc,Freeze FROM SUBGROUPMASTER ";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string FREEZE;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (Txt_Subgroupdesc.Text == "")
            {
                MessageBox.Show(" Group Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Subgroupdesc.Focus();
                MeValidate = true;
                return;
            }
            if (Cmb_group.Text == "")
            {
                MessageBox.Show(" Category cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_group.Focus();
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

            sql = "select * from subgroupmaster where subgroupcode = '" + Txt_Subgroupcode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  subgroupmaster set subGroupdesc = '" + Txt_Subgroupdesc.Text + "',groupcode='" + Groupcode + "',Groupdesc='"+ Cmb_group.Text +"',Updateuser='" + GlobalVariable.gUserName + "',updatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N'";
                }
                else
                {
                    sql = sql + "freeze='Y'";
                }
                sql = sql + " where subgroupcode = '" + Txt_Subgroupcode.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);
            }
            else
            {


                dt = new DataTable();

                sql = "select isnull(subgroupdesc,'')as subgroupdesc  from subgroupmaster ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = Txt_Subgroupdesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("SubGroup Already Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }
                sql = "Select Groupcode  FROM groupmaster where Groupdesc= '" + Cmb_group.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    Groupcode = dt.Rows[0].ItemArray[0].ToString();
                }

                sql = "select [dbo].[GetSeqno]('" + Txt_Subgroupcode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sqlstring = "INSERT INTO subgroupmaster (Groupcode,Groupseqno,Groupdesc,ShortName,Freeze,AddUserId,AddDateTime,subgroupcode,subGroupdesc) ";
                sqlstring = sqlstring + " Values ('" + Groupcode + "','" + vseqno + "','" + Cmb_group.Text + "','',";
             if (Cmb_freeze.Text == "NO")
                {
                    sqlstring = sqlstring + "'N',";
                }
                else
                {
                    sqlstring = sqlstring + "'Y',";
                }
                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + Txt_Subgroupcode.Text + "','" + Txt_Subgroupdesc.Text + "')";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                btn_new_Click(sender, e);
            }
        }
        public void maxid()
        {
            DataTable dtS = new DataTable();

            dtS = new DataTable();
            sql = "Select isnull(MAX(CAST(subgroupCode AS INT)),0)+1 as subgroupCode  FROM SUBGROUPMASTER where isnumeric(subgroupCode)=1";

            dtS = GCon.getDataSet(sql);
            if (dtS.Rows.Count > 0)
            {
                Txt_Subgroupcode.Text = dtS.Rows[0].ItemArray[0].ToString();
            }
        }
        private void btn_new_Click(object sender, EventArgs e)
        {
            Txt_Subgroupdesc.Focus();

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            fillGRID();
            Cmb_group.SelectedIndex = -1;
            Cmb_group.Text = "";
            Txt_Subgroupdesc.Text = "";
            Txt_Subgroupdesc.Focus();

            dataGridView1.ReadOnly = true;
            maxid(); 
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Txt_Subgroupcode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_Subgroupdesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();

            Cmb_group.Text = "";
            Cmb_group.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            FREEZE = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();

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

        private void button1_Click(object sender, EventArgs e)
        {
            SearchGrid SG = new SearchGrid(this, dataGridView1, "SELECT subGroupCode,subGroupDesc,GroupDesc,Freeze FROM SUBGROUPMASTER", "subGroupDesc,GroupDesc");
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
