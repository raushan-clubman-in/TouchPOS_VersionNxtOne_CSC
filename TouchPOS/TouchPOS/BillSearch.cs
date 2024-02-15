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

namespace TouchPOS
{
    public partial class BillSearch : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public BillSearch()
        {
            InitializeComponent();
        }

        string sql = "";

        private void BillSearch_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");

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
            Dtp_FromDate.MinDate = GlobalVariable.ServerDate;
            Dtp_FromDate.MaxDate = GlobalVariable.ServerDate;
            Dtp_ToDate.MinDate = GlobalVariable.ServerDate;
            Dtp_ToDate.MaxDate = GlobalVariable.ServerDate;

            DataTable Rights = new DataTable();
            if (GlobalVariable.gUserCategory != "S")
            {
                sql = "SELECT Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM,UserName FROM Tbl_TransactionFormUserTag WHERE ISNULL(FormName,'') = 'SETTLEMENT FORM MODIFY' And Isnull(UserName,'') = '" + GlobalVariable.gUserName + "' AND (ISNULL(EditM,'N') = 'Y' Or ISNULL(DelM,'N') = 'Y')";
                Rights = GCon.getDataSet(sql);
                if (Rights.Rows.Count > 0)
                {
                    if (Rights.Rows[0].ItemArray[1].ToString() == "Y")
                    { Cmd_SettChanges.Enabled = true; }
                    else { Cmd_SettChanges.Enabled = false; }
                    if (Rights.Rows[0].ItemArray[2].ToString() == "Y")
                    { Cmd_Delete.Enabled = true; }
                    else { Cmd_Delete.Enabled = false; }
                }
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
        }

        private void Cmd_Search_Click(object sender, EventArgs e)
        {
            DataTable BillData = new DataTable();
            sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo FROM BILL_HDR  ";
            sql = sql + " WHERE BillDate Between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' And '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Order by BillDate Desc,BillDetails Desc ";
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
                    dataGridView1.Rows[i].Cells[1].Value = Strings.Format(BillData.Rows[i].ItemArray[1],"dd/MM/yyyy");
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
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Hide();
        }

        private void Cmd_SettChanges_Click(object sender, EventArgs e)
        {
            //return;
            int rowindex = dataGridView1.CurrentRow.Index;
            string Bno = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            ModifyPayForm MPF = new ModifyPayForm();
            MPF.CBillNo = Bno;
            MPF.Show();
        }

