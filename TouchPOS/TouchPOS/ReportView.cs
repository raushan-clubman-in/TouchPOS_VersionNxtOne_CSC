using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

namespace TouchPOS
{
    public partial class ReportView : Form
    {
        public string ssql, TableName;
        public object Report;
        SqlConnection Myconn = new SqlConnection();

        public ReportView()
        {
            InitializeComponent();
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            GetDetails(ssql, TableName, Report);
        }

        private void GetDetails(string sqlstring, string TabName, object rpt) 
        {
            SqlCommand cmd = new SqlCommand(sqlstring, Myconn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, TabName);
            ReportDocument report = new ReportDocument();
            report.Load("" + GlobalVariable.appPath + "Reports\\" + rpt + "");
            report.SetDataSource(ds);
            crystalReportViewer1.ReportSource = report; 
        }

        private void GetDetails1(string ssql, string Tab, string rpt) 
        {
            
            SqlCommand cmd = new SqlCommand(ssql, Myconn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, Tab);
            ReportDocument report = new ReportDocument();
            report.Load("" + GlobalVariable.appPath + "Reports\\" + rpt + "");
            report.SetDataSource(ds);
            crystalReportViewer1.ReportSource = report; 
        }
    }
}
