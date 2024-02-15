using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class TransferTable : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int LocationCode = 0;

        public TransferTable()
        {
            InitializeComponent();
        }

        string sql = "";
        DataTable Ocpd = new DataTable();
        DataTable Vct = new DataTable();

        private void TransferTable_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");

            sql = "SELECT LocName,TableNo,LocCode,ChairSeqNo FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' And LocCode = " + LocationCode + " Order by ChairSeqno";
            if (GlobalVariable.gCompName == "SKYYE")
            {
                sql = "SELECT LocName,H.TableNo,LocCode,ChairSeqNo FROM KOT_HDR H,TableMaster T WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' And LocCode = " + LocationCode + " And H.TableNo = T.TableNo Order by LocName,TableOrder";
            }
            Ocpd = GCon.getDataSet(sql);
            if (Ocpd.Rows.Count > 0) 
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in Ocpd.Rows)
                {
                    lst.Add(r["LocName"].ToString() + "/" + r["TableNo"].ToString() + "/" + r["ChairSeqNo"].ToString() + "/" + r["LocCode"].ToString());
                }
                FromListBox.Items.Clear();
                FromListBox.DataSource = lst;
            }

            sql = "SELECT S.LOCNAME,TABLENO,LocCode FROM TableMaster T,ServiceLocation_HDR S WHERE T.Pos = CAST(S.LocCode AS VARCHAR(10)) AND T.TableNo NOT IN (SELECT TableNo FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' ) And S.LocCode = " + LocationCode + " and ISNULL(T.Freeze,'') <> 'Y' order by TableOrder ";
            if (GlobalVariable.gCompName == "SKYYE") 
            {
                sql = "SELECT S.LOCNAME,TABLENO,LocCode FROM TableMaster T,ServiceLocation_HDR S WHERE T.Pos = CAST(S.LocCode AS VARCHAR(10)) AND T.TableNo NOT IN (SELECT TableNo FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' ) and ISNULL(T.Freeze,'') <> 'Y' and Loccode in (2,5,6) order by  S.LOCNAME,TableOrder ";
            }
            Vct = GCon.getDataSet(sql);
            if (Vct.Rows.Count > 0)
            {
                List<string> lst1 = new List<string>();
                foreach (DataRow r in Vct.Rows)
                {
                    lst1.Add(r["LocName"].ToString() + "/" + r["TableNo"].ToString() + "/" + r["LocCode"].ToString());
                }
                ToListBox.Items.Clear();
                ToListBox.DataSource = lst1;
            }
            FromListBox.Dock = DockStyle.Fill;
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            ServiceLocation SL = new ServiceLocation();
            SL.Show();
            this.Close();
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            string selectedItem = "";
            string toselectedItem = "";
            selectedItem = FromListBox.SelectedItem.ToString();
            toselectedItem = ToListBox.SelectedItem.ToString();

            if (selectedItem == toselectedItem)
            {
                MessageBox.Show("Sorry! your have Selected Same Location and Table");
                return;
            }
            //MessageBox.Show(toselectedItem);
            string[] FromItem = selectedItem.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string[] ToItem = toselectedItem.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList List = new ArrayList();
            string sqlstring = "";
            string KorderNo = "";

            KorderNo = Convert.ToString(GCon.getValue("SELECT Kotdetails FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + FromItem[1] + "' AND ISNULL(LocCode,0) = " + FromItem[3] + " AND ISNULL(ChairSeqNo,0) = " + FromItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' And Isnull(Delflag,'') <> 'Y'"));
            if (KorderNo != "") 
            {
                sqlstring = " UPDATE KOT_HDR SET TableNo = '" + ToItem[1] + "',LocCode = " + ToItem[2] + ",LocName = '" + ToItem[0] + "'  WHERE KOTDETAILS = '" + KorderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET SET TableNo = '" + ToItem[1] + "'  WHERE KOTDETAILS = '" + KorderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE PosTableStatus SET TableNo = '" + ToItem[1] + "'  WHERE ISNULL(TableNo,'') = '" + FromItem[1] + "' AND LocCode = " + FromItem[3] + " ";
                List.Add(sqlstring);

                if (GCon.Moretransaction(List) > 0) 
                {
                    MessageBox.Show("Transfer Sucessfully Done ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    ServiceLocation SL = new ServiceLocation();
                    SL.Show();
                    this.Close();
                }
            }
        }
    }
}
