using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchPOS.CRYSTAL;

namespace TouchPOS.REPORTS
{
    public partial class SETTLEMENT : Form
    {
       public static String ssql;
       GlobalClass GCON = new GlobalClass();

       public SETTLEMENT()
        {
            InitializeComponent();
        }

       string sqlstring = "";

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SETTLEMENT_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            fillUsernames();
            fillPosLocations();
            fillpaymentmode();
            PAYMENTWISE.Checked = true;

            dtp_fromdate.Value = DateTime.Now;
            dtp_todate.Value = DateTime.Now;
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void fillUsernames()
        {
        
            int i;
            USER_LISTBOX.Items.Clear();
            ssql = "SELECT DISTINCT ISNULL(adduserid,'')AS adduserid  FROM kot_DET  ORDER BY adduserid";
            GCON.getDataSet1(ssql, "Users");
            if (GlobalVariable.gdataset.Tables["Users"].Rows.Count>0) {
                for (i = 0; i < GlobalVariable.gdataset.Tables["Users"].Rows.Count ; i++)
                {
                    USER_LISTBOX.Items.Add(GlobalVariable.gdataset.Tables["Users"].Rows[i].Field<String>("adduserid").Trim());
                }
            }
            USER_LISTBOX.Sorted = true;
        }
        private void fillPosLocations()
        {
                   int i;
            POS_LISTBOX.Items.Clear();
            if(GlobalVariable.gUserCategory!="S")
            {
               // ssql = "SELECT DISTINCT poscode,posdesc FROM posmaster ";
                ssql = "SELECT DISTINCT poscode,posdesc FROM posmaster Where poscode in (SELECT poscode FROM POS_USERCONTROL where username='" + GlobalVariable.gUserName + "') ";
                          }
            else
            {
                ssql = "SELECT DISTINCT poscode,posdesc FROM posmaster";
            }
            GCON.getDataSet1(ssql, "PosMaster");
            if (GlobalVariable.gdataset.Tables["PosMaster"].Rows.Count > 0) {
                for (i = 0; i < GlobalVariable.gdataset.Tables["PosMaster"].Rows.Count ; i++)
                {
                   POS_LISTBOX.Items.Add(GlobalVariable.gdataset.Tables["PosMaster"].Rows[i].Field<String>("posdesc").Trim());
                }//for
            }//if
            POS_LISTBOX.Sorted=true;

           
        }
        private void fillpaymentmode()
        {
                   int i;
            PAYMENT_LISTBOX.Items.Clear();
            ssql = "SELECT DISTINCT PAYMENTCODE,PAYMENTNAME FROM PAYMENTMODEMASTER ";
            GCON.getDataSet1(ssql, "PAYMENTMODEMASTER");
            if (GlobalVariable.gdataset.Tables["PAYMENTMODEMASTER"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["PAYMENTMODEMASTER"].Rows.Count ; i++)
                {
                    PAYMENT_LISTBOX.Items.Add(GlobalVariable.gdataset.Tables["PAYMENTMODEMASTER"].Rows[i].Field<String>("PAYMENTCODE").Trim());
                }
                PAYMENT_LISTBOX.Sorted = true;
            }


        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            POS_LISTBOX.Items.Clear();
            USER_LISTBOX.Items.Clear();
            PAYMENT_LISTBOX.Items.Clear();
            Chk_SETTLEMENT.Checked = false;
            
            USERWISE.Checked = false;
            PAYMENTWISE.Checked = false;
            
            chkbox_withdaybrkup.Checked = false;
            fillUsernames();
            fillPosLocations();
            fillpaymentmode();
            PAYMENTWISE.Checked = true;
            dtp_fromdate.Value = DateTime.Now;
            dtp_todate.Value = DateTime.Now;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            if(POS_LISTBOX.CheckedItems.Count==0 && USER_LISTBOX.CheckedItems.Count==0 ){
                if (POS_LISTBOX.CheckedItems.Count == 0) {
                    MessageBox.Show("Select Pos Locations");
                    return;

                }
                if (USER_LISTBOX.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select Users ");
                    return;
                }
            }      
            else if (POS_LISTBOX.CheckedItems.Count==0 && PAYMENT_LISTBOX.CheckedItems.Count==0)
            {
                if (POS_LISTBOX.CheckedItems.Count == 0) {
                    MessageBox.Show("Select Pos Locations");
                    return;
                }
                if(PAYMENT_LISTBOX.CheckedItems.Count==0){
                    MessageBox.Show("select Bearers");
                    return;
                }
            }
            Checkdaterangevalidate(dtp_fromdate.Value, dtp_todate.Value);
            if (GlobalVariable.chkdatevalidate == false)
            { return; }

            if (Chk_SETTLEMENT.Checked == true || Chk_SETTLEMENT.Checked == false)
            {
              
               if ( PAYMENTWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    if (Chk_WithoutNC.Checked == true)
                    {
                        sqlstring = "DELETE FROM poswisesales WHERE BILLDETAILS IN (SELECT BILLNO FROM BillSettlement  WHERE PAYMENTMODE = 'NC' AND BILLDATE BETWEEN '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "')";
                        GCON.ExecuteStoredProcedure(sqlstring);
                    }
                    SETTLEMENTPAMENTMODEWISE1();
                    return;
                }
                else if ( PAYMENTWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    if (Chk_WithoutNC.Checked == true)
                    {
                        sqlstring = "DELETE FROM poswisesales WHERE BILLDETAILS IN (SELECT BILLNO FROM BillSettlement  WHERE PAYMENTMODE = 'NC' AND BILLDATE BETWEEN '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "')";
                        GCON.ExecuteStoredProcedure(sqlstring);
                    }
                    SETTLEMENTPAMENTMODEWISEWDB();
                    return;
                }
                else if ( USERWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    if (Chk_WithoutNC.Checked == true)
                    {
                        sqlstring = "DELETE FROM poswisesales WHERE BILLDETAILS IN (SELECT BILLNO FROM BillSettlement  WHERE PAYMENTMODE = 'NC' AND BILLDATE BETWEEN '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "')";
                        GCON.ExecuteStoredProcedure(sqlstring);
                    }
                    SETTLEMENTUSERWISE();
                    return;
                }
                else if ( USERWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    if (Chk_WithoutNC.Checked == true)
                    {
                        sqlstring = "DELETE FROM poswisesales WHERE BILLDETAILS IN (SELECT BILLNO FROM BillSettlement  WHERE PAYMENTMODE = 'NC' AND BILLDATE BETWEEN '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "')";
                        GCON.ExecuteStoredProcedure(sqlstring);
                    }
                    SETTLEMENTUSERWISEWDB();
                    return;
                }
            }
        }
        public void SETTLEMENTPAMENTMODEWISE1()
        {

            sqlstring = "Exec SETTLEPAYWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            GCON.ExecuteStoredProcedure(sqlstring);

            if (Chk_WithoutNC.Checked == true)
            {
                sqlstring = "DELETE FROM TEMPVIEW_SETTLEMENTBILLDETAILS1 WHERE BILLNO IN (SELECT BILLNO FROM BillSettlement  WHERE PAYMENTMODE = 'NC' AND BILLDATE BETWEEN '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "')";
                GCON.ExecuteStoredProcedure(sqlstring);
            }

            //String sql16 = "select * from VIEW_SETTLEMENTBILLDETAILS1 WHERE";
            String sql16 = "select * from TEMPVIEW_SETTLEMENTBILLDETAILS1 WHERE";
            sql16 = sql16 + " CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sql16 = sql16 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
          
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sql16 = sql16 + " and ADDUSERID in (";
                for (int i = 0; i < USER_LISTBOX.CheckedItems.Count ; i++)
                {
                    sql16 = sql16 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sql16 = sql16.Remove(sql16.Length - 2);
                sql16 = sql16 + ")";
            }

            GCON.getDataSet1(sql16, "VIEW_SETTLEMENTBILLDETAILS1");
            
            Report rv = new Report();
            Cry_SettlementDetails r = new Cry_SettlementDetails();


            rv.GetDetails(sql16, "VIEW_SETTLEMENTBILLDETAILS1", r);

            r.SetDataSource(GlobalVariable.gdataset);

            rv.crystalReportViewer1.ReportSource = r;
            rv.crystalReportViewer1.Zoom(100);
        
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text15"];
            TXTOBJ1.Text = GlobalVariable.gCompanyName;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
            TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text13"];
            TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text17"];
            TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;



            rv.Show();
        }
        public void POSDETAILPOSWISEWDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSPOSWISEWDB r = new POSWISEDETAILSPOSWISEWDB();

