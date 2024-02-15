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

namespace TouchPOS
{
    public partial class ModifyPayForm : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string KOrderNo = "";
        public bool gPrint = true;
        public string CBillNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ModifyPayForm()
        {
            InitializeComponent();
        }

        string sql = "", NCFlag = "N", ExemptTaxFlag = "N", WaiveSCGFlag = "N";
        string PutPay = "";

        private void ModifyPayForm_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            var items = CBillNo.Split('/');
            //int BNo = Convert.ToInt16(CBillNo.Substring(5,6));
            int BNo = Convert.ToInt16(items[1]);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            Grp_NCRemarks.Visible = false;
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].Width = 50;
            this.dataGridView1.Columns[1].Width = 200;
            this.dataGridView1.Columns[2].Width = 50;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            DataTable BTData = new DataTable();
            sql = "SELECT BillDetails,BillDate,TotalAmount,SerType,LocName,(Select Top 1 TableNo From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as TableNo,LocCode,";
            sql = sql + " (Select Top 1 KOTDETAILS From KOT_det K Where k.BILLDETAILS= BILL_HDR.BillDetails and Isnull(k.FinYear,'') = Isnull(BILL_HDR.FinYear,'')) as Kotdetails,Isnull(NCRemarks,'') as NCRemarks,Isnull(Remarks,'') Remarks FROM BILL_HDR WHERE BillDetails = '" + CBillNo + "' And Isnull(DelFlag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Order by BillDate Desc,BillDetails Desc";
            BTData = GCon.getDataSet(sql);
            if (BTData.Rows.Count > 0) 
            {
                label1.Text = BTData.Rows[0].ItemArray[3].ToString();
                label2.Text = "TBL No: " + BTData.Rows[0].ItemArray[4].ToString() + "/" + BTData.Rows[0].ItemArray[5].ToString();
                label3.Text = "Order No: " + BTData.Rows[0].ItemArray[7].ToString();
                KOrderNo = BTData.Rows[0].ItemArray[7].ToString();
                Txt_Remarks.Text = BTData.Rows[0].ItemArray[9].ToString();
                label4.Text = "Bill No: " + CBillNo;
                DataTable KotData = new DataTable();
                sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,SLNO,MODIFIER,AUTOID,Isnull(ExemptTaxFlag,'') as ExemptTaxFlag,Isnull(WaiveSCGFlag,'') WaiveSCGFlag from KOT_det where KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                KotData = GCon.getDataSet(sql);
                if (KotData.Rows.Count > 0)
                {
                    for (int i = 0; i < KotData.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
                        dataGridView1.Rows[i].Cells[1].Value = KotData.Rows[i].ItemArray[3];
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[9]);
                    }
                    ExemptTaxFlag = Convert.ToString(KotData.Rows[0].ItemArray[13]);
                    WaiveSCGFlag = Convert.ToString(KotData.Rows[0].ItemArray[14]);
                    if (ExemptTaxFlag == "Y") 
                    {
                        Chk_ExmptedTax.Checked = true;
                    }
                    if (WaiveSCGFlag == "Y")
                    {
                        Check_WaiveSChg.Checked = true;
                    }
                    panel3.Enabled = false;
                    dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView2.ColumnHeadersVisible = false;
                    dataGridView2.RowHeadersVisible = false;
                    dataGridView2.ReadOnly = true;
                    DataTable TaxData = new DataTable();
                    Double DisPercent = 0,DisAmount =0;
                    string ValDesc = "";
                    //DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    DisAmount = Convert.ToDouble(GCon.getValue("SELECT (sum((Amount*Isnull(ItemDiscPerc,0))/100)) FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));

                    sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                    if (DisAmount > 0) 
                    {
                        //sql = sql + "SELECT 'DISC' As TDesc,- (sum((Amount*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                        sql = sql + "SELECT 'DISC' As TDesc,- (sum(Round(((Amount*Isnull(ItemDiscPerc,0))/100),2))) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
                    }
                    sql = sql + " SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum((isnull(ModifierCharges,0)*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(ModifierCharges,0)) >0  Union all ";
                    sql = sql + " SELECT 'OTH' As TDesc, sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - sum((((isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*Isnull(ItemDiscPerc,0))/100)) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' HAVING sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) > 0 Union all ";
                    //sql = sql + " SELECT a.taxdesc As TDesc,sum(TAXAMT)-(sum(TAXAMT)*" + DisPercent + ")/100 as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' group by a.taxdesc";
                    sql = sql + " SELECT a.taxdesc As TDesc,sum(k.TAXAMT)-(sum((k.TAXAMT * Isnull(d.ItemDiscPerc,0))/100)) as Amount FROM KOT_DET_TAX k,accountstaxmaster a,KOT_det D WHERE k.KOTDETAILS = d.KOTDETAILS AND D.ITEMCODE = k.ITEMCODE AND ISNULL(D.SLNO,0) = ISNULL(k.SLNO,0) AND ISNULL(D.FinYear,'') = ISNULL(k.FinYear,'') and  k.TAXCODE = a.taxcode and k.KOTDETAILS = '" + KOrderNo + "' AND ISNULL(k.FinYear,'') = '" + FinYear1 + "' And isnull(k.Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' group by a.taxdesc ";
                    sql = sql + " Union ALL SELECT PAYMENTMODE,PAYAMOUNT FROM BillSettlement WHERE BILLNO = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    sql = sql + " Union ALL SELECT 'TIPS' As TDesc,ISNULL(ExtraTips,0) AS Amount FROM BILL_HDR WHERE BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And ISNULL(ExtraTips,0) >0 ";
                    sql = sql + " Union ALL SELECT 'REFUND' As TDesc,ISNULL(RefundAmt,0) AS Amount FROM BILL_HDR WHERE BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And ISNULL(RefundAmt,0) >0 ";
                    TaxData = GCon.getDataSet(sql);
                    if (TaxData.Rows.Count > 0)
                    {
                        for (int j = 0; j < TaxData.Rows.Count; j++)
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[j].Cells[0].Value = TaxData.Rows[j].ItemArray[0];
                            ValDesc = TaxData.Rows[j].ItemArray[0].ToString();
                            //dataGridView2.Rows[j].Cells[1].Value = Convert.ToDouble(TaxData.Rows[j].ItemArray[1]);
                            dataGridView2.Rows[j].Cells[1].Value = String.Format("{0:0.00}", TaxData.Rows[j].ItemArray[1]);
                            if (ValDesc == "DISC")
                            {
                                dataGridView2.Rows[j].Cells[2].Value = String.Format("{0:0.00}", DisPercent);
                            }
                        }
                    }
                    NCFlag = Convert.ToString(GCon.getValue("Select Isnull(NCFlag,'') NCFlag from KOT_HDR where KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    if (NCFlag == "Y")
                    {
                        Grp_NCRemarks.Visible = true;
                        Txt_NCRemarks.Text = BTData.Rows[0].ItemArray[8].ToString();
                        Cmd_CASH.Enabled = false;
                        Cmd_Cards.Enabled = false;
                    }
                    else 
                    {
                        Cmd_NotCharges.Enabled = false;
                    }
                }
            }
            Grp_DenoINR.Visible = true;
            Grp_INR.Visible = false;
            Grp_Cards.Visible = false;
            Cmd_CloseBill.Enabled = false;
            FillDiscount();
            Grp_Discount.Visible = false;
            if (GlobalVariable.gCompName == "RTC")
            {
                Cmd_Discount.Enabled = true;
            }
            else
            {
                Cmd_Discount.Enabled = false;
            }
            Calculate();
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

        private void Cmd_BackToMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Calculate() 
        {
            double ToPay = 0,TotalRefund=0;
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
                        if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
                        {
                            ToPay = ToPay - Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                        }
                        else
                        {
                            ToPay = ToPay + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                        }

                        if (DescVal == "REFUND") { TotalRefund = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value); }
                    }
                }
            }
            //Txt_BalAmt.Text = String.Format("{0:0.00}", ToPay.ToString());
            //Txt_BalAmt.Text = String.Format("{0:0.##}", ToPay);
            ToPay = ToPay + 0.01;
            Txt_BalAmt.Text = String.Format("{0:0.##}", Math.Round(ToPay, 0));
            if (Convert.ToDouble(Txt_BalAmt.Text) == 0)
            {
                Cmd_CloseBill.Enabled = true;
                Cmd_CloseBill.BackColor = Color.Green;
                TotalRefund = TotalRefund + (-(Convert.ToDouble(Txt_BalAmt.Text)));
            }
            else 
            {
                Cmd_CloseBill.Enabled = false;
                Cmd_CloseBill.BackColor = Color.LightGray;
            }

            //for (int i = 0; i < dataGridView2.Rows.Count; i++) 
            //{
            //    if (dataGridView2.Rows[i].Cells[1].Value != null) 
            //    {
            //        string DescVal = "";
            //        if (dataGridView2.Rows[i].Cells[0].Value != null)
            //        {
            //            DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
            //        }
            //        else { DescVal = ""; }
            //        if (DescVal == "REFUND" && TotalRefund > 0) 
            //        {
            //            dataGridView2.Rows[i].Cells[1].Value = String.Format("{0:0.##}", TotalRefund);
            //        }
            //    }
            //}
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID") 
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "TIPS" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
                dataGridView2.CurrentCell = dataGridView2[0, RowCnt - 1];
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
                .Where(x => string.Format("{0}", x.FormattedValue) == "SWIGGY")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "SWIGGY";
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

            string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE MCODE IN (select mcode from BILL_HDR WHERE BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')"));
            if (Member == "") { return; }

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
            int PayCount = 0;
            int i = 0;
            int BillNo = 1;
            Double BillAmount = 0;
            Double VariableRefund = 0, BillTotalBal = 0;
            string DescVal = "";
            DateTime BillDate = Convert.ToDateTime(GCon.getValue("Select BillDate from BILL_HDR Where BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            bool PaySettle = CheckValidate();
            if (PaySettle == false)
            {
                MessageBox.Show("MultiSettlement Not Avail,", GlobalVariable.gCompanyName);
                return;
            }
            try 
            {
                for (i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value != null)
                    {
                        DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    }
                    else { DescVal = ""; }
                    if (DescVal == "SCARD")
                    {
                        MessageBox.Show("Re-Settle on SCARD Not Avail");
                    }
                    
                }

                //BillNo = Convert.ToInt32(CBillNo);
                BillTotalBal = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                if (BillTotalBal < 0) 
                {
                    VariableRefund = Convert.ToDouble(Txt_BalAmt.Text = string.IsNullOrEmpty(Txt_BalAmt.Text) ? "0.00" : Txt_BalAmt.Text);
                    VariableRefund = -(VariableRefund);
                }
                DataTable KotDet = new DataTable();
                sqlstring = " Select * from BILL_HDR Where BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                KotDet = GCon.getDataSet(sqlstring);
                if (KotDet.Rows.Count > 0) 
                {
                    sqlstring = "Delete from BillSettlement Where Billno = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
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
                                if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG")
                                {
                                    sqlstring = "Insert Into BillSettlement (FinYear,Billno,BILLDATE,PAYMENTMODE,PAYMENTACCOUNTCODE,MCODE,MNAME,CARDTYPE,INSTRUMENTNO,BANKNAME,RECEIVEDNAME,PAYAMOUNT,BILLAMOUNT,BALANCEAMOUNT,ADDUSERID,ADDDATETIME,DELFLAG) ";
                                    sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + CBillNo + "','" + Strings.Format(BillDate, "dd-MMM-yyyy") + "','" + DescVal + "','','','','','','',''," + PayAmt + "," + BillAmount + "," + Convert.ToDouble(Txt_BalAmt.Text) + ",'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N')";
                                    List.Add(sqlstring);
                                    PayCount = PayCount + 1; 
                                }
                                if (DescVal == "DISC")
                                {
                                    //Double DiscAmt = 0, DiscPerc = 0;
                                    //if (dataGridView2.Rows[i].Cells[1].Value != null) { DiscAmt = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value); }
                                    //if (dataGridView2.Rows[i].Cells[2].Value != null) { DiscPerc = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value); }
                                    //sqlstring = "UPDATE BILL_HDR SET DiscPercent=" + (DiscPerc) + ",DiscAmount = " + -(DiscAmt) + " WHERE BillDetails = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                                    //List.Add(sqlstring);
                                }
                                if (DescVal == "TIPS") 
                                {
                                    sqlstring = " UPDATE BILL_HDR SET ExtraTips = " + PayAmt + " WHERE BillDetails = '" + CBillNo + "' ";
                                    List.Add(sqlstring);
                                }
                                if (DescVal == "REFUND") 
                                {
                                    VariableRefund = VariableRefund + PayAmt;
                                }
                                if (DescVal == "ROOM")
                                {
                                    RoomAvail = true;
                                }
                                
                            }
                        }
                    }

                    if (VariableRefund >= 0)
                    {
                        sqlstring = " UPDATE BILL_HDR SET RefundAmt = " + VariableRefund + " WHERE BillDetails = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                    }

                    if (PayCount == 1 && GlobalVariable.gCompName == "KGA")
                    {
                        sqlstring = " Update KOT_det set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,KOT_det D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update KOT_HDR set PaymentType = D.PAYMENTMODE from KOT_det D,KOT_HDR B where B.Kotdetails = D.KOTDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update BILL_hdr set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,BILL_hdr D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                    }

                    if (PayCount == 1 && GlobalVariable.MultiPayMode == "NO")
                    {
                        sqlstring = " Update KOT_det set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,KOT_det D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update KOT_HDR set PaymentType = D.PAYMENTMODE from KOT_det D,KOT_HDR B where B.Kotdetails = D.KOTDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Update BILL_hdr set PAYMENTMODE = B.PAYMENTMODE from BillSettlement B,BILL_hdr D where B.BILLNO = D.BILLDETAILS And D.BILLDETAILS = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                    }

                    if (RoomAvail == true)
                    {
                        sqlstring = "DELETE FROM ROOMLEDGER WHERE DOCNO = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                        sqlstring = " Insert into roomledger (Chkno,Docno,DocDate,Doctype,Foliono,Amount,PosCode,Roomno,RefNo,CreditDebit,Paymentmode,SlCode,Description,Cancel,AddUserid,AddDatetime,VoidStatus,vouchertype,vouchercategory,Taxcode,Source,BookingId) ";
                        sqlstring = sqlstring + " SELECT (SELECT TOP 1 Checkin FROM kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND H.FinYear = D.FinYear AND D.BILLDETAILS = B.BILLNO AND D.FinYear = B.FinYear ) AS CHKNO,BILLNO AS DOCNO,BILLDATE,'SALE' AS DOCTYPE,1 AS Foliono,PAYAMOUNT,'' as poscode, ";
                        sqlstring = sqlstring + " (SELECT TOP 1 RoomNo FROM kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND H.FinYear = D.FinYear AND D.BILLDETAILS = B.BILLNO  AND D.FinYear = B.FinYear ) as Roomno,0 as Refno,'DEBIT' as creditdebit,'ROOM' as Paymentmode,'' as slcode,'POSBILL-' + BILLNO as description,'N' as Cancel,B.ADDUSERID,B.ADDDATETIME,'N' as VoidStatus,'RM' as vouchertype,'RM' as vouchercategory,'' as taxcode,'POS' as Source, ";
                        sqlstring = sqlstring + " (select TOP 1 isnull(Reservationid,0) from RoomCheckin R,kot_hdr H,kot_det d WHERE H.Kotdetails = D.Kotdetails AND D.BILLDETAILS = B.BILLNO AND H.FinYear = D.FinYear AND D.FinYear = B.FinYear and isnull(R.ChkNo,0) = isnull(H.Checkin,0)) as Bookingid ";
                        sqlstring = sqlstring + " FROM BillSettlement B WHERE B.PAYMENTMODE = 'ROOM'  AND BILLNO = '" + CBillNo + "' ";
                        List.Add(sqlstring);
                    }

                    if (GCon.Moretransaction(List) > 0)
                    {
                        //MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TrnDone = true;
                        List.Clear();
                        gPrint = true;
                        PrintOperation(CBillNo,"O");
                        if (GlobalVariable.gCompName == "EPC" || GlobalVariable.AccountPostFlag == "YES")
                        {
                            sqlstring = " EXEC [PROC_JOURNAL_POSPOST_DIR] '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','P','" + CBillNo + "' ";
                            List.Add(sqlstring);
                            if (GCon.Moretransaction(List) > 0)
                            {
                                List.Clear();
                            }
                        }
                        this.Close();
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
                        if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "SCARD" || DescVal == "ROOM" || DescVal == "ZOMATO" || DescVal == "PG")
                        {
                            PayCount = PayCount + 1;
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
                        }
                        if (DescVal == "ROOM") { RoomAccount = true; }
                    }
                }
            }
            if (PayCount > 1 && RoomAccount == true)
            {
                MessageBox.Show("Multi Settlement Not Avail if Room is applicable,", GlobalVariable.gCompanyName);
                return false;
            }
            if (RoomAccount == true) 
            {
                DataTable RoomCheckOut = new DataTable();
                sql = " SELECT * FROM RoomCheckin WHERE ChkNo IN (SELECT TOP 1 H.Checkin FROM KOT_HDR H , KOT_DET D WHERE H.Kotdetails = D.KOTDETAILS  AND D.BILLDETAILS = '" + CBillNo + "') AND ISNULL(CHECKOUT,'') <> 'Y' And ISNULL(Booking_Status,'') = 'IN HOUSE' ";
                RoomCheckOut = GCon.getDataSet(sql);
                if (RoomCheckOut.Rows.Count == 0) 
                {
                    MessageBox.Show("Can't Modify Settlement, Person Already Checkout!");
                    return false;
                }
            }

            if (PayCount > 1)
            {
                if (GlobalVariable.gCompName == "KGA" || GlobalVariable.MultiPayMode == "NO")
                {
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

        private void PrintOperation_Size33(string Bno,string Type)
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
            const string GS1 = "\u001D";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";
            const string DoubleOn = GS1 + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
            const string DoubleOff = GS1 + "!" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                //DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        if (GlobalVariable.gCompName == "RTC")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
                        else 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (Type == "D") 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "DUPLICATE TAX INVOICE".Length) / 2) + "DUPLICATE TAX INVOICE");
                            if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "NC".Length) / 2) + "NC"); }
                        }
                        else 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                            if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "NC".Length) / 2) + "NC"); }
                        }
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
                    Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
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
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
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

        public void PrintOperation(string Bno, string Type)
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
            const string GS1 = "\u001D";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";
            const string DoubleOn = GS1 + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
            const string DoubleOff = GS1 + "!" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;
            string ItemHSN = "";

            sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                //DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        if (GlobalVariable.gCompName == "RTC")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
                        else
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
                        if (Type == "D")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "RE-PRINT TAX INVOICE".Length) / 2) + "RE-PRINT TAX INVOICE");
                            if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
                        }
                        else
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                            if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
                        }
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        //Filewrite.WriteLine();
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "HSN", "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "QTY", "", "ITEM", "RATE","AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
                        Vrowcount = 16;
                    }
                    if (ItemHSN != RData["HSNNO"].ToString()) 
                    {
                        ////Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(),1,5), "", "", "", "");
                        Filewrite.WriteLine("{0,-4}{1,7}{2,-1}{3,-17}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 7), "", "", "", "");
                        Vrowcount = Vrowcount + 1;
                        ItemHSN = RData["HSNNO"].ToString();
                    }
                    Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                    DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
                    //ItemHSN = Convert.ToString(GCon.getValue("select isnull(HSNNO,'') from itemmaster Where Itemcode = '" + RData["ITEMCODE"].ToString() + "'"));
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
                    sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ) ";
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
                        sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ) ";
                        CData = GCon.getDataSet(sql);
                        if (CData.Rows.Count > 0)
                        {
                            var RData1 = CData.Rows[0];
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", RData1["GuestName"]);
                            Filewrite.WriteLine("{0,-4}{1,-41}", "", "GSTIN: " + RData1["GuestGSTIN"]);
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
                sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
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
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
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

        //static void TestImage(ThermalPrinter printer) 
        //{ }

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
            const string GS1 = "\u001D";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";
            const string DoubleOn = GS1 + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
            const string DoubleOff = GS1 + "!" + "\0";

            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT Top 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
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
                        if (GlobalVariable.gCompName == "RTC")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
                        else
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
                        }
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
                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) -(sum(T.TAXAMT)*" + DisPercent + ")/100 AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' GROUP BY A.taxdesc ";
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
            ArrayList List = new ArrayList();
            gPrint = true;
            PrintOperation(CBillNo,"D");
            List.Clear();
            sql = "Insert Into BillDuplicatePrint (BillNo,UserName,TakenDate) Values ('" + CBillNo + "','" + GlobalVariable.gUserName + "',getdate()) ";
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
            Double Total = 0, BillTotal = 0, TaxTotal = 0;

            VBMath.Randomize();
            vOutfile = Strings.Mid("CKP" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            string Add1 = Convert.ToString(GCon.getValue(" SELECT ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT D.KOTDETAILS,D.Kotdate,H.Adddatetime,H.Adduserid,H.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo  ";
            sql = sql + " FROM KOT_DET D,KOT_HDR H WHERE D.KOTDETAILS = H.Kotdetails   AND H.Kotdetails = '" + KNo + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' ";
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
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - Add1.Length) / 2) + Add1);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - SecLine.Length) / 2) + SecLine);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "CHECK PRINT".Length) / 2) + "CHECK PRINT");
                        Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " WATER :" + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + "TABLE :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + "ORD NO:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine();
                        Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "QTY", "", "ITEM", "AMOUNT");
                        //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 17;
                    }
                    Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["AMOUNT"], "0.00"));
                    Vrowcount = Vrowcount + 1;
                    Total = Total + Convert.ToDouble(RData["AMOUNT"]);
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));

                sql = "SELECT A.taxdesc,SUM(T.TAXAMT) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) ";
                sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.KOTDETAILS = '" + KNo + "' GROUP BY A.taxdesc ";
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
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal, "0.00"));
                Double Rnd = Math.Round(Total + TaxTotal) - (Total + TaxTotal);
                Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
                BillTotal = Total + TaxTotal + Rnd;
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
                Filewrite.WriteLine();
                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thanak You Visit Again**".Length) / 2) + "**Thanak You Visit Again**");
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

        private void Cmd_ClearAll_Click(object sender, EventArgs e)
        {
            string DescVal = "";
            int rowindex = dataGridView2.CurrentRow.Index;
            if (dataGridView2.Rows[rowindex].Cells[0].Value != null) 
                {
                    DescVal = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
                    if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "NC" || DescVal == "REFUND" || DescVal == "ZOMATO" || DescVal == "PG")
                    {
                        dataGridView2.Rows.RemoveAt(rowindex);
                    }
                }
            Calculate();
        }

        private void Cmd_NotCharges_Click(object sender, EventArgs e)
        {
            int RowCnt;
            string DescVal = "";

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null) { DescVal = dataGridView2.Rows[i].Cells[0].Value.ToString(); }
                else { DescVal = ""; }
                if (DescVal == "INR" || DescVal == "VISA" || DescVal == "SWIGGY" || DescVal == "PAYTM" || DescVal == "AMEX" || DescVal == "CBILL" || DescVal == "ZOMATO" || DescVal == "PG" || DescVal == "PREPAID")
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
            }
        }

        public void FillDiscount()
        {
            int PHeight = 168;
            DataTable Btndt = new DataTable();
            if (GlobalVariable.gUserCategory == "S")
            {
                sql = "SELECT DISTINCT DISCPERCENT FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' ";
            }
            else
            {
                sql = "SELECT DISTINCT DISCPERCENT FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' AND USERNAME = '" + GlobalVariable.gUserName + "'";
            }
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0)
            {
                int X = 10;
                int Y = 15;
                //PHeight = (groupBox1.Height - 20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.YellowGreen;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 142;
                    btn.Height = PHeight;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Location = new Point(X, Y);
                    Grp_Discount.Controls.Add(btn);
                    btn.Click += new EventHandler(button_Disc_Click);
                    X = X + 142;
                    if (X > 430) { Y = Y + (PHeight + 10); X = 10; }
                }
            }
        }

        private void button_Disc_Click(object sender, EventArgs e)
        {
            Double Discper = 0;
            string ValDesc = "";
            Button selectedBtn = sender as Button;
            Discper = Convert.ToDouble(selectedBtn.Text.ToString());

            dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            DataTable TaxData = new DataTable();
            sql = "SELECT 'Total' As TDesc,sum(Amount)as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT 'DISC' As TDesc,-((sum(Amount)*" + Discper + ")/100) as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT 'Modifier CHG' As TDesc,sum(isnull(ModifierCharges,0)) - (sum(isnull(ModifierCharges,0))*" + Discper + ")/100 as Amount as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT 'OTH' As TDesc,sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0)) - (sum(isnull(packamount,0)+isnull(TipsAmt,0)+isnull(AdCgsAmt,0)+isnull(PartyAmt,0)+isnull(RoomAmt,0))*" + Discper + ")/100 as Amount as Amount FROM KOT_det WHERE KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' Union all ";
            sql = sql + " SELECT a.taxdesc As TDesc,sum(TAXAMT)-(sum(TAXAMT)*" + Discper + ")/100 as Amount FROM KOT_DET_TAX k,accountstaxmaster a WHERE k.TAXCODE = a.taxcode and KOTDETAILS = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And isnull(k.VOID,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' group by a.taxdesc";
            sql = sql + " Union ALL SELECT PAYMENTMODE,PAYAMOUNT FROM BillSettlement WHERE BILLNO = '" + CBillNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
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
            Calculate();
        }

        private void Cmd_Discount_Click(object sender, EventArgs e)
        {
            Grp_DenoINR.Visible = false;
            Grp_Cards.Visible = false;
            Grp_INR.Visible = false;
            Grp_Discount.Visible = true;
        }

        private void Button_ZOMATO_Click(object sender, EventArgs e)
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
                .Where(x => string.Format("{0}", x.FormattedValue) == "ZOMATO")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "ZOMATO";
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
                .Where(x => string.Format("{0}", x.FormattedValue) == "PG")
                .FirstOrDefault();
            if (cell != null)
            {
                this.dataGridView2.CurrentCell = cell;
            }
            else
            {
                RowCnt = dataGridView2.RowCount;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[RowCnt - 1].Cells[0].Value = "PG";
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

    }
}
