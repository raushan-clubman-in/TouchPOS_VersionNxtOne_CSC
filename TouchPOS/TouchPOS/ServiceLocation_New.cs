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
    public partial class ServiceLocation_New : Form
    {
        GlobalClass GCon = new GlobalClass();
        public bool AddChairFlag = false;
        bool gPrint = false;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ServiceLocation_New()
        {
            InitializeComponent();
        }

        string sql = "";
        string KotCompName = "", KotPrinterName = "";
        string KotCompNameCopy = "", KotPrinterNameCopy = "";
       
        private void ServiceLocation_New_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1366, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            label1.Text = GlobalVariable.gUserName;
            label2.Text = GlobalVariable.ServiceType;
            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
            FillLocation();
            Cmb_Location.SelectedIndex = GlobalVariable.LocTabIndex;
            FillPendingData();

            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[0].Width = 150;
            this.dataGridView2.Columns[1].Width = 50;
            this.dataGridView2.Columns[2].Width = 50;
            this.dataGridView2.Columns[3].Width = 100;

            if (GlobalVariable.gUserCategory == "S") { }
            else 
            {
                DataTable Rights = new DataTable();
                sql = "select Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM from Tbl_TransactionFormUserTag Where FormName = 'KOT ENTRY FORM' And UserName = '" + GlobalVariable.gUserName + "' ";
                Rights = GCon.getDataSet(sql);
                if (Rights.Rows.Count > 0)
                {
                    if (Rights.Rows[0].ItemArray[2].ToString() == "Y")
                    { Cmd_DelKOT.Enabled = true; }
                    else { Cmd_DelKOT.Enabled = false; }
                }
                else { Cmd_DelKOT.Enabled = false; }
            }
        }

        private void FillLocation() 
        {
            DataTable dt = new DataTable();
            sql = "SELECT WaiterName as ServerName,WaiterCode as ServerCode FROM WaiterMaster wHERE WaiterType = 'STEWARD' AND ISNULL(Void,'') <> 'Y' ORDER BY 1 ";
            if (GlobalVariable.gUserCategory == "S")
            {
                sql = "SELECT LocName,LocCode FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            }
            else
            {
                sql = "SELECT LocName,LocCode FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' And LocCode in (Select Loccode from Tbl_LocationUserTag Where UserName = '" + GlobalVariable.gUserName + "') ";
            }
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_Location.DataSource = dt;
                Cmb_Location.DisplayMember = "LocName";
                Cmb_Location.ValueMember = "LocCode";
                Cmb_Location.SelectedIndex = GlobalVariable.LocTabIndex;
            }
        }

        private void FillPendingData() 
        {
            DataTable BillData = new DataTable();

            string Locid, LocNameName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locid = drv["LocCode"].ToString();
            LocNameName = drv["LocName"].ToString();

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            sql = "SELECT KotDetails,KotDate,Isnull(Mcode,'') as Mcode,Isnull(Mname,'') as Mname,Isnull(STWName,'') as WaiterName,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Isnull(LocCode,0) =  "+ Locid +" ";
            sql = sql + " And Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0) 
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(dataGridView1.Font, FontStyle.Bold);

                for (int i = 0; i < BillData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = BillData.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = Strings.Format(DateTime.Parse(BillData.Rows[i].ItemArray[1].ToString()), "dd/MM/yyyy");
                    dataGridView1.Rows[i].Cells[2].Value = BillData.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = BillData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = BillData.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(BillData.Rows[i].ItemArray[5]);
                    dataGridView1.Rows[i].Cells[6].Value = BillData.Rows[i].ItemArray[6];
                    dataGridView1.Rows[i].Cells[7].Value = BillData.Rows[i].ItemArray[7];
                    dataGridView1.Rows[i].Cells[8].Value = BillData.Rows[i].ItemArray[8];
                    dataGridView1.Rows[i].DefaultCellStyle = style;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Cmd_BPOS_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            AddChairFlag = false;
            this.Close();
        }

        private void Cmb_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPendingData();
            Calculate();
        }

        private void Cmd_PlaceOrder_Click(object sender, EventArgs e)
        {
            string TableOpenStatus = "";
            GlobalVariable.LocTabIndex = Cmb_Location.SelectedIndex;
            string Locid, LocNameName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locid = drv["LocCode"].ToString();
            LocNameName = drv["LocName"].ToString();
            GlobalVariable.SLocation = LocNameName.ToString();
            GlobalVariable.TableNo = Convert.ToString(GCon.getValue("SELECT TOP 1 TableNo FROM TableMaster WHERE Pos = " + Int32.Parse(Locid.ToString()) + " AND ISNULL(Freeze,'') <> 'Y'"));
            string TableBilling = Convert.ToString(GCon.getValue("SELECT ISNULL(TableBillingYn,'N') FROM ServiceLocation_Hdr WHERE LOCCODE = " + Int32.Parse(Locid.ToString()) + ""));

            if (GlobalVariable.TableNo == "") 
            {
                MessageBox.Show("Atleast One Table Shoule be in Master", GlobalVariable.gCompanyName);
                return;
            }

            if (TableBilling == "Y") 
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                int RndNumber = 0;
                int RowCnt1 = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(Locid.ToString()) + " AND TableNo = '" + GlobalVariable.TableNo.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt1 > 0)
                {
                    int MaxChirNo = Convert.ToInt16(GCon.getValue("SELECT Isnull(max(ChairSeqNo),0) + 1 as ChairSeqNo FROM KOT_HDR WHERE LocCode = " + Int32.Parse(Locid.ToString()) + " AND TableNo = '" + GlobalVariable.TableNo.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    ChNo = MaxChirNo;
                    VBMath.Randomize();
                    RndNumber = Convert.ToInt32(VBMath.Rnd() * 8000);
                    ChNo = RndNumber;
                }
                EntryForm EF = new EntryForm();
                EF.Loccode = Int32.Parse(Locid.ToString());
                AddChairFlag = false;
                EF.UpdFlag = false;
                GlobalVariable.ChairNo = ChNo;
                PaxForm PF = new PaxForm();
                if (GlobalVariable.gCompName == "HBCC") { }
                else { PF.ShowDialog(); }
                EF.Pax = PF.SPax;
                if (PF.CancelFlag == true) { return; }
                if (GlobalVariable.EntryType.ToUpper() == "MEMBER" || GlobalVariable.EntryType.ToUpper() == "BOTH")
                {
                    MemValidate MV = new MemValidate();
                    MV.LocCode = Int32.Parse(Locid.ToString());
                    MV.ShowDialog();
                    if (MV.MCode == "" && MV.MemType == "M") { return; }
                    if (MV.CancelFlag == true) { return; }
                    EF.MemberCode = MV.MCode;
                    EF.MemberName = MV.MName;
                    EF.CardHolderCode = MV.CardCode;
                    EF.CardHolderName = MV.CardName;
                    EF.DigitCode = MV.DCode;
                    EF.GuestMobno = MV.GMobNo;
                    EF.GuestName = MV.GName;
                }
                EF.Show();
                //this.Hide();
                this.Close();
            }
            else if (TableBilling == "N")
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                int RndNumber = 0;
                int RowCnt1 = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(Locid.ToString()) + " AND TableNo = '" + GlobalVariable.TableNo.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt1 > 0)
                {
                    int MaxChirNo = Convert.ToInt16(GCon.getValue("SELECT Isnull(max(ChairSeqNo),0) + 1 as ChairSeqNo FROM KOT_HDR WHERE LocCode = " + Int32.Parse(Locid.ToString()) + " AND TableNo = '" + GlobalVariable.TableNo.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    ChNo = MaxChirNo;
                    VBMath.Randomize();
                    RndNumber = Convert.ToInt32(VBMath.Rnd() * 8000);
                    ChNo = RndNumber;
                }
                EntryForm EF = new EntryForm();
                EF.Loccode = Int32.Parse(Locid.ToString());
                AddChairFlag = false;
                EF.UpdFlag = false;
                GlobalVariable.ChairNo = ChNo;
                EF.Pax = 0;
                if (GlobalVariable.EntryType.ToUpper() == "MEMBER" || GlobalVariable.EntryType.ToUpper() == "BOTH")
                {
                    MemValidate MV = new MemValidate();
                    MV.LocCode = Int32.Parse(Locid.ToString());
                    MV.ShowDialog();
                    if (MV.MCode == "" && MV.MemType == "M") { return; }
                    if (MV.CancelFlag == true) { return; }
                    EF.MemberCode = MV.MCode;
                    EF.MemberName = MV.MName;
                    EF.CardHolderCode = MV.CardCode;
                    EF.CardHolderName = MV.CardName;
                    EF.DigitCode = MV.DCode;
                    EF.TabBill = "N";
                    EF.GuestMobno = MV.GMobNo;
                    EF.GuestName = MV.GName;
                }
                EF.Show();
                //this.Hide();
                this.Close();
            }
        }

        private void dataGridView1_CellContentChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable KOTData = new DataTable();
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "";
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[0].Value as String))
            { val = ""; }
            else { val = dataGridView1.Rows[rowindex].Cells[0].Value.ToString(); }
            if (val != "") 
            {
                dataGridView2.Rows.Clear();
                sql = "SELECT Itemdesc,SUM(QTY) AS QTY,Rate,SUM(AMOUNT) AS AMOUNT FROM KOT_DET WHERE KotDetails = '" + val + "' AND ISNULL(KotStatus,'') <> 'Y' AND ISNULL(DELFLAG,'') <> 'Y' and FinYear = '"+ FinYear1 +"' GROUP BY Itemdesc,Rate";
                KOTData = GCon.getDataSet(sql);
                if (KOTData.Rows.Count > 0) 
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    for (int i = 0; i < KOTData.Rows.Count; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = KOTData.Rows[i].ItemArray[0];
                        dataGridView2.Rows[i].Cells[1].Value = Convert.ToDouble(KOTData.Rows[i].ItemArray[1]);
                        dataGridView2.Rows[i].Cells[2].Value = Convert.ToDouble(KOTData.Rows[i].ItemArray[2]);
                        dataGridView2.Rows[i].Cells[3].Value = Convert.ToDouble(KOTData.Rows[i].ItemArray[3]);
                        dataGridView2.Rows[i].DefaultCellStyle = style;
                        dataGridView2.Rows[i].Height = 30;
                    }
                }
                Calculate();
            }
        }

        private void Cmd_ReOrder_Click(object sender, EventArgs e)
        {
            GlobalVariable.LocTabIndex = Cmb_Location.SelectedIndex;
            DataTable ReOrderData = new DataTable();
            string Locid, LocNameName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locid = drv["LocCode"].ToString();
            LocNameName = drv["LocName"].ToString();
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "";
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[0].Value as String))
            { val = ""; }
            else { val = dataGridView1.Rows[rowindex].Cells[0].Value.ToString(); }
            if (val != "")
            {
                sql = "SELECT KotDetails,LocCode,TableNo,ChairSeqNo FROM KOT_HDR WHERE KotDetails = '" + val + "' and BillStatus = 'PO' AND  ISNULL(DELFLAG,'') <> 'Y' and FinYear = '" + FinYear1 + "' and Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' ";
                sql = sql + " And Isnull(kotdetails,'') in (select KotDetails from KOT_DET where KotDetails = '" + val + "' and isnull(Billdetails,'') = '' and Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and FinYear = '" + FinYear1 + "') ";
                ReOrderData = GCon.getDataSet(sql);
                if (ReOrderData.Rows.Count > 0)
                {
                    GlobalVariable.TableNo = Convert.ToString(ReOrderData.Rows[0].ItemArray[2]);
                    if (Locid != Convert.ToString(ReOrderData.Rows[0].ItemArray[1]))
                    { Locid = Convert.ToString(ReOrderData.Rows[0].ItemArray[1]); }
                    GlobalVariable.ChairNo = Convert.ToInt32(ReOrderData.Rows[0].ItemArray[3]);
                    EntryForm EF = new EntryForm();
                    EF.Loccode = Int32.Parse(Locid.ToString());
                    EF.UpdFlag = true;
                    EF.Show();
                    this.Close();
                }
            }
            else 
            {
                MessageBox.Show("Sorry! No Record Selected");
            }
        }

        private void Calculate() 
        {
            int counter, qty, totQty = 0;
            double BAsicAmt =0;
            string icode, kotstatus = "";
            for (counter = 0; counter < (dataGridView2.Rows.Count - 1); counter++)
            {
                icode = dataGridView2.Rows[counter].Cells[0].Value.ToString();
                if (icode != "") 
                {
                    BAsicAmt = BAsicAmt + Convert.ToDouble(dataGridView2.Rows[counter].Cells[3].Value.ToString());
                }
            }
            TotBillAmt.Text = BAsicAmt.ToString();
        }

        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            string Locid, LocNameName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locid = drv["LocCode"].ToString();
            LocNameName = drv["LocName"].ToString();

            DataTable BillData = new DataTable();

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            sql = "SELECT KotDetails,KotDate,Isnull(Mcode,'') as Mcode,Isnull(Mname,'') as Mname,Isnull(STWName,'') as WaiterName,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Isnull(LocCode,0) =  " + Locid + " ";
            //sql = sql + " And (Mcode Like '%" + Txt_Search.Text + "%' or Mname Like '%" + Txt_Search.Text + "%') ";
            sql = sql + " And (Mcode Like '%" + Txt_Search.Text + "%' or STWName Like '%" + Txt_Search.Text + "%') ";
            sql = sql + " And Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                for (int i = 0; i < BillData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = BillData.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = Strings.Format(DateTime.Parse(BillData.Rows[i].ItemArray[1].ToString()), "dd/MM/yyyy");
                    dataGridView1.Rows[i].Cells[2].Value = BillData.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = BillData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = BillData.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(BillData.Rows[i].ItemArray[5]);
                    dataGridView1.Rows[i].Cells[6].Value = BillData.Rows[i].ItemArray[6];
                    dataGridView1.Rows[i].Cells[7].Value = BillData.Rows[i].ItemArray[7];
                    dataGridView1.Rows[i].Cells[8].Value = BillData.Rows[i].ItemArray[8];
                    dataGridView1.Rows[i].DefaultCellStyle = style;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Cmd_DupBillPrint_Click(object sender, EventArgs e)
        {
            string Locid, LocNameName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locid = drv["LocCode"].ToString();
            LocNameName = drv["LocName"].ToString();

            BillSearchPrint BSP = new BillSearchPrint();
            BSP.LocNum = Convert.ToInt32(Locid);
            BSP.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CBillNo = "";
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            gPrint = false;
            PrintToKitchen(CBillNo);
        }

        private void PrintToKitchen(string kotno)
        {
            string PName = "";
            DataTable PData = new DataTable();
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + kotno + "' AND ISNULL(updatedBy,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            int NOdrNo = 1;
            //sql = "select DISTINCT i.kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN Positemstorelink I ON D.ITEMCODE = I.ItemCode and d.PosCode = i.POS  ";
            //sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
            sql = "select DISTINCT kitchencode,D.POSCODE from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode AND D.POSCODE = I.PosCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "'";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    PrintKitchen(kotno, KotPrinterName, PName, NOdrNo);
                }
            }
        }

        private void PrintKitchen(string kotno, string PrintName, string KitCode, int OrdNo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "", Remarks = "", Prepaidby = "";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.createdAt as Adddatetime,D.createdBy as Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,UOM,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME,isnull(H.mcode,'') as mcode,isnull(Mname,'') as Mname FROM KOT_DET D,KOT_HDR	H,Positemstorelink I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode and d.PosCode = i.POS AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND I.kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,cast(D.createdAt as time) as Adddatetime,D.createdBy as Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,UOM,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME,isnull(H.mcode,'') as mcode,isnull(Mname,'') as Mname FROM KOT_DET D,KOT_HDR	H,Positemstorelink I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode and d.PosCode = i.POS AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND I.kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,(select top 1 LocName from ServiceLocation_Hdr sh where sh.LocCode = H.LocCode) as LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,UOM,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWCODE,'') AS STWCODE,ISNULL(STWNAME,'') AS STWNAME,isnull(H.mcode,'') as mcode,isnull(Mname,'') as Mname FROM KOT_DET D,KOT_HDR	H,VwPrinterAllocation I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode and D.POSCODE = I.PosCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
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
                        if (GlobalVariable.gCompName == "TRNG")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("KOT").ToString().Length) / 2) + ("KOT").ToString());
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - KitName.ToString().Length) / 2) + BoldOn + KitName.ToString() + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + "STWD  : " + RData["STWNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TABLE NO - " + RData["TABLENO"]).ToString().Length) / 2) + BoldOn + ("TABLE NO - " + RData["TABLENO"]) + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME                 ");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Prepaidby = RData["Adduserid"].ToString();
                            Vrowcount = 13;
                        }
                        else
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));//Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString("HH:mm")
                            //Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(Convert.ToDateTime(RData["Kotdate"]).ToString(), "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(Convert.ToDateTime(RData["Adddatetime"]).ToString(), "T")), 1, 15));
                            //Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Convert.ToDateTime(RData["Kotdate"].ToString()).ToString("dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Convert.ToDateTime(RData["Adddatetime"].ToString()).ToString("T"), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Convert.ToDateTime(RData["Kotdate"].ToString()).ToString("dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Convert.ToDateTime(RData["Adddatetime"].ToString()).ToString("T"), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + "MCODE  : " + RData["mcode"]);
                            Filewrite.WriteLine(Strings.Space(4) + "MNAME  : " + RData["Mname"]);
                            Filewrite.WriteLine(Strings.Space(4) + "SERVER : " + RData["STWCODE"] + " [" + RData["STWNAME"] + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            string llname = "LOCATION : " + RData["LOCNAME"].ToString();
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - llname.Length) / 2) + llname);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME              UOM");
                            Filewrite.WriteLine(Strings.Space(4) + "ITEM NAME                UOM  QTY");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Prepaidby = RData["Adduserid"].ToString();
                            Vrowcount = 13;
                        }
                    }
                    if (GlobalVariable.gCompName == "TRNG")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-22}{3,4}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), "");
                        Vrowcount = Vrowcount + 1;
                    }
                    else
                    {
                        Filewrite.WriteLine("{0,-4}{1,-22}{2,7}{3,4}", "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), Strings.Mid(RData["UOM"].ToString(),1,6), Strings.Format(RData["QTY"], "0"));
                        Vrowcount = Vrowcount + 1;
                    }
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine(Strings.Space(4) + "Total No of Item : " + CountItem);
                Filewrite.WriteLine(Strings.Space(4) + "Prepared by : " + Prepaidby);

                //if (GlobalVariable.gCompName == "NZC")
                //{
                //    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                //    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                //}
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(4) + "Remarks  : " + Remarks);
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));

                for (int i = 1; i <= 5; i++)
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        public void GetPrinter_KOT(string KitCode)
        {
            OleDbConnection ServerConn = new OleDbConnection();
            OleDbDataAdapter servercmd;
            DataSet getserver = new DataSet();
            DataTable dt = new DataTable();
            string sql, ssql;
            sql = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + GlobalVariable.appPath + "\\DBS_KEY.MDB";
            ServerConn.ConnectionString = sql;
            try
            {
                ServerConn.Open();
                ssql = "SELECT COMPUTERNAME ,PRINTERNAME,COMPUTERNAME_Copy,PRINTERNAME_Copy FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin");
                dt = getserver.Tables["admin"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    KotCompName = Convert.ToString(da["Computername"]);
                    KotPrinterName = Convert.ToString(da["printername"]);
                    KotCompNameCopy = Convert.ToString(da["COMPUTERNAME_Copy"]);
                    KotPrinterNameCopy = Convert.ToString(da["PRINTERNAME_Copy"]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                ServerConn.Close();

            }
        }

        private void Cmd_Pay_Click(object sender, EventArgs e)
        {
            try 
            {
                string KOrderNo = "";
                int rowindex = dataGridView1.CurrentRow.Index;
                KOrderNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();

                int Rowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + KOrderNo + "' AND isnull(billdetails,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (Rowcnt > 0)
                {
                    PayForm PF = new PayForm();
                    PF.KOrderNo = KOrderNo;
                    PF.Show();
                    this.Close();
                }
            }
            catch
            {
                throw;
            }
            
        }

        private void Cmd_PrintKot_Click(object sender, EventArgs e)
        {
            string CBillNo = "";
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            gPrint = true;
            PrintToKitchen(CBillNo);
        }

        private void Cmd_DelKOT_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string OrderNo = "";
            string OrderReason = "";
            bool CancelBool = false;
            int rowindex = dataGridView1.CurrentRow.Index;
            string Kno = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();

            OrderNo = Kno;
            OrderCancel OC = new OrderCancel(this);
            OC.Kotno = OrderNo;
            OC.ShowDialog();
            OrderReason = OC.OrderReason;
            CancelBool = OC.Cancelbool;
            if (CancelBool == false || OrderReason == "")
            {
                return;
            }
            if (OrderNo == "") { return; }
            DataTable BTData = new DataTable();
            sql = "SELECT KotDetails,KotDate,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And KotDetails = '" + Kno + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And KotDetails = '" + Kno + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc";
            BTData = GCon.getDataSet(sql);
            if (BTData.Rows.Count > 0)
            {
                OrderNo = BTData.Rows[0].ItemArray[0].ToString();
                sqlstring = " UPDATE KOT_HDR SET DelFlag = 'Y',DELUSER = '" + GlobalVariable.gUserName + "' ,DELDATETIME = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_det SET DelFlag = 'Y' ,KotStatus = 'Y',reason = '" + OrderReason + "' ,UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET_TAX SET KOTSTATUS = 'Y',VOID = 'Y',ADD_USER= '" + GlobalVariable.gUserName + "',ADD_DATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                //sqlstring = "DELETE FROM closingqty where TRNNO = '" + OrderNo + "' ";
                //List.Add(sqlstring);
                sqlstring = "UPDATE SUBSTORECONSUMPTIONDETAIL SET Void = 'Y' WHERE Docdetails = '" + OrderNo + "'";
                List.Add(sqlstring);

                if (GCon.Moretransaction(List) > 0)
                {
                    MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    gPrint = true;
                    OrderPrintOperation(OrderNo, "Y");
                    FillPendingData();
                    Calculate();
                }
                else
                {
                    MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OrderPrintOperation(string kotno, string CanFlag)
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

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.DELDATETIME as Adddatetime,H.DELUSER as Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,UOM,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(STWCODE,'') AS STWCODE,ISNULL(STWNAME,'') AS STWNAME,isnull(H.mcode,'') as mcode,isnull(Mname,'') as Mname FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') = 'Y' And Isnull(D.DelFlag,'') = 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
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
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Convert.ToDateTime(DateTime.Parse(RData["Kotdate"].ToString())).ToString("dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Convert.ToDateTime(DateTime.Parse(RData["Adddatetime"].ToString())).ToString("T"), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"]);
                        //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + "MCODE  : " + RData["mcode"]);
                        Filewrite.WriteLine(Strings.Space(4) + "MNAME  : " + RData["Mname"]);
                        Filewrite.WriteLine(Strings.Space(4) + "SERVER : " + RData["STWCODE"] + " [" + RData["STWNAME"] + "]");
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + "ITEM NAME              UOM    QTY");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    //Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Filewrite.WriteLine("{0,-4}{1,-22}{2,7}{3,4}", "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), Strings.Mid(RData["UOM"].ToString(), 1, 6), Strings.Format(RData["QTY"], "0"));
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
                    //string PName = "";
                    //string KitCode = Convert.ToString(GCon.getValue("select TOP 1 isnull(kitchencode,'') from KOT_DET D,Positemstorelink p Where  D.ItemCode = P.ITEMCODE AND D.PosCode = P.POS AND KotDetails = '" + kotno + "' AND D.itemcode = '" + icode + "'"));
                    //KotPrinterName = "";
                    //PName = KitCode;
                    //GetPrinter_KOT(PName);
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void Cmd_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; 
        }
    }
}
