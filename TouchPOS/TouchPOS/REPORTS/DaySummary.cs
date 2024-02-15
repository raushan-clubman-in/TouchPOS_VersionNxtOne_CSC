using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.REPORTS
{
    public partial class DaySummary : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DaySummary()
        {
            InitializeComponent();
        }

        private void DaySummary_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillPosLocations();
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
        }

        public void FillPosLocations()
        {
            String sqlstring;
            chklist_POSlocation.Items.Clear();
            int i;
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC FROM posmaster ";
            GCon.getDataSet1(sqlstring, "posmaster");
            if (GlobalVariable.gdataset.Tables["posmaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["posmaster"].Rows.Count; i++)
                {
                    chklist_POSlocation.Items.Add(GlobalVariable.gdataset.Tables["posmaster"].Rows[i].Field<String>("POSDESC").Trim());
                }
            }
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            CRYSTAL.DaySummary RPS = new CRYSTAL.DaySummary();
            POSNAME = "";

            sqlstring = "Exec Pos_SaleDetails '" + dtp1.Value.ToString("dd-MMM-yyyy") + "','" + dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(sqlstring);
            sqlstring = "EXEC GROUPWISESALE '" + this.dtp1.Value.ToString("dd-MMM-yyyy") + "','" + this.dtp2.Value.ToString("dd-MMM-yyyy") + "','N'";
            GCon.ExecuteStoredProcedure(sqlstring);

            //sqlstring = " select PosDesc,sum(ItemTotal+OthChargs) as ItemTotal,sum(Tax) as Tax,Sum(Adjust) as Adjust,Sum(ExtraTips) as ExtraTips,Sum(Discount) as Discount from SaleDetails  ";
            sqlstring = "select CATEGORY as PosDesc,sum(ItemTotal) as ItemTotal,sum(TaxTotal) as Tax,Sum(Adjust) as Adjust,Sum(Tips) as ExtraTips,Sum(Discount) as Discount from DaySummary where ";
            sqlstring = sqlstring + " KOTDATE Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' and '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' ";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " And POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            sqlstring = sqlstring + " Group by CATEGORY ORDER BY CATEGORY ";
            GCon.getDataSet1(sqlstring, "SaleDetails");
            if (GlobalVariable.gdataset.Tables["SaleDetails"].Rows.Count > 0)
            {
                string sql = "";
                sql = " select PAYMENTMODE,sum(BALANCEAMOUNT) as BALANCEAMOUNT, sum(PAYAMOUNT) as PAYAMOUNT from (select PAYMENTMODE,COUNT(PAYMENTMODE) AS BALANCEAMOUNT,SUM(PAYAMOUNT) AS PAYAMOUNT from BillSettlement ";
                sql = sql + " WHERE BILLNO IN (SELECT billdetails FROM DaySummary) GROUP BY PAYMENTMODE union all select 'INR',0,isnull(-RefundAmt,0) from bill_hdr where isnull(RefundAmt,0) > 0 and billdetails IN (SELECT billdetails FROM DaySummary) ) Pay group by PAYMENTMODE ";
                GCon.getDataSet1(sql, "BillSettlement");
                if (GlobalVariable.gdataset.Tables["BillSettlement"].Rows.Count > 0)
                {
                    rv.GetDetails(sql, "BillSettlement", RPS);
                    RPS.SetDataSource(GlobalVariable.gdataset);
                    rv.crystalReportViewer1.ReportSource = RPS;
                }

                int Cover = Convert.ToInt32(GCon.getValue("select Isnull(sum(Covers),0) as Covers from Kot_Hdr where Kotdetails in (select KOTDETAILS from Kot_Det Where billdetails IN (select billdetails from SaleDetails))"));
                int VoidCheck = Convert.ToInt32(GCon.getValue("select count(*) as VoidCheck from Kot_Hdr where Kotdetails in (select KOTDETAILS from Kot_Det Where billdetails IN (select billdetails from SaleDetails)) and isnull(DelFlag,'') = 'Y' "));

                rv.GetDetails(sqlstring, "SaleDetails", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text15"];
                TXTOBJ1.Text = "Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text14"];
                TXTOBJ3.Text = GlobalVariable.gCompanyName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
                TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text17"];
                TXTOBJ4.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "Business Date " + Strings.Format((DateTime)GlobalVariable.ServerDate, "yyyy-MM-dd") + " ";

                RPS.DataDefinition.FormulaFields["UnboundNumber1"].Text = Cover.ToString();

                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Chk_POSlocation.Checked = false;
            FillPosLocations();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        private void Chk_POSlocation_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_POSlocation.Checked == true)
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, false);
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
