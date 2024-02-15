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
using System.Data.SqlClient;

namespace TouchPOS.MASTER
{
    public partial class PromotionMaster : Form
    {
        SqlConnection Myconn = new SqlConnection();
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;

        public PromotionMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }


        string sql = "";
        string sqlstring = "";

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_type.Text == "QTY")
            {
                txt_rate.Visible = false;
                txt_disc.Visible = false;
                txt_qty.Visible = true;
                label10.Text = "Discount Qty";
            }
            else if (cmb_type.Text == "FIXED RATE")
            {

                txt_rate.Visible = true;
                txt_qty.Visible = false;
                txt_disc.Visible = false;
                label10.Text = "Discount Rate";
            }
            else
            {

                txt_rate.Visible = false;
                txt_qty.Visible = false;
                txt_disc.Visible = true;
                label10.Text = "Discount %";
            }
        }

        private void Cmd_UpdTodate_Click(object sender, EventArgs e)
        {
            Grp_ToDate.Visible = true;
        }
        Boolean chkdatevalidate;
        private void Cmb_fromdate_ValueChanged(object sender, EventArgs e)
        {
            DateTime fromdate;
            DateTime todate;
            fromdate = DateTime.Parse(Cmb_fromdate.Text);
            todate = DateTime.Parse(cmb_todate.Text);

            //vdate = todate.Subtract(fromdate);
            if (fromdate.Date > todate.Date)
            {

                MessageBox.Show("From Date cannot be greater than To Date ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmb_todate.Value = Cmb_fromdate.Value;
            }

        }


        private void AutoComplete()
        {
            DataTable dtPosts = new DataTable();
            sql = "SELECT DISTINCT Itemcode,replace(ItemDesc,'''','') as ItemDesc,BaseUOMstd,BASERATESTD FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);

            txt_saleitemcode.AutoCompleteCustomSource = source;
            txt_saleitemcode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_saleitemcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.txt_saleitemcode.DataBindings.Add("Text", dtPosts, "ItemDesc");


            txt_promitemcode.AutoCompleteCustomSource = source;
            txt_promitemcode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_promitemcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.txt_promitemcode.DataBindings.Add("Text", dtPosts, "ItemDesc");

        }


        





        public void fillpos()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT ISNULL(POSDESC,'') AS POSDESC,ISNULL(POSSEQNO,0) AS POSSEQNO FROM POSMaster WHERE ISNULL(Freeze,'') <> 'Y'  ORDER BY POSCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                DB2.Rows.Clear();
                this.DB2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.DB2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.DB2.Columns[0].Width = 130;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DB2.Rows.Add();
                    DB2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    DB2.Rows[i].Cells[0].ReadOnly = true;
                    DB2.Rows[i].Height = 30;
                    //dataGridView1.Rows[i].Cells[1].Value = "NO";
                }
            }
        }


        public void fillUom()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT DISTINCT ISNULL(UOMDESC,'') AS UOMDESC FROM UOMMaster WHERE ISNULL(Freeze,'') <> 'Y' ORDER BY UOMDESC ASC ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                cmb_saleuom.Items.Clear();
                cmb_promuom.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_promuom.Items.Add(dt.Rows[i]["UOMDESC"].ToString());
                    cmb_saleuom.Items.Add(dt.Rows[i]["UOMDESC"].ToString());
                }
                cmb_saleuom.SelectedIndex = 0;
                cmb_promuom.SelectedIndex = 0;
            }
        }

        private void PromotionMaster_Load(object sender, EventArgs e)
        {
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            //Utility.relocate(this, 1368, 768);
            //Utility.repositionForm(this, screenWidth, screenHeight);

            BlackGroupBox();

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.CenterToScreen();

            fillday();

            this.DB2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            this.dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.DB2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersVisible = false;

            fillpos();

            cmb_type.SelectedIndex = 0;

            fillUom();
            groupBox7.Visible = false;
            Maxid();
            dataGridView2.Columns[1].DefaultCellStyle.Format = @"HH\:mm";
            dataGridView2.Columns[2].DefaultCellStyle.Format = @"HH\:mm";
            this.Dtp_EffectiveDate.Value = DateTime.Now;
            this.cmb_todate.Value = DateTime.Now;
            this.Cmb_fromdate.Value = DateTime.Now;
            AutoComplete();
            //AutoComplete2();
            txt_saleitemcode.Text = "";
            cmb_saleuom.SelectedIndex = -1;
            txt_promitemcode.Text = "";
            cmb_promuom.SelectedIndex = -1;
            txt_salerate.Text = "";



            comboBox1.SelectedIndex = 0;


            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////validatedatagrid();
            //dataGridView1.Columns[0].Width = 100;// The id column 
            //dataGridView1.Columns[1].Width = 200;// The abbrevation columln
            fillday();
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

            myGroupBox myGroupBox2 = new myGroupBox();
            myGroupBox2.Text = "SALE ON";
            myGroupBox2.BorderColor = Color.Black;
            myGroupBox2.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox2);

            myGroupBox myGroupBox3 = new myGroupBox();
            myGroupBox3.Text = "PROMOTIONAL ON";
            myGroupBox3.BorderColor = Color.Black;
            myGroupBox3.Size = groupBox4.Size;
            groupBox4.Controls.Add(myGroupBox3);

            myGroupBox myGroupBox4 = new myGroupBox();
            myGroupBox4.Text = "";
            myGroupBox4.BorderColor = Color.Black;
            myGroupBox4.Size = groupBox5.Size;
            groupBox5.Controls.Add(myGroupBox4);

            myGroupBox myGroupBox5 = new myGroupBox();
            myGroupBox5.Text = "";
            myGroupBox5.BorderColor = Color.Black;
            myGroupBox5.Size = groupBox6.Size;
            groupBox6.Controls.Add(myGroupBox5);

            myGroupBox myGroupBox6 = new myGroupBox();
            myGroupBox6.Text = "";
            myGroupBox6.BorderColor = Color.Black;
            myGroupBox6.Size = groupBox8.Size;
            groupBox8.Controls.Add(myGroupBox6);
        }

        
        public string ImageToBase64(Image image,System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, PROMOCODE)),0)+1 as PROMOCODE from prommaster where isnumeric(PROMOCODE) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                txt_promcode.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmb_todate_ValueChanged(object sender, EventArgs e)
        {
            DateTime fromdate;
            DateTime todate;
            fromdate = DateTime.Parse(Cmb_fromdate.Text);
            todate = DateTime.Parse(cmb_todate.Text);
            dataGridView2.Rows.Clear();
            //vdate = todate.Subtract(fromdate);
            if (fromdate.Date > todate.Date)
            {

                MessageBox.Show("From Date cannot be greater than To Date ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmb_todate.Value = Cmb_fromdate.Value;
            }

        }

        public void fillday()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select  upper(DATENAME(weekday, DATEADD(DAY,number,'" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "'))) [Date] from master..spt_values WHERE type = 'P' AND DATEADD(DAY,number,'" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "') <= '" + Strings.Format((DateTime)cmb_todate.Value, "dd-MMM-yyyy") + "' and number<7  order by number";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].ValueType = typeof(DateTime);
                this.dataGridView2.Columns[2].ValueType = typeof(DateTime);
                this.dataGridView2.Columns[0].Width = 250;
                //this.dataGridView2.Columns[1].DefaultCellStyle.Format="HH:mm";
                //this.dataGridView2.Columns[2].DefaultCellStyle.Format = "HH:mm";
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    dataGridView2.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        private void txt_saleitemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();

                sql = "SELECT ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(ITEMDESC,'') AS ITEMDESC,BaseUOMstd FROM ITEMMASTER WHERE  ";
                sql = sql + " ItemDesc = '" + txt_saleitemcode.Text.Replace("'", "''") + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    txt_saleitemcode.Text = dt.Rows[0]["ITEMDESC"].ToString();
                    cmb_saleuom.Text = dt.Rows[0]["BaseUOMstd"].ToString();

                }

            }


        }

        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " select distinct PROMOCODE,promodesc,freeze from prommaster ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();

                this.dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView3.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView3.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];

                }
            }
        }

        public void fillGRIDitem()
        {
            DataTable PosCate = new DataTable();
            if (comboBox1.Text == "PROMOCODE")
            {
            sql = " select distinct PROMOCODE,promodesc,freeze from prommaster where PROMOCODE like'%" + Txt_Modifier.Text + "%' ";
            }
            else if (comboBox1.Text == "PROMODESC")
            {
                sql = " select distinct PROMOCODE,promodesc,freeze from prommaster where PROMODESC like'%" + Txt_Modifier.Text + "%' ";
            }
            else
            {
                sql = " select distinct PROMOCODE,promodesc,freeze from prommaster where PROMODESC like'%" + Txt_Modifier.Text + "%' ";
         
            }
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();

                this.dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView3.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView3.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];

                }
            }
        }


        private void txt_promitemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                sql = "SELECT ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(ITEMDESC,'') AS ITEMDESC ,ISNULL(BASEUOMSTD,'') AS BASEUOMSTD,ISNULL(BASERATESTD,0) AS BASERATESTD FROM ITEMMASTER WHERE ITEMDESC =  '" + txt_promitemcode.Text.Replace("'", "''") + "' AND ISNULL(FREEZE,'') <> 'Y' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {

                    txt_promitemcode.Text = dt.Rows[0]["ITEMDESC"].ToString();
                    cmb_promuom.Text = dt.Rows[0]["BaseUOMstd"].ToString();
                    txt_salerate.Text = dt.Rows[0]["BASERATESTD"].ToString();

                    cmb_promuom.Enabled = false;
                    cmb_promuom.DropDownStyle = ComboBoxStyle.DropDownList;
                }

            }
        }



        private void txt_saleqty_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void txt_salerate_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void txt_maxqty_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            Maxid();
            
            
            pictureBox1.Image = null; 
            cmb_type.SelectedIndex = 0;
            //AutoComplete();
            fillUom();
            Txt_Modifier.Text = "";
            txt_saleqty.Text = "";
            btn_save.Text = "Save";
            txt_saleitemcode.Text = "";
            cmb_saleuom.SelectedIndex = -1;
            txt_promitemcode.Text = "";
            cmb_promuom.SelectedIndex = -1;
            txt_salerate.Text = "";
            txt_maxqty.Text = "";
            dataGridView2.Refresh();
            dataGridView3.Refresh();
            DB2.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            Txt_promname.Text = "";
            txt_disc.Text = "";
            txt_rate.Text = "";
            txt_qty.Text = "";
            fillpos();


            this.Dtp_EffectiveDate.Value = DateTime.Now;
            this.cmb_todate.Value = DateTime.Now;
            this.Cmb_fromdate.Value = DateTime.Now;
            fillday();
        }

        private void Label14_Click(object sender, EventArgs e)
        {
            fillday();
        }
        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();

            MeValidate = false;
            if (Txt_promname.Text == "")
            {
                MessageBox.Show(" Promotional Name cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_promname.Focus();
                MeValidate = true;
                return;
            }

            if (txt_maxqty.Text == "")
            {
                txt_maxqty.Focus();
                txt_maxqty.Text = "0.00";
                MeValidate = false;

            }
            
            if (txt_rate.Text == "")
            {
                txt_rate.Focus();
                txt_rate.Text = "0.00";
                MeValidate = false;
                
            }
            
            if (txt_qty.Text == "")
            {
                txt_qty.Focus();
                txt_qty.Text = "0.00";
                MeValidate = false;
                
            }
            if (txt_disc.Text == "")
            {
                txt_disc.Focus();
                txt_disc.Text = "0.00";
                MeValidate = false;
               
            }
            if (txt_saleitemcode.Text == "")
            {
                MessageBox.Show(" Sale Item Name cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_promname.Focus();
                MeValidate = true;
                return;
            }
            if (txt_saleqty.Text == ""  )
            {
                MessageBox.Show(" Sale Qty cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_promname.Focus();
                MeValidate = true;
                return;
            }

            if (txt_promitemcode.Text == "")
            {
                MessageBox.Show(" Base Item Name cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_promname.Focus();
                MeValidate = true;
                return;
            }

            sql = "SELECT DISTINCT Itemcode,ItemDesc as ItemDesc FROM ItemMaster WHERE ItemDesc='" + txt_saleitemcode.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show(" Sale Item is Not Exists in Itemmaster", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_saleitemcode.Focus();
                MeValidate = true;
                return;
            }

            sql = "SELECT DISTINCT Itemcode,ItemDesc as ItemDesc FROM ItemMaster WHERE ItemDesc='" + txt_promitemcode.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show(" Base Item is Not Exists in Itemmaster", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_promitemcode.Focus();
                MeValidate = true;
                return;
            }

            int i, j;
            var posList = new List<string>();

            for (i = 0; i <= DB2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(DB2.Rows[i].Cells[1].Value) == true))
                {
                    posList.Add(DB2.Rows[i].Cells[0].Value.ToString());
                }
            }


            if (posList.Count == 0)
            {
                MessageBox.Show("Kindly Select Minimum One Pos Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                MeValidate = true;
                return;
            }
            MeValidate = false;


        }

        public SqlConnection getConn()
        {
            SqlConnection Con = new SqlConnection(Myconn.ConnectionString);
            return Con;
        }

        String BASEDON, pos1;
        private void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Myconn.ConnectionString);
            string vseqno;
            ArrayList List = new ArrayList();
            DateTime vDate;

            DataTable dtSb = new DataTable();
            int status;

            if (txt_promcode.Text != "")
            {
                checkvalidate();

                if (MeValidate == true)
                {
                    return;
                }
                DataTable dt = new DataTable();

                sql = "select [dbo].[GetSeqno]('" + txt_promcode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sql = "select * FROM prommaster  WHERE promocode='" + txt_promcode.Text + "'";

                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    if (btn_save.Text == "Freeze")
                    {


                        {
                            sql = "UPDATE  PromMaster ";
                            sql = sql + " SET FREEZE= 'Y',UPDATEUSER='" + GlobalVariable.gUserName + " ', voiddate='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "'";
                            sql = sql + " WHERE promocode = '" + txt_promcode.Text + "'";
                            dt = GCon.getDataSet(sql);
                            

                            btn_new_Click(sender, e);

                        }

                    }


                }
                else
                {

                    //***********************************************************************
                    sql = "select upper(isnull(promodesc,''))as promodesc  from prommaster ";
                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {

                            string s = (dt.Rows[k][0].ToString());
                            string p = Txt_promname.Text;

                            if (s == p)
                            {
                                MessageBox.Show("Promotional Already Exist ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MeValidate = true;
                                return;
                            }
                        }

                    }
                    //************************************
                    int i, j;
                    string poschecked;

                    if (cmb_type.Text == "QTY")
                    {
                        BASEDON = "Q";
                    }
                    else if (cmb_type.Text == "DISCOUNT ")
                    {
                        BASEDON = "D";
                    }
                    else if (cmb_type.Text == "FIXED RATE")
                    {
                        BASEDON = "F";
                    }
                    else
                    {
                        BASEDON = "P";
                    }

                    if (pictureBox1.Image != null)
                    {

                        sql = "INSERT INTO PROMPICTURE (PROMOCODE) VALUES ('" + txt_promcode.Text + "')";
                        List.Add(sql);
                       
                    }



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

                        for (j = 0; j <= dataGridView2.RowCount - 1; j++)
                        {
                            string Fromtime = "";
                            string totime = "";
                            string days = dataGridView2.Rows[j].Cells[0].Value.ToString();
                            Fromtime = Convert.ToDateTime(dataGridView2.Rows[j].Cells[1].Value).ToString("HH:mm");
                            totime = Convert.ToDateTime(dataGridView2.Rows[j].Cells[2].Value).ToString("HH:mm");

                            sql = "INSERT INTO PROMMASTER    (POSCODE,PROMOCODE,PROMODATE,PROMODESC,BASEDON,BASEDITEMCODE,basename,";
                            sql = sql + "BASEDUOM,SALEQTY,MaxOnProm,PITEMCODE,PITEMDESC,PUOM,SALERATE,FREEQTY,DISCOUNT,Discountprice,WDAY,FROMTIME,TOTIME,FROMDATE,TODATE,FREEZE,ADDUSER,ADDDATETIME) Values";
                            sql = sql + "('" + pos1 + "','" + txt_promcode.Text + "','" + Strings.Format((DateTime)Dtp_EffectiveDate.Value, "dd-MMM-yyyy") + "','" + Txt_promname.Text + "','" + BASEDON + "','" + ITEMCODE + "','" + txt_saleitemcode.Text + "','" + cmb_saleuom.Text + "','" + txt_saleqty.Text + "','" + txt_maxqty.Text + "','" + ITEMCODE + "','" + txt_promitemcode.Text + "','" + cmb_promuom.Text + "','" + txt_salerate.Text + "','" + txt_qty.Text + "','" + txt_disc.Text + "','" + txt_rate.Text + "','" + days + "','" + Fromtime + "','" + totime + "','" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)cmb_todate.Value, "dd-MMM-yyyy") + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "')";

                            List.Add(sql);
                        }

                    }

                    


                        if (GCon.Moretransaction(List) > 0)
                    {
                               List.Clear();
                            if (pictureBox1.Image != null)
                    {

                                
                                byte[] arr;
                                ImageConverter converter = new ImageConverter();
                                arr = (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[]));
                        sql = "update prompicture set pimage='" + arr + "' where PROMOCODE='" + txt_promcode.Text + "'";


                        //byte[] img = ImageToBase64(pictureBox1.Image,System.Drawing.Imaging.ImageFormat.Png);
                        //GCon.SaveImage("update prompicture set pimage =@user_image where PROMOCODE='" + txt_promcode.Text + "'", pictureBox1.Image, "@user_image", con);
                        List.Add(sql);
                        if (GCon.Moretransaction(List) > 0)
                        {
                            List.Clear();

                            MessageBox.Show("Data Added Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                        List.Clear();
                        btn_new_Click(sender, e);
                    }
                    


                }


            }
        }


        //private byte[] ImageToStream(string fileName)
        //{
        //    MemoryStream stream = new MemoryStream();
        //tryagain:
        //    try
        //    {
        //        Bitmap image = new Bitmap(fileName);
        //        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    }
        //    catch (Exception ex)
        //    {
        //        goto tryagain;
        //    }

        //    return stream.ToArray();
        //}

        private void btn_edit_Click(object sender, EventArgs e)
        {
            btn_save.Text = "Freeze";
            fillGRID();
            groupBox7.Visible = true;
           // this.Enabled = false;
        }

        private void Cmd_Save_Click(object sender, EventArgs e)
        {
             DataTable dt = new DataTable();
             if (Txt_promname.Text == "")
             {
                 MessageBox.Show(" Promotional Name cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 Txt_promname.Focus();
                 MeValidate = true;
                 return;
             }
             if (MessageBox.Show("ARE U SURE, TO CHANGE TO DATE FOR THIS PROMOTIONAL", Application.ProductName, MessageBoxButtons.OKCancel) == DialogResult.OK)
             {

                 if (txt_promcode.Text != "" && Txt_promname.Text != "")
                 {

                     sqlstring = "UPDATE  PromMaster ";
                     sqlstring = sqlstring + " SET ToDate= '" + Strings.Format((DateTime)dtp_chdate.Value, "dd-MMM-yyyy") + "'";
                     sqlstring = sqlstring + " WHERE promocode = '" + txt_promcode.Text + "'";
                     dt = GCon.getDataSet(sqlstring);

                     MessageBox.Show("Date Updated Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                     btn_new_Click(sender, e);
                 } 

             }
           
   }
        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            txt_promcode.ReadOnly = false;
           
                Text = this.dataGridView3.CurrentRow.Cells[0].Value.ToString();
                txt_promcode.Text = Text;
            
            groupBox7.Visible = false;
            this.Enabled = true;
            txt_promcode.ReadOnly = false;
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            btn_new_Click(sender, e);
        }

        private void txt_promcode_TextChanged(object sender, EventArgs e)
        {
             txt_promcode_Validated(sender, e);
        }

        private void txt_promcode_Validated(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dts = new DataTable();
            if (txt_promcode.Text != "")
            {
                sqlstring = "SELECT ISNULL(PROMODESC,'') AS PROMODESC,ISNULL(PItemDesc,'') AS PItemDesc,isnull(PROMODATE,'') as PROMODATE,isnull(BASEDON,'') AS BASEDON,ISNULL(BASEDITEMCODE,'') AS BASEDITEMCODE,ISNULL(basename,'') AS basename,isnull(saleqty,0) as saleqty, ISNULL(PITEMCODE,'') AS PITEMCODE,ISNULL(BASEDUOM,'') AS BASEDUOM,ISNULL(PUOM,'') AS PUOM,isnull(MaxonProm,0) as MaxonProm,";
                sqlstring = sqlstring + "ISNULL(PItemDesc,'') AS PItemDesc,ISNULL(FROMQTY,0) AS FROMQTY,ISNULL(TOQTY,0) AS TOQTY,ISNULL(FREEQTY,0) AS FREEQTY,ISNULL(ADDUSER,'') AS ADDUSER,ADDDATETIME,ISNULL(FROMDATE,'') AS FROMDATE,ISNULL(TODATE,'') AS TODATE,ISNULL(FROMTIME,'') AS FROMTIME,ISNULL(TOTIME,'') AS TOTIME,isnull(wday,'') as  wday,isnull(salerate,0) as salerate,isnull(discount,0) as discount,isnull(fixedrate,0) as fixedrate,isnull(Discountprice,0) as Discountprice,isnull(freeze,'') as freeze,isnull(voiddate,'') as voiddate  FROM PROMMASTER";
                sqlstring = sqlstring + " WHERE  ISNULL(PROMOCODE,'') = '" + txt_promcode.Text + "'";
                dt = GCon.getDataSet(sqlstring);
                if (dt.Rows.Count > 0)
                {
                    Txt_promname.Text = dt.Rows[0]["PROMODESC"].ToString();
                    Dtp_EffectiveDate.Text = dt.Rows[0]["PROMODATE"].ToString();

                    Cmb_fromdate.Text = dt.Rows[0]["FROMDATE"].ToString();
                    cmb_todate.Text = dt.Rows[0]["TODATE"].ToString();

                    cmb_type.Text = dt.Rows[0]["BASEDON"].ToString();
                    string T = dt.Rows[0]["BASEDON"].ToString();
                    string F = dt.Rows[0]["freeze"].ToString();
                    if (F == "Y")
                    {
btn_save.Enabled=false;
                    }
                    else
                    {
                        btn_save.Enabled = true;
                    }

                    if (T == "Q")
                    {
                        cmb_type.SelectedIndex = 0;
                        txt_rate.Visible = false;
                        txt_qty.Visible = true;
                        txt_qty.Text = dt.Rows[0]["FREEQTY"].ToString();
                        txt_disc.Visible = false;
                        label10.Text = "Discount Qty";
                    }
                    else if (T == "F")
                    {
                        cmb_type.SelectedIndex = 1;

                        txt_rate.Visible = true;
                        txt_rate.Text = dt.Rows[0]["Discountprice"].ToString();
                        txt_qty.Visible = false;
                        txt_disc.Visible = false;
                        label10.Text = "Discount Rate";
                    }
                    else if (T == "D")
                    {
                        cmb_type.SelectedIndex = 2;
                        txt_rate.Visible = false;
                        txt_qty.Visible = false;
                        txt_disc.Visible = true;
                        txt_disc.Text = dt.Rows[0]["Discount"].ToString();
                        label10.Text = "Discount %";
                    }
                    else
                    {
                        cmb_type.SelectedIndex = 0;
                        txt_rate.Visible = false;
                        txt_disc.Visible = false;
                        txt_qty.Visible = true;
                        label10.Text = "Discount Qty";
                    }
                    txt_saleitemcode.Text = dt.Rows[0]["basename"].ToString();
                    cmb_saleuom.Text = dt.Rows[0]["BASEDUOM"].ToString();
                    txt_salerate.Text = dt.Rows[0]["salerate"].ToString();
                    txt_saleqty.Text = dt.Rows[0]["saleqty"].ToString();
                    string ITEM =dt.Rows[0]["PItemDesc"].ToString();
                    txt_promitemcode.Text = dt.Rows[0]["PItemDesc"].ToString();
                    cmb_promuom.Text = dt.Rows[0]["PUOM"].ToString();
                    txt_maxqty.Text = dt.Rows[0]["MaxonProm"].ToString();

                    sql = "SELECT DISTINCT POSCODE FROM PROMMASTER WHERE PROMOCODE='" + txt_promcode.Text + "'";
                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            pos1 = "";
                            sql = "select posdesc from posmaster where poscode='" + (dt.Rows[i][0].ToString()) + "'";
                            dts = GCon.getDataSet(sql);
                            if (dts.Rows.Count > 0)
                            {
                                pos1 = dts.Rows[0][0].ToString();
                            }
                            for (int j = 0; j <= DB2.RowCount - 1; j++)
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

                    sql = "SELECT DISTINCT WDAY,FROMTIME,TOTIME FROM PROMMASTER WHERE PROMOCODE='" + txt_promcode.Text + "'";

                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.Columns[1].DefaultCellStyle.Format = @"hh\:mm";
                        dataGridView2.Columns[2].DefaultCellStyle.Format = @"hh\:mm";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                            
                            dataGridView2.Rows[i].Cells[1].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[1]).ToString("HH:mm");
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[2]).ToString("HH:mm");
                            //dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                        }

                    }
                    DataSet ds = new DataSet();
                    sql = "select pimage from PROMPICTURE WHERE PROMOCODE='" + txt_promcode.Text + "'";

                    GCon.getDataSet1(sql, "PROMPICTURE");



                    if (GlobalVariable.gdataset.Tables["PROMPICTURE"].Rows.Count > 0)
                    {
                        ////    byte[] getImg = new byte[0];

                        int count = GlobalVariable.gdataset.Tables["PROMPICTURE"].Rows.Count;

                        var data = (Byte[])GlobalVariable.gdataset.Tables["PROMPICTURE"].Rows[count - 1]["pimage"];
                        var stream = new MemoryStream(data);
                        Random rnd = new Random();
                            string vOutfile = (("Pho" + (rnd.Next() * 800000).ToString()).Substring( 1, 8));
                            string VFilePath = Application.StartupPath + "\\Reports\\" + vOutfile + ".JPG";
                       if (Convert.ToBoolean(File.Exists(VFilePath) == true ))
                        {
                       File.Delete(VFilePath);
                        }
                       Bitmap myBitmap = (Bitmap)Bitmap.FromStream(stream);
                       myBitmap.Save(VFilePath);
                       myBitmap.Dispose();
                        pictureBox1.Image = Image.FromStream(stream);

                    }

                    




                }
            }  
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        string ITEMCODE;
        private void txt_promitemcode_TextChanged(object sender, EventArgs e)
        {
            if (txt_promitemcode.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "SELECT ITEMCODE  FROM ITEMMASTER WHERE ITEMDESC='" + txt_promitemcode.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    ITEMCODE = dt.Rows[0]["ITEMCODE"].ToString();

                }

            }
        }

       

        private void txt_disc_KeyPress(object sender, KeyPressEventArgs e)
        {
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

       
        
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            
pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = new Bitmap(open.FileName);

            } 
        
    }

        private void txt_saleqty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string time = Convert.ToDateTime((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()).ToString("HH:mm:ss");
    string reg = "(([0-1][0-9])|([2][0-3])):([0-5][0-9]):([0-5][0-9])";
    Regex regex = new Regex(reg);
    bool isValid = regex.IsMatch(time);
    if (!isValid)
    {
        MessageBox.Show("please enter Valid Time");
    }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("please enter Valid Time");

        }


        private void Txt_Modifier_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Modifier.Text!="")
            {
                fillGRIDitem();
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