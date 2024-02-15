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
    public partial class ITEMWISESALESREPORT : Form
    {
        
        public static String ssql;
        GlobalClass GCON = new GlobalClass();
         
        public ITEMWISESALESREPORT()
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
            fillGroup();
            fillcategory();
            fillSubGroup();
            fillTaxType();
            fillPosLocations();
            fillpaymentmode();
            string FinYear323 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
            Chk_SelectAllCategory.Checked = false;
            Chk_SelectAllGroup.Checked = false;
          
            chk_subgroupwise.Checked = false;
           
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

        public void fillpaymentmode()
        {
            String sqlstring;
            CHKLIST_PAYMENTMODE.Items.Clear();
            int i;


            sqlstring = "select paymentcode from PaymentModeMaster ";
            GCON.getDataSet1(sqlstring, "PaymentModeMaster");
            if (GlobalVariable.gdataset.Tables["PaymentModeMaster"].Rows.Count  > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["PaymentModeMaster"].Rows.Count ; i++)
                {
                    CHKLIST_PAYMENTMODE.Items.Add(GlobalVariable.gdataset.Tables["PaymentModeMaster"].Rows[i].Field<String>("paymentcode").Trim());
                }
            }
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
        public void fillcategory() 
        {
            String sqlstring;
            lstcategory.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT CATEGORY FROM ITEMMaster ";
            GCON.getDataSet1(sqlstring, "ITEMMaster");
            if (GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count  > 0)
                for (i = 0; i < GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count ; i++)
                {
            {
                lstcategory.Items.Add(GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("CATEGORY").Trim());
            }
                }
           // lstcategory.Sorted = true;
        }
    
        public void fillGroup() {
            String sqlstring;
            LstGroup.Items.Clear();
            int i;
               sqlstring = "SELECT DISTINCT Groupcode,Groupdesc FROM GroupMaster ";
               GCON.getDataSet1(sqlstring, "GroupMaster");
               if (GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count  >0)
               {
                   for (i = 0; i < GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count ; i++)
                   {
                       LstGroup.Items.Add(GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupcode").Trim() + "-" + GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupdesc").Trim());
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
           

            if (lstcategory.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Item name(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (LstGroup.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Location(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (lstsubgroup.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select the Item name(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            Checkdaterangevalidate(mskFromdate.Value, mskTodate.Value);
            if (GlobalVariable.chkdatevalidate == false)
                return;

            string SSQL;
            SSQL = "EXEC GROUPWISESALE '" + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "','" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "','N'";
            GCON.ExecuteStoredProcedure(SSQL);

            if (chk_itemwise.Checked == true)
            {
                ITEMWISESUMMARYWD();
            }
            else if (chk_poswise.Checked == true)
            {
                POS_GROUPWISE();
            }
            else if (chk_subgroupwise.Checked == true)
            {
                SUBGROUPWISE();
            }
            else if (CHK_GROUP.Checked == true)
            {
                GROUPWISE();
            }
            else if (CHK_CATEGORY.Checked == true)
            {
                CATEGORYWISE();
            }
            else if (chk_top10.Checked == true)
            {
                SSQL = "exec proc_top10saleitem '" + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "','" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "' ";
                TOP10ITEMWISE();
            }
            else if (chk_his.Checked == true)
            {
                
                SSQL = "EXEC GROUPWISESALE '" + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "','" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "','N'";
                SalesHistory();
            }
        }

        public void TOP10ITEMWISE()
        {
            int i;
            String sqlstring, ssql3;
            String[]  TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();

            TOP10ITEM r = new TOP10ITEM();
            POSNAME = "";


            sqlstring = " SELECT ITEMCODE, ITEMDESC,SUBGROUPDESC, Sum (TOTALQTY)as TOTALQTY,Sum (TOTALAMOUNT)as TOTALAMOUNT FROM VIEW_TOP10sALESiTEM ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and POSDESC IN (";
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + "group by ITEMCODE, ITEMDESC,SUBGROUPDESC Order by SUBGROUPDESC ,TOTALQTY desc," + Cmb_Order.Text + " ";


            GCON.getDataSet1(sqlstring, "VIEW_TOP10sALESiTEM");

            if (GlobalVariable.gdataset.Tables["VIEW_TOP10sALESiTEM"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEW_TOP10sALESiTEM", r);              
                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text7"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text5"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)r.ReportDefinition.ReportObjects["Text9"];
                TXTOBJ5.Text = "UserName : " + GlobalVariable.gUserName;
                rv.Show();

            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }


        public void SalesHistory()
        {
            int i;
            String sqlstring, ssql3;
            String[] TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();

            SalesHistory r = new SalesHistory();
            POSNAME = "";


            sqlstring = " SELECT ITEMCODE,ITEMDESC,SUM(QTY) AS TODAYQTY,SUM(AMOUNT) AS TOAYAMOUNT,0 AS MONTHQTY,0 AS MONTHAMOUNT,0 AS YEARQTY,0 AS YEARAMOUNT FROM VIEW_SSALEShISTORY ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and POSDESC IN (";
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + "group by ITEMCODE, ITEMDESC";


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
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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


        public void SUBGROUPWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            String[] POSdesc, MemberCode, GRPCODE, TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            ITEMWISEDETAILSWDgroupwise r = new ITEMWISEDETAILSWDgroupwise();
            POSNAME = "";
           

            sqlstring = " SELECT ITEMCODE,ITEMDESC,subGroupdesc,Rate,SUM(QTY) AS QTY,UOM,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(AMT) AS AMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess,(isnull(PURCHASERATE,0)) as PURCHASERATE FROM VIEWITEMWISESALESUMMARY_JIC ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
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
            //if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)
            //{
            //    sqlstring = sqlstring + " and PAYMENTMODE in (";
            //    HNAME = "(";
            //    for (i = 0; i <= CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            //    {
            //        sqlstring = sqlstring + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //    }
            //    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            //    sqlstring = sqlstring + ")";
            //    HNAME = HNAME.Remove(HNAME.Length - 2);
            //    HNAME = HNAME + ")";
            //}
            //else
            //{
            //    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + " GROUP BY ITEMCODE,itemdesc,Rate,UOM ,subGroupdesc,PURCHASERATE  ORDER BY " + Cmb_Order.Text.ToString().Trim() + "";

            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERYGROUPWISE";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            //if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)
            //{
            //    ssql3 = ssql3 + " and PAYMENTMODE in (";
            //    HNAME = "(";
            //    for (i = 0; i < CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            //    {
            //        ssql3 = ssql3 + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //    }
            //    ssql3 = ssql3.Remove(ssql3.Length - 2);
            //    ssql3 = ssql3 + ")";
            //    HNAME = HNAME.Remove(HNAME.Length - 2);
            //    HNAME = HNAME + ")";
            //}
            //else
            //{
            //    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            if (lstsubgroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (LstGroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (chklist_Type.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "VIEWITEMWISESALESUMMARY_JIC");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERYGROUPWISE");
            if (GlobalVariable.gdataset.Tables["VIEWITEMWISESALESUMMARY_JIC"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEWITEMWISESALESUMMARY_JIC", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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

        public void GROUPWISE()
        {
            String[] servercode;
            int i;
            String sqlstring,  ssql3;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            String[]  TYPE;
            TextObject txtobj1, TXTOBJ10;
            ITEMWISEGROUPDETAILS r = new ITEMWISEGROUPDETAILS();
            POSNAME = "";


            sqlstring = " SELECT ITEMCODE,ITEMDESC,Groupdesc,Rate,SUM(QTY) AS QTY,UOM,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(AMT) AS AMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess,(isnull(PURCHASERATE,0)) as PURCHASERATE FROM VIEWITEMWISESALESUMMARY_JIC ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + " GROUP BY ITEMCODE,itemdesc,Rate,UOM ,Groupdesc,PURCHASERATE  ORDER BY " + Cmb_Order.Text.ToString().Trim() + "";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERYGROUPWISE";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
            if (lstsubgroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (LstGroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (chklist_Type.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "VIEWITEMWISESALESUMMARY_JIC");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERYGROUPWISE");
            if (GlobalVariable.gdataset.Tables["VIEWITEMWISESALESUMMARY_JIC"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEWITEMWISESALESUMMARY_JIC", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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

        public void CATEGORYWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql3;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            String[] TYPE;
            TextObject txtobj1, TXTOBJ10;
            ITEMWISECATEGORYDETAILS r = new ITEMWISECATEGORYDETAILS();
            POSNAME = "";


            sqlstring = " SELECT ITEMCODE,ITEMDESC,CATEGORY,Rate,SUM(QTY) AS QTY,UOM,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(AMT) AS AMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess,(isnull(PURCHASERATE,0)) as PURCHASERATE FROM VIEWITEMWISESALESUMMARY_JIC ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + " GROUP BY ITEMCODE,itemdesc,Rate,UOM ,CATEGORY,PURCHASERATE  ORDER BY " + Cmb_Order.Text.ToString().Trim() + "";


            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERYGROUPWISE";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            if (lstsubgroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (LstGroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (chklist_Type.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "VIEWITEMWISESALESUMMARY_JIC");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERYGROUPWISE");
            if (GlobalVariable.gdataset.Tables["VIEWITEMWISESALESUMMARY_JIC"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEWITEMWISESALESUMMARY_JIC", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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
        public void POS_GROUPWISE()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            String[] POSdesc, MemberCode, GRPCODE, TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            ITEMWISEDETAILSWD_POSWISE r = new ITEMWISEDETAILSWD_POSWISE();
            POSNAME = "";

            sqlstring = " SELECT ITEMCODE,ITEMDESC,POSDESC,SUM(QTY) AS QTY,UOM,sum(SCHARGE)as SCHARGE,sum(TAXAMOUNT)as TAXAMOUNT,sum(PACKAMOUNT)as PACKAMOUNT,SUM(AMT) AS AMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM VIEWITEMWISESALESUMMARY_JIC ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
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
            ////if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)
            ////{
            ////    sqlstring = sqlstring + " and PAYMENTMODE in (";
            ////    HNAME = "(";
            ////    for (i = 0; i < CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////    HNAME = HNAME.Remove(HNAME.Length - 2);
            ////    HNAME = HNAME + ")";
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            ////}
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + " GROUP BY ITEMCODE,itemdesc,UOM ,POSDESC ORDER BY " + Cmb_Order.Text.ToString().Trim() + "";

            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERYGROUPWISE";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            //if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)
            //{
            //    ssql3 = ssql3 + " and PAYMENTMODE in (";
            //    HNAME = "(";
            //    for (i = 0; i <= CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            //    {
            //        ssql3 = ssql3 + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            //    }
            //    ssql3 = ssql3.Remove(ssql3.Length - 2);
            //    ssql3 = ssql3 + ")";
            //    HNAME = HNAME.Remove(HNAME.Length - 2);
            //    HNAME = HNAME + ")";
            //}
            //else
            //{
            //    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            if (lstsubgroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (LstGroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (chklist_Type.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "VIEWITEMWISESALESUMMARY_JIC");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERYGROUPWISE");
            if (GlobalVariable.gdataset.Tables["VIEWITEMWISESALESUMMARY_JIC"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEWITEMWISESALESUMMARY_JIC", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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
        public void ITEMWISESUMMARYWD()
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            String[] POSdesc, MemberCode, GRPCODE,TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;
            ITEMWISEDETAILSWD r = new ITEMWISEDETAILSWD();
            POSNAME = "";

            sqlstring = " SELECT ITEMCODE,ITEMDESC,SUM(QTY) AS QTY,UOM,sum(TAXAMOUNT)as TAXAMOUNT,sum(SCHARGE)as SCHARGE,sum(PACKAMOUNT)as PACKAMOUNT,SUM(AMT) AS AMOUNT,SUM(isnull(ACHARGE,0)) AS ACHARGE ,SUM(isnull(PCHARGE,0)) AS PCHARGE,SUM(isnull(RCHARGE,0)) AS RCHARGE,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess FROM VIEWITEMWISESALESUMMARY_JIC ";
            sqlstring = sqlstring + "  where CAST(CONVERT(VARCHAR,KOTDATE,106)AS DATETIME) BETWEEN '";
            sqlstring = sqlstring + this.mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + this.mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            ////if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)                
            ////{
            ////    sqlstring = sqlstring + " and PAYMENTMODE in (";
            ////    HNAME = "(";
            ////    for (i = 0; i < CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            ////    {
            ////        sqlstring = sqlstring + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////    }
            ////    sqlstring = sqlstring.Remove(sqlstring.Length - 2);
            ////    sqlstring = sqlstring + ")";
            ////    HNAME = HNAME.Remove(HNAME.Length - 2);
            ////    HNAME = HNAME+ ")";
            ////}
            ////  else {
            ////    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            ////}
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
            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";
                
                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";
                    
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            sqlstring = sqlstring + " GROUP BY ITEMCODE,itemdesc,UOM ,itemcode  ORDER BY " + Cmb_Order.Text.ToString().Trim() + "";
           
            //  rv.crystalReportViewer1.ReportSource =r;

            ssql3 = "SELECT ISNULL(TAXCODE,'')AS TAXCODE,ISNULL(TAXDESC,'')AS TAXDESC,ISNULL(taxpercent,0)AS TAXPERCENT,SUM(TAXAMOUNT)AS TAXAMOUNT FROM NANO_TAXGROUPINGSUMMERYGROUPWISE";
            ssql3 = ssql3 + " where kotdate between '";
            ssql3 = ssql3 + mskFromdate.Value.ToString("dd-MMM-yyyy") + "' AND '" + mskTodate.Value.ToString("dd-MMM-yyyy") + "'";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " AND POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            ////if (CHKLIST_PAYMENTMODE.CheckedItems.Count != 0)
            ////{
            ////    ssql3 = ssql3 + " and PAYMENTMODE in (";
            ////    HNAME = "(";
            ////    for (i = 0; i < CHKLIST_PAYMENTMODE.CheckedItems.Count - 1; i++)
            ////    {
            ////        ssql3 = ssql3 + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////        HNAME = HNAME + " '" + CHKLIST_PAYMENTMODE.CheckedItems[i] + "', ";
            ////    }
            ////    ssql3 = ssql3.Remove(ssql3.Length - 2);
            ////    ssql3 = ssql3 + ")";
            ////    HNAME = HNAME.Remove(HNAME.Length - 2);
            ////    HNAME = HNAME + ")";
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Select the PAYMENTMODE(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            ////}
            if (lstsubgroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }
            else
            {
                MessageBox.Show("Select the SUBGROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (LstGroup.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (lstcategory.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    ssql3 = ssql3 + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the Category(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            if (chklist_Type.CheckedItems.Count != 0)
            {
                ssql3 = ssql3 + " and itemcode in (";

                for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                {
                    TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                    ssql3 = ssql3 + " '" + TYPE[0] + "', ";

                }
                ssql3 = ssql3.Remove(ssql3.Length - 2);
                ssql3 = ssql3 + ")";

            }
            else
            {
                MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            ssql3 = ssql3 + " GROUP BY taxcode,TAXDESC ,taxpercent  ORDER BY taxpercent";
            GCON.getDataSet1(sqlstring, "VIEWITEMWISESALESUMMARY_JIC");
            GCON.getDataSet1(ssql3, "NANO_TAXGROUPINGSUMMERYGROUPWISE");
            if (GlobalVariable.gdataset.Tables["VIEWITEMWISESALESUMMARY_JIC"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "VIEWITEMWISESALESUMMARY_JIC", r);
                rv.GetDetails(ssql3, "NANO_TAXGROUPINGSUMMERY", r);



                r.SetDataSource(GlobalVariable.gdataset);

                rv.crystalReportViewer1.ReportSource = r;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)r.ReportDefinition.ReportObjects["Text3"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ16;
                TXTOBJ16 = (TextObject)r.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ16.Text = "PERIOD FROM " + mskFromdate.Value.ToString("dd-MMM-yyyy") + "  TO" + " " + mskTodate.Value.ToString("dd-MMM-yyyy") + "";
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
            this.Hide();
        }

        private void Chk_SelectAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_SelectAllCategory.Checked == true)
            {
                for (i = 0; i < lstcategory.Items.Count ; i++)
                {
                    lstcategory.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < lstcategory.Items.Count ; i++)
                {
                    lstcategory.SetItemChecked(i, false);
                }
            }

        }

        private void Chk_SelectAllGroup_CheckedChanged(object sender, EventArgs e)
        {
              int i;
              if (Chk_SelectAllGroup.Checked == true)
              {
                  for (i = 0; i < LstGroup.Items.Count ; i++)
                  {
                      LstGroup.SetItemChecked(i, true);
                  }
              }
              else
              {
                  for (i = 0; i < LstGroup.Items.Count ; i++)
                  {
                      LstGroup.SetItemChecked(i, false);
                  }
              }
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

        private void Chk_PAYMENTMODE_CheckedChanged(object sender, EventArgs e)
        {
             int i;
            if (Chk_PAYMENTMODE.Checked == true)
            {
                for (i = 0; i < CHKLIST_PAYMENTMODE.Items.Count - 1; i++)
                {
                    CHKLIST_PAYMENTMODE.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < CHKLIST_PAYMENTMODE.Items.Count - 1; i++)
                {
                    CHKLIST_PAYMENTMODE.SetItemChecked(i, false);
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
            lstcategory.Items.Clear();
            LstGroup.Items.Clear();
            lstsubgroup.Items.Clear();
            chklist_Type.Items.Clear();
            Chk_SelectAllCategory.Checked = false;
            Chk_SelectAllGroup.Checked = false;
            chk_subgroupwise.Checked = false;
            Chk_Taxtype.Checked = false;
            Chk_SelectAllsubgroup.Checked = false;
            Chk_POSlocation.Checked = false;
            fillGroup();
            fillcategory();
            fillSubGroup();
            fillTaxType();
            fillPosLocations();
            chk_itemwise.Checked = false;
            CHK_GROUP.Checked = false;
            chk_subgroupwise.Checked = false;
            CHK_CATEGORY.Checked = false;
            chk_poswise.Checked = false;

            


        }

        private void chk_itemwise_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_itemwise.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                CHK_GROUP.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_poswise.Checked = false;
                chk_top10.Checked = false;
                chk_his.Checked = false;
            }
        }

        private void chk_poswise_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_poswise.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                CHK_GROUP.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_his.Checked = false;
                chk_itemwise.Checked = false;
                chk_top10.Checked = false;
            }
        }

        private void CHK_CATEGORY_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_CATEGORY.Checked == true)
            {
                CHK_GROUP.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_poswise.Checked = false;
                chk_itemwise.Checked = false;
                chk_top10.Checked = false;
                chk_his.Checked = false;
            }
        }

        private void CHK_GROUP_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_GROUP.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_poswise.Checked = false;
                chk_itemwise.Checked = false;
                chk_top10.Checked = false;
                chk_his.Checked = false;
            }
        }

        private void chk_subgroupwise_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_subgroupwise.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                CHK_GROUP.Checked = false;
                chk_poswise.Checked = false;
                chk_itemwise.Checked = false;
                chk_top10.Checked = false;
                chk_his.Checked = false;
            }
        }

        private void chk_top10_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_top10.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                CHK_GROUP.Checked = false;
                chk_poswise.Checked = false;
                chk_itemwise.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_his.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_his.Checked == true)
            {
                CHK_CATEGORY.Checked = false;
                CHK_GROUP.Checked = false;
                chk_poswise.Checked = false;
                chk_itemwise.Checked = false;
                chk_subgroupwise.Checked = false;
                chk_top10.Checked = false;
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
    

