using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlManager;
using System.Collections;
using System.Net.Mail;
using System.Web;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using TouchPOS.REPORTS;
namespace TouchPOS
{
    public partial class ServiceType : Form
    {
        GlobalClass GCon = new GlobalClass();
        static int minTime, maxTime, dateValue;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ServiceType()
        {
            InitializeComponent();
        }

        ToolTip t1 = new ToolTip();
        string sql = "";

        private void ServiceType_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + GlobalVariable.gUserName;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            ////ControlMoverOrResizer.Init(label1);
            ////ControlMoverOrResizer.Init(button1);
            ////ControlMoverOrResizer.Init(button2);
            ////ControlMoverOrResizer.Init(button3);
            ////ControlMoverOrResizer.Init(Cmd_Exit);
            ////ControlMoverOrResizer.Init(pictureBox1, panel1);
            ////ControlMoverOrResizer.Init(Cmd_Support, panel1);
            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
            if (GlobalVariable.gCompName == "RTC" || GlobalVariable.gCompName == "NEO")
            { }
            else 
            {
                //Cmd_EXP.Enabled = false;
                //Cmd_KDS.Enabled = false;
            }
            if (GlobalVariable.gCompName == "RTC") 
            {
                DataTable dt = new DataTable();
                sql = " select * from CashOpeningBal Where OpenDate = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count == 0) 
                {
                    OpeningUpdate OP = new OpeningUpdate(this);
                    OP.ShowDialog();
                }
            }

            if (GlobalVariable.gUserCategory != "S") 
            { 
                //Cmd_Master.Enabled = false;
                Cmd_Search.Enabled = false;
                Cmd_KOTSearch.Enabled = false;
                Cmd_EXP.Enabled = false;
                Cmd_KDS.Enabled = false;
                Cmd_Rights.Enabled = false;
            }
            //if (GlobalVariable.AccountPostFlag == "YES" || GlobalVariable.gCompName == "EPC" || GlobalVariable.gCompName == "SKYYE") 
            //{
            //    Cmd_DayClose.Visible = false;
            //}
            Cmd_DayClose.Visible = true;
            DataTable Rights = new DataTable();
            if (GlobalVariable.gUserCategory != "S") 
            {
                sql = "SELECT * FROM Tbl_TransactionFormUserTag WHERE ISNULL(FormName,'') = 'SETTLEMENT FORM MODIFY' And Isnull(UserName,'') = '" + GlobalVariable.gUserName + "' AND (ISNULL(EditM,'N') = 'Y' Or ISNULL(DelM,'N') = 'Y')";
                Rights = GCon.getDataSet(sql);
                if (Rights.Rows.Count > 0) 
                {
                    Cmd_Search.Enabled = true;
                }
            }
            Cmd_EInvoiceStatus.Visible = false;
            if (GlobalVariable.gCompName == "MONTANA") 
            {
                Cmd_EInvoiceStatus.Visible = true;
            }
            if (GlobalVariable.gUserCategory == "S") 
            {
                Cmd_DayEndRpt.Visible = true;
                Cmd_DBillPrint.Visible = true;
                Cmd_Rights.Visible = true;
            }
            else { Cmd_DayEndRpt.Visible = true; Cmd_DBillPrint.Visible = false; Cmd_Rights.Visible = false; }

