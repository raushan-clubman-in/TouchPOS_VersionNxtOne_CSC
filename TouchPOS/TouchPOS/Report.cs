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
    public partial class Report : Form
    {
        static DataTable dt;
        static DataSet ds;
        GlobalClass gconn = new GlobalClass();
        static int minTime, maxTime;
        public Report()
        {
            InitializeComponent();
        }
          public string ssql, TableName;
        public Object Report1;
        SqlConnection Myconn = new SqlConnection();
       
       
        
      

        private void ReportView_Load(object sender, EventArgs e)
        {
            GetDetails(ssql, TableName, Report1);
        }
        public void OpenConnection()
        {
            ConnectionString css = new ConnectionString();
            try
            {
                Myconn.ConnectionString = css.ReadCS();
            }
            catch { }
        }

        public void GetDetails(string sqlstring, string TabName, Object rpt) 
        {
             dt = new DataTable();
            // GlobalVariable.gdataset = new DataSet();
           // SqlConnection Myconn = new SqlConnection();
            //GlobalClass gconn = new GlobalClass();
             if (sqlstring != "")
             {
                 OpenConnection();
                 SqlConnection conn = new SqlConnection(Myconn.ConnectionString);

                 SqlDataAdapter sda = new SqlDataAdapter(sqlstring, conn);

                 sda.SelectCommand.CommandTimeout = 100000;
                 sda.Fill(dt);
                 dt.TableName = TabName;
             }
             if (GlobalVariable.gdataset.Tables.Contains(TabName))
             {
                 GlobalVariable.gdataset.Tables.Remove(TabName);
             }
             GlobalVariable.gdataset.Tables.Add(dt);
            // rpt.SetDataSource(GlobalVariable.gdataset);
            // ////   // Dim report As New Report()
            // ////this.da
            // ////this.crystalReportViewer1.ReportSource = rpt;

            ////// ReportDocument report = new ReportDocument();
            //////report.Load("" + GlobalVariable.appPath  +"\\DailyReport.rpt " + "");
            ////// report.SetDataSource(ds);
            // crystalReportViewer1.ReportSource = rpt;
            // crystalReportViewer1.Zoom(100);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            getMinandMaxTime();
        }

        public  void getMinandMaxTime() {
            
            String sql;
            sql = "SELECT DATEPART(HOUR, isnull(min(billtime),0))as minhours,(DATEPART(HOUR, isnull(max(billtime),0))+1) as maxhours from Touchposwisesales";
            gconn.getDataSet1(sql, "Touchposwisesales");
            if (GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows.Count > 0)
            {
                minTime = GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows[0].Field<int>("minhours");
                maxTime = GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows[0].Field<int>("maxhours");
                if (maxTime > 24)
                {
                    maxTime = maxTime - 24;
                }


            }




        }

    //      public void GetDetails1(String  ssql ,String Tab , Object rpt )
    //{
       
    //        DataTable dt=new DataTable();
    //      OpenConnection();
    //        SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
    //    SqlDataAdapter adapter = new SqlDataAdapter(ssql, conn);
    //    adapter.SelectCommand.CommandTimeout = 100000;
    //    adapter.Fill(dt);
    //    dt.TableName = Tab;
    //    //If gdataset.Tables.Contains(Tab) = True Then
    //    //    gdataset.Tables.Remove(Tab)
    //    //End If
    //          DataSet ds=new DataSet();
    //    ds.Tables.Add(dt);
    //    rpt.setDataSource(ds);
    //         crystalReportViewer1.D
    //    crystalReportViewer1.ReportSource = rpt;
    //    crystalReportViewer1.Zoom(100);
    //}

        ////private void GetDetails1(string ssql, string Tab, string rpt) 
        ////{
            
        ////    SqlCommand cmd = new SqlCommand(ssql, Myconn);
        ////    cmd.CommandType = CommandType.StoredProcedure;
        ////    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        ////    DataSet ds = new DataSet();
        ////    sda.Fill(ds, Tab);
        ////    ReportDocument report = new ReportDocument();
        ////    report.Load("" + GlobalVariable.appPath + "Reports\\" + rpt + "");
        ////    report.SetDataSource(ds);
        ////    crystalReportViewer1.ReportSource = report; 
        ////}
    }
}
