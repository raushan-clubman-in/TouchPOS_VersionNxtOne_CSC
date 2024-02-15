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


    public partial class DailyCovers : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DailyCovers()
        {
         
            InitializeComponent();
        }
        string sqlstring = "";
        


        private void MEMBERWISE_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            fillpos();
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            dateTimePicker1.Value = DateTime.Now;
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
            CHK_DET.Checked = true;
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);

            myGroupBox myGroupBox2 = new myGroupBox();
            myGroupBox2.Text = "";
            myGroupBox2.BorderColor = Color.Black;
            myGroupBox2.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox2);

            myGroupBox myGroupBox3 = new myGroupBox();
            myGroupBox3.Text = "";
            myGroupBox3.BorderColor = Color.Black;
            myGroupBox3.Size = groupBox4.Size;
            groupBox4.Controls.Add(myGroupBox3);

            myGroupBox myGroupBox4 = new myGroupBox();
            myGroupBox4.Text = "";
            myGroupBox4.BorderColor = Color.Black;
            myGroupBox4.Size = groupBox5.Size;
            groupBox5.Controls.Add(myGroupBox4);

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


        



        
    
        public void COVERSREGISTER()
        {
            int i;
            String sqlstring;
            DataTable dt = new DataTable();
            dt = new DataTable();            
            Report rv = new Report();

            DailycoversRpt r = new DailycoversRpt();
            
            



            sqlstring = "SELECT POSDESC ,SUM(COVERS)AS COVERS,SUM(MEMAMOUNT)MEMAMOUNT,SUM(WALKCOVERS)WALKCOVERS,SUM(WALKAMOUNT)WALKAMOUNT,SUM(MEMAVG)AS MEMAVG,SUM(WALKAVG)AS WALKAVG FROM VW_DAY_COVERS  ";
            sqlstring = sqlstring + " WHERE CAST(CONVERT(VARCHAR,BILLDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + Strings.Format((DateTime)dateTimePicker1.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)dateTimePicker1.Value, "dd-MMM-yyyy") + "'";



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
            sqlstring = sqlstring + "group by POSDESC";

            GCon.getDataSet1(sqlstring, "DAY_COVERS");
            if (GlobalVariable.gdataset.Tables["DAY_COVERS"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "DAY_COVERS", r);
                 r.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text13"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text12"];
                TXTOBJ16.Text = "FOR " + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "  ";
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


        public void MISCOVERSREGISTER()
        {
            int i;
            String sqlstring;
            DataTable dt = new DataTable();
            dt = new DataTable();
            Report rv = new Report();
            MIScoversRpt r = new MIScoversRpt();

            sqlstring = "SELECT POSDESC ,SUM(COVERS)AS COVERS,SUM(MEMAMOUNT)MEMAMOUNT,SUM(WALKCOVERS)WALKCOVERS,SUM(WALKAMOUNT)WALKAMOUNT ,SUM(MEMAVG)AS MEMAVG,SUM(WALKAVG) WALKAVG FROM VW_DAY_COVERS  ";
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
            sqlstring = sqlstring + "group by POSDESC";

            GCon.getDataSet1(sqlstring, "DAY_COVERS");
            if (GlobalVariable.gdataset.Tables["DAY_COVERS"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "DAY_COVERS", r);
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

            if (Chk_SUMM.Checked == true)
            {
                Checkdaterangevalidate(dateTimePicker1.Value, dateTimePicker1.Value);
                String SSQL;
                SSQL = "EXEC POS_POSWISE '" + Strings.Format((DateTime)dateTimePicker1.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)dateTimePicker1.Value, "dd-MMM-yyyy") + "'";
                dt = GCon.getDataSet(SSQL);
                COVERSREGISTER();
            }
            else
            {
                Checkdaterangevalidate(dtp1.Value, dtp2.Value);
                String SSQL;
                SSQL = "EXEC POS_POSWISE '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "','" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "'";
                dt = GCon.getDataSet(SSQL);
                MISCOVERSREGISTER();
            }


            
            

        }

       
       
      

     

        private void btn_new_Click(object sender, EventArgs e)
        {
            POS_LIST.Items.Clear();
            checkBox1.Checked = false;

            groupBox2.Visible = false;
            groupBox4.Visible = false;
            
            fillpos();
            dateTimePicker1.Value = DateTime.Now;
            dtp1.Value = DateTime.Now;
            dtp2.Value = DateTime.Now;
        }

        private void Chk_SUMM_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SUMM.Checked == true)
            {
                CHK_DET.Checked = false;
                
                groupBox2.Visible = false;
                groupBox4.Visible = true;

            }
        }

        private void CHK_DET_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_DET.Checked == true)
            {
                Chk_SUMM.Checked = false;
                groupBox2.Visible = true;
                groupBox4.Visible = false;
            }
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
