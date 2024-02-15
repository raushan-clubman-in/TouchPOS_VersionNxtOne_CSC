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
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TouchPOS.MASTER
{
    public partial class Discountmaster : Form
    {

        public readonly MastersForm _form1;

        public Discountmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }


        GlobalClass GCon = new GlobalClass();

        string sql = "";
        string sqlstring = "";

        private void Discountmaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            Txt_discdesc.Focus();
            dataGridView1.ReadOnly = true;
            fillGRID();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //GCon.OpenConnection();
            this.dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Txt_discdesc.Focus();

            Maxid();
            filluser();

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            Cmb_DiscountType.SelectedIndex = 0;
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

        public void filluser()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "select distinct username  from Master..UserAdmin";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.RowHeadersVisible = false;
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    dataGridView2.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_discdesc.Text == "")
            {
                MessageBox.Show("  Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_discdesc.Focus();
                MeValidate = true;
                return;
            }
            if (txt_descper.Text == "")
            {
                txt_descper.Text = "0.00";
                txt_descper.Focus();
                MeValidate = false;
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
        public void Maxid()
        {
            DataTable dtS = new DataTable();

            dtS = new DataTable();
            sql = "Select isnull(MAX(CAST(DISCOUNTID AS INT)),0)+1 as DISCOUNTID  FROM DISCOUNTEDUSERLIST where isnumeric(DISCOUNTID) = 1";
            dtS = GCon.getDataSet(sql);
            if (dtS.Rows.Count > 0)
            {
                Txt_disccode.Text = dtS.Rows[0].ItemArray[0].ToString();
            }
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ArrayList List = new ArrayList();
            checkvalidate();

            if (MeValidate == true)
            {
                return;
            }

            if (Cmb_freeze.Text == "YES")
            {
                sqlstring = "UPDATE  DISCOUNTEDUSERLIST SET FREEZE='Y' WHERE  ISNULL(FREEZE,'')<>'Y'AND DISCOUNTID='" + Txt_disccode.Text + "'";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Data Freezed Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
                return;
            }
            var userList = new List<string>();
            for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[1].Value) == true))
                {
                    userList.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }

            }
            sql = "select * from DISCOUNTEDUSERLIST where DISCOUNTID = '" + Txt_disccode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (string user in userList)
                {
                    sqlstring = "UPDATE  DISCOUNTEDUSERLIST SET FREEZE='Y' WHERE  ISNULL(FREEZE,'')<>'Y'AND DISCOUNTID='" + Txt_disccode.Text + "'";
                    dt = GCon.getDataSet(sqlstring);
                    sql = "INSERT INTO DISCOUNTEDUSERLIST (DISCOUNTID,DISCOUNTNAME,USERNAME,DISCPERCENT,DiscType,Freeze,AddUserId,AddDateTime) ";
                    sql = sql + " Values ('" + Txt_disccode.Text + "','" + Txt_discdesc.Text + "','" + user + "','" + txt_descper.Text + "','" + Cmb_DiscountType.Text + "',";
                    if (Cmb_freeze.Text == "NO")
                    {
                        sql = sql + "'N',";
                    }
                    else
                    {
                        sql = sql + "'Y',";
                    }
                    sql = sql + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                    List.Add(sql);
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    MessageBox.Show("Data Updated Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    btn_new_Click(sender, e);

                }
                //dt = GCon.getDataSet(sql);
                //MessageBox.Show("Data Updated Sucssfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sql = "select upper(isnull(DISCOUNTNAME,''))as DISCOUNTNAME  from DISCOUNTEDUSERLIST ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = Txt_discdesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("Discount Already Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }

                foreach (string user in userList)
                {
                    sqlstring = "INSERT INTO DISCOUNTEDUSERLIST (DISCOUNTID,DISCOUNTNAME,USERNAME,DISCPERCENT,DiscType,Freeze,AddUserId,AddDateTime) ";
                    sqlstring = sqlstring + " Values ('" + Txt_disccode.Text + "','" + Txt_discdesc.Text + "','" + user + "','" + txt_descper.Text + "','" + Cmb_DiscountType.Text + "',";
                    if (Cmb_freeze.Text == "NO")
                    {
                        sqlstring = sqlstring + "'N',";
                    }
                    else
                    {
                        sqlstring = sqlstring + "'Y',";
                    }
                    sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                    List.Add(sqlstring);
                }

                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    MessageBox.Show("Data Added Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_new_Click(sender, e);
                }



            }

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Txt_disccode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_discdesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_descper.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            freeze = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (freeze == "N")
            {
                Cmb_freeze.Text = "NO";
            }
            else
            {
                Cmb_freeze.Text = "YES";
            }
            Cmb_DiscountType.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString(); ;
            Cmb_freeze.Enabled = true;
        }
        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT DISTINCT Discountid,DISCOUNTNAME,DISCPERCENT,Freeze,Isnull(DiscType,'FIXED PERCENTAGE') as DiscType FROM DISCOUNTEDUSERLIST Where ISNULL(Freeze,'') <> 'Y'  ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.ReadOnly = true;

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = PosCate.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = PosCate.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }


        private void btn_new_Click(object sender, EventArgs e)
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();
            filluser();
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            fillGRID();
            Maxid();

            txt_descper.Text = "";
            Txt_discdesc.Text = "";
            Txt_discdesc.Focus();
            Cmb_DiscountType.SelectedIndex = 0;
            dataGridView1.ReadOnly = true;
            DataTable dtS = new DataTable();

            dtS = new DataTable();

            sql = "Select isnull(MAX(CAST(DISCOUNTID AS INT)),0)+1 as DISCOUNTID  FROM DISCOUNTEDUSERLIST";
            dtS = GCon.getDataSet(sql);
            if (dtS.Rows.Count > 0)
            {
                Txt_disccode.Text = dtS.Rows[0].ItemArray[0].ToString();
            }
        }
        string freeze;


        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_descper_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_descper_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar)
        && !char.IsDigit(e.KeyChar)
        && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point 
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Txt_disccode_TextChanged(object sender, EventArgs e)
        {
            Txt_disccode_Validated(sender, e);
        }

        private void Txt_disccode_Validated(object sender, EventArgs e)
        {
            if (Txt_disccode.Text != "")
            {
                DataTable dt = new DataTable();
                sql = "SELECT  DISTINCT username FROM DISCOUNTEDUSERLIST WHERE DISCOUNTID='" + Txt_disccode.Text + "'and isnull(freeze,'')<>'Y'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                        {
                            string s = (dt.Rows[i][0].ToString());
                            string p = dataGridView2.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[1];
                                chkbox.Value = true;
                            }
                        }

                    }
                }
            }
        }

        private void Cmb_DiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_DiscountType.Text == "FIXED PERCENTAGE")
            {
                txt_descper.Enabled = true;
            }
            else 
            {
                txt_descper.Enabled = false;
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