            if (GlobalVariable.gCompName == "CSC") 
            {
                //Cmd_Master.Visible = false;
                Cmd_Report.Visible = false;
                Cmd_DirectBilling.Visible = false;
                Cmd_TakeAway.Width = 390;
                Cmd_HDelivery.Width = 390;
                Cmd_DineIn.Width = 390;
                pictureBox1.Visible = false;
                Cmd_DayClose.Visible = false;
                Cmd_DayEndRpt.Visible = false;
            }
            if (GlobalVariable.gCompName == "CFC") 
            {
                Cmd_Report.Visible = false;
                Cmd_DayEndRpt.Visible = false;
            }
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "";
            UserRealease();
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        public void UserRealease() 
        {
            ArrayList List = new ArrayList();
            string deviceInformation = System.Environment.MachineName;
            List.Clear();
            sql = "Insert into UserActiveSession_Log (UserId,ModuleName,SessionStart,SessionType,DeviceId) Select UserId,ModuleName,SessionStart,'LogIn',DeviceId From UserActiveSession Where UserId = '" + GlobalVariable.gUserName + "' ";
            List.Add(sql);
            sql = "Insert into UserActiveSession_Log (UserId,ModuleName,SessionStart,SessionType,DeviceId) Values ('" + GlobalVariable.gUserName + "','TPOS',Getdate(),'LogOut','" + deviceInformation + "')";
            List.Add(sql);
            sql = "Delete From UserActiveSession Where UserId = '" + GlobalVariable.gUserName + "'";
            List.Add(sql);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
        }

        private void Cmd_DineIn_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Dine-In";
            if (GlobalVariable.gCompName == "CSC")
            {
                ServiceLocationDisplay SL = new ServiceLocationDisplay();
                SL.Show();
            }
            else 
            {
                ServiceLocation SL = new ServiceLocation();
                SL.Show();
            }
           
            GlobalVariable.LocTabIndex = 0;
            this.Hide();
        }

