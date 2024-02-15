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
    public partial class Taxmaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        public Taxmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(22, 10);
        }

        string sql = "";
        string sqlstring = "";
        string vseqno;

        private void Tablemater_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            Cmb_TaxFlag.SelectedIndex = 1;
            fillGRID();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            FillGrid2();
            //dtp_fromdate.MinDate = DateTime.Now.Date;
            //dtp_todate.MinDate = DateTime.Now.Date;
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
            sql = " SELECT DISTINCT ISNULL(T.TaxCode,'') AS TaxCode,ISNULL(T.Taxdesc,'')AS Taxdesc,";
            sql = sql + " ISNULL(T.Taxpercentage,0) AS Taxpercentage   ";
            sql = sql + " FROM ACCOUNTSTAXMASTER AS T  WHERE  ISNULL(Freezeflag,'') <> 'Y'";
            if (Cmb_TaxFlag.Text == "YES")
            {
                sql = sql + " AND SUBSTRING(typeoftax,2,3) IN ('GST','ESS') AND TAXCODE <> 'CIGCESS' ";
            }
            else
            {
                sql = sql + " AND SUBSTRING(typeoftax,2,3) NOT IN ('GST','ESS') AND TAXCODE <> 'CIGCESS' ";
            }
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

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[3];
                    cbCell.ReadOnly = false;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
            if (txt_taxtype.Text == "")
            {
                MessageBox.Show("Tax code can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_taxtype.Focus();
                MeValidate = true;
                return;
            }
            if (txt_taxdesc.Text == "")
            {
                MessageBox.Show("Tax Desc can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_taxdesc.Focus();
                MeValidate = true;
                return;
            }
            int i;
            DataTable dt = new DataTable();
            int Taxoncount = 0;
            for (i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                string R = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);

                if (R != "")
                {
                    Taxoncount = Taxoncount + 1;
                    
                }
            }
            if (Taxoncount == 0)
            {
                MessageBox.Show("Atleast one Tax Should be Select", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MeValidate = true;
                return;
            }
            if (dtp_fromdate.Value > dtp_todate.Value)
            {
                MessageBox.Show("From Date Should Not Be Greater Than To Date ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MeValidate = true;
                return;
            }
            MeValidate = false;
        }


        private void Cmb_TaxFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGRID();
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DataTable dt = new DataTable();
            checkvalidate();
            string Taxon = "";
            Double TaxPer = 0.00;
            string TaxCode = "";

            if (MeValidate == true)
            { return; }

            sql = "select * from ITEMTYPEMASTER Where ItemTypeCode = '" + txt_taxtype.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "select [dbo].[GetSeqno]('" + txt_taxtype.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }
                sqlstring = "delete from ITEMTYPEMASTER where ItemTypeCode='" + txt_taxtype.Text + "'";
                List.Add(sqlstring);
                sqlstring = "delete from taxitemlink where ItemTypeCode='" + txt_taxtype.Text + "'";
                List.Add(sqlstring);

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value != null)
                    { Taxon = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value); }
                    else { Taxon = ""; }
                    if (Taxon != "")
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value != null)
                        { TaxCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                        else { TaxCode = ""; }
                        if (dataGridView1.Rows[i].Cells[2].Value != null)
                        { TaxPer = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value); }
                        else { TaxPer = 0.00; }
                        sqlstring = "Insert into ITEMTYPEMASTER (ItemTypeCode,ItemTypeseqno,ItemTypeDesc,TaxPercentage,TaxCode,Freeze,StartingDate,EndingDate,AddUserin,AddDateTime,TAXON,GSTFlag) Values ( ";
                        sqlstring = sqlstring + " '" + txt_taxtype.Text + "','" + vseqno + "','" + txt_taxdesc.Text + "'," + TaxPer + ",'" + TaxCode + "','N','" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "', ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + Taxon + "','" + Cmb_TaxFlag.Text + "') ";
                        List.Add(sqlstring);

                        sqlstring = "Insert into TAXITEMLINK (ItemTypeCode,ItemSeqno,TaxCode,TaxPercentage,TAXON,adduser,adddatetime) Values ( ";
                        sqlstring = sqlstring + " '" + txt_taxtype.Text + "','" + vseqno + "','" + TaxCode + "'," + TaxPer + ",'" + Taxon + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "') ";
                        List.Add(sqlstring);
                    }
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    btn_new_Click(sender, e);
                }
            }
            else 
            {
                sql = "select [dbo].[GetSeqno]('" + txt_taxtype.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }
                for (int i = 0; i < dataGridView1.RowCount - 1; i++) 
                {
                    if (dataGridView1.Rows[i].Cells[3].Value != null)
                    { Taxon = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value); }
                    else { Taxon = ""; }
                    if (Taxon != "") 
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value != null)
                        { TaxCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                        else { TaxCode = ""; }
                        if (dataGridView1.Rows[i].Cells[2].Value != null)
                        { TaxPer = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value); }
                        else { TaxPer = 0.00; }
                        sqlstring = "Insert into ITEMTYPEMASTER (ItemTypeCode,ItemTypeseqno,ItemTypeDesc,TaxPercentage,TaxCode,Freeze,StartingDate,EndingDate,AddUserin,AddDateTime,TAXON,GSTFlag) Values ( ";
                        sqlstring = sqlstring + " '" + txt_taxtype.Text + "','" + vseqno + "','" + txt_taxdesc.Text + "'," + TaxPer + ",'" + TaxCode + "','N','" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "', ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + Taxon + "','" + Cmb_TaxFlag.Text + "') ";
                        List.Add(sqlstring);

                        sqlstring = "Insert into TAXITEMLINK (ItemTypeCode,ItemSeqno,TaxCode,TaxPercentage,TAXON,adduser,adddatetime) Values ( ";
                        sqlstring = sqlstring + " '" + txt_taxtype.Text + "','" + vseqno + "','" + TaxCode + "'," + TaxPer + ",'" + Taxon + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "') ";
                        List.Add(sqlstring);
                    }
                }
                if (GCon.Moretransaction(List) > 0) 
                {
                    List.Clear();
                    btn_new_Click(sender, e);
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_taxtype.Text = "";
            txt_taxdesc.Text = "";
            Cmb_freeze.Text = "NO";
            Cmb_TaxFlag.SelectedIndex = 1;
            dtp_fromdate.Value = DateTime.Now.Date;
            dtp_todate.Value = DateTime.Now.Date;
            fillGRID();
            FillGrid2();
            txt_taxtype.Enabled = true;
        }

        public void FillGrid2()
        {
            DataTable ItemType = new DataTable();
            sql = " select ItemTypeCode,ItemTypeDesc,I.TaxPercentage,i.TAXON,I.TaxCode,A.taxdesc,StartingDate,EndingDate from ITEMTYPEMASTER I,accountstaxmaster A Where I.TaxCode = A.taxcode ORDER BY 1,3 ";
            ItemType = GCon.getDataSet(sql);
            if (ItemType.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.ReadOnly = true;
                for (int i = 0; i < ItemType.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = ItemType.Rows[i].ItemArray[0];
                    dataGridView2.Rows[i].Cells[1].Value = ItemType.Rows[i].ItemArray[1];
                    dataGridView2.Rows[i].Cells[2].Value = ItemType.Rows[i].ItemArray[2];
                    dataGridView2.Rows[i].Cells[3].Value = ItemType.Rows[i].ItemArray[3];
                    dataGridView2.Rows[i].Cells[4].Value = ItemType.Rows[i].ItemArray[4];
                    dataGridView2.Rows[i].Cells[5].Value = ItemType.Rows[i].ItemArray[5];
                    dataGridView2.Rows[i].Cells[6].Value = (Convert.ToDateTime(ItemType.Rows[i].ItemArray[6])).ToString("dd-MMM-yyyy");
                    dataGridView2.Rows[i].Cells[7].Value = (Convert.ToDateTime(ItemType.Rows[i].ItemArray[7])).ToString("dd-MMM-yyyy");
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataTable Itemtype = new DataTable();
            string GTaxCode = "",GTaxOn="";
            txt_taxtype.Text = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
            sqlstring = "select ItemTypeCode,ItemTypeDesc,Freeze,GSTFlag,StartingDate,EndingDate,I.TaxPercentage,i.TAXON,I.TaxCode,A.taxdesc from ITEMTYPEMASTER I,accountstaxmaster A  Where I.TaxCode = A.taxcode AND ItemTypeCode = '" + txt_taxtype.Text + "' ";
            Itemtype = GCon.getDataSet(sqlstring);
            if (Itemtype.Rows.Count > 0)
            {
                txt_taxtype.Text = Convert.ToString(Itemtype.Rows[0].ItemArray[0]);
                txt_taxdesc.Text = Convert.ToString(Itemtype.Rows[0].ItemArray[1]);
                Cmb_TaxFlag.Text = Convert.ToString(Itemtype.Rows[0].ItemArray[3]);
                fillGRID();
                if (Convert.ToString(Itemtype.Rows[0].ItemArray[2]) == "Y")
                {
                    Cmb_freeze.Text = "YES";
                }
                else
                {
                    Cmb_freeze.Text = "NO";
                }
                dtp_fromdate.Value = Convert.ToDateTime(Itemtype.Rows[0].ItemArray[4]);
                dtp_todate.Value = Convert.ToDateTime(Itemtype.Rows[0].ItemArray[5]);
                txt_taxtype.Enabled = false;
                for (int i = 0; i < Itemtype.Rows.Count; i++) 
                {
                    GTaxCode = Convert.ToString(Itemtype.Rows[i].ItemArray[8]);
                    GTaxOn = Convert.ToString(Itemtype.Rows[i].ItemArray[7]);
                    for (int j = 0; j < dataGridView1.Rows.Count; j++) 
                    {
                        if (dataGridView1.Rows[j].Cells[0].Value != null)
                        {
                            if (GTaxCode == Convert.ToString(dataGridView1.Rows[j].Cells[0].Value) && GTaxOn != "")
                            {
                                dataGridView1.Rows[j].Cells[3].Value = GTaxOn;
                            }
                        }
                    }
                }
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


