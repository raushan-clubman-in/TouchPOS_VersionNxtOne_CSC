using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office;

namespace TouchPOS.REPORTS
{
    public partial class ReportFilter : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string RName = "";
        public string ReportNode = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ReportFilter()
        {
            InitializeComponent();
        }

        string sql = "";

        private void ReportFilter_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            ReportName.Text = RName;
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);

            GlobalClass.myGroupBox myGroupBox3 = new GlobalClass.myGroupBox();
            myGroupBox3.Text = "";
            myGroupBox3.BorderColor = Color.Black;
            myGroupBox3.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox3);
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            if (ReportNode == "Node_OpenCheck") 
            {
                DataTable BillData = new DataTable();
                sql = "SELECT KotDetails,KotDate,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' and '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' ";
                sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' and '" + dtp2.Value.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
                BillData = GCon.getDataSet(sql);
                if (BillData.Rows.Count > 0) 
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = BillData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = BillData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = BillData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_TaxExemptedCheck") 
            {
                DataTable TaxExpData = new DataTable();
                sql = " SELECT B.BillDetails,B.BillDate,SUM(AMOUNT) AS BasicAmount, B.AddUserId AS UserName,B.Remarks As UserRemarks FROM KOT_DET D,BILL_HDR B WHERE D.BILLDETAILS = B.BillDetails And D.FinYear = B.FinYear AND ISNULL(ExemptTaxFlag,'') = 'Y' AND ISNULL(D.DelFlag,'') <> 'Y' ";
                sql = sql + " AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.BILLDETAILS,'') <> '' And BillDate Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' Group by B.BillDetails,B.BillDate,B.AddUserId,B.Remarks Order by 2,1 ";
                TaxExpData = GCon.getDataSet(sql);
                if (TaxExpData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = TaxExpData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = TaxExpData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = TaxExpData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_SCExemptedCheck")
            {
                DataTable TaxExpData = new DataTable();
                sql = " SELECT B.BillDetails,B.BillDate,SUM(AMOUNT) AS BasicAmount,tips AS SCPercent,SUM((AMOUNT*tips)/100) as SCAmount, B.AddUserId AS UserName,B.Remarks As UserRemarks FROM KOT_DET D,BILL_HDR B,PosMaster P WHERE D.BILLDETAILS = B.BillDetails And D.FinYear = B.FinYear AND  D.POSCODE = P.POSCode AND ISNULL(WaiveSCGFlag,'') = 'Y' AND ISNULL(D.DelFlag,'') <> 'Y' ";
                sql = sql + " AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.BILLDETAILS,'') <> '' And BillDate Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' Group by B.BillDetails,B.BillDate,B.AddUserId,B.Remarks,tips Order by 2,1 ";
                TaxExpData = GCon.getDataSet(sql);
                if (TaxExpData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = TaxExpData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = TaxExpData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = TaxExpData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_DiscByItem")
            {
                DataTable DiscItemData = new DataTable();
                sql = " SELECT ItemDesc,Sum(Qty) as Qty,Rate,Sum(Amount) As Amount,Isnull(ItemDiscPerc,0) as DiscPerc,Sum(((Amount*Isnull(ItemDiscPerc,0))/100)) DiscAmount FROM KOT_DET D,BILL_HDR B WHERE D.BILLDETAILS = B.BillDetails And D.FinYear = B.FinYear AND Isnull(ItemDiscPerc,0) > 0 AND ISNULL(D.DelFlag,'') <> 'Y'  ";
                sql = sql + " AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.BILLDETAILS,'') <> '' And Cast(Convert(Varchar(11),Kotdate,106) as Datetime) Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' Group by ItemDesc,Rate,Isnull(ItemDiscPerc,0) Order by 1,5 ";
                DiscItemData = GCon.getDataSet(sql);
                if (DiscItemData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = DiscItemData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = DiscItemData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = DiscItemData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_DiscByClint")
            {
                DataTable DiscClientData = new DataTable();
                sql = " SELECT B.BillDetails,Cast(Convert(Varchar(11),Kotdate,106) as Datetime) as BillDate,MCode = Case When B.MCode = '' then 'Walk-In' else B.MCode End,Mname = Case When B.Mname = '' then 'Walk-In' else B.Mname End,ItemDesc,Sum(Amount) As Amount,Isnull(ItemDiscPerc,0) as DiscPerc,Sum(((Amount*Isnull(ItemDiscPerc,0))/100)) DiscAmount,Isnull(DiscUser,'') as DiscUser  ";
                sql = sql + " FROM KOT_DET D,BILL_HDR B WHERE D.BILLDETAILS = B.BillDetails And D.FinYear = B.FinYear AND Isnull(ItemDiscPerc,0) > 0 AND ISNULL(D.DelFlag,'') <> 'Y' AND ISNULL(D.KOTSTATUS,'') <> 'Y' AND ISNULL(D.BILLDETAILS,'') <> '' And Cast(Convert(Varchar(11),Kotdate,106) as Datetime) Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' ";
                sql = sql + " Group by B.MCode,B.Mname,ItemDesc,Isnull(ItemDiscPerc,0),Cast(Convert(Varchar(11),Kotdate,106) as Datetime),B.BillDetails,Isnull(DiscUser,'') Order by 2,1,5 ";
                DiscClientData = GCon.getDataSet(sql);
                if (DiscClientData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = DiscClientData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = DiscClientData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = DiscClientData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_DuplicateBill")
            {
                DataTable DiscClientData = new DataTable();
                sql = " select B.BillNo,H.BillDate,B.UserName,B.TakenDate from [BillDuplicatePrint] B,BILL_HDR H where B.BillNo = H.BillDetails and H.FinYear = '" + FinYear1 + "' and Cast(Convert(Varchar(11),BillDate,106) as Datetime) Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' Order by 1 ";
                DiscClientData = GCon.getDataSet(sql);
                if (DiscClientData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = DiscClientData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = DiscClientData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = DiscClientData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            else if (ReportNode == "Node_GuestPhone")
            {
                DataTable DiscClientData = new DataTable();
                sql = " SELECT Kotdetails,T.MobileNo,GuestName,ItemDesc,Qty FROM KOT_DET D,Tbl_HomeTakeAwayBill T WHERE D.KOTDETAILS = T.KotNo AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' And Cast(convert(varchar(11),kotdate,106) as datetime) between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' And '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' ORDER BY KOTDETAILS ";
                DiscClientData = GCon.getDataSet(sql);
                if (DiscClientData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = DiscClientData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;

                    //set DGV's column names and headings from the Datatable properties
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = DiscClientData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = DiscClientData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    dataGridView1.Refresh();
                }
            }
            dataGridView1.ReadOnly = true;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }


        private void Cmd_Export_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] = RName + " Report Between " + " " + dtp1.Value.ToString("dd-MMM-yyyy") + " And  " + dtp2.Value.ToString("dd-MMM-yyyy") ;
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[2, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 3, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 3, j + 1] = "";
                    }
                }
            }

            //copyAlltoClipboard();
            //Microsoft.Office.Interop.Excel.Application xlexcel;
            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;
            //xlexcel = new Microsoft.Office.Interop.Excel.Application();
            //xlexcel.Visible = true;
            //xlWorkBook = xlexcel.Workbooks.Add(misValue);
            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            //CR.Select();
            //xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);  
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
    }
}
