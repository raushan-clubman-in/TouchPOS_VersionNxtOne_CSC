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


    public partial class DISCREPORT : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DISCREPORT()
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

        


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j < POS_LIST.Items.Count ; j++)
                {
                    POS_LIST.SetItemChecked(j, true);

                }

               
            }
            else
                if (checkBox1.Checked == false)
                {
                    for (int j = 0; j < POS_LIST.Items.Count ; j++)
                    {
                        POS_LIST.SetItemChecked(j, false);

                    }
                }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
        public void DISCREGISTER()
        {
            int i;
            String sqlstring, SSQL3;
            DataTable dt = new DataTable();
            dt = new DataTable();            
            Report rv = new Report();
            DISCRpt r = new DISCRpt();



            sqlstring = "SELECT  DiscCategory,DiscUser,	BILLDETAILS	,BILLDATE,	POSDESC,	CATEGORY,	ItemDiscPerc,	SUM(ItemDiscAmt)ItemDiscAmt,SUM(FOOD)AS FOOD,SUM(HARDBEVERAGES)AS HARDBEVERAGES,	SUM(SOFTBEVERAGE)SOFTBEVERAGE,	SUM(FRUITJUICE)FRUITJUICE,	SUM(TOTAL)TOTAL FROM DISCOUNT_REPORT ";
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
            sqlstring = sqlstring + "group by DiscCategory,DiscUser,	BILLDETAILS	,BILLDATE,	POSDESC,	CATEGORY,	ItemDiscPerc";

            GCon.getDataSet1(sqlstring, "DISCOUNT_REPORT");
            if (GlobalVariable.gdataset.Tables["DISCOUNT_REPORT"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "NC_REPORT", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text13"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text12"];
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
          

            Checkdaterangevalidate(dtp1.Value, dtp2.Value);
            String SSQL;
            SSQL = "EXEC POS_POSWISE '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            dt = GCon.getDataSet(SSQL);
            DISCREGISTER();

            
            

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            POS_LIST.Items.Clear();
            checkBox1.Checked = false;
           
            
            fillpos();
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
