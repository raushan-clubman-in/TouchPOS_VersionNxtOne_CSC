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
    public partial class EntryPrintDialog : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public int Slno = 0;
        public string KotOrderNo = "";
        bool gPrint = true;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public readonly EntryForm _form1;

        public EntryPrintDialog(EntryForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string KotCompName = "", KotPrinterName = "";
        string KotCompNameCopy = "", KotPrinterNameCopy = "";

        private void EntryPrintDialog_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].Width = 200;
            DataTable KotData = new DataTable();
            sql = "Select SLNO,ITEMCODE,ITEMDESC,QTY,Isnull(ItemPrintFlag,'N') AS ItemPrintFlag,KOTNO,KOTDETAILS,ITEMTYPE,POSCODE,UOM,RATE,AMOUNT,MODIFIER,AUTOID,isnull(OrderNo,0) as OrderNo,Isnull(KotStatus,'N') as KotStatus from KOT_det where KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND Isnull(KotStatus,'N') <> 'Y' ORDER BY SLNO ";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0) 
            {
                for (int i = 0; i < KotData.Rows.Count; i++) 
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[0]);
                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(KotData.Rows[i].ItemArray[1]);
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(KotData.Rows[i].ItemArray[2]);
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[3]);
                    
                    if (Convert.ToString(KotData.Rows[i].ItemArray[4]) == "Y")
                    {
                        dataGridView1.Rows[i].ReadOnly = true;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    }
                    else 
                    { }
                }
            }

            if (GlobalVariable.gCompName == "CFC") 
            {
                Cmd_Printoptional.Visible = false;
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrintToKitchen(string kotno)
        {
            string PName = "";
            DataTable PData = new DataTable();
            int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + kotno + "' AND ISNULL(UpdUserid,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            sql = "select DISTINCT kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'N' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchen_Skyye(kotno, KotPrinterName, PName, NOdrNo,"P");
                    }
                    else if (GlobalVariable.gCompName == "CFC")
                    {
                        PrintKitchen_CFC(kotno, KotPrinterName, PName, NOdrNo);
                    }
                    else 
                    {
                        PrintKitchen(kotno, KotPrinterName, PName, NOdrNo);
                    }
                    
                }
            }

            PData = new DataTable();
            //sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = "select DISTINCT isnull(kitchencode,'') as kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchenAdd_Skyye(kotno, KotPrinterName, PName, NOdrNo,"");
                    }
                    else 
                    {
                        PrintKitchenAdd(kotno, KotPrinterName, PName, NOdrNo);
                    }
                }
            }

            PData = new DataTable();
            sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchenLess_Skyye(kotno, Convert.ToInt16(dr["SLNO"]), Convert.ToString(dr["ItemCode"]), KotPrinterName, PName,"");
                    }
                    else 
                    {
                        PrintKitchenLess(kotno, Convert.ToInt16(dr["SLNO"]), Convert.ToString(dr["ItemCode"]), KotPrinterName, PName);
                    }
                }
            }
        }

        private void PrintToKitchen_Skyye(string kotno)
        {
            string PName = "",PosCode = "";
            DataTable PData = new DataTable();
            int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + kotno + "' AND ISNULL(UpdUserid,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            ////sql = "select DISTINCT kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            ////sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'N' ";
            sql = "select DISTINCT kitchencode,D.POSCODE from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode AND D.POSCODE = I.PosCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'N'";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    //GetPrinter_KOT_Skyye(PName);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "CSC")
                    {
                        PrintKitchen_Skyye(kotno, KotPrinterName, PName, NOdrNo,PosCode);
                    }
                }
            }

            PData = new DataTable();
            //sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = "select DISTINCT isnull(kitchencode,'') as kitchencode,isnull(D.POSCODE,'') as POSCODE from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode and D.POSCODE = I.PosCode  ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "CSC")
                    {
                        PrintKitchenAdd_Skyye(kotno, KotPrinterName, PName, NOdrNo, PosCode);
                    }
                }
            }

            PData = new DataTable();
            ////sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            ////sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            sql = "select DISTINCT kitchencode,D.POSCODE,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode and D.POSCODE = I.PosCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "CSC")
                    {
                        PrintKitchenLess_Skyye(kotno, Convert.ToInt16(dr["SLNO"]), Convert.ToString(dr["ItemCode"]), KotPrinterName, PName, PosCode);
                    }
                }
            }
        }

        private void PrintToKitchen_CSC(string kotno)
        {
            string PName = "", PosCode = "";
            DataTable PData = new DataTable();
            int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + kotno + "' AND ISNULL(UpdUserid,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            ////sql = "select DISTINCT kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            ////sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'N' ";
            sql = "select DISTINCT D.POSCODE from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode AND D.POSCODE = I.PosCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'N'";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["POSCODE"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    GetPrinter_KOT_CSC(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchen_Skyye(kotno, KotPrinterName, PName, NOdrNo, PosCode);
                    }
                }
            }

            PData = new DataTable();
            //sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = "select DISTINCT isnull(kitchencode,'') as kitchencode,isnull(D.POSCODE,'') as POSCODE from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode and D.POSCODE = I.PosCode  ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchenAdd_Skyye(kotno, KotPrinterName, PName, NOdrNo, PosCode);
                    }
                }
            }

            PData = new DataTable();
            ////sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            ////sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            sql = "select DISTINCT kitchencode,D.POSCODE,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode and D.POSCODE = I.PosCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    PosCode = Convert.ToString(dr["POSCODE"]);
                    GetPrinter_KOT(PName);
                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        PrintKitchenLess_Skyye(kotno, Convert.ToInt16(dr["SLNO"]), Convert.ToString(dr["ItemCode"]), KotPrinterName, PName, PosCode);
                    }
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
                ssql = "SELECT COMPUTERNAME ,PRINTERNAME,COMPUTERNAME_Copy,PRINTERNAME_Copy FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin");
                dt = getserver.Tables["admin"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    KotCompName = Convert.ToString(da["Computername"]);
                    KotPrinterName = Convert.ToString(da["printername"]);
                    KotCompNameCopy = Convert.ToString(da["COMPUTERNAME_Copy"]);
                    KotPrinterNameCopy = Convert.ToString(da["PRINTERNAME_Copy"]);
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

        public void GetPrinter_KOT_Skyye(string KitCode)
        {
            OleDbConnection ServerConn = new OleDbConnection();
            OleDbDataAdapter servercmd;
            DataSet getserver = new DataSet();
            DataTable dt = new DataTable();
            DataTable PtrData = new DataTable();
            string sql, ssql;
            //sql = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + GlobalVariable.appPath + "\\DBS_KEY.MDB";
            //ServerConn.ConnectionString = sql;
            try
            {
                sql = "SELECT top 1 isnull(TABPRINTERPATH,'') as TABPRINTERPATH,isnull(COPYTOKITCHEN,'') as COPYTOKITCHEN FROM kitchenmaster Where kitchenCode = '" + KitCode + "'";
                PtrData = GCon.getDataSet(sql);
                if (PtrData.Rows.Count > 0)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PtrData.Rows[0];
                    KotCompName = Convert.ToString(dr["TABPRINTERPATH"]);
                    KotPrinterName = Convert.ToString(dr["TABPRINTERPATH"]);
                    KotCompNameCopy = Convert.ToString(dr["COPYTOKITCHEN"]);
                    KotPrinterNameCopy = Convert.ToString(dr["COPYTOKITCHEN"]);
                }
                else 
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                }
                //ServerConn.Open();
                //ssql = "SELECT COMPUTERNAME ,PRINTERNAME,COMPUTERNAME_Copy,PRINTERNAME_Copy FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                //servercmd = new OleDbDataAdapter(ssql, ServerConn);
                //servercmd.Fill(getserver, "admin");
                //dt = getserver.Tables["admin"];
                //if (dt.Rows.Count > 0)
                //{
                //    DataRow da = dt.Rows[0];
                //    KotCompName = Convert.ToString(da["Computername"]);
                //    KotPrinterName = Convert.ToString(da["printername"]);
                //    KotCompNameCopy = Convert.ToString(da["COMPUTERNAME_Copy"]);
                //    KotPrinterNameCopy = Convert.ToString(da["PRINTERNAME_Copy"]);
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                //ServerConn.Close();
            }
        }

        public void GetPrinter_KOT_CSC(string KitCode)
        {
            OleDbConnection ServerConn = new OleDbConnection();
            OleDbDataAdapter servercmd;
            DataSet getserver = new DataSet();
            DataTable dt = new DataTable();
            DataTable PtrData = new DataTable();
            string sql, ssql;
            //sql = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + GlobalVariable.appPath + "\\DBS_KEY.MDB";
            //ServerConn.ConnectionString = sql;
            try
            {
                //sql = "SELECT top 1 isnull(TABPRINTERPATH,'') as TABPRINTERPATH,isnull(COPYTOKITCHEN,'') as COPYTOKITCHEN FROM kitchenmaster Where kitchenCode = '" + KitCode + "'";
                sql = " SELECT ISNULL(KOTCOMP1,'') AS KOTCOMP1,ISNULL(KOTPRINT1,'') AS KOTPRINT1 FROM POSMASTER Where Poscode = '" + KitCode + "'";
                PtrData = GCon.getDataSet(sql);
                if (PtrData.Rows.Count > 0)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                    DataRow dr = PtrData.Rows[0];
                    KotCompName = Convert.ToString(dr["KOTCOMP1"]);
                    KotPrinterName = Convert.ToString(dr["KOTPRINT1"]);
                    KotCompNameCopy = "";
                    KotPrinterNameCopy ="";
                }
                else
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    KotPrinterNameCopy = "";
                    KotCompNameCopy = "";
                }
                //ServerConn.Open();
                //ssql = "SELECT COMPUTERNAME ,PRINTERNAME,COMPUTERNAME_Copy,PRINTERNAME_Copy FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                //servercmd = new OleDbDataAdapter(ssql, ServerConn);
                //servercmd.Fill(getserver, "admin");
                //dt = getserver.Tables["admin"];
                //if (dt.Rows.Count > 0)
                //{
                //    DataRow da = dt.Rows[0];
                //    KotCompName = Convert.ToString(da["Computername"]);
                //    KotPrinterName = Convert.ToString(da["printername"]);
                //    KotCompNameCopy = Convert.ToString(da["COMPUTERNAME_Copy"]);
                //    KotPrinterNameCopy = Convert.ToString(da["PRINTERNAME_Copy"]);
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                //ServerConn.Close();
            }
        }

        private void PrintKitchen(string kotno, string PrintName, string KitCode, int OrdNo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "", Remarks = "";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
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
                        if (GlobalVariable.gCompName == "TRNG") 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("KOT").ToString().Length) / 2) + ("KOT").ToString());
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - KitName.ToString().Length) / 2) + BoldOn + KitName.ToString() + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + "STWD  : " + RData["STWNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TABLE NO - " + RData["TABLENO"]).ToString().Length) / 2) + BoldOn + ("TABLE NO - " + RData["TABLENO"]) + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME                 ");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Vrowcount = 13;
                        }
                        else 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Vrowcount = 13;
                        }
                    }
                    if (GlobalVariable.gCompName == "TRNG") 
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-22}{3,4}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), "");
                        Vrowcount = Vrowcount + 1;
                    }
                    else 
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-22}{3,4}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), RData["ServiceOrder"]);
                        Vrowcount = Vrowcount + 1;
                    }
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
                
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(4) + "Remarks  : " + Remarks);
                    Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                }
                
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "") 
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
                sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM Itemmaster I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                GCon.dataOperation(1, sql);
            }
        }

        private void PrintKitchen_Skyye(string kotno, string PrintName, string KitCode, int OrdNo,string PosCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "", Remarks = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,VwPrinterAllocation I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode and D.POSCODE = I.PosCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND D.PosCode = '" + PosCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
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
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(1) + "0");
                        //Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["KOTDETAILS"] + "  " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["CheckNo"] + "  " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "TABLE:" + RData["TABLENO"] + "  " + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(1) + "PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        // "--PAX:" + RData["Covers"] RData["OrderNo"]
                        Filewrite.WriteLine(Strings.Space(1) + "SERVER: " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Remarks = RData["Remarks"].ToString();
                        Vrowcount = 7;
                    }
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}{3,1}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 30), "");
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(1) + "Remarks  : " + Remarks);
                    Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(33, "-"));
                }

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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
                ////sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM Itemmaster I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM PrinterAllocation I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode and D.POSCODE = I.PosCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND D.POSCODE = '" + PosCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                GCon.dataOperation(1, sql);
            }
        }

        private void PrintKitchen_CSC(string kotno, string PrintName, string KitCode, int OrdNo, string PosCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "", Remarks = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,VwPrinterAllocation I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode and D.POSCODE = I.PosCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND D.PosCode = '" + PosCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
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
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(1) + "0");
                        //Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["KOTDETAILS"] + "  " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["CheckNo"] + "  " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "TABLE:" + RData["TABLENO"] + "  " + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(1) + "PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        // "--PAX:" + RData["Covers"] RData["OrderNo"]
                        Filewrite.WriteLine(Strings.Space(1) + "SERVER: " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Remarks = RData["Remarks"].ToString();
                        Vrowcount = 7;
                    }
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}{3,1}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 30), "");
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(1) + "Remarks  : " + Remarks);
                    Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(33, "-"));
                }

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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
                ////sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM Itemmaster I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM PrinterAllocation I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode and D.POSCODE = I.PosCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND D.POSCODE = '" + PosCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                GCon.dataOperation(1, sql);
            }
        }

        private void PrintKitchen_CFC(string kotno, string PrintName, string KitCode, int OrdNo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "", Remarks = "", Prepaidby = "";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,D.UOM,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWCODE,'') AS STWCODE,ISNULL(STWNAME,'') AS STWNAME,H.MCODE,H.MNAME FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' Order by ServiceOrder ";
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
                        if (GlobalVariable.gCompName == "TRNG")
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("KOT").ToString().Length) / 2) + ("KOT").ToString());
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - KitName.ToString().Length) / 2) + BoldOn + KitName.ToString() + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + "STWD  : " + RData["STWNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TABLE NO - " + RData["TABLENO"]).ToString().Length) / 2) + BoldOn + ("TABLE NO - " + RData["TABLENO"]) + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME                 ");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Vrowcount = 13;
                        }
                        else
                        {
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Remarks = RData["Remarks"].ToString();
                            Prepaidby = RData["Adduserid"].ToString();
                            Vrowcount = 13;

                            Filewrite.WriteLine(Strings.Space(0) + "");
                            Filewrite.WriteLine(Strings.Space(0) + "KITCHEN ORDER TICKET" );
                            Filewrite.WriteLine(Strings.Space(0) + "");
                            Filewrite.WriteLine(Strings.Space(0) + "KOT NO   : " + RData["KOTDETAILS"]);
                            Filewrite.WriteLine(Strings.Space(0) + "MEMB NO  : " + RData["MCODE"] + " [" + RData["MNAME"] + "]");
                            Filewrite.WriteLine(Strings.Space(0) + "WAITER   : " + RData["STWCODE"] + " [" + RData["STWNAME"] + "]");
                            Filewrite.WriteLine(Strings.Space(0) + "DATE     :" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(0) + "TABLE    : " + RData["TABLENO"]);
                            Filewrite.WriteLine(Strings.Space(0) + "LOCATION : " + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(0) + "SLNO ITEM DESC       UOM     QTY");
                            Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(33, "-"));
                        }
                    }
                    if (GlobalVariable.gCompName == "TRNG")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-22}{3,4}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), "");
                        Vrowcount = Vrowcount + 1;
                    }
                    else
                    {
                        Filewrite.WriteLine("{0,-2}{1,-18}{2,-8}{3,2}{4,8}", CountItem, Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20), Strings.Mid(RData["UOM"].ToString(), 1, 8), Strings.Format(RData["QTY"], "0"),"");
                        Vrowcount = Vrowcount + 1;
                    }
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(33, "-"));
                Filewrite.WriteLine(Strings.Space(0) + "PREPARED BY :" + Prepaidby);

                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(0) + "Remarks  : " + Remarks);
                    Filewrite.WriteLine(Strings.Space(0) + Strings.StrDup(33, "-"));
                }

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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
                sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' FROM Itemmaster I,KOT_DET D WHERE  D.ITEMCODE = i.ItemCode AND D.KOTDETAILS = '" + kotno + "' AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' ";
                GCon.dataOperation(1, sql);
            }
        }

        private void PrintKitchenAdd_old(string kotno, string PrintName, string KitCode, int slno, string itemcode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And Slno = " + slno + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
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
                        Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        //Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
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
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
               
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }
        
        private void PrintKitchenAdd_old_Skyye(string kotno, string PrintName, string KitCode, int slno, string itemcode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And Slno = " + slno + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
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
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(1) + "0");
                        Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["CheckNo"] + "  " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "TABLE:" + RData["TABLENO"] + "  " + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(1) + "PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(1) + "SERVER: " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        ////Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 7;
                    }
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-1}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(33, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintKitchenAdd(string kotno, string PrintName, string KitCode,int ONo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "";

            const string ESC1 = "\u001B";
            const string BoldOn = ESC1 + "E" + "\u0001";
            const string BoldOff = ESC1 + "E" + "\0";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And Slno = " + slno + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            //sql = " select D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + ONo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND kitchencode = '" + KitCode + "' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
            sql = " select D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + ONo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND kitchencode = '" + KitCode + "' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
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
                        if (GlobalVariable.gCompName == "TRNG") 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("KOT").ToString().Length) / 2) + ("KOT").ToString());
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - KitName.ToString().Length) / 2) + BoldOn + KitName.ToString() + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                            //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                            Filewrite.WriteLine(Strings.Space(4) + "STWD  : " + RData["STWNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - ("TABLE NO - " + RData["TABLENO"]).ToString().Length) / 2) + BoldOn + ("TABLE NO - " + RData["TABLENO"]) + BoldOff);
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                            Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Vrowcount = 13;
                        }
                        else 
                        {
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                            Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                            Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                            //Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                            Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
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
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
                
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintKitchenAdd_Skyye(string kotno, string PrintName, string KitCode, int ONo, string PosCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "";
            DateTime AddItemTime;

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));

            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And Slno = " + slno + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            sql = " select D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(D.CheckNo,'') as CheckNo,isnull(slno,0) as slno from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN VwPrinterAllocation I ON D.ITEMCODE = I.ItemCode and D.POSCODE = I.PosCode  WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + ONo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND kitchencode = '" + KitCode + "' and D.PosCode = '" + PosCode + "' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND Isnull(ItemPrintFlag,'N') = 'Y' ";
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
                        AddItemTime = Convert.ToDateTime(GCon.getValue("select AddDate from KotItemAddCancel Where KOTDETAILS = '" + RData["KOTDETAILS"] + "' And ITEMCODE = '" + RData["ITEMCODE"] + "' and SLNO = " + RData["slno"] + " "));
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(1) + "0");
                        Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["CheckNo"] + "   " + KitName);
                        Filewrite.WriteLine(Strings.Space(1) + "TABLE:" + RData["TABLENO"] + "  " + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(1) + "PAX:" + RData["Covers"]);
                        //Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(AddItemTime, "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(1) + "SERVER: " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        //Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        //Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        //Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 7;
                    }
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", "MOD: ", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintKitchenLess(string kotno, int sno, string icode, string PrintName, string KitCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            //sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,(Isnull(QTY2,0)-Isnull(QTY,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,Isnull(D.reason,'') as reason,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "' And itemcode = '" + icode + "' And SLNO = " + sno + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,D.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,(Isnull(QTY2,0)-Isnull(QTY,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,Isnull(D.reason,'') as reason,Isnull(D.CheckNo,'') as CheckNo,ISNULL(STWNAME,'') AS STWNAME FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "' And itemcode = '" + icode + "' And SLNO = " + sno + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
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
                        //Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["CheckNo"] + "  ORDER ID:" + RData["OrderNo"]);
                        //Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + "STWD  : " + RData["STWNAME"]);
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
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void PrintKitchenLess_Skyye(string kotno, int sno, string icode, string PrintName, string KitCode, string PosCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,(Isnull(QTY2,0)-Isnull(QTY,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,Isnull(D.reason,'') as reason,Isnull(D.CheckNo,'') as CheckNo FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "' And itemcode = '" + icode + "' And SLNO = " + sno + " and D.PosCode = '" + PosCode + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
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
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(1) + Strings.Space((33 - "ORDER CANCEL".ToString().Length) / 2) + "ORDER CANCEL");
                        Filewrite.WriteLine(Strings.Space(1) + "0");
                        Filewrite.WriteLine(Strings.Space(1) + "KOT:" + RData["CheckNo"] + "  " + KitCode);
                        Filewrite.WriteLine(Strings.Space(1) + "TABLE:" + RData["TABLENO"] + "  " + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(1) + "PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(1) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd/MM/yyyy"), 1, 12) + Strings.Space(2) + "TIME:" + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 10));
                        Filewrite.WriteLine(Strings.Space(1) + "SERVER: " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        ////Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        ////Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        ////Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Filewrite.WriteLine("{0,-1}{1,-7}{2,-30}", "", "", RData["reason"]);
                    Vrowcount = Vrowcount + 2;
                }

                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(1) + Strings.StrDup(40, "-"));
                if (GlobalVariable.gCompName == "NZC")
                {
                    string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + kotno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                    Filewrite.WriteLine(Strings.Space(4) + "Bar Code : " + BCode);
                }
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
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    if (KotPrinterNameCopy != "")
                    {
                        GCon.PrintTextFile1(vFilepath, KotPrinterNameCopy);
                    }
                    if (GlobalVariable.KotOptionLocal == "Y")
                    {
                        GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                    }
                }
            }
        }

        private void Cmd_Printoptional_Click(object sender, EventArgs e)
        {
            Boolean Select;
            string itemcode="",KitCode="";
            string PName = "";
            int sno = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++) 
            {
                Select = Convert.ToBoolean(dataGridView1.Rows[i].Cells[4].Value);
                if (Select == true) 
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    { sno = Convert.ToInt16(dataGridView1.Rows[i].Cells[0].Value); }
                    else { sno = 0; }
                    if (dataGridView1.Rows[i].Cells[1].Value != null )
                    {
                        itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                        KitCode = Convert.ToString(GCon.getValue("select isnull(kitchencode,'') from itemmaster Where itemcode = '" + itemcode + "'"));
                        KotPrinterName = "";
                        KotCompName = "";
                        PName = KitCode;
                        GetPrinter_KOT(PName);
                        if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "CSC")
                        {
                            PrintKitchenAdd_old_Skyye(KotOrderNo, KotPrinterName, PName, sno, itemcode);
                        }
                        else 
                        {
                            PrintKitchenAdd_old(KotOrderNo, KotPrinterName, PName, sno, itemcode);
                        }
                    }
                    sql = "UPDATE KOT_DET SET ItemPrintFlag = 'Y' Where KOTDETAILS = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(ItemPrintFlag,'N') = 'N' AND SLNO = " + sno + " ";
                    GCon.dataOperation(1, sql);
                }
            }
            GC.Collect();
            this.Close();
        }

        private void Cmd_PrintAll_Click(object sender, EventArgs e)
        {

            if (GlobalVariable.gCompName == "SKYYE" || GlobalVariable.gCompName == "CSC") 
            {
                PrintToKitchen_Skyye(KotOrderNo);
            }
            else
            {
                PrintToKitchen(KotOrderNo);
            }
            GC.Collect();
            this.Close();
        }


       
    }
}
