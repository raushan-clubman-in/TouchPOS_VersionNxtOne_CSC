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
using WinHttp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace TouchPOS
{
    public partial class PayForm : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string KOrderNo = "";
        bool gPrint = true;
        static string DocType = "TBIL";
        public string FinYear = (GlobalVariable.FinStart.Year.ToString()).Substring(2, 2) + "-" + (GlobalVariable.FinEnd.Year.ToString()).Substring(2, 2);
        public string FinYear_CSC = (GlobalVariable.FinStart.Year.ToString()).Substring(2, 2) + "" + (GlobalVariable.FinEnd.Year.ToString()).Substring(2, 2);
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        string CardCode="", DigitCode = "",NCFlag = "N";
        string OMemCode = "";
        string NCDocType = "NC";
        public string TabBill = "";
        Double CrLimit = 0;

        public PayForm()
        {
            InitializeComponent();
        }

        string sql = "";
        string PutPay = "";
        double GridTotal = 0, GridTaxTotal = 0, GridOTHTotal = 0, GridMFTotal = 0;
        string DiscountUserName = "",DiscountCategory="";
        bool DelRightsFlag = false;
        bool CloseBillRightsFlag = false;

        private void PayForm_Load(object sender, EventArgs e)
        {
            BlackGroupBox();

            if (GlobalVariable.gCompName == "SKYYE") 
            {
                DocType = Convert.ToString(GCon.getValue("SELECT ISNULL(BillPrefix,'') BillPrefix FROM POSSETUP"));
                if (DocType == "") 
                {
                    DocType = Convert.ToString(GCon.getValue("Select Isnull(BillPrefix,'') as BillPrefix from ServiceLocation_Hdr Where LocCode IN (SELECT LocCode FROM KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')"));
                }
            }
            else 
            {
                DocType = Convert.ToString(GCon.getValue("Select Isnull(BillPrefix,'') as BillPrefix from ServiceLocation_Hdr Where LocCode IN (SELECT LocCode FROM KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')"));
            }
            //DocType = Convert.ToString(GCon.getValue("Select Isnull(BillPrefix,'') as BillPrefix from ServiceLocation_Hdr Where LocCode IN (SELECT LocCode FROM KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')"));
            //int BNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(BILLno,1,6) As Numeric)),0) + 1  FROM BILL_HDR "));
            int BNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(BILLno,1,6) As Numeric)),0) + 1  FROM BILL_HDR  WHERE BILLDETAILS LIKE '" + DocType + "%' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (GlobalVariable.gCompName == "CSC")
            {
                BNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'GCSC' ")) + 1;
            }
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
            label1.Text = GlobalVariable.ServiceType;
            label2.Text = "TBL No: " + GlobalVariable.SLocation + "/" + GlobalVariable.TableNo;
            //label3.Text = "Order No: " + KOrderNo.Substring(5,6);
            label3.Text = "Order No: " + KOrderNo;
            label4.Text = "Bill No: " + BNo.ToString();
            if (GlobalVariable.gCompName == "CSC") 
            {
                //string bno = "1";
                //bno = BNo.ToString()
                label4.Text = "Bill No: G" + DocType + "/" + BNo.ToString("000000") + "/" + FinYear_CSC;
            }
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].Width = 50;
            this.dataGridView1.Columns[1].Width = 200;
            this.dataGridView1.Columns[2].Width = 50;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            DataTable KotData = new DataTable();
            ////sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,SLNO,MODIFIER,AUTOID from KOT_det where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            ////sql = "Select KOTNO,D.KOTDETAILS,D.ITEMCODE,ITEMDESC,SUM(D.QTY) AS QTY,SUM(AMOUNT) AS AMOUNT,SUM(ISNULL(T.TAXAMT,0)) AS TAXAMOUNT,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) as OTHAmount,sum(isnull(ModifierCharges,0)) as MFAmount ";
            ////sql = sql + " from KOT_det D,KOT_DET_TAX T where D.KOTDETAILS = T.KOTDETAILS AND D.ITEMCODE = T.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(T.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(T.FinYear,'') ";
            ////sql = sql + " AND D.KOTDETAILS = '" + KOrderNo + "' And isnull(D.Kotstatus,'') <> 'Y' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' GROUP BY KOTNO,D.KOTDETAILS,D.ITEMCODE,ITEMDESC ";
            sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,SUM(QTY) AS QTY,SUM(AMOUNT) AS AMOUNT,SUM(ISNULL(TAXAMOUNT,0)) AS TAXAMOUNT,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) as OTHAmount,sum(isnull(ModifierCharges,0)) as MFAmount from KOT_det where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' GROUP BY KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC ";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0)
            {
                for (int i = 0; i < KotData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[4]);
                    dataGridView1.Rows[i].Cells[1].Value = KotData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[5]);
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[6]);
                    dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[7]);
                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[8]);
                    GridTotal = GridTotal + Convert.ToDouble(KotData.Rows[i].ItemArray[5]);
                    GridTaxTotal = GridTaxTotal + Convert.ToDouble(KotData.Rows[i].ItemArray[6]);
                    GridOTHTotal = GridOTHTotal + Convert.ToDouble(KotData.Rows[i].ItemArray[7]);
                    GridMFTotal = GridMFTotal + Convert.ToDouble(KotData.Rows[i].ItemArray[8]);
                }
                dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView2.ColumnHeadersVisible = false;
                dataGridView2.RowHeadersVisible = false;

                CheckDiscountForMember();

                DataTable TaxData = new DataTable();
                sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                ////sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 Union all ";
                ////sql = sql + "SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 Union all ";
                ////sql = sql + " SELECT a.taxdesc As TDesc,sum(TAXAMT)as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND TAXAMT > 0 group by a.taxdesc";
                sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Having (sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) > 0 Union all ";
                sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0  ";
                sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
                sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
                TaxData = GCon.getDataSet(sql);

                if (TaxData.Rows.Count > 0)
                {
                    dataGridView2.ReadOnly = true;
                    for (int j = 0; j < TaxData.Rows.Count; j++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                        //dataGridView2.Rows[j].Cells[1].Value = Convert.ToDouble(TaxData.Rows[j].ItemArray[1]);
                        dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                    }
                }
            }
            DataTable Member = new DataTable();
            Member = GCon.getDataSet("select MCode,CARDHOLDERCODE,[16_DIGIT_CODE],Isnull(NCFlag,'N') as NCFlag from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'");
            if (Member.Rows.Count > 0)
            {
                CardCode = Member.Rows[0].ItemArray[1].ToString();
                DigitCode = Member.Rows[0].ItemArray[2].ToString();
                NCFlag = Member.Rows[0].ItemArray[3].ToString();
            }
            if (GlobalVariable.gCompName == "HPRC") 
            {
                NCCheckingHPRC();
            }

            if (NCFlag == "Y")
            {
                Cmd_CASH.Enabled = false;
                Cmd_Cards.Enabled = false;
            }
            else 
            {
                Cmd_NotCharges.Enabled = false;
            }
            Grp_DenoINR.Visible = true;
            Grp_INR.Visible = false;
            Grp_Cards.Visible = false;
            Cmd_CloseBill.Enabled = false;
            FillDiscount(GlobalVariable.gUserName);
            Grp_Discount.Visible = false;
            ////if (GlobalVariable.gCompName == "RTC" )
            ////{
            ////    Cmd_Discount.Enabled = true;
            ////}
            ////else 
            ////{
            ////    Cmd_Discount.Enabled = true;
            ////}
            Grp_NCRemarks.Visible = false;
            if (GlobalVariable.gUserCategory != "S") { GetRights(); }
            if (GlobalVariable.gUserCategory != "S")
            {
                if (DelRightsFlag == true) { Cmd_OrdCancel.Enabled = true; } else { Cmd_OrdCancel.Enabled = false; }
            }
            else
            {
                DelRightsFlag = true;
                CloseBillRightsFlag = true;
            }
            if (GlobalVariable.gCompName == "SKYYE" )
            {
                if (GlobalVariable.gUserName == "VASANTH") { }
                else { Cmd_OrdCancel.Enabled = false; }
            }
            if (GlobalVariable.gCompName == "CSC")
            {
                Cmd_Tips.Visible = false;
                Chk_ExmptedTax.Visible = false;
                Check_WaiveSChg.Visible = false;
                Cmd_Discount.Visible = false;
                //Cmd_NotCharges.Visible = false;
                Cmd_ArFlag.Visible = false;
                Cmd_SMFlag.Visible = true;
                Cmd_Cards.Width = 209;
                Button_ZOMATO.Text = "R.MEMBER";
                Button_SWIGGY.Text = "COUPON";
                Button_CBILL.Text = "CREDIT";
                Button_PG.Text = "CARD";
                Cmd_Cards.Text = "OTHER PAYMENTS";
                Button_AMEX.Enabled = false;
                Button_VISA.Enabled = false;
                Button_PAYTM.Enabled = false;
                Button_PREPAID.Enabled = false;
                Button_UPI.Enabled = false;
            }
            else { Cmd_SMFlag.Visible = false; }
            if (GlobalVariable.gCompName == "CFC" && GlobalVariable.gUserCategory != "S")
            {
                Cmd_MemberTag.Enabled = false;
                Cmd_RoomTag.Enabled = false;
                Cmd_SMFlag.Enabled = false;
            }
            if (GlobalVariable.gCompName == "CFC") 
            {
                Button_VISA.Enabled = false;
                Button_SWIGGY.Enabled = false;
                Button_PAYTM.Enabled = false;
                Button_AMEX.Enabled = false;
                Button_ROOM.Enabled = false;
                Button_ZOMATO.Enabled = false;
                Button_PG.Enabled = false;
                Button_PREPAID.Enabled = false;
                Button_UPI.Enabled = false;
                Button_NEFT.Enabled = false;
            }
            Calculate();
        }

        public void CheckDiscountForMember() 
        {
            try
            {
                string member = "",DiscType= "";
                double DiscPerc = 0;
                DataTable DiscMember = new DataTable();
                member = Convert.ToString(GCon.getValue("SELECT top 1 ISNULL(mcode,'') AS MCODE FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "' AND KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (member != "") 
                {
                    sql = "SELECT DISTINCT Mcode,DiscType,DiscPerc FROM DiscountMemberList_ItemWise WHERE '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN FromDate AND ToDate AND ISNULL(Active,'') = 'Y' AND ISNULL(VOID,'') <> 'Y' AND MCODE = '" + member + "'";
                    DiscMember = GCon.getDataSet(sql);
                    if (DiscMember.Rows.Count > 0) 
                    {
                        Cmd_Discount.Enabled = false;
                        DiscType = DiscMember.Rows[0].ItemArray[1].ToString();
                        DiscPerc = Convert.ToDouble(DiscMember.Rows[0].ItemArray[2]);
                        if (DiscType == "OnBill") 
                        {
                            sql = "Update Kot_det set ItemDiscPerc = " + DiscPerc + " Where KOTDETAILS = '" + KOrderNo + "' AND KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                            GCon.dataOperation(1,sql);
                        }
                        else if (DiscType == "OnItem") 
                        {
                            sql = "Update Kot_det Set ItemDiscPerc = DiscPerc from DiscountMemberList_ItemWise I,Kot_Det D Where i.ItemCode = d.ITEMCODE And KOTDETAILS = '" + KOrderNo + "' AND KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between i.FromDate and i.ToDate And isnull(Active,'') = 'Y' and i.Mcode = '" + member + "' and Isnull(Void,'') <> 'Y' ";
                            GCon.dataOperation(1,sql);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
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
            myGroupBox3.Size = Grp_INR.Size;
            Grp_INR.Controls.Add(myGroupBox3);

            myGroupBox myGroupBox4 = new myGroupBox();
            myGroupBox4.Text = "";
            myGroupBox4.BorderColor = Color.Black;
            myGroupBox4.Size = Grp_Cards.Size;
            Grp_Cards.Controls.Add(myGroupBox4);

            myGroupBox myGroupBox5 = new myGroupBox();
            myGroupBox5.Text = "";
            myGroupBox5.BorderColor = Color.Black;
            myGroupBox5.Size = Grp_DenoINR.Size;
            Grp_DenoINR.Controls.Add(myGroupBox5);
        }

        public void NCCheckingHPRC() 
        {
            DataTable NCMember = new DataTable();
            DataTable NCCardCode = new DataTable();
            NCMember = GCon.getDataSet("SELECT mcode,mname FROM membermaster WHERE mcode in (select mcode from KOT_HDR where Kotdetails = '" + KOrderNo + "' and Isnull(FinYear,'') = '" + FinYear1 + "') and curentstatus in('LIVE','ACTIVE') and membertypecode in(select subtypecode from subcategorymaster where isnull(clubaccount,'')='YES')");
            if (NCMember.Rows.Count > 0)
            {
                NCFlag = "Y";
            }
            NCCardCode = GCon.getDataSet("SELECT * FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardCode + "' AND ISNULL(CLUB_ACCOUNT_YN,'') = 'Y' ");
            if (NCCardCode.Rows.Count > 0)
            {
                NCFlag = "Y";
            }
        }

        private void Cmd_BackToMenu_Click(object sender, EventArgs e)
        {
            DataTable CheckPrintFlag = new DataTable();
            sql = "UPDATE TableMaster SET OpenStatus = '' WHERE Pos IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);
            sql = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE LocCode IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);

            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            { }
            else 
            {
                sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                GCon.dataOperation(1, sql);
            }

            ////sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
            ////GCon.dataOperation(1, sql);

            if (GlobalVariable.ServiceType == "Dine-In")
            {
                if (TabBill == "N") { }
                else 
                {
                    ServiceLocation SL = new ServiceLocation();
                    SL.Show();
                    this.Close();
                }
            }
            else
            {
                if (GlobalVariable.ServiceType == "Direct-Billing")
                {
                    GlobalVariable.ServiceType = "Direct-Billing";
                    DataTable dt = new DataTable();
                    sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable ChkChair = new DataTable();
                        int ChNo = 1;
                        GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                        GlobalVariable.TableNo = "V1";
                        EntryForm EF = new EntryForm();
                        EF.Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                        sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                        ChkChair = GCon.getDataSet(sql);
                        if (ChkChair.Rows.Count > 0)
                        {
                            ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                        }
                        else { ChNo = 1; }
                        int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                        if (RowCnt > 0)
                        {
                            EF.UpdFlag = true;
                            GlobalVariable.ChairNo = ChNo;
                        }
                        else
                        {
                            EF.UpdFlag = false;
                            GlobalVariable.ChairNo = ChNo;
                            EF.Pax = 1;
                        }
                        EF.Show();
                        this.Close();
                    }
                }
                else 
                {
                    ServiceType SL = new ServiceType();
                    SL.Show();
                    this.Close();
                }
            }
        }

        private void Calculate() 
        {
            DataTable RCal = new DataTable();
            double ToPay = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++) 
            {
                if (dataGridView2.Rows[i].Cells[1].Value != null) 
                {
                    string DescVal = "";
                    if (dataGridView2.Rows[i].Cells[0].Value != null)
                    {
                        DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    }
                    else { DescVal = ""; }
                    if (dataGridView2.Rows[i].Cells[1].Value != null) 
                    {
                        if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
                        {
                            ToPay = ToPay - Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                        }
                        else
                        {
                            ToPay = ToPay + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                        }
                    }
                }
            }
            //sql = "SELECT Round("+ ToPay +",0)";
            //ToPay = Convert.ToDouble(GCon.getValue(sql));
            //Txt_BalAmt.Text = String.Format("{0:0.00}", ToPay.ToString());
            ////if (ToPay > 0)
            ////{
            ////    Txt_BalAmt.Text = String.Format("{0:0.##}", Math.Round(ToPay, 0, MidpointRounding.AwayFromZero));
            ////}
            ////else 
            ////{
            ////    Txt_BalAmt.Text = String.Format("{0:0.##}", Math.Round(ToPay, 0));
            ////    Txt_BalAmt.Text = String.Format("{0:0.##}", Math.Round(ToPay, 0, MidpointRounding.AwayFromZero));
            ////}
            ToPay = ToPay + 0.01;
            Txt_BalAmt.Text = String.Format("{0:0.##}", Math.Round(ToPay, 0));
            if (Convert.ToDouble(Txt_BalAmt.Text) == 0)
            {
                Cmd_CloseBill.Enabled = true;
                Cmd_CloseBill.BackColor = Color.Green;
                if (CloseBillRightsFlag == true) { Cmd_CloseBill.Enabled = true; } else { Cmd_CloseBill.Enabled = false; }
            }
            else 
            {
                Cmd_CloseBill.Enabled = false;
                Cmd_CloseBill.BackColor = Color.LightGray;
            }
        }

        private void Cmd_CASH_Click(object sender, EventArgs e)
        {
            Button_INR.Text = "INR :" + Txt_BalAmt.Text;
            Grp_DenoINR.Visible = false;
            Grp_Cards.Visible = false;
            Grp_INR.Visible = true;
            Grp_Discount.Visible = false;
        }

        private void Button_INR_Click(object sender, EventArgs e)
        {
            bool bollINR = false;
            int RowCnt;
            for (int i = 0; i < dataGridView2.Rows.Count; i++) 
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null) 
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString() =="INR") 
                    {
                        bollINR = true;
                    }
                }
            }
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }

            if (bollINR == false) 
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "INR";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
            }
            
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "INR")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT") 
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0 && dataGridView2.Rows[rowindex].Cells[1].Value != null)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_0.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0 && dataGridView2.Rows[rowindex].Cells[1].Value != null)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_1.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_dot_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                {
                    if (dataGridView2.Rows[rowindex].Cells[1].Value.ToString().IndexOf(".", StringComparison.CurrentCultureIgnoreCase) != -1) 
                    {
                        return;  
                    }
                    PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); 
                }
                else { PutPay = ""; }
                PutPay = PutPay + Button_dot.Text ;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_2.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_3.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_4.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_5.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_6.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_7.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_8.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                PutPay = PutPay + Button_9.Text;
                dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
            }
            Calculate();
        }

        private void Cmd_BackSpace_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView2.CurrentRow.Index;
            string DescVal = "";
            DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
            {
                if (Convert.ToDouble(dataGridView2.Rows[rowindex].Cells[1].Value) > 0)
                { PutPay = dataGridView2.Rows[rowindex].Cells[1].Value.ToString(); }
                else { PutPay = ""; }
                if (PutPay.Length > 0) 
                {
                    PutPay = PutPay.Remove(PutPay.Length - 1, 1);
                    dataGridView2.Rows[rowindex].Cells[1].Value = (PutPay);
                }
                if (PutPay.Length == 0) 
                {
                    dataGridView2.Rows[rowindex].Cells[1].Value = "0";
                }
            }
            Calculate();
        }

        private void Cmd_Cards_Click(object sender, EventArgs e)
        {
            Grp_DenoINR.Visible = false;
            Grp_Cards.Visible = true;
            Grp_INR.Visible = false;
            Grp_Discount.Visible = false;
        }

        private void Button_VISA_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "VISA")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else 
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "VISA";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt-1];
            }
        }

        private void Button_SWIGGY_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == Button_SWIGGY.Text)
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = Button_SWIGGY.Text;
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void Button_PAYTM_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "PAYTM")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "PAYTM";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void Button_AMEX_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "AMEX")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "AMEX";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void Button_CBILL_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
            if (Member == "") 
            {
                string ARMember = Convert.ToString(GCon.getValue("select DISTINCT ARCode from Tbl_ARFlagUpdation WHERE KotNo IN (select Kotdetails from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
                if (ARMember == "") 
                {
                    MessageBox.Show("For this Bill Member or AR Member Not Tag, Can't Make Credit Bill");
                    return;
                }
            }

            DataTable ChkDt = new DataTable();
            if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT * FROM MEMBERMASTER WHERE MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y') AND ISNULL(CRIDITNUMBER,'') <> '' ";
                ChkDt = GCon.getDataSet(sql);
                if (ChkDt.Rows.Count == 0)
                {
                    MessageBox.Show("Credit Facility Not avail for Taged Member, Can't Make Credit Bill");
                    return;
                }
            }

            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "CBILL")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "CBILL";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void Cmd_CloseBill_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            bool TrnDone = false;
            bool RoomAvail = false;
            bool TaxB = false;
            bool NTaxB = false;
            int PayCount = 0;
            int i = 0,DiscCount=0;
            int BillNo = 1,NBillNo = 1;
            string BillDetails = "", NBillDetails = "";
            Double BillAmount = 0, CardBal = 0;
            Double TotAmount = 0;
            Double INRAmt = 0;
            bool TipsGiven = false;
            Double VariableTips = 0, VariableRefund = 0,BillTotalBal = 0;
            string DescVal = "";
            string MainCard = "", MainCardDigitCode = "";
            DateTime BillDate = GlobalVariable.ServerDate;
            bool PaySettle = CheckValidate();
            if (PaySettle == false) 
            {
                return;
            }
            try 
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                Cmd_CloseBill.Enabled = false;

                for (i = 0; i < dataGridView2.Rows.Count; i++) 
                {
                    if (dataGridView2.Rows[i].Cells[0].Value != null)
                    {
                        DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    }
                    else { DescVal = ""; }
                    if (DescVal == "TIPS")
                    { TipsGiven = true; }
                    if (DescVal == "INR") 
                    {
                        INRAmt = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                    }
                }

                BillTotalBal = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                if (BillTotalBal < 0)
                {
                    if (TipsGiven == false)
                    {
                        DialogResult result = MessageBox.Show("Pay Amount is More then Bill, Tips Will be given? ", GlobalVariable.gCompanyName, buttons);
                        if (result == DialogResult.Yes)
                        {
                            Cmd_Tips_Click(sender, e);
                            return;
                        }
                        else
                        {
                            VariableRefund = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                            VariableRefund = -(VariableRefund);
                            if (INRAmt == 0 && VariableRefund > 0) 
                            {
                                MessageBox.Show("Refund Not Applicable on selected Settlement!");
                                return;
                            }
                            if (VariableRefund > 0 && INRAmt > 0 && INRAmt < VariableRefund) 
                            {
                                MessageBox.Show("Refund Not Applicable CASH is Less then Refund Amount!");
                                return;
                            }
                        }
                    }
                    else 
                    {
                        VariableRefund = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                        VariableRefund = -(VariableRefund);
                        if (INRAmt == 0 && VariableRefund > 0)
                        {
                            MessageBox.Show("Refund Not Applicable on selected Settlement!");
                            return;
                        }
                        if (VariableRefund > 0 && INRAmt > 0 && INRAmt < VariableRefund)
                        {
                            MessageBox.Show("Refund Not Applicable CASH is Less then Refund Amount!");
                            return;
                        }
                    }
                }

                DataTable KotDetCheck = new DataTable();
                sqlstring = "SELECT * FROM Kot_Det WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(Billdetails,'') = ''";
                KotDetCheck = GCon.getDataSet(sqlstring);
                if (KotDetCheck.Rows.Count == 0) 
                {
                    MessageBox.Show("Bill Generated For this, Please Check in BillSearch Option");
                    return;
                }


                if (Chk_ExmptedTax.Checked == true && Check_WaiveSChg.Checked == true) 
                {
                    DialogResult result = MessageBox.Show("Are u Sure to Making bill Exempt Tax & Waive Service Charges" , GlobalVariable.gCompanyName, buttons);
                    if (result == DialogResult.Yes)
                    {
                        sqlstring = " UPDATE KOT_DET SET PACKAMOUNT =0,TipsAmt=0,AdCgsAmt=0,PartyAmt=0,RoomAmt =0 WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE KOT_DET_TAX SET TAXAMT = 0 WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE KOT_DET SET ExemptTaxFlag ='Y',WaiveSCGFlag='Y' WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (KOrderNo) + "'";
                        List.Add(sqlstring);
                        if (GCon.Moretransaction(List) > 0)
                        {
                            List.Clear();
                        }
                    }
                    else { return; }
                }
                else if (Chk_ExmptedTax.Checked == true && Check_WaiveSChg.Checked == false) 
                {
                    DialogResult result = MessageBox.Show("Are u Sure to Making bill Exempt Tax", GlobalVariable.gCompanyName, buttons);
                    if (result == DialogResult.Yes)
                    {
                        sqlstring = " UPDATE KOT_DET_TAX SET TAXAMT = 0 WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE KOT_DET SET ExemptTaxFlag ='Y',WaiveSCGFlag='N' WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (KOrderNo) + "'";
                        List.Add(sqlstring);
                        if (GCon.Moretransaction(List) > 0)
                        {
                            List.Clear();
                        }
                    }
                    else { return; }
                }
                else if (Chk_ExmptedTax.Checked == false && Check_WaiveSChg.Checked == true)
                {
                    DialogResult result = MessageBox.Show("Are u Sure to Making bill, Waive Service Charges", GlobalVariable.gCompanyName, buttons);
                    if (result == DialogResult.Yes)
                    {
                        sqlstring = " UPDATE KOT_DET SET PACKAMOUNT =0,TipsAmt=0,AdCgsAmt=0,PartyAmt=0,RoomAmt =0 WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE KOT_DET_TAX SET TAXAMT = 0 WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  AND ISNULL(Trans_Flag,'') <> '' ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE KOT_DET SET ExemptTaxFlag ='N',WaiveSCGFlag='Y' WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                        sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (KOrderNo) + "'";
                        List.Add(sqlstring);
                        if (GCon.Moretransaction(List) > 0)
                        {
                            List.Clear();
                        }
                    }
                    else { return; }
                }

                BillNo = Convert.ToInt32(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(BILLno,1,6) As Numeric)),0) + 1  FROM BILL_HDR WHERE BILLDETAILS LIKE '" + DocType + "%' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                //BillDetails = DocType + "/" + BillNo.ToString("000000") + "/" + FinYear;
                BillDetails = DocType + "/" + BillNo.ToString("000000") ;
                if (GlobalVariable.gCompName == "CSC")
                {
                    BillNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'GCSC' ")) + 1;
                    BillDetails = "G" + DocType + "/" + BillNo.ToString("000000") + "/" + FinYear_CSC;
                }

                if (NCFlag == "Y" && GlobalVariable.gCompName != "CSC") 
                {
                    BillNo = Convert.ToInt32(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(BILLno,1,6) As Numeric)),0) + 1  FROM BILL_HDR WHERE BILLDETAILS LIKE '" + NCDocType + "%' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                    BillDetails = NCDocType + "/" + BillNo.ToString("000000");
                }

                DataTable KotDet = new DataTable();
                if (GlobalVariable.gCompName == "CSC")
                {
                    sqlstring = "SELECT H.KOTDETAILS,H.KotNo,H.Kotdate,H.TableNo,H.LocCode,H.ChairSeqNo,ISNULL(H.MCODE,'') AS MCODE,ISNULL(H.MNAME,'') AS MNAME,ISNULL(H.CARDHOLDERCODE,'') AS CARDHOLDERCODE,ISNULL(H.CARDHOLDERNAME,'') AS CARDHOLDERNAME,SUM(D.AMOUNT) AS AMOUNT,SUM(ISNULL(D.TAXAMOUNT,0)) AS TAXAMOUNT,SUM(ISNULL(D.PACKAMOUNT,0)) AS PACKAMOUNT,";
                    sqlstring = sqlstring + " SUM(ISNULL(D.TipsAmt,0)) AS TipsAmt,SUM(ISNULL(D.AdCgsAmt,0)) AS AdCgsAmt,SUM(ISNULL(D.PartyAmt,0)) AS PartyAmt,SUM(ISNULL(D.RoomAmt,0)) AS RoomAmt,SUM(ISNULL(D.ModifierCharges,0)) AS ModifierCharges,Isnull(GSTFlagKot,'NO') as GSTFlagKot ";
                    sqlstring = sqlstring + " FROM KOT_DET D,KOT_HDR H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND H.Kotdetails = '" + KOrderNo + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' And Isnull(Billdetails,'') = '' ";
                    sqlstring = sqlstring + " GROUP BY H.KOTDETAILS,H.KotNo,H.Kotdate,H.TableNo,H.LocCode,H.ChairSeqNo,ISNULL(H.MCODE,''),ISNULL(H.MNAME,''),ISNULL(H.CARDHOLDERCODE,''),ISNULL(H.CARDHOLDERNAME,''),Isnull(GSTFlagKot,'NO') ";
                }
                else
                {
                    sqlstring = "SELECT H.KOTDETAILS,H.KotNo,H.Kotdate,H.TableNo,H.LocCode,H.ChairSeqNo,ISNULL(H.MCODE,'') AS MCODE,ISNULL(H.MNAME,'') AS MNAME,ISNULL(H.CARDHOLDERCODE,'') AS CARDHOLDERCODE,ISNULL(H.CARDHOLDERNAME,'') AS CARDHOLDERNAME,SUM(D.AMOUNT) AS AMOUNT,SUM(ISNULL(D.TAXAMOUNT,0)) AS TAXAMOUNT,SUM(ISNULL(D.PACKAMOUNT,0)) AS PACKAMOUNT,";
                    sqlstring = sqlstring + " SUM(ISNULL(D.TipsAmt,0)) AS TipsAmt,SUM(ISNULL(D.AdCgsAmt,0)) AS AdCgsAmt,SUM(ISNULL(D.PartyAmt,0)) AS PartyAmt,SUM(ISNULL(D.RoomAmt,0)) AS RoomAmt,SUM(ISNULL(D.ModifierCharges,0)) AS ModifierCharges ";
                    sqlstring = sqlstring + " FROM KOT_DET D,KOT_HDR H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND H.Kotdetails = '" + KOrderNo + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' And Isnull(Billdetails,'') = '' ";
                    sqlstring = sqlstring + " GROUP BY H.KOTDETAILS,H.KotNo,H.Kotdate,H.TableNo,H.LocCode,H.ChairSeqNo,ISNULL(H.MCODE,''),ISNULL(H.MNAME,''),ISNULL(H.CARDHOLDERCODE,''),ISNULL(H.CARDHOLDERNAME,'') ";
                }
                KotDet = GCon.getDataSet(sqlstring);
                if (KotDet.Rows.Count > 0) 
                {
                    if (GlobalVariable.gCompName == "CSC") 
                    {
                        for (i = 0; i < KotDet.Rows.Count; i++) 
                        {
                            DataRow dr1 = KotDet.Rows[i];
                            if (dr1["GSTFlagKot"].ToString() == "YES") 
                            {
                                TaxB = true;
                            }
                            else if (dr1["GSTFlagKot"].ToString() == "NO")
                            {
                                NTaxB = true;
                            }
                            else { NTaxB = true; }
                        }
                        if (TaxB == true && NTaxB == true) 
                        {
                            BillNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'GCSC' ")) + 1;
                            BillDetails = "GCSC" + "/" + BillNo.ToString("000000") + "/" + FinYear_CSC;
                            NBillNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'NCSC' ")) + 1;
                            NBillDetails = "NCSC" + "/" + NBillNo.ToString("000000") + "/" + FinYear_CSC;
                        }
                        else if (TaxB == true && NTaxB == false) 
                        {
                            BillNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'GCSC' ")) + 1;
                            BillDetails = "GCSC" + "/" + BillNo.ToString("000000") + "/" + FinYear_CSC;
                            NBillNo = Convert.ToInt32(0);
                            NBillDetails = "";
                        }
                        else if (TaxB == false && NTaxB == true)
                        {
                            BillNo = Convert.ToInt32(0);
                            BillDetails = "";
                            NBillNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0),DOCFLAG FROM PoSKotDoc with(rowlock,holdlock) Where DocType = 'NCSC' ")) + 1;
                            NBillDetails = "NCSC" + "/" + NBillNo.ToString("000000") + "/" + FinYear_CSC;
                        }
                        for (i = 0; i < KotDet.Rows.Count; i++)
                        {
                            DataRow dr = KotDet.Rows[i];
                            BillDate = Convert.ToDateTime(dr["Kotdate"]);
                            BillAmount = BillAmount + Convert.ToDouble(dr["TAXAMOUNT"]) + Convert.ToDouble(dr["AMOUNT"]) + Convert.ToDouble(dr["PACKAMOUNT"]) + Convert.ToDouble(dr["TipsAmt"]) + Convert.ToDouble(dr["AdCgsAmt"]) + Convert.ToDouble(dr["PartyAmt"]) + Convert.ToDouble(dr["RoomAmt"]) + Convert.ToDouble(dr["ModifierCharges"]);
                            if (dr["GSTFlagKot"].ToString() == "YES" && TaxB == true)
                            {
                                //Bill_Hdr
                                sqlstring = "Insert Into Bill_Hdr (FinYear,Billno,BillDetails,BillDate,BillTime,TaxAmount,BillAmount,Roundoff,PayMentmode,Paymentaccountcode,SubPaymentMode,Subpaymentaccountcode,Mcode,Mname, ";
                                sqlstring = sqlstring + " CARDHOLDERCODE,CARDHOLDERNAME,Scode,Sname,CroStatus,Roomno,ChkId,Guest,AddUserId,AddDatetime,Upduserid,DelFlag,Adjflag,Adjdate,Minimumusage,CardAmount,remarks,Discountamount,Packamount, ";
                                sqlstring = sqlstring + " TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,STWCODE,STWNAME,SerType,LocCode,LocName,ChairSeqNo,NCRemarks,ModifierCharges) ";
                                sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + BillNo + "','" + BillDetails + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(DateAndTime.Now, "T") + "'," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",0,'','','','','" + dr["MCODE"] + "','" + dr["MNAME"] + "',";
                                sqlstring = sqlstring + " '" + dr["CARDHOLDERCODE"] + "','" + dr["CARDHOLDERNAME"] + "','','','N','0','0','','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','','N','N','',0,0,'" + Txt_Remarks.Text + "',0," + Convert.ToDouble(dr["PACKAMOUNT"]) + ",";
                                sqlstring = sqlstring + " " + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ",'','','" + GlobalVariable.ServiceType + "'," + dr["LocCode"] + ",'" + GlobalVariable.SLocation + "'," + dr["ChairSeqNo"] + ",'" + Txt_NCRemarks.Text + "'," + Convert.ToDouble(dr["ModifierCharges"]) + ") ";
                                List.Add(sqlstring);

                                //Bill_Det
                                sqlstring = "Insert Into Bill_Det (Billno,BillDetails,OthBillDetails,BillDate,KotDate,KotDetails,TaxCode,Taxamount,KotAmount,DelFlag,Packamount,TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,OthbillDetails1) ";
                                sqlstring = sqlstring + " Values ('" + BillNo + "','" + BillDetails + "','" + NBillDetails + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + dr["KOTDETAILS"] + "',''," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",'N',";
                                sqlstring = sqlstring + " " + Convert.ToDouble(dr["PACKAMOUNT"]) + "," + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ",'')";
                                List.Add(sqlstring);

                                //Update in Kot Det & Kot Hdr
                                sqlstring = " UPDATE KOT_HDR SET BillStatus = 'ST' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                                List.Add(sqlstring);
                                sqlstring = " UPDATE KOT_DET SET BILLDETAILS = '" + BillDetails + "' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(GSTFlagKot,'NO') = 'YES' ";
                                List.Add(sqlstring);
                            }
                            if (dr["GSTFlagKot"].ToString() == "NO" && NTaxB == true)
                            {
                                //Bill_Hdr
                                sqlstring = "Insert Into Bill_Hdr (FinYear,Billno,BillDetails,BillDate,BillTime,TaxAmount,BillAmount,Roundoff,PayMentmode,Paymentaccountcode,SubPaymentMode,Subpaymentaccountcode,Mcode,Mname, ";
                                sqlstring = sqlstring + " CARDHOLDERCODE,CARDHOLDERNAME,Scode,Sname,CroStatus,Roomno,ChkId,Guest,AddUserId,AddDatetime,Upduserid,DelFlag,Adjflag,Adjdate,Minimumusage,CardAmount,remarks,Discountamount,Packamount, ";
                                sqlstring = sqlstring + " TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,STWCODE,STWNAME,SerType,LocCode,LocName,ChairSeqNo,NCRemarks,ModifierCharges) ";
                                sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + NBillNo + "','" + NBillDetails + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(DateAndTime.Now, "T") + "'," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",0,'','','','','" + dr["MCODE"] + "','" + dr["MNAME"] + "',";
                                sqlstring = sqlstring + " '" + dr["CARDHOLDERCODE"] + "','" + dr["CARDHOLDERNAME"] + "','','','N','0','0','','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','','N','N','',0,0,'" + Txt_Remarks.Text + "',0," + Convert.ToDouble(dr["PACKAMOUNT"]) + ",";
                                sqlstring = sqlstring + " " + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ",'','','" + GlobalVariable.ServiceType + "'," + dr["LocCode"] + ",'" + GlobalVariable.SLocation + "'," + dr["ChairSeqNo"] + ",'" + Txt_NCRemarks.Text + "'," + Convert.ToDouble(dr["ModifierCharges"]) + ") ";
                                List.Add(sqlstring);

                                //Bill_Det
                                sqlstring = "Insert Into Bill_Det (Billno,BillDetails,OthBillDetails,BillDate,KotDate,KotDetails,TaxCode,Taxamount,KotAmount,DelFlag,Packamount,TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,OthbillDetails1) ";
                                sqlstring = sqlstring + " Values ('" + NBillNo + "','" + NBillDetails + "','" + BillDetails + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + dr["KOTDETAILS"] + "',''," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",'N',";
                                sqlstring = sqlstring + " " + Convert.ToDouble(dr["PACKAMOUNT"]) + "," + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ",'')";
                                List.Add(sqlstring);

                                //Update in Kot Det & Kot Hdr
                                sqlstring = " UPDATE KOT_HDR SET BillStatus = 'ST' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                                List.Add(sqlstring);
                                sqlstring = " UPDATE KOT_DET SET BILLDETAILS = '" + NBillDetails + "' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(GSTFlagKot,'NO') = 'NO' ";
                                List.Add(sqlstring);
                            }
                        }
                    }
                    else 
                    {
                        for (i = 0; i < KotDet.Rows.Count; i++)
                        {
                            DataRow dr = KotDet.Rows[i];
                            BillDate = Convert.ToDateTime(dr["Kotdate"]);
                            BillAmount = BillAmount + Convert.ToDouble(dr["TAXAMOUNT"]) + Convert.ToDouble(dr["AMOUNT"]) + Convert.ToDouble(dr["PACKAMOUNT"]) + Convert.ToDouble(dr["TipsAmt"]) + Convert.ToDouble(dr["AdCgsAmt"]) + Convert.ToDouble(dr["PartyAmt"]) + Convert.ToDouble(dr["RoomAmt"]) + Convert.ToDouble(dr["ModifierCharges"]);
                            //Bill_Hdr
                            sqlstring = "Insert Into Bill_Hdr (FinYear,Billno,BillDetails,BillDate,BillTime,TaxAmount,BillAmount,Roundoff,PayMentmode,Paymentaccountcode,SubPaymentMode,Subpaymentaccountcode,Mcode,Mname, ";
                            sqlstring = sqlstring + " CARDHOLDERCODE,CARDHOLDERNAME,Scode,Sname,CroStatus,Roomno,ChkId,Guest,AddUserId,AddDatetime,Upduserid,DelFlag,Adjflag,Adjdate,Minimumusage,CardAmount,remarks,Discountamount,Packamount, ";
                            sqlstring = sqlstring + " TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,STWCODE,STWNAME,SerType,LocCode,LocName,ChairSeqNo,NCRemarks,ModifierCharges) ";
                            sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + BillNo + "','" + BillDetails + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(DateAndTime.Now, "T") + "'," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",0,'','','','','" + dr["MCODE"] + "','" + dr["MNAME"] + "',";
                            sqlstring = sqlstring + " '" + dr["CARDHOLDERCODE"] + "','" + dr["CARDHOLDERNAME"] + "','','','N','0','0','','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','','N','N','',0,0,'" + Txt_Remarks.Text + "',0," + Convert.ToDouble(dr["PACKAMOUNT"]) + ",";
                            sqlstring = sqlstring + " " + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ",'','','" + GlobalVariable.ServiceType + "'," + dr["LocCode"] + ",'" + GlobalVariable.SLocation + "'," + dr["ChairSeqNo"] + ",'" + Txt_NCRemarks.Text + "'," + Convert.ToDouble(dr["ModifierCharges"]) + ") ";
                            List.Add(sqlstring);

                            //Bill_Det
                            sqlstring = "Insert Into Bill_Det (Billno,BillDetails,OthBillDetails,BillDate,KotDate,KotDetails,TaxCode,Taxamount,KotAmount,DelFlag,Packamount,TipsAmt,AdCgsAmt,PartyAmt,RoomAmt) ";
                            sqlstring = sqlstring + " Values ('" + BillNo + "','" + BillDetails + "','','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + Strings.Format(dr["Kotdate"], "dd-MMM-yyyy") + "','" + dr["KOTDETAILS"] + "',''," + Convert.ToDouble(dr["TAXAMOUNT"]) + "," + Convert.ToDouble(dr["AMOUNT"]) + ",'N',";
                            sqlstring = sqlstring + " " + Convert.ToDouble(dr["PACKAMOUNT"]) + "," + Convert.ToDouble(dr["TipsAmt"]) + "," + Convert.ToDouble(dr["AdCgsAmt"]) + "," + Convert.ToDouble(dr["PartyAmt"]) + "," + Convert.ToDouble(dr["RoomAmt"]) + ")";
                            List.Add(sqlstring);

                            //Update in Kot Det & Kot Hdr
                            sqlstring = " UPDATE KOT_HDR SET BillStatus = 'ST' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                            List.Add(sqlstring);
                            sqlstring = " UPDATE KOT_DET SET BILLDETAILS = '" + BillDetails + "' WHERE KOTDETAILS = '" + dr["KOTDETAILS"] + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                            List.Add(sqlstring);
                        }
                    }

                    for (i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        Double PayAmt = 0;
                        if (dataGridView2.Rows[i].Cells[1].Value != null)
                        {
                            if (dataGridView2.Rows[i].Cells[0].Value != null)
                            {
                                DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                            }
                            else { DescVal = ""; }
                            if (dataGridView2.Rows[i].Cells[1].Value != null)
                            {
                                PayAmt = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                                if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
                                {
                                    ////sqlstring = "Insert Into BillSettlement (FinYear,Billno,BILLDATE,PAYMENTMODE,PAYMENTACCOUNTCODE,MCODE,MNAME,CARDTYPE,INSTRUMENTNO,BANKNAME,RECEIVEDNAME,PAYAMOUNT,BILLAMOUNT,BALANCEAMOUNT,ADDUSERID,ADDDATETIME,DELFLAG) ";
                                    ////sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + BillDetails + "','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','" + DescVal + "','','','','','','',''," + PayAmt + "," + BillAmount + "," + Convert.ToDouble(Txt_BalAmt.Text) + ",'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N')";
                                    ////List.Add(sqlstring);
                                    if (GlobalVariable.gCompName == "CSC")
                                    {
                                        if (TaxB == true) 
                                        {
                                            sqlstring = "Insert Into BillSettlement (FinYear,Billno,BILLDATE,PAYMENTMODE,PAYMENTACCOUNTCODE,MCODE,MNAME,CARDTYPE,INSTRUMENTNO,BANKNAME,RECEIVEDNAME,PAYAMOUNT,BILLAMOUNT,BALANCEAMOUNT,ADDUSERID,ADDDATETIME,DELFLAG) ";
                                            sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + BillDetails + "','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','" + DescVal + "','','','','','','',''," + PayAmt + "," + BillAmount + "," + Convert.ToDouble(Txt_BalAmt.Text) + ",'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N')";
                                            List.Add(sqlstring);
                                        }
                                        if (NTaxB == true) 
                                        {
                                            sqlstring = "Insert Into BillSettlement (FinYear,Billno,BILLDATE,PAYMENTMODE,PAYMENTACCOUNTCODE,MCODE,MNAME,CARDTYPE,INSTRUMENTNO,BANKNAME,RECEIVEDNAME,PAYAMOUNT,BILLAMOUNT,BALANCEAMOUNT,ADDUSERID,ADDDATETIME,DELFLAG) ";
                                            sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + NBillDetails + "','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','" + DescVal + "','','','','','','',''," + PayAmt + "," + BillAmount + "," + Convert.ToDouble(Txt_BalAmt.Text) + ",'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N')";
                                            List.Add(sqlstring);
                                        }
                                    }
                                    else 
                                    {
                                        sqlstring = "Insert Into BillSettlement (FinYear,Billno,BILLDATE,PAYMENTMODE,PAYMENTACCOUNTCODE,MCODE,MNAME,CARDTYPE,INSTRUMENTNO,BANKNAME,RECEIVEDNAME,PAYAMOUNT,BILLAMOUNT,BALANCEAMOUNT,ADDUSERID,ADDDATETIME,DELFLAG) ";
                                        sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + BillDetails + "','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','" + DescVal + "','','','','','','',''," + PayAmt + "," + BillAmount + "," + Convert.ToDouble(Txt_BalAmt.Text) + ",'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N')";
                                        List.Add(sqlstring);
                                    }
                                    PayCount = PayCount + 1; 
                                }
                                if (DescVal == "DISC") 
                                {
                                    ////Double DiscAmt=0, DiscPerc = 0;
                                    ////if (dataGridView2.Rows[i].Cells[1].Value != null) { DiscAmt = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value); }
                                    ////if (dataGridView2.Rows[i].Cells[2].Value != null) { DiscPerc = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value); }
                                    ////sqlstring = "UPDATE BILL_HDR SET DiscPercent=" + (DiscPerc) + ",DiscAmount = " + -(DiscAmt) + " WHERE BillDetails = '" + BillDetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                                    ////List.Add(sqlstring);
                                    DiscCount = DiscCount + 1;
                                }
                                if (DescVal == "ROOM")
                                {
                                    RoomAvail = true;
                                }
                                
                                if (DescVal == "SCARD") 
                                {
                                    if (GlobalVariable.MainCardDeductFlag == "Y") 
                                    {
                                        DataTable CardInfo = new DataTable();
                                        DataTable MainCardInfo = new DataTable();
                                        sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,[16_DIGIT_CODE] FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardCode + "' AND [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y' ";
                                        CardInfo = GCon.getDataSet(sql);
                                        if (CardInfo.Rows.Count > 0) 
                                        {
                                            DataRow dr = CardInfo.Rows[0];
                                            if (dr["ISSUETYPE"].ToString() == "MEM")
                                            {
                                                sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,[16_DIGIT_CODE] FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + dr["MEMBERCODE"].ToString() + "-00' And isnull(Activation_Flag,'') = 'Y' ";
                                                MainCardInfo = GCon.getDataSet(sql);
                                                if (MainCardInfo.Rows.Count > 0) 
                                                {
                                                    DataRow dr1 = MainCardInfo.Rows[0];
                                                    MainCard = dr1["CARDCODE"].ToString();
                                                    MainCardDigitCode = dr1["16_DIGIT_CODE"].ToString();
                                                    CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + MainCard + "' And [16_DIGIT_CODE] = '" + MainCardDigitCode + "' And isnull(Activation_Flag,'') = 'Y'"));
                                                    if (CardBal >= PayAmt)
                                                    {
                                                        if (GlobalVariable.gCompName == "CSC")
                                                        {
                                                            if (BillDetails != "")
                                                            {
                                                                sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + MainCardDigitCode + "','" + MainCard + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                                List.Add(sqlstring);
                                                            }
                                                            else 
                                                            {
                                                                sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + MainCardDigitCode + "','" + MainCard + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + NBillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                                List.Add(sqlstring);
                                                            }
                                                        }
                                                        else 
                                                        {
                                                            sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + MainCardDigitCode + "','" + MainCard + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                            List.Add(sqlstring);
                                                        }
                                                        sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE-" + PayAmt + " WHERE CARDCODE='" + MainCard + "'";
                                                        List.Add(sqlstring);
                                                        string EBal = GCon.abcdAdd((CardBal - PayAmt).ToString());
                                                        sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + MainCard + "'";
                                                        List.Add(sqlstring);
                                                    }
                                                    else { MessageBox.Show("Card Balance Not Availble."); return; }
                                                }
                                                else { MessageBox.Show("Main card Not Issued for this Card,please check!"); return; }
                                            }
                                            else 
                                            {
                                                CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'"));
                                                if (CardBal >= PayAmt)
                                                {
                                                    sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                    List.Add(sqlstring);
                                                    if (GlobalVariable.gCompName == "CSC")
                                                    {
                                                        if (BillDetails != "")
                                                        {
                                                            sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                            List.Add(sqlstring);
                                                        }
                                                        else
                                                        {
                                                            sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + NBillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                            List.Add(sqlstring);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                        List.Add(sqlstring);
                                                    }
                                                    sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE-" + PayAmt + " WHERE CARDCODE='" + CardCode + "'";
                                                    List.Add(sqlstring);
                                                    string EBal = GCon.abcdAdd((CardBal - PayAmt).ToString());
                                                    sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + CardCode + "'";
                                                    List.Add(sqlstring);
                                                }
                                                else { MessageBox.Show("Card Balance Not Availble."); return; }
                                            }
                                        }
                                        else { MessageBox.Show("Card is not Valid,please check again"); return; }
                                    }
                                    else 
                                    {
                                        CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'"));
                                        if (CardBal >= PayAmt)
                                        {
                                            //sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                            //List.Add(sqlstring);
                                            if (GlobalVariable.gCompName == "CSC")
                                            {
                                                if (BillDetails != "")
                                                {
                                                    sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                    List.Add(sqlstring);
                                                }
                                                else
                                                {
                                                    sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + NBillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                    List.Add(sqlstring);
                                                }
                                            }
                                            else
                                            {
                                                sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + DigitCode + "','" + CardCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                                List.Add(sqlstring);
                                            }
                                            sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE-" + PayAmt + " WHERE CARDCODE='" + CardCode + "'";
                                            List.Add(sqlstring);
                                            string EBal = GCon.abcdAdd((CardBal - PayAmt).ToString());
                                            sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + CardCode + "'";
                                            List.Add(sqlstring);
                                        }
                                        else { MessageBox.Show("Card Balance Not Availble."); return; }
                                    }
                                }

                                if (DescVal == "PREPAID")
                                {
                                    DataTable PreCard = new DataTable();
                                    DataTable CardDetails = new DataTable();
                                    Double PrepBal = 0;
                                    Double PrePaidTagAmt = 0;
                                    String PreDigitCode = "";
                                    String PreHolderCode = "";
                                    sql = "select Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(DeductAmt,0) as DeductAmt from Tbl_PrePaidCardTagging Where KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "'";
                                    PreCard = GCon.getDataSet(sql);
                                    if (PreCard.Rows.Count > 0)
                                    {
                                        ////DataRow dr = PreCard.Rows[0];
                                        ////PreDigitCode = dr["DigitCode"].ToString();
                                        ////PreHolderCode = dr["HolderCode"].ToString();
                                        ////sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,Isnull(Balance,0) as Balance FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + PreHolderCode + "' AND [16_DIGIT_CODE] = '" + PreDigitCode + "' And isnull(Activation_Flag,'') = 'Y' AND ISSUETYPE = 'PREP' ";
                                        ////CardDetails = GCon.getDataSet(sql);
                                        ////if (CardDetails.Rows.Count > 0)
                                        ////{
                                        ////    DataRow dr1 = CardDetails.Rows[0];
                                        ////    PrepBal = Convert.ToDouble(dr1["Balance"]);
                                        ////    if (PrepBal >= PayAmt)
                                        ////    {
                                        ////        sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + PreDigitCode + "','" + PreHolderCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PayAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PayAmt + ")";
                                        ////        List.Add(sqlstring);
                                        ////        sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE-" + PayAmt + " WHERE CARDCODE='" + PreHolderCode + "'";
                                        ////        List.Add(sqlstring);
                                        ////        string EBal = GCon.abcdAdd((CardBal - PayAmt).ToString());
                                        ////        sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + PreHolderCode + "'";
                                        ////        List.Add(sqlstring);
                                        ////    }
                                        ////    else { MessageBox.Show("Prepaid Card Balance Not Availble."); return; }
                                        ////}
                                        ////else
                                        ////{
                                        ////    MessageBox.Show("Valid Prepaid Card Not Tag With KOT,Kindly Tag Valid Card then use Prepaid Option ", GlobalVariable.gCompanyName);
                                        ////    return ;
                                        ////}
                                        for (int k = 0; k < PreCard.Rows.Count; k++) 
                                        {
                                            PreDigitCode = PreCard.Rows[k].ItemArray[0].ToString();
                                            PreHolderCode = PreCard.Rows[k].ItemArray[1].ToString();
                                            PrePaidTagAmt = Convert.ToDouble(PreCard.Rows[k].ItemArray[2]);
                                            sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,Isnull(Balance,0) as Balance FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + PreHolderCode + "' AND [16_DIGIT_CODE] = '" + PreDigitCode + "' And isnull(Activation_Flag,'') = 'Y' AND ISSUETYPE = 'PREP' ";
                                            CardDetails = GCon.getDataSet(sql);
                                            if (CardDetails.Rows.Count > 0) 
                                            {
                                                DataRow dr1 = CardDetails.Rows[0];
                                                PrepBal = Convert.ToDouble(dr1["Balance"]);
                                                CardBal = PrepBal;
                                                if (PrepBal >= PrePaidTagAmt) 
                                                {
                                                    sqlstring = " INSERT INTO SM_POSTRANSACTION ([16_DIGIT_CODE],CARDCODE,POSCODE,POSDATE,FROM_DATE,TO_DATE,FROM_TIME,TO_TIME,DURATION,BILL_NO,BILL_AMOUNT,ADDDATETIME,ADDUSERID,VOID,REMARKS,DEDUCT_TYPE,DEDUCT_FROM_card) VALUES ( '" + PreDigitCode + "','" + PreHolderCode + "','01','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','','','','','','" + BillDetails + "'," + PrePaidTagAmt + ",'" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "','N','','FC'," + PrePaidTagAmt + ")";
                                                    List.Add(sqlstring);
                                                    sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE-" + PrePaidTagAmt + " WHERE CARDCODE='" + PreHolderCode + "'";
                                                    List.Add(sqlstring);
                                                    string EBal = GCon.abcdAdd((CardBal - PrePaidTagAmt).ToString());
                                                    sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + PreHolderCode + "'";
                                                    List.Add(sqlstring);
                                                }
                                                else { MessageBox.Show("Prepaid Card Balance Not Availble For :" + PreHolderCode); return; }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Valid Prepaid Card Not Tag With KOT,Kindly Tag Valid Card then use Prepaid Option ", GlobalVariable.gCompanyName);
                                                return ;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Prepaid Card Not Tag With KOT,Kindly Tag first then use Prepaid Option ", GlobalVariable.gCompanyName);
                                        return ;
                                    }
                                }

                                if (DescVal == "TIPS") 
                                {
                                    sqlstring = " UPDATE BILL_HDR SET ExtraTips = " + PayAmt + " WHERE BillDetails = '" + BillDetails + "' ";
                                    List.Add(sqlstring);
                                }
                            }
                        }
                    }

                    if (DiscCount == 0)
                    {
                        sqlstring = "Update Kot_Det Set ItemDiscPerc = 0 ,DiscCategory = '',DiscUser = ''  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                        List.Add(sqlstring);
                    }
                    else if (DiscCount > 0)
                    {
                        sqlstring = "Update Kot_Det Set ItemDiscAmt = (Amount * Isnull(ItemDiscPerc,0)) / 100 ,DiscCategory = '" + DiscountCategory + "',DiscUser = '" + DiscountUserName + "'  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                    }

                    if (VariableRefund > 0) 
                    {
                        sqlstring = " UPDATE BILL_HDR SET RefundAmt = " + VariableRefund + " WHERE BillDetails = '" + BillDetails + "' ";
                        List.Add(sqlstring);
                    }

                    if (PayCount == 1 && GlobalVariable.gCompName == "KGA") 
                    {
                        sqlstring = " Update KOT_det set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,KOT_det D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + BillDetails + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update KOT_HDR set PaymentType = D.PAYMENTMODE from KOT_det D,KOT_HDR B where B.Kotdetails = D.KOTDETAILS And D.BILLDETAILS = '" + BillDetails + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update BILL_hdr set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,BILL_hdr D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + BillDetails + "' ";
                        List.Add(sqlstring);
                    }

                    if (PayCount == 1 && GlobalVariable.MultiPayMode == "NO")
                    {
                        sqlstring = " Update KOT_det set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,KOT_det D where B.BILLNO = D.BILLDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + BillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update KOT_HDR set PaymentType = D.PAYMENTMODE from KOT_det D,KOT_HDR B where B.Kotdetails = D.KOTDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + BillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update BILL_hdr set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,BILL_hdr D where B.BILLNO = D.BILLDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + BillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                        if (GlobalVariable.gCompName == "CSC") 
                        {
                            sqlstring = " Update KOT_det set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,KOT_det D where B.BILLNO = D.BILLDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + NBillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                            List.Add(sqlstring);
                            sqlstring = " Update KOT_HDR set PaymentType = D.PAYMENTMODE from KOT_det D,KOT_HDR B where B.Kotdetails = D.KOTDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + NBillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                            List.Add(sqlstring);
                            sqlstring = " Update BILL_hdr set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,BILL_hdr D where B.BILLNO = D.BILLDETAILS AND B.FinYear = D.FinYear And D.BILLDETAILS = '" + NBillDetails + "' AND B.FinYear = '" + FinYear1 + "' ";
                            List.Add(sqlstring);
                        }
                    }

                    sqlstring = " UPDATE BillSettlement SET MCODE = B.Mcode,MNAME = B.Mname FROM BILL_HDR B,BillSettlement S WHERE B.BillDetails = S.BILLNO AND B.FinYear = S.FinYear AND B.BILLDETAILS = '" + BillDetails + "' AND S.FinYear = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE BillSettlement SET MCODE = T.ARCode,MNAME = T.ARName FROM kot_det K,Tbl_ARFlagUpdation T,BillSettlement S WHERE K.BILLDETAILS = S.BILLNO AND K.FinYear = S.FinYear  AND K.KOTDETAILS = T.KotNo AND S.BILLNO = '" + BillDetails + "' AND S.FinYear = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    if (GlobalVariable.gCompName == "CSC" && NTaxB == true) 
                    {
                        sqlstring = " UPDATE BillSettlement SET MCODE = B.Mcode,MNAME = B.Mname FROM BILL_HDR B,BillSettlement S WHERE B.BillDetails = S.BILLNO AND B.FinYear = S.FinYear AND B.BILLDETAILS = '" + NBillDetails + "' AND S.FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                        sqlstring = " UPDATE BillSettlement SET MCODE = T.ARCode,MNAME = T.ARName FROM kot_det K,Tbl_ARFlagUpdation T,BillSettlement S WHERE K.BILLDETAILS = S.BILLNO AND K.FinYear = S.FinYear  AND K.KOTDETAILS = T.KotNo AND S.BILLNO = '" + NBillDetails + "' AND S.FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                    }

                    if (RoomAvail == true) 
                    {
                        sqlstring = " Insert into roomledger (Chkno,Docno,DocDate,Doctype,Foliono,Amount,PosCode,Roomno,RefNo,CreditDebit,Paymentmode,SlCode,Description,Cancel,AddUserid,AddDatetime,VoidStatus,vouchertype,vouchercategory,Taxcode,Source,BookingId) ";
                        sqlstring = sqlstring + " SELECT (SELECT TOP 1 Checkin FROM kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND H.FinYear = D.FinYear AND D.BILLDETAILS = B.BILLNO AND D.FinYear = B.FinYear ) AS CHKNO,BILLNO AS DOCNO,BILLDATE,'SALE' AS DOCTYPE,1 AS Foliono,PAYAMOUNT,'' as poscode, ";
                        sqlstring = sqlstring + " (SELECT TOP 1 RoomNo FROM kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND H.FinYear = D.FinYear AND D.BILLDETAILS = B.BILLNO  AND D.FinYear = B.FinYear ) as Roomno,0 as Refno,'DEBIT' as creditdebit,'ROOM' as Paymentmode,'' as slcode,'POSBILL-' + BILLNO as description,'N' as Cancel,B.ADDUSERID,B.ADDDATETIME,'N' as VoidStatus,'RM' as vouchertype,'RM' as vouchercategory,'' as taxcode,'POS' as Source, ";
                        sqlstring = sqlstring + " (select TOP 1 isnull(Reservationid,0) from RoomCheckin R,kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND D.BILLDETAILS = B.BILLNO AND H.FinYear = D.FinYear AND D.FinYear = B.FinYear and isnull(R.ChkNo,0) = isnull(H.Checkin,0)) as Bookingid ";
                        sqlstring = sqlstring + " FROM BillSettlement B WHERE B.PAYMENTMODE = 'ROOM'  AND BILLNO = '" + BillDetails + "' ";
                        List.Add(sqlstring);
                    }

                    if (GlobalVariable.gCompName == "CSC")
                    {
                        if (TaxB == true && NTaxB == true)
                        {
                            sqlstring = "Update PoSKotDoc Set DocNo = " + BillNo + ",DOCFLAG='N' Where DocType = 'GCSC'";
                            List.Add(sqlstring);
                            sqlstring = "Update PoSKotDoc Set DocNo = " + NBillNo + ",DOCFLAG='N' Where DocType = 'NCSC'";
                            List.Add(sqlstring);
                        }
                        else if (TaxB == true && NTaxB == false)
                        {
                            sqlstring = "Update PoSKotDoc Set DocNo = " + BillNo + ",DOCFLAG='N' Where DocType = 'GCSC'";
                            List.Add(sqlstring);
                        }
                        else if (TaxB == false && NTaxB == true)
                        {
                            sqlstring = "Update PoSKotDoc Set DocNo = " + NBillNo + ",DOCFLAG='N' Where DocType = 'NCSC'";
                            List.Add(sqlstring);
                        }
                    }

                    ////sqlstring = "Delete from PosTableStatus where TableNo = '" + GlobalVariable.TableNo + "'";
                    ////List.Add(sqlstring);

                    if (GCon.Moretransaction(List) > 0)
                    {
                        //MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sql = "UPDATE TableMaster SET OpenStatus = '' WHERE Pos IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
                        GCon.dataOperation(1, sql);
                        sql = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE LocCode IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
                        GCon.dataOperation(1, sql);
                        sql = "Insert into PosTableStatus_LOG(LocCode,TableNo,Ttype,Mcode,Mname,CardCode,TotalPax,TotalKot,OccupiedFrom,Remarks,AddUserId,AddDateTime)";
                        sql = sql + " select LocCode,TableNo,Ttype,Mcode,Mname,CardCode,TotalPax,TotalKot,OccupiedFrom,Remarks,'" + GlobalVariable.gUserName + "',getdate() from PosTableStatus where LocCode IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') and TableNo = '" + GlobalVariable.TableNo + "'";
                        GCon.dataOperation(1, sql);
                        sql = "Delete from PosTableStatus where LocCode IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "') and TableNo = '" + GlobalVariable.TableNo + "'";
                        GCon.dataOperation(1, sql);

                        //TotAmount = Convert.ToDouble(GCon.getValue("SELECT Isnull(Round(SUM(AMOUNT),0),0) FROM KotAccountCheck WHERE BillDetails = '" + BillDetails + "' AND CREDITDEBIT = 'CREDIT'"));
                        TrnDone = true;
                        List.Clear();
                        ////if (GlobalVariable.gCompName == "MONTANA")
                        ////{
                        ////    EInvoicing(BillDetails, FinYear1);
                        ////}
                        gPrint = true;
                        if (GlobalVariable.gCompName == "NZC")
                        {
                            PrintOperationNZC(BillDetails);
                        }
                        else if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "ITCD" || GlobalVariable.gCompName == "TRNG") 
                        {
                            PrintOPeration_Windows(BillDetails);
                        }
                        else if (GlobalVariable.gCompName == "CSC")
                        {
                            if (BillDetails != "") { PrintOperation(BillDetails); }
                            if (NBillDetails != "") { PrintOperation(NBillDetails); }
                        }
                        else if (GlobalVariable.gCompName == "CFC")
                        {
                            PrintOperation_CFC_Lat(BillDetails);
                        }
                        else
                        {
                            PrintOperation(BillDetails);
                        }
                        if (GlobalVariable.gCompName == "SKYYE") 
                        {
                            GCon.SendSMS_SkyyeBill(BillDetails);
                        }
                        else if (GlobalVariable.gCompName == "HPRC")
                        {
                            GCon.SendSMS_HPRCBill(BillDetails);
                        }
                        if (GlobalVariable.gCompName == "EPC" || GlobalVariable.AccountPostFlag == "YES")
                        {
                            sqlstring = " EXEC [PROC_JOURNAL_POSPOST_DIR] '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','P','" + BillDetails + "' ";
                            List.Add(sqlstring);
                            if (GCon.Moretransaction(List) > 0)
                            {
                                List.Clear();
                            }
                        }
                        if (GlobalVariable.ServiceType == "Dine-In")
                        {
                            ServiceLocation SL = new ServiceLocation();
                            SL.Show();
                            this.Close();
                        }
                        else
                        {
                            if (GlobalVariable.ServiceType == "Direct-Billing")
                            {
                                GlobalVariable.ServiceType = "Direct-Billing";
                                DataTable dt = new DataTable();
                                sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
                                dt = GCon.getDataSet(sql);
                                if (dt.Rows.Count > 0)
                                {
                                    DataTable ChkChair = new DataTable();
                                    int ChNo = 1;
                                    GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                                    GlobalVariable.TableNo = "V1";
                                    EntryForm EF = new EntryForm();
                                    EF.Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                                    sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                                    ChkChair = GCon.getDataSet(sql);
                                    if (ChkChair.Rows.Count > 0)
                                    {
                                        ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                                    }
                                    else { ChNo = 1; }
                                    int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                                    if (RowCnt > 0)
                                    {
                                        EF.UpdFlag = true;
                                        GlobalVariable.ChairNo = ChNo;
                                    }
                                    else
                                    {
                                        EF.UpdFlag = false;
                                        GlobalVariable.ChairNo = ChNo;
                                        EF.Pax = 1;
                                    }
                                    EF.Show();
                                    this.Close();
                                }
                            }
                            else 
                            {
                                ServiceType SL = new ServiceType();
                                SL.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TrnDone = false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckValidate() 
        {
            string DescVal = "";
            bool RoomAccount = false;
            Double CardBal = 0;
            int PayCount = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                Double PayAmt = 0;
                if (dataGridView2.Rows[i].Cells[1].Value != null)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value != null)
                    {
                        DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    }
                    else { DescVal = ""; }
                    if (dataGridView2.Rows[i].Cells[1].Value != null)
                    {
                        PayAmt = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                        if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT")
                        {
                            PayCount = PayCount + 1;
                            if (DescVal == "SCARD") 
                            {
                                if (GlobalVariable.MainCardDeductFlag == "Y")
                                {
                                    DataTable CardInfo = new DataTable();
                                    sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardCode + "' AND [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y' ";
                                    CardInfo = GCon.getDataSet(sql);
                                    if (CardInfo.Rows.Count > 0) 
                                    {
                                        DataRow dr = CardInfo.Rows[0];
                                        if (dr["ISSUETYPE"].ToString() == "MEM")
                                        {
                                            CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + dr["MEMBERCODE"].ToString() + "-00' And isnull(Activation_Flag,'') = 'Y'"));
                                        }
                                        else 
                                        {
                                            CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'"));
                                        }
                                    }
                                }
                                else 
                                {
                                    CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'"));
                                }
                            }
                            if (DescVal == "NC" && Txt_NCRemarks.Text.Length == 0) 
                            {
                                MessageBox.Show("NC Remarks Should Not be Blank", GlobalVariable.gCompanyName);
                                Grp_NCRemarks.Visible = true;
                                Txt_NCRemarks.Focus();
                                return false; 
                            }
                            if (CardBal < PayAmt && DescVal == "SCARD") 
                            {
                                MessageBox.Show("Smart Card Balance Not Available", GlobalVariable.gCompanyName);
                                return false; 
                            }
                            if (DescVal == "PREPAID") 
                            {
                                DataTable PreCard = new DataTable();
                                DataTable CardDetails = new DataTable();
                                Double PrepBal = 0;
                                Double PrePaidTagAmt = 0;
                                String PreDigitCode = "";
                                String PreHolderCode = "";
                                sql = "select Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(DeductAmt,0) as DeductAmt from Tbl_PrePaidCardTagging Where KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "'";
                                PreCard = GCon.getDataSet(sql);
                                if (PreCard.Rows.Count > 0)
                                {
                                    ////DataRow dr = PreCard.Rows[0];
                                    ////PreDigitCode = dr["DigitCode"].ToString();
                                    ////PreHolderCode = dr["HolderCode"].ToString();
                                    ////sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,Isnull(Balance,0) as Balance FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + PreHolderCode + "' AND [16_DIGIT_CODE] = '" + PreDigitCode + "' And isnull(Activation_Flag,'') = 'Y' AND ISSUETYPE = 'PREP' ";
                                    ////CardDetails = GCon.getDataSet(sql);
                                    ////if (CardDetails.Rows.Count > 0)
                                    ////{
                                    ////    DataRow dr1 = CardDetails.Rows[0];
                                    ////    PrepBal = Convert.ToDouble(dr1["Balance"]);
                                    ////    if (PrepBal < PayAmt && DescVal == "PREPAID")
                                    ////    {
                                    ////        MessageBox.Show("Prepaid Card Balance Not Available, Available Balance in Tag Prepaid Card is :" + PrepBal, GlobalVariable.gCompanyName);
                                    ////        return false;
                                    ////    }
                                    ////}
                                    ////else
                                    ////{
                                    ////    MessageBox.Show("Valid Prepaid Card Not Tag With KOT,Kindly Tag Valid Card then use Prepaid Option ", GlobalVariable.gCompanyName);
                                    ////    return false;
                                    ////}
                                    for (int j = 0; j < PreCard.Rows.Count; j++) 
                                    {
                                        PreDigitCode = PreCard.Rows[j].ItemArray[0].ToString();
                                        PreHolderCode = PreCard.Rows[j].ItemArray[1].ToString();
                                        PrePaidTagAmt = Convert.ToDouble(PreCard.Rows[j].ItemArray[2]);
                                        sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,Isnull(Balance,0) as Balance FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + PreHolderCode + "' AND [16_DIGIT_CODE] = '" + PreDigitCode + "' And isnull(Activation_Flag,'') = 'Y' AND ISSUETYPE = 'PREP' ";
                                        CardDetails = GCon.getDataSet(sql);
                                        if (CardDetails.Rows.Count > 0)
                                        {
                                            DataRow dr1 = CardDetails.Rows[0];
                                            PrepBal = Convert.ToDouble(dr1["Balance"]);
                                            if (PrepBal < PrePaidTagAmt && DescVal == "PREPAID") 
                                            {
                                                MessageBox.Show("Holder " + PreHolderCode + ", Prepaid Card Balance Not Available, Available Balance in Tag Prepaid Card is :" + PrepBal, GlobalVariable.gCompanyName);
                                                return false;
                                            }
                                        }
                                        else 
                                        {
                                            MessageBox.Show("Valid Prepaid Card Not Tag With KOT,Kindly Tag Valid Card then use Prepaid Option ", GlobalVariable.gCompanyName);
                                            return false;
                                        }
                                    }
                                }
                                else 
                                {
                                    MessageBox.Show("Prepaid Card Not Tag With KOT,Kindly Tag first then use Prepaid Option ", GlobalVariable.gCompanyName);
                                    return false;
                                }
                            }
                            if (DescVal == "ROOM") { RoomAccount = true; }
                            if (GlobalVariable.gCompName == "EPC" && GlobalVariable.AccountPostFlag == "YES") 
                            {
                                DataTable AccCheck = new DataTable();
                                sql = "SELECT * FROM PAYMENTMODEMASTER WHERE ISNULL(ACCOUNTIN,'') = '' AND PAYMENTCODE = '" + DescVal + "' ";
                                AccCheck = GCon.getDataSet(sql);
                                if (AccCheck.Rows.Count > 0) 
                                {
                                    MessageBox.Show(DescVal + "PayMent Account Tag Not Done", GlobalVariable.gCompanyName);
                                    return false; 
                                }
                            }
                            if (DescVal == "CBILL")
                            {
                                DataTable Member = new DataTable();
                                Member = GCon.getDataSet("select MCode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'");
                                if (Member.Rows.Count > 0)
                                {
                                    OMemCode = Member.Rows[0].ItemArray[0].ToString();
                                }
                                bool CStat = CreditCheck(OMemCode);
                                if (CStat == false)
                                {
                                    MessageBox.Show("Member Crossed Credit Limit, Plz check account", GlobalVariable.gCompanyName);
                                    return false;
                                }
                                else 
                                {
                                    if (CrLimit < PayAmt) 
                                    {
                                        MessageBox.Show("Credit Limit Balance Not Available", GlobalVariable.gCompanyName);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (PayCount > 1 && RoomAccount == true) 
            {
                MessageBox.Show("Multi Settlement Not Avail if Room is applicable,", GlobalVariable.gCompanyName);
                return false;
            }
            if (PayCount > 1)
            {
                if (GlobalVariable.gCompName == "KGA" || GlobalVariable.MultiPayMode == "NO")
                {
                    MessageBox.Show("Multi Settlement Not Avail,", GlobalVariable.gCompanyName);
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            else 
            { 
                return true;
            }

        }

        public bool CreditCheck(string MemCode)
        {
            CrLimit = 0;
            string CreditYN = "";
            DataTable MemCat = new DataTable();
            DataTable MemCheck = new DataTable();
            DataTable MemOut = new DataTable();
            sql = "SELECT ISNULL(Creditlimit,0) AS Creditlimit,ISNULL(creditlimityn,'N') AS creditlimityn FROM SUBCATEGORYMASTER WHERE SUBTYPECODE IN (SELECT MEMBERTYPECODE FROM MEMBERMASTER WHERE MCODE = '" + MemCode + "')";
            MemCat = GCon.getDataSet(sql);
            if (MemCat.Rows.Count > 0)
            {
                CrLimit = Convert.ToDouble(MemCat.Rows[0].ItemArray[0]);
                CreditYN = Convert.ToString(MemCat.Rows[0].ItemArray[1]);
            }
            else
            {
                CrLimit = 0;
                CreditYN = "N";
            }
            sql = "SELECT ISNULL(MEMLIMIT,0) AS MEMLIMIT FROM MEMBERMASTER WHERE MCODE = '" + MemCode + "'";
            MemCheck = GCon.getDataSet(sql);
            if (MemCheck.Rows.Count > 0)
            {
                if (Convert.ToDouble(MemCheck.Rows[0].ItemArray[0]) > 0)
                {
                    CrLimit = Convert.ToDouble(MemCheck.Rows[0].ItemArray[0]);
                    CreditYN = "Y";
                }
            }
            if (CreditYN == "Y")
            {
                sql = "SELECT ISNULL((SUM(DEB)-SUM(CRE)),0) AS OUTABL from Get_CreditBal where SLCODE = '" + (MemCode) + "'";
                MemOut = GCon.getDataSet(sql);
                if (MemOut.Rows.Count > 0)
                {
                    CrLimit = CrLimit - Convert.ToDouble(MemOut.Rows[0].ItemArray[0]);
                }
                if (CrLimit < 0)
                {
                    return false;
                }
                else { return true; }
            }
            else
            { return true; }
        }

        public void EInvoicing(string Bno, string FYear)
        {
            string Reqid = "";
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable EData = new DataTable();
            DataTable GspDetails = new DataTable();
            DataTable CheckExit = new DataTable();
            VBMath.Randomize();
            Reqid = Strings.Mid("R" + (VBMath.Rnd() * 800000), 1, 5);

            sql = "Select  ISNULL((select case when (select Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')<>'' then (Select  isnull(gstinno,'') from membermaster where mcode=(select top 1  Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) ";
            sql = sql + " else (select  isnull(GuestGSTIN,'')  from Tbl_HomeTakeAwayBill where KotNo=(select top 1  KOTDETAILS from KOT_DET where BILLDETAILS='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) end ),'') as gstinno ";
            EData = GCon.getDataSet(sql);
            if (EData.Rows.Count > 0)
            {
                if (Convert.ToString(EData.Rows[0].ItemArray[0]) == "")
                {
                    MessageBox.Show("GSTIN NO not Provide for this Bill");
                    return;
                }
                sql = "select * from GSPIRNUPDATE where billdetails = '" + Bno + "' and isnull(Void,'') <> 'Y'";
                CheckExit = GCon.getDataSet(sql);
                if (CheckExit.Rows.Count > 0)
                {
                    MessageBox.Show("IRN Already Created,First Delete then try again for new Registration");
                    return;
                }
                sql = "  Select  gspappid,gspappsecret,user_name,password,gstin from gspdetails ";
                GspDetails = GCon.getDataSet(sql);
                if (GspDetails.Rows.Count > 0)
                {
                    string gspappid = "", gspappsecret = "", user_name = "", password = "", gstin = "";
                    gspappid = GspDetails.Rows[0].ItemArray[0].ToString();
                    gspappsecret = GspDetails.Rows[0].ItemArray[1].ToString();
                    user_name = GspDetails.Rows[0].ItemArray[2].ToString();
                    password = GspDetails.Rows[0].ItemArray[3].ToString();
                    gstin = GspDetails.Rows[0].ItemArray[4].ToString();
                    object xmlobj;
                    string strPostData, params1 = "";
                    string USERID, SID, PWD, SMCODE;
                    Int32 i;
                    string url;
                    WinHttpRequest HttpReq = new WinHttpRequest();
                    try
                    {
                        string token = "";
                        SMCODE = Bno;
                        url = "https://gsp.adaequare.com/gsp/authenticate?action=GSP&grant_type=token";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("gspappid", gspappid);
                        HttpReq.SetRequestHeader("gspappsecret", gspappsecret);
                        HttpReq.Send();
                        string result2;
                        result2 = HttpReq.ResponseText;
                        if (result2.Length > 1)
                        {
                            JObject ser = JObject.Parse(result2);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";

                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name.ToUpper())
                                {
                                    case "ACCESS_TOKEN":
                                        token = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                }
                            }
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        string insert;
                        DataTable dt = new DataTable();
                        sqlstring = "exec get_einvoicePOS '" + SMCODE + "'";
                        dt = GCon.getDataSet(sqlstring);
                        if (dt.Rows.Count > 0)
                        {
                            params1 = dt.Rows[0].ItemArray[0].ToString();
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("Content-Type", "application/json");
                        HttpReq.SetRequestHeader("user_name", user_name);
                        HttpReq.SetRequestHeader("password", password);
                        HttpReq.SetRequestHeader("gstin", gstin);
                        HttpReq.SetRequestHeader("requestid", Reqid + SMCODE);
                        HttpReq.SetRequestHeader("Authorization", "Bearer " + token);
                        HttpReq.Send(params1);
                        string result;
                        result = HttpReq.ResponseText;

                        SqlConnection CC = new SqlConnection();
                        CC.ConnectionString = GCon.GetConnection();
                        CC.Open();
                        SqlCommand osqlcommand = new SqlCommand();
                        osqlcommand = new SqlCommand("proc_GSPIRNUPDATE", CC);
                        osqlcommand.CommandType = CommandType.StoredProcedure;
                        Boolean desbool = false;
                        if (result.Length > 1)
                        {
                            JObject ser = JObject.Parse(result);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";
                            desbool = false;
                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name)
                                {
                                    case "success":
                                        desbool = true;
                                        //osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString();
                                        break;
                                    case "message":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "result":
                                        string abc = item.Value.ToString();
                                        JObject ser1 = JObject.Parse(abc);
                                        List<JToken> data1 = ser1.Children().ToList();
                                        string output1 = "";
                                        foreach (JProperty item1 in data1)
                                        {
                                            switch (item1.Name)
                                            {
                                                case "AckNo":
                                                    desbool = true;
                                                    //osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item1.Value.ToString();
                                                    break;
                                                case "AckDt":
                                                    DateTime ackdt;
                                                    ackdt = Convert.ToDateTime(item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2));
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = ackdt;
                                                    break;
                                                case "Irn":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedInvoice":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedQRCode":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "Status":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        if (desbool == false)
                        {
                            string result1 = "";
                            //result1 = result.Replace(vbCr, "").Replace(vbLf, "");
                            result1 = result1.Replace(",", "");
                            result1 = result1.Replace("'", "");
                            //result1 = result1.Replace("""", "");
                            osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = result1;
                        }
                        //osqlcommand.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "mobile_membermasteruinserted";
                        osqlcommand.Parameters.Add("@adduser", SqlDbType.VarChar, 50).Value = GlobalVariable.gUserName.ToString();
                        //osqlcommand.Parameters.Add("@col", SqlDbType.VarChar, 50).Value = "MembershipNo";
                        osqlcommand.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = SMCODE;
                        osqlcommand.ExecuteNonQuery();
                        if (desbool == true)
                        {
                            sqlstring = "UPDATE GSPIRNUPDATE SET ReqId = '" + Reqid + "' where billdetails = '" + SMCODE + "' and isnull(Void,'') <> 'Y'";
                            List.Add(sqlstring);
                            if (GCon.Moretransaction(List) > 0)
                            { List.Clear(); }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
        public void EInvoicing_old(string Bno) 
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable EData = new DataTable();
            DataTable GspDetails = new DataTable();

            sql = "Select  (select case when (select Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')<>'' then (Select  isnull(gstinno,'') from membermaster where mcode=(select top 1  Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')) ";
            sql = sql + " else (select  isnull(GuestGSTIN,'')  from Tbl_HomeTakeAwayBill where KotNo=(select top 1  KOTDETAILS from KOT_DET where BILLDETAILS='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')) end )as gstinno ";
            EData = GCon.getDataSet(sql);
            if (EData.Rows.Count > 0)
            {
                sql = "  Select  gspappid,gspappsecret,user_name,password,gstin from gspdetails ";
                GspDetails = GCon.getDataSet(sql);
                if (GspDetails.Rows.Count > 0) 
                {
                    string gspappid = "", gspappsecret= "", user_name = "", password= "", gstin= "";
                    gspappid = GspDetails.Rows[0].ItemArray[0].ToString();
                    gspappsecret = GspDetails.Rows[0].ItemArray[1].ToString();
                    user_name = GspDetails.Rows[0].ItemArray[2].ToString();
                    password = GspDetails.Rows[0].ItemArray[3].ToString();
                    gstin = GspDetails.Rows[0].ItemArray[4].ToString();
                    object xmlobj;
                    string strPostData, params1 = "";
                    string USERID, SID, PWD, SMCODE;
                    Int32 i;
                    string url;
                    WinHttpRequest HttpReq = new WinHttpRequest();
                    try 
                    {
                        string token = "";
                        SMCODE = Bno;
                        url = "https://gsp.adaequare.com/gsp/authenticate?action=GSP&grant_type=token";
                        HttpReq.Open("POST",url,false);
                        HttpReq.SetRequestHeader("gspappid", gspappid);
                        HttpReq.SetRequestHeader("gspappsecret", gspappsecret);
                        HttpReq.Send();
                        string result2;
                        result2 = HttpReq.ResponseText;
                        if (result2.Length > 1)
                        {
                            JObject ser = JObject.Parse(result2);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";

                           foreach (JProperty item in data) 
                           {
                               item.CreateReader();
                               switch (item.Name.ToUpper())
                               {
                                   case "ACCESS_TOKEN":
                                      token = item.Value.ToString().Substring(1,item.Value.ToString().Length -2);
                                      break;
                               }
                           }
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        string insert;
                        DataTable dt = new DataTable();
                        sqlstring = "exec get_einvoicePOS '" + SMCODE + "'";
                        dt = GCon.getDataSet(sqlstring);
                        if (dt.Rows.Count > 0) 
                        {
                            params1 = dt.Rows[0].ItemArray[0].ToString();
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("Content-Type", "application/json");
                        HttpReq.SetRequestHeader("user_name", user_name);
                        HttpReq.SetRequestHeader("password", password);
                        HttpReq.SetRequestHeader("gstin", gstin);
                        HttpReq.SetRequestHeader("requestid", "p258" + SMCODE);
                        HttpReq.SetRequestHeader("Authorization", "Bearer " + token);
                        HttpReq.Send(params1);
                        string result;
                        result = HttpReq.ResponseText;
                        Boolean tt=false;                   
                        SqlConnection CC = new SqlConnection();
                        CC.ConnectionString = GCon.GetConnection();
                        CC.Open();
                        SqlCommand osqlcommand = new SqlCommand();
                        osqlcommand = new SqlCommand("proc_GSPIRNUPDATE", CC);
                        osqlcommand.CommandType = CommandType.StoredProcedure;
                        Boolean desbool = false;
                        if (result.Length > 1) 
                        {
                            JObject ser = JObject.Parse(result);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";
                            desbool = false;
                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name)
                                {
                                    case "success":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        if (item.Value.ToString().Substring(1, item.Value.ToString().Length - 2) == "false")
                                        {
                                            tt = true;
                                        }
                                        break;
                                    case "message":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        
                                        break;
                                    case "result":

                                        JObject ser1 = JObject.Parse(result);
                                        List<JToken> data1 = ser1.Children().ToList();
                                        string output1 = "";
                                        foreach (JProperty item1 in data1)
                                        {
                                            switch (item1.Name) 
                                            {
                                                case "AckNo":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "AckDt":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "Irn":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedInvoice":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedQRCode":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "Status":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "result":
                                       
                                                    JObject ser2 = JObject.Parse(result);
                                                    List<JToken> data2 = ser2.Children().ToList();
                                                    string output2 = "";
                                                    foreach (JProperty item2 in data2)
                                                    {
                                                        switch (item2.Name) 
                                                        {
                                                            case "AckNo":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                            case "AckDt":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                            case "Irn":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                            case "SignedInvoice":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                            case "SignedQRCode":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                            case "Status":
                                                                desbool = true;
                                                                osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item2.Value.ToString().Substring(1, item2.Value.ToString().Length - 2);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                        }
                        if (desbool == false) 
                        {
                            string result1 = "";
                            //result1 = result.Replace(vbCr, "").Replace(vbLf, "");
                            result1 = result1.Replace(",", "");
                            result1 = result1.Replace("'", "");
                            //result1 = result1.Replace("""", "");
                            osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = result1;
                        }
                        if (tt == false)
                        { 
                             
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = "nil";

                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = "nil";

                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = "nil";

                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = "nil";

                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = "nil";

                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = "nil";
                                                    MessageBox.Show("E-invoice is not Succeeded");
                                                  
                        }
                        //osqlcommand.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "mobile_membermasteruinserted";
                        osqlcommand.Parameters.Add("@adduser", SqlDbType.VarChar, 50).Value = GlobalVariable.gUserName.ToString();
                        //osqlcommand.Parameters.Add("@col", SqlDbType.VarChar, 50).Value = "MembershipNo";
                        osqlcommand.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = "p258" + SMCODE;
                        osqlcommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        private void PrintOperation_Size33(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0,BillTotal=0,TaxTotal=0,OthTotal=0,MFTotal=0,DiscAmount =0;
            Double DisPercent = 0;
            Double ExtraTips = 0, RefundAmt = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "NC".Length) / 2) + "NC"); }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + "LOC :" +RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine();
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}","","QTY","","ITEM","AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 16;
                    }
                    Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", Strings.Format(RData["QTY"], "0"), "",Strings.Mid(RData["ITEMDESC"].ToString(),1,18), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100) ;
                    if (DisPercent > 0)
                    {
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0) 
                {
                    //Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
                    //Total = Total - ((Total * DisPercent) / 100);
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                    //MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
                }
                if (OthTotal > 0) 
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal , "0.00"));
                    //OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0) 
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++) 
                    {
                        var RData = TData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["taxdesc"]+":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

                sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
                SData = GCon.getDataSet(sql);
                if (SData.Rows.Count > 0)
                {
                    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                    {
                        var RData = SData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["PAYMENTMODE"]+":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                    }
                }

                ExtraTips = Convert.ToDouble(GCon.getValue(" Select Isnull(ExtraTips,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                RefundAmt = Convert.ToDouble(GCon.getValue(" Select Isnull(RefundAmt,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (ExtraTips > 0) 
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "TIPS:", Strings.Format(ExtraTips, "0.00"));
                }
                if (RefundAmt > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
                }

                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                DataTable CData = new DataTable();
                DataTable MData = new DataTable();
                DataTable ARMData = new DataTable();
                sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "') ";
                MData = GCon.getDataSet(sql);
                if (MData.Rows.Count > 0)
                {
                    var RData1 = MData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "MCODE: " + RData1["MCODE"]);
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "MNAME: " + RData1["MNAME"]);
                }
                else 
                {
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                    ARMData = GCon.getDataSet(sql);
                    if (ARMData.Rows.Count > 0)
                    {
                        var RData1 = ARMData.Rows[0];
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "AR Code: " + RData1["ARCode"]);
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "AR Name: " + RData1["ARName"]);
                    }
                    else
                    {
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        }
                    }
                }
                Filewrite.WriteLine();
                DataTable Remark = new DataTable();
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                Remark = GCon.getDataSet(sql);
                if (Remark.Rows.Count > 0)
                {
                    var ReData = Remark.Rows[0];
                    if (ReData["REMARKS"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "Remarks : " + ReData["REMARKS"]);
                    }
                    if (ReData["NCRemarks"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "NC Remarks : " + ReData["NCRemarks"]);
                    }
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
               
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void PrintOperation(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
            Double DisPercent = 0;
            Double ExtraTips = 0, RefundAmt = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;
            string ItemHSN = "";

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        //Filewrite.WriteLine();
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "HSN", "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "QTY", "", "ITEM", "RATE", "AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Vrowcount = 16;
                    }
                    if (ItemHSN != RData["HSNNO"].ToString())
                    {
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 5), "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-1}{3,-17}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                        Vrowcount = Vrowcount + 1;
                        ItemHSN = RData["HSNNO"].ToString();
                    }
                    Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    if (DisPercent > 0)
                    {
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
                    //Total = Total - ((Total * DisPercent) / 100);
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                    //MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
                }
                if (OthTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                    //OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0)
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++)
                    {
                        var RData = TData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

                sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
                SData = GCon.getDataSet(sql);
                if (SData.Rows.Count > 0)
                {
                    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                    {
                        var RData = SData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                    }
                }

                ExtraTips = Convert.ToDouble(GCon.getValue(" Select Isnull(ExtraTips,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                RefundAmt = Convert.ToDouble(GCon.getValue(" Select Isnull(RefundAmt,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (ExtraTips > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TIPS:", Strings.Format(ExtraTips, "0.00"));
                }
                if (RefundAmt > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
                }

                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                DataTable CData = new DataTable();
                DataTable MData = new DataTable();
                DataTable ARMData = new DataTable();
                DataTable RoomData = new DataTable();
                sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                MData = GCon.getDataSet(sql);
                if (MData.Rows.Count > 0)
                {
                    var RData1 = MData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "MCODE: " + RData1["MCODE"]);
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "MNAME: " + RData1["MNAME"]);
                }
                else
                {
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                    ARMData = GCon.getDataSet(sql);
                    if (ARMData.Rows.Count > 0)
                    {
                        var RData1 = ARMData.Rows[0];
                        string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Code: " + RData1["ARCode"]);
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Name: " + RData1["ARName"]);
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "GSTIN  : " + ArGSt);
                    }
                    else
                    {
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        }
                    }
                }
                sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
                RoomData = GCon.getDataSet(sql);
                if (RoomData.Rows.Count > 0)
                {
                    var RData1 = RoomData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Info");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Name: " + RData1["Mname"]);
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
                }
                Filewrite.WriteLine();
                DataTable Remark = new DataTable();
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                Remark = GCon.getDataSet(sql);
                if (Remark.Rows.Count > 0)
                {
                    var ReData = Remark.Rows[0];
                    if (ReData["REMARKS"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "Remarks : " + ReData["REMARKS"]);
                    }
                    if (ReData["NCRemarks"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "NC Remarks : " + ReData["NCRemarks"]);
                    }
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    if (GlobalVariable.gCompName == "CFC") 
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintOperation_CFC(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
            Double DisPercent = 0;
            Double ExtraTips = 0, RefundAmt = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;
            string ItemHSN = "";

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(0) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(0) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(0) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(0) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        //Filewrite.WriteLine();
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "HSN", "", "", "", "");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "QTY", "", "ITEM", "RATE", "AMOUNT");
                        Filewrite.WriteLine("{0,5}{1,-2}{2,-18}{3,8}{4,8}", "HSN", "", "", "", "");
                        Filewrite.WriteLine("{0,5}{1,-2}{2,-18}{3,8}{4,8}", "QTY", "", "ITEM", "RATE", "AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Vrowcount = 16;
                    }
                    if (ItemHSN != RData["HSNNO"].ToString())
                    {
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 5), "", "", "", "");
                        //Filewrite.WriteLine("{0,-4}{1,7}{2,-1}{3,-17}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                        Filewrite.WriteLine("{0,7}{1,-1}{2,-17}{3,8}{4,8}",  Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                        Vrowcount = Vrowcount + 1;
                        ItemHSN = RData["HSNNO"].ToString();
                    }
                    //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Filewrite.WriteLine("{0,5}{1,-2}{2,-18}{3,8}{4,8}", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    if (DisPercent > 0)
                    {
                        //Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Filewrite.WriteLine("{0,7}{1,-26}",  "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                Filewrite.WriteLine("{0,33}{1,8}", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
                    //Total = Total - ((Total * DisPercent) / 100);
                    //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Filewrite.WriteLine("{0,33}{1,8}","DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                    Filewrite.WriteLine("{0,33}{1,8}",  "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                    //MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
                }
                if (OthTotal > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                    Filewrite.WriteLine("{0,33}{1,8}", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                    //OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0)
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++)
                    {
                        var RData = TData.Rows[i];
                        //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        Filewrite.WriteLine("{0,33}{1,8}", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                //Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                //BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));
                Filewrite.WriteLine("{0,33}{1,8}",  "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,33}{1,8}", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,33}{1,8}","BILL AMT:", Strings.Format(BillTotal, "0.00"));

                sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
                SData = GCon.getDataSet(sql);
                if (SData.Rows.Count > 0)
                {
                    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                    {
                        var RData = SData.Rows[i];
                        //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                        Filewrite.WriteLine("{0,33}{1,8}",  RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                    }
                }

                ExtraTips = Convert.ToDouble(GCon.getValue(" Select Isnull(ExtraTips,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                RefundAmt = Convert.ToDouble(GCon.getValue(" Select Isnull(RefundAmt,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (ExtraTips > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TIPS:", Strings.Format(ExtraTips, "0.00"));
                    Filewrite.WriteLine("{0,33}{1,8}", "TIPS:", Strings.Format(ExtraTips, "0.00"));
                }
                if (RefundAmt > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
                    Filewrite.WriteLine("{0,33}{1,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
                }

                Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                DataTable CData = new DataTable();
                DataTable MData = new DataTable();
                DataTable ARMData = new DataTable();
                DataTable RoomData = new DataTable();
                DataTable MemD = new DataTable();
                DataTable OutData = new DataTable();
                Double ClsAmount = 0;
                sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                MData = GCon.getDataSet(sql);
                if (MData.Rows.Count > 0)
                {
                    var RData1 = MData.Rows[0];
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "MCODE: " + RData1["MCODE"]);
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "MNAME: " + RData1["MNAME"]);
                    Filewrite.WriteLine("{0,-41}",  "Customer Info");
                    Filewrite.WriteLine("{0,-41}", "-------------");
                    Filewrite.WriteLine("{0,-41}",  "MCODE: " + RData1["MCODE"]);
                    Filewrite.WriteLine("{0,-41}",  "MNAME: " + RData1["MNAME"]);
                    sql = "SELECT  ISNULL(MCode,'') AS Mcode FROM MEMBERMASTER Where MCODE ='" + RData1["MCODE"] + "'";
                    MemD = GCon.getDataSet(sql);
                    if (MemD.Rows.Count > 0) 
                    {
                        sql = "SELECT SLCODE,ISNULL(SUM(DEB),0)-ISNULL(SUM(CRE),0) AS CLS FROM Get_CreditBal WHERE SLCODE = '" + RData1["MCODE"] + "' GROUP BY SLCODE ORDER BY SLCODE";
                        OutData = GCon.getDataSet(sql);
                        if (OutData.Rows.Count > 0) 
                        {
                            ClsAmount = Convert.ToDouble(OutData.Rows[0].ItemArray[1]);
                        }
                        if (ClsAmount >= 0)
                        {
                            Filewrite.WriteLine("{0,-41}", "Your Balance: " + ClsAmount + "/- Dr.");
                        }
                        else 
                        {
                            Filewrite.WriteLine("{0,-41}", "Your Balance: " + -(ClsAmount) + "/- Cr.");
                        }
                    }
                }
                else
                {
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                    ARMData = GCon.getDataSet(sql);
                    if (ARMData.Rows.Count > 0)
                    {
                        var RData1 = ARMData.Rows[0];
                        string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Code: " + RData1["ARCode"]);
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Name: " + RData1["ARName"]);
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "GSTIN  : " + ArGSt);
                        Filewrite.WriteLine("{0,-41}",  "Customer Info");
                        Filewrite.WriteLine("{0,-41}",  "-------------");
                        Filewrite.WriteLine("{0,-41}",  "AR Code: " + RData1["ARCode"]);
                        Filewrite.WriteLine("{0,-41}",  "AR Name: " + RData1["ARName"]);
                        Filewrite.WriteLine("{0,-41}",  "GSTIN  : " + ArGSt);
                    }
                    else
                    {
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                            //Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                            //Filewrite.WriteLine("{0,-4}{1,-41}", "", RData1["GuestName"]);
                            //Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            //Filewrite.WriteLine("{0,-4}{1,-41}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine("{0,-41}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-41}", "", "-------------");
                            Filewrite.WriteLine("{0,-41}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            Filewrite.WriteLine("{0,-41}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        }
                    }
                }
                sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
                RoomData = GCon.getDataSet(sql);
                if (RoomData.Rows.Count > 0)
                {
                    var RData1 = RoomData.Rows[0];
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Info");
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Name: " + RData1["Mname"]);
                    //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
                    Filewrite.WriteLine("{0,-41}", "", "Guest Info");
                    Filewrite.WriteLine("{0,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-41}", "", "Guest Name: " + RData1["Mname"]);
                    Filewrite.WriteLine("{0,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
                }
                Filewrite.WriteLine();
                DataTable Remark = new DataTable();
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                Remark = GCon.getDataSet(sql);
                if (Remark.Rows.Count > 0)
                {
                    var ReData = Remark.Rows[0];
                    if (ReData["REMARKS"] != "")
                    {
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Remarks : " + ReData["REMARKS"]);
                        Filewrite.WriteLine("{0,-41}", "Remarks : " + ReData["REMARKS"]);
                    }
                    if (ReData["NCRemarks"] != "")
                    {
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "NC Remarks : " + ReData["NCRemarks"]);
                        Filewrite.WriteLine("{0,-41}","NC Remarks : " + ReData["NCRemarks"]);
                    }
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(0) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    if (GlobalVariable.gCompName == "CFC")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintOperation_CFC_Lat(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
            Double DisPercent = 0;
            Double ExtraTips = 0, RefundAmt = 0;
            DataTable CData = new DataTable();
            DataTable MData = new DataTable();
            DataTable ARMData = new DataTable();
            DataTable RoomData = new DataTable();
            DataTable MemD = new DataTable();
            DataTable OutData = new DataTable();
            String Mcode = "", Mname = "",Adduser ="";

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string gEmail = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(EMAIL,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string gWebsite = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(WEBSITE,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;
            string ItemHSN = "";

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Adduser = RData["Adduserid"].ToString();
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("Email:" + gEmail).ToString().Length) / 2) + "Email:" + gEmail);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("Web:" + gWebsite).ToString().Length) / 2) + "Web:" + gWebsite);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(0) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(0) + "BILL NO   :" + RData["BillDetails"]);
                        Filewrite.WriteLine(Strings.Space(0) + "BILL DATE :" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(0) + "LOC       :" + RData["LOCNAME"] + "/" + RData["TABLENO"]);
                        Filewrite.WriteLine(Strings.Space(0) + "STEWARD   :" + RData["STWNAME"]);
                        sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                        MData = GCon.getDataSet(sql);
                        if (MData.Rows.Count > 0) 
                        {
                            var RData1 = MData.Rows[0];
                            Mcode = RData1["MCODE"].ToString();
                            Mname = RData1["MNAME"].ToString();
                            Filewrite.WriteLine(Strings.Space(0) + "MEM CODE  :" + Mcode);
                            Filewrite.WriteLine(Strings.Space(0) + "MEM NAME  :" + Mname);
                        }
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        //Filewrite.WriteLine("{0,5}{1,-2}{2,-18}{3,8}{4,8}", "HSN", "", "", "", "");
                        //Filewrite.WriteLine("{0,5}{1,-2}{2,-18}{3,8}{4,8}", "QTY", "", "ITEM", "RATE", "AMOUNT");
                        Filewrite.WriteLine("{0,5}{1,-15}{2,8}{3,5}{4,8}","HSN","ITEM","RATE","QTY","AMOUNT");
                        Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                        Vrowcount = 16;
                    }
                    //if (ItemHSN != RData["HSNNO"].ToString())
                    //{
                    //    ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 5), "", "", "", "");
                    //    //Filewrite.WriteLine("{0,-4}{1,7}{2,-1}{3,-17}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                    //    Filewrite.WriteLine("{0,7}{1,-1}{2,-17}{3,8}{4,8}", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                    //    Vrowcount = Vrowcount + 1;
                    //    ItemHSN = RData["HSNNO"].ToString();
                    //}
                    Filewrite.WriteLine("{0,5}{1,-15}{2,8}{3,5}{4,8}", Strings.Mid(RData["HSNNO"].ToString(), 1, 4), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 15), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["QTY"], "0"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    if (DisPercent > 0)
                    {
                        Filewrite.WriteLine("{0,7}{1,-26}", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                //Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                Filewrite.WriteLine("{0,33}{1,8}", "TOTAL    :", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0)
                {
                    Filewrite.WriteLine("{0,33}{1,8}", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,33}{1,8}", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                }
                if (OthTotal > 0)
                {
                    Filewrite.WriteLine("{0,33}{1,8}", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0)
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++)
                    {
                        var RData = TData.Rows[i];
                        Filewrite.WriteLine("{0,33}{1,8}", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                //Filewrite.WriteLine("{0,33}{1,8}", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,33}{1,8}", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,33}{1,8}", "BILL AMT:", Strings.Format(BillTotal, "0.00"));
                Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                String KotNumber1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 KOTNO FROM KOT_DET WHERE BILLDETAILS = '" + Bno + "'  AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite.WriteLine("{0,-41}", "KOT's: " + KotNumber1);
                Filewrite.WriteLine("");
                String number = GlobalClass.ConvertAmount(Math.Round(Total + TaxTotal + OthTotal + MFTotal));
                Filewrite.WriteLine(number);
                String Paymode1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 PAYMENTMODE FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                Filewrite.WriteLine("{0,-41}", "Payment Type: " + Paymode1);

                if (Paymode1 == "CBILL")
                {
                        Double ClsAmount = 0;
                        sql = "SELECT SLCODE,ISNULL(SUM(DEB),0)-ISNULL(SUM(CRE),0) AS CLS FROM Get_CreditBal WHERE SLCODE = '" + Mcode + "' GROUP BY SLCODE ORDER BY SLCODE";
                        OutData = GCon.getDataSet(sql);
                        if (OutData.Rows.Count > 0)
                        {
                            ClsAmount = Convert.ToDouble(OutData.Rows[0].ItemArray[1]);
                        }
                        if (ClsAmount >= 0)
                        {
                            Filewrite.WriteLine("{0,-41}", "Your Balance: " + ClsAmount + "/- Dr.");
                        }
                        else
                        {
                            Filewrite.WriteLine("{0,-41}", "Your Balance: " + -(ClsAmount) + "/- Cr.");
                        }
                }
                if (Paymode1 =="SCARD")
                {
                    Double ClsAmount = 0,OpAmount = 0 ,TrnAmount =0;

                    sql = "select Top 1 ISNULL(BALANCE,0) AS BALANCE from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')";
                    if (OutData.Rows.Count > 0)
                    {
                        ClsAmount = Convert.ToDouble(OutData.Rows[0].ItemArray[0]);
                    }
                    TrnAmount = Math.Round(Total + TaxTotal + OthTotal + MFTotal); 
                    OpAmount = ClsAmount + TrnAmount;
                    Filewrite.WriteLine("{0,-15}{1,-15}{2,11}", "CARD OP BAL","TRN AMOUNT","CLBAL");
                     Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                    Filewrite.WriteLine("{0,-15}{1,-15}{2,11}", Strings.Format(OpAmount, "0.00"),Strings.Format(TrnAmount, "0.00"),Strings.Format(ClsAmount, "0.00"));
                     Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(41, "-"));
                }

                Filewrite.WriteLine();
                Filewrite.WriteLine("{0,-41}", "Prepared By: " + Adduser);
                Filewrite.WriteLine();
                Filewrite.WriteLine();
                Filewrite.WriteLine();
                Filewrite.WriteLine("{0,-41}", "Member's Signature");
              
                DataTable Remark = new DataTable();
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                Remark = GCon.getDataSet(sql);
                if (Remark.Rows.Count > 0)
                {
                    var ReData = Remark.Rows[0];
                    if (ReData["REMARKS"] != "")
                    {
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "Remarks : " + ReData["REMARKS"]);
                        Filewrite.WriteLine("{0,-41}", "Remarks : " + ReData["REMARKS"]);
                    }
                    if (ReData["NCRemarks"] != "")
                    {
                        //Filewrite.WriteLine("{0,-4}{1,-41}", "", "NC Remarks : " + ReData["NCRemarks"]);
                        Filewrite.WriteLine("{0,-41}", "NC Remarks : " + ReData["NCRemarks"]);
                    }
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(0) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    if (GlobalVariable.gCompName == "CFC")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintOperationNZC(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
            Double DisPercent = 0;
            Double ExtraTips = 0, RefundAmt = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;
            string ItemHSN = "";

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        //Filewrite.WriteLine();
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "HSN", "", "", "", "");
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "QTY", "", "ITEM", "RATE", "AMOUNT");
                        Filewrite.WriteLine("{0,-4}{1,-18}{2,-2}{3,5}{4,8}{5,8}", "", "HSN", "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,-18}{2,-2}{3,5}{4,8}{5,8}", "", "ITEM", "", "QTY", "RATE", "AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Vrowcount = 16;
                    }
                    if (ItemHSN != RData["HSNNO"].ToString())
                    {
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 5), "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-1}{3,-17}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                        Vrowcount = Vrowcount + 1;
                        ItemHSN = RData["HSNNO"].ToString();
                    }
                    ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Filewrite.WriteLine("{0,-4}{1,-18}{2,-2}{3,5}{4,8}{5,8}", "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), "", Strings.Format(RData["QTY"], "0"), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    if (DisPercent > 0)
                    {
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0)
                {
                    //Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
                    //Total = Total - ((Total * DisPercent) / 100);
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                    //MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
                }
                if (OthTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                    //OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0)
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++)
                    {
                        var RData = TData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

                sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
                SData = GCon.getDataSet(sql);
                if (SData.Rows.Count > 0)
                {
                    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                    {
                        var RData = SData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                    }
                }

                ExtraTips = Convert.ToDouble(GCon.getValue(" Select Isnull(ExtraTips,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                RefundAmt = Convert.ToDouble(GCon.getValue(" Select Isnull(RefundAmt,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (ExtraTips > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TIPS:", Strings.Format(ExtraTips, "0.00"));
                }
                if (RefundAmt > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
                }

                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Total Item Count :" + CountItem, "");
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                DataTable CData = new DataTable();
                DataTable MData = new DataTable();
                DataTable ARMData = new DataTable();
                DataTable RoomData = new DataTable();
                sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                MData = GCon.getDataSet(sql);
                if (MData.Rows.Count > 0)
                {
                    var RData1 = MData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "MCODE: " + RData1["MCODE"]);
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "MNAME: " + RData1["MNAME"]);
                }
                else
                {
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                    ARMData = GCon.getDataSet(sql);
                    if (ARMData.Rows.Count > 0)
                    {
                        var RData1 = ARMData.Rows[0];
                        string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Code: " + RData1["ARCode"]);
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Name: " + RData1["ARName"]);
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "GSTIN  : " + ArGSt);
                    }
                    else
                    {
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        }
                    }
                }
                sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
                RoomData = GCon.getDataSet(sql);
                if (RoomData.Rows.Count > 0)
                {
                    var RData1 = RoomData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Info");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Name: " + RData1["Mname"]);
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
                }
                Filewrite.WriteLine();
                DataTable Remark = new DataTable();
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                Remark = GCon.getDataSet(sql);
                if (Remark.Rows.Count > 0)
                {
                    var ReData = Remark.Rows[0];
                    if (ReData["REMARKS"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "Remarks : " + ReData["REMARKS"]);
                    }
                    if (ReData["NCRemarks"] != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-41}", "", "NC Remarks : " + ReData["NCRemarks"]);
                    }
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }


        private void PrintOPeration_Windows(string Bno) 
        {
            String sqlstring, sql, sql1, sql2;
            String NCRemk = "", BillRemk = "",FssaiNo = "",CatName = "";
            string CustPrint1 = "", CustPrint2 = "", CustPrint3 = "";
            Int32 PreSettle = 0;
            Double BalonCard = 0;
            String PBalRemarks = "";
            Report rv = new Report();
            CrystalDecisions.CrystalReports.Engine.ReportDocument RPS;
            if (GlobalVariable.gCompName == "SKYYE")
            {
                RPS = new CRYSTAL.BillPrint_Skyye_A4();
            }
            else if (GlobalVariable.gCompName == "TRNG") 
            {
                RPS = new CRYSTAL.BillPrint_TRNG_A4();
            }
            else
            {
                RPS = new CRYSTAL.BillPrint_General_A4();
            }
            //CRYSTAL.BillPrint_Skyye_A4 RPS = new CRYSTAL.BillPrint_Skyye_A4();

            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Web = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(WebSite,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;


            sql = "SELECT BILLDETAILS as BILLDETAILSTAX,A.taxdesc as TAXCODE,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAXAMT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
            sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' And ISNULL(T.TAXAMT,0) > 0  GROUP BY A.taxdesc,BILLDETAILS  ";
            GCon.getDataSet1(sql, "KOT_DET_TAX");
            if (GlobalVariable.gdataset.Tables["KOT_DET_TAX"].Rows.Count > 0)
            {
                rv.GetDetails(sql, "KOT_DET_TAX", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql1 = " SELECT BILLNO AS BillDetails,PAYMENTMODE,PAYAMOUNT AS TotalAmount FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
            GCon.getDataSet1(sql1, "BILL_HDR");
            if (GlobalVariable.gdataset.Tables["BILL_HDR"].Rows.Count > 0)
            {
                rv.GetDetails(sql1, "BILL_HDR", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql2 = "SELECT billdetails,CATEGORY,cast(ISNULL(ItemDiscPerc,0) as varchar(6)) + ' %' as ItemDiscPerc,-(sum((AMOUNT*ISNULL(ItemDiscPerc,0))/100)) AS ItemDiscAmt FROM KOT_DET WHERE billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "' and ISNULL(ItemDiscPerc,0) > 0 and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' group by billdetails,CATEGORY,ISNULL(ItemDiscPerc,0) ";
            GCon.getDataSet1(sql2, "KOT_DET");
            if (GlobalVariable.gdataset.Tables["KOT_DET"].Rows.Count > 0)
            {
                rv.GetDetails(sql2, "KOT_DET", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }

            sqlstring = "  select * from BillPrintCrystal where billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "' ";
            GCon.getDataSet1(sqlstring, "BillPrintCrystal");
            if (GlobalVariable.gdataset.Tables["BillPrintCrystal"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "BillPrintCrystal", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);
            }

            DataTable CData = new DataTable();
            DataTable MData = new DataTable();
            DataTable ARMData = new DataTable();
            DataTable RoomData = new DataTable();
            DataTable CardData = new DataTable();
            sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
            MData = GCon.getDataSet(sql);
            if (MData.Rows.Count > 0)
            {
                var RData1 = MData.Rows[0];
                CustPrint1 = "MCODE: " + RData1["MCODE"];
                CustPrint2 = "MNAME: " + RData1["MNAME"];
                CustPrint3 = "";
            }
            else
            {
                sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                ARMData = GCon.getDataSet(sql);
                if (ARMData.Rows.Count > 0)
                {
                    var RData1 = ARMData.Rows[0];
                    string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                    CustPrint1 = "AR Code: " + RData1["ARCode"];
                    CustPrint2 = "AR Name: " + RData1["ARName"];
                    CustPrint3 = "GSTIN  : " + ArGSt;
                }
                else
                {
                    sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                    CData = GCon.getDataSet(sql);
                    if (CData.Rows.Count > 0)
                    {
                        var RData1 = CData.Rows[0];
                        CustPrint1 = RData1["GuestName"].ToString();
                        CustPrint2 = "GSTIN: " + RData1["GuestGSTIN"];
                        CustPrint3 = "ADD: " + RData1["GuestAdd"];
                    }
                }
            }
            sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
            RoomData = GCon.getDataSet(sql);
            if (RoomData.Rows.Count > 0)
            {
                var RData1 = RoomData.Rows[0];
                CustPrint1 = "Guest Name: " + RData1["Mname"];
                CustPrint2 = "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]";
                CustPrint3 = "";
            }
            NCRemk = GCon.getValue("select Isnull(NCRemarks,'') as NCRemarks  from Bill_Hdr Where Billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "'").ToString();
            string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
            if (NCFlag == "Y") 
            {
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ10;
                TXTOBJ10 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ10.Text = "NC Bill";
                CustPrint1 = "";
                CustPrint2 = "";
                CustPrint3 = "";
            }
            BillRemk = GCon.getValue("select Isnull(remarks,'') as remarks  from Bill_Hdr Where Billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "'").ToString();
            PreSettle = Convert.ToInt32(GCon.getValue("SELECT count(*) as Settle FROM BillSettlement WHERE BILLNO = '" + Bno + "' and FinYear = '" + FinYear1 + "' And PAYMENTMODE = 'PREPAID'"));
            if (PreSettle > 0)
            {
                BalonCard = Convert.ToInt32(GCon.getValue("select Top 1 ISNULL(BALANCE,0) AS BALANCE from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')"));
                sql = "select CARDCODE,balance from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')";
                CardData = GCon.getDataSet(sql);
                if (CardData.Rows.Count > 0)
                {
                    for (int i = 0; i <= CardData.Rows.Count - 1; i++)
                    {
                        var CRData = CardData.Rows[i];
                        PBalRemarks = PBalRemarks + CRData["CARDCODE"].ToString() + " : " + CRData["balance"].ToString() + ",";
                    }
                    PBalRemarks = PBalRemarks.Remove(PBalRemarks.Length - 1);
                }
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ12;
                TXTOBJ12 = (TextObject)RPS.ReportDefinition.ReportObjects["Text35"];
                TXTOBJ12.Text = "Card Bal-> " + PBalRemarks;
            }
            if (GlobalVariable.gCompName == "TRNG")
            {
                FssaiNo = GCon.getValue("SELECT Isnull(FssaiNo,'') as FssaiNo FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE billdetails = '" + Bno + "' AND FinYear = '" + FinYear1 + "')").ToString();
                CatName = GCon.getValue("SELECT ISNULL(Cat_Name,'') AS Cat_Name FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE billdetails = '" + Bno + "' AND FinYear = '" + FinYear1 + "')").ToString();
            }
            else { FssaiNo = ""; CatName = ""; }
         
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text27"];
            TXTOBJ1.Text = CustPrint1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
            TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text28"];
            TXTOBJ2.Text = CustPrint2;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
            TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text30"];
            TXTOBJ3.Text = CustPrint3;

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
            TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text1"];
            TXTOBJ4.Text = GlobalVariable.gCompanyName;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text2"];
            TXTOBJ5.Text = Add1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ6;
            TXTOBJ6 = (TextObject)RPS.ReportDefinition.ReportObjects["Text3"];
            TXTOBJ6.Text = SecLine;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ7;
            TXTOBJ7 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
            TXTOBJ7.Text = "GSTIN:-" + GSTIN;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ8;
            TXTOBJ8 = (TextObject)RPS.ReportDefinition.ReportObjects["Text5"];
            TXTOBJ8.Text = "T : " + Phone + " " + Web;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ9;
            TXTOBJ9 = (TextObject)RPS.ReportDefinition.ReportObjects["Text33"];
            TXTOBJ9.Text = NCRemk;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ11;
            TXTOBJ11 = (TextObject)RPS.ReportDefinition.ReportObjects["Text34"];
            TXTOBJ11.Text = "Remarks : " + BillRemk;

            if (GlobalVariable.gCompName == "TRNG") 
            {
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ13;
                TXTOBJ13 = (TextObject)RPS.ReportDefinition.ReportObjects["Text36"];
                if (FssaiNo == "")
                {
                    TXTOBJ13.Text = "" ;
                }
                else 
                {
                    TXTOBJ13.Text = "FSSAI No. : " + FssaiNo;
                }

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ14;
                TXTOBJ14 = (TextObject)RPS.ReportDefinition.ReportObjects["Text37"];
                if (CatName == "")
                {
                    TXTOBJ14.Text = "";
                }
                else
                {
                    TXTOBJ14.Text =  CatName;
                }
            }
           

            //RPS.SetParameterValue("ImageUrl", QrPath);
            RPS.PrintOptions.PrinterName = GlobalVariable.PrinterName;
            RPS.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            RPS.PrintToPrinter(1, false, 0, 0);
            if (GlobalVariable.gCompName == "TRNG" || GlobalVariable.gCompName == "SKYYE")
            { RPS.PrintToPrinter(1, false, 0, 0); }
            RPS.Close();
            RPS.Dispose();
            rv.Close();
            rv.Dispose();
            GC.Collect();
            //rv.Show();
        }

        private void PrintOPeration_Windows_CheckPrint(string KNo)
        {
            String sqlstring, sql, sql1, sql2;
            String NCRemk = "", FssaiNo = "", CatName = "";
            string CustPrint1 = "", CustPrint2 = "", CustPrint3 = "";
            Report rv = new Report();
            CrystalDecisions.CrystalReports.Engine.ReportDocument RPS;
            if (GlobalVariable.gCompName == "SKYYE")
            {
                RPS = new CRYSTAL.BillPrint_Skyye_A4Prebil();
            }
            else if (GlobalVariable.gCompName == "TRNG")
            {
                RPS = new CRYSTAL.BillPrint_TRNG_A4Prebil();
            }
            else
            {
                RPS = new CRYSTAL.BillPrint_General_A4Prebil();
            }
            //CRYSTAL.BillPrint_Skyye_A4Prebil RPS = new CRYSTAL.BillPrint_Skyye_A4Prebil();

            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Web = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(WebSite,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            ////sql = "SELECT 'Pre-Bill' as BILLDETAILSTAX,A.taxdesc as TAXCODE,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAXAMT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
            ////sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.KOTDETAILS = '" + KNo + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' And ISNULL(T.TAXAMT,0) > 0  GROUP BY A.taxdesc,BILLDETAILS  ";
            sql = "SELECT 'Pre-Bill' as BILLDETAILSTAX,A.taxdesc as TAXCODE,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAXAMT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
            sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.KOTDETAILS = '" + KNo + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' And ISNULL(T.TAXAMT,0) > 0  GROUP BY A.taxdesc  ";
            GCon.getDataSet1(sql, "KOT_DET_TAX");
            if (GlobalVariable.gdataset.Tables["KOT_DET_TAX"].Rows.Count > 0)
            {
                rv.GetDetails(sql, "KOT_DET_TAX", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql1 = " SELECT BILLNO AS BillDetails,PAYMENTMODE,PAYAMOUNT AS TotalAmount FROM BILLSETTLEMENT WHERE BILLNO = '" + KNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
            GCon.getDataSet1(sql1, "BILL_HDR");
            if (GlobalVariable.gdataset.Tables["BILL_HDR"].Rows.Count > 0)
            {
                rv.GetDetails(sql1, "BILL_HDR", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql2 = "SELECT 'Pre-Bill' as billdetails,CATEGORY,cast(ISNULL(ItemDiscPerc,0) as varchar(6)) + ' %' as ItemDiscPerc,-(sum((AMOUNT*ISNULL(ItemDiscPerc,0))/100)) AS ItemDiscAmt FROM KOT_DET WHERE KOTDETAILS = '" + KNo + "' and FinYear = '" + FinYear1 + "' and ISNULL(ItemDiscPerc,0) > 0 and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' group by billdetails,CATEGORY,ISNULL(ItemDiscPerc,0) ";
            GCon.getDataSet1(sql2, "KOT_DET");
            if (GlobalVariable.gdataset.Tables["KOT_DET"].Rows.Count > 0)
            {
                rv.GetDetails(sql2, "KOT_DET", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sqlstring = "  select * from [CheckPrintCrystal] where KOTDETAILS = '" + KNo + "' and FinYear = '" + FinYear1 + "' ";
            GCon.getDataSet1(sqlstring, "BillPrintCrystal");
            if (GlobalVariable.gdataset.Tables["BillPrintCrystal"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "BillPrintCrystal", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);
            }

            DataTable CData = new DataTable();
            DataTable MData = new DataTable();
            DataTable ARMData = new DataTable();
            DataTable RoomData = new DataTable();
            sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM kot_HDR WHERE Kotdetails = '" + KNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
            MData = GCon.getDataSet(sql);
            if (MData.Rows.Count > 0)
            {
                var RData1 = MData.Rows[0];
                CustPrint1 = "MCODE: " + RData1["MCODE"];
                CustPrint2 = "MNAME: " + RData1["MNAME"];
                CustPrint3 = "";
            }
            else
            {
                sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where Kotdetails = '" + KNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                ARMData = GCon.getDataSet(sql);
                if (ARMData.Rows.Count > 0)
                {
                    var RData1 = ARMData.Rows[0];
                    string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                    CustPrint1 = "AR Code: " + RData1["ARCode"];
                    CustPrint2 = "AR Name: " + RData1["ARName"];
                    CustPrint3 = "GSTIN  : " + ArGSt;
                }
                else
                {
                    sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where Kotdetails = '" + KNo + "') ";
                    CData = GCon.getDataSet(sql);
                    if (CData.Rows.Count > 0)
                    {
                        var RData1 = CData.Rows[0];
                        CustPrint1 = RData1["GuestName"].ToString();
                        CustPrint2 = "GSTIN: " + RData1["GuestGSTIN"];
                        CustPrint3 = "ADD: " + RData1["GuestAdd"];
                    }
                }
            }
            sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + KNo + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
            RoomData = GCon.getDataSet(sql);
            if (RoomData.Rows.Count > 0)
            {
                var RData1 = RoomData.Rows[0];
                CustPrint1 = "Guest Name: " + RData1["Mname"];
                CustPrint2 = "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]";
                CustPrint3 = "";
            }
            //NCRemk = GCon.getValue("select Isnull(NCRemarks,'') as NCRemarks  from Bill_Hdr Where Billdetails = '" + KNo + "' and FinYear = '" + FinYear1 + "'").ToString();
            NCRemk = "";
            ////string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE Kotdetails = '" + KNo + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
            ////if (NCFlag == "Y")
            ////{
            ////    CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ10;
            ////    TXTOBJ10 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
            ////    TXTOBJ10.Text = "NC Bill";
            ////}

            if (GlobalVariable.gCompName == "TRNG")
            {
                FssaiNo = GCon.getValue("SELECT Isnull(FssaiNo,'') as FssaiNo FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE KOTdetails = '" + KNo + "' AND FinYear = '" + FinYear1 + "')").ToString();
                CatName = GCon.getValue("SELECT ISNULL(Cat_Name,'') AS Cat_Name FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE KOTdetails = '" + KNo + "' AND FinYear = '" + FinYear1 + "')").ToString();
            }
            else { FssaiNo = ""; CatName = ""; }

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text27"];
            TXTOBJ1.Text = CustPrint1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
            TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text28"];
            TXTOBJ2.Text = CustPrint2;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
            TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text30"];
            TXTOBJ3.Text = CustPrint3;

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
            TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text1"];
            TXTOBJ4.Text = GlobalVariable.gCompanyName;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text2"];
            TXTOBJ5.Text = Add1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ6;
            TXTOBJ6 = (TextObject)RPS.ReportDefinition.ReportObjects["Text3"];
            TXTOBJ6.Text = SecLine;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ7;
            TXTOBJ7 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
            TXTOBJ7.Text = "GSTIN:-" + GSTIN;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ8;
            TXTOBJ8 = (TextObject)RPS.ReportDefinition.ReportObjects["Text5"];
            TXTOBJ8.Text = "T : " + Phone + " " + Web;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ9;
            TXTOBJ9 = (TextObject)RPS.ReportDefinition.ReportObjects["Text33"];
            TXTOBJ9.Text = NCRemk;

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ10;
            TXTOBJ10 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
            TXTOBJ10.Text = "PROFORMA INVOICE";

            if (GlobalVariable.gCompName == "TRNG")
            {
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ13;
                TXTOBJ13 = (TextObject)RPS.ReportDefinition.ReportObjects["Text36"];
                if (FssaiNo == "")
                {
                    TXTOBJ13.Text = "";
                }
                else
                {
                    TXTOBJ13.Text = "FSSAI No. : " + FssaiNo;
                }

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ14;
                TXTOBJ14 = (TextObject)RPS.ReportDefinition.ReportObjects["Text37"];
                if (CatName == "")
                {
                    TXTOBJ14.Text = "";
                }
                else
                {
                    TXTOBJ14.Text = CatName;
                }
            }
            

            //RPS.SetParameterValue("ImageUrl", QrPath);
            RPS.PrintOptions.PrinterName = GlobalVariable.PrinterName;
            RPS.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            RPS.PrintToPrinter(1, false, 0, 0);
            RPS.Close();
            RPS.Dispose();
            rv.Close();
            rv.Dispose();
            GC.Collect();
            //rv.Show();
        }

        private void PrintOPeration_Windows_CFC(string Bno)
        {
            String sqlstring, sql, sql1, sql2;
            String NCRemk = "", BillRemk = "", FssaiNo = "", CatName = "";
            string CustPrint1 = "", CustPrint2 = "", CustPrint3 = "";
            Int32 PreSettle = 0;
            Double BalonCard = 0,ScardbillValue = 0,ScardOpening = 0;
            String PBalRemarks = "";
            String Paymodes = "", Kotdetno = "";
            Report rv = new Report();
            CrystalDecisions.CrystalReports.Engine.ReportDocument RPS;

            RPS = new CRYSTAL.BillPrint_CFC_A4();
            //CRYSTAL.BillPrint_Skyye_A4 RPS = new CRYSTAL.BillPrint_Skyye_A4();

            ////string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string Web = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(WebSite,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            ////string SecLine = Add2 + ", " + City + "-" + PinNo;

            Paymodes = Convert.ToString(GCon.getValue(" Select Top 1 PAYMENTMODE from BillSettlement where BILLNO = '" + Bno + "' and FinYear = '" + FinYear1 + "'"));
            Kotdetno = Convert.ToString(GCon.getValue(" select Top 1 KOTNO from KOT_det where BILLDETAILS = '" + Bno + "' and FinYear = '" + FinYear1 + "'"));


            sql = "SELECT BILLDETAILS as BILLDETAILSTAX,A.taxdesc as TAXCODE,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAXAMT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
            sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' And ISNULL(T.TAXAMT,0) > 0  GROUP BY A.taxdesc,BILLDETAILS  ";
            GCon.getDataSet1(sql, "KOT_DET_TAX");
            if (GlobalVariable.gdataset.Tables["KOT_DET_TAX"].Rows.Count > 0)
            {
                rv.GetDetails(sql, "KOT_DET_TAX", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql1 = " SELECT BILLNO AS BillDetails,PAYMENTMODE,PAYAMOUNT AS TotalAmount FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
            GCon.getDataSet1(sql1, "BILL_HDR");
            if (GlobalVariable.gdataset.Tables["BILL_HDR"].Rows.Count > 0)
            {
                rv.GetDetails(sql1, "BILL_HDR", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql2 = "SELECT billdetails,CATEGORY,cast(ISNULL(ItemDiscPerc,0) as varchar(6)) + ' %' as ItemDiscPerc,-(sum((AMOUNT*ISNULL(ItemDiscPerc,0))/100)) AS ItemDiscAmt FROM KOT_DET WHERE billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "' and ISNULL(ItemDiscPerc,0) > 0 and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' group by billdetails,CATEGORY,ISNULL(ItemDiscPerc,0) ";
            GCon.getDataSet1(sql2, "KOT_DET");
            if (GlobalVariable.gdataset.Tables["KOT_DET"].Rows.Count > 0)
            {
                rv.GetDetails(sql2, "KOT_DET", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }

            sqlstring = "  select * from BillPrintCrystal where billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "' ";
            GCon.getDataSet1(sqlstring, "BillPrintCrystal");
            if (GlobalVariable.gdataset.Tables["BillPrintCrystal"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "BillPrintCrystal", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);
            }

            DataTable CData = new DataTable();
            DataTable MData = new DataTable();
            DataTable ARMData = new DataTable();
            DataTable RoomData = new DataTable();
            DataTable CardData = new DataTable();
            sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
            MData = GCon.getDataSet(sql);
            if (MData.Rows.Count > 0)
            {
                var RData1 = MData.Rows[0];
                CustPrint1 = "MCODE: " + RData1["MCODE"];
                CustPrint2 = "MNAME: " + RData1["MNAME"];
                CustPrint3 = "";
            }
            else
            {
                sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                ARMData = GCon.getDataSet(sql);
                if (ARMData.Rows.Count > 0)
                {
                    var RData1 = ARMData.Rows[0];
                    string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                    CustPrint1 = "AR Code: " + RData1["ARCode"];
                    CustPrint2 = "AR Name: " + RData1["ARName"];
                    CustPrint3 = "GSTIN  : " + ArGSt;
                }
                else
                {
                    sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                    CData = GCon.getDataSet(sql);
                    if (CData.Rows.Count > 0)
                    {
                        var RData1 = CData.Rows[0];
                        CustPrint1 = RData1["GuestName"].ToString();
                        CustPrint2 = "GSTIN: " + RData1["GuestGSTIN"];
                        CustPrint3 = "ADD: " + RData1["GuestAdd"];
                    }
                }
            }
            sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
            RoomData = GCon.getDataSet(sql);
            if (RoomData.Rows.Count > 0)
            {
                var RData1 = RoomData.Rows[0];
                CustPrint1 = "Guest Name: " + RData1["Mname"];
                CustPrint2 = "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]";
                CustPrint3 = "";
            }
            NCRemk = GCon.getValue("select Isnull(NCRemarks,'') as NCRemarks  from Bill_Hdr Where Billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "'").ToString();
            string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
            if (NCFlag == "Y")
            {
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ10;
                TXTOBJ10 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ10.Text = "NC Bill";
                CustPrint1 = "";
                CustPrint2 = "";
                CustPrint3 = "";
            }
            BillRemk = GCon.getValue("select Isnull(remarks,'') as remarks  from Bill_Hdr Where Billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "'").ToString();
            PreSettle = Convert.ToInt32(GCon.getValue("SELECT count(*) as Settle FROM BillSettlement WHERE BILLNO = '" + Bno + "' and FinYear = '" + FinYear1 + "' And PAYMENTMODE = 'PREPAID'"));
            if (PreSettle > 0)
            {
                BalonCard = Convert.ToInt32(GCon.getValue("select Top 1 ISNULL(BALANCE,0) AS BALANCE from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')"));
                sql = "select CARDCODE,balance from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')";
                CardData = GCon.getDataSet(sql);
                if (CardData.Rows.Count > 0)
                {
                    for (int i = 0; i <= CardData.Rows.Count - 1; i++)
                    {
                        var CRData = CardData.Rows[i];
                        PBalRemarks = PBalRemarks + CRData["CARDCODE"].ToString() + " : " + CRData["balance"].ToString() + ",";
                    }
                    PBalRemarks = PBalRemarks.Remove(PBalRemarks.Length - 1);
                }
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ12;
                TXTOBJ12 = (TextObject)RPS.ReportDefinition.ReportObjects["Text35"];
                TXTOBJ12.Text = "Card Bal-> " + PBalRemarks;
            }
            PreSettle = Convert.ToInt32(GCon.getValue("SELECT count(*) as Settle FROM BillSettlement WHERE BILLNO = '" + Bno + "' and FinYear = '" + FinYear1 + "' And PAYMENTMODE = 'SCARD'"));
            if (PreSettle > 0) 
            {
                BalonCard = Convert.ToInt32(GCon.getValue("select Top 1 ISNULL(BALANCE,0) AS BALANCE from SM_CARDFILE_HDR where CARDCODE in (select CARDCODE from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "')"));
                ScardbillValue = Convert.ToInt32(GCon.getValue("select isnull(Bill_Amount,0) as BillAmount from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "'"));
                ScardOpening = BalonCard - ScardbillValue;
                 CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ12;
                TXTOBJ12 = (TextObject)RPS.ReportDefinition.ReportObjects["Text35"];
                TXTOBJ12.Text = "Card Bal-> " + BalonCard;
            }

            if (GlobalVariable.gCompName == "TRNG")
            {
                FssaiNo = GCon.getValue("SELECT Isnull(FssaiNo,'') as FssaiNo FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE billdetails = '" + Bno + "' AND FinYear = '" + FinYear1 + "')").ToString();
                CatName = GCon.getValue("SELECT ISNULL(Cat_Name,'') AS Cat_Name FROM PosMaster where POSCode in (SELECT TOP 1 POSCODE FROM KOT_DET WHERE billdetails = '" + Bno + "' AND FinYear = '" + FinYear1 + "')").ToString();
            }
            else { FssaiNo = ""; CatName = ""; }

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text27"];
            TXTOBJ1.Text = CustPrint1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
            TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text28"];
            TXTOBJ2.Text = CustPrint2;
            //CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
            //TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text30"];
            //TXTOBJ3.Text = CustPrint3;

            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
            ////TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text1"];
            ////TXTOBJ4.Text = GlobalVariable.gCompanyName;
            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            ////TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text2"];
            ////TXTOBJ5.Text = Add1;
            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ6;
            ////TXTOBJ6 = (TextObject)RPS.ReportDefinition.ReportObjects["Text3"];
            ////TXTOBJ6.Text = SecLine;
            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ7;
            ////TXTOBJ7 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
            ////TXTOBJ7.Text = "GSTIN:-" + GSTIN;
            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ8;
            ////TXTOBJ8 = (TextObject)RPS.ReportDefinition.ReportObjects["Text5"];
            ////TXTOBJ8.Text = "T : " + Phone + " " + Web;

            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ9;
            ////TXTOBJ9 = (TextObject)RPS.ReportDefinition.ReportObjects["Text33"];
            ////TXTOBJ9.Text = NCRemk;
            ////CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ11;
            ////TXTOBJ11 = (TextObject)RPS.ReportDefinition.ReportObjects["Text34"];
            ////TXTOBJ11.Text = "Remarks : " + BillRemk;

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ13;
            TXTOBJ13 = (TextObject)RPS.ReportDefinition.ReportObjects["Text37"];
            TXTOBJ13.Text = "Payment Type : " + Paymodes;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ14;
            TXTOBJ14 = (TextObject)RPS.ReportDefinition.ReportObjects["Text38"];
            TXTOBJ14.Text = "KOT's : " + Kotdetno;

            //RPS.SetParameterValue("ImageUrl", QrPath);
            RPS.PrintOptions.PrinterName = GlobalVariable.PrinterName;
            RPS.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            RPS.PrintToPrinter(1, false, 0, 0);
            RPS.Close();
            RPS.Dispose();
            rv.Close();
            rv.Dispose();
            GC.Collect();
            //rv.Show();
        }


        private void PrintOperation_RTCOLD(string Bno)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0;
            Double DisPercent = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " WATER :" + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + "TABLE :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine();
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "QTY", "", "ITEM", "AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 16;
                    }
                    Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DisPercent > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
                    Total = Total - ((Total * DisPercent) / 100);
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal - ((MFTotal * DisPercent) / 100), "0.00"));
                    MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
                }
                if (OthTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal - ((OthTotal * DisPercent) / 100), "0.00"));
                    OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
                }
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) -(sum(T.TAXAMT)*" + DisPercent + ")/100  AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
                TData = GCon.getDataSet(sql);
                if (TData.Rows.Count > 0)
                {
                    for (int i = 0; i <= TData.Rows.Count - 1; i++)
                    {
                        var RData = TData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                        TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                    }
                }
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

                sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
                SData = GCon.getDataSet(sql);
                if (SData.Rows.Count > 0)
                {
                    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                    {
                        var RData = SData.Rows[i];
                        Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                    }
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //EInvoicing("CAFB/002205");
            ArrayList List = new ArrayList();
            Int32 CheckCount = 0;
            CheckCount = Convert.ToInt32(GCon.getValue("select Count(*) from Tbl_CheckPrint where KotNo = '" + KOrderNo + "' and FinYear = '" + FinYear1 + "'"));
            gPrint = true;
            //CheckPrintPrintOperation(KOrderNo);
            if (GlobalVariable.gCompName == "TRNG") { }
            else 
            {
                if (CheckCount > 0 && GlobalVariable.gUserName != "VASANTH")
                {
                    MessageBox.Show("Check Print Already Taken, Cont. Admin", GlobalVariable.gCompanyName);
                    return;
                }
            }
            if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "ITCD" || GlobalVariable.gCompName == "TRNG")
            {
                PrintOPeration_Windows_CheckPrint(KOrderNo);
            }
            else 
            {
                CheckPrintPrintOperation(KOrderNo);
            }
            
            List.Clear();
            sql = "Insert Into Tbl_CheckPrint (KotNo,PrintTakenBy,PrintTakenDate,FinYear) Values ('" + KOrderNo + "','" + GlobalVariable.gUserName + "',getdate(),'" + FinYear1 + "') ";
            List.Add(sql);
            if (GCon.Moretransaction(List) > 0) { List.Clear(); }
        }

        private void CheckPrintPrintOperation(string KNo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            DataTable TData = new DataTable();
            DataTable SData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
            Double DisPercent = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("CKP" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT D.KOTDETAILS,D.Kotdate,H.Adddatetime,H.Adduserid,H.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,H.STWCODE,H.STWNAME,Isnull(ItemDiscPerc,0) as ItemDiscPerc  ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'')  AND H.Kotdetails = '" + KNo + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(KOTSTATUS,'') <> 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((44 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((44 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((44 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "PRO FORMA INVOICE".Length) / 2) + "PRO FORMA INVOICE");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "CHECK PRINT".Length) / 2) + "CHECK PRINT");
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine();
                        if (GlobalVariable.gCompName == "NZC")
                        {
                            Filewrite.WriteLine("{0,-4}{1,-18}{2,-2}{3,5}{4,8}", "", "ITEM", "", "QTY", "AMOUNT");
                        }
                        else 
                        {
                            Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "QTY", "", "ITEM", "AMOUNT");
                        }
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 17;
                    }
                    if (GlobalVariable.gCompName == "NZC") 
                    {
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), "", Strings.Format(RData["QTY"], "0"),  Strings.Format(RData["AMOUNT"], "0.00"));
                    }
                    else 
                    {
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["AMOUNT"], "0.00"));
                    }
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    if (DisPercent > 0)
                    {
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
                        Vrowcount = Vrowcount + 1;
                    }
                    //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
                    //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
                    OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
                    MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
                if (DiscAmount > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
                    Total = Total - DiscAmount;
                }
                if (MFTotal > 0)
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
                }
                
                if (Check_WaiveSChg.Checked == true) 
                {
                    OthTotal = 0;
                } 
                else
                {
                    if (OthTotal > 0)
                    {
                        if (GlobalVariable.gCompName == "SKYYE")
                        {
                            Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "SC @ 10%:", Strings.Format(OthTotal, "0.00"));
                        }
                        else 
                        {
                            Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
                        }
                    }
                }

                if (Chk_ExmptedTax.Checked == true)
                {
                    TaxTotal = 0;
                }
                else 
                {
                    ////sql = "SELECT A.taxdesc,SUM(T.TAXAMT) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                    ////sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.KOTDETAILS = '" + KNo + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' GROUP BY A.taxdesc ";
                    sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                    sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.KOTDETAILS = '" + KNo + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' GROUP BY A.taxdesc ";
                    TData = GCon.getDataSet(sql);
                    if (TData.Rows.Count > 0)
                    {
                        for (int i = 0; i <= TData.Rows.Count - 1; i++)
                        {
                            var RData = TData.Rows[i];
                            Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
                            TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
                        }
                    }
                }
                
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal + OthTotal) - (Total + TaxTotal + OthTotal);
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + OthTotal + Rnd;
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

                ////sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' ORDER BY AUTOID ";
                ////SData = GCon.getDataSet(sql);
                ////if (SData.Rows.Count > 0)
                ////{
                ////    for (int i = 0; i <= SData.Rows.Count - 1; i++)
                ////    {
                ////        var RData = SData.Rows[i];
                ////        Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
                ////    }
                ////}
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (GlobalVariable.gCompName == "NZC") 
                {
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Total Item Count :" + CountItem, "");
                    Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                }
                DataTable CData = new DataTable();
                DataTable MData = new DataTable();
                DataTable ARMData = new DataTable();
                DataTable RoomData = new DataTable();
                sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM KOT_HDR WHERE KOTDETAILS = '" + KNo + "' AND FINYEAR = '" + FinYear1 + "') ";
                MData = GCon.getDataSet(sql);
                if (MData.Rows.Count > 0)
                {
                    var RData1 = MData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "MCODE: " + RData1["MCODE"]);
                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "MNAME: " + RData1["MNAME"]);
                }
                else
                {
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_HDR where KOTDETAILS = '" + KNo + "' AND FINYEAR = '" + FinYear1 + "') ";
                    ARMData = GCon.getDataSet(sql);
                    if (ARMData.Rows.Count > 0)
                    {
                        var RData1 = ARMData.Rows[0];
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "AR Code: " + RData1["ARCode"]);
                        Filewrite.WriteLine("{0,-4}{1,-33}", "", "AR Name: " + RData1["ARName"]);
                    }
                    else
                    {
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_HDR where KOTDETAILS = '" + KNo + "' AND FINYEAR = '" + FinYear1 + "') ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "-------------");
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
                            Filewrite.WriteLine("{0,-4}{1,-33}", "", "ADD: " + RData1["GuestAdd"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        }
                    }
                }
                sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FINYEAR = D.FINYEAR and R.ChkNo = H.Checkin and D.KOTDETAILS = '" + KNo + "' AND H.FINYEAR = '" + FinYear1 + "' ";
                RoomData = GCon.getDataSet(sql);
                if (RoomData.Rows.Count > 0)
                {
                    var RData1 = RoomData.Rows[0];
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Info");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Name: " + RData1["Mname"]);
                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
                }
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }

                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void Cmd_OrdCancel_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string OrderNo = "";
            string OrderReason = "";
            bool CancelBool = false;
            OrderNo = KOrderNo;
            OrderCancel OC = new OrderCancel(this);
            OC.Kotno = OrderNo;
            OC.ShowDialog();
            OrderReason = OC.OrderReason;
            CancelBool = OC.Cancelbool;
            if (CancelBool == false || OrderReason == "") 
            {
                return;
            }
            sqlstring = " UPDATE KOT_HDR SET DelFlag = 'Y',DELUSER = '" + GlobalVariable.gUserName + "' ,DELDATETIME = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = " UPDATE KOT_DET SET DelFlag = 'Y' ,KotStatus = 'Y',reason = '" + OrderReason + "' ,UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = " UPDATE KOT_DET_TAX SET KOTSTATUS = 'Y',VOID = 'Y',VOIDUSER= '" + GlobalVariable.gUserName + "',VOIDDATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = "DELETE FROM closingqty where TRNNO = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = "UPDATE SUBSTORECONSUMPTIONDETAIL SET Void = 'Y' WHERE Docdetails = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
            List.Add(sqlstring);
            sqlstring = "UPDATE TableMaster SET OpenStatus = '' WHERE Pos IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + OrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
            List.Add(sqlstring);
            sql = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE LocCode IN (SELECT CAST(LocCode AS VARCHAR(10)) FROM KOT_HDR WHERE KOTDETAILS = '" + OrderNo + "') AND TableNo = '" + GlobalVariable.TableNo + "' ";
            List.Add(sqlstring);

            if (GCon.Moretransaction(List) > 0)
            {
                MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                List.Clear();
                OrderPrintOperation(OrderNo, "Y");
                if (GlobalVariable.ServiceType == "Dine-In")
                {
                    ServiceLocation SL = new ServiceLocation();
                    SL.Show();
                    this.Close();
                }
                else
                {
                    ServiceType SL = new ServiceType();
                    SL.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OrderPrintOperation(string kotno,string CanFlag)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,H.DELUSER as Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') = 'Y' And Isnull(D.DelFlag,'') = 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "ORDER CANCEL".ToString().Length) / 2) + "ORDER CANCEL");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void Cmd_NotCharges_Click(object sender, EventArgs e)
        {
            int RowCnt;
            string DescVal = "";

            for (int i = 0; i < dataGridView2.Rows.Count; i++) 
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null) { DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString(); }
                else { DescVal = ""; }
                if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID" || DescVal == "UPI" || DescVal == "R.MEMBER" || DescVal == "COUPON" || DescVal == "CARD" || DescVal == "NEFT") 
                {
                    return;
                }
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "NC";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", Txt_BalAmt.Text);
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
                Calculate();
                Grp_NCRemarks.Visible = true;
                Txt_NCRemarks.Focus();
            }
        }


        private void Cmd_ClearAll_Click(object sender, EventArgs e)
        {
            int RowCnt = dataGridView2.Rows.Count;
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            { }
            else
            {
                sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                GCon.dataOperation(1, sql);
            }
            ////sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
            ////GCon.dataOperation(1, sql);
            Chk_ExmptedTax.Checked = false;
            Check_WaiveSChg.Checked = false;
            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            DataTable TaxData = new DataTable();
            sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            //sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 Union all ";
            //sql = sql + "SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 Union all ";
            //sql = sql + " SELECT a.taxdesc As TDesc,sum(TAXAMT)as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND TAXAMT > 0 group by a.taxdesc";

            sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Having (sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) > 0 Union all ";
            sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0  ";
            sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
            sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
            TaxData = GCon.getDataSet(sql);
            if (TaxData.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                for (int j = 0; j < TaxData.Rows.Count; j++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                    dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                }
            }
            Calculate();
        }

        private void Button_SCARD_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }

            DataTable CardCheck = new DataTable();
            if (GlobalVariable.MainCardDeductFlag == "Y") 
            {
                DataTable CardInfo = new DataTable();
                sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardCode + "' AND [16_DIGIT_CODE] = '" + DigitCode + "'";
                CardInfo = GCon.getDataSet(sql);
                if (CardInfo.Rows.Count > 0) 
                {
                    DataRow dr = CardInfo.Rows[0];
                    if (dr["ISSUETYPE"].ToString() == "MEM")
                    {
                        CardCheck = GCon.getDataSet("Select * from SM_CARDFILE_HDR where CARDCODE = '" + dr["MEMBERCODE"].ToString() + "-00' And isnull(Activation_Flag,'') = 'Y'");
                        if (CardCheck.Rows.Count == 0)
                        {
                            MessageBox.Show("Main Member Card Not Exits");
                            return;
                        }
                    }
                    else 
                    {
                        CardCheck = GCon.getDataSet("Select * from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'");
                        if (CardCheck.Rows.Count == 0)
                        {
                            return;
                        }
                    }
                }
                else 
                {
                    return;
                }
            }
            else 
            {
                CardCheck = GCon.getDataSet("Select * from SM_CARDFILE_HDR where CARDCODE = '" + CardCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'");
                if (CardCheck.Rows.Count == 0)
                {
                    return; 
                }
            }

            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "SCARD")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "SCARD";
                //dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", Txt_BalAmt.Text);
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
                Calculate();
            }
        }

        private void Cmd_Discount_Click(object sender, EventArgs e)
        {
            DiscountUserName = "";
            if (GlobalVariable.gCompName == "RTC")
            {
                Grp_DenoINR.Visible = false;
                Grp_Cards.Visible = false;
                Grp_INR.Visible = false;
                Grp_Discount.Visible = true;
            }
            else 
            {
                string ValDesc = "";
                DataTable CheckPrintFlag = new DataTable();
                sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                CheckPrintFlag = GCon.getDataSet(sql);
                if (CheckPrintFlag.Rows.Count > 0)
                {
                    //MessageBox.Show("Check Print Done, You can't Modify");
                    //return;
                }
                
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Discount On Same User Or Diffrent User", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes)
                {
                    FillDiscount(GlobalVariable.gUserName);
                    DiscountUserName = GlobalVariable.gUserName;
                    Grp_DenoINR.Visible = false;
                    Grp_Cards.Visible = false;
                    Grp_INR.Visible = false;
                    Grp_Discount.Visible = true;
                }
                else 
                {
                    UserSelecton US = new UserSelecton(this);
                    US.ShowDialog();
                    FillDiscount(US.DisUserName);
                    DiscountUserName = US.DisUserName;
                    Grp_DenoINR.Visible = false;
                    Grp_Cards.Visible = false;
                    Grp_INR.Visible = false;
                    Grp_Discount.Visible = true;
                }

                sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                GCon.dataOperation(1, sql);

                DataTable TaxData = new DataTable();
                sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 ";
                if (Check_WaiveSChg.Checked == true) { }
                else
                {
                    sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
                }
                if (Chk_ExmptedTax.Checked == true) { }
                else
                {
                    sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
                }
                TaxData = GCon.getDataSet(sql);
                if (TaxData.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                    for (int j = 0; j < TaxData.Rows.Count; j++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                        ValDesc = TaxData.Rows[j].ItemArray[0].ToString();
                        dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                        if (ValDesc == "DISC")
                        {
                            dataGridView2.Rows[j].Cells[2].Value = String.Format("{0:0.00}", 0.00);
                            dataGridView2.Rows[j].Cells[2].Value = "B";
                        }
                    }
                }
                Calculate();
            }
        }

        public void FillDiscount(string UserName) 
        {
            int PHeight = 168;
            int PWidght = 142;
            int GroupHeight = Grp_Discount.Height;
            DataTable Btndt = new DataTable();
            if (GlobalVariable.gUserCategory == "S" && GlobalVariable.gCompName == "RTC")
            {
                sql = "SELECT DISTINCT DISCPERCENT,Isnull(DiscType,'FIXED PERCENTAGE') as DiscType FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' ";
            }
            else 
            {
                sql = "SELECT DISTINCT DISCPERCENT,Isnull(DiscType,'FIXED PERCENTAGE') as DiscType,isnull(DISCOUNTID,0) as DISCOUNTID FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' AND USERNAME = '" + UserName + "'";
            }
            Grp_Discount.Controls.Clear();
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0)
            {
                Double TotalRow = 0.00;
                TotalRow = Convert.ToDouble(Btndt.Rows.Count) / 3;
                TotalRow = Math.Ceiling(TotalRow);
                GroupHeight = GroupHeight - (Convert.ToInt32(TotalRow * 15));
                PHeight = GroupHeight / Convert.ToInt32(TotalRow);
                PWidght = (Grp_Discount.Width - 30) / 3;
                int X = 15;
                int Y = 15;
                //PHeight = (groupBox1.Height - 20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[0].ToString();
                    ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
                    toolTip1.SetToolTip(btn, dr1[1].ToString());
                    btn.Tag = dr1[2].ToString(); ;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.YellowGreen;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = PWidght;
                    btn.Height = PHeight;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Location = new Point(X, Y);
                    Grp_Discount.Controls.Add(btn);
                    btn.Click += new EventHandler(button_Disc_Click);
                    X = X + PWidght;
                    if (X > Grp_Discount.Width - 20) { Y = Y + (PHeight + 10); X = 15; }
                }
            }
        }

        private void button_Disc_Click(object sender, EventArgs e)
        {
            Double Discper= 0;
            Double BillValue = 0;
            string ValDesc = "";
            string DiscType = "";
            Button selectedBtn = sender as Button;
            Discper = Convert.ToDouble(selectedBtn.Text.ToString());
            string id = selectedBtn.Tag.ToString();
            DiscType = Convert.ToString(GCon.getValue("SELECT DISTINCT Isnull(DiscType,'FIXED PERCENTAGE') FROM DISCOUNTEDUSERLIST WHERE DISCPERCENT = " + Discper + " AND USERNAME = '" + DiscountUserName + "' And DISCOUNTID = '" + id + "'"));
            BillValue = Convert.ToDouble(Txt_BalAmt.Text);
            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;

            if (GlobalVariable.gCompName == "RTC")
            {
                DataTable TaxData = new DataTable();
                sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                sql = sql + " SELECT 'DISC' As TDesc,-((sum(Amount)*" + Discper + ")/100) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
                sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum(isnull(ModifierCharges,0))*" + Discper + ")/100  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 > 0  ";
                if (Check_WaiveSChg.Checked == true) { }
                else
                {
                    sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - (sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*" + Discper + ")/100 as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 ";
                }
                if (Chk_ExmptedTax.Checked == true) { }
                else
                {
                    sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(TAXAMT)-(sum(TAXAMT)*" + Discper + ")/100 as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND TAXAMT > 0 group by a.taxdesc";
                }
                TaxData = GCon.getDataSet(sql);
                if (TaxData.Rows.Count > 0)
                {
                    dataGridView2.Rows.Clear();
                    for (int j = 0; j < TaxData.Rows.Count; j++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                        ValDesc = TaxData.Rows[j].ItemArray[0].ToString();
                        dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                        if (ValDesc == "DISC")
                        {
                            dataGridView2.Rows[j].Cells[2].Value = String.Format("{0:0.00}", Discper);
                        }
                    }
                }
            }
            else 
            {
                double DisCountAmt = 0;
                DiscBasisSelection DBS = new DiscBasisSelection(this);
                DBS.DiscType = DiscType;
                DBS.GlobalDiscPerc = Discper;
                DBS.GBillValue = Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value);
                DBS.ShowDialog();
                DiscountCategory = DBS.DCategory;
                Discper = DBS.GlobalDiscPerc;
                if (DBS.BasisType == "B") 
                {
                    sql = "Update Kot_Det Set ItemDiscPerc = " + Discper + "  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                    GCon.dataOperation(1, sql);

                    DataTable TaxData = new DataTable();
                    sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                    //sql = sql + " SELECT 'DISC' As TDesc,-(sum((Amount*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
                    sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
                    sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 ";
                    if (Check_WaiveSChg.Checked == true) { }
                    else
                    {
                        sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
                    }
                    if (Chk_ExmptedTax.Checked == true) { }
                    else
                    {
                        sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
                    }
                    TaxData = GCon.getDataSet(sql);
                    if (TaxData.Rows.Count > 0)
                    {
                        dataGridView2.Rows.Clear();
                        for (int j = 0; j < TaxData.Rows.Count; j++)
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                            ValDesc = TaxData.Rows[j].ItemArray[0].ToString();
                            dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                            if (ValDesc == "DISC")
                            {
                                dataGridView2.Rows[j].Cells[2].Value = String.Format("{0:0.00}", Discper);
                                dataGridView2.Rows[j].Cells[2].Value = "B";
                            }
                        }
                    }
                }
                if (DBS.BasisType == "I") 
                {
                    sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                    GCon.dataOperation(1, sql);

                    DiscItemUpdation DIU = new DiscItemUpdation(this);
                    DIU.KotOrderNo = KOrderNo;
                    DIU.DUserName = DiscountUserName;
                    DIU.DiscType = DiscType;
                    DIU.GlobalDiscPerc = Discper;
                    DIU.ShowDialog();

                    DataTable TaxData = new DataTable();
                    sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                    //sql = sql + " SELECT 'DISC' As TDesc,-(sum((Amount*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
                    sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
                    sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0  ";
                    if (Check_WaiveSChg.Checked == true) { }
                    else
                    {
                        sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
                    }
                    if (Chk_ExmptedTax.Checked == true) { }
                    else
                    {
                        sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
                    }
                    TaxData = GCon.getDataSet(sql);
                    if (TaxData.Rows.Count > 0)
                    {
                        dataGridView2.Rows.Clear();
                        for (int j = 0; j < TaxData.Rows.Count; j++)
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                            ValDesc = TaxData.Rows[j].ItemArray[0].ToString();
                            dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                            if (ValDesc == "DISC")
                            {
                                dataGridView2.Rows[j].Cells[2].Value = String.Format("{0:0.00}", Discper);
                                dataGridView2.Rows[j].Cells[2].Value = "I";
                            }
                        }
                    }
                }
            }
            Calculate();
        }

        private void Chk_ExmptedTax_CheckedChanged(object sender, EventArgs e)
        {
            int RowCnt = dataGridView2.Rows.Count;
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            { }
            else
            {
                sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                GCon.dataOperation(1, sql);
            }
            ////sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
            ////GCon.dataOperation(1, sql);
            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            DataTable TaxData = new DataTable();
            sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
            ////sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 ";
            sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0  ";
            if (Check_WaiveSChg.Checked == true) { }
            else 
            {
                ////sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 ";
                sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
            }
            if (Chk_ExmptedTax.Checked == true)
            { }
            else 
            {
                ////sql = sql + "  Union all SELECT a.taxdesc As TDesc,sum(TAXAMT)as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND TAXAMT > 0 group by a.taxdesc";
                sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
            }
            TaxData = GCon.getDataSet(sql);
            if (TaxData.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                for (int j = 0; j < TaxData.Rows.Count; j++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                    dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                }
            }
            Calculate();
        }

        private void Check_WaiveSChg_CheckedChanged(object sender, EventArgs e)
        {
            int RowCnt = dataGridView2.Rows.Count;
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            { }
            else
            {
                sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
                GCon.dataOperation(1, sql);
            }
            ////sql = "Update Kot_Det Set ItemDiscPerc = 0  Where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Isnull(ItemDiscPerc,0) > 0 ";
            ////GCon.dataOperation(1, sql);
            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            DataTable TaxData = new DataTable();
            sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT 'DISC' As TDesc,-(sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' Union all ";
            ////sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0 ";
            sql = sql + "SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100))  as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING SUM(isnull(ModifierCharges,0)) > 0  ";
            if (Check_WaiveSChg.Checked == true) { }
            else
            {
                ////sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 ";
                sql = sql + " Union all SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0  ";
            }
            if (Chk_ExmptedTax.Checked == true)
            { }
            else
            {
                ////sql = sql + "  Union all SELECT a.taxdesc As TDesc,sum(TAXAMT)as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND TAXAMT > 0 group by a.taxdesc";
                if (Check_WaiveSChg.Checked == true) 
                {
                    sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND ISNULL(Trans_Flag,'') = '' AND k.TAXAMT > 0 group by a.taxdesc";
                }
                else 
                {
                    sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
                }
                ////sql = sql + " Union all SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND k.TAXAMT > 0 group by a.taxdesc";
            }
            TaxData = GCon.getDataSet(sql);
            if (TaxData.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                for (int j = 0; j < TaxData.Rows.Count; j++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                    dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                }
            }
            Calculate();
        }

        private void Cmd_WalkInfo_Click(object sender, EventArgs e)
        {
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }
            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember != "")
            {
                MessageBox.Show("Room Tagging already done.");
                return;
            }
            string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
            string ARMember = Convert.ToString(GCon.getValue("SELECT ARCode FROM Tbl_ARFlagUpdation Where KotNo = '" + KOrderNo + "'"));
            if (Member == "" && ARMember =="")
            {
                WalkInInfo WI = new WalkInInfo(this);
                WI.KotOrderNo = KOrderNo;
                WI.ShowDialog();
                Cmd_ClearAll_Click(sender, e);
            }
        }

        private void Cmd_ArFlag_Click(object sender, EventArgs e)
        {
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }

            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> 0 And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with Room, R U Sure to Processed for AR ID ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
                //MessageBox.Show("Room Tagging already done.");
                //return;
            }
            string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
            if (Member != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with Member, R U Sure to Processed for AR ID ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
            }
            if (GlobalVariable.AR_ACCode != "") 
            {
                ARFlagUpdation AFU = new ARFlagUpdation(this);
                AFU.KotOrderNo = KOrderNo;
                AFU.ShowDialog();
            }
            else
            {
                MessageBox.Show("Not AR Account Tag in Master");
            }
        }

        private void Button_ROOM_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }

            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember == "")
            {
                MessageBox.Show("For this Bill Room Tag Not Done, Can't Make Room Bill");
                return;
            }

            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "ROOM")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "ROOM";
                //dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", Txt_BalAmt.Text);
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
                Calculate();
            }
        }

        private void Cmd_RoomTag_Click(object sender, EventArgs e)
        {
            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }

            string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
            if (Member != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with Member, R U Sure to Processed for Room ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
                //MessageBox.Show("This Bill is Tag with Member,Room Tagging Not Allowed");
                //return;
            }
            string ARMember = Convert.ToString(GCon.getValue("SELECT ARCode FROM Tbl_ARFlagUpdation Where KotNo = '" + KOrderNo + "'"));
            if (ARMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with AR Member, R U Sure to Processed for Room ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
                //MessageBox.Show("This Bill is Tag with AR Member,Room Tagging Not Allowed");
                //return;
            }

            RoomAccTagging AFU = new RoomAccTagging(this);
            AFU.KotOrderNo = KOrderNo;
            AFU.ShowDialog();
        }

        private void Cmd_Tips_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "TIPS")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                Double BalanceAmt = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                if (BalanceAmt < 0) 
                {
                    RowCnt = dataGridView2.RowCount;
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "TIPS";
                    //dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                    dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", -(BalanceAmt));
                    dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
                }
                Calculate();
            }
        }

        private void Cmd_MemberTag_Click(object sender, EventArgs e)
        {
            //DataTable CheckPrintFlag = new DataTable();
            //sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            //CheckPrintFlag = GCon.getDataSet(sql);
            //if (CheckPrintFlag.Rows.Count > 0)
            //{
            //    MessageBox.Show("Check Print Done, You can't Modify");
            //    return;
            //}
            string ARMember = Convert.ToString(GCon.getValue("SELECT ARCode FROM Tbl_ARFlagUpdation Where KotNo = '" + KOrderNo + "'"));
            if (ARMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with AR Member, R U Sure to Processed for Member ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
            }
            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '0' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with Room, R U Sure to Processed for Member ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
            }

            MemberTagging MT = new MemberTagging(this);
            MT.KotOrderNo = KOrderNo;
            MT.ShowDialog();
        }

        private void GetRights()
        {
            DataTable Rights = new DataTable();
            Cmd_CloseBill.Enabled = false;
            Cmd_OrdCancel.Enabled = false;
            sql = "select Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM from Tbl_TransactionFormUserTag Where FormName = 'KOT ENTRY FORM' And UserName = '" + GlobalVariable.gUserName + "' ";
            Rights = GCon.getDataSet(sql);
            if (Rights.Rows.Count > 0)
            {
                if (Rights.Rows[0].ItemArray[2].ToString() == "Y")
                { DelRightsFlag = true; }
                else { DelRightsFlag = false; }
            }
            Rights = new DataTable();
            sql = "select Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM from Tbl_TransactionFormUserTag Where FormName = 'SETTLEMENT FORM' And UserName = '" + GlobalVariable.gUserName + "' ";
            Rights = GCon.getDataSet(sql);
            if (Rights.Rows.Count > 0)
            {
                if (Rights.Rows[0].ItemArray[0].ToString() == "Y")
                { CloseBillRightsFlag = true; }
                else { CloseBillRightsFlag = false; }
            }

            //if (GlobalVariable.gUserCategory == "S") 
            //{
            //    DelRightsFlag = true;
            //    CloseBillRightsFlag = true;
            //}
        }

        private void Button_ZOMATO_Click(object sender, EventArgs e)
        {
            int RowCnt;

            if (GlobalVariable.gCompName == "CSC") 
            {
                ////string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
                ////if (Member != "")
                ////{
                ////    MessageBox.Show("Member is Tag in this Order Want to");
                ////    return;
                ////}
            }

            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == Button_ZOMATO.Text)
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = Button_ZOMATO.Text;
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void Button_PG_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == Button_PG.Text)
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = Button_PG.Text;
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
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

        private void Cmd_PrepaidTagging_Click(object sender, EventArgs e)
        {
            PrePaidCardTagging PPC = new PrePaidCardTagging(this);
            PPC.KotOrderNo = KOrderNo;
            PPC.ShowDialog();
        }

        private void Button_PREPAID_Click(object sender, EventArgs e)
        {
            Double CardBal = 0;
            Double TotCardAmt = 0;
            String DescVal = "";
            int i = 0;
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }

            for (i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                }
                if (DescVal == "PREPAID") 
                {
                    dataGridView2.Rows.RemoveAt(i); 
                }
            }

            Calculate();

            PrePaidMultiCardTagging PPC = new PrePaidMultiCardTagging(this);
            PPC.KotOrderNo = KOrderNo;
            PPC.BillAmount = Convert.ToDouble(Txt_BalAmt.Text);
            PPC.ShowDialog();

           
            //DataTable CardDetails = new DataTable();
            //DataTable CardCheck = new DataTable();
            //DataTable KotNonCheck = new DataTable();
            //sql = "select Isnull(KotNo,'') as KotNo,Isnull(FinYear,'') as FinYear,Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(HolderName,'') as HolderName from Tbl_PrePaidCardTagging Where KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "'";
            //KotNonCheck = GCon.getDataSet(sql);
            //if (KotNonCheck.Rows.Count > 0)
            //{
            //    CardCheck = GCon.getDataSet("Select * from SM_CARDFILE_HDR where CARDCODE = '" + KotNonCheck.Rows[0].ItemArray[3].ToString() + "' And [16_DIGIT_CODE] = '" + KotNonCheck.Rows[0].ItemArray[2].ToString() + "' And isnull(Activation_Flag,'') = 'Y' And Isnull(IssueType,'') = 'PREP' ");
            //    if (CardCheck.Rows.Count == 0)
            //    {
            //        return;
            //    }
            //    else 
            //    {
            //        sql = "SELECT MEMBERCODE,CARDCODE,MEMBERSUBCODE,ISSUETYPE,Isnull(Balance,0) as Balance FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + KotNonCheck.Rows[0].ItemArray[3].ToString() + "' AND [16_DIGIT_CODE] = '" + KotNonCheck.Rows[0].ItemArray[2].ToString() + "' And isnull(Activation_Flag,'') = 'Y' AND ISSUETYPE = 'PREP' ";
            //        CardDetails = GCon.getDataSet(sql);
            //        if (CardDetails.Rows.Count > 0)
            //        {
            //            DataRow dr1 = CardDetails.Rows[0];
            //            CardBal = Convert.ToDouble(dr1["Balance"]);
            //            if (CardBal > 0){}
            //            else { MessageBox.Show("Prepaid Card Balance Not Availble."); return; }
            //        }
            //    }
            //}
            //else 
            //{
            //    return;
            //}
            DataTable KotNonCheck = new DataTable();
            sql = "select Isnull(KotNo,'') as KotNo,Isnull(FinYear,'') as FinYear,Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(HolderName,'') as HolderName,Isnull(DeductAmt,0) as DeductAmt from Tbl_PrePaidCardTagging Where KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                for (i = 0; i < KotNonCheck.Rows.Count; i++) 
                {
                    TotCardAmt = TotCardAmt + Convert.ToDouble(KotNonCheck.Rows[i].ItemArray[5]);
                }
            }

            if (TotCardAmt <= 0) 
            {
                return;
            }

            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "PREPAID")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "PREPAID";
                //dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                ////if (Convert.ToDouble(Txt_BalAmt.Text) > CardBal) 
                ////{
                ////    dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", CardBal);
                ////}
                ////else 
                ////{
                ////    dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", Txt_BalAmt.Text);
                ////}
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = String.Format("{0:0.##}", TotCardAmt);
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
                Calculate();
            }
        }

        private void Button_UPI_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }
            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "UPI")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "UPI";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Cmd_SMFlag_Click(object sender, EventArgs e)
        {
            string ARMember = Convert.ToString(GCon.getValue("SELECT ARCode FROM Tbl_ARFlagUpdation Where KotNo = '" + KOrderNo + "'"));
            if (ARMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with AR Member, R U Sure to Processed for Member ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
            }
            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '0' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("This Bill is Tag with Room, R U Sure to Processed for Member ", GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes) { }
                else { return; }
            }

            SmartCardTagging SMT = new SmartCardTagging(this);
            SMT.KotOrderNo = KOrderNo;
            SMT.ShowDialog();
        }

        private void Button_NEFT_Click(object sender, EventArgs e)
        {
            int RowCnt;
            var cell1 = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NC")
                .FirstOrDefault();
            if (cell1 != null)
            {
                return;
            }

            var cell = dataGridView2.Rows.Cast<DataGridViewRow>()
                .SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(x => string.Format("{0}", x.FormattedValue) == "NEFT")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "NEFT";
                dataGridView2.Rows[RowCnt - 1].Cells[1].Value = 0;
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
            }
        }
    }
}
