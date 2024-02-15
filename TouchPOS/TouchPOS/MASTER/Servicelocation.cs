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

namespace TouchPOS.MASTER
{
    public partial class Servicelocation : Form
    {
        public readonly MastersForm _form1;
        public Servicelocation(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }


        GlobalClass GCon = new GlobalClass();

        string sql = "";
        string sqlstring = "";
        private void rdb_dinein_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_dinein.Checked == true)
            {
                rdb_hd.Checked = false;
                rdb_takeaway.Checked = false;
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdb_takeaway_CheckedChanged(object sender, EventArgs e)
        {

           
            
            if (rdb_takeaway.Checked == true)
            {
                rdb_hd.Checked = false;
                rdb_dinein.Checked = false;
            }
        }

        private void rdb_hd_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rdb_hd.Checked == true)
            {
                rdb_dinein.Checked = false;
                rdb_takeaway.Checked = false;
            }

        }

        private void Servicelocation_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            fillpos();
            
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            fillGRID();
            this.DB2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dataGridView1.RowHeadersVisible = false;
            DB2.RowHeadersVisible = false;

            Maxid();
            showHideColumns(false);
            CMB_S.SelectedIndex = 0;
            cmb_scv.SelectedIndex = -1;
            Cmb_Freeze.SelectedIndex = 1;
            Cmb_Freeze.Enabled = false;
            Cmb_Freeze.DropDownStyle = ComboBoxStyle.DropDownList;
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
            DataTable dtS = new DataTable();

            dtS = new DataTable();
            sql = "Select isnull(MAX(CAST(LocCode AS INT)),0)+1 as LocCode  FROM ServiceLocation_Hdr where isnumeric(LocCode) = 1";
            dtS = GCon.getDataSet(sql);
            if (dtS.Rows.Count > 0)
            {

                Txt_slcode.Text = dtS.Rows[0].ItemArray[0].ToString();
            }
        }
        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " select LocCode,LocName,SERVICEFLAG,SCardValidate,void,kotprefix,billprefix,APPLIED_TO from ServiceLocation_Hdr  ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              
                
                this.dataGridView1.ReadOnly = true;

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];                    
                    dataGridView1.Rows[i].Cells[3].Value = PosCate.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = PosCate.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[5].Value = PosCate.Rows[i].ItemArray[5];
                    dataGridView1.Rows[i].Cells[6].Value = PosCate.Rows[i].ItemArray[6];
                    dataGridView1.Rows[i].Cells[7].Value = PosCate.Rows[i].ItemArray[7];
                    dataGridView1.Rows[i].Height = 30;
                }
                
            }
        }

        private void showHideColumns(Boolean colHideStatus)
        {
            dataGridView1.Columns[2].Visible = colHideStatus;
            dataGridView1.Columns[3].Visible = colHideStatus;

            dataGridView1.Columns[5].Visible = colHideStatus;
            dataGridView1.Columns[6].Visible = colHideStatus;
            dataGridView1.Columns[7].Visible = colHideStatus;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (Txt_slname.Text == "")
            {
                MessageBox.Show("  Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_slname.Focus();
                MeValidate = true;
                return;
            }
            if (cmb_scv.Text == "")
            {

                Txt_slname.Focus();
                MeValidate = false;
                return;
            }

            if (Cmb_Freeze.Text == "")
            {
                Cmb_Freeze.Focus();
                Cmb_Freeze.Text = "NO";
                MeValidate = false;
            }
            if (txt_kot.Text == "")
            {
                MessageBox.Show(" Kot Prefix cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_kot.Focus();
                MeValidate = true;
                return;
            }
            if (txt_finalbill.Text == "")
            {
                MessageBox.Show(" Bill Prefix cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_finalbill.Focus();
                MeValidate = true;
                return;
            }




               int i, j;
            string posstr= "";
                    var posList = new List<string>();
            
                    for (i = 0; i <= DB2.RowCount - 1; i++)
                    {
                            if ((Convert.ToBoolean(DB2.Rows[i].Cells[1].Value) == true))
                        {
                            posList.Add(DB2.Rows[i].Cells[0].Value.ToString());
                            posstr = posstr + "'" + DB2.Rows[i].Cells[0].Value.ToString() + "',";
                        }
                    }


                    if (posList.Count == 0)
                    {
                        MessageBox.Show("Kindly Select Minimum One Pos Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Txt_slname.Focus();
                        MeValidate = true;
                        return;
                    }
                         else if (posList.Count == 1)
                        {
                         MeValidate = false;
                        }
                    else if (posList.Count > 1)
                        {

                            posstr = posstr.Substring(0, posstr.Length - 1);

                           
                            sql = "select itemcode from posmenulink where pos in (" + posstr + ") group by itemcode having count(itemcode) > 1";
                            dt = GCon.getDataSet(sql);
                            if (dt.Rows.Count > 0)
                            {
                            //    string itemcode = posstr = posstr + "'" + DB2.Rows[i].Cells[0].Value.ToString() + "',";
                                MessageBox.Show("Duplicate Item Is Present So You Cant Tag With Multiple Pos   ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MeValidate = true;
                                return;
                            }
                        }

                    
                    


            MeValidate = false;
        }


        public void fillpos()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC,ISNULL(POSSEQNO,0) AS POSSEQNO FROM POSMaster WHERE ISNULL(Freeze,'') <> 'Y'  ORDER BY POSCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                this.DB2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.DB2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DB2.Rows.Add();
                    DB2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[1].ToString();
                    DB2.Rows[i].Cells[0].ReadOnly = true;
                    //dataGridView2.Rows[i].Cells[1].Value = "NO";
                    DB2.Rows[i].Height = 30;
                }
            }
        }



        private void btn_new_Click(object sender, EventArgs e)
        {

            txt_finalbill.ReadOnly = false;
            txt_kot.ReadOnly = false;
            dataGridView1.Refresh();
            DB2.Refresh();
            dataGridView1.Rows.Clear();
            DB2.Rows.Clear();
            fillpos();
            Maxid();
            dataGridView1.ReadOnly = true;
            fillGRID();
            Txt_slname.Focus();
            txt_finalbill.Text = "";
            txt_kot.Text = "";
            Cmb_Freeze.Enabled = true;
            Txt_slname.Text = "";            
            cmb_scv.SelectedIndex = -1;
            Cmb_Freeze.SelectedIndex = 1;
            Cmb_Freeze.Enabled = false;
            CMB_S.SelectedIndex = 0;
            Cmb_Freeze.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            {
                ArrayList List = new ArrayList();
                DataTable dt = new DataTable();

                checkvalidate();

                if (MeValidate == true)
                {
                    return;
                }

                if (rdb_dinein.Checked = false)
                {
                    DataTable dtS = new DataTable();

                    dtS = new DataTable();
                    if (rdb_hd.Checked = true)
                    {
                        sql = "Select SERVICEFLAG  FROM ServiceLocation_Hdr where SERVICEFLAG in('H')and LocCode not in  ('" + Txt_slcode.Text + "')";
                    }
                    else if (rdb_takeaway.Checked = true)
                    {
                        sql = "Select SERVICEFLAG  FROM ServiceLocation_Hdr where SERVICEFLAG in('T')and LocCode not in  ('" + Txt_slcode.Text + "')";
                    }
                    dtS = GCon.getDataSet(sql);
                    if (dtS.Rows.Count > 0)
                    {
                        if (rdb_hd.Checked = true)
                        {
                            MessageBox.Show("Already Location Has Created For Home Delivery You Can't Create It Again!!! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (rdb_takeaway.Checked = true)
                        {
                            MessageBox.Show("Already Location Has Created For Takeaway You Can't Create It Again!!! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                }

                sql = "select * from ServiceLocation_Hdr where LocCode = '" + Txt_slcode.Text + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "Update  ServiceLocation_Hdr set Locname = '" + Txt_slname.Text + "',";

                    sql = sql + "Updateuser='" + GlobalVariable.gUserName + "',";
                    sql = sql + "UpdateDateTime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',";
                    sql = sql + "KotPrefix='" + txt_kot.Text + "',";
                    sql = sql + "BillPrefix='" + txt_finalbill.Text + "',";
                    sql = sql + "APPLIED_TO='" + CMB_S.Text + "',";
                    if (Cmb_Freeze.Text == "NO")
                    {
                        sql = sql + " void='N',";
                    }
                    else
                    {
                        sql = sql + "void='Y',";
                    }
                    if (cmb_scv.Text == "NO")
                    {
                        sql = sql + " SCardValidate='NO',";
                    }
                    else
                    {
                        sql = sql + "SCardValidate='YES',";
                    }
                    if (rdb_dinein.Checked == true)
                    {
                        sql = sql + "SERVICEFLAG='D'";
                    }
                    else if (rdb_hd.Checked == true)
                    {
                        sql = sql + "SERVICEFLAG='H'";
                    }
                    else if (rdb_takeaway.Checked == true)
                    {
                        sql = sql + "SERVICEFLAG='T'";
                    }
                    else
                    {
                        sql = sql + "SERVICEFLAG='D'";
                    }
                    sql = sql + " where LocCode = '" + Txt_slcode.Text + "'";
                    dt = GCon.getDataSet(sql);

                    sql = "delete from [ServiceLocation_Det] where loccode='" + Txt_slcode.Text + "'";
                    dt = GCon.getDataSet(sql);
                    posdet();

                    MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    btn_new_Click(sender, e);

                }
                else
                {


                    sql = "select isnull(kotprefix,'')as kotprefix ,isnull(billprefix,'')billprefix from ServiceLocation_Hdr ";
                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {

                            string s = (dt.Rows[k][0].ToString());

                            string t = (dt.Rows[k][1].ToString());
                            string p = txt_kot.Text;
                            string q = txt_finalbill.Text;
                            if (s == p)
                            {
                                MessageBox.Show("Kot Prefix Already Present kindly Change", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MeValidate = true;
                                return;
                            }
                            if (t == q)
                            {
                                MessageBox.Show("bill Prefix Already Present kindly Change", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MeValidate = true;
                                return;
                            }


                        }
                    }


                    sqlstring = "INSERT INTO ServiceLocation_Hdr (LocName,SCardValidate,SERVICEFLAG,KotPrefix,BillPrefix,APPLIED_TO,AddUserId,AddDateTime) ";
                    sqlstring = sqlstring + " Values ('" + Txt_slname.Text + "',";
                    
                    if (cmb_scv.Text == "NO")
                    {
                        sqlstring = sqlstring + "'NO',";
                    }
                    else
                    {
                        sqlstring = sqlstring + "'YES',";
                    }
                    if (rdb_dinein.Checked == true)
                    {
                        sqlstring = sqlstring + "'D',";
                    }
                    else if (rdb_hd.Checked == true)
                    {
                        sqlstring = sqlstring + "'H',";
                    }
                    else if (rdb_takeaway.Checked == true)
                    {
                        sqlstring = sqlstring + "'T',";
                    }
                    else
                    {
                        sqlstring = sqlstring + "'D',";
                    }
                    sqlstring = sqlstring + "'" + txt_kot.Text + "','" + txt_finalbill.Text + "','"+ CMB_S.Text +"','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                    List.Add(sqlstring);
                   
                    
                    if (GCon.Moretransaction(List) > 0)
                    {
                        MessageBox.Show("Data Added Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        posdet();
                        btn_new_Click(sender, e);
                    }

                }
            }


        }
        string maxid1;
        private void posdet()
        {

            DataTable dtS = new DataTable();
            
            dtS = new DataTable();
            sql = "Select  LocCode  FROM ServiceLocation_Hdr where LocNAME ='"+Txt_slname.Text+"' ";
            dtS = GCon.getDataSet(sql);
            if (dtS.Rows.Count > 0)
            {

                maxid1 = dtS.Rows[0].ItemArray[0].ToString();
            }
            DataTable dtSb = new DataTable();
            ArrayList List = new ArrayList();
                         int i, j;
                    var posList = new List<string>();
                    for (i = 0; i <= DB2.RowCount - 1; i++)
                    {
                        if ((Convert.ToBoolean(DB2.Rows[i].Cells[1].Value) == true))
                        {
                            posList.Add(DB2.Rows[i].Cells[0].Value.ToString());
                        }
                    }
                    

                    foreach (string pos in posList)
                    {

                        pos1 = "";
                        sql = "select poscode from posmaster where posdesc='" + pos + "'";
                        dtSb = GCon.getDataSet(sql);
                        if (dtSb.Rows.Count > 0)
                        {
                            pos1 = dtSb.Rows[0]["poscode"].ToString();
                        }
                        

                            sql = "INSERT INTO ServiceLocation_Det (LOCCODE,PosCode,VOID,ADDUSERID,AddDateTime)";
                            sql = sql + "values('" + maxid1 + "','" + pos1 + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "')";
                            List.Add(sql);
                    }
                    

                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                    }   
                    
}
        string freeze,scv,sflag;

        private void btn_edit_Click(object sender, EventArgs e)
        {
            
            Txt_slcode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_slname.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_kot.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_finalbill.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            CMB_S.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txt_finalbill.ReadOnly = true;
            txt_kot.ReadOnly = true;

            sflag = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            if (sflag == "D")
            {
                rdb_dinein.Checked = true;
            }
            else if (sflag == "H")
            {
                rdb_hd.Checked = true;
            }
            else if (sflag == "T")
            {
                rdb_takeaway.Checked = true;
            }
            scv = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if (scv == "NO")
            {
                cmb_scv.Text = "NO";
            }
            else
            {
                cmb_scv.Text = "YES";
            }
            freeze = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();

            if (freeze == "N")
            {
                Cmb_Freeze.Text = "NO";
            }
            else
            {
                Cmb_Freeze.Text = "YES";
            }

            Cmb_Freeze.Enabled = true;
        }

        private void Txt_slcode_TextChanged(object sender, EventArgs e)
        {
            Txt_slcode_Validated(sender, e);
        }
        string pos1 ;
        private void Txt_slcode_Validated(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            DataTable dts = new DataTable();
            if (Txt_slcode.Text != "")
            {
                sql = "SELECT DISTINCT POSCODE FROM [ServiceLocation_Det] WHERE Loccode='" + Txt_slcode.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pos1 = "";
                        sql = "select posdesc from posmaster where poscode='" + (dt.Rows[i][0].ToString()) +"'";
                        dts = GCon.getDataSet(sql);
                        if (dts.Rows.Count > 0)
                        {
                            pos1 = dts.Rows[0][0].ToString();
                        }
                        for (int j = 0; j <= DB2.RowCount - 2; j++)
                        {
                            string s = pos1;
                            string p = DB2.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)DB2.Rows[j].Cells[1];
                                chkbox.Value = true;
                            }
                        }

                    }
                }
            }
        }


        private void txt_kot_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
   
        }

        private void txt_finalbill_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
   
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
