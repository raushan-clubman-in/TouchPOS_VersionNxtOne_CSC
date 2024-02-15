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
    public partial class DailyTransactionRpt : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DailyTransactionRpt()
        {
            InitializeComponent();
        }

        private void DailyTransactionRpt_Load(object sender, EventArgs e)
        {

        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;
            String Sqlstring1="";
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            CRYSTAL.POS_DailyTransReport CO = new CRYSTAL.POS_DailyTransReport();

            sqlstring = "Exec Pos_DailyTransactionReport '" + dtp1.Value.ToString("dd-MMM-yyyy") + "','" + dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(sqlstring);

            sqlstring = "Select * from POS_DailyTransReport Where BillDate Between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' Order by BILLDETAILS,ITEMDESC ";
            GCon.getDataSet1(sqlstring, "POS_DailyTransReport");
            Sqlstring1 = "Select * from POS_DailyTransReportSettlement Where BillDate Between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' Order by BILLNO ";
            if (GlobalVariable.gdataset.Tables["POS_DailyTransReport"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POS_DailyTransReport", CO);
                rv.GetDetails(Sqlstring1, "POS_DailyTransReportSettlement", CO);
                CO.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = CO;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)CO.ReportDefinition.ReportObjects["Text17"];
                TXTOBJ1.Text = "Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)CO.ReportDefinition.ReportObjects["Text15"];
                TXTOBJ3.Text = GlobalVariable.gCompanyName;
                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {

        }
    }
}