            sqlstring = "SELECT  POSDESC,MNAME,BILLDetails,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            
            sqlstring = sqlstring + "GROUP BY POSDESC,MNAME,BILLDetails,BILLDATE ORDER BY POSDESC,MNAME,BILLDetails,BILLDATE";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void POSDETAILPOSWISEWD()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSPOSWISEWD r = new POSWISEDETAILSPOSWISEWD();

            sqlstring = "SELECT POSDESC,MNAME,billdetails,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    sqlstring = sqlstring + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            ////if (USER_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    sqlstring = sqlstring + " and ADDUSERID in (";
            ////    for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            sqlstring = sqlstring + "GROUP BY POSDESC,MNAME,billdetails,BILLDATE ORDER BY POSDESC,MNAME,billdetails,BILLDATE";


            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    ssql3 = ssql3 + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            ////if (USER_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    ssql3 = ssql3 + " and ADDUSERID in (";
            ////    for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void POSDETAILSBEARERWISEDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSBEARERWISEWDB r = new POSWISEDETAILSBEARERWISEWDB();

            sqlstring = "SELECT  POSDESC,MNAME,BILLDETAILS,SERVERNAME,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            ////if (POS_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    sqlstring = sqlstring + " AND POSDESC IN (";
            ////    for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and SERVERNAME in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,MNAME,SERVERNAME,BILLDATE,BILLDETAILS ORDER BY POSDESC,MNAME,SERVERNAME,BILLDATE,BILLDETAILS";


            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            ////if (POS_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    ssql3 = ssql3 + " AND POSDESC IN (";
            ////    for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                ssql3 = ssql3 + " and SERVERNAME in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void POSDETAILSBEARERWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSBEARERWISEWD r = new POSWISEDETAILSBEARERWISEWD();

            sqlstring = "SELECT POSDESC,ServerName,MNAME,BILLDETAILS,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            ////if (POS_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    sqlstring = sqlstring + " AND POSDESC IN (";
            ////    for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and SERVERNAME in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,ServerName,MNAME,BILLDETAILS,BILLDATE ORDER BY POSDESC,ServerName,MNAME,BILLDETAILS,BILLDATE";


            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            ////if (POS_LISTBOX.CheckedItems.Count != 0)
            ////{
            ////    ssql3 = ssql3 + " AND POSDESC IN (";
            ////    for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                ssql3 = ssql3 + " and SERVERNAME in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void POSDETAILSUSERWISEDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSUSERWISEWDB r = new POSWISEDETAILSUSERWISEWDB();

            sqlstring = "SELECT POSDESC,MNAME,BILLDETAILS,AddUserid,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    sqlstring = sqlstring + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,MNAME,AddUserid,BILLDATE,BILLDETAILS ORDER BY POSDESC,MNAME,AddUserid,BILLDATE,BILLDETAILS";

            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    ssql3 = ssql3 + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
          public void POSDETAILSUSERWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISEDETAILSUSERWISEWD r = new POSWISEDETAILSUSERWISEWD();
            
           sqlstring = "SELECT POSDESC,MNAME,BILLDETAILS,AddUserid,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    sqlstring = sqlstring + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////}
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,BILLDETAILS,MNAME,AddUserid,BILLDATE ORDER BY POSDESC,ADDUSERID,BILLDATE,BILLDETAILS";

            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ////if (BEARER_LISTBOX.CheckedItems.Count != 0)
            ////    ssql3 = ssql3 + " and SERVERNAME in (";
            ////{
            ////    for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////}
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
         }
          public void SETTLEMENTUSERWISEWDB()
        {
            string P_MODE1;
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CRUSERWISESETTLEMENT_DB r = new CRUSERWISESETTLEMENT_DB();
            P_MODE1 = "";
            //sqlstring = " SELECT BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC ,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM SETTLEMENTREPORT where";
            sqlstring = " SELECT ADDUSERID,BILLDATE,BILLDETAILS,MCODE,MNAME,POSDESC,PAYMENTMODE,PAYAMOUNT AS TOTALAMOUNT FROM SETTLEMENTREPORTLAT Where ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                    P_MODE1 = P_MODE1 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                P_MODE1 = P_MODE1.Remove(P_MODE1.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and PAYMENTMODE in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count ; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            //sqlstring = sqlstring + "GROUP BY BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC";
            sqlstring = sqlstring + " ORDER BY ADDUSERID,BILLDATE,BILLDETAILS";

            //  rv.crystalReportViewer1.ReportSource =r;


            GCON.getDataSet1(sqlstring, "SETTLEMENTREPORT");

            if (GlobalVariable.gdataset.Tables["SETTLEMENTREPORT"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "SETTLEMENTREPORT", r);
               



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text10"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text16"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
         }
        public void SETTLEMENTUSERWISE()
        {
            string P_MODE1;
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CRUSERWISESETTLEMENT r = new CRUSERWISESETTLEMENT();
            P_MODE1 = "";

            //sqlstring = " SELECT BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC ,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM SETTLEMENTREPORT where";
            sqlstring = "SELECT * FROM SETTLEMENTREPORTLAT WHERE ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                    P_MODE1 = P_MODE1 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                P_MODE1 = P_MODE1.Remove(P_MODE1.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and PAYMENTMODE in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            //sqlstring = sqlstring + "GROUP BY BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC";
            sqlstring = sqlstring + " ORDER BY BILLDATE,BILLDETAILS";

            //  rv.crystalReportViewer1.ReportSource =r;


            GCON.getDataSet1(sqlstring, "SETTLEMENTREPORTLAT");

            if (GlobalVariable.gdataset.Tables["SETTLEMENTREPORTLAT"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "SETTLEMENTREPORTLAT", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text10"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text16"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void SETTLEMENTPAMENTMODEWISEWDB()
        {
            string P_MODE1;
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            P_MODE1 = "";
            CRPAYMENTMODEWISESETTLEMENT_WDB r = new CRPAYMENTMODEWISESETTLEMENT_WDB();
            //sqlstring = " SELECT BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC ,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM SETTLEMENTREPORT where";
            sqlstring = " SELECT ADDUSERID,BILLDATE,BILLDETAILS,MCODE,MNAME,POSDESC,PAYMENTMODE,PAYAMOUNT AS TOTALAMOUNT FROM SETTLEMENTREPORTLAT Where ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                    P_MODE1 = P_MODE1 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                P_MODE1 = P_MODE1.Remove(P_MODE1.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and PAYMENTMODE in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            //sqlstring = sqlstring + "GROUP BY BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC";
            sqlstring = sqlstring + " ORDER BY PAYMENTMODE,BILLDATE,BILLDETAILS";

            //  rv.crystalReportViewer1.ReportSource =r;
            GCON.getDataSet1(sqlstring, "SETTLEMENTREPORT");
            if (GlobalVariable.gdataset.Tables["SETTLEMENTREPORT"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "SETTLEMENTREPORT", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text10"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text16"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void SETTLEMENTPAMENTMODEWISE()
        {
            string P_MODE1;
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CRPAYMENTMODEWISESETTLEMENT_DB r = new CRPAYMENTMODEWISESETTLEMENT_DB();
            P_MODE1 = "";
            sqlstring = " SELECT BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC ,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM SETTLEMENTREPORT where";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                    P_MODE1 = P_MODE1 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                P_MODE1 = P_MODE1.Remove(P_MODE1.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (PAYMENT_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and PAYMENTMODE in (";
            {
                for (i = 0; i < PAYMENT_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i < USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY BILLDETAILS,BILLDATE,MCODE,MNAME,PAYMENTMODE,AddUserid,POSDESC";
            sqlstring = sqlstring + " ORDER BY BILLDATE,BILLDETAILS";
            //  rv.crystalReportViewer1.ReportSource =r;


            GCON.getDataSet1(sqlstring, "SETTLEMENTREPORT");

            if (GlobalVariable.gdataset.Tables["SETTLEMENTREPORT"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "SETTLEMENTREPORT", r);
               



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text10"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text16"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void SETTLEMENTRECEIPTWISE()
        {
            string P_MODE1;
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CrptUSERWISESETTLEMENT_RECIPT r = new CrptUSERWISESETTLEMENT_RECIPT();
            P_MODE1 = "";
            sqlstring = " SELECT * FROM reciept_posreport where";
            sqlstring = sqlstring + " CAST(CONVERT(VARCHAR,VOUCHERDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";

            GCON.getDataSet1(sqlstring, "reciept_posreport");

            if (GlobalVariable.gdataset.Tables["reciept_posreport"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "reciept_posreport", r);
               



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text5"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ7;
                TXTOBJ7 = (TextObject)r.ReportDefinition.ReportObjects["Text21"];
                TXTOBJ7.Text = P_MODE1;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
        public void POSWISESUMMARYUSERWISE()
        {
            String[] servercode;
            int i;
            String sqlstring,ssql1,ssql3;
            Report rv=new Report();
              TextObject txtobj1,TXTOBJ10;
            POSWISESUMMERYUSERWISEWD r=new POSWISESUMMERYUSERWISEWD();
            sqlstring = " SELECT POSDESC,AddUserid,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE ";
            sqlstring = sqlstring + " CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
        sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if(POS_LISTBOX.CheckedItems.Count !=0){
             sqlstring = sqlstring + " AND POSDESC IN (";
            for(i=0;i<POS_LISTBOX.CheckedItems.Count-1;i++){
                sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            }
            sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if(PAYMENT_LISTBOX.CheckedItems.Count != 0) 
                 sqlstring = sqlstring + " and SERVERNAME in (";
            {
            for(i=0;i<PAYMENT_LISTBOX.CheckedItems.Count-1;i++){
            sqlstring = sqlstring + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
            }
            sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if(USER_LISTBOX.CheckedItems.Count !=0) {
                 sqlstring = sqlstring + " and ADDUSERID in (";
                for(i=0;i<USER_LISTBOX.CheckedItems.Count-1;i++){
                     sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
             sqlstring = sqlstring + " GROUP BY POSDESC,AddUserid ORDER BY POSDESC,ADDUSERID";
       //  rv.crystalReportViewer1.ReportSource =r;
        
        ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
        ssql3 = ssql3 + " where kotdate between '";
        ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
             if(POS_LISTBOX.CheckedItems.Count !=0){
             ssql3 = ssql3 + " AND POSDESC IN (";
            for(i=0;i<POS_LISTBOX.CheckedItems.Count-1;i++){
                ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
            }
            ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
             if(PAYMENT_LISTBOX.CheckedItems.Count != 0) 
                 ssql3 = ssql3 + " and SERVERNAME in (";
            {
            for(i=0;i<PAYMENT_LISTBOX.CheckedItems.Count-1;i++){
            ssql3 = ssql3 + " '" + PAYMENT_LISTBOX.CheckedItems[i] + "', ";
            }
            ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
             if(USER_LISTBOX.CheckedItems.Count !=0) {
                 ssql3 = ssql3 + " and ADDUSERID in (";
                for(i=0;i<USER_LISTBOX.CheckedItems.Count-1;i++){
                     ssql3 = ssql3 + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring,"POSWISESALESUMMARY");
            GCON.getDataSet1(ssql3,"NANO_TAXGROUPINGSUMMERY");
            if (GlobalVariable.gdataset.Tables["POSWISESALESUMMARY"].Rows.Count > 0){
            rv.GetDetails(sqlstring, "POSWISESALESUMMARY", r);
            rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);
                
       

                 r.SetDataSource(GlobalVariable.gdataset);
           
           rv.crystalReportViewer1.ReportSource = r;
            rv.crystalReportViewer1.Zoom(100);
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text1"];
            TXTOBJ1.Text = GlobalVariable.gCompanyName;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
            TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text2"];
            TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
            TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();
               
            }
            else
            {
                MessageBox.Show("No Records");
            }
        }
  
   
        public Boolean Checkdaterangevalidate(DateTime  Startdate, DateTime Enddate) {
            GlobalVariable.chkdatevalidate = true;
            if ((Enddate.Date - DateTime.Now.Date).Days > 0) {
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

        private void chk_poslist_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (chk_poslist.Checked == true)
            {
                for (i = 0; i < POS_LISTBOX.Items.Count ; i++)
                {
                    POS_LISTBOX.SetItemChecked(i, true);
                }
            }
            else {
                for (i = 0; i < POS_LISTBOX.Items.Count ; i++)
                {
                    POS_LISTBOX.SetItemChecked(i, false);
                }
            }

              }

        private void chk_userlist_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (chk_userlist.Checked == true)
            {
                for (i = 0; i < USER_LISTBOX.Items.Count ; i++)
                {
                  USER_LISTBOX.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < USER_LISTBOX.Items.Count; i++)
                {
                    USER_LISTBOX.SetItemChecked(i, false);
                }
            }

        }

        private void chk_bearer_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (chk_PAYMENT.Checked == true)
            {
                for (i = 0; i < PAYMENT_LISTBOX.Items.Count; i++)
                {
                    PAYMENT_LISTBOX.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < PAYMENT_LISTBOX.Items.Count ; i++)
                {
                    PAYMENT_LISTBOX.SetItemChecked(i, false);
                }
            }

        }

        private void Chk_SUMM_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SETTLEMENT.Checked == true) {
               
            }
        }

        

        private void USERWISE_CheckedChanged(object sender, EventArgs e)
        {
            if (USERWISE.Checked == true) {
                PAYMENTWISE.Checked = false;
                

            }
        }

        private void BEARERWISE_CheckedChanged(object sender, EventArgs e)
        {
            if (PAYMENTWISE.Checked == true) {
                USERWISE.Checked = false;
                
            }
        }

        private void Chk_PosWise_CheckedChanged(object sender, EventArgs e)
        {
            
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
