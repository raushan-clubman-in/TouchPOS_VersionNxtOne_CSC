using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModecardVB;
using System.Collections;
using Microsoft.VisualBasic;

namespace TouchPOS
{
    public partial class WalkInInfo : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public WalkInInfo(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtGuest = new DataTable();
        DataTable dtGSTIN = new DataTable();

        private void WalkInInfo_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            DataTable KotNonCheck = new DataTable();
            AutoCompleteMobile();
            AutoCompleteGSTIN();

            sql = "select KotNo,Isnull(MobileNo,'') as MobileNo,Isnull(GuestName,'') as GuestName,Isnull(GuestAdd,'') as GuestAdd,Isnull(GuestGSTIN,'') as GuestGSTIN,Isnull(GuestPAN,'') as GuestPAN,Isnull(SEZFlag,'N') as SEZFlag,Isnull(GuestCity,'') as GuestCity,Isnull(GuestPin,'') as GuestPin from Tbl_HomeTakeAwayBill Where KotNo = '" + KotOrderNo + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                Txt_MobileNo.Text = KotNonCheck.Rows[0].ItemArray[1].ToString();
                Txt_GuestName.Text = KotNonCheck.Rows[0].ItemArray[2].ToString();
                Txt_Address.Text = KotNonCheck.Rows[0].ItemArray[3].ToString();
                Txt_GSTINNo.Text = KotNonCheck.Rows[0].ItemArray[4].ToString();
                Txt_PANNo.Text = KotNonCheck.Rows[0].ItemArray[5].ToString();
                if (Convert.ToString(KotNonCheck.Rows[0].ItemArray[6]) == "Y")
                { Chk_ApplySEZ.Checked = true; }
                else { Chk_ApplySEZ.Checked = false; }
                Txt_GuestCity.Text = KotNonCheck.Rows[0].ItemArray[7].ToString();
                Txt_GuestPin.Text = KotNonCheck.Rows[0].ItemArray[8].ToString();
            }
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            //groupBox1.Text = "                 ";
            myGroupBox1.Text = "Walk-In-Member ";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void AutoCompleteMobile()
        {
            sql = "Select MobileNo,GuestName from Tbl_HomeTakeAwayGuest ";
            dtGuest = GCon.getDataSet(sql);
            string[] postSource1 = dtGuest
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MobileNo"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource1);
            Txt_MobileNo.AutoCompleteCustomSource = source;
            Txt_MobileNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_MobileNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void AutoCompleteGSTIN()
        {
            sql = "Select Isnull(GuestGSTIN,'') as GuestGSTIN,GuestName from Tbl_HomeTakeAwayGuest ";
            dtGSTIN = GCon.getDataSet(sql);
            string[] postSource2 = dtGSTIN
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("GuestGSTIN"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource2);
            Txt_GSTINNo.AutoCompleteCustomSource = source1;
            Txt_GSTINNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_GSTINNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Txt_MobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "select MobileNo,GuestName,GuestAdd,Isnull(GuestGSTIN,'') as GuestGSTIN,Isnull(GuestPAN,'') as GuestPAN,Isnull(GuestCity,'') as GuestCity,Isnull(GuestPin,'') as GuestPin from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_MobileNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_Address.Text = GCheck.Rows[0].ItemArray[2].ToString();
                    Txt_GSTINNo.Text = GCheck.Rows[0].ItemArray[3].ToString();
                    Txt_PANNo.Text = GCheck.Rows[0].ItemArray[4].ToString();
                    Txt_GuestCity.Text = GCheck.Rows[0].ItemArray[5].ToString();
                    Txt_GuestPin.Text = GCheck.Rows[0].ItemArray[6].ToString();
                }
            }
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            String ASEZ = "N";
            DataTable GCheck = new DataTable();
            DataTable RefKotTax = new DataTable();
            DataTable CheckSEZtax = new DataTable();
            int i = 0;
            string Taxon, Taxcode, IType;
            double TPercent = 0.00;
            double Rate=0.00;
            int Qty = 1,Slno = 0;
            double Zero1, ZeroA1, ZeroB1, One1, OneA1, OneB1, Two1, TwoA1, TwoB1, Three1, ThreeA1, ThreeB1;
            double GZero1, GZeroA1, GZeroB1, GOne1, GOneA1, GOneB1, GTwo1, GTwoA1, GTwoB1, GThree1, GThreeA1, GThreeB1;
            string kotstatus = "N";

