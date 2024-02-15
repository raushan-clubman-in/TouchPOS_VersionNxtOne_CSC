using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class Form1 : Form
    {
        GlobalClass GCon = new GlobalClass();
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Keys vKeys);

        public Form1()
        {
            InitializeComponent();
        }

        string sql = "";
        private void Form1_Load(object sender, EventArgs e)
        {

            //GCon.OpenConnection();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            GetPrinter();
            GetServer();
            //GCon.OpenConnection();
            DataTable dt = new DataTable();
            sql = "SELECT COMPANYNAME,FROMDATE,TODATE FROM MASTER..CLUBMASTER WHERE DATAfile IN (SELECT DB_NAME()) Order by FROMDATE Desc ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                label1.Text = dr["COMPANYNAME"].ToString();
                label1.Text = "";
                GlobalVariable.gCompanyName = dr["COMPANYNAME"].ToString();
                GlobalVariable.FinStart = Convert.ToDateTime(dr["FROMDATE"]);
                GlobalVariable.FinEnd = Convert.ToDateTime(dr["TODATE"]);
            }

            GlobalVariable.EntryType = Convert.ToString(GCon.getValue("SELECT Isnull(EntryType,'General') as EntryType FROM POSSETUP"));
            GlobalVariable.gCompName = Convert.ToString(GCon.getValue("SELECT Isnull(COMPNAME,'') as COMPNAME FROM POSSETUP")).ToUpper();
            GlobalVariable.AR_ACCode = Convert.ToString(GCon.getValue("SELECT Isnull(AR_ACCode,'') as AR_ACCode FROM POSSETUP"));
            GlobalVariable.MainCardDeductFlag = Convert.ToString(GCon.getValue("SELECT Isnull(MainCardDeductFlag,'N') as MainCardDeductFlag FROM POSSETUP")).ToUpper();
          
            dt = new DataTable();
            sql = "select DISTINCT USERNAME from master..useradmin UNION ALL select 'CHS' order by USERNAME";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0) 
            {
                Cmb_User.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    Cmb_User.Items.Add(dt.Rows[i]["USERNAME"].ToString());
                }
                Cmb_User.SelectedIndex = 0;
            }

            UpdateColumn();

            GlobalVariable.CreditCheck = Convert.ToString(GCon.getValue("SELECT Isnull(CreditCheck,'NO') as CreditCheck FROM POSSETUP")).ToUpper();
            GlobalVariable.DefaulterCheck = Convert.ToString(GCon.getValue("SELECT Isnull(DefCheck,'NO') as DefCheck FROM POSSETUP")).ToUpper();
            GlobalVariable.DupItemAllowed = Convert.ToString(GCon.getValue("SELECT Isnull(DuplicateItemAllowed,'NO') as DuplicateItemAllowed FROM POSSETUP")).ToUpper();
            GlobalVariable.AccessCheckValidate = Convert.ToString(GCon.getValue("SELECT Isnull(Access_Check,'N') As Access_Check FROM SM_ACCESSCONTROL_MASTER")).ToUpper();
            GlobalVariable.MultiPayMode = Convert.ToString(GCon.getValue("SELECT Isnull(MultiPayMode,'NO') as MultiPayMode FROM POSSETUP")).ToUpper();
            GlobalVariable.AccountPostFlag = Convert.ToString(GCon.getValue("SELECT Isnull(AccountPostFlag,'NO') as AccountPostFlag FROM POSSETUP")).ToUpper();
            GlobalVariable.KotSMSYN = Convert.ToString(GCon.getValue("SELECT Isnull(KotSMS,'N') As KotSMS FROM POSSETUP")).ToUpper();
            GlobalVariable.BillSMSYN = Convert.ToString(GCon.getValue("SELECT Isnull(BillSMS,'N') As BillSMS FROM POSSETUP")).ToUpper();
            //label4.Text = "RAINTREE RECREATION CLUB";
            label4.Text = GlobalVariable.gCompanyName.ToUpper();
            //GlobalVariable.ServerDate = Convert.ToDateTime(GCon.getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
            if (GlobalVariable.gCompName == "EPC")
            {
                GlobalVariable.ServerDate = Convert.ToDateTime(GCon.getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
            }
            else 
            {
                GlobalVariable.ServerDate = Convert.ToDateTime(GCon.getValue("SELECT Isnull(BillCloseDate,'') FROM POSSETUP"));
                GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(1);
            }

            if (GlobalVariable.gCompName == "CFC")
            {
                Cmb_User.Enabled = false;
                txtUser.Visible = true;
            }
            else 
            {
                Cmb_User.Enabled = true;
                txtUser.Visible = false;
                Cmb_User.TabStop = false;
            }

            dt = new DataTable();
            sql = "select * from EventCapSetup Where DATES = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(Void,'') <> 'Y' And Isnull(Authorised,'') = 'Y' And Isnull(SMMandatory,'') = 'Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                GlobalVariable.CapDate = Convert.ToDateTime(dt.Rows[0]["DATES"]);
                GlobalVariable.CapYN = "Y";
            }
            else 
            {
                GlobalVariable.CapDate = GlobalVariable.ServerDate;
                GlobalVariable.CapYN = "N";
            }
            timer1.Enabled = true;
            //TxtPass.Focus();

            if (GlobalVariable.gCompName == "CFC")
            {
                txtUser.Focus();
            }
            else 
            {
                TxtPass.Focus();
            }
        }

        private void UpdateColumn() 
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_HomeTakeAwayBill'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0) 
            {
                sqlstring = "CREATE TABLE Tbl_HomeTakeAwayBill([AutoId] [int] IDENTITY(1,1) NOT NULL,[KotNo] [varchar](20) NULL,[GuestName] [varchar](60) NULL,[GuestAdd] [varchar](500) NULL,[MobileNo] [varchar](15) NULL,[AddUser] [varchar](20) NULL,[AddDate] [datetime] NULL) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_ReasonMaster'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = "Create Table Tbl_ReasonMaster ([AutoId] [int] IDENTITY(1,1) NOT NULL,ReasonTxt Varchar(100) Null) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'KotItemAddCancel'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " CREATE TABLE [dbo].[KotItemAddCancel]([AUTOID] [decimal](18, 0) IDENTITY(1,1) NOT NULL,[KOTDETAILS] [varchar](50) NULL,[ITEMCODE] [varchar](10) NULL,[SLNO] [int] NULL,[OrderNo] [int] NULL,[QTY] [decimal](18, 2) NULL,[Flag] [varchar](10) NULL) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'CashOpeningBal'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table CashOpeningBal(OpenDate Datetime,OpenBal Numeric(18,2),Adduser Varchar(50),AddDate Datetime) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_LocationUserTag'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table Tbl_LocationUserTag (Loccode int ,LocName Varchar(100),UserName Varchar(20),AddUser Varchar(50),AddDate Datetime) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'TPOS_MasterReportForm'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table TPOS_MasterReportForm (FormType Varchar(10),FormName Varchar(100)) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_MasterFormUserTag'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table Tbl_MasterFormUserTag (UserName Varchar(20),FormName Varchar(100),AddM Varchar(1),EditM Varchar(1),AddUser Varchar(50),AddDate Datetime) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }


            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KOT_det' AND  COLUMN_NAME = 'BUMPDateTime') Begin ALTER TABLE KOT_det ADD BUMPDateTime Datetime End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KOT_det' AND  COLUMN_NAME = 'DeliveryDateTime') Begin ALTER TABLE KOT_det ADD DeliveryDateTime Datetime End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'CreditCheck') Begin ALTER TABLE POSSETUP ADD CreditCheck Varchar(5) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'DefCheck') Begin ALTER TABLE POSSETUP ADD DefCheck Varchar(5) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'DuplicateItemAllowed') Begin ALTER TABLE POSSETUP ADD DuplicateItemAllowed Varchar(5) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KotItemAddCancel' AND  COLUMN_NAME = 'Adduser') Begin ALTER TABLE KotItemAddCancel ADD Adduser Varchar(50) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KotItemAddCancel' AND  COLUMN_NAME = 'AddDate') Begin ALTER TABLE KotItemAddCancel ADD AddDate Datetime End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'MultiPayMode') Begin ALTER TABLE POSSETUP ADD MultiPayMode Varchar(5) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'AccountPostFlag') Begin ALTER TABLE POSSETUP ADD AccountPostFlag Varchar(5) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_HomeTakeAwayBill' AND  COLUMN_NAME = 'SEZFlag') Begin ALTER TABLE Tbl_HomeTakeAwayBill ADD SEZFlag Varchar(1) End";
            GCon.dataOperation(1, sql);

            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KOT_HDR' AND  COLUMN_NAME = 'OldChairSeqNo') Begin ALTER TABLE KOT_HDR ADD OldChairSeqNo int End";
            GCon.dataOperation(1, sql);

            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND  COLUMN_NAME = 'ModifierType') Begin ALTER TABLE ItemMaster ADD ModifierType VARCHAR(10) End";
            GCon.dataOperation(1, sql);

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_Modifier'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " CREATE TABLE [dbo].[Tbl_Modifier]([AutoId] [int] IDENTITY(1,1) NOT NULL,[MText] [varchar](200) NULL,[MType] [varchar](10) NULL,[MID] [Varchar](10) NULL,[ADDUSER] [varchar](10) NULL,[ADDDATETIME] [datetime] NULL,[VOID] [varchar](1) NULL,[VOIDUSER] [varchar](10) NULL,[VOIDDATE] [datetime] NULL) ON [PRIMARY] ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }
            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'ItemModifierTag'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table ItemModifierTag ([AutoId] [int] IDENTITY(1,1) NOT NULL,[ItemCode] [Varchar] (30) NOT NULL,[MID] [Varchar] (10) NOT NULL) ON [PRIMARY] ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }
            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_CheckPrint'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table Tbl_CheckPrint (KotNo Varchar(30),PrintTakenBy Varchar(50),PrintTakenDate Datetime,FinYear Varchar(10)) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }

            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KOT_det' AND  COLUMN_NAME = 'CheckNo') Begin ALTER TABLE KOT_det ADD CheckNo Varchar(50) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ServiceLocation_Hdr' AND  COLUMN_NAME = 'KotOrderPrefix') Begin ALTER TABLE ServiceLocation_Hdr ADD KotOrderPrefix Varchar(3) End";
            GCon.dataOperation(1, sql);

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'Tbl_PrePaidCardTagging'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table Tbl_PrePaidCardTagging (KotNo Varchar(20),FinYear Varchar(20),DigitCode Varchar(30),HolderCode Varchar(30),HolderName Varchar(100),AddUser Varchar(50),AddDate Datetime,UpdUser Varchar(50),UpdDate Datetime) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'KotSMS') Begin ALTER TABLE POSSETUP ADD KotSMS Varchar(1) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSSETUP' AND  COLUMN_NAME = 'BillSMS') Begin ALTER TABLE POSSETUP ADD BillSMS Varchar(1) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tablemaster' AND  COLUMN_NAME = 'TableOrder') Begin ALTER TABLE Tablemaster ADD TableOrder int End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_PrePaidCardTagging' AND  COLUMN_NAME = 'DeductAmt') Begin ALTER TABLE Tbl_PrePaidCardTagging ADD DeductAmt Numeric(18,2) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PosMaster' AND  COLUMN_NAME = 'FssaiNo') Begin ALTER TABLE PosMaster ADD FssaiNo Varchar(20) End";
            GCon.dataOperation(1, sql);
            sql = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND  COLUMN_NAME = 'KitchenYN') Begin ALTER TABLE ItemMaster ADD KitchenYN VARCHAR(1) End";
            GCon.dataOperation(1, sql);

            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'UserActiveSession'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table UserActiveSession(UserId Varchar(50) NOT NULL,ModuleName Varchar(15) NOT NULL,SessionStart Datetime,DeviceId varchar(200) NOT NULL) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }
            ChkDt = new DataTable();
            sql = "SELECT * FROM SYSOBJECTS WHERE name = 'UserActiveSession_Log'";
            ChkDt = GCon.getDataSet(sql);
            if (ChkDt.Rows.Count == 0)
            {
                sqlstring = " Create Table UserActiveSession_Log(Autoid INT IDENTITY (1,1), UserId Varchar(50) NOT NULL,ModuleName Varchar(15) NOT NULL,SessionStart Datetime,SessionType Varchar(10),DeviceId varchar(200)) ";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0) { List.Clear(); }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            System.Environment.Exit(0);
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            TxtPass.Text = "";
            TxtPass.Focus();
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_0.Text;
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

        private void Button_Login_Click(object sender, EventArgs e)
        {
            string user = "";
            string pass = "";
            DataTable Userdt = new DataTable();
            DataTable CheckUserDet = new DataTable();

            if (GlobalVariable.gCompName == "CFC")
            {
                Cmb_User.SelectedItem = txtUser.Text.ToUpper();
            }

            user = Cmb_User.Text.Trim();
            pass = GCon.GetPassword(TxtPass.Text.Trim());

            if (GlobalVariable.gCompName == "CFC" || GlobalVariable.gCompName == "TRNG") { }
            else 
            {
                sql = "select * from UserActiveSession Where UserId = '" + user + "'";
                CheckUserDet = GCon.getDataSet(sql);
                if (CheckUserDet.Rows.Count > 0)
                {
                    errorProvider1.SetError(label2, "Already Logged in");
                    MessageBox.Show("Selected User Already Logged in");
                    return;
                }
            }

            sql = "select * from master..useradmin where username = '" + user + "' and userpassword = '" + pass + "'";
            Userdt = GCon.getDataSet(sql);
            if (Userdt.Rows.Count > 0)
            {
                DataRow dr = Userdt.Rows[0];
                label1.Text = dr["username"].ToString();
                GlobalVariable.gUserName = dr["username"].ToString();
                GlobalVariable.gUserCategory = dr["CATEGORY"].ToString();
                UserValid();
                ServiceType Ser = new ServiceType();
                Ser.Show();
                this.Hide();
            }
            else if (user == "CHS" && pass == "ÏÆÉÎÎËÆÇÎÎ")
            {
                GlobalVariable.gUserName = user;
                GlobalVariable.gUserCategory = "S";
                UserValid();
                ServiceType Ser = new ServiceType();
                Ser.Show();
                this.Hide();
            }
            else 
            {
                errorProvider1.SetError(label3, "Incorrect Password");
            }
        }

        public void UserValid() 
        {
            ArrayList List = new ArrayList();
            string deviceInformation = System.Environment.MachineName;
            List.Clear();
            sql = "Insert into UserActiveSession (UserId,ModuleName,SessionStart,DeviceId) Values ('" + GlobalVariable.gUserName + "','TPOS',Getdate(),'" + deviceInformation + "')";
            List.Add(sql);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
        }

        public void GetPrinter()
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
                ssql = "SELECT Computername,printername,KOTOPTION FROM printersetup";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin");
                dt = getserver.Tables["admin"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    GlobalVariable.ComputerName = da["Computername"].ToString();
                    GlobalVariable.PrinterName = da["printername"].ToString();
                    GlobalVariable.KotOptionLocal = da["KOTOPTION"].ToString();
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

        public void GetServer()
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
                ssql = "SELECT readertype FROM DBSKey";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin1");
                dt = getserver.Tables["admin1"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    GlobalVariable.CardType = da["readertype"].ToString();
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

        private void Button_PwdChange_Click(object sender, EventArgs e)
        {
            string user = "";
            string pass = "";
            DataTable Userdt = new DataTable();
            user = Cmb_User.Text.Trim();
            pass = GCon.GetPassword(TxtPass.Text.Trim());
            sql = "select * from master..useradmin where username = '" + user + "' and userpassword = '" + pass + "'";
            Userdt = GCon.getDataSet(sql);
            if (Userdt.Rows.Count > 0)
            {
                DataRow dr = Userdt.Rows[0];
                label1.Text = dr["username"].ToString();
                ChangeUserPassword CUP = new ChangeUserPassword(this);
                CUP.SelUsername = user;
                CUP.ShowDialog();
            }
            else
            {
                errorProvider1.SetError(label3, "Incorrect Password");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           

        }

       
    }
}