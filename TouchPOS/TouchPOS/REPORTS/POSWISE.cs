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
    public partial class POSWISE : Form
    {
        public static String ssql;
        GlobalClass GCON = new GlobalClass();

        public POSWISE()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void POSWISE_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            fillUsernames();
            fillPosLocations();
            fillBearernames();
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
            if (GlobalVariable.gdataset.Tables["Users"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["Users"].Rows.Count; i++)
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
            if (GlobalVariable.gUserCategory != "S")
            {
                ssql = "SELECT DISTINCT poscode,posdesc FROM posmaster Where poscode in (SELECT poscode FROM POS_USERCONTROL where username='" + GlobalVariable.gUserName + "') ";
            }
            else
            {
                ssql = "SELECT DISTINCT poscode,posdesc FROM posmaster";
            }
            GCON.getDataSet1(ssql, "PosMaster");
            if (GlobalVariable.gdataset.Tables["PosMaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["PosMaster"].Rows.Count; i++)
                {
                    POS_LISTBOX.Items.Add(GlobalVariable.gdataset.Tables["PosMaster"].Rows[i].Field<String>("posdesc").Trim());
                }
            }
            POS_LISTBOX.Sorted = true;


        }
        private void fillBearernames()
        {
            int i;
            BEARER_LISTBOX.Items.Clear();
            //ssql = " SELECT DISTINCT ISNULL(SCODE,'') AS SERVERCODE ,ISNULL(S.SERVERNAME,'') AS SERVERNAME  FROM SERVERMASTER S RIGHT OUTER JOIN KOT_DET K ON S.SERVERCODE=K.SCODE --WHERE SERVERTYPE='STEWARD'";
            ssql = " SELECT DISTINCT ISNULL(ServerCode,'') AS SERVERCODE ,ISNULL(S.SERVERNAME,'') AS SERVERNAME  FROM SERVERMASTER S ";
            GCON.getDataSet1(ssql, "SERVERMASTER");
            if (GlobalVariable.gdataset.Tables["SERVERMASTER"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["SERVERMASTER"].Rows.Count; i++)
                {
                    BEARER_LISTBOX.Items.Add(GlobalVariable.gdataset.Tables["SERVERMASTER"].Rows[i].Field<String>("SERVERNAME").Trim());
                }
                BEARER_LISTBOX.Sorted = true;
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
            BEARER_LISTBOX.Items.Clear();
            Chk_SUMM.Checked = false;
            CHK_DET.Checked = false;
            USERWISE.Checked = false;
            BEARERWISE.Checked = false;
            Chk_PosWise.Checked = false;
            chkbox_withdaybrkup.Checked = false;
            fillUsernames();
            fillPosLocations();
            fillBearernames();
            dtp_fromdate.Value = DateTime.Now;
            dtp_todate.Value = DateTime.Now;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            if (POS_LISTBOX.CheckedItems.Count == 0 && USER_LISTBOX.CheckedItems.Count == 0)
            {
                if (POS_LISTBOX.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select Pos Locations");
                    return;

                }
                if (USER_LISTBOX.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select Users ");
                    return;
                }
            }
            else if (POS_LISTBOX.CheckedItems.Count == 0 && BEARER_LISTBOX.CheckedItems.Count == 0)
            {
                if (POS_LISTBOX.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select Pos Locations");
                    return;
                }
                if (BEARER_LISTBOX.CheckedItems.Count == 0)
                {
                    MessageBox.Show("select Bearers");
                    return;
                }
            }
            Checkdaterangevalidate(dtp_fromdate.Value, dtp_todate.Value);
            if (GlobalVariable.chkdatevalidate == false)
            { return; }

            if (Chk_SUMM.Checked == true)
            {
                if (Chk_SUMM.Checked == true && USERWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARYUSERWISE();
                    return;
                }
                else if (Chk_SUMM.Checked == true && USERWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);

                    POSWISESUMMARYUSERWISEDB();
                    return;
                }
                else if (Chk_SUMM.Checked == true && BEARERWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARYBEARERWISE();
                    return;
                }
                else if (Chk_SUMM.Checked == true && BEARERWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARYBEARERWISEWDB();
                    return;
                }
                else if (Chk_SUMM.Checked == true && CHK_CATEGORY.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSCATWISESUMMARY();
                    return;
                }
                else if (Chk_SUMM.Checked == true && Chk_PosWise.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARY_POS();
                    return;
                }
                else if (Chk_SUMM.Checked == true && Chk_PosWise.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARY_POSWDB();
                    return;
                }
                else
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSWISESUMMARY_POS();
                    return;
                }

            }
            else if (CHK_DET.Checked == true)
            {
                if (CHK_DET.Checked == true && USERWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILSUSERWISE();
                    return;
                }
                else if (CHK_DET.Checked == true && USERWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);

                    POSDETAILSUSERWISEDB();
                    return;
                }
                else if (CHK_DET.Checked == true && BEARERWISE.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILSBEARERWISE();
                    return;
                }
                else if (CHK_DET.Checked == true && BEARERWISE.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILSBEARERWISEDB();
                    return;
                }
                else if (CHK_DET.Checked == true && Chk_PosWise.Checked == true && chkbox_withdaybrkup.Checked == false)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILPOSWISEWD();
                    return;
                }
                else if (CHK_DET.Checked == true && Chk_PosWise.Checked == true && chkbox_withdaybrkup.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILPOSWISEWDB();
                    return;
                }

                else if (CHK_DET.Checked == true && CHK_CATEGORY.Checked == true)
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSCATDETAILPOSWISEWD();
                    return;
                }
                else
                {
                    ssql = "exec POS_POSWISE '" + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "','" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
                    GCON.ExecuteStoredProcedure(ssql);
                    POSDETAILPOSWISEWD();
                    return;
                }

            }

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
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,MNAME,BILLDetails,BILLDATE ORDER BY POSDESC,MNAME,BILLDetails,BILLDATE";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
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
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,MNAME,billdetails,BILLDATE ORDER BY POSDESC,MNAME,billdetails,BILLDATE";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
            }
        }

        public void POSCATDETAILPOSWISEWD()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISECATEGORYDETAILSPOSWISEWD r = new POSWISECATEGORYDETAILSPOSWISEWD();

            sqlstring = "SELECT POSDESC,MNAME,billdetails,BILLDATE,CATEGORY,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,MNAME,billdetails,BILLDATE,CATEGORY ORDER BY POSDESC,MNAME,billdetails,BILLDATE";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
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
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and SERVERNAME in (";
                for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,MNAME,SERVERNAME,BILLDATE,BILLDETAILS ORDER BY POSDESC,MNAME,SERVERNAME,BILLDATE,BILLDETAILS";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and SERVERNAME in (";
                for (i = 0; i < BEARER_LISTBOX.CheckedItems.Count; i++)
                {
                    ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
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
                MessageBox.Show("No Records To Display..");
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
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and SERVERNAME in (";
            {
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,ServerName,MNAME,BILLDETAILS,BILLDATE ORDER BY POSDESC,ServerName,MNAME,BILLDETAILS,BILLDATE";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i < POS_LISTBOX.CheckedItems.Count; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
                ssql3 = ssql3 + " and SERVERNAME in (";
            {
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
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
                MessageBox.Show("No Records To Display..");
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
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + " GROUP BY POSDESC,MNAME,AddUserid,BILLDATE,BILLDETAILS ORDER BY POSDESC,MNAME,AddUserid,BILLDATE,BILLDETAILS";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
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
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + " GROUP BY POSDESC,BILLDETAILS,MNAME,AddUserid,BILLDATE ORDER BY POSDESC,ADDUSERID,BILLDATE,BILLDETAILS";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
            }
        }
        public void POSWISESUMMARY_POSWDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYWDB r = new POSWISESUMMERYWDB();

            sqlstring = "SELECT POSDESC,SUM(BILLAMOUNT) AS BILLAMOUNT,BILLDATE,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,BILLDATE ORDER BY POSDESC";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
            }
        }
        public void POSWISESUMMARY_POS()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYWD r = new POSWISESUMMERYWD();
            TaxBreakup r1 = new TaxBreakup();
            sqlstring = "SELECT POSDESC,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC ORDER BY POSDESC";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
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
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text1"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text2"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }

        public void POSCATWISESUMMARY()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISECATEGORYSUMMERY r = new POSWISECATEGORYSUMMERY();
            TaxBreakup r1 = new TaxBreakup();
            sqlstring = "SELECT POSDESC,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS,category FROM POSWISESALESUMMARY WHERE ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + "GROUP BY POSDESC,category ORDER BY POSDESC";



            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
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
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text1"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text2"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }

        public void POSWISESUMMARYBEARERWISEWDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYBEARERWISEWDB r = new POSWISESUMMERYBEARERWISEWDB();
            sqlstring = "SELECT POSDESC,ServerName,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE ";
            sqlstring = sqlstring + "  CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and SERVERNAME in (";
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,SERVERNAME ,BILLDATE ORDER BY POSDESC,SERVERNAME,BILLDATE";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
                ssql3 = ssql3 + " and SERVERNAME in (";
            {
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
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
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text11"];
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
                MessageBox.Show("No Records To Display..");
            }
        }
        public void POSWISESUMMARYBEARERWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYBEARERWISEWD r = new POSWISESUMMERYBEARERWISEWD();
            sqlstring = "SELECT POSDESC,ServerName,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + " where CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
                sqlstring = sqlstring + " and SERVERNAME in (";
            {
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            sqlstring = sqlstring + " GROUP BY POSDESC,SERVERNAME ORDER BY POSDESC,SERVERNAME";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }
            if (BEARER_LISTBOX.CheckedItems.Count != 0)
                ssql3 = ssql3 + " and SERVERNAME in (";
            {
                for (i = 0; i <= BEARER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + BEARER_LISTBOX.CheckedItems[i] + "', ";
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
                MessageBox.Show("No Records To Display..");
            }
        }
        public void POSWISESUMMARYUSERWISEDB()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYUSERWISEWDB r = new POSWISESUMMERYUSERWISEWDB();
            sqlstring = "SELECT POSDESC,AddUserid,BILLDATE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE";
            sqlstring = sqlstring + " CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + " GROUP BY POSDESC,AddUserid,billdate ORDER BY POSDESC,ADDUSERID,BILLDATE";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
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
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text1"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text2"];
                TXTOBJ16.Text = "PERIOD FROM " + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + dtp_todate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }
        public void POSWISESUMMARYUSERWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql3;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            POSWISESUMMERYUSERWISEWD r = new POSWISESUMMERYUSERWISEWD();
            sqlstring = " SELECT POSDESC,AddUserid,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT) AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS FROM POSWISESALESUMMARY WHERE ";
            sqlstring = sqlstring + " CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + USER_LISTBOX.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            sqlstring = sqlstring + " GROUP BY POSDESC,AddUserid ORDER BY POSDESC,ADDUSERID";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + dtp_fromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + dtp_todate.Value.ToString("dd-MMM-yyyy") + "'";
            if (POS_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LISTBOX.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + POS_LISTBOX.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
            }

            if (USER_LISTBOX.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and ADDUSERID in (";
                for (i = 0; i <= USER_LISTBOX.CheckedItems.Count - 1; i++)
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
                MessageBox.Show("No Records To Display..");
            }
        }


        public Boolean Checkdaterangevalidate(DateTime Startdate, DateTime Enddate)
        {
            GlobalVariable.chkdatevalidate = true;
            if ((Enddate.Date - DateTime.Now.Date).Days > 0)
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

        private void chk_poslist_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (chk_poslist.Checked == true)
            {
                for (i = 0; i < POS_LISTBOX.Items.Count; i++)
                {
                    POS_LISTBOX.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < POS_LISTBOX.Items.Count; i++)
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
                for (i = 0; i < USER_LISTBOX.Items.Count; i++)
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
            if (chk_bearer.Checked == true)
            {
                for (i = 0; i < BEARER_LISTBOX.Items.Count; i++)
                {
                    BEARER_LISTBOX.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < BEARER_LISTBOX.Items.Count; i++)
                {
                    BEARER_LISTBOX.SetItemChecked(i, false);
                }
            }

        }

        private void Chk_SUMM_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SUMM.Checked == true)
            {
                CHK_DET.Checked = false;
            }
        }

        private void CHK_DET_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_DET.Checked == true)
            {
                Chk_SUMM.Checked = false;
            }
        }

        private void USERWISE_CheckedChanged(object sender, EventArgs e)
        {
            if (USERWISE.Checked == true)
            {
                BEARERWISE.Checked = false;
                Chk_PosWise.Checked = false;

            }
        }

        private void BEARERWISE_CheckedChanged(object sender, EventArgs e)
        {
            if (BEARERWISE.Checked == true)
            {
                USERWISE.Checked = false;
                Chk_PosWise.Checked = false;
            }
        }

        private void Chk_PosWise_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_PosWise.Checked == true)
            {
                USERWISE.Checked = false;
                BEARERWISE.Checked = false;
            }
        }

        private void CHK_CATEGORY_CheckedChanged(object sender, EventArgs e)
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
