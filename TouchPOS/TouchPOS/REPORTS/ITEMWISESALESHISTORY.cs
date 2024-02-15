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
    public partial class ITEMWISESALESHISTORY : Form
    {
        
        public static String ssql;
        GlobalClass GCON = new GlobalClass();

        public ITEMWISESALESHISTORY()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ITEMWISESALESREPORT_Load(object sender, EventArgs e)
        {
            String sqlstring;
            BlackGroupBox();
            fillSubGroup();
            fillTaxType();
            fillPosLocations();
           
            string FinYear323 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
           
           
            Cmb_Order.SelectedIndex = 0;
            mskFromdate.Value = DateTime.Now;
            mskTodate.Value = DateTime.Now;
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }
        
        public void fillPosLocations()
        {
            String sqlstring;
            chklist_POSlocation.Items.Clear();
            int i;


            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC FROM posmaster ";
            GCON.getDataSet1(sqlstring, "posmaster");
            if (GlobalVariable.gdataset.Tables["posmaster"].Rows.Count  >0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["posmaster"].Rows.Count ; i++)
                {
                    chklist_POSlocation.Items.Add(GlobalVariable.gdataset.Tables["posmaster"].Rows[i].Field<String>("POSDESC").Trim());
                }
            }
            
        }
        public void fillTaxType()
        {
            String sqlstring;
            chklist_Type.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT itemcode,itemdesc FROM ITEMMaster ORDER BY ITEMDESC";
            GCON.getDataSet1(sqlstring, "ITEMMaster");
            if (GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count  > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count ; i++)
                {
                    chklist_Type.Items.Add(GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("itemcode").Trim() + "-" + GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("itemdesc").Trim());
                }
            }
            chklist_Type.Sorted = true;
            
        }

        public void fillSubGroup()
        {
            String sqlstring;
            lstsubgroup.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT subGROUPDESC  FROM SUBGROUPMASTER ";
            GCON.getDataSet1(sqlstring, "SUBGROUPMASTER");
            if (GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows.Count  > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows.Count ; i++)
                {
                    lstsubgroup.Items.Add(GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows[i].Field<String>("subGROUPDESC").Trim());
                }
            }
            
        }
      
       

        private void btn_save_Click(object sender, EventArgs e)
        {
        
            if (chklist_POSlocation.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (chklist_Type.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Item name(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
           
            if (lstsubgroup.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Item name(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //Checkdaterangevalidate(mskFromdate.Value, mskTodate.Value);
            //if (GlobalVariable.chkdatevalidate == false)
                //return;


            string SSQL;
            


            SSQL = "EXEC proc_SSALEShISTORY '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "','" + (GlobalVariable.FinStart.Year.ToString()) + "'";
            GCON.ExecuteStoredProcedure(SSQL);
                SalesHistory();
            
        }

        public void SalesHistory()
        {
            int i;
            String sqlstring;
            String[] TYPE;
            string HNAME, POSNAME;
            Report rv = new Report();

            SalesHistory r = new SalesHistory();
            POSNAME = "";


            sqlstring = " SELECT ITEMCODE,ITEMDESC,subgroupdesc,sum(TODAYQTY)TODAYQTY, sum(TOAYAMOUNT)TOAYAMOUNT, sum(MONTHQTY)MONTHQTY, sum(MONTHAMOUNT)MONTHAMOUNT, sum(YEARQTY)YEARQTY,sum(YEARAMOUNT)YEARAMOUNT FROM VIEW_SSALEShISTORY ";
           
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " where POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            if (lstsubgroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
            if (chklist_Type.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            sqlstring = sqlstring + "group by ITEMCODE, ITEMDESC,subgroupdesc";


            GCON.getDataSet1(sqlstring, "VIEW_SSALEShISTORY");

            if (GlobalVariable.gdataset.Tables["VIEW_SSALEShISTORY"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEW_SSALEShISTORY", r);
                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text13"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text12"];
                TXTOBJ16.Text = "PERIOD FROM " + "01-Apr-" + (GlobalVariable.FinStart.Year.ToString()) + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void Chk_Taxtype_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_Taxtype.Checked == true)
            {
                for (i = 0; i < chklist_Type.Items.Count ; i++)
                {
                    chklist_Type.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_Type.Items.Count ; i++)
                {
                    chklist_Type.SetItemChecked(i, false);
                }
            }
        }

        private void Chk_POSlocation_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_POSlocation.Checked == true)
            {
                for (i = 0; i < chklist_POSlocation.Items.Count ; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_POSlocation.Items.Count ; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, false);
                }
            }
        }

     

        private void Chk_SelectAllsubgroup_CheckedChanged(object sender, EventArgs e)
        {
          int i;
            if (Chk_SelectAllsubgroup.Checked == true)
            {
                for (i = 0; i < lstsubgroup.Items.Count ; i++)
                {
                    lstsubgroup.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < lstsubgroup.Items.Count ; i++)
                {
                    lstsubgroup.SetItemChecked(i, false);
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            mskFromdate.Value = DateTime.Now;
            mskTodate.Value = DateTime.Now;
           
            chklist_POSlocation.Items.Clear();
          
            lstsubgroup.Items.Clear();
            chklist_Type.Items.Clear();
            
            Chk_Taxtype.Checked = false;
            Chk_SelectAllsubgroup.Checked = false;
            Chk_POSlocation.Checked = false;
            fillSubGroup();
            fillTaxType();
            fillPosLocations();
           

            


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
    

