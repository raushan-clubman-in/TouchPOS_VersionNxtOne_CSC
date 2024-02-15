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
    public partial class HappyhourMaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        public HappyhourMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        string po;


        public void fillday()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select  upper(DATENAME(weekday, DATEADD(DAY,number,'" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "'))) [Date] from master..spt_values WHERE type = 'P' AND DATEADD(DAY,number,'" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "') <= '" + Strings.Format((DateTime)cmb_todate.Value, "dd-MMM-yyyy") + "' and number<7  order by number";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.RowHeadersVisible = false;
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].ValueType = typeof(DateTime);
                this.dataGridView2.Columns[2].ValueType = typeof(DateTime);
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
        public void fillpos()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC,ISNULL(POSSEQNO,0) AS POSSEQNO FROM POSMaster WHERE ISNULL(Freeze,'') <> 'Y'  ORDER BY POSCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                DB1.Rows.Clear();
                DB1.RowHeadersVisible = false;
                this.DB1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.DB1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.DB1.Columns[0].Width = 150;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DB1.Rows.Add();
                    DB1.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[1].ToString();
                    DB1.Rows[i].Cells[2].Value = dt.Rows[i].ItemArray[0].ToString();
                    DB1.Rows[i].Cells[0].ReadOnly = true;
                    DB1.Rows[i].Height = 30;
                    //dataGridView1.Rows[i].Cells[1].Value = "NO";
                }
            }
        }


        public void fillgroup()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "select distinct groupcode,GroupDesc from groupmaster WHERE ISNULL(Freeze,'') <> 'Y'";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();
                dataGridView3.RowHeadersVisible = false;
                this.dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //this.dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[0].Width = 250;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[1].ToString();
                    dataGridView3.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView3.Rows[i].Cells[1].Value = false;
                    dataGridView3.Rows[i].Height = 30;
                }
            }
        }

        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, docno)),0)+1 as docno from HappyLink where isnumeric(docno) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                txt_docno.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }


        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT DISTINCT docno,CAST(AddDate AS DATE) AS AddDate,Void FROM HappyLink ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView5.Rows.Clear();
                dataGridView5.RowHeadersVisible = false;
                this.dataGridView5.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView5.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView5.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView5.Rows.Add();
                    dataGridView5.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView5.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView5.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    dataGridView5.Rows[i].Height = 30;
                }
            }
        }
        private void showHideColumns(Boolean colHideStatus)
        {
            DB1.Columns[2].Visible = colHideStatus;

        }
        
            
        private void HappyhourMaster_Load(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            BlackGroupBox();
            Maxid();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            fillday();
            fillgroup();
            showHideColumns(false);
           //dataGridView1.Columns[0].Width = 100;
            this.DB1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.dataGridView3.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.dataGridView4.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dataGridView4.RowHeadersVisible = false;
            
            fillpos();

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;

            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            myGroupBox2.Text = "";
            myGroupBox2.BorderColor = Color.Black;
            myGroupBox2.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox2);

            myGroupBox myGroupBox3 = new myGroupBox();
            myGroupBox3.Text = "";
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
        }

        private void Label14_Click(object sender, EventArgs e)
        {
            fillday();
        }

        public void FILLITEM()
        {
            int rowid = dataGridView3.CurrentRow.Index;
            if ((Convert.ToBoolean(dataGridView3.Rows[rowid].Cells[1].Value) == true))
            {
                dataGridView3.Rows[rowid].Cells[1].Value = false;
            }
            else { dataGridView3.Rows[rowid].Cells[1].Value = true; }
            dataGridView4.Refresh();
            dataGridView4.Rows.Clear();
            int i;
            string groupstr = "";
            string posstr = "";
            var grouplist = new List<string>();
            var poslist = new List<string>();
            for (i = 0; i <= dataGridView3.RowCount-1 ; i++)
            {
                if ((Convert.ToBoolean(dataGridView3.Rows[i].Cells[1].Value) == true))
                {
                    grouplist.Add(dataGridView3.Rows[i].Cells[1].Value.ToString());
                    groupstr = groupstr + "'" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "',";
                }
            }

            if (grouplist.Count >= 1)
            {
                groupstr = groupstr.Substring(0, groupstr.Length - 1);
            }
            else
            {

            }

            for (i = 0; i <= DB1.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(DB1.Rows[i].Cells[1].Value) == true))
                {
                    poslist.Add(DB1.Rows[i].Cells[1].Value.ToString());
                    posstr = posstr + "'" + DB1.Rows[i].Cells[2].Value.ToString() + "',";
                }
            }

            if (grouplist.Count >= 1)
            {
                posstr = posstr.Substring(0, posstr.Length - 1);
            }
            else
            {

            }
            sqlstring = "";
            DataTable dt = new DataTable();
            dt = new DataTable();
            if (grouplist.Count >= 1)
            {
                sqlstring = " select distinct UPPER(itemdesc),a.itemcode from ItemMaster a inner join posmenulink b on a.itemcode=b.itemcode where groupcodedec in (" + groupstr + ") and b.pos in(" + posstr + ")and ISNULL(Freeze,'') <> 'Y'";
             }
            else
            {
                sqlstring = "select distinct UPPER(itemdesc),a.itemcode from ItemMaster a inner join posmenulink b on a.itemcode=b.itemcode where groupcodedec ='abc' and b.pos ='abc' and ISNULL(Freeze,'') <> 'Y'";
            }
            //dataGridView4.Rows.Clear();
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                
                this.dataGridView4.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView4.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                for ( i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add();
                    dataGridView4.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    dataGridView4.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView4.Rows[i].Height = 30;
                }
            }

        }
        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int i;
        

        

        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {          
            FILLITEM();  
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            sql = "select DOCNO from HAPPYLINK where DOCNO='" + txt_docno.Text + "'AND VOID='Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                MessageBox.Show(" Freezed item We Cant Update it Again..", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "select DOCNO from HAPPYLINK where DOCNO='" + txt_docno.Text + "'";
            dt = GCon.getDataSet(sql);

            if (dt.Rows.Count > 0)
            {

                checkvalidate();

                if (MeValidate == true)
                {
                    return;
                } 
                sql = "Update HAPPYLINK SET VOID='Y' WHERE DOCNO='" + txt_docno.Text + "'";
                dt = GCon.getDataSet(sql);
                if (Cmb_freeze.Text == "NO")
                {
                    Saveoperation();
                }
                else 
                {
                    sql = "Update HAPPYLINK SET VOID='Y' WHERE DOCNO='" + txt_docno.Text + "'";
                }
            }
            else
            {
                checkvalidate();

                if (MeValidate == true)
                {
                    return;
                } 
                Saveoperation();
            }
            btn_new_Click(sender, e);
        }

        public void Saveoperation()
        {
            DataTable dt = new DataTable();
            ArrayList List = new ArrayList();
            var posList = new List<string>();
            for (i = 0; i <= DB1.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(DB1.Rows[i].Cells[1].Value) == true))
                
                {
                    posList.Add(DB1.Rows[i].Cells[0].Value.ToString());
                }

            }

            foreach (string pos in posList)
            {

                sql = "select poscode from posmaster where posdesc='" + pos + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    po = dt.Rows[0]["poscode"].ToString();
                }
                for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                {

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
                        string ITEM = row.Cells[0].Value.ToString();
                        string Icode = "";
                        string gcode = "";
                        string gdesc = "";
                        sql = "select itemcode,groupcode,groupcodedec from itemmaster where itemdesc='" + ITEM + "'";
                        dt = GCon.getDataSet(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Icode = dt.Rows[0]["itemcode"].ToString();
                            gcode = dt.Rows[0]["groupcode"].ToString();
                            gdesc = dt.Rows[0]["groupcodedec"].ToString();
                        }

                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            string PERC = row.Cells[2].Value.ToString();
                            sql = "select DOCNO from HAPPYLINK where DOCNO='" + txt_docno.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Maxid();
                sql = "INSERT INTO HappyLink (docno,Poscode,PosDesc,DiscPer,Fromdate,ToDate,Wday,FromTime,ToTime,SGcode,SGDesc,itemcode,itemdesc,Void,Adduser,AddDate) VALUES";
                sql = sql + "('" + txt_docno.Text + "','" + po + "','" + pos + "','" + PERC + "','" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)cmb_todate.Value, "dd-MMM-yyyy") + "','" + dataGridView2.Rows[j].Cells[0].Value.ToString() + "','" + Convert.ToDateTime(dataGridView2.Rows[j].Cells[1].Value).ToString("HH:mm") + "','" + Convert.ToDateTime(dataGridView2.Rows[j].Cells[2].Value).ToString("HH:mm") + "','" + gcode + "','" + gdesc + "','" + Icode + "','" + ITEM + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "')";
            }
            else
            {
                sql = "INSERT INTO HappyLink (docno,Poscode,PosDesc,DiscPer,Fromdate,ToDate,Wday,FromTime,ToTime,SGcode,SGDesc,itemcode,itemdesc,Void,Adduser,AddDate) VALUES";
                sql = sql + "('" + txt_docno.Text + "','" + po + "','" + pos + "','" + PERC + "','" + Strings.Format((DateTime)Cmb_fromdate.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)cmb_todate.Value, "dd-MMM-yyyy") + "','" + dataGridView2.Rows[j].Cells[0].Value.ToString() + "','" + Convert.ToDateTime(dataGridView2.Rows[j].Cells[1].Value).ToString("HH:mm") + "','" + Convert.ToDateTime(dataGridView2.Rows[j].Cells[2].Value).ToString("HH:mm") + "','" + gcode + "','" + gdesc + "','" + Icode + "','" + ITEM + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "')";
            }

                            List.Add(sql);
                        }

                    }
                }
            }


            try
            {

                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    MessageBox.Show("Data Added Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
             
            }
            catch
            {
            
            }



            
        }

        
       

        

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("please enter Valid Time");
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //string time = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //string reg = "(([0-1][0-9])|([2][0-3])):([0-5][0-9]):([0-5][0-9])";
            //Regex regex = new Regex(reg);
            //bool isValid = regex.IsMatch(time);
            //if (!isValid)
            //{
            //    MessageBox.Show("please enter Valid Time");
            //}
        }
        

        private void btn_new_Click(object sender, EventArgs e)
        {
            Maxid();
            DB1.Refresh();
            dataGridView2.Refresh();
            dataGridView3.Refresh();
            dataGridView4.Refresh();
            DB1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            fillgroup();
            fillpos();
            textBox1.Text = "";
            this.cmb_todate.Value = DateTime.Now;
            this.Cmb_fromdate.Value = DateTime.Now;
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = true;
            fillGRID();
            Cmb_freeze.Enabled = true;
        }

        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            
            Text = this.dataGridView5.CurrentRow.Cells[0].Value.ToString();
            txt_docno.Text = Text;
            groupBox7.Visible = false;
            this.Enabled = true;
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            
            btn_new_Click(sender, e);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Refresh();
            FILLITEM();
        }

        private void txt_docno_TextChanged(object sender, EventArgs e)
        {
            txt_docno_Validated(sender, e);
        }


        

        Boolean MeValidate;
        public void checkvalidate()
        {
            MeValidate = false;
           
            
            if (Cmb_freeze.Text == "")
            {
                Cmb_freeze.Focus();
                Cmb_freeze.Text = "NO";
                MeValidate = false;
            }
            int i, j;

            var posList = new List<string>();

            for (i = 0; i <= DB1.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(DB1.Rows[i].Cells[1].Value) == true))
                {
                    posList.Add(DB1.Rows[i].Cells[0].Value.ToString());

                }
            }

            if (posList.Count == 0)
            {
                MessageBox.Show("Kindly Select Minimum One Pos Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                DB1.Focus();
                MeValidate = true;
                return;
            }

           

            var ItemList = new List<string>();

            for (i = 0; i <= dataGridView4.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView4.Rows[i].Cells[1].Value) == true))               
                {
                    ItemList.Add(dataGridView4.Rows[i].Cells[0].Value.ToString());

                }
            }

            if (ItemList.Count == 0)
            {
                MessageBox.Show("Kindly Select Minimum Item", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView4.Focus();
                MeValidate = true;
                return;
            }
            

            for (i = 0; i <= dataGridView4.RowCount - 1; i++)
            {
                string G = Convert.ToString(dataGridView4.Rows[i].Cells[0].Value);
                string T = Convert.ToString(dataGridView4.Rows[i].Cells[1].Value);
                string R = Convert.ToString(dataGridView4.Rows[i].Cells[2].Value);

                if (T == "True" && R == "")
                {
                    MessageBox.Show("Percent Column Should Not Be Blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView4.Focus ();
                    MeValidate = true;
                    return;

                }

            }


            
           
            MeValidate = false;
        }



        private void txt_docno_Validated(object sender, EventArgs e)
        {
            if (txt_docno.Text!="")
            {
                fillpos();
                DataTable dt = new DataTable();
                sql = "SELECT *  FROM HAPPYlink WHERE DOCNO='" + txt_docno.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    Cmb_fromdate.Text = dt.Rows[0]["FROMDATE"].ToString();
                    cmb_todate.Text = dt.Rows[0]["TODATE"].ToString();
                    string frz = dt.Rows[0]["VOID"].ToString();
                    if (frz == "Y")
                    {
                        Cmb_freeze.SelectedIndex = 1;

                    }
                    else
                    {
                        Cmb_freeze.SelectedIndex = 0;
                    }
                }
                sql = "SELECT  DISTINCT POSDESC FROM HAPPYlink WHERE DOCNO='" + txt_docno.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j <= DB1.RowCount - 1; j++)
                        {
                            string s = (dt.Rows[i][0].ToString());
                            string p = DB1.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)DB1.Rows[j].Cells[1];
                                chkbox.Value = true;
                            }
                        }

                    }
                }

                sql = "SELECT DISTINCT WDAY,FROMTIME,TOTIME FROM HAPPYlink WHERE DOCNO='" + txt_docno.Text + "'";

                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.Columns[1].DefaultCellStyle.Format = @"HH\:mm";
                    dataGridView2.Columns[2].DefaultCellStyle.Format = @"HH\:mm";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();

                        dataGridView2.Rows[i].Cells[1].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[1]).ToString("HH:mm");
                        dataGridView2.Rows[i].Cells[2].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[2]).ToString("HH:mm");
                        //dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    }

                }
                fillgroup();

                sql = "SELECT DISTINCT UPPER(SGDESC) FROM HAPPYlink WHERE DOCNO='" + txt_docno.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j <= dataGridView3.RowCount - 1; j++)
                        {
                            string s = (dt.Rows[i][0].ToString());
                            string p = dataGridView3.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView3.Rows[j].Cells[1];
                                chkbox.Value = true;
                            }
                        }

                    }   
                }


                sql = "SELECT DISTINCT UPPER(itemdesc),DISCPER FROM HAPPYlink WHERE DOCNO='" + txt_docno.Text + "'";
                dt = GCon.getDataSet(sql);
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                        dataGridView4.Rows[i].Cells[2].Value = dt.Rows[i].ItemArray[1].ToString();
                        for (int j = 0; j <= dataGridView4.RowCount - 1; j++)
                        {
                            string s = (dt.Rows[i][0].ToString());
                            string p = dataGridView4.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView4.Rows[j].Cells[1];
                                chkbox.Value = true;
                                
                            }
                        }

                    }
                }
            }

            


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j <= dataGridView4.RowCount - 1; j++)
                {
                    DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView4.Rows[j].Cells[1];
                    chkbox.Value = true;

                }
            }
            else
                if (checkBox1.Checked == false)
                {
                    for (int j = 0; j <= dataGridView4.RowCount - 1; j++)
                    {
                        DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView4.Rows[j].Cells[1];
                        chkbox.Value = false;

                    }
                }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int j = 0; j <= dataGridView4.RowCount - 1; j++)
                {
                    if ((Convert.ToBoolean(dataGridView4.Rows[j].Cells[1].Value) == true))
                    {
                        dataGridView4.Rows[j].Cells[2].Value = textBox1.Text;
                    }
                    


                }
            }

            else
            {
                for (int j = 0; j <= dataGridView4.RowCount - 1; j++)
                {
                    
                        dataGridView4.Rows[j].Cells[2].Value = "";
                    
                }
            }

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
               
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
