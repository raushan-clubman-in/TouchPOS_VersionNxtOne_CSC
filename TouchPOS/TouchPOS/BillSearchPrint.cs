using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace TouchPOS
{
    public partial class BillSearchPrint : Form
    {
        GlobalClass GCon = new GlobalClass();
        ModifyPayForm MPF = new ModifyPayForm();
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public BillSearchPrint()
        {
            InitializeComponent();
        }

        string sql = "";

        private void BillSearchPrint_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            //Utility.relocate(this, 1368, 768);
            //Utility.repositionForm(this, screenWidth, screenHeight);
            GCon.GetBillCloseDate();

            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;
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
        }

        private void Cmd_Search_Click(object sender, EventArgs e)
        {
            DataTable BillData = new DataTable();
            sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo FROM BILL_HDR  ";
            sql = sql + " WHERE BillDate = '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Order by BillDate Desc,BillDetails Desc ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(dataGridView1.Font, FontStyle.Bold);

                for (int i = 0; i < BillData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = BillData.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = Strings.Format(BillData.Rows[i].ItemArray[1], "dd/MM/yyyy");
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(BillData.Rows[i].ItemArray[2]);
                    dataGridView1.Rows[i].Cells[3].Value = BillData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = BillData.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[5].Value = BillData.Rows[i].ItemArray[5];
                    dataGridView1.Rows[i].DefaultCellStyle = style;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Cmd_BPOS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_PrintDuplicate_Click(object sender, EventArgs e)
        {
            string CBillNo = "";
            ArrayList List = new ArrayList();
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            //WindowsPring(CBillNo, "D");
            if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "ITCD" || GlobalVariable.gCompName == "TRNG")
            {
                WindowsPring(CBillNo, "D");
            }
            else 
            {
                MPF.gPrint = true;
                MPF.PrintOperation(CBillNo, "D");
            }
            List.Clear();
            sql = "Insert Into BillDuplicatePrint (BillNo,UserName,TakenDate) Values ('" + CBillNo + "','" + GlobalVariable.gUserName + "',getdate()) ";
            List.Add(sql);
            if (GCon.Moretransaction(List) > 0) { List.Clear(); }
        }

        public void WindowsPring(string Bno, string Type) 
        {
            String sqlstring, sql, sql1, sql2;
            String NCRemk = "", BillRemk = "", FssaiNo = "", CatName = "";
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
            

            //RPS.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

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

            ////RPS.SetParameterValue("ImageUrl", QrPath);
            RPS.PrintOptions.PrinterName = GlobalVariable.PrinterName;
            RPS.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
           
            RPS.PrintToPrinter(1, false, 0, 0);

            RPS.Clone();
            RPS.Dispose();
            rv.Close();
            rv.Dispose();
            GC.Collect();
            //rv.Show();
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
