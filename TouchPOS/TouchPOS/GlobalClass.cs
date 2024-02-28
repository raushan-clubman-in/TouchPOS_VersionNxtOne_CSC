using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing;

namespace TouchPOS
{
    class GlobalClass
    {
        SqlConnection Myconn = new SqlConnection();
        SqlTransaction MyTrans;
        SqlCommand Cmd = new SqlCommand();
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        private static String[] units = { "Zero", "One", "Two", "Three",  
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",  
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",  
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",  
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" }; 

        public void OpenConnection() 
        { 
            ConnectionString css = new ConnectionString();
            try
            {
                Myconn.ConnectionString = css.ReadCS();
            }
            catch{}
        }
        public string GetConnection() 
        {
            OpenConnection();
            return Myconn.ConnectionString;
        }

        public DataTable getDataSet(string Sqlstring) 
        {
            OpenConnection();
            SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter sqldtp = new SqlDataAdapter(Sqlstring, conn);
            sqldtp.SelectCommand.CommandType = CommandType.Text;
            sqldtp.SelectCommand.CommandTimeout = 100000;
            DataSet ds = new DataSet();
            try
            {
                sqldtp.Fill(ds, "Table");
                return ds.Tables["Table"];
            }
            catch
            {
                throw;
            }
            finally
            {
                ds.Dispose();
                sqldtp.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        public void getDataSet1(String strSQL, String Tabname)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                GlobalVariable.gadapter = new SqlDataAdapter(strSQL, Myconn);
                GlobalVariable.gadapter.SelectCommand.CommandTimeout = 999999;
                GlobalVariable.gadapter.Fill(dt);
                dt.TableName = Tabname;
                if (GlobalVariable.gdataset.Tables.Contains(Tabname) == true)
                {
                    GlobalVariable.gdataset.Tables.Remove(Tabname);
                }
                GlobalVariable.gdataset.Tables.Add(dt);
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error in Retriveing Data  ");
            }
            finally
            {
                Myconn.Close();
            }
        }

        public object getValue(string QryString) 
        {
            object objVariable;
            SqlCommand Cmd = new SqlCommand();
            OpenConnection();
            SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            Cmd.Connection = conn;
            Cmd.CommandText = QryString;
            Cmd.CommandTimeout = 999999;
            Cmd.CommandType = CommandType.Text;
            try
            {
                objVariable = Cmd.ExecuteScalar();
                return objVariable;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public string GetPassword(string vUser) 
        {
            string Vdesc;
            string  vPass;
            int vAsc; 
            Vdesc = "";
            vPass = "";
            for (int Loopindex = 0; Loopindex < vUser.Length; Loopindex++) 
            {
                Vdesc = vUser.Substring(Loopindex, 1).ToString();
                vAsc = (int)Convert.ToChar(Vdesc) + 150;
                vPass =  vPass.Trim() + (char)vAsc;
            }
            return vPass;
        }

        public string abcdAdd(string vString)
        {
            string Vdesc;
            string vDt;
            int vAsc;
            Vdesc = "";
            vDt = "";
            for (int Loopindex = 0; Loopindex < vString.Length; Loopindex++)
            {
                Vdesc = vString.Substring(Loopindex, 1).ToString();
                vAsc = (int)Convert.ToChar(Vdesc) + 150;
                vDt = vDt.Trim() + (char)vAsc;
            }
            return vDt;
        }

        public int Moretransaction_old(ArrayList List)
        {
            OpenConnection();
            SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            ////conn.Open();
            //SqlTransaction myTrans;
            //myTrans = conn.BeginTransaction();
            SqlCommand Cmd;
            try
            {
                foreach (string str in List)
                {
                    Cmd = new SqlCommand(str, conn);
                    Cmd.CommandTimeout = 100000;
                    Cmd.ExecuteNonQuery();
                    //Cmd.Dispose(); 
                }
                return 1;
            }
            catch
            {
                //myTrans.Rollback();
                return 0;
            }
            finally
            {
                //myTrans.Commit();
                conn.Close();
                conn.Dispose();
            }

        }

        public int Moretransaction(ArrayList List) 
        {
            SqlTransaction trans;
            SqlConnection connection = new SqlConnection(Myconn.ConnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            //connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                foreach (string commandString in List)
                {
                    SqlCommand command = new SqlCommand(commandString, connection, trans);
                    command.CommandTimeout = 100000;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                return 1;
            }
            catch (Exception ex) //error occurred
            {
                trans.Rollback();
                return 0;
                //Handel error
            }
            finally
            {
                //myTrans.Commit();
                connection.Close();
                connection.Dispose();
            }
        }

        public void dataOperation(int genum, string ssql) 
        {
            OpenConnection();
            SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction gtrans = conn.BeginTransaction();
            SqlCommand gcommand = new SqlCommand();
            try
            {
                switch (genum)
                {
                    case 1:
                        gcommand = new SqlCommand(ssql, conn);
                        gcommand.CommandTimeout = 999999;
                        gcommand.Transaction = gtrans;
                        gcommand.ExecuteNonQuery();
                        gtrans.Commit();
                        break;
                    case 2:
                    //Console.WriteLine("Case 2");
                    //break;
                    default:
                        gcommand = new SqlCommand(ssql, conn);
                        gcommand.CommandTimeout = 999999;
                        gcommand.Transaction = gtrans;
                        gcommand.ExecuteNonQuery();
                        gtrans.Commit();
                        break;
                }
            }
            catch
            {
                gtrans.Rollback();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public void OpenTextFile(string VOutputfile)
        {
            string filepath = Application.StartupPath + @"\Reports\" + VOutputfile + ".txt";
            string Wordpath = Application.StartupPath + @"\Wordpad.exe ";
            if (System.IO.File.Exists(filepath))
            {
                if (System.IO.File.Exists(Wordpath))
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = filepath;
                    proc.Start();
                }
                else
                {
                    MessageBox.Show("Wordpad.Exe Not Found in your System", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                MessageBox.Show(VOutputfile + " Not Found in your System", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PrintTextFile1(string VOutputfile, string printername)
        {

            //if (usbprint == "Y")
            //{
            string xPrinterName = "";
            System.Windows.Forms.PrintDialog _printDialog = new System.Windows.Forms.PrintDialog();
            _printDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            xPrinterName = _printDialog.PrinterSettings.PrinterName;
            xPrinterName = printername;
            string GS = ((char)29).ToString();
            string ESC = ((char)27).ToString();
            string COMMAND = "";
            COMMAND = ESC + "@";
            COMMAND = COMMAND + GS + "V" + (char)1;

            string output = (char)0x1D + "V" + (char)66 + "\0";

            output = (char)6 + "F" + "P";
            RawPrinterHelper _RawPrinterHelper = new RawPrinterHelper();
            RawPrinterHelper _RawPrinterHelper1 = new RawPrinterHelper();

            string xFileName = VOutputfile;
            _RawPrinterHelper._DocumentName = "Raw Data Print Sample";
            //  _RawPrinterHelper1.SendStringToPrinter(Printername, output)

            RawPrinterHelper.SendFileToPrinter(xPrinterName, xFileName);
            _RawPrinterHelper = null;
 
        }

        public int STOCKAVAILABILITY(DataGridView DG, int Row)
        {
            string Sql = "";
            string ItemCode="", PosItemCode="", PosItemUom="", VarPosCode="", SUBSTORECODE = "";
            Double DBLCALQTY = 0, CLSQTY1 = 0;
            string BControl = "NO";
            string LiqYN = "NO";
            //DataGridView dataGridView1 = new DataGridView();
            //dataGridView1 = (System.Windows.Forms.DataGridView)DG;

            ItemCode = Convert.ToString(DG.Rows[Row].Cells[0].Value);
            PosItemCode =Convert.ToString(DG.Rows[Row].Cells[0].Value);
            PosItemUom = Convert.ToString(DG.Rows[Row].Cells[6].Value);
            VarPosCode = Convert.ToString(DG.Rows[Row].Cells[5].Value);

            //BControl = Convert.ToString(getValue("select distinct ISNULL(StkCtl,'NO') as BControl from ITEMMASTER WHERE ItemCode = '" + ItemCode + "'"));
            BControl = Convert.ToString(getValue("select Isnull(StockCtrl,'NO') As BControl from POSMASTER WHERE poscode = '" + VarPosCode + "'"));

            if (GlobalVariable.gCompName == "KGA")
            { }
            else 
            {
                if (BControl.ToUpper() == "NO")
                {
                    return 2;
                }
            }

            SUBSTORECODE = Convert.ToString(getValue("SELECT STORECODE FROM POSITEMSTORELINK WHERE POS='" + (VarPosCode) + "' AND ITEMCODE='" + PosItemCode + "'"));
            if (SUBSTORECODE == "") { SUBSTORECODE = VarPosCode; }

            for (int i = 0; i < DG.RowCount - 1; i++)
            {
                if (ItemCode == Convert.ToString(DG.Rows[i].Cells[0].Value))
                {
                    DBLCALQTY = DBLCALQTY + Convert.ToDouble(DG.Rows[i].Cells[2].Value);
                }
            }

            if (GlobalVariable.gCompName == "KGA") 
            {
                LiqYN = Convert.ToString(getValue("SELECT ISNULL(LiqYN,'NO') FROM GROUPMASTER WHERE GROUPCODE IN (SELECT GroupCode FROM ItemMaster WHERE ItemCode = '" + PosItemCode + "')"));
                if (LiqYN.ToUpper() == "YES") 
                {
                    SUBSTORECODE = "ODC";
                }
                //SUBSTORECODE = "MNB";
            }

            DataTable BOMDet = new DataTable();
            DataTable Closing = new DataTable();

            Sql = "SELECT GITEMCODE,GUOM,GQTY,ISNULL(Cocktail,'N') AS CTYN FROM BOM_DET WHERE ITEMCODE='" + PosItemCode + "' AND ITEMUOM='" + PosItemUom + "' AND ISNULL(VOID,'') <> 'Y'";
            BOMDet = getDataSet(Sql);
            if (BOMDet.Rows.Count > 0)
            {
                for (int K = 0; K < BOMDet.Rows.Count; K++)
                {
                    DataRow dr = BOMDet.Rows[K];
                    Sql = "select top(1) ISNULL(closingstock,0) AS closingstock,isnull(closingvalue,0) as closingvalue,uom from closingqty where itemcode='" + Convert.ToString(dr["GITEMCODE"]) + "' and storecode='" + SUBSTORECODE + "'  and trndate<= Getdate() order by AUTOID desc";
                    Closing = getDataSet(Sql);
                    if (Closing.Rows.Count > 0)
                    {
                        CLSQTY1 = Convert.ToDouble(Closing.Rows[0].ItemArray[0]);
                    }
                    if (GlobalVariable.gCompName == "KGA") 
                    {
                        CLSQTY1 = ClosingQuantity_KGA(Convert.ToString(dr["GITEMCODE"]), SUBSTORECODE, Convert.ToString(dr["GUOM"]));
                    }
                    if (GlobalVariable.gCompName == "CSC")
                    {
                        CLSQTY1 = ClosingQuantity_CSC(Convert.ToString(dr["GITEMCODE"]), SUBSTORECODE, Convert.ToString(dr["GUOM"]));
                    }
                    if ((CLSQTY1 / Convert.ToDouble(dr["GQTY"])) < DBLCALQTY)
                    {
                        MessageBox.Show(PosItemCode + " NOT AVAILABLE, BALANCE IS " + CLSQTY1);
                        return 0;
                    }
                }
            }
            return 2;
        }

        public double ClosingQuantity_CSC(string Itemcode, string Storecode, string STUom) 
        {
            Double AdjustQty = 0, ClsQty = 0, MainstockQty = 0, TransQty = 0, TransFromQty = 0, TransToQty = 0;
            Double OpQty = 0, GrnQty = 0, IssueQty = 0, IssueToQty = 0, IssueFromQty = 0, ConsumedQty = 0;
            DataTable OpStock = new DataTable();
            DataTable IssueFrom = new DataTable();
            DataTable IssueTo = new DataTable();
            DataTable Adjust = new DataTable();
            DataTable TransFrom = new DataTable();
            DataTable TransTo = new DataTable();
            DataTable Consum = new DataTable();
            string Sql = "";

            Sql = "SELECT ISNULL(OPSTOCK,0) * ISNULL(CONVVALUE,0) AS OPSTOCK1,ISNULL(OPSTOCK,0) AS OPSTOCK FROM INVENTORYITEMMASTER WHERE ITEMCODE='" + Itemcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND STORECODE='" + Storecode + "'";
            OpStock = getDataSet(Sql);
            if (OpStock.Rows.Count > 0)
            {
                OpQty = Convert.ToDouble(OpStock.Rows[0].ItemArray[1]);
            }
            else { OpQty = 0; }

            MainstockQty = OpQty;

            Sql = "SELECT ISNULL(SUM(DBLAMT),0) AS QTY1,ISNULL(SUM(QTY),0) AS QTY FROM STOCKISSUEDETAIL WHERE ITEMCODE='" + Itemcode + "' AND STORELOCATIONCODE = '" + Storecode + "' AND ISNULL(VOID,'')<>'Y'";
            IssueFrom = getDataSet(Sql);
            if (IssueFrom.Rows.Count > 0)
            {
                IssueFromQty = Convert.ToDouble(IssueFrom.Rows[0].ItemArray[1]);
            }
            else { IssueFromQty = 0; }

            Sql = "SELECT ISNULL(SUM(DBLAMT),0) AS QTY1,ISNULL(SUM(QTY),0) AS QTY FROM STOCKISSUEDETAIL WHERE ITEMCODE='" + Itemcode + "' AND OPSTORELOCATIONCODE = '" + Storecode + "' AND ISNULL(VOID,'')<>'Y'";
            IssueTo = getDataSet(Sql);
            if (IssueTo.Rows.Count > 0)
            {
                IssueToQty = Convert.ToDouble(IssueTo.Rows[0].ItemArray[1]);
            }
            else { IssueToQty = 0; }

            IssueQty = IssueToQty - IssueFromQty;

            Sql = "SELECT ISNULL(SUM(DBLAMOUNT),0) AS QTY1,ISNULL(SUM(ADJUSTEDSTOCK),0) AS QTY FROM STOCKADJUSTDETAILS WHERE ITEMCODE='" + Itemcode + "' AND STORELOCATIONCODE = '" + Storecode + "' AND ISNULL(VOID,'')<>'Y' ";
            Adjust = getDataSet(Sql);
            if (Adjust.Rows.Count > 0)
            {
                AdjustQty = Convert.ToDouble(Adjust.Rows[0].ItemArray[1]);
            }
            else { AdjustQty = 0; }

            Sql = "SELECT ISNULL(SUM(DBLAMT),0) AS QTY1,ISNULL(SUM(QTY),0) AS QTY FROM STOCKTRANSFERDETAIL WHERE ITEMCODE='" + Itemcode + "' AND FROMSTORECODE = '" + Storecode + "'  AND ISNULL(VOID,'')<>'Y' ";
            TransFrom = getDataSet(Sql);
            if (TransFrom.Rows.Count > 0)
            {
                TransFromQty = Convert.ToDouble(TransFrom.Rows[0].ItemArray[1]);
            }
            else { TransFromQty = 0; }

            Sql = "SELECT ISNULL(SUM(DBLAMT),0) AS QTY1,ISNULL(SUM(QTY),0) AS QTY FROM STOCKTRANSFERDETAIL WHERE ITEMCODE='" + Itemcode + "' AND TOSTORECODE = '" + Storecode + "'  AND ISNULL(VOID,'')<>'Y' ";
            TransTo = getDataSet(Sql);
            if (TransTo.Rows.Count > 0)
            {
                TransToQty = Convert.ToDouble(TransTo.Rows[0].ItemArray[1]);
            }
            else { TransToQty = 0; }

            TransQty = TransToQty - TransFromQty;

            Sql = "SELECT ISNULL(SUM(DBLAMT),0) AS QTY1,ISNULL(SUM(QTY),0) AS QTY FROM SUBSTORECONSUMPTIONDETAIL WHERE ITEMCODE='" + Itemcode + "' AND STORELOCATIONCODE = '" + Storecode + "'  AND ISNULL(VOID,'')<>'Y' ";
            Consum = getDataSet(Sql);
            if (Consum.Rows.Count > 0)
            {
                ConsumedQty = Convert.ToDouble(Consum.Rows[0].ItemArray[1]);
            }
            else { ConsumedQty = 0; }

            ClsQty = (MainstockQty + AdjustQty) + IssueQty + TransQty - ConsumedQty;

            return ClsQty;

        }
        public double ClosingQuantity_KGA(string Itemcode, string Storecode, string STUom) 
        {
            Double ClsQty = 0;
            SqlCommand gcommand = new SqlCommand();
            OpenConnection();
            SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try 
            {
                using (SqlConnection con = new SqlConnection(Myconn.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SPA_ClosingQtyItem_INV", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", Itemcode);
                        cmd.Parameters.AddWithValue("@StoreCode", Storecode);
                        cmd.Parameters.AddWithValue("@Uom", STUom);
                        cmd.Parameters.Add("@closingQty", SqlDbType.Float, 53);
                        cmd.Parameters["@closingQty"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                        if (cmd.Parameters["@closingQty"].Value.ToString() != "")
                        {
                            ClsQty = Convert.ToDouble(cmd.Parameters["@closingQty"].Value);
                        }
                        else { ClsQty = 0; }
                    }
                }
            }
            catch
            {
                ClsQty = 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return ClsQty;
        }

        public void fromCrystalExportTo(Object report)
        {
            try
            {
                Boolean X;
                String vpath, vLog, strpath;

                vpath = Application.StartupPath + @"\Reports\CrystalExport";
               // vLog = Application.StartupPath + @"\Reports\CrystalExport.Txt";
                //X = ssgrid.ExportRangeToTextFile(0, 0, ssgrid.Col2, ssgrid.Row2, Application.StartupPath & "\Reports\One.txt", "", ",", vbCrLf, FPSpreadADO.ExportRangeToTextFileConstants.ExportRangeToTextFileCreateNewFile, Application.StartupPath & "\Reports\One.log")

                ////        if (Directory.(vpath + ".html") != ""){
                ////           (vpath + ".html");
                ////}

                vpath = vpath + ".pdf";
                if (File.Exists(vpath) == true)
                {
                    File.Delete(vpath);
                }
                ((ReportDocument)report).ExportToDisk(ExportFormatType.PortableDocFormat, vpath);
                // strpath = strexcelpath & " " & vpath;
                // shell(strpath, Microsoft.VisualBasic.AppWinStyle.NormalFocus);

                //X = .ExportToExcel(vpath & ".Xls", "", "")
                //strpath = strexcelpath & " " & vpath & ".xls"

            }
            catch (Exception ex)
            {
                MessageBox.Show("Export to PDF not Done");
            }

        }

        public void ExecuteStoredProcedure(String qry)
        {
            int i;
            try
            {
                OpenConnection();
                SqlConnection conn = new SqlConnection(Myconn.ConnectionString);
                if (Myconn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                MyTrans = conn.BeginTransaction();
                Cmd.Connection = conn;
                Cmd.Transaction = MyTrans;
                Cmd.CommandText = qry;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 999999;
                Cmd.ExecuteNonQuery();
                MyTrans.Commit();
                conn.Close();
            }
            catch (Exception e)
            {
                MyTrans.Rollback();
                MessageBox.Show("Error in Retrievind data as " + e.Message);
                return;
            }
            finally
            {
                Myconn.Close();
            }

        }

        public void GetBillCloseDate() 
        {
            string Sql = "";
            DataTable dt = new DataTable();
            if (GlobalVariable.gCompName == "EPC" )
            {
                GlobalVariable.ServerDate = Convert.ToDateTime(getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
            }
            else if (GlobalVariable.gCompName == "CSC")
            {
                Sql = "select * from VIEW_SERVER_DATETIME Where cast(convert(varchar(11),servertime,108) as datetime) Between '00:00:00' And '04:00:00'";
                dt = getDataSet(Sql);
                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.ServerDate = Convert.ToDateTime(dt.Rows[0]["SERVERDATE"]);
                    GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(-1);
                }
                else 
                {
                    GlobalVariable.ServerDate = Convert.ToDateTime(getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
                }
            }
            else if (GlobalVariable.gCompName == "CFC")
            {
                Sql = "select * from VIEW_SERVER_DATETIME Where cast(convert(varchar(11),servertime,108) as datetime) Between '00:00:00' And '01:00:00'";
                dt = getDataSet(Sql);
                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.ServerDate = Convert.ToDateTime(dt.Rows[0]["SERVERDATE"]);
                    GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(-1);
                }
                else
                {
                    GlobalVariable.ServerDate = Convert.ToDateTime(getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
                }
            }
            else 
            {
                GlobalVariable.ServerDate = Convert.ToDateTime(getValue("SELECT Isnull(BillCloseDate,'') FROM POSSETUP"));
                GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(1);
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

        public void SendSMS_SkyyeBill(string BillNo) 
        {
            string sqlMsg = "";
            string Ph = "", GuestN = "";
            Double BillValue = 0;
            string HOST1="", strPostData="",Msg = "";
            DataTable dtPhone = new DataTable();
            DataTable dtBillAmt = new DataTable();
            sqlMsg = "SELECT REPLACE(REPLACE(ISNULL(MobileNo,''),'-',''),' ','') AS CONTCELL,ISNULL(GuestName,'') AS GuestName FROM Tbl_HomeTakeAwayBill WHERE KotNo IN (select DISTINCT KOTDETAILS from KOT_DET WHERE billdetails = '" + BillNo + "' AND FinYear = '" + FinYear1 + "')";
            dtPhone = getDataSet(sqlMsg);
            if (dtPhone.Rows.Count > 0) 
            {
                DataRow dr = dtPhone.Rows[0];
                Ph = dr["CONTCELL"].ToString();
                GuestN = dr["GuestName"].ToString();
            }

            sqlMsg = "SELECT ISNULL(SUM(ISNULL(PAYAMOUNT,0)),0) AS BILLAMOUNT FROM BillSettlement WHERE BILLNO = '" + BillNo + "' AND FinYear = '" + FinYear1 + "'";
            dtBillAmt = getDataSet(sqlMsg);
            if (dtBillAmt.Rows.Count > 0)
            {
                DataRow dr1 = dtBillAmt.Rows[0];
                BillValue = Convert.ToDouble(dr1["BILLAMOUNT"].ToString());
            }

            if (Ph != "" && Ph.Length == 10 && BillValue > 0 && GlobalVariable.BillSMSYN == "Y") 
            {
                Msg = "Dear Guest, Your Bill Amount for the usage at Skyye is " + BillValue + ".  Thank you. Looking forward to see you again. Team Skyye.";
                HOST1 = "https://sms.myconnectlounge.in/fe/api/v1/multiSend?username=skyyein.trans&password=qXNnA&unicode=false&from=SKYYEB&to=" + Ph + "&text=" + Msg ;
                strPostData = HOST1;
                sqlMsg = "INSERT INTO SENDSMS VALUES('" + Ph + "','" + strPostData + "','N','" + Ph + "','" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + BillNo + "')";
                dataOperation(1, sqlMsg);
            }
        }

        public void SendSMS_SkyyeKot(string KotNo,bool UpdFlag)
        {
            string sqlMsg = "";
            string Ph = "", GuestN = "";
            Double BillValue = 0;
            string HOST1 = "", strPostData = "", Msg = "",ItemName = "";
            DataTable dtPhone = new DataTable();
            DataTable KotData = new DataTable();
            sqlMsg = "SELECT REPLACE(REPLACE(ISNULL(MobileNo,''),'-',''),' ','') AS CONTCELL,ISNULL(GuestName,'') AS GuestName FROM Tbl_HomeTakeAwayBill WHERE KotNo IN (select DISTINCT KOTDETAILS from KOT_DET WHERE kotdetails = '" + KotNo + "' AND FinYear = '" + FinYear1 + "')";
            dtPhone = getDataSet(sqlMsg);
            if (dtPhone.Rows.Count > 0)
            {
                DataRow dr = dtPhone.Rows[0];
                Ph = dr["CONTCELL"].ToString();
                GuestN = dr["GuestName"].ToString();
            }

            if (UpdFlag == false) 
            {
                sqlMsg = "SELECT KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT FROM KOT_DET WHERE KOTDETAILS = '" + KotNo + "'";
                KotData = getDataSet(sqlMsg);
                if (KotData.Rows.Count > 0) 
                {
                    for (int i = 0; i < KotData.Rows.Count; i++) 
                    {
                        ItemName = ItemName + KotData.Rows[i].ItemArray[3] + " " + KotData.Rows[i].ItemArray[7] + ",";
                    }
                    ItemName = ItemName.Substring(1, ItemName.Length - 2);
                }
            }
            else if (UpdFlag == true)
            {
                sqlMsg = "select OrderNo,KOTDETAILS,ITEMCODE,ITEMDESC,(ISNULL(QTY,0)-ISNULL(QTY2,QTY)) AS QTY from kot_det where KOTDETAILS = '" + KotNo + "' and OrderNo in (select max(OrderNo) from KotItemAddCancel where KOTDETAILS = '" + KotNo + "')";
                KotData = getDataSet(sqlMsg);
                if (KotData.Rows.Count > 0)
                {
                    for (int i = 0; i < KotData.Rows.Count; i++)
                    {
                        ItemName = ItemName + KotData.Rows[i].ItemArray[3] + " " + KotData.Rows[i].ItemArray[4] + ",";
                    }
                    ItemName = ItemName.Substring(1, ItemName.Length - 2);
                }
                else 
                {
                    sqlMsg = "select OrderNo,KOTDETAILS,ITEMCODE,ITEMDESC,(ISNULL(QTY,0)) AS QTY from kot_det where KOTDETAILS = '" + KotNo + "' and FinYear = '" + FinYear1 + "' and OrderNo in (select max(OrderNo) from kot_det where KOTDETAILS = '" + KotNo + "' and FinYear = '" + FinYear1 + "')";
                    KotData = getDataSet(sqlMsg);
                    if (KotData.Rows.Count > 0)
                    {
                        for (int i = 0; i < KotData.Rows.Count; i++)
                        {
                            ItemName = ItemName + KotData.Rows[i].ItemArray[3] + " " + KotData.Rows[i].ItemArray[4] + ",";
                        }
                        ItemName = ItemName.Substring(1, ItemName.Length - 2);
                    }
                }
            }
            else { return; }
            if (Ph != "" && Ph.Length == 10 && ItemName !="" && GlobalVariable.KotSMSYN == "Y")
            {
                Msg = "Dear Guest, You have ordered for " + ItemName + ". We are taking our best efforts to give you experience. Team Skyye.";
                HOST1 = "https://sms.myconnectlounge.in/fe/api/v1/multiSend?username=skyyein.trans&password=qXNnA&unicode=false&from=SKYYEB&to=" + Ph + "&text=" + Msg;
                strPostData = HOST1;
                sqlMsg = "INSERT INTO SENDSMS VALUES('" + Ph + "','" + strPostData + "','N','" + Ph + "','" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + KotNo + "')";
                dataOperation(1, sqlMsg);
            }
        }

        public void SendSMS_HPRCBill(string BillNo)
        {
            string sqlMsg = "";
            string Ph = "", GuestN = "";
            Double BillValue = 0;
            string HOST1 = "", strPostData = "", Msg = "";
            DataTable dtPhone = new DataTable();
            DataTable dtBillAmt = new DataTable();
            sqlMsg = "SELECT REPLACE(REPLACE(ISNULL(MobileNo,''),'-',''),' ','') AS CONTCELL,ISNULL(GuestName,'') AS GuestName FROM Tbl_HomeTakeAwayBill WHERE KotNo IN (select DISTINCT KOTDETAILS from KOT_DET WHERE billdetails = '" + BillNo + "' AND FinYear = '" + FinYear1 + "')";
            dtPhone = getDataSet(sqlMsg);
            if (dtPhone.Rows.Count > 0)
            {
                DataRow dr = dtPhone.Rows[0];
                Ph = dr["CONTCELL"].ToString();
                GuestN = dr["GuestName"].ToString();
            }

            sqlMsg = "SELECT ISNULL(SUM(ISNULL(PAYAMOUNT,0)),0) AS BILLAMOUNT FROM BillSettlement WHERE BILLNO = '" + BillNo + "' AND FinYear = '" + FinYear1 + "'";
            dtBillAmt = getDataSet(sqlMsg);
            if (dtBillAmt.Rows.Count > 0)
            {
                DataRow dr1 = dtBillAmt.Rows[0];
                BillValue = Convert.ToDouble(dr1["BILLAMOUNT"].ToString());
            }

            if (Ph != "" && Ph.Length == 10 && BillValue > 0 && GlobalVariable.BillSMSYN == "Y")
            {
                string iwcomma = @"""PE_ID"""+":"+@"""1201161486445246844"""+","+@"""Template_ID"""+":"+@"""1207163332570710883""";
                //Msg = "Dear Guest, Your Bill Amount for the usage at Skyye is " + BillValue + ".  Thank you. Looking forward to see you again. Team Skyye.";
                Msg = "Dear Guest,Your Bill Amount for the usage at HPRC is " + BillValue + ". Thank you. Looking forward to seeing you again. Team HPRC.";
                ////HOST1 = "https://sms.myconnectlounge.in/fe/api/v1/multiSend?username=skyyein.trans&password=qXNnA&unicode=false&from=SKYYEB&to=" + Ph + "&text=" + Msg;
                ////HOST1 = "https://info.watchadz.net/smsapi/index?key=25E69F5E36459C&campaign=0&routeid=288&type=text&contacts=8777373351&senderid=xHPRCx&msg=" + Msg + "&tlv={" + iwcomma + "}";
                HOST1 = "https://info.watchadz.net/smsapi/index?key=25E69F5E36459C&campaign=0&routeid=288&type=text&contacts=" + Ph + "&senderid=xHPRCx&msg=" + Msg + "&tlv={%22PE_ID%22:%221201161486445246844%22,%22Template_ID%22:%221207163332570710883%22}";
                strPostData = HOST1;
                sqlMsg = "INSERT INTO SENDSMS VALUES('" + Ph + "','" + strPostData + "','N','" + Ph + "','" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + BillNo + "')";
                dataOperation(1, sqlMsg);
            }
        }

        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertR(amount_int) + " Only.";
                }
                else
                {
                    return ConvertR(amount_int) + " Point " + ConvertR(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertR(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + (i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + (i % 100) : "");
            }
            if (i < 100000)
            {
                return (i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + (i % 1000) : "");
            }
            if (i < 10000000)
            {
                return (i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + (i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return (i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + (i % 10000000) : "");
            }
            return (i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + (i % 1000000000) : "");
        }


       
        


    }
}
