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
    public partial class ReportForm : Form
    {
        GlobalClass GCon = new GlobalClass();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void Cmd_PosWiseRegister_Click(object sender, EventArgs e)
        {
            POSWISE PM = new POSWISE();
            PM.ShowDialog();
        }

        private void Cmd_ItemWise_Click(object sender, EventArgs e)
        {
            ITEMWISESALESREPORT IR = new ITEMWISESALESREPORT();
            IR.ShowDialog();
        }

        private void Cmd_Memberwise_Click(object sender, EventArgs e)
        {
            MEMBERWISE MW = new MEMBERWISE();
            MW.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SETTLEMENT ST = new SETTLEMENT();
            ST.ShowDialog();
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Close();
        }

        private void Cmd_DiscReg_Click(object sender, EventArgs e)
        {
            DISCREPORT DSR = new DISCREPORT();
            DSR.ShowDialog();
        }

        private void Cmd_NCReg_Click(object sender, EventArgs e)
        {
            NCREPORT NCR = new NCREPORT();
            NCR.ShowDialog();
        }

        private void Cmd_ConverReg_Click(object sender, EventArgs e)
        {
            DailyCovers DC = new DailyCovers();
            DC.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ITEMWISESALESHISTORY DC = new ITEMWISESALESHISTORY();
            DC.ShowDialog();
        }

        private void Cmd_MenuList_Click(object sender, EventArgs e)
        {
            MenuList ML = new MenuList();
            ML.ShowDialog();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            //GCon.GetBillCloseDate();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CancelOrderAndItem COI = new CancelOrderAndItem();
            COI.ShowDialog();
        }

        private void Cmd_DailyTransRpt_Click(object sender, EventArgs e)
        {
            DailyTransactionRpt DTR = new DailyTransactionRpt();
            DTR.ShowDialog();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "Node_PeriodicSumm")
            {
                PeriodicSummary PS = new PeriodicSummary();
                PS.ShowDialog();
            }
            else if (e.Node.Name == "Node_DaySumm")
            {
                DaySummary DS = new DaySummary();
                DS.ShowDialog();
            }
            else if (e.Node.Name == "Node_OpenCheck")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Open Check";
                RF.ReportNode = "Node_OpenCheck";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_TaxExemptedCheck")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Tax Exempted Check";
                RF.ReportNode = "Node_TaxExemptedCheck";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_SCExemptedCheck")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "SC Exempted Check";
                RF.ReportNode = "Node_SCExemptedCheck";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_DiscByItem")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Discount By Item";
                RF.ReportNode = "Node_DiscByItem";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_DiscByClint")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Discount By Client";
                RF.ReportNode = "Node_DiscByClint";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_SaleDetails")
            {
                SaleDetailsPOS SDP = new SaleDetailsPOS();
                SDP.ShowDialog();
            }
            else if (e.Node.Name == "Node_NCChkItemwise")
            {
                NCCHECKSITEMWISE NCI = new NCCHECKSITEMWISE();
                NCI.ShowDialog();
            }
            else if (e.Node.Name == "Node_SaleHistory")
            {
                SALESHISTORY SH = new SALESHISTORY();
                SH.ShowDialog();
            }
            else if (e.Node.Name == "Node_SalesHistory2")
            {
                SALESHISTORY2 SH = new SALESHISTORY2();
                SH.ShowDialog();
            }
            else if (e.Node.Name == "Node_MisChgSumm")
            {
                MISCHARGESUMMARY Dialog = new MISCHARGESUMMARY();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_MisCoverSumm")
            {
                MISCOVERSUMMARY Dialog = new MISCOVERSUMMARY();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_RevenueSumm")
            {
                REVENUESUMMARY Dialog = new REVENUESUMMARY();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_NonChgCheckDtl")
            {
                NCCHECKDTL Dialog = new NCCHECKDTL();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_NonChgChkSumm")
            {
                NONCHARECHECKSUM Dialog = new NONCHARECHECKSUM();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_ChkKOTRecon")
            {
                KOTRECONS Dialog = new KOTRECONS();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_TimeDiff")
            {
                TimeDifferenceReport Dialog = new TimeDifferenceReport();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_SettByMealType")
            {
                MEALSETTELBY Dialog = new MEALSETTELBY();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_RevByMealTypeDaily")
            {
                REVENUEBYMEALTYPE Dialog = new REVENUEBYMEALTYPE();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_AccSettlement")
            {
                POSACCOUNTSSETTLEMENET Dialog = new POSACCOUNTSSETTLEMENET();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_BSource")
            {
                BusinessSourceRpt Dialog = new BusinessSourceRpt();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_DuplicateBill")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Duplicate Bill Print Report";
                RF.ReportNode = "Node_DuplicateBill";
                RF.ShowDialog();
            }
            else if (e.Node.Name == "Node_DiscCheckByCategory")
            {
                DiscountedByCategory Dialog = new DiscountedByCategory();
                Dialog.ShowDialog();
            }
            else if (e.Node.Name == "Node_GuestPhone")
            {
                ReportFilter RF = new ReportFilter();
                RF.RName = "Guest Phone With Order";
                RF.ReportNode = "Node_GuestPhone";
                RF.ShowDialog();
            }
        }

        private void treeView1_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //e.Node.ForeColor = Color.Blue;
            
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e) 
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand; 
        }

        private void treeView1_MouseEnter(object sender, EventArgs e)
        {
            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
        }

        private void treeView1_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.No;
        }

        private void Cmd_PromoRpt_Click(object sender, EventArgs e)
        {
            PromotionRpt PRT = new PromotionRpt();
            PRT.ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Cmd_DayEndSummary_Click(object sender, EventArgs e)
        {
            DayEndSummary DES = new DayEndSummary();
            DES.ShowDialog();
        }

        private void Cmd_ItemWisedet_Click(object sender, EventArgs e)
        {
            Frm_ItemWiseDetails IWD = new Frm_ItemWiseDetails();
            IWD.ShowDialog();
        }

        private void Cmd_SaleRegisterItemwise_Click(object sender, EventArgs e)
        {
            SaleRregisterItemWise SRI = new SaleRregisterItemWise();
            SRI.ShowDialog();
        }

        private void Cmd_TableRpt_Click(object sender, EventArgs e)
        {
            TableWiseRpt TWR = new TableWiseRpt();
            TWR.ShowDialog();
        }

        private void Cmd_ItemWiseWithSteward_Click(object sender, EventArgs e)
        {
            ItemWiseWithSteward IWS = new ItemWiseWithSteward();
            IWS.ShowDialog();
        }

       
        
    }
}
