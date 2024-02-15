using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.REPORTS
{
    public partial class DiscountedByCategory : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DiscountedByCategory()
        {
            InitializeComponent();
        }

        private void DiscountedByCategory_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillPosLocations();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void FillPosLocations()
        {
            String sqlstring;
            chklist_POSlocation.Items.Clear();
            int i;
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC FROM posmaster ";
            GCon.getDataSet1(sqlstring, "posmaster");
            if (GlobalVariable.gdataset.Tables["posmaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["posmaster"].Rows.Count; i++)
                {
                    chklist_POSlocation.Items.Add(GlobalVariable.gdataset.Tables["posmaster"].Rows[i].Field<String>("POSDESC").Trim());
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Chk_POSlocation.Checked = false;
            FillPosLocations();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            CRYSTAL.Crpt_DiscByCategory RPS = new CRYSTAL.Crpt_DiscByCategory();
            POSNAME = "";
            string SSQL;
            ////SSQL = "EXEC GROUPWISESALE '" + this.dtp1.Value.ToString("dd-MMM-yyyy") + "','" + this.dtp2.Value.ToString("dd-MMM-yyyy") + "','N'";
            ////GCon.ExecuteStoredProcedure(SSQL);

            sqlstring = " select KOTDETAILS,KOTDATE,Covers,Adduserid,Sum(Amount) as Amount,Sum(Food) as Food,Sum(Liquor) as Liquor,SUm(Wine) as Wine,Sum(Cocktails) as Cocktails,Sum(Others) as Others,DiscCategory,DiscUser from DiscountByCategory ";
            sqlstring = sqlstring + " where KOTDATE Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' and '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' ";
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " And POSDesc IN (";
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
                return;
            }
            sqlstring = sqlstring + "  group by KOTDETAILS,KOTDATE,Covers,Adduserid,DiscCategory,DiscUser ";
            GCon.getDataSet1(sqlstring, "DiscountByCategory");
            if (GlobalVariable.gdataset.Tables["DiscountByCategory"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "DiscountByCategory", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text13"];
                TXTOBJ1.Text = "Discounted Checks By Category Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text12"];
                TXTOBJ3.Text = GlobalVariable.gCompanyName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
                TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text17"];
                TXTOBJ4.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text18"];
                TXTOBJ5.Text = "Business Date " + Strings.Format((DateTime)GlobalVariable.ServerDate, "yyyy-MM-dd") + " ";

                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }
        }

        private void Chk_POSlocation_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_POSlocation.Checked == true)
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, false);
                }
            }
        }
    }
}
