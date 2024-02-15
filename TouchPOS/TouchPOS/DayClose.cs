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
    public partial class DayClose : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly ServiceType _form1;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public DayClose(ServiceType form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable PenKot = new DataTable();
        DataTable CheckDiscPerc = new DataTable();
        DataTable CheckDiffDebitOne = new DataTable();
        Boolean BoolDayClose = true;
        Double TotDeb = 0, TotCre = 0;

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            TotDeb = 0;
            TotCre = 0;
            int i = 0;
            BoolDayClose = true;
            sql = "SELECT KotDetails,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
            PenKot = GCon.getDataSet(sql);
            if (PenKot.Rows.Count > 0) 
            {
                BoolDayClose = false;
                PendingInfo PI = new PendingInfo(this);
                PI.FillData = PenKot;
                PI.datatype = "Pending KOT";
                PI.ShowDialog();
                if (BoolDayClose == false) { return; }
            }

            sql = "select TType1 as ForType,ITEMCODE as OnCode from KotAccountCheck Where Isnull(AcountCode,'') = '' And KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' group by TType1,ITEMCODE,AcountCode Order by 1";
            PenKot = GCon.getDataSet(sql);
            if (PenKot.Rows.Count > 0)
            {
                BoolDayClose = false;
                PendingInfo PI = new PendingInfo(this);
                PI.FillData = PenKot;
                PI.datatype = "Missing Account";
                PI.ShowDialog();
                if (BoolDayClose == false) { return; }
            }

            sql = " SELECT * FROM KOT_DET WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(ItemDiscAmt,0) > 0 AND ISNULL(ItemDiscPerc,0) = 0 AND FinYear = '" + FinYear1 + "' ";
            CheckDiscPerc = GCon.getDataSet(sql);
            if (CheckDiscPerc.Rows.Count > 0)
            {
                List.Clear();
                sqlstring = " Update KOT_DET set ItemDiscPerc = Round(((ItemDiscAmt * 100) /AMOUNT),2) WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(ItemDiscAmt,0) > 0 AND ISNULL(ItemDiscPerc,0) = 0 AND FinYear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                }
            }

            sql = " SELECT BILLDETAILS,SUM(DEBIT),SUM(CREDIT) FROM CHECKPERODIC WHERE KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' GROUP BY BILLDETAILS HAVING (SUM(DEBIT)-SUM(CREDIT)) >= 1 AND (SUM(DEBIT)-SUM(CREDIT)) <= 2 ";
            CheckDiffDebitOne = GCon.getDataSet(sql);
            if (CheckDiffDebitOne.Rows.Count > 0)
            {
                List.Clear();
                sqlstring = " UPDATE BILL_HDR SET ExtraTips = 0.01 WHERE BillDetails IN (SELECT BILLDETAILS FROM CHECKPERODIC WHERE KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' GROUP BY BILLDETAILS HAVING (SUM(DEBIT)-SUM(CREDIT)) >= 1 AND (SUM(DEBIT)-SUM(CREDIT)) <= 2) AND FinYear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                }
            }

            List.Clear();
            sqlstring = sqlstring = "EXEC PROC_AUDITTRIAL '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "'";
            List.Add(sqlstring);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
            DataTable Final = new DataTable();
            //sql = "Select AcountCode,acdesc,'CREDIT' = CASE WHEN CREDITDEBIT = 'CREDIT' THEN AMOUNT ELSE 0 END,'DEBIT' = CASE WHEN CREDITDEBIT = 'DEBIT' THEN AMOUNT ELSE 0 END,CREDITDEBIT from POSACCOUNTTOBEPOST ORDER BY 5";
            sql = " Select AcountCode,acdesc,'CREDIT' = CASE WHEN CREDITDEBIT = 'CREDIT' THEN SUM(AMOUNT) ELSE 0 END,'DEBIT' = CASE WHEN CREDITDEBIT = 'DEBIT' THEN SUM(AMOUNT) ELSE 0 END,CREDITDEBIT from POSACCOUNTTOBEPOST GROUP BY AcountCode,acdesc,CREDITDEBIT  ORDER BY 5 ";
            Final = GCon.getDataSet(sql);
            if (Final.Rows.Count > 0) 
            {
                
                for (i = 0; i < Final.Rows.Count; i++) 
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Final.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = Final.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(Final.Rows[i].ItemArray[2]);
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(Final.Rows[i].ItemArray[3]);
                    if (Convert.ToString(Final.Rows[i].ItemArray[4]) == "CREDIT") 
                    {
                        TotCre = TotCre + Convert.ToDouble(Final.Rows[i].ItemArray[2]);
                        //TotCre = TotCre + Convert.ToDouble(String.Format("{0:0.##}", Final.Rows[i].ItemArray[2]));
                    }
                    else if (Convert.ToString(Final.Rows[i].ItemArray[4]) == "DEBIT")
                    {
                        TotDeb = TotDeb + Convert.ToDouble(Final.Rows[i].ItemArray[3]);
                        //TotDeb = TotDeb + Convert.ToDouble(String.Format("{0:0.##}", Final.Rows[i].ItemArray[3]));
                    }
                }
                TotCre = Math.Round(TotCre, 2);
                TotDeb = Math.Round(TotDeb, 2);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = "Total";
                dataGridView1.Rows[i].Cells[2].Value = TotCre;
                dataGridView1.Rows[i].Cells[3].Value = TotDeb;
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Cmd_Confirm.Visible = true;
                Cmd_Processed.Enabled = false;
            }
            else if (Final.Rows.Count == 0 && BoolDayClose == true) 
            {
                Cmd_Confirm.Visible = true;
                Cmd_Processed.Enabled = false;
            }
        }

        private void DayClose_Load(object sender, EventArgs e)
        {
            Cmd_Confirm.Visible = false;
            label2.Text = "Process Date :" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Confirm_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string Accode = "", Accdesc = "";
            if (TotDeb != TotCre) 
            {
                MessageBox.Show("DEbit/Credit Not Matching Try again");
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    Accode = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
                else { Accode = ""; }
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    Accdesc = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
                else { Accdesc = ""; }
                if (Accdesc != "Total" && (Accode == "" || Accdesc == "")) 
                {
                    MessageBox.Show("Account Code Missing or Not Valid");
                    return;
                }
            }
            List.Clear();
            sqlstring = sqlstring = "EXEC PROC_POSTAUDITTRIAL '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "'";
            List.Add(sqlstring);
            sqlstring = sqlstring = "EXEC DAYENDPOSfinal '" + GlobalVariable.gUserName + "'";
            List.Add(sqlstring);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
            GlobalVariable.ServerDate = Convert.ToDateTime(GCon.getValue("SELECT Isnull(BillCloseDate,'') FROM POSSETUP"));
            GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(1);
            MessageBox.Show("Night Audit Completed Successfully");
            this.Close();
        }
    }
}