        private void Cmd_TakeAway_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Take Away";
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'T' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = '0'.ToString();
                EntryForm EF = new EntryForm();
                EF.Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = '0' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = '0' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    EF.UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    EF.UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    EF.Pax = 0;
                    TakeAwayMemValidate MV = new TakeAwayMemValidate(this);
                    MV.LocCode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    MV.ShowDialog();
                    if (MV.MCode == "" && MV.MemType == "M") { return; }
                    if (MV.CancelFlag == true) { return; }
                    EF.MemberCode = MV.MCode;
                    EF.MemberName = MV.MName;
                    EF.CardHolderCode = MV.CardCode;
                    EF.CardHolderName = MV.CardName;
                    EF.DigitCode = MV.DCode;
                    EF.HTPhoneNo = MV.HTPhoneNo;
                }
                EF.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void Cmd_HDelivery_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Home-Delivery";
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'H' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = '0'.ToString();
                EntryForm EF = new EntryForm();
                EF.Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = '0' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = '0' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    EF.UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    EF.UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    EF.Pax = 0;
                    HomeMemValidate MV = new HomeMemValidate(this);
                    MV.LocCode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    MV.ShowDialog();
                    if (MV.MCode == "" && MV.MemType == "M") { return; }
                    if (MV.CancelFlag == true) { return; }
                    EF.MemberCode = MV.MCode;
                    EF.MemberName = MV.MName;
                    EF.CardHolderCode = MV.CardCode;
                    EF.CardHolderName = MV.CardName;
                    EF.DigitCode = MV.DCode;
                    EF.HTPhoneNo = MV.HTPhoneNo;
                }
                EF.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void Cmd_Search_Click(object sender, EventArgs e)
        {
            BillSearch BS = new BillSearch();
            BS.Show();
            this.Hide();
        }

        private void Cmd_KOTSearch_Click(object sender, EventArgs e)
        {
            KOTSearch KS = new KOTSearch();
            KS.Show();
            this.Hide();
        }

        private void Cmd_Support_Click(object sender, EventArgs e)
        {
            Support Sp = new Support(this);
            Sp.ShowDialog();
        }

        private void Cmd_KDS_Click(object sender, EventArgs e)
        {
            KitchenDisplay KD = new KitchenDisplay();
            KD.Show();
            this.Hide();
        }

        private void Cmd_EXP_Click(object sender, EventArgs e)
        {
            ExpeditureDisplay KD = new ExpeditureDisplay();
            KD.Show();
            this.Hide();
        }

        private void Cmd_Master_Click(object sender, EventArgs e)
        {
            MASTER.MastersForm MS = new MASTER.MastersForm();
            MS.Show();
            this.Close();
        }

        public void getMinandMaxTime()
        {

            String sql;
            sql = "SELECT DATEPART(HOUR, min(billtime))as minhours,(DATEPART(HOUR, max(billtime))+1) as maxhours,date(billdate) as datevalue from Touchposwisesales";
            GCon.getDataSet1(sql, "Touchposwisesales");
            if (GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows.Count > 0)
            {
                minTime = GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows[0].Field<int>("minhours");
                maxTime = GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows[0].Field<int>("maxhours");
                dateValue = GlobalVariable.gdataset.Tables["Touchposwisesales"].Rows[0].Field<int>("datevalue");
                if (maxTime > 24)
                {
                    maxTime = maxTime - 24;
                    dateValue = dateValue + 1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            String firstbill = "";
            String lastbill = "";

            string sql17 = "select top 1 billno from billsettlement where billdate=cast(convert(varchar,getdate(),106) as datetime)  order by adddatetime asc";
            String sql18 = "select top 1 billno from billsettlement where billdate=cast(convert(varchar,getdate(),106) as datetime)  order by adddatetime desc";
            GCon.getDataSet1(sql17, "billsettlement");
            GCon.getDataSet1(sql18, "billsettlement1");
            if (GlobalVariable.gdataset.Tables["billsettlement"].Rows.Count > 0)
            {
                firstbill = GlobalVariable.gdataset.Tables["billsettlement"].Rows[0].Field<String>("billno");
            }
            else
            {
                MessageBox.Show("Today No Records are there");

            }
            if (GlobalVariable.gdataset.Tables["billsettlement1"].Rows.Count > 0)
            {
                lastbill = GlobalVariable.gdataset.Tables["billsettlement1"].Rows[0].Field<String>("billno");
            }
            else
            {
                MessageBox.Show("Today No Records are there");
            }

            // String firstbill = GlobalVariable.gdataset.Tables["billsettlement"].Rows[0].Field<String>("billno");
            //String lastbill = GlobalVariable.gdataset.Tables["billsettlement1"].Rows[0].Field<String>("billno");

            string sql = "exec  TOUCHPOS_POSWISE";
            al.Add(sql);
            //  GCon.Moretransaction(al);
            string SQL6 = "EXEC hourlyReportGenerator";
            al.Add(SQL6);
            string SQL5 = "EXEC TOUCHPOS_POSWISEMONTHLY";
            al.Add(SQL5);
            GCon.Moretransaction(al);
            //  getMinandMaxTime();
            String sql1 = "select * from VIEW_SALEBYTYPE";
            String sql2 = "select * from VIEW_SECTION";
            String sql3 = "select * from VIEW_GROUPREPORT2";
            string sql4 = "select * from VIEW_ITEMGROUPREPORT";
            string SQL7 = "SELECT * FROM VIEW_TIMEWISESALES ";
            string SQL8 = "SELECT * FROM View_SessionSales1 ";
            string SQL9 = "SELECT * FROM VIEW_SALESHISTORY ";
            string SQL10 = "SELECT * FROM VIEW_MONTHLYrEPORT ";
            string SQL11 = "SELECT * FROM view_paymentdetails";
            string sql12 = "select * from VIEW_CANCELLEDITEMS";
            String sql13 = "select * from VIEW_CASHBOXAMOUNT";
            String sql14 = "select * from VIEW_DISCOUNTSALES";
            String sql15 = "select * from VIEW_CREDITBILLS";
            String sql16 = "select * from VIEW_SETTLEMENTBILLDETAILS1";

            GCon.getDataSet1(sql1, "VIEW_SALEBYTYPE");
            GCon.getDataSet1(sql2, "VIEW_SECTION");
            GCon.getDataSet1(sql3, "VIEW_GROUPREPORT2");
            GCon.getDataSet1(sql4, "VIEW_ITEMGROUPREPORT");
            GCon.getDataSet1(SQL7, "VIEW_TIMEWISESALES");
            GCon.getDataSet1(SQL8, "View_SessionSales1");
            GCon.getDataSet1(SQL9, "VIEW_SALESHISTORY");
            GCon.getDataSet1(SQL10, "VIEW_MONTHLYrEPORT");
            GCon.getDataSet1(SQL11, "view_paymentdetails");
            GCon.getDataSet1(sql12, "VIEW_CANCELLEDITEMS");
            GCon.getDataSet1(sql13, "VIEW_CASHBOXAMOUNT");
            GCon.getDataSet1(sql14, "VIEW_DISCOUNTSALES");
            GCon.getDataSet1(sql15, "VIEW_CREDITBILLS");
            GCon.getDataSet1(sql16, "VIEW_SETTLEMENTBILLDETAILS1");
            // GlobalVariable.gdataset 
            Report rv = new Report();
            DailyReport r = new DailyReport();

            rv.GetDetails(sql1, "VIEW_SALEBYTYPE", r);
            rv.GetDetails(sql2, "VIEW_SECTION", r);
            rv.GetDetails(SQL8, "View_SessionSales1", r);
            rv.GetDetails(SQL9, "VIEW_SALESHISTORY", r);
            rv.GetDetails(sql3, "VIEW_GROUPREPORT2", r);
            rv.GetDetails(sql4, "VIEW_ITEMGROUPREPORT", r);
            rv.GetDetails(SQL7, "VIEW_TIMEWISESALES", r);

            rv.GetDetails(SQL10, "VIEW_MONTHLYrEPORT", r);
            rv.GetDetails(SQL11, "view_paymentdetails", r);
            rv.GetDetails(sql12, "VIEW_CANCELLEDITEMS", r);
            rv.GetDetails(sql13, "VIEW_CASHBOXAMOUNT", r);
            rv.GetDetails(sql14, "VIEW_DISCOUNTSALES", r);
            rv.GetDetails(sql15, "VIEW_CREDITBILLS", r);
            rv.GetDetails(sql16, "VIEW_SETTLEMENTBILLDETAILS1", r);

            r.SetDataSource(GlobalVariable.gdataset);

            rv.crystalReportViewer1.ReportSource = r;
            rv.crystalReportViewer1.Zoom(100);
            // HttpResponse Response = new HttpResponse();
            ////MemoryStream oStream = new MemoryStream();
            ////// using System.IO

            ////oStream = (MemoryStream)
            ////((ReportDocument)r).ExportToStream(
            ////CrystalDecisions.Shared.ExportFormatType.HTML40);

            ////oStream.Position = 0;
            ////var sr = new StreamReader(oStream);
            ////var html = sr.ReadToEnd();

            //  sendmail(html);
            // rv.crystalReportViewer1.

            //   MyCrystalReportViewer.SeparatePages = false;
            // System.IO.MemoryStream oStream; // using System.IO
            /* oStream = (MemoryStream)
             rv.crystalReportViewer1.ExportToStream(
             CrystalDecisions.Shared.ExportFormatType.HTML40);

             oStream.Position = 0;
             var sr = new StreamReader(ms);
             var html = sr.ReadToEnd();

             sendmail(html); */
            TextObject txtobj, txtobj2;
            txtobj = (TextObject)r.ReportDefinition.ReportObjects["Text13"];
            txtobj2 = (TextObject)r.ReportDefinition.ReportObjects["Text14"];
            txtobj.Text = "First Bill No :" + firstbill;
            txtobj2.Text = "Last Bill No :" + lastbill;

            GCon.fromCrystalExportTo(r);

            rv.Show();

            sendmail("");
        }

        private void sendmail(String html)
        {
            try
            {
                String sql;
                sql = "select * from TOUCHMAIL";
                GCon.getDataSet1(sql, "TOUCHMAIL");
                Boolean state;
                String emailid = GlobalVariable.gdataset.Tables["TOUCHMAIL"].Rows[0].Field<String>("FROMMAIL");
                String password = GlobalVariable.gdataset.Tables["TOUCHMAIL"].Rows[0].Field<String>("PASS");
                // String password = "siva12345";
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                for (int i = 0; i < GlobalVariable.gdataset.Tables["TOUCHMAIL"].Rows.Count; i++)
                {
                    mailMessage.To.Add(GlobalVariable.gdataset.Tables["TOUCHMAIL"].Rows[i].Field<String>("TOMAIL"));
                }

                //      mailMessage.To.Add("madhu2777@gmail.com");
                // mailMessage.To.Add("KRISHNARAJACSE@gmail.com");
                mailMessage.From = new MailAddress(emailid);
                mailMessage.Subject = "DAILY REPORT FOR THE DATE OF" + DateTime.Now.ToString("dd/MMM/yyyy");

                string vpath = Application.StartupPath + @"\Reports\CrystalExport.pdf";
                mailMessage.Body = System.IO.File.ReadAllText(vpath);
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = html;
                if (vpath != "")
                {
                    mailMessage.Attachments.Add(new Attachment(vpath));
                }
                // client = New Mail.SmtpClient(str);
                // client.EnableSsl = state;
                ////state = false;
                ////SmtpServer.UseDefaultCredentials = false;
                // SmtpClient smtpServer = new SmtpClient();
                SmtpClient smtpClient = new SmtpClient();
                state = true;
                smtpClient.EnableSsl = state;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(emailid, password);
                SmtpServer.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;

                smtpClient.Send(mailMessage);
                // Response.End();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void Cmd_Report_Click(object sender, EventArgs e)
        {
            ReportForm PW = new ReportForm();
            PW.Show();
            this.Close();
        }

        private void Cmd_DayClose_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.gCompName == "MONTANA" || GlobalVariable.gCompName == "HPRC") 
            {
                DayClose DC = new DayClose(this);
                DC.ShowDialog();
            }
            else 
            {
                GeneralDayClose DC = new GeneralDayClose(this);
                DC.ShowDialog();
            }
        }

        private void Cmd_Rights_Click(object sender, EventArgs e)
        {
            MASTER.FormsRights MS = new MASTER.FormsRights();
            MS.Show();
            this.Close();
        }

        private void Cmd_DBillPrint_Click(object sender, EventArgs e)
        {
            BillSearchPrint BSP = new BillSearchPrint();
            BSP.ShowDialog();
        }

        private void Cmd_EInvoiceStatus_Click(object sender, EventArgs e)
        {
            EInvoiceingStatus EIS = new EInvoiceingStatus();
            EIS.ShowDialog();
        }

        private void Cmd_DayEndRpt_Click(object sender, EventArgs e)
        {
            DayEndSummary DES = new DayEndSummary();
            DES.ShowDialog();
        }

        private void Cmd_DirectBilling_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Direct-Billing";
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = "V1";
                EntryForm EF = new EntryForm();
                EF.Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    EF.UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    EF.UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    EF.Pax = 1;
                    //HomeMemValidate MV = new HomeMemValidate(this);
                    //MV.LocCode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    //MV.ShowDialog();
                    //if (MV.MCode == "" && MV.MemType == "M") { return; }
                    //if (MV.CancelFlag == true) { return; }
                    //EF.MemberCode = MV.MCode;
                    //EF.MemberName = MV.MName;
                    //EF.CardHolderCode = MV.CardCode;
                    //EF.CardHolderName = MV.CardName;
                    //EF.DigitCode = MV.DCode;
                    //EF.HTPhoneNo = MV.HTPhoneNo;
                }
                
                EF.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

    }
}
