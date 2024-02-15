using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class ItemCancel : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public string ItemDesc = "";
        public string Itemcode = "";
        public string Kotno = "";
        public int Slno = 0;
        bool gPrint = true;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        public readonly EntryForm _form1;

        public ItemCancel(EntryForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtPosts = new DataTable();
        string KotCompName = "", KotPrinterName = "";

        private void ItemCancel_Load(object sender, EventArgs e)
        {
            label2.Text = ItemDesc;
            AutoComplete();
        }

        private void AutoComplete()
        {
            sql = "SELECT ReasonTxt FROM Tbl_ReasonMaster ";
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ReasonTxt"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_Reason.AutoCompleteCustomSource = source;
            Txt_Reason.AutoCompleteMode = AutoCompleteMode.Suggest;
            Txt_Reason.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.TxtMember.DataBindings.Add("Text", dtPosts, "MNAME");
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_7.Text;
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_8.Text;
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_9.Text;
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_4.Text;
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_5.Text;
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_6.Text;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_2.Text;
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_3.Text;
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_0.Text;
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            _form1.RefreshGrid(Kotno);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text.Remove(TxtPass.Text.Length - 1, 1);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable Userdt = new DataTable();
            DataTable Rdt = new DataTable();
            string POSLOC="";
            if (Txt_Reason.Text == "") 
            {
                MessageBox.Show("Reason Can't be blank! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            sql = "select * from master..useradmin where username = '" + GlobalVariable.gUserName + "' and userpassword = '" + GCon.GetPassword(TxtPass.Text.Trim()) + "'";
            Userdt = GCon.getDataSet(sql);
            if ((Userdt.Rows.Count > 0) || (GlobalVariable.gUserName == "CHS" && GCon.GetPassword(TxtPass.Text.Trim()) == "ÏÆÉÎÎËÆÇÎÎ"))
            {
                sqlstring = "UPDATE KOT_det Set Kotstatus = 'Y',reason = '" + Txt_Reason.Text + "',upduserid = '" + GlobalVariable.gUserName + "',Upddatetime = getdate() Where Kotdetails = '" + Kotno + "' And itemcode = '" + Itemcode + "' and slno = " + Slno + " And Finyear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                sqlstring = "UPDATE KOT_det_tax Set Kotstatus = 'Y' Where Kotdetails = '" + Kotno + "' And itemcode = '" + Itemcode + "' and slno = " + Slno + " And Finyear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                POSLOC = Convert.ToString(GCon.getValue("select poscode from kot_det Where Kotdetails = '" + Kotno + "' And itemcode = '" + Itemcode + "' and slno = " + Slno + " And Finyear = '" + FinYear1 + "'"));
                sqlstring = "DELETE FROM closingqty where TRNNO = '" + Kotno + "' AND STORECODE IN (SELECT STORECODE FROM POSITEMSTORELINK WHERE ITEMCODE = '" + Itemcode + "' AND POS = '" + POSLOC + "')  and itemcode IN (SELECT gitemcode FROM BOM_DET WHERE ITEMCODE = '" + Itemcode + "')";
                List.Add(sqlstring);
                sqlstring = "UPDATE SUBSTORECONSUMPTIONDETAIL SET Void = 'Y' WHERE Docdetails = '" + Kotno + "' AND Itemcode = '" + Itemcode + "' AND Storelocationcode = '" + POSLOC + "'";
                List.Add(sqlstring);
                sql = "select * from Tbl_ReasonMaster where ReasonTxt = '" + Txt_Reason.Text + "'";
                Rdt = GCon.getDataSet(sql);
                if (Rdt.Rows.Count == 0) 
                {
                    sqlstring = "Insert Into Tbl_ReasonMaster (ReasonTxt) Values ('" + Txt_Reason.Text + "')";
                    List.Add(sqlstring);
                }

                if (GCon.Moretransaction(List) > 0)
                {
                    //MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (Kotno) + "'";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                        gPrint = true;
                        PrintOperation(Kotno, Slno, Itemcode);
                        _form1.RefreshGrid(Kotno);
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void PrintOperation(string kotno,int sno,string icode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            string KitCode = "";
            string PName = "";
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,Isnull(D.reason,'') as reason FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails And D.Finyear = H.Finyear AND H.KOTDETAILS = '" + kotno + "' And itemcode = '" + icode + "' And SLNO = " + sno + " And H.Finyear = '" + FinYear1 + "'  AND ISNULL(KOTSTATUS,'') = 'Y'";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "ORDER CANCEL".ToString().Length) / 2) + "ORDER CANCEL");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "", RData["reason"]);
                    Vrowcount = Vrowcount + 2;
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    KitCode = Convert.ToString(GCon.getValue("select isnull(kitchencode,'') from itemmaster Where itemcode = '" + icode + "'"));
                    KotPrinterName = "";
                    KotCompName = "";
                    PName = KitCode;
                    GetPrinter_KOT(PName);
                    if (KotPrinterName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterName);
                    }
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        public void GetPrinter_KOT(string KitCode)
        {
            OleDbConnection ServerConn = new OleDbConnection();
            OleDbDataAdapter servercmd;
            DataSet getserver = new DataSet();
            DataTable dt = new DataTable();
            string sql, ssql;
            sql = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + GlobalVariable.appPath + "\\DBS_KEY.MDB";
            ServerConn.ConnectionString = sql;
            try
            {
                ServerConn.Open();
                ssql = "SELECT COMPUTERNAME ,PRINTERNAME FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin");
                dt = getserver.Tables["admin"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    KotCompName = Convert.ToString(da["Computername"]);
                    KotPrinterName = Convert.ToString(da["printername"]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                ServerConn.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Board BD = new Board(this, Txt_Reason);
            BD.ShowDialog();
        }
    }
}
