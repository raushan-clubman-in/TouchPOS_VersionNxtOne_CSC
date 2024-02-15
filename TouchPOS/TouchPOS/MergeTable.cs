using Microsoft.VisualBasic;
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
    public partial class MergeTable : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int LocationCode = 0;

        public MergeTable()
        {
            InitializeComponent();
        }

        string sql = "";
        DataTable Ocpd = new DataTable();
        DataTable Ocpd2 = new DataTable();

        private void MergeTable_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");

            sql = "SELECT LocName,TableNo,LocCode,ChairSeqNo FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' And LocCode = " + LocationCode + " Order by 1";
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

            sql = "SELECT LocName,TableNo,LocCode,ChairSeqNo FROM KOT_HDR WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' And LocCode = " + LocationCode + " Order by 1 ";
            if (GlobalVariable.gCompName == "SKYYE")
            {
                sql = "SELECT LocName,H.TableNo,LocCode,ChairSeqNo FROM KOT_HDR H,TableMaster T WHERE CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' AND SERTYPE = 'Dine-In' And isnull(DelFlag,'') <> 'Y' And LocCode = " + LocationCode + " And H.TableNo = T.TableNo Order by LocName,TableOrder ";
            }
            Ocpd2 = GCon.getDataSet(sql);
            if (Ocpd2.Rows.Count > 0)
            {
                List<string> lst1 = new List<string>();
                foreach (DataRow r in Ocpd2.Rows)
                {
                    lst1.Add(r["LocName"].ToString() + "/" + r["TableNo"].ToString() + "/" + r["ChairSeqNo"].ToString() + "/" + r["LocCode"].ToString());
                }
                ToListBox.Items.Clear();
                ToListBox.DataSource = lst1;
            }
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

            string[] FromItem = selectedItem.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string[] ToItem = toselectedItem.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList List = new ArrayList();
            string sqlstring = "";
            string ForderNo = "";
            string TorderNo = "";
            string TKotNo = "";
            int TChairNo = 0;
            int TMaxSlno = 0;
            

            ForderNo = Convert.ToString(GCon.getValue("SELECT Kotdetails FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + FromItem[1] + "' AND ISNULL(LocCode,0) = " + FromItem[3] + " AND ISNULL(ChairSeqNo,0) = " + FromItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO' And Isnull(Delflag,'') <> 'Y' "));
            TorderNo = Convert.ToString(GCon.getValue("SELECT Kotdetails FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + ToItem[1] + "' AND ISNULL(LocCode,0) = " + ToItem[3] + " AND ISNULL(ChairSeqNo,0) = " + ToItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' "));
            TKotNo = Convert.ToString(GCon.getValue("SELECT KOTNO FROM KOT_HDR WHERE ISNULL(TableNo,'') = '" + ToItem[1] + "' AND ISNULL(LocCode,0) = " + ToItem[3] + " AND ISNULL(ChairSeqNo,0) = " + ToItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y' "));

            if (ForderNo != "" && TorderNo != "") 
            {
                TChairNo = Convert.ToInt16(GCon.getValue("SELECT ISNULL(ChairSeqNo,0) AS ChairSeqNo FROM KOT_HDR WHERE KOTDETAILS = '" + TorderNo + "' AND ISNULL(TableNo,'') = '" + ToItem[1] + "' AND ISNULL(LocCode,0) = " + ToItem[3] + " AND ISNULL(ChairSeqNo,0) = " + ToItem[2] + " AND CAST(CONVERT(VARCHAR(11),KOTDATE,106) AS DATETIME) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(BILLSTATUS,'') = 'PO'  And Isnull(Delflag,'') <> 'Y'"));
                TMaxSlno = Convert.ToInt16(GCon.getValue("SELECT ISNULL(MAX(ISNULL(SLNO,0)),0) FROM KOT_DET WHERE KOTDETAILS = '" + TorderNo + "'"));
                DataTable FromKotData = new DataTable();
                //sql = "SELECT KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                //sql = sql + " TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,isnull(KOTTYPE,'') as KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode, ";
                //sql = sql + " TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,HAPPYSTATUS,FinYear,isnull(ServiceOrder,0) as ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,BusinessSource,Isnull(QTY2,0) as QTY2,isnull(ItemPrintFlag,'N') as ItemPrintFlag FROM KOT_det WHERE KOTDETAILS = '" + ForderNo + "' And Isnull(DelFlag,'') <> 'Y' ";
                sql = "SELECT isnull(KOTNO,'') as KOTNO,isnull(KOTDETAILS,'') as KOTDETAILS,KOTDATE,isnull(BILLDETAILS,'') as BILLDETAILS,isnull(CATEGORY,'') as CATEGORY,isnull(ITEMCODE,'') as ITEMCODE,isnull(ITEMDESC,'') as ITEMDESC,isnull(GROUPCODE,'') as GROUPCODE,isnull(ITEMTYPE,'') as ITEMTYPE,isnull(POSCODE,'') as POSCODE,isnull(UOM,'') as UOM,isnull(QTY,0) as QTY,isnull(RATE,0) as RATE,isnull(AMOUNT,0) as AMOUNT,isnull(TAXTYPE,'') as TAXTYPE,isnull(TAXCODE,'') as TAXCODE,isnull(TAXAMOUNT,0) as TAXAMOUNT, ";
                sql = sql + " isnull(TAXACCOUNTCODE,'') as TAXACCOUNTCODE,isnull(KOTSTATUS,'') as KOTSTATUS,isnull(MCODE,'') as MCODE,isnull(SCODE,'') as SCODE,isnull(COVERS,0) as COVERS,isnull(TABLENO,'') as TABLENO,isnull(KOTTYPE,'') as KOTTYPE,isnull(PAYMENTMODE,'') as PAYMENTMODE,isnull(DelFlag,'') as DelFlag,isnull(AddUserid,'') as AddUserid,isnull(Adddatetime,'') as Adddatetime,isnull(UpdUserid,'') as UpdUserid,isnull(PACKPERCENT,0) as PACKPERCENT,isnull(PACKAMOUNT,0) as PACKAMOUNT,isnull(PROMOTIONALST,'') as PROMOTIONALST,isnull(SUBGroupCode,'') as SUBGroupCode , ";
                sql = sql + " isnull(TIPSPER,0) as TIPSPER,isnull(TipsAmt,0) as TipsAmt,isnull(AdCgsPer,0) as AdCgsPer,isnull(AdCgsAmt,0) as AdCgsAmt,isnull(PartyPer,0) as PartyPer,isnull(PartyAmt,0) as PartyAmt,isnull(RoomPer,0) as RoomPer,isnull(RoomAmt,0) as RoomAmt,isnull(MKOTNO,'') as MKOTNO,isnull(SLNO,0) as SLNO,isnull(Modifier,'') as Modifier,isnull(OrderNo,0) as OrderNo,isnull(HAPPYSTATUS,'') as HAPPYSTATUS,isnull(FinYear,'') as FinYear,isnull(ServiceOrder,0) as ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,isnull(BusinessSource,'') as BusinessSource,Isnull(QTY2,0) as QTY2,isnull(ItemPrintFlag,'N') as ItemPrintFlag FROM KOT_det WHERE KOTDETAILS = '" + ForderNo + "' And Isnull(DelFlag,'') <> 'Y' ";
                FromKotData = GCon.getDataSet(sql);
                if (FromKotData.Rows.Count > 0) 
                {
                    for (int i = 0; i < FromKotData.Rows.Count; i++) 
                    {
                        DataRow dr = FromKotData.Rows[i];
                        sqlstring = "INSERT INTO KOT_det (KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                        sqlstring = sqlstring + "TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode,";
                        sqlstring = sqlstring + "TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,RefOrderNo,HAPPYSTATUS,FinYear,ServiceOrder,ModifierCharges,BusinessSource,QTY2,ItemPrintFlag)";
                        sqlstring = sqlstring + " VALUES('" + TKotNo + "','" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["BILLDETAILS"] + "','" + dr["CATEGORY"] + "','" + dr["ITEMCODE"] + "','" + dr["ITEMDESC"] + "','" + dr["GROUPCODE"] + "','" + dr["ITEMTYPE"] + "', ";
                        sqlstring = sqlstring + " '" + dr["POSCODE"] + "','" + dr["UOM"] + "'," + dr["QTY"] + "," + dr["RATE"] + "," + dr["AMOUNT"] + ",'SALES',''," + dr["TAXAMOUNT"] + ", ";
                        sqlstring = sqlstring + " '','" + dr["KOTSTATUS"] + "','" + dr["MCODE"] + "','" + dr["SCODE"] + "','" + dr["COVERS"] + "','" + ToItem[1] + "','" + dr["KOTTYPE"] + "','','" + dr["DelFlag"] + "','" + dr["AddUserid"] + "','" + Strings.Format(dr["Adddatetime"], "dd-MMM-yyyy HH:mm:ss") + "','" + GlobalVariable.gUserName + "'," + dr["PACKPERCENT"] + "," + dr["PACKAMOUNT"] + ",'" + dr["PROMOTIONALST"] + "','" + dr["SUBGroupCode"] + "', ";
                        sqlstring = sqlstring + " " + dr["TIPSPER"] + "," + dr["TipsAmt"] + "," + dr["AdCgsPer"] + "," + dr["AdCgsAmt"] + "," + dr["PartyPer"] + "," + dr["PartyAmt"] + "," + dr["RoomPer"] + "," + dr["RoomAmt"] + ",''," + (TMaxSlno + Convert.ToInt16(dr["SLNO"])) + ",'" + dr["Modifier"] + "'," + Convert.ToInt16(dr["OrderNo"]) + ",'" + ForderNo + "','" + dr["HAPPYSTATUS"] + "','" + dr["FinYear"] + "'," + Convert.ToInt16(dr["ServiceOrder"]) + "," + dr["ModifierCharges"] + ",'" + dr["BusinessSource"] + "'," + dr["QTY2"] + ",'" + dr["ItemPrintFlag"] + "') ";
                        List.Add(sqlstring);
                    }
                }
                DataTable FromTaxData = new DataTable();
                sql = "SELECT KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear FROM KOT_DET_TAX WHERE KOTDETAILS = '" + ForderNo + "' And Isnull(VOID,'') <> 'Y' ";
                FromTaxData = GCon.getDataSet(sql);
                if (FromTaxData.Rows.Count > 0) 
                {
                    for (int i = 0; i < FromTaxData.Rows.Count; i++) 
                    {
                        DataRow dr = FromTaxData.Rows[i];
                        sqlstring = "INSERT INTO KOT_DET_TAX (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO,FinYear) VALUES ( ";
                        sqlstring = sqlstring + " '" + TorderNo + "','" + Strings.Format(dr["KOTDATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["TTYPE"] + "','" + dr["CHARGECODE"] + "','" + dr["TYPE_CODE"] + "','" + dr["POSCODE"] + "','" + dr["ITEMCODE"] + "','" + dr["KOTSTATUS"] + "', ";
                        sqlstring = sqlstring + " '" + dr["TAXCODE"] + "','" + dr["TAXON"] + "'," + dr["RATE"] + "," + dr["QTY"] + "," + dr["TAXPERCENT"] + "," + dr["TAXAMT"] + ",'" + dr["ADD_USER"] + "','" + Strings.Format(dr["ADD_DATE"], "dd-MMM-yyyy HH:mm:ss") + "','" + dr["VOID"] + "','" + dr["VOIDUSER"] + "', " + (TMaxSlno + Convert.ToInt16(dr["SLNO"])) + ",'" + dr["FinYear"] + "')";
                        List.Add(sqlstring);
                    }
                }

                sqlstring = " UPDATE KOT_HDR SET DelFlag = 'Y',DELUSER = '" + GlobalVariable.gUserName + "' ,DELDATETIME = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + ForderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET SET DelFlag = 'Y' ,KotStatus = 'Y',UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',RefOrderNo = '" + TorderNo + "' WHERE KOTDETAILS = '" + ForderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET_TAX SET KOTSTATUS = 'Y',VOID = 'Y',VOIDUSER= '" + GlobalVariable.gUserName + "',VOIDDATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + ForderNo + "' ";
                List.Add(sqlstring);

                if (GCon.Moretransaction(List) > 0) 
                {
                    MessageBox.Show("Merge Sucessfully Done ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (TorderNo) + "'";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                    }
                    ServiceLocation SL = new ServiceLocation();
                    SL.Show();
                    this.Close();
                }
            }
        }
    }
}
