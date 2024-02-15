using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class SplitTableItem : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public int Slno = 0;
        public int LocationCode = 0;
        bool gPrint = true;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);

        public readonly ServiceLocation _form1;

        public SplitTableItem(ServiceLocation form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable Ocpd2 = new DataTable();

        private void SplitTableItem_Load(object sender, EventArgs e)
        {
            sql = "SELECT LocName,TableNo,LocCode,ChairSeqNo,KOTDETAILS FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And LocCode = " + LocationCode + " Order by 1 ";
            if (GlobalVariable.gCompName == "SKYYE")
            {
                sql = "SELECT LocName,H.TableNo,LocCode,ChairSeqNo,KOTDETAILS FROM KOT_HDR H,TableMaster T WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And LocCode = " + LocationCode + " And H.TableNo = T.TableNo Order by LocName,TableOrder ";
            }
            Ocpd2 = GCon.getDataSet(sql);
            if (Ocpd2.Rows.Count > 0)
            {
                List<string> lst1 = new List<string>();
                foreach (DataRow r in Ocpd2.Rows)
                {
                    lst1.Add(r["LocName"].ToString() + "=>" + r["TableNo"].ToString() + "=>" + r["ChairSeqNo"].ToString() + "=>" + r["LocCode"].ToString() + "=>" + r["KOTDETAILS"].ToString());
                }
                ToListBox.Items.Clear();
                ToListBox.DataSource = lst1;
            }
        }

        private void ToListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void Cmd_Refresh_Click(object sender, EventArgs e)
        {
            string FromselectedItem = "";
            string FromKotNo = "";
            string FromorderNo = "";
            FromselectedItem = ToListBox.SelectedItem.ToString();
            string[] FromItem = FromselectedItem.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            FromorderNo = FromItem[4];
            FromKotNo = Convert.ToString(GCon.getValue("SELECT KOTNO FROM KOT_HDR WHERE KOTDETAILS = '" + FromorderNo + "' And ISNULL(TableNo,'') = '" + FromItem[1] + "' AND ISNULL(LocCode,0) = " + FromItem[3] + " AND ISNULL(ChairSeqNo,0) = " + FromItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].Width = 200;
            this.dataGridView1.Columns[0].Width = 50;
            DataTable KotData = new DataTable();
            sql = "Select SLNO,ITEMCODE,ITEMDESC,QTY,KOTNO,KOTDETAILS,ITEMTYPE,POSCODE,UOM,RATE,AMOUNT,MODIFIER,AUTOID,isnull(OrderNo,0) as OrderNo,Isnull(KotStatus,'N') as KotStatus from KOT_det where KOTDETAILS = '" + FromorderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND Isnull(KotStatus,'N') <> 'Y' ORDER BY SLNO ";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0)
            {
                for (int i = 0; i < KotData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[0]);
                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(KotData.Rows[i].ItemArray[1]);
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(KotData.Rows[i].ItemArray[2]);
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[3]);
                }
            }
            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns[1].ReadOnly = true;
            this.dataGridView1.Columns[2].ReadOnly = true;
            this.dataGridView1.Columns[3].ReadOnly = true;
        }

        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Transfer_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string DocType = "TKOT";
            double TotQty = 0, ToQty = 0, BalQty = 0;
            double FromTotQty = 0, ToTotQty = 0;
            string Itemcode = "", ItemName = "";
            int ItemSlno = 0;
            int TKotNo = 0;
            int TChairNo = 0;
            int TCovers = 0;
            int TMaxSlno = 0;
            string sqlstring = "";
            string TorderNo = "";
            int i = 0;
            string FromselectedItem = "";
            string FromKotNo = "";
            string FromorderNo = "";
            FromselectedItem = ToListBox.SelectedItem.ToString();
            string[] FromItem = FromselectedItem.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            FromorderNo = FromItem[4];

            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { ItemSlno = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value); }
                else { ItemSlno = 0; }
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                { Itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value); }
                else { Itemcode = ""; }
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                { ItemName = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                else { ItemName = ""; }
                if (dataGridView1.Rows[i].Cells[3].Value != null)
                { TotQty = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value); }
                else { TotQty = 0; }
                if (dataGridView1.Rows[i].Cells[4].Value != null)
                { ToQty = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value); }
                else { ToQty = 0; }
                FromTotQty = FromTotQty + TotQty;
                ToTotQty = ToTotQty + ToQty;
                if (ToQty > TotQty)
                {
                    MessageBox.Show("Transfer Qty Can't be Greater then Total Qty for " + ItemName);
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[2];
                    return;
                }
                decimal d = new Decimal(ToQty);
                d = d % 1;
                if (d == Convert.ToDecimal(0.50) || d == Convert.ToDecimal(0.00)) { }
                else { MessageBox.Show("given Qty not allowed to split for " + ItemName); dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[2]; return; }
            }
            FromTotQty = FromTotQty - ToTotQty;
            if (FromTotQty <= 0)
            {
                MessageBox.Show("Split Not possible U are Spliting all Item and Qty, use Merge Table or Transfer Table");
                return;
            }
            if (ToTotQty <= 0)
            {
                MessageBox.Show("Not item Qty selected For Spliting!");
                return;
            }

            FromKotNo = Convert.ToString(GCon.getValue("SELECT ISNULL(KOTNO,'') as KOTNO FROM KOT_HDR WHERE KOTDETAILS = '" + FromorderNo + "' And ISNULL(TableNo,'') = '" + FromItem[1] + "' AND ISNULL(LocCode,0) = " + FromItem[3] + " AND ISNULL(ChairSeqNo,0) = " + FromItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            TChairNo = Convert.ToInt32(GCon.getValue("SELECT ISNULL(MAX(ChairSeqNo),0) + 1 FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + FromItem[1] + "' AND ISNULL(LocCode,0) = " + FromItem[3] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            DocType = Convert.ToString(GCon.getValue("Select Isnull(KotPrefix,'') as KotPrefix from ServiceLocation_Hdr Where LocCode = " + FromItem[3] + ""));
            TKotNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' AND DOCTYPE = '" + DocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ")) + 1;
            TorderNo = DocType + "/" + TKotNo.ToString("000000");

            sqlstring = " INSERT INTO KOT_HDR (TTYPE,BOOKNO,CHITNO,KotNo,Kotdetails,Kotdate,DocType,SaleType,AccountCode,SLCode,MCode,Mname,CARDHOLDERCODE,CARDHOLDERNAME,TableNo,STCode,Receiptstatus,ServerName,Covers,Pricetype,TotalTax,Guest,PaymentType,SubPaymentMode,ServiceType,Postingtype,RoomNo,RoundOff,Total,BillAmount,Checkin, ";
            sqlstring = sqlstring + " PartyOrderNo,Remarks,Adduserid,Adddatetime,upduserid,BillStatus,ServerCode,DelFlag,Manualbillstatus,DiscountAmt,PackAmt,[16_DIGIT_CODE],TipsAmt, AdCgsAmt,PartyAmt,RoomAmt,STWCODE,STWNAME,SerType,LocCode,LocName,ChairSeqNo,FinYear,NCFlag,NCCategory) ";
            //sqlstring = sqlstring + " SELECT TTYPE,BOOKNO,CHITNO,'" + TKotNo + "','" + TorderNo + "',Kotdate,DocType,SaleType,AccountCode,SLCode,MCode,Mname,CARDHOLDERCODE,CARDHOLDERNAME,TableNo,STCode,Receiptstatus,ServerName,Covers,Pricetype,TotalTax,Guest,PaymentType,SubPaymentMode,ServiceType,Postingtype,RoomNo,RoundOff,Total,BillAmount,Checkin, ";
            sqlstring = sqlstring + " SELECT TTYPE,BOOKNO,CHITNO,'" + TKotNo + "','" + TorderNo + "',Kotdate,'" + DocType + "',SaleType,AccountCode,SLCode,MCode,Mname,CARDHOLDERCODE,CARDHOLDERNAME,TableNo,STCode,Receiptstatus,ServerName,Covers,Pricetype,TotalTax,Guest,PaymentType,SubPaymentMode,ServiceType,Postingtype,RoomNo,RoundOff,Total,BillAmount,Checkin, ";
            sqlstring = sqlstring + " PartyOrderNo,Remarks,'" + GlobalVariable.gUserName + "',getdate(),'',BillStatus,ServerCode,DelFlag,Manualbillstatus,DiscountAmt,PackAmt,[16_DIGIT_CODE],TipsAmt, AdCgsAmt,PartyAmt,RoomAmt,STWCODE,STWNAME,SerType,LocCode,LocName," + TChairNo + ",FinYear,NCFlag,NCCategory FROM KOT_HDR WHERE Kotdetails = '" + FromorderNo + "' ";
            List.Add(sqlstring);

            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { ItemSlno = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value); }
                else { ItemSlno = 0; }
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                { Itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value); }
                else { Itemcode = ""; }
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                { ItemName = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                else { ItemName = ""; }
                if (dataGridView1.Rows[i].Cells[3].Value != null)
                { TotQty = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value); }
                else { TotQty = 0; }
                if (dataGridView1.Rows[i].Cells[4].Value != null)
                { ToQty = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value); }
                else { ToQty = 0; }
                if (ToQty > TotQty)
                {
                    MessageBox.Show("Transfer Qty Can't be Greater then Total Qty for " + ItemName);
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[2];
                    return;
                }

                if (ToQty > 0 && ToQty == TotQty)
                {
                    TMaxSlno = TMaxSlno + 1;
                    DataTable FromKotData = new DataTable();
                    ////sql = "SELECT KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                    ////sql = sql + " TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode, ";
                    ////sql = sql + " TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,HAPPYSTATUS,FinYear,ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,BusinessSource,Isnull(QTY2,0) as QTY2 FROM KOT_det WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(DelFlag,'') <> 'Y' And ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    sql = "SELECT ISNULL(KOTNO,'') AS KOTNO,ISNULL(KOTDETAILS,'') AS KOTDETAILS,ISNULL(KOTDATE,'') AS KOTDATE,ISNULL(BILLDETAILS,'') AS BILLDETAILS,ISNULL(CATEGORY,'') AS CATEGORY,ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(ITEMDESC,'') AS ITEMDESC,ISNULL(GROUPCODE,'') AS GROUPCODE,ISNULL(ITEMTYPE,'') AS ITEMTYPE,ISNULL(POSCODE,'') AS POSCODE,ISNULL(UOM,'') AS UOM,ISNULL(QTY,0) AS QTY,ISNULL(RATE,0) AS RATE,ISNULL(AMOUNT,0) AS AMOUNT,ISNULL(TAXTYPE,'') AS TAXTYPE,ISNULL(TAXCODE,'') AS TAXCODE,ISNULL(TAXAMOUNT,0) AS TAXAMOUNT,";
                    sql = sql + " ISNULL(TAXACCOUNTCODE,'') AS TAXACCOUNTCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,ISNULL(MCODE,'') AS MCODE,ISNULL(SCODE,'') AS SCODE,ISNULL(COVERS,0) AS COVERS,ISNULL(TABLENO,'') AS TABLENO,ISNULL(KOTTYPE,'') AS KOTTYPE,ISNULL(PAYMENTMODE,'') AS PAYMENTMODE,ISNULL(DelFlag,'') AS DelFlag,ISNULL(AddUserid,'') AS AddUserid,ISNULL(Adddatetime,'') AS Adddatetime,ISNULL(UpdUserid,'') AS UpdUserid,ISNULL(PACKPERCENT,0) AS PACKPERCENT,ISNULL(PACKAMOUNT,0) AS PACKAMOUNT,ISNULL(PROMOTIONALST,'') AS PROMOTIONALST,ISNULL(SUBGroupCode,'') AS SUBGroupCode, ";
                    sql = sql + " ISNULL(TIPSPER,0) AS TIPSPER,ISNULL(TipsAmt,0) AS TipsAmt,ISNULL(AdCgsPer,0) AS AdCgsPer,ISNULL(AdCgsAmt,0) AS AdCgsAmt,ISNULL(PartyPer,0) AS PartyPer,ISNULL(PartyAmt,0) AS PartyAmt,ISNULL(RoomPer,0) AS RoomPer,ISNULL(RoomAmt,0) AS RoomAmt,ISNULL(MKOTNO,'') AS MKOTNO,ISNULL(SLNO,0) AS SLNO,ISNULL(Modifier,'') AS Modifier,ISNULL(OrderNo,0) AS OrderNo,ISNULL(HAPPYSTATUS,'') AS HAPPYSTATUS,ISNULL(FinYear,'') AS FinYear,ISNULL(ServiceOrder,0) AS ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,ISNULL(BusinessSource,'') AS BusinessSource,Isnull(QTY2,0) as QTY2,ISNULL(ItemPrintFlag,'') AS ItemPrintFlag,ISNULL(CheckNo,'') AS CheckNo  FROM KOT_det WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(DelFlag,'') <> 'Y' And ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    FromKotData = GCon.getDataSet(sql);
                    if (FromKotData.Rows.Count > 0)
                    {
                        for (int J = 0; J < FromKotData.Rows.Count; J++)
                        {
                            DataRow dr = FromKotData.Rows[J];
                            sqlstring = "INSERT INTO KOT_det (KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                            sqlstring = sqlstring + "TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode,";
                            sqlstring = sqlstring + "TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,RefOrderNo,HAPPYSTATUS,FinYear,ServiceOrder,ModifierCharges,BusinessSource,QTY2,ItemPrintFlag,CheckNo)";
                            sqlstring = sqlstring + " VALUES('" + TKotNo + "','" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["BILLDETAILS"] + "','" + dr["CATEGORY"] + "','" + dr["ITEMCODE"] + "','" + dr["ITEMDESC"] + "','" + dr["GROUPCODE"] + "','" + dr["ITEMTYPE"] + "', ";
                            sqlstring = sqlstring + " '" + dr["POSCODE"] + "','" + dr["UOM"] + "'," + dr["QTY"] + "," + dr["RATE"] + "," + dr["AMOUNT"] + ",'SALES',''," + dr["TAXAMOUNT"] + ", ";
                            sqlstring = sqlstring + " '','" + dr["KOTSTATUS"] + "','" + dr["MCODE"] + "','" + dr["SCODE"] + "','" + TCovers + "','" + FromItem[1] + "','" + dr["KOTTYPE"] + "','','" + dr["DelFlag"] + "','" + dr["AddUserid"] + "','" + Strings.Format(dr["Adddatetime"], "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "'," + dr["PACKPERCENT"] + "," + dr["PACKAMOUNT"] + ",'" + dr["PROMOTIONALST"] + "','" + dr["SUBGroupCode"] + "', ";
                            sqlstring = sqlstring + " " + dr["TIPSPER"] + "," + dr["TipsAmt"] + "," + dr["AdCgsPer"] + "," + dr["AdCgsAmt"] + "," + dr["PartyPer"] + "," + dr["PartyAmt"] + "," + dr["RoomPer"] + "," + dr["RoomAmt"] + ",''," + (TMaxSlno) + ",'" + dr["Modifier"] + "'," + Convert.ToInt16(dr["OrderNo"]) + ",'" + FromorderNo + "','" + dr["HAPPYSTATUS"] + "','" + dr["FinYear"] + "'," + Convert.ToInt16(dr["ServiceOrder"]) + "," + dr["ModifierCharges"] + ",'" + dr["BusinessSource"] + "'," + dr["QTY2"] + ",'" + dr["ItemPrintFlag"] + "','" + dr["CheckNo"] + "') ";
                            List.Add(sqlstring);
                        }
                    }
                    DataTable FromTaxData = new DataTable();
                    ////sql = "SELECT KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear FROM KOT_DET_TAX WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(VOID,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    sql = "SELECT ISNULL(KOTDETAILS,'') AS KOTDETAILS,ISNULL(KOTDATE,'') AS KOTDATE,ISNULL(TTYPE,'') AS TTYPE,ISNULL(CHARGECODE,'') AS CHARGECODE,ISNULL(TYPE_CODE,'') AS TYPE_CODE,ISNULL(POSCODE,'') AS POSCODE,ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,ISNULL(TAXCODE,'') AS TAXCODE,ISNULL(TAXON,'') AS TAXON,ISNULL(RATE,0) AS RATE,ISNULL(QTY,0) AS QTY,ISNULL(TAXPERCENT,0) AS TAXPERCENT,ISNULL(TAXAMT,0) AS TAXAMT,ISNULL(ADD_USER,'') AS ADD_USER,ISNULL(ADD_DATE,'') AS ADD_DATE,ISNULL(VOID,'') AS VOID,ISNULL(VOIDUSER,'') AS VOIDUSER,ISNULL(SLNO,0) AS SLNO,ISNULL(FinYear,'') AS FinYear FROM KOT_DET_TAX WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(VOID,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    FromTaxData = GCon.getDataSet(sql);
                    if (FromTaxData.Rows.Count > 0)
                    {
                        for (int J = 0; J < FromTaxData.Rows.Count; J++)
                        {
                            DataRow dr = FromTaxData.Rows[J];
                            sqlstring = "INSERT INTO KOT_DET_TAX (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear) VALUES ( ";
                            sqlstring = sqlstring + " '" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["TTYPE"] + "','" + dr["CHARGECODE"] + "','" + dr["TYPE_CODE"] + "','" + dr["POSCODE"] + "','" + dr["ITEMCODE"] + "','" + dr["KOTSTATUS"] + "', ";
                            sqlstring = sqlstring + " '" + dr["TAXCODE"] + "','" + dr["TAXON"] + "'," + dr["RATE"] + "," + dr["QTY"] + "," + dr["TAXPERCENT"] + "," + dr["TAXAMT"] + ",'" + dr["ADD_USER"] + "','" + Strings.Format(dr["ADD_DATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["VOID"] + "','" + dr["VOIDUSER"] + "', " + (TMaxSlno) + ",'" + dr["FinYear"] + "')";
                            List.Add(sqlstring);
                        }
                    }

                    sqlstring = " DELETE FROM KOT_DET WHERE KOTDETAILS = '" + FromorderNo + "' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " DELETE FROM KOT_DET_TAX WHERE KOTDETAILS = '" + FromorderNo + "'  AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }
                if (ToQty > 0 && ToQty < TotQty)
                {
                    TMaxSlno = TMaxSlno + 1;
                    BalQty = TotQty - ToQty;
                    DataTable FromKotData = new DataTable();
                    ////sql = "SELECT KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                    ////sql = sql + " TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode, ";
                    ////sql = sql + " TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,HAPPYSTATUS,FinYear,ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,BusinessSource,Isnull(QTY2,0) as QTY2 FROM KOT_det WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(DelFlag,'') <> 'Y' And ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    sql = "SELECT ISNULL(KOTNO,'') AS KOTNO,ISNULL(KOTDETAILS,'') AS KOTDETAILS,ISNULL(KOTDATE,'') AS KOTDATE,ISNULL(BILLDETAILS,'') AS BILLDETAILS,ISNULL(CATEGORY,'') AS CATEGORY,ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(ITEMDESC,'') AS ITEMDESC,ISNULL(GROUPCODE,'') AS GROUPCODE,ISNULL(ITEMTYPE,'') AS ITEMTYPE,ISNULL(POSCODE,'') AS POSCODE,ISNULL(UOM,'') AS UOM,ISNULL(QTY,0) AS QTY,ISNULL(RATE,0) AS RATE,ISNULL(AMOUNT,0) AS AMOUNT,ISNULL(TAXTYPE,'') AS TAXTYPE,ISNULL(TAXCODE,'') AS TAXCODE,ISNULL(TAXAMOUNT,0) AS TAXAMOUNT, ";
                    sql = sql + " ISNULL(TAXACCOUNTCODE,'') AS TAXACCOUNTCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,ISNULL(MCODE,'') AS MCODE,ISNULL(SCODE,'') AS SCODE,ISNULL(COVERS,0) AS COVERS,ISNULL(TABLENO,'') AS TABLENO,ISNULL(KOTTYPE,'') AS KOTTYPE,ISNULL(PAYMENTMODE,'') AS PAYMENTMODE,ISNULL(DelFlag,'') AS DelFlag,ISNULL(AddUserid,'') AS AddUserid,ISNULL(Adddatetime,'') AS Adddatetime,ISNULL(UpdUserid,'') AS UpdUserid,ISNULL(PACKPERCENT,0) AS PACKPERCENT,ISNULL(PACKAMOUNT,0) AS PACKAMOUNT,ISNULL(PROMOTIONALST,'') AS PROMOTIONALST,ISNULL(SUBGroupCode,'') AS SUBGroupCode, ";
                    sql = sql + " ISNULL(TIPSPER,0) AS TIPSPER,ISNULL(TipsAmt,0) AS TipsAmt,ISNULL(AdCgsPer,0) AS AdCgsPer,ISNULL(AdCgsAmt,0) AS AdCgsAmt,ISNULL(PartyPer,0) AS PartyPer,ISNULL(PartyAmt,0) AS PartyAmt,ISNULL(RoomPer,0) AS RoomPer,ISNULL(RoomAmt,0) AS RoomAmt,ISNULL(MKOTNO,'') AS MKOTNO,ISNULL(SLNO,0) AS SLNO,ISNULL(Modifier,'') AS Modifier,ISNULL(OrderNo,0) AS OrderNo,ISNULL(HAPPYSTATUS,'') AS HAPPYSTATUS,ISNULL(FinYear,'') AS FinYear,ISNULL(ServiceOrder,0) AS ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,ISNULL(BusinessSource,'') AS BusinessSource,Isnull(QTY2,0) as QTY2,ISNULL(ItemPrintFlag,'') AS ItemPrintFlag,ISNULL(CheckNo,'') AS CheckNo  FROM KOT_det WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(DelFlag,'') <> 'Y' And ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    FromKotData = GCon.getDataSet(sql);
                    if (FromKotData.Rows.Count > 0)
                    {
                        for (int J = 0; J < FromKotData.Rows.Count; J++)
                        {
                            DataRow dr = FromKotData.Rows[J];
                            sqlstring = "INSERT INTO KOT_det (KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                            sqlstring = sqlstring + "TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode,";
                            sqlstring = sqlstring + "TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,RefOrderNo,HAPPYSTATUS,FinYear,ServiceOrder,ModifierCharges,BusinessSource,QTY2,ItemPrintFlag,CheckNo)";
                            sqlstring = sqlstring + " VALUES('" + TKotNo + "','" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["BILLDETAILS"] + "','" + dr["CATEGORY"] + "','" + dr["ITEMCODE"] + "','" + dr["ITEMDESC"] + "','" + dr["GROUPCODE"] + "','" + dr["ITEMTYPE"] + "', ";
                            sqlstring = sqlstring + " '" + dr["POSCODE"] + "','" + dr["UOM"] + "'," + ToQty + "," + dr["RATE"] + "," + ToQty * Convert.ToDouble(dr["RATE"]) + ",'SALES',''," + dr["TAXAMOUNT"] + ", ";
                            sqlstring = sqlstring + " '','" + dr["KOTSTATUS"] + "','" + dr["MCODE"] + "','" + dr["SCODE"] + "','" + TCovers + "','" + FromItem[1] + "','" + dr["KOTTYPE"] + "','','" + dr["DelFlag"] + "','" + dr["AddUserid"] + "','" + Strings.Format(dr["Adddatetime"], "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "'," + dr["PACKPERCENT"] + "," + ((Convert.ToDouble(dr["RATE"]) * Convert.ToDouble(dr["PACKPERCENT"])) / 100) * ToQty + ",'" + dr["PROMOTIONALST"] + "','" + dr["SUBGroupCode"] + "', ";
                            sqlstring = sqlstring + " " + dr["TIPSPER"] + "," + ((Convert.ToDouble(dr["RATE"]) * Convert.ToDouble(dr["TIPSPER"])) / 100) * ToQty + "," + dr["AdCgsPer"] + "," + ((Convert.ToDouble(dr["RATE"]) * Convert.ToDouble(dr["AdCgsPer"])) / 100) * ToQty + "," + dr["PartyPer"] + "," + ((Convert.ToDouble(dr["RATE"]) * Convert.ToDouble(dr["PartyPer"])) / 100) * ToQty + "," + dr["RoomPer"] + "," + ((Convert.ToDouble(dr["RATE"]) * Convert.ToDouble(dr["RoomPer"])) / 100) * ToQty + ",''," + (TMaxSlno) + ",'" + dr["Modifier"] + "'," + Convert.ToInt16(dr["OrderNo"]) + ",'" + FromorderNo + "','" + dr["HAPPYSTATUS"] + "','" + dr["FinYear"] + "'," + Convert.ToInt16(dr["ServiceOrder"]) + "," + (Convert.ToDouble(dr["ModifierCharges"]) / TotQty) * ToQty + ",'" + dr["BusinessSource"] + "'," + ToQty + ",'" + dr["ItemPrintFlag"] + "','" + dr["CheckNo"] + "') ";
                            List.Add(sqlstring);
                        }
                    }

                    DataTable FromTaxData = new DataTable();
                    ////sql = "SELECT KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear FROM KOT_DET_TAX WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(VOID,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    sql = "SELECT ISNULL(KOTDETAILS,'') AS KOTDETAILS,ISNULL(KOTDATE,'') AS KOTDATE,ISNULL(TTYPE,'') AS TTYPE,ISNULL(CHARGECODE,'') AS CHARGECODE,ISNULL(TYPE_CODE,'') AS TYPE_CODE,ISNULL(POSCODE,'') AS POSCODE,ISNULL(ITEMCODE,'') AS ITEMCODE,ISNULL(KOTSTATUS,'') AS KOTSTATUS,ISNULL(TAXCODE,'') AS TAXCODE,ISNULL(TAXON,'') AS TAXON,ISNULL(RATE,0) AS RATE,ISNULL(QTY,0) AS QTY,ISNULL(TAXPERCENT,0) AS TAXPERCENT,ISNULL(TAXAMT,0) AS TAXAMT,ISNULL(ADD_USER,'') AS ADD_USER,ISNULL(ADD_DATE,'') AS ADD_DATE,ISNULL(VOID,'') AS VOID,ISNULL(VOIDUSER,'') AS VOIDUSER,ISNULL(SLNO,0) AS SLNO,ISNULL(FinYear,'') AS FinYear FROM KOT_DET_TAX WHERE KOTDETAILS = '" + FromorderNo + "' And Isnull(VOID,'') <> 'Y' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    FromTaxData = GCon.getDataSet(sql);
                    if (FromTaxData.Rows.Count > 0)
                    {
                        for (int J = 0; J < FromTaxData.Rows.Count; J++)
                        {
                            DataRow dr = FromTaxData.Rows[J];
                            sqlstring = "INSERT INTO KOT_DET_TAX (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear) VALUES ( ";
                            sqlstring = sqlstring + " '" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["TTYPE"] + "','" + dr["CHARGECODE"] + "','" + dr["TYPE_CODE"] + "','" + dr["POSCODE"] + "','" + dr["ITEMCODE"] + "','" + dr["KOTSTATUS"] + "', ";
                            sqlstring = sqlstring + " '" + dr["TAXCODE"] + "','" + dr["TAXON"] + "'," + dr["RATE"] + "," + ToQty + "," + dr["TAXPERCENT"] + "," + ((ToQty * Convert.ToDouble(dr["RATE"])) * Convert.ToDouble(dr["TAXPERCENT"])) / 100 + ",'" + dr["ADD_USER"] + "','" + Strings.Format(dr["ADD_DATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["VOID"] + "','" + dr["VOIDUSER"] + "', " + (TMaxSlno) + ",'" + dr["FinYear"] + "')";
                            List.Add(sqlstring);
                        }
                    }

                    sqlstring = " UPDATE KOT_DET SET QTY =  " + BalQty + " , AMOUNT = " + BalQty + " * RATE ,UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',RefOrderNo = '" + TorderNo + "' WHERE KOTDETAILS = '" + FromorderNo + "' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_DET_TAX SET QTY =  " + BalQty + ",TAXAMT = ((RATE*" + BalQty + ") * TAXPERCENT) /100,  VOIDUSER= '" + GlobalVariable.gUserName + "',VOIDDATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + FromorderNo + "' AND ISNULL(SLNO,0) = " + ItemSlno + " AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }
            }

            sqlstring = "UPDATE KOT_det SET PACKPERCENT = P.packingpercent,TipsPer = P.tips, AdCgsPer = P.adcharge  FROM posmaster P,KOT_det K WHERE P.poscode = K.POSCODE AND KOTDETAILS = '" + FromorderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = "UPDATE KOT_det SET PACKPERCENT = P.packingpercent,TipsPer = P.tips, AdCgsPer = P.adcharge  FROM posmaster P,KOT_det K WHERE P.poscode = K.POSCODE AND KOTDETAILS = '" + TorderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);

            sqlstring = " UPDATE Kot_Det SET PACKAMOUNT = (AMOUNT * ISNULL(PACKPERCENT,0)) /100,TIPSAMT = (AMOUNT * ISNULL(TipsPer,0)) /100,ADCGSAMT = (AMOUNT * ISNULL(AdCgsPer,0)) /100,ROOMAMT = (AMOUNT * ISNULL(RoomPer,0)) /100,PARTYAMT = (AMOUNT * ISNULL(PartyPer,0)) /100 WHERE KOTDETAILS = '" + FromorderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);
            sqlstring = " UPDATE Kot_Det SET PACKAMOUNT = (AMOUNT * ISNULL(PACKPERCENT,0)) /100,TIPSAMT = (AMOUNT * ISNULL(TipsPer,0)) /100,ADCGSAMT = (AMOUNT * ISNULL(AdCgsPer,0)) /100,ROOMAMT = (AMOUNT * ISNULL(RoomPer,0)) /100,PARTYAMT = (AMOUNT * ISNULL(PartyPer,0)) /100 WHERE KOTDETAILS = '" + TorderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            List.Add(sqlstring);

            if (List.Count < 2) { MessageBox.Show("Nothing to be transfer"); return; }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (TorderNo) + "'";
                List.Add(sqlstring);
                sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (FromorderNo) + "'";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0)
                {
                    MessageBox.Show("Transfer Item Sucessfully Done ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    this.Close();
                }
            }

        }
    }
}
