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
using TouchPOS.CRYSTAL;

namespace TouchPOS.REPORTS
{
    public partial class PromotionRpt : Form
    {
        GlobalClass GCon = new GlobalClass();

        public PromotionRpt()
        {
            InitializeComponent();
        }

        String ssql= "";

        private void PromotionRpt_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            Dtp_FromDate.Value = GlobalVariable.ServerDate;
            Dtp_ToDate.Value = GlobalVariable.ServerDate;
            fillGroup();
            fillTaxType();
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void fillGroup()
        {
            String sqlstring;
            LstGroup.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT Groupcode,Groupdesc FROM GroupMaster ";
            GCon.getDataSet1(sqlstring, "GroupMaster");
            if (GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count; i++)
                {
                    LstGroup.Items.Add(GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupcode").Trim() + "-" + GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupdesc").Trim());
                }
            }
        }
        public void fillTaxType()
        {
            String sqlstring;
            chklist_Type.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT itemcode,itemdesc FROM ITEMMaster ORDER BY ITEMDESC";
            GCon.getDataSet1(sqlstring, "ITEMMaster");
            if (GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count; i++)
                {
                    chklist_Type.Items.Add(GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("itemcode").Trim() + "-" + GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("itemdesc").Trim());
                }
            }
            chklist_Type.Sorted = true;
        }

        private void Chk_SelectAllGroup_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_SelectAllGroup.Checked == true)
            {
                for (i = 0; i < LstGroup.Items.Count; i++)
                {
                    LstGroup.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < LstGroup.Items.Count; i++)
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
                for (i = 0; i < chklist_Type.Items.Count; i++)
                {
                    chklist_Type.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_Type.Items.Count; i++)
                {
                    chklist_Type.SetItemChecked(i, false);
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring = "", Strsql = "";
            string HNAME, POSNAME, Catname;
            String[] TYPE;
            Report rv = new Report();
            if (Rdb_Details.Checked == true) 
            {
                if (Cmb_PType.Text.Trim() == "Qty")
                {
                    CRYSTAL.Crpt_PromoSale_LatQty RPS = new CRYSTAL.Crpt_PromoSale_LatQty();
                    Strsql = "SELECT KOTDATE,KOTDETAILS,BILLNO,P.ITEMCODE,P.ITEMDESC,Sum(SaleQty) As SaleQty,SUM(FreeQty) As FreeQty FROM Prom_SaleReport P,ItemMaster I Where Type1 = 'Q' AND P.ITEMCODE = I.ItemCode AND KOTDATE Between '" + Strings.Format((DateTime)Dtp_FromDate.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)Dtp_ToDate.Value, "dd-MMM-yyyy") + "' ";
                    if (LstGroup.CheckedItems.Count != 0)
                    {
                        Strsql = Strsql + " and GROUPCODE in (";
                        for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                        {
                            TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                            Strsql = Strsql + " '" + TYPE[0] + "', ";
                        }
                        Strsql = Strsql.Remove(Strsql.Length - 2);
                        Strsql = Strsql + ")";
                    }
                    else
                    {
                        MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (chklist_Type.CheckedItems.Count != 0)
                    {
                        Strsql = Strsql + " and P.itemcode in (";
                        for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                        {
                            TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                            Strsql = Strsql + " '" + TYPE[0] + "', ";
                        }
                        Strsql = Strsql.Remove(Strsql.Length - 2);
                        Strsql = Strsql + ")";
                    }
                    else
                    {
                        MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    Strsql = Strsql + " Group by KOTDATE,KOTDETAILS,BILLNO,P.ITEMCODE,P.ITEMDESC ORDER BY P.ITEMCODE";
                    GCon.getDataSet1(Strsql, "Prom_SaleReport");
                    if (GlobalVariable.gdataset.Tables["Prom_SaleReport"].Rows.Count > 0)
                    {
                        rv.GetDetails(Strsql, "Prom_SaleReport", RPS);
                        RPS.SetDataSource(GlobalVariable.gdataset);
                        rv.crystalReportViewer1.ReportSource = RPS;
                        rv.crystalReportViewer1.Zoom(100);

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                        TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text7"];
                        TXTOBJ1.Text = "Promotional " + Cmb_PType.Text.Trim() + " Sale From " + Strings.Format((DateTime)Dtp_FromDate.Value, "dd-MMM-yyyy") + " To " + Strings.Format((DateTime)Dtp_ToDate.Value, "dd-MMM-yyyy");

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                        TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                        TXTOBJ3.Text = GlobalVariable.gCompanyName;

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
                        TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text11"];
                        TXTOBJ4.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                        TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text10"];
                        TXTOBJ5.Text = "Business Date " + Strings.Format((DateTime)GlobalVariable.ServerDate, "yyyy-MM-dd") + " ";

                        rv.Show();
                    }
                    else
                    {
                        MessageBox.Show("No Records To Display..");
                    }
                }
                else 
                {
                    CRYSTAL.Crpt_PromoSale_LatRate RPS = new CRYSTAL.Crpt_PromoSale_LatRate();
                    if (Cmb_PType.Text.Trim() == "Fixed Rate") 
                    {
                        Strsql = "SELECT KOTDATE,KOTDETAILS,BILLNO,P.ITEMCODE,P.ITEMDESC,ActualRate,DiscountRate,Discount FROM Prom_SaleReport P,ItemMaster I Where Type1 = 'F' AND P.ITEMCODE = I.ItemCode AND KOTDATE Between '" + Strings.Format((DateTime)Dtp_FromDate.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)Dtp_ToDate.Value, "dd-MMM-yyyy") + "' ";
                    }
                    else if (Cmb_PType.Text.Trim() == "Discount Rate")
                    {
                        Strsql = "SELECT KOTDATE,KOTDETAILS,BILLNO,P.ITEMCODE,P.ITEMDESC,ActualRate,DiscountRate,Discount FROM Prom_SaleReport P,ItemMaster I Where Type1 = 'D' AND P.ITEMCODE = I.ItemCode AND KOTDATE Between '" + Strings.Format((DateTime)Dtp_FromDate.Value, "dd-MMM-yyyy") + "' AND '" + Strings.Format((DateTime)Dtp_ToDate.Value, "dd-MMM-yyyy") + "' ";
                    }
                    if (LstGroup.CheckedItems.Count != 0)
                    {
                        Strsql = Strsql + " and GROUPCODE in (";
                        for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                        {
                            TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                            Strsql = Strsql + " '" + TYPE[0] + "', ";
                        }
                        Strsql = Strsql.Remove(Strsql.Length - 2);
                        Strsql = Strsql + ")";
                    }
                    else
                    {
                        MessageBox.Show("Select the GROUPS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (chklist_Type.CheckedItems.Count != 0)
                    {
                        Strsql = Strsql + " and P.itemcode in (";
                        for (i = 0; i <= chklist_Type.CheckedItems.Count - 1; i++)
                        {
                            TYPE = chklist_Type.CheckedItems[i].ToString().Split('-');
                            Strsql = Strsql + " '" + TYPE[0] + "', ";
                        }
                        Strsql = Strsql.Remove(Strsql.Length - 2);
                        Strsql = Strsql + ")";
                    }
                    else
                    {
                        MessageBox.Show("Select the ITEMS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    Strsql = Strsql + " ORDER BY P.ITEMCODE";
                    GCon.getDataSet1(Strsql, "Prom_SaleReport");
                    if (GlobalVariable.gdataset.Tables["Prom_SaleReport"].Rows.Count > 0)
                    {
                        rv.GetDetails(Strsql, "Prom_SaleReport", RPS);
                        RPS.SetDataSource(GlobalVariable.gdataset);
                        rv.crystalReportViewer1.ReportSource = RPS;
                        rv.crystalReportViewer1.Zoom(100);

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                        TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text7"];
                        TXTOBJ1.Text = "Promotional " + Cmb_PType.Text.Trim() + " Sale From " + Strings.Format((DateTime)Dtp_FromDate.Value, "dd-MMM-yyyy") + " To " + Strings.Format((DateTime)Dtp_ToDate.Value, "dd-MMM-yyyy");

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                        TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                        TXTOBJ3.Text = GlobalVariable.gCompanyName;

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
                        TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text11"];
                        TXTOBJ4.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                        CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
                        TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text10"];
                        TXTOBJ5.Text = "Business Date " + Strings.Format((DateTime)GlobalVariable.ServerDate, "yyyy-MM-dd") + " ";

                        rv.Show();
                    }
                    else
                    {
                        MessageBox.Show("No Records To Display..");
                    }
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Chk_SelectAllGroup.Checked = false;
            Chk_Taxtype.Checked = false;
            Chk_SelectAllGroup_CheckedChanged(sender,e);
            Chk_Taxtype_CheckedChanged(sender, e);
            Dtp_FromDate.Value = GlobalVariable.ServerDate;
            Dtp_ToDate.Value = GlobalVariable.ServerDate;
        }
       
    }
}
