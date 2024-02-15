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


    public partial class NONCHARECHECKSUM : Form
    {
        GlobalClass GCon = new GlobalClass();
        
        public NONCHARECHECKSUM()
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

        public void NONCHARGESUMMARY()
        {
            string[] MemberCode = null;
            int i;
            String sqlstring;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
           // TextObject txtobj1, TXTOBJ10;
            CRYSTAL.Cry_noncharegeablechecksum r = new CRYSTAL.Cry_noncharegeablechecksum();

            sqlstring = "SELECT *  FROM NONCHARGEABLECHECKSUM ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,Billdate,106)AS DATETIME) = '";
            sqlstring = sqlstring  + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";

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

            GCon.getDataSet1(sqlstring, "NONCHARGEABLECHECKSUM");

            if (GlobalVariable.gdataset.Tables["NONCHARGEABLECHECKSUM"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "NONCHARGEABLECHECKSUM", r);
                r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text8"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text9"];
                TXTOBJ16.Text = "Period From "  + dtp2.Value.ToString("dd-MMM-yyyy") + "";
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

     
        private void btn_view_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            if (POS_LIST.CheckedItems.Count == 0)                 
            {
                MessageBox.Show("Select the Location(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            String SSQL;
            SSQL = "EXEC Pos_Nonchargecheckssum '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
            dt = GCon.getDataSet(SSQL);

                NONCHARGESUMMARY();
                  }

        private void btn_new_Click(object sender, EventArgs e)
        {
          
            dtp2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox1_CheckedChanged(sender, e);
        }

    }
}
