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


    public partial class NCCHECKSITEMWISE : Form
    {
        GlobalClass GCon = new GlobalClass();
        
        public NCCHECKSITEMWISE()
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
            fillMember();
            fillncchecks();
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void fillncchecks()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT distinct ISNULL(NCCategory,'') AS NCCategory  FROM kot_hdr WHERE isnull(NCCategory,'')<>''  ORDER BY NCCategory";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                Chk_NC.Items.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Chk_NC.Items.Add(dt.Rows[i].ItemArray[0].ToString());

                }
            }
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
            sqlstring = "SELECT DISTINCT ISNULL(K.ITEMCODE,'') AS ITEMCODE,ISNULL(m.ItemDesc,'') AS ItemDesc FROM KOT_DET AS K left outer JOIN ITEMMASTER AS M ON ";
            sqlstring = sqlstring + " M.ITEMCODE = K.ITEMCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                ITEM_LIST.Items.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ITEM_LIST.Items.Add(dt.Rows[i].ItemArray[0].ToString() + "->" + dt.Rows[i].ItemArray[1].ToString());

                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j <= POS_LIST.Items.Count - 1; j++)
                {
                    POS_LIST.SetItemChecked(j, true);

                }
            }
            else
                if (checkBox1.Checked == false)
                {
                    for (int j = 0; j <= POS_LIST.Items.Count - 1; j++)
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
                for (int j = 0; j < ITEM_LIST.Items.Count ; j++)
                {
                    ITEM_LIST.SetItemChecked(j, true);

                }
            }
            else
                if (checkBox2.Checked == false)
                {
                    for (int j = 0; j < ITEM_LIST.Items.Count ; j++)
                    {
                        ITEM_LIST.SetItemChecked(j, false);

                    }
                }
        }


        public void Ncchecksitems()
        {
            string[] MemberCode = null;
            int i;
            String sqlstring, SSQL, SSQL3;
            DataTable dt = new DataTable();
            dt = new DataTable();
            string str = null;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            CRYSTAL.Cry_NCchecksitemwise r = new CRYSTAL.Cry_NCchecksitemwise();
             
            //sqlstring = "SELECT Itemcode,Itemdesc,Kotdate,sum(Qty) as Qty,sum(Amount) as Amount,Poscode,Posdesc,Nccategory  FROM NCchecksitems ";
            sqlstring = "SELECT Kotdetails,BILLDETAILS,Itemcode,Itemdesc,Kotdate,sum(Qty) as Qty,sum(Amount) as Amount,Poscode,Posdesc,Nccategory,NCRemarks  FROM NCchecksitems ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
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

            if (Chk_NC.CheckedItems.Count != 0)
            {

                sqlstring = sqlstring + " AND NCCategory IN (";
                for (i = 0; i < Chk_NC.CheckedItems.Count; i++)
                {
                    sqlstring = sqlstring + " '" + Chk_NC.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {

                MessageBox.Show("Select the POS Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ITEM_LIST.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND ITEMCODE IN (";
                for (i = 0; i < ITEM_LIST.CheckedItems.Count; i++)
                {
                    var mcode = ITEM_LIST.CheckedItems[i].ToString();
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

            //sqlstring = sqlstring + " Group by Itemcode,Itemdesc,Kotdate,Poscode,Posdesc,Nccategory";
            sqlstring = sqlstring + " Group by Itemcode,Itemdesc,Kotdate,Poscode,Posdesc,Nccategory,Kotdetails,NCRemarks,BILLDETAILS order by Nccategory,BILLDETAILS,itemcode ";

            GCon.getDataSet1(sqlstring, "NCchecksitemsLat");

            if (GlobalVariable.gdataset.Tables["NCchecksitemsLat"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "NCchecksitemsLat", r);
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
            if (Chk_NC.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the NC TYPE(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ITEM_LIST.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Member name(s)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Checkdaterangevalidate(dtp1.Value, dtp2.Value);
           
                Ncchecksitems();
          
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                for (int j = 0; j < Chk_NC.Items.Count; j++)
                {
                    Chk_NC.SetItemChecked(j, true);

                }
            }
            else
                if (checkBox3.Checked == false)
                {
                    for (int j = 0; j < Chk_NC.Items.Count; j++)
                    {
                        Chk_NC.SetItemChecked(j, false);

                    }
                }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
