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
    public partial class CancelOrderAndItem : Form
    {
        GlobalClass GCon = new GlobalClass();

        public CancelOrderAndItem()
        {
            InitializeComponent();
        }

        string sqlstring= "";

        private void CancelOrderAndItem_Load(object sender, EventArgs e)
        {
            FillPos();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        public void FillPos()
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

        private void Chk_OrderCancel_CheckedChanged(object sender, EventArgs e)
        {
            Chk_ItemCancel.Checked = false;
        }

        private void Chk_ItemCancel_CheckedChanged(object sender, EventArgs e)
        {
            Chk_OrderCancel.Checked = false;
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            if (Chk_OrderCancel.Checked == true) 
            {
                CRYSTAL.CancelOrder CO = new CRYSTAL.CancelOrder();
                POSNAME = "";
                sqlstring = " SELECT * From DeletedOrder Where Kotdate between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' ";

                if (POS_LIST.CheckedItems.Count != 0)
                {
                    sqlstring = sqlstring + " AND POSDESC IN (";
                    for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
                    {
                        sqlstring = sqlstring + " '" + POS_LIST.CheckedItems[i] + "', ";
                        POSNAME = POSNAME + POS_LIST.CheckedItems[i] + ", ";
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

                GCon.getDataSet1(sqlstring, "DeletedOrder");
                if (GlobalVariable.gdataset.Tables["DeletedOrder"].Rows.Count > 0)
                {
                    rv.GetDetails(sqlstring, "DeletedOrder", CO);
                    CO.SetDataSource(GlobalVariable.gdataset);
                    rv.crystalReportViewer1.ReportSource = CO;
                    rv.crystalReportViewer1.Zoom(100);
                    CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                    TXTOBJ1 = (TextObject)CO.ReportDefinition.ReportObjects["Text11"];
                    TXTOBJ1.Text = "Cancel Order Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";
                    CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                    TXTOBJ3 = (TextObject)CO.ReportDefinition.ReportObjects["Text10"];
                    TXTOBJ3.Text = GlobalVariable.gCompanyName;
                    rv.Show();
                }
                else
                {
                    MessageBox.Show("No Records To Display..");
                }
            }
            else if (Chk_ItemCancel.Checked == true) 
            {
                CRYSTAL.CancelItem CO = new CRYSTAL.CancelItem();
                POSNAME = "";
                sqlstring = " SELECT * From CancelOrderItem Where Kotdate between '" + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + "' And '" + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + "' ";

                if (POS_LIST.CheckedItems.Count != 0)
                {
                    sqlstring = sqlstring + " AND POSDESC IN (";
                    for (i = 0; i <= POS_LIST.CheckedItems.Count - 1; i++)
                    {
                        sqlstring = sqlstring + " '" + POS_LIST.CheckedItems[i] + "', ";
                        POSNAME = POSNAME + POS_LIST.CheckedItems[i] + ", ";
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

                GCon.getDataSet1(sqlstring, "CancelOrderItem");
                if (GlobalVariable.gdataset.Tables["CancelOrderItem"].Rows.Count > 0)
                {
                    rv.GetDetails(sqlstring, "CancelOrderItem", CO);
                    CO.SetDataSource(GlobalVariable.gdataset);
                    rv.crystalReportViewer1.ReportSource = CO;
                    rv.crystalReportViewer1.Zoom(100);
                    CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                    TXTOBJ1 = (TextObject)CO.ReportDefinition.ReportObjects["Text11"];
                    TXTOBJ1.Text = "Cancel Item Peroid " + Strings.Format((DateTime)dtp1.Value, "dd-MMM-yyyy") + " And " + Strings.Format((DateTime)dtp2.Value, "dd-MMM-yyyy") + " ";
                    CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                    TXTOBJ3 = (TextObject)CO.ReportDefinition.ReportObjects["Text10"];
                    TXTOBJ3.Text = GlobalVariable.gCompanyName;
                    rv.Show();
                }
                else
                {
                    MessageBox.Show("No Records To Display..");
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FillPos();
            checkBox1.Checked = false;
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }


    }
}
