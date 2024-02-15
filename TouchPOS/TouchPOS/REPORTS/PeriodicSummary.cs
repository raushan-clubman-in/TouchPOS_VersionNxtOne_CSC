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
    public partial class PeriodicSummary : Form
    {
        GlobalClass GCon = new GlobalClass();

        public PeriodicSummary()
        {
            InitializeComponent();
        }

        private void PeriodicSummary_Load(object sender, EventArgs e)
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            Chk_POSlocation.Checked = false;
            FillPosLocations();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring,sql1;
            string HNAME, POSNAME, Catname;
            Double PendingAmount = 0;
            Int32 UnsettledTable = 0;
            Report rv = new Report();
            CRYSTAL.Rpt_PeriodicSumm RPS = new CRYSTAL.Rpt_PeriodicSumm();
            POSNAME = "";
            sqlstring = " Select OrderSeq,GType,CATEGORY,Sum(Debit) as Debit,Sum(Credit) as Credit From PeriodicSummary Where Kotdate between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' ";

            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
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
            sqlstring = sqlstring + " Group by OrderSeq,GType,CATEGORY Order by 1 ";

            GCon.getDataSet1(sqlstring, "PeriodicSummary");
            if (GlobalVariable.gdataset.Tables["PeriodicSummary"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "PeriodicSummary", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);

                sql1 = "SELECT ISNULL(SUM(ISNULL(AMOUNT,0)+ISNULL(TAXAMOUNT,0)+ISNULL(PACKAMOUNT,0)+ISNULL(TIPSAMT,0)+ISNULL(ADCGSAMT,0)+ISNULL(ModifierCharges,0)),0) FROM KOT_DET ";
                sql1 = sql1 + " WHERE Cast(Convert(varchar(11),kotdate,106) as Datetime) Between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' AND isnull(billdetails,'') = '' AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(DELFLAG,'') <> 'Y'";
                PendingAmount = Convert.ToDouble(GCon.getValue(sql1));

                sql1 = "SELECT ISNULL(COUNT(*),0) From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) Between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'  And Isnull(Kotdetails,'') in (SELECT ISNULL(KOTDETAILS,'') FROM KOT_DET ";
                sql1 = sql1 + " WHERE Cast(Convert(varchar(11),kotdate,106) as Datetime) Between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' AND isnull(billdetails,'') = '' AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(DELFLAG,'') <> 'Y') ";
                UnsettledTable = Convert.ToInt32(GCon.getValue(sql1));


                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text7"];
                TXTOBJ1.Text = "Periodic Summary Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ3.Text = GlobalVariable.gCompanyName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
                TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ4.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text5"];
                TXTOBJ5.Text = "Business Date " + Strings.Format((DateTime)GlobalVariable.ServerDate, "yyyy-MM-dd") + " ";

                RPS.DataDefinition.FormulaFields["UnboundNumber1"].Text = PendingAmount.ToString();
                RPS.DataDefinition.FormulaFields["UnboundNumber2"].Text = UnsettledTable.ToString();

                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }

        }
    }
}