        private void Cmd_Delete_Click(object sender, EventArgs e)
        {
            //return;
            ArrayList List = new ArrayList();
            string DOrderNo = "";
            string OrderReason = "";
            bool CancelBool = false;
            int TChairNo = 0;
            int oldChairNo = 0;
            string sqlstring = "";
            string OrderNo = "";
            int rowindex = dataGridView1.CurrentRow.Index;
            string Bno = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            string OthBno = "";
            string ActBillNo = "";

            DOrderNo = Bno;
            OrderCancel OC = new OrderCancel(this);
            OC.Kotno = DOrderNo;
            OC.OptionOrder = "B";
            OC.ShowDialog();
            OrderReason = OC.OrderReason;
            CancelBool = OC.Cancelbool;
            if (CancelBool == false || OrderReason == "")
            {
                return;
            }

            DataTable CheckSCARD = new DataTable();
            DataTable CheckROOM = new DataTable();
            DataTable CardTrans = new DataTable();
            DataTable BTData = new DataTable();

            if (GlobalVariable.gCompName == "CSC")
            {
                OthBno = GCon.getValue("SELECT isnull(OthBillDetails,'') as OthBillDetails FROM Bill_Det Where BillDetails = '" + Bno + "'").ToString();

                sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo,LocCode,";
                sql = sql + " (Select Top 1 KOTDETAILS From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as Kotdetails FROM BILL_HDR WHERE BillDetails = '" + Bno + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                if (OthBno != "")
                {
                    sql = sql + " Union All SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo,LocCode,";
                    sql = sql + " (Select Top 1 KOTDETAILS From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as Kotdetails FROM BILL_HDR WHERE BillDetails = '" + OthBno + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                }
                sql = sql + " Order by BillDate Desc,BillDetails Desc ";
                BTData = GCon.getDataSet(sql);
                if (BTData.Rows.Count > 0)
                {
                    for (int i = 0; i < BTData.Rows.Count; i++) 
                    {
                        DataRow dr = BTData.Rows[i];
                        ActBillNo = ActBillNo + " '" + dr["BillDetails"].ToString() +"',";
                    }
                    ActBillNo = ActBillNo.Remove(ActBillNo.Length - 1);

                    OrderNo = BTData.Rows[0].ItemArray[7].ToString();
                    TChairNo = Convert.ToInt32(GCon.getValue("SELECT ISNULL(MAX(ChairSeqNo),0) + 1 FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + BTData.Rows[0].ItemArray[5].ToString() + "' AND ISNULL(LocCode,0) = " + BTData.Rows[0].ItemArray[6].ToString() + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    oldChairNo = Convert.ToInt32(GCon.getValue("SELECT ISNULL(ChairSeqNo,0) FROM KOT_HDR WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));

                    sqlstring = " UPDATE BILL_HDR SET DelFlag = 'Y',REASON = '" + OrderReason + "',Upduserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE BillDetails in ( " + ActBillNo + ") AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE BILL_det SET DelFlag = 'Y' WHERE BillDetails in ( " + ActBillNo + ") ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE BillSettlement SET DelFlag = 'Y' WHERE BILLNO in ( " + ActBillNo + ") AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_HDR SET BILLSTATUS ='PO',OldChairSeqNo = " + oldChairNo + " ,ChairSeqNo = " + TChairNo + " WHERE KOTDETAILS in (Select KOTDETAILS From KOT_DET WHERE BillDetails in ( " + ActBillNo + ") AND ISNULL(FinYear,'') = '" + FinYear1 + "') AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_DET SET BILLDETAILS ='' WHERE BillDetails in (" + ActBillNo + ") AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);

                    sql = "SELECT * FROM BillSettlement WHERE BILLNO  in (" + ActBillNo + ") AND PAYMENTMODE = 'SCARD' AND ISNULL(DELFLAG,'') <> 'Y'";
                    CheckSCARD = GCon.getDataSet(sql);
                    if (CheckSCARD.Rows.Count > 0)
                    {
                        Double CardBal = 0;
                        sql = "select CARDCODE,BILL_AMOUNT from SM_POSTRANSACTION WHERE BILL_NO  in (" + ActBillNo + ") AND ISNULL(VOID,'') <> 'Y'";
                        CardTrans = GCon.getDataSet(sql);
                        if (CardTrans.Rows.Count > 0)
                        {
                            CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardTrans.Rows[0].ItemArray[0].ToString() + "' And isnull(Activation_Flag,'') = 'Y'"));
                            sqlstring = " UPDATE SM_POSTRANSACTION SET VOID ='Y' WHERE BILL_NO in (" + ActBillNo + ") AND ISNULL(VOID,'') <> 'Y' ";
                            List.Add(sqlstring);
                            sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE+" + Convert.ToDouble(CardTrans.Rows[0].ItemArray[1]) + " WHERE CARDCODE='" + CardTrans.Rows[0].ItemArray[0].ToString() + "'";
                            List.Add(sqlstring);
                            string EBal = GCon.abcdAdd((CardBal + Convert.ToDouble(CardTrans.Rows[0].ItemArray[1])).ToString());
                            sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + CardTrans.Rows[0].ItemArray[0].ToString() + "'";
                            List.Add(sqlstring);
                        }
                    }
                    sql = "SELECT * FROM BillSettlement WHERE BILLNO in (" + ActBillNo + ") AND PAYMENTMODE = 'ROOM' AND ISNULL(DELFLAG,'') <> 'Y'";
                    CheckROOM = GCon.getDataSet(sql);
                    if (CheckROOM.Rows.Count > 0)
                    {
                        sqlstring = " UPDATE roomledger SET VoidStatus = 'Y' WHERE Docno in (" + ActBillNo + ") ";
                        List.Add(sqlstring);
                    }

                    if (GCon.Moretransaction(List) > 0)
                    {
                        MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        List.Clear();
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else 
            {
                sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo,LocCode,";
                sql = sql + " (Select Top 1 KOTDETAILS From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as Kotdetails FROM BILL_HDR WHERE BillDetails = '" + Bno + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Order by BillDate Desc,BillDetails Desc";
                BTData = GCon.getDataSet(sql);
                if (BTData.Rows.Count > 0)
                {
                    OrderNo = BTData.Rows[0].ItemArray[7].ToString();
                    TChairNo = Convert.ToInt32(GCon.getValue("SELECT ISNULL(MAX(ChairSeqNo),0) + 1 FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + BTData.Rows[0].ItemArray[5].ToString() + "' AND ISNULL(LocCode,0) = " + BTData.Rows[0].ItemArray[6].ToString() + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    oldChairNo = Convert.ToInt32(GCon.getValue("SELECT ISNULL(ChairSeqNo,0) FROM KOT_HDR WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    //sqlstring = " UPDATE KOT_HDR SET BillStatus = 'PO' WHERE KOTDETAILS = '" + OrderNo + "' ";
                    //List.Add(sqlstring);
                    //sqlstring = " UPDATE KOT_DET SET BILLDETAILS = '' WHERE KOTDETAILS = '" + OrderNo + "' ";
                    //List.Add(sqlstring);

                    sqlstring = " UPDATE BILL_HDR SET DelFlag = 'Y',REASON = '" + OrderReason + "',Upduserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE BILL_det SET DelFlag = 'Y' WHERE BillDetails = '" + Bno + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE BillSettlement SET DelFlag = 'Y' WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_HDR SET BILLSTATUS ='PO',OldChairSeqNo = " + oldChairNo + " ,ChairSeqNo = " + TChairNo + " WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_DET SET BILLDETAILS ='' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);

                    sql = "SELECT * FROM BillSettlement WHERE BILLNO = '" + Bno + "' AND PAYMENTMODE = 'SCARD' AND ISNULL(DELFLAG,'') <> 'Y'";
                    CheckSCARD = GCon.getDataSet(sql);
                    if (CheckSCARD.Rows.Count > 0)
                    {
                        Double CardBal = 0;
                        sql = "select CARDCODE,BILL_AMOUNT from SM_POSTRANSACTION WHERE BILL_NO = '" + Bno + "' AND ISNULL(VOID,'') <> 'Y'";
                        CardTrans = GCon.getDataSet(sql);
                        if (CardTrans.Rows.Count > 0)
                        {
                            CardBal = Convert.ToDouble(GCon.getValue("Select Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardTrans.Rows[0].ItemArray[0].ToString() + "' And isnull(Activation_Flag,'') = 'Y'"));
                            sqlstring = " UPDATE SM_POSTRANSACTION SET VOID ='Y' WHERE BILL_NO = '" + Bno + "' AND ISNULL(VOID,'') <> 'Y' ";
                            List.Add(sqlstring);
                            sqlstring = " UPDATE SM_CARDFILE_HDR SET BALANCE = BALANCE+" + Convert.ToDouble(CardTrans.Rows[0].ItemArray[1]) + " WHERE CARDCODE='" + CardTrans.Rows[0].ItemArray[0].ToString() + "'";
                            List.Add(sqlstring);
                            string EBal = GCon.abcdAdd((CardBal + Convert.ToDouble(CardTrans.Rows[0].ItemArray[1])).ToString());
                            sqlstring = " UPDATE SM_CARDFILE_HDR SET EBALANCE = '" + EBal + "' WHERE CARDCODE='" + CardTrans.Rows[0].ItemArray[0].ToString() + "'";
                            List.Add(sqlstring);
                        }
                    }
                    sql = "SELECT * FROM BillSettlement WHERE BILLNO = '" + Bno + "' AND PAYMENTMODE = 'ROOM' AND ISNULL(DELFLAG,'') <> 'Y'";
                    CheckROOM = GCon.getDataSet(sql);
                    if (CheckROOM.Rows.Count > 0)
                    {
                        sqlstring = " UPDATE roomledger SET VoidStatus = 'Y' WHERE Docno = '" + Bno + "' ";
                        List.Add(sqlstring);
                    }

                    ////sqlstring = " UPDATE KOT_HDR SET DelFlag = 'Y',DELUSER = '" + GlobalVariable.gUserName + "' ,DELDATETIME = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    ////List.Add(sqlstring);
                    ////sqlstring = " UPDATE KOT_DET SET DelFlag = 'Y' ,KotStatus = 'Y',reason = '" + OrderReason + "' ,UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    ////List.Add(sqlstring);
                    ////sqlstring = " UPDATE KOT_DET_TAX SET KOTSTATUS = 'Y',VOID = 'Y',VOIDUSER= '" + GlobalVariable.gUserName + "',VOIDDATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    ////List.Add(sqlstring);

                    if (GCon.Moretransaction(List) > 0)
                    {
                        MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        List.Clear();
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void RefreshGrid() 
        {
            DataTable BillData = new DataTable();
            dataGridView1.Rows.Clear();
            sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo FROM BILL_HDR  ";
            sql = sql + " WHERE BillDate Between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' And '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  Order by BillDate Desc,BillDetails Desc ";
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