            if (Txt_MobileNo.Text == "" || Txt_MobileNo.Text.Length == 0) 
            { 
                MessageBox.Show("Mobile No Can't Blank", GlobalVariable.gCompanyName);
                return;
            }

            if (Chk_ApplySEZ.Checked == true) { ASEZ = "Y"; }
            else { ASEZ = "N"; }

            if (ASEZ == "Y") 
            {
                sql = "select d.* from kot_det d,posmenulinK p where d.POSCODE = p.Pos and d.ITEMCODE = p.ItemCode and isnull(TaxOnItemSEZ,'') = '' and KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                CheckSEZtax = GCon.getDataSet(sql);
                if (CheckSEZtax.Rows.Count > 0) 
                {
                    MessageBox.Show("Tax Charge code not define for all item for this KOT Items");
                    return;
                }
            }

            sql = "select * from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_MobileNo.Text + "'";
            GCheck = GCon.getDataSet(sql);
            if (GCheck.Rows.Count > 0)
            {
                sqlstring = "UPDATE Tbl_HomeTakeAwayGuest SET GuestAdd = '" + Txt_Address.Text + "', GuestGSTIN = '" + Txt_GSTINNo.Text + "', GuestPAN = '" + Txt_PANNo.Text + "',GuestCity = '" + Txt_GuestCity.Text + "',GuestPin = '" + Txt_GuestPin.Text + "'  Where MobileNo = '" + Txt_MobileNo.Text + "' ";
                List.Add(sqlstring);
                //if (GCon.Moretransaction(List) > 0)
                //{
                //    List.Clear();
                //}
            }
            else
            {
                sqlstring = "Insert Into Tbl_HomeTakeAwayGuest (GuestName,GuestAdd,MobileNo,AddUser,AddDate,GuestGSTIN,GuestPAN,GuestCity,GuestPin) ";
                sqlstring = sqlstring + " Values ('" + Txt_GuestName.Text + "','" + Txt_Address.Text + "','" + Txt_MobileNo.Text + "','" + GlobalVariable.gUserName + "',getdate(),'" + Txt_GSTINNo.Text + "','" + Txt_PANNo.Text + "','" + Txt_GuestCity.Text + "','" + Txt_GuestPin.Text + "')";
                List.Add(sqlstring);
                //if (GCon.Moretransaction(List) > 0)
                //{
                //    List.Clear();
                //}
            }

            DataTable KotCheck = new DataTable();
            sql = "select * from Tbl_HomeTakeAwayBill Where KotNo = '" + KotOrderNo +"'";
            KotCheck = GCon.getDataSet(sql);
            if (KotCheck.Rows.Count > 0)
            {
                sqlstring = "UPDATE Tbl_HomeTakeAwayBill SET GuestName = '" + Txt_GuestName.Text + "', GuestAdd = '" + Txt_Address.Text + "', MobileNo = '" + Txt_MobileNo.Text + "', GuestGSTIN = '" + Txt_GSTINNo.Text + "', GuestPAN = '" + Txt_PANNo.Text + "',SEZFlag = '" + ASEZ + "',GuestCity = '" + Txt_GuestCity.Text + "',GuestPin = '" + Txt_GuestPin.Text + "'  Where KotNo = '" + KotOrderNo + "' ";
                List.Add(sqlstring);
                //if (GCon.Moretransaction(List) > 0)
                //{
                //    List.Clear();
                //}
            }
            else
            {
                sqlstring = "Insert Into Tbl_HomeTakeAwayBill (KotNo,GuestName,GuestAdd,MobileNo,AddUser,AddDate,GuestGSTIN,GuestPAN,SEZFlag,GuestCity,GuestPin) ";
                sqlstring = sqlstring + " Values ('" + KotOrderNo + "','" + Txt_GuestName.Text + "','" + Txt_Address.Text + "','" + Txt_MobileNo.Text + "','" + GlobalVariable.gUserName + "',getdate(),'" + Txt_GSTINNo.Text + "','" + Txt_PANNo.Text + "','" + ASEZ + "','" + Txt_GuestCity.Text + "','" + Txt_GuestPin.Text + "')";
                List.Add(sqlstring);
                //if (GCon.Moretransaction(List) > 0)
                //{
                //    List.Clear();
                //}
            }

