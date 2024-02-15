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
using TouchPOS.CRYSTAL;

namespace TouchPOS.REPORTS
{


    public partial class MEMBERWISE : Form
    {
        GlobalClass GCon = new GlobalClass();

        public MEMBERWISE()
        {

            InitializeComponent();
        }
        string sql = "";
        string sqlstring = "";
        string itemcode;
        string vseqno, gvseqno;
        string ssql = "";


        private void MEMBERWISE_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            fillpos();
            fillMember();
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
        }


        public void BlackGroupBox()
        {
           
            myGroupBox myGroupBox1 = new myGroupBox();
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

        public void fillMember()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            if (CHK_MEM.Checked == true)
            {
                sqlstring = "SELECT DISTINCT ISNULL(K.MCODE,'') AS MCODE,ISNULL(m.MNAME,'') AS MNAME FROM KOT_DET AS K left outer JOIN viewmember AS M ON  M.MCODE = K.MCODE UNION ALL SELECT DISTINCT ISNULL(MCODE,'') AS MCODE,ISNULL(MNAME,'') AS MNAME FROM viewmember WHERE TYPE='OUTSIDER1'";
            }
            else if (CHK_WALK.Checked == true)
            {
                sqlstring = " Select DISTINCT ISNULL(MCODE,'') AS MCODE,ISNULL(MNAME,'') AS MNAME FROM viewmember WHERE TYPE='OUTSIDER' ";
            }
            else
            {
                sqlstring = "SELECT DISTINCT ISNULL(K.MCODE,'') AS MCODE,ISNULL(m.MNAME,'') AS MNAME FROM KOT_DET AS K left outer JOIN viewmember AS M ON  M.MCODE = K.MCODE UNION ALL SELECT DISTINCT ISNULL(MCODE,'') AS MCODE,ISNULL(MNAME,'') AS MNAME FROM viewmember WHERE TYPE IN('OUTSIDER1','OUTSIDER')";
            }
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                MEMBER_LIST.Items.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MEMBER_LIST.Items.Add(dt.Rows[i].ItemArray[0].ToString() + "->" + dt.Rows[i].ItemArray[1].ToString());

                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j < POS_LIST.Items.Count; j++)
                {
                    POS_LIST.SetItemChecked(j, true);

                }


            }
            else
                if (checkBox1.Checked == false)
                {
                    for (int j = 0; j < POS_LIST.Items.Count; j++)
                    {
                        POS_LIST.SetItemChecked(j, false);

                    }
                }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                for (int j = 0; j < MEMBER_LIST.Items.Count; j++)
                {
                    MEMBER_LIST.SetItemChecked(j, true);

                }
            }
            else
                if (checkBox2.Checked == false)
                {
                    for (int j = 0; j < MEMBER_LIST.Items.Count; j++)
                    {
                        MEMBER_LIST.SetItemChecked(j, false);

                    }
                }
        }



        public void MEMBERWISESUMMARYWD()
        {
            string[] MemberCode = null;
            int i;
            String sqlstring, SSQL, SSQL3;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            MEMBERWISEDETAILS_SUMMARYWD r = new MEMBERWISEDETAILS_SUMMARYWD();



            sqlstring = "SELECT MNAME,MCODE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";



            if (POS_LIST.CheckedItems.Count != 0)
            {

                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
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

            if (MEMBER_LIST.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND MCODE IN (";
                for (i = 0; i <= MEMBER_LIST.CheckedItems.Count - 1; i++)
                {
                    var mcode = MEMBER_LIST.CheckedItems[i].ToString();
                    MemberCode = mcode.Split('-');
                    sqlstring = sqlstring + "'" + MemberCode[0] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            else
            {
                MessageBox.Show("Select the MEMBER NAME(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //if (CHK_WALK.Checked == false)
            //{
                
            //}
            //else { sqlstring = sqlstring + " AND MCODE IN ('WALKIN') "; }

            sqlstring = sqlstring + "GROUP BY MNAME,MCODE ORDER BY MNAME,MCODE";


            SSQL3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            SSQL3 = SSQL3 + " where kotdate between'";
            SSQL3 = SSQL3 + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            if (POS_LIST.CheckedItems.Count != 0)
            {

                SSQL3 = SSQL3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
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



            if (MEMBER_LIST.CheckedItems.Count != 0)
            {
                SSQL3 = SSQL3 + " AND MCODE IN (";
                for (i = 0; i <= MEMBER_LIST.CheckedItems.Count - 1; i++)
                {
                    var mcode = MEMBER_LIST.CheckedItems[i].ToString();
                    MemberCode = mcode.Split('-');
                    SSQL3 = SSQL3 + "'" + MemberCode[0] + "', ";
                }
                SSQL3 = SSQL3.Remove(SSQL3.Length - 2);
                SSQL3 = SSQL3 + ")";
            }
            else
            {
                MessageBox.Show("Select the MEMBER NAME(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            SSQL3 = SSQL3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";

            GCon.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCon.getDataSet1(SSQL3, "NANO_TAXGROUPINGSUMMERY");
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


        public void MEMBERWISESUMMARY()
        {
            string[] MemberCode = null;
            int i;
            String sqlstring, SSQL, SSQL3;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            MEMBERWISEDETAILSWD r = new MEMBERWISEDETAILSWD();



            sqlstring = "SELECT POSDESC,MNAME,MCODE,BILLDETAILS,BILLDATE,PAYMENTMODE,SUM(BILLAMOUNT) AS BILLAMOUNT,SUM(TAXAMOUNT) AS TAXAMOUNT,SUM(PACKAMOUNT)AS PACKAMOUNT,SUM(SCHARGE) AS SCHARGE,SUM(ACHARGE) AS ACHARGE,SUM(PCHARGE) AS PCHARGE,SUM(RCHARGE) AS RCHARGE,SUM(ISNULL(CGST,0)) AS CGST,SUM(ISNULL(SGST,0)) AS SGST,SUM(ISNULL(CESS,0)) AS CESS  FROM POSWISESALESUMMARY ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";



            if (POS_LIST.CheckedItems.Count != 0)
            {

                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
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



            if (MEMBER_LIST.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND MCODE IN (";
                for (i = 0; i <= MEMBER_LIST.CheckedItems.Count - 1; i++)
                {
                    var mcode = MEMBER_LIST.CheckedItems[i].ToString();
                    MemberCode = mcode.Split('-');
                    sqlstring = sqlstring + "'" + MemberCode[0] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
            }
            else
            {
                MessageBox.Show("Select the MEMBER NAME(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            sqlstring = sqlstring + "GROUP BY POSDESC,MNAME,MCODE,BILLDETAILS,BILLDATE,PAYMENTMODE ORDER BY POSDESC,MNAME,MCODE,BILLDETAILS,BILLDATE,PAYMENTMODE";



            SSQL3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERY";
            SSQL3 = SSQL3 + " where kotdate between'";
            SSQL3 = SSQL3 + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            if (POS_LIST.CheckedItems.Count != 0)
            {

                SSQL3 = SSQL3 + " AND POSDESC IN (";
                for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
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



            if (MEMBER_LIST.CheckedItems.Count != 0)
            {
                SSQL3 = SSQL3 + " AND MCODE IN (";
                for (i = 0; i <= MEMBER_LIST.CheckedItems.Count - 1; i++)
                {
                    var mcode = MEMBER_LIST.CheckedItems[i].ToString();
                    MemberCode = mcode.Split('-');
                    SSQL3 = SSQL3 + "'" + MemberCode[0] + "', ";
                }
                SSQL3 = SSQL3.Remove(SSQL3.Length - 2);
                SSQL3 = SSQL3 + ")";
            }
            else
            {
                MessageBox.Show("Select the MEMBER NAME(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            SSQL3 = SSQL3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";

            GCon.getDataSet1(sqlstring, "POSWISESALESUMMARY");
            GCon.getDataSet1(SSQL3, "NANO_TAXGROUPINGSUMMERY");
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

        private void btn_view_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            if (POS_LIST.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Location(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MEMBER_LIST.CheckedItems.Count == 0  && CHK_MEM.Checked == true)
            {
                MessageBox.Show("Select the Member name(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Checkdaterangevalidate(dtp1.Value, dtp2.Value);
            String SSQL;
            SSQL = "EXEC POS_POSWISE '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            dt = GCon.getDataSet(SSQL);


            if (Chk_SUMM.Checked == true)
            {
                MEMBERWISESUMMARYWD();
            }
            else
            {
                MEMBERWISESUMMARY();
            }

        }

        private void CHK_DET_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_DET.Checked == true)
            {
                Chk_SUMM.Checked = false;
            }
        }

        private void Chk_SUMM_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SUMM.Checked == true)
            {
                CHK_DET.Checked = false;
            }
        }

        private void CHK_MEM_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_MEM.Checked == true)
            {
                CHK_BOTH.Checked = false;
                CHK_WALK.Checked = false;
                fillMember();
                checkBox2_CheckedChanged(sender, e);
            }
        }

        private void CHK_WALK_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_WALK.Checked == true)
            {
                CHK_BOTH.Checked = false;
                CHK_MEM.Checked = false;
                fillMember();
                checkBox2_CheckedChanged(sender, e);
            }
        }

        private void CHK_BOTH_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_BOTH.Checked == true)
            {
                CHK_WALK.Checked = false;
                CHK_MEM.Checked = false;
                fillMember();
                checkBox2_CheckedChanged(sender, e);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            POS_LIST.Items.Clear();
            MEMBER_LIST.Items.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            CHK_MEM.Checked = false;
            CHK_BOTH.Checked = false;
            CHK_DET.Checked = false;
            Chk_SUMM.Checked = false;
            CHK_WALK.Checked = false;
            fillpos();
            fillMember();
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
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
