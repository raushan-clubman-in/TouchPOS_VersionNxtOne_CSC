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
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;


namespace TouchPOS.REPORTS
{


    public partial class POSACCOUNTSSETTLEMENET : Form
    {
        GlobalClass GCon = new GlobalClass();
       
        public POSACCOUNTSSETTLEMENET()
        {

            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        string itemcode;
        string vseqno, gvseqno;
        string ssql = "";


        private void NCCHECKSITEMWISE_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            fillpos();
          //  fillMember();
            //fillncchecks();
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void fillpos()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,upper(ISNULL(POSDESC,'')) AS POSDESC,ISNULL(POSSEQNO,0) AS POSSEQNO FROM POSMaster WHERE ISNULL(Freeze,'') <> 'Y'  ORDER BY POSCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                POS_LIST.Items.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    POS_LIST.Items.Add(dt.Rows[i].ItemArray[1].ToString());

                }
            }
        }

     

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j <= POS_LIST.Items.Count-1; j++)
                {
                    POS_LIST.SetItemChecked(j, true);

                }
            }
            else
                if (checkBox1.Checked == false)
                {
                    for (int j = 0; j <= POS_LIST.Items.Count-1; j++)
                    {
                        POS_LIST.SetItemChecked(j, false);

                    }
                }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        





        public void MEMBERWISESUMMARYWD()
    {
        string[] MemberCode = null;
        int i ;
        String sqlstring, SSQL, SSQL3 ;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CRYSTAL.MEMBERWISEDETAILS_SUMMARYWD r = new CRYSTAL.MEMBERWISEDETAILS_SUMMARYWD();



            sqlstring = "SELECT MNAME,MCODE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy")+ "' AND '" +Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy")+ "'";


                
            if( POS_LIST.CheckedItems.Count != 0 )
            {

                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LIST.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LIST.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                
            }
            else
            {
                
                MessageBox.Show("Select the POS Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            

              
        
        sqlstring = sqlstring + "GROUP BY MNAME,MCODE ORDER BY MNAME,MCODE";
     
            
        SSQL3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
        SSQL3 = SSQL3 + " where kotdate between'";
        SSQL3 = SSQL3 + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
        if (POS_LIST.CheckedItems.Count != 0)
        {

            SSQL3 = SSQL3 + " AND POSDESC IN (";
            for (i = 0; i < POS_LIST.CheckedItems.Count - 1; i++)
            {
                SSQL3 = SSQL3 + " '" + POS_LIST.CheckedItems[i] + "', ";
            }
            SSQL3 = SSQL3.Remove(SSQL3.Length - 2);
            SSQL3 = SSQL3 + ")";

        }
        else
        {

            MessageBox.Show("Select the POS Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }



     

        SSQL3 = SSQL3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";

            GCon.getDataSet1(sqlstring,"POSWISESALESUMMARY");
            GCon.getDataSet1(SSQL3,"NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(SSQL3, "NANO_TAXGROUPINGSUMMERY", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp1.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp2.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }



            else
            {
                MessageBox.Show("NO RECORDS TO DISPLAY", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }


        public void REVENUE()
        {
            string[] MemberCode = null;
            int i;
            String sqlstring;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
       //     TextObject txtobj1, TXTOBJ10;
            CRYSTAL.Cry_accountssettlement r = new CRYSTAL.Cry_accountssettlement();


            sqlstring = "SELECT * FROM Pos_Accountssettelment ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,Billdate,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";



            if (POS_LIST.CheckedItems.Count != 0)
            {

                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LIST.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LIST.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {

                MessageBox.Show("Select the POS Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GCon.getDataSet1(sqlstring, "Pos_Accountssettelment");

            if (GlobalVariable.gdataset.Tables["Pos_Accountssettelment"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "Pos_Accountssettelment", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text9"];
                TXTOBJ16.Text = "Period From " + dtp1.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp2.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text11"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
       else
            {
                MessageBox.Show("NO RECORDS TO DISPLAY", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Boolean Checkdaterangevalidate(DateTime Startdate, DateTime Enddate)
        {
            GlobalVariable.chkdatevalidate = true;
            if ((Enddate.Date - DateTime.Now.Date).Days < 0)
            {
                MessageBox.Show("To Date cannot be greater than Current Date");
                GlobalVariable.chkdatevalidate = false;
                return GlobalVariable.chkdatevalidate;
            }
            if ((Enddate.Date - Startdate.Date).Days < 0)
            {
                MessageBox.Show("From Date cannot be greater than To Date");
                GlobalVariable.chkdatevalidate = false;
                return GlobalVariable.chkdatevalidate;
            }
            return GlobalVariable.chkdatevalidate;
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            if (POS_LIST.CheckedItems.Count == 0)                 
            {
                MessageBox.Show("Select the Location(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
           
            Checkdaterangevalidate(dtp1.Value, dtp2.Value);
            String SSQL;
            SSQL = "EXEC Pos_Accounts_settelment '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            dt = GCon.getDataSet(SSQL);
            REVENUE();
                  }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox1_CheckedChanged(sender, e);
         }

    }
}