            //Tax Details Start
            sqlstring = "DELETE FROM KOT_DET_TAX WHERE KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
            List.Add(sqlstring);
            if (ASEZ == "Y")
            {
                sql = "SELECT D.ITEMCODE,ITEMDESC,POSCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,P.TaxOnItemSEZ as TaxOnItem,RATE,QTY,ISNULL(DELFLAG,'') AS DELFLAG,ISNULL(SLNO,0) AS SLNO FROM kot_det D,posmenulinK P WHERE D.POSCODE = P.Pos AND D.ITEMCODE = P.ItemCode AND KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(BILLDETAILS,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            }
            else 
            {
                sql = "SELECT D.ITEMCODE,ITEMDESC,POSCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,P.TaxOnItem,RATE,QTY,ISNULL(DELFLAG,'') AS DELFLAG,ISNULL(SLNO,0) AS SLNO FROM kot_det D,posmenulinK P WHERE D.POSCODE = P.Pos AND D.ITEMCODE = P.ItemCode AND KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(BILLDETAILS,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            }
            RefKotTax = GCon.getDataSet(sql);
            if (RefKotTax.Rows.Count > 0) 
            {
                DateTime Sdate = Convert.ToDateTime(GCon.getValue("SELECT Kotdate FROM KOT_HDR WHERE KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                for (i = 0; i <= RefKotTax.Rows.Count - 1; i++)
                {
                    DataRow drRef = RefKotTax.Rows[i];
                    object ItemTypeCode = GCon.getValue("select Distinct TAXTYPECODE from CHARGEMASTER Where Chargecode = '" + Convert.ToString(drRef["TaxOnItem"]) + "'");
                    Rate = Convert.ToDouble(drRef["RATE"]);
                    Qty = Convert.ToInt16(drRef["QTY"]);
                    Slno = Convert.ToInt16(drRef["SLNO"]);

                    if (Convert.ToString(drRef["KOTSTATUS"]) != null) { kotstatus = Convert.ToString(drRef["KOTSTATUS"]); }
                    else { kotstatus = "N"; }

                    sqlstring = "SELECT ItemTypeCode,TaxCode,TAXON,TaxPercentage FROM ITEMTYPEMASTER WHERE ItemTypeCode = '" + ItemTypeCode + "' ORDER BY TAXON";
                    DataTable TAXON = new DataTable();
                    TAXON = GCon.getDataSet(sqlstring);
                    if (TAXON.Rows.Count > 0)
                    {
                        Zero1 = 0; ZeroA1 = 0; ZeroB1 = 0; One1 = 0; OneA1 = 0; OneB1 = 0; Two1 = 0; TwoA1 = 0; TwoB1 = 0; Three1 = 0; ThreeA1 = 0; ThreeB1 = 0;
                        GZero1 = 0; GZeroA1 = 0; GZeroB1 = 0; GOne1 = 0; GOneA1 = 0; GOneB1 = 0; GTwo1 = 0; GTwoA1 = 0; GTwoB1 = 0; GThree1 = 0; GThreeA1 = 0; GThreeB1 = 0;
                        for (int j = 0; j < TAXON.Rows.Count; j++)
                        {
                            DataRow dr = TAXON.Rows[j];
                            IType = Convert.ToString(dr["ItemTypeCode"]);
                            Taxcode = Convert.ToString(dr["Taxcode"]);
                            Taxon = Convert.ToString(dr["TAXON"]);
                            TPercent = Convert.ToDouble(dr["TaxPercentage"]);
                            sqlstring = "INSERT INTO KOT_DET_TAX (FinYear,KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO) VALUES ( ";
                            sqlstring = sqlstring + " '" + FinYear1 + "','" + KotOrderNo + "','" + Sdate.ToString("dd-MMM-yyyy") + "','SALE','" + Convert.ToString(drRef["TaxOnItem"]) + "','" + Strings.Trim(IType) + "'";
                            if (Convert.ToString(drRef["POSCODE"]) != null)
                            {
                                sqlstring = sqlstring + ",'" + Convert.ToString(drRef["POSCODE"]) + "'"; //Poscode
                            }
                            else { sqlstring = sqlstring + ",''"; }
                            if (Convert.ToString(drRef["ITEMCODE"]) != null)
                            {
                                sqlstring = sqlstring + ",'" + Convert.ToString(drRef["ITEMCODE"]) + "'"; //Itemcode
                            }
                            else { sqlstring = sqlstring + ",''"; }
                            sqlstring = sqlstring + ",'" + kotstatus + "','" + Taxcode + "','" + Taxon + "'";
                            sqlstring = sqlstring + ",'" + Rate + "'"; //rate
                            sqlstring = sqlstring + ",'" + Qty + "'"; //qty
                            sqlstring = sqlstring + ",'" + TPercent + "',";
                            if (dr["TAXON"].ToString() == "0")
                            {
                                Zero1 = (Rate * TPercent) / 100;
                                GZero1 = GZero1 + Zero1;
                                sqlstring = sqlstring + "'" + Zero1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "0A")
                            {
                                ZeroA1 = (GZero1 * TPercent) / 100;
                                GZeroA1 = GZeroA1 + ZeroA1;
                                sqlstring = sqlstring + "'" + ZeroA1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "0B")
                            {
                                ZeroB1 = ((GZero1 + GZeroA1) * TPercent) / 100;
                                GZeroB1 = GZeroB1 + ZeroB1;
                                sqlstring = sqlstring + "'" + ZeroB1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "1")
                            {
                                One1 = ((Rate + GZero1 + GZeroA1) * TPercent) / 100;
                                GOne1 = GOne1 + One1;
                                sqlstring = sqlstring + "'" + One1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "1A")
                            {
                                OneA1 = (One1 * TPercent) / 100;
                                GOneA1 = GOneA1 + OneA1;
                                sqlstring = sqlstring + "'" + OneA1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "1B")
                            {
                                OneB1 = ((GOne1 + GOneA1) * TPercent) / 100;
                                GOneB1 = GOneB1 + OneB1;
                                sqlstring = sqlstring + "'" + OneB1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "2")
                            {
                                Two1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1) * TPercent) / 100;
                                GTwo1 = GTwo1 + Two1;
                                sqlstring = sqlstring + "'" + Two1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "2A")
                            {
                                TwoA1 = (Two1 * TPercent) / 100;
                                GTwoA1 = GTwoA1 + TwoA1;
                                sqlstring = sqlstring + "'" + TwoA1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "2B")
                            {
                                TwoB1 = ((GTwo1 + GTwoA1) * TPercent) / 100;
                                GTwoB1 = GTwoB1 + TwoB1;
                                sqlstring = sqlstring + "'" + TwoB1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "3")
                            {
                                Three1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1 + GTwo1 + GTwoA1) * TPercent) / 100;
                                GThree1 = GThree1 + Three1;
                                sqlstring = sqlstring + "'" + Three1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "3A")
                            {
                                ThreeA1 = (Three1 * TPercent) / 100;
                                GThreeA1 = GThreeA1 + ThreeA1;
                                sqlstring = sqlstring + "'" + ThreeA1 * Qty + "',";
                            }
                            else if (dr["TAXON"].ToString() == "3B")
                            {
                                ThreeB1 = ((GThree1 + GThreeA1) * TPercent) / 100;
                                GThreeB1 = GThreeB1 + ThreeB1;
                                sqlstring = sqlstring + "'" + ThreeB1 * Qty + "',";
                            }
                            sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N',''," + Slno + ")";
                            List.Add(sqlstring);
                        }
                    }
                }

                if (ASEZ == "Y")
                {
                    sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                    sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,ModifierCharges,QTY,i.TaxPercentage,((ModifierCharges * i.TaxPercentage) /100) as TAXAMT,'" + GlobalVariable.gUserName + "' as ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'MC',FinYear ";
                    sqlstring = sqlstring + " from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmenulinK p WHERE D.ITEMCODE = P.ItemCode AND D.POSCODE = P.Pos AND P.TaxOnItemSEZ = c.CHARGECODE And c.TAXTYPECODE = i.ItemTypeCode And KOTDETAILS = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "' And isnull(ModifierCharges,0) > 0 ";
                    List.Add(sqlstring);
                    sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                    sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,PACKAMOUNT,QTY,i.TaxPercentage,((PACKAMOUNT * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'PP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmenulinK p WHERE D.ITEMCODE = P.ItemCode AND D.POSCODE = P.Pos AND c.TAXTYPECODE = i.ItemTypeCode and c.CHARGECODE = p.TaxOnItemSEZ  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(PACKAMOUNT,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,TipsAmt,QTY,i.TaxPercentage,((TipsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'TP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmenulinK p WHERE D.ITEMCODE = P.ItemCode AND D.POSCODE = P.Pos AND c.TAXTYPECODE = i.ItemTypeCode and c.CHARGECODE = p.TaxOnItemSEZ  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(TipsAmt,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,AdCgsAmt,QTY,i.TaxPercentage,((AdCgsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'AD',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmenulinK p WHERE D.ITEMCODE = P.ItemCode AND D.POSCODE = P.Pos AND c.TAXTYPECODE = i.ItemTypeCode and c.CHARGECODE = p.TaxOnItemSEZ  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(AdCgsAmt,0) > 0  ";
                    List.Add(sqlstring);
                }
                else 
                {

                    sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                    sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,ModifierCharges,QTY,i.TaxPercentage,((ModifierCharges * i.TaxPercentage) /100) as TAXAMT,'" + GlobalVariable.gUserName + "' as ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'MC',FinYear ";
                    sqlstring = sqlstring + " from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmenulinK p WHERE D.ITEMCODE = P.ItemCode AND D.POSCODE = P.Pos AND P.TaxOnItem = c.CHARGECODE And c.TAXTYPECODE = i.ItemTypeCode And KOTDETAILS = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "' And isnull(ModifierCharges,0) > 0 ";
                    List.Add(sqlstring);
                    if (GlobalVariable.gCompName == "SKYYE") 
                    {
                        sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                        sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,PACKAMOUNT,QTY,i.TaxPercentage,((PACKAMOUNT * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'PP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.pos and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(PACKAMOUNT,0) > 0  Union all  ";
                        sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,TipsAmt,QTY,i.TaxPercentage,((TipsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'TP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE d.POSCODE = p.pos and c.TAXTYPECODE = i.ItemTypeCode  and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(TipsAmt,0) > 0  Union all  ";
                        sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,AdCgsAmt,QTY,i.TaxPercentage,((AdCgsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'AD',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE  c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.pos and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(AdCgsAmt,0) > 0  ";
                        List.Add(sqlstring);
                    }
                    else 
                    {
                        sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                        sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,PACKAMOUNT,QTY,i.TaxPercentage,((PACKAMOUNT * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'PP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.poscode and c.CHARGECODE = p.PChgCode  ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(PACKAMOUNT,0) > 0  Union all  ";
                        sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,TipsAmt,QTY,i.TaxPercentage,((TipsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'TP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE d.POSCODE = p.poscode and c.TAXTYPECODE = i.ItemTypeCode  and c.CHARGECODE = p.TipsChgCode  ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(TipsAmt,0) > 0  Union all  ";
                        sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,AdCgsAmt,QTY,i.TaxPercentage,((AdCgsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                        sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'AD',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE  c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.poscode and c.CHARGECODE = p.AdChgChgCode  ";
                        sqlstring = sqlstring + " And KOTDETAILS = '" + KotOrderNo + "'  And FinYear = '" + FinYear1 + "' And isnull(AdCgsAmt,0) > 0  ";
                        List.Add(sqlstring);
                    }
                }
            }
            //Tax Details End
            
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (KotOrderNo) + "'";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    MessageBox.Show("Infomation Updated Successfully");
                    this.Hide();
                }
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_MobileNo.Text = "";
            Txt_GuestName.Text = "";
            Txt_GSTINNo.Text = "";
            Txt_Address.Text = "";
            Txt_PANNo.Text = "";
        }

        private void Txt_GSTINNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "select MobileNo,GuestName,GuestAdd,Isnull(GuestGSTIN,''),Isnull(GuestPAN,'') as GuestPAN from Tbl_HomeTakeAwayGuest Where GuestGSTIN = '" + Txt_GSTINNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_Address.Text = GCheck.Rows[0].ItemArray[2].ToString();
                    Txt_MobileNo.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_PANNo.Text = GCheck.Rows[0].ItemArray[4].ToString();
                }
            }
        }

        private void Txt_MobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

    }
}
