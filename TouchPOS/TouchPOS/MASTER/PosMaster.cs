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
    public partial class PosMaster : Form
    {

        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        public PosMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        string po, vseqno;
        string taxdesc;
        public void filltax()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT ISNULL(CHARGECODE,'') AS CHARGECODE,chargedesc FROM CHARGEMASTER  WHERE ISNULL(RATE,0)=0 AND ISNULL(Freeze,'') <> 'Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                taxdesc = dt.Rows[0]["chargedesc"].ToString();
                cmb_taxtype.Items.Clear();
                cmb_taxtype2.Items.Clear();
                cmb_taxtype3.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_taxtype.Items.Add(dt.Rows[i]["chargedesc"].ToString());
                    cmb_taxtype2.Items.Add(dt.Rows[i]["chargedesc"].ToString());
                    cmb_taxtype3.Items.Add(dt.Rows[i]["chargedesc"].ToString());
                }
                cmb_taxtype.SelectedIndex = 0;
                cmb_taxtype2.SelectedIndex = 0;
                cmb_taxtype3.SelectedIndex = 0;
            }
        }

        private void PettycashMaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            Maxid();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Maxid();
            fillaccount();
            fillcost();
            fillGRID();
            filltax();

            CMB_SURCHR.SelectedIndex = -1;
            cmb_cost.SelectedIndex = -1;
            CMB_ADD.SelectedIndex = -1;
            CMB_TIPS.SelectedIndex = -1;
            cmb_taxtype.SelectedIndex = -1;
            cmb_taxtype2.SelectedIndex = -1;
            cmb_taxtype3.SelectedIndex = -1;
            cmb_st.SelectedIndex = 1;

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

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " select POSCODE,POSdesc,freeze,storetype from posmaster  ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                this.dataGridView1.RowHeadersVisible = false;
                
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                this.dataGridView1.ReadOnly = true;

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[j].Value = PosCate.Rows[i].ItemArray[j];
                        this.dataGridView1.Rows[i].Height = 30;
                    }


                }

            }
        }
        private void showHideColumns(Boolean colHideStatus)
        {
            dataGridView1.Columns[3].Visible = colHideStatus;
            dataGridView1.Columns[4].Visible = colHideStatus;

            dataGridView1.Columns[5].Visible = colHideStatus;
            dataGridView1.Columns[6].Visible = colHideStatus;
            dataGridView1.Columns[7].Visible = colHideStatus;
            dataGridView1.Columns[8].Visible = colHideStatus;

            dataGridView1.Columns[9].Visible = colHideStatus;
            dataGridView1.Columns[10].Visible = colHideStatus;
            dataGridView1.Columns[11].Visible = colHideStatus;
            dataGridView1.Columns[12].Visible = colHideStatus;

            //dataGridView1.Columns[13].Visible = colHideStatus;
            //dataGridView1.Columns[14].Visible = colHideStatus;

        }
        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, POSCODE)),0)+1 as POSCODE from posmaster where isnumeric(POSCODE) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                txt_poscode.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }


        Boolean MeValidate;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (cmb_st.Text == "")
            {
                MessageBox.Show(" Type cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_poscode.Focus();
                MeValidate = true;
                return;
            }

            if (txt_posdesc.Text == "")
            {
                MessageBox.Show(" Pos Description cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_poscode.Focus();
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
            if (txt_surcharge.Text == "")
            {
                txt_surcharge.Text = "0.00";
            }
            if (txt_tips1.Text == "")
            {
                txt_tips1.Text = "0.00";
            }
            if (txt_add1.Text == "")
            {
                txt_add1.Text = "0.00";
            }

            sql = "select * from posmaster where POScode = '" + txt_poscode.Text + "' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                sql = "Update  posmaster set POSDESC = '" + txt_posdesc.Text + "',Storetype='" + cmb_st.Text + "',";
                sql = sql + "PackingPercent='" + txt_surcharge.Text + "',tips='" + txt_tips1.Text + "',adcharge='" + txt_add1.Text + "',";
                sql = sql + "UPDATEUSER='" + GlobalVariable.gUserName + "',cat_name='" + TXT_CATRER.Text + "',cat_gstin='" + Txt_gstin.Text + "',";
                sql = sql + "PAcctin='" + accodesuchg + "',TipsAcctin='" + accodetips + "',AdChgAcctin='" + accodeadd + "',PChgCode='" + TAX + "',TipsChgCode='" + TAX2 + "',AdChgChgCode='"+ TAX3 +"',CostCenterCode='" + COST + "',"; sql = sql + "CostCenterDesc='" + cmb_cost.Text + "',";
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " freeze='N',";
                } 
                else
                {
                    sql = sql + "freeze='Y',";
                }
                sql = sql + " UPDATETIME=getdate() where poscode = '" + txt_poscode.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                btn_new_Click(sender, e);
            }
            else
            {
                sql = "select isnull(posdesc,'')as itemdesc  from posmaster ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        string s = (dt.Rows[k][0].ToString());
                        string p = txt_posdesc.Text;

                        if (s == p)
                        {
                            MessageBox.Show("Pos Name Already Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MeValidate = true;
                            return;
                        }
                    }

                }
                sql = "select [dbo].[GetSeqno]('" + txt_poscode.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }
                if (txt_surcharge.Text == "")
                {
                    txt_surcharge.Text = "0.00";
                }
                if (txt_tips1.Text == "")
                {
                    txt_tips1.Text = "0.00";
                }
                if (txt_add1.Text == "")
                {
                    txt_add1.Text = "0.00";
                }
                sqlstring = " INSERT INTO POSMASTER (Storetype,POSseqno,Poscode,PosDesc,PackingPercent,PCKDESC,PCKLIMIT,tips,SCDESC,SCLIMIT,adcharge,ADDESC,ADLIMIT,prcharge,grcharge,roundoffyesno,roundval,paymentmode,centralizedkot,";
                sqlstring = sqlstring + "kotentry,kottype,kotprefix,finalprefix,directbill,directtype,directprefix,tablerequired,smartrequired,AddUSerId,AddDateTime,freeze,PAcctin,TipsAcctin,AdChgAcctin,ADPartyAcctin,ADRoomAcctin,CostCenterCode,CostCenterDesc,BControl,Cat_Name,TaxApp,KotPrint,TakeAway,Cat_GSTIN,PChgCode,TipsChgCode,AdChgChgCode)";
                sqlstring = sqlstring + " VALUES ('" + cmb_st.Text + "','" + vseqno + "','" + txt_poscode.Text + "','" + txt_posdesc.Text + "','" + txt_surcharge.Text + "'";
                sqlstring = sqlstring + ",'','0.00','" + txt_tips1.Text + "','','0.00','" + txt_add1.Text + "','','0.00','0.00','0.00','NO','0.00','CASH','NO','YES','AUTO','','','NO','AUTO','','NO','NO','" + GlobalVariable.gUserName + "',";
                sqlstring = sqlstring + " '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N','" + accodesuchg + "','" + accodetips + "','" + accodeadd + "','','','" + COST + "',";
                sqlstring = sqlstring + " '" + cmb_cost.Text + "','NO','" + TXT_CATRER.Text + "','NO','NO','NO','" + Txt_gstin.Text + "','"+ TAX+"','"+ TAX2 +"','"+ TAX3 +"')";
                 dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed Successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_new_Click(sender, e);

            }
        }


        public void fillcost()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct costcentercode,costcenterdesc from ACCOUNTSCOSTCENTERMASTER WHERE ISNULL(FREEZEFLAG,'')<>'Y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {


                cmb_cost.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    cmb_cost.Items.Add(dt.Rows[i]["costcenterdesc"].ToString());
                }
                cmb_cost.SelectedIndex = 0;
            }
        }
        String COST;
        private void cmb_cost_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Text;
            if (cmb_cost.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select costcentercode,costcenterdesc from ACCOUNTSCOSTCENTERMASTER WHERE costcenterdesc='" + cmb_cost.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    COST = dt.Rows[0]["costcentercode"].ToString();
                }

            }
        }



        public void fillaccount()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct accode,acdesc from accountsglaccountmaster";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                CMB_SURCHR.Items.Clear();
                CMB_ADD.Items.Clear();
                CMB_TIPS.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    CMB_SURCHR.Items.Add(dt.Rows[i]["acdesc"].ToString());
                    CMB_TIPS.Items.Add(dt.Rows[i]["acdesc"].ToString());
                    CMB_ADD.Items.Add(dt.Rows[i]["acdesc"].ToString());
                }
                CMB_TIPS.SelectedIndex = 0;

                CMB_ADD.SelectedIndex = 0;
                CMB_SURCHR.SelectedIndex = 0;
            }
        }
        String accodesuchg;
        private void CMB_SURCHR_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_cost.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select accode,acdesc from accountsglaccountmaster where acdesc='" + CMB_SURCHR.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    accodesuchg = dt.Rows[0]["accode"].ToString();
                }

            }
        }
        String accodetips;
        private void CMB_TIPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_cost.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select accode,acdesc from accountsglaccountmaster where acdesc='" + CMB_TIPS.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    accodetips = dt.Rows[0]["accode"].ToString();
                }

            }
        }
        String accodeadd;
        private void CMB_ADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_cost.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select accode,acdesc from accountsglaccountmaster where acdesc='" + CMB_ADD.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    accodeadd = dt.Rows[0]["accode"].ToString();
                }

            }
        }
        string FREEZE;
        private void btn_edit_Click(object sender, EventArgs e)
        {
            txt_poscode.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_posdesc.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmb_st.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
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
            CMB_SURCHR.SelectedIndex = -1;
            cmb_cost.SelectedIndex = -1;
            CMB_ADD.SelectedIndex = -1;
            cmb_st.SelectedIndex = 1;
            CMB_TIPS.SelectedIndex = -1;
            Txt_gstin.Text = "";
            txt_tips1.Text = "";
            txt_add1.Text = "";
            TXT_CATRER.Text = "";
            txt_posdesc.Text = "";
            txt_surcharge.Text = "";
            Maxid();
            fillaccount();
            fillcost();
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_cost.SelectedIndex = -1;
            CMB_SURCHR.SelectedIndex = -1;
            CMB_TIPS.SelectedIndex = -1;
            CMB_ADD.SelectedIndex = -1;
            fillGRID();
            cmb_taxtype.SelectedIndex = -1;
            cmb_taxtype2.SelectedIndex = -1;
            cmb_taxtype3.SelectedIndex = -1;
        }

        private void txt_poscode_TextChanged(object sender, EventArgs e)
        {
            txt_poscode_Validated(sender, e);
        }
        string costcenter,scharge,tcharge,adcharge,ctax,ctax2,ctax3; 
        private void txt_poscode_Validated(object sender, EventArgs e)
        {
           
            DataTable PosCate = new DataTable();
            sql = " select * from posmaster where POSCODE='" + txt_poscode.Text + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                txt_poscode.Text = PosCate.Rows[0]["POSCODE"].ToString();

                cmb_st.Text = PosCate.Rows[0]["storetype"].ToString();
                txt_posdesc.Text = PosCate.Rows[0]["posdesc"].ToString();
                txt_tips1.Text = PosCate.Rows[0]["TIPS"].ToString();
                txt_add1.Text = PosCate.Rows[0]["adcharge"].ToString();
                txt_surcharge.Text = PosCate.Rows[0]["PackingPercent"].ToString();
                cmb_st.Text = PosCate.Rows[0]["Storetype"].ToString();
                TXT_CATRER.Text = PosCate.Rows[0]["Cat_Name"].ToString();
                Txt_gstin.Text = PosCate.Rows[0]["Cat_GSTIN"].ToString();
                costcenter = PosCate.Rows[0]["CostCenterCode"].ToString();
                scharge = PosCate.Rows[0]["PAcctin"].ToString();
                tcharge = PosCate.Rows[0]["TipsAcctin"].ToString();
                adcharge = PosCate.Rows[0]["AdChgAcctin"].ToString();
                ctax = PosCate.Rows[0]["PChgCode"].ToString();
                ctax2 = PosCate.Rows[0]["TipsChgCode"].ToString();
                ctax3 = PosCate.Rows[0]["AdChgChgCode"].ToString();
            }

            sql = " select chargedesc  from chargemaster where chargecode='" + ctax + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                cmb_taxtype.Text = PosCate.Rows[0]["chargedesc"].ToString();
              
            }
            sql = " select chargedesc  from chargemaster where chargecode='" + ctax2 + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                cmb_taxtype2.Text = PosCate.Rows[0]["chargedesc"].ToString();

            }
            sql = " select chargedesc  from chargemaster where chargecode='" + ctax3 + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                cmb_taxtype3.Text = PosCate.Rows[0]["chargedesc"].ToString();

            }


            cmb_cost.SelectedIndex = -1;
            CMB_SURCHR.SelectedIndex = -1;
            CMB_TIPS.SelectedIndex = -1;
            CMB_ADD.SelectedIndex = -1;
            sql = " select * from ACCOUNTSCOSTCENTERMASTER where costcentercode='" + costcenter + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                cmb_cost.Text = PosCate.Rows[0]["costcenterdesc"].ToString();
              
            }

            sql = " select accode,acdesc from accountsglaccountmaster where accode='" + scharge + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {

                CMB_SURCHR.Text = PosCate.Rows[0]["acdesc"].ToString();

            }
            sql = " select accode,acdesc from accountsglaccountmaster where accode='" + tcharge + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {

                CMB_TIPS.Text = PosCate.Rows[0]["acdesc"].ToString();

            }
            sql = " select accode,acdesc from accountsglaccountmaster where accode='" + adcharge + "' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {

                CMB_ADD.Text = PosCate.Rows[0]["acdesc"].ToString();

            }
        }

       

        private void txt_surcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_tips_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }  
        }

        private void txt_tips1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            } 
        }

        private void txt_add1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            } 
        }
        string TAX;
        private void cmb_taxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_taxtype.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select CHARGECODE,CHARGEDESC from CHARGEMASTER where CHARGEDESC='" + cmb_taxtype.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    TAX = dt.Rows[0]["CHARGECODE"].ToString();
                }

            }
        }
        string TAX2;
        private void cmb_taxtype2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_taxtype2.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select CHARGECODE,CHARGEDESC from CHARGEMASTER where CHARGEDESC='" + cmb_taxtype2.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    TAX2 = dt.Rows[0]["CHARGECODE"].ToString();
                }

            }

        }
        string TAX3;
        private void cmb_taxtype3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Text;
            if (cmb_taxtype3.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();

                sql = "select CHARGECODE,CHARGEDESC from CHARGEMASTER where CHARGEDESC='" + cmb_taxtype3.Text + "'  ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    TAX3 = dt.Rows[0]["CHARGECODE"].ToString();
                }

            }
        }

        private void cmb_st_SelectedIndexChanged(object sender, EventArgs e)
        {

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
