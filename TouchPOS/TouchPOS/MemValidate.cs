using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModecardVB;
using System.Diagnostics;
using System.Collections;

namespace TouchPOS
{
    public partial class MemValidate : Form
    {
        GlobalClass GCon = new GlobalClass();
        string ProcType = "";
        public int LocCode = 0;
        public string MCode = "";
        public string MName = "";
        public string CardCode = "";
        public string CardName = "";
        public string DCode = "";
        public string MemType = "";
        public string GMobNo = "";
        public string GName = "";
        public bool CancelFlag = false;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        private string osk = "C:/Windows/System32/osk.exe";
        private Process oskProcess;

        public readonly ServiceLocation _form1;

        public MemValidate(ServiceLocation form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable dtPosts = new DataTable();
        DataTable dtGuest = new DataTable();
        string GBL_SMARTCARDSNO = "";
        string LocEntryType = "BOTH";

        private void MemValidate_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            if (GlobalVariable.gCompName == "CSC")
            {
                Rdb_Walk.Text = "Others";
                groupBox1.Visible = false;
                Chk_WithCard.Visible = true;
            }
            else { Chk_WithCard.Visible = false; }
            Lbl_MCode.Text = "";
            Lbl_Mname.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_Address.Text = "";
            Lbl_OutStanding.Text = "";
            Lbl_CardBal.Text = "";
            Lbl_BirthdayWish.Text = "";
            pictureBox1.Image = null;

            AutoCompleteMobile();

            if (Rdb_Mem.Checked == true)
            { Grp_WalkinInfo.Visible = false; }
            else if (Rdb_Walk.Checked == true)
            { Grp_WalkinInfo.Visible = true; }
           
            ProcType = Convert.ToString(GCon.getValue("Select Isnull(SCardValidate,'NO') as SCardValidate from ServiceLocation_Hdr Where LocCode = " + LocCode + ""));
            if (ProcType.ToUpper() == "NO") 
            {
                Txt_Cardid.Enabled = false;
                TxtMember.Enabled = true;
                AutoComplete();
                Cmd_OK.Text = "Search"; 
                TxtMember.Text = "";
                TxtMember.Focus();
            }
            else if (ProcType.ToUpper() == "YES")
            {
                AutoComplete();
                Txt_Cardid.Enabled = true;
                if (GlobalVariable.gCompName == "RTC") { }
                else { Txt_Cardid.ReadOnly = true; }
                TxtMember.Enabled = false;
                Cmd_OK.Text = "Read Card";
                Txt_Cardid.Focus();
            }
            else 
            {
                Txt_Cardid.Enabled = false;
                TxtMember.Enabled = true;
                AutoComplete();
                Cmd_OK.Text = "Search";
                TxtMember.Text = "";
                TxtMember.Focus();
            }

            if (GlobalVariable.EntryType.ToUpper() == "MEMBER")
            {
                Rdb_Mem.Checked = true;
                Rdb_Walk.Enabled = false;
                Rdb_Mem.Enabled = false;
            }
            else if (GlobalVariable.EntryType.ToUpper() == "BOTH")
            {
                Rdb_Mem.Checked = true;
                Rdb_Walk.Enabled = true;
                Rdb_Mem.Enabled = true;
            }

            LocEntryType = Convert.ToString(GCon.getValue("Select Isnull(APPLIED_TO,'BOTH') as APPLIED_TO from ServiceLocation_Hdr Where LocCode = " + LocCode + ""));

            if (LocEntryType == "WALK-IN") { Rdb_Mem.Enabled = false; Rdb_Walk.Checked = true; }
            else if (LocEntryType == "MEMBER") { Rdb_Walk.Enabled = false; }

            if (GlobalVariable.CapYN == "Y") 
            {
                Rdb_Walk.Enabled = false;
                ProcType = "YES";
                Txt_Cardid.Enabled = true;
                Txt_Cardid.ReadOnly = true; 
                TxtMember.Enabled = false;
                Cmd_OK.Text = "Read Card";
                Txt_Cardid.ReadOnly = false; 
                Txt_Cardid.Focus();
            }
            if (GlobalVariable.gCompName == "CSC") 
            {
                if (ProcType.ToUpper() == "YES") 
                { Chk_WithCard.Checked = true; }
                else { Chk_WithCard.Checked = false; }
            }
            if (GlobalVariable.gCompName == "SKYYE")
            {
                if (ProcType.ToUpper() == "YES")
                {
                    Txt_Cardid.Enabled = true;
                    if (GlobalVariable.gCompName == "RTC") { }
                    else { Txt_Cardid.ReadOnly = false; }
                    TxtMember.Enabled = false;
                    Cmd_OK.Text = "Read Card";
                    Txt_Cardid.Focus();
                }
            }
        }

        private void AutoComplete()
        {
            sql = "SELECT MCODE,MNAME + '=>'+ MCODE as MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') In ('ACTIVE','LIVE') AND ISNULL(freeze,'') <> 'Y' ";
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MCODE"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            TxtMember.AutoCompleteCustomSource = source;
            TxtMember.AutoCompleteMode = AutoCompleteMode.Suggest;
            TxtMember.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.TxtMember.DataBindings.Add("Text", dtPosts, "MNAME");
            
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            MCode = "";
            MName = "";
            CardCode = "";
            CardName = "";
            DCode = "";
            if (Rdb_Mem.Checked == true) { MemType = "M"; }
            else if (Rdb_Walk.Checked == true) { MemType = "W"; }
            CancelFlag = true;
            this.Hide();
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            string Add = "";
            bool MemYN = false;
            if (ProcType.ToUpper() == "NO") 
            {
                DataTable Mem = new DataTable();
                sql = "SELECT MCODE,MNAME,MEMIMAGE,ISNULL(CONTADD1,'') as CONTADD1,ISNULL(CONTADD2,'') as CONTADD2,ISNULL(CONTADD3,'') as CONTADD3,ISNULL(ContCity,'') as ContCity,ISNULL(ContPin,'') as ContPin,ISNULL(CONTSTATE,'') as CONTSTATE FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') In ('ACTIVE','LIVE') AND ISNULL(freeze,'') <> 'Y' and mcode = '" + TxtMember.Text + "'";
                Mem = GCon.getDataSet(sql);
                if (Mem.Rows.Count > 0)
                {
                    Lbl_MCode.Text = Mem.Rows[0].ItemArray[0].ToString();
                    Lbl_Mname.Text = Mem.Rows[0].ItemArray[1].ToString();
                    Add = "Address :" + Mem.Rows[0].ItemArray[3].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[4].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[5].ToString() + System.Environment.NewLine;
                    Add = Add + Mem.Rows[0].ItemArray[6].ToString() + "-" + Mem.Rows[0].ItemArray[7].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[8].ToString();
                    Lbl_Address.Text = Add;
                    Lbl_CardCode.Text = "";
                    Lbl_CardHolderName.Text = "";
                    byte[] picbyte = Mem.Rows[0].ItemArray[2] as byte[] ?? null;
                    if (picbyte != null)
                    {
                        var data = (Byte[])Mem.Rows[0].ItemArray[2];
                        var stream = new MemoryStream(data);
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    MCode = Mem.Rows[0].ItemArray[0].ToString();
                    MName = Mem.Rows[0].ItemArray[1].ToString();
                    CardCode = "";
                    CardName = "";
                    DCode = "";
                    MemYN = true;
                }
                else 
                {
                    Lbl_MCode.Text = "";
                    Lbl_Mname.Text = "";
                    Lbl_Address.Text = "";
                    Lbl_CardCode.Text = "";
                    Lbl_CardHolderName.Text = "";
                    pictureBox1.Image = null;
                    MCode = "";
                    MName = "";
                    CardCode = "";
                    CardName = "";
                    DCode = "";
                    MemYN = false;
                }
            }
            else if (ProcType.ToUpper() == "YES") 
            {
                DataTable dt = new DataTable();
                DataTable dtC = new DataTable();
                byte[] ResultSN = new byte[11];
                byte[] TagType = new byte[50];
                GBL_SMARTCARDSNO = "";

                if (GlobalVariable.gCompName == "RTC") 
                {
                    if (Txt_Cardid.Text == "") 
                    {
                        return;
                        //if (GlobalVariable.CardType == "ACR120U")
                        //{
                        //    ModecardVB.ModecardVB.cardread ABC = new ModecardVB.ModecardVB.cardread();
                        //    ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        //    ABC.GetSMARTDEVICEPORT(GlobalVariable.CardType);
                        //    GBL_SMARTCARDSNO = ABC.GetSMART_CARDID(GlobalVariable.CardType);
                        //    ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        //    Txt_Cardid.Text = GBL_SMARTCARDSNO;
                        //}
                        //else
                        //{
                        //    ModecardVB.ModecardVB.cardread ABC = new ModecardVB.ModecardVB.cardread();
                        //    ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        //    ABC.GetSMARTDEVICEPORT(GlobalVariable.CardType);
                        //    GBL_SMARTCARDSNO = ABC.GetSMART_CARDID(GlobalVariable.CardType);
                        //    ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        //    Txt_Cardid.Text = GBL_SMARTCARDSNO;
                        //}
                    }
                    else 
                    {
                        DataTable ChkReg = new DataTable();
                        sql = "SELECT RTRIM(LTRIM([16_DIGIT_CODE])) FROM SM_REGISTRATION WHERE ISNULL(PREDEFINED_CARDCODE,'') = '" + Txt_Cardid.Text + "'";
                        ChkReg = GCon.getDataSet(sql);
                        if (ChkReg.Rows.Count > 0) 
                        {
                            GBL_SMARTCARDSNO = ChkReg.Rows[0].ItemArray[0].ToString();
                            Txt_Cardid.Text = GBL_SMARTCARDSNO;
                        }
                    }
                }
                else if (GlobalVariable.gCompName == "SKYYE" && GlobalVariable.CapYN == "Y")
                {
                    GBL_SMARTCARDSNO = Txt_Cardid.Text;
                }
                else if (GlobalVariable.gCompName == "SKYYE" && ProcType.ToUpper() == "YES")
                {
                    GBL_SMARTCARDSNO = Txt_Cardid.Text;
                }
                else 
                {
                    if (GlobalVariable.CardType == "ACR120U")
                    {
                        ModecardVB.ModecardVB.cardread ABC = new ModecardVB.ModecardVB.cardread();
                        ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        ABC.GetSMARTDEVICEPORT(GlobalVariable.CardType);
                        GBL_SMARTCARDSNO = ABC.GetSMART_CARDID(GlobalVariable.CardType);
                        ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        Txt_Cardid.Text = GBL_SMARTCARDSNO;
                    }
                    else
                    {
                        ModecardVB.ModecardVB.cardread ABC = new ModecardVB.ModecardVB.cardread();
                        ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        ABC.GetSMARTDEVICEPORT(GlobalVariable.CardType);
                        GBL_SMARTCARDSNO = ABC.GetSMART_CARDID(GlobalVariable.CardType);
                        ABC.CloseSmartDevicePort(GlobalVariable.CardType);
                        Txt_Cardid.Text = GBL_SMARTCARDSNO;
                    }
                }
                if (GlobalVariable.gCompName == "CSC") 
                {
                    sql = "SELECT [16_DIGIT_CODE] as DigitCode FROM SM_CARDFILE_HDR WHERE ([16_DIGIT_CODE] = '" + GBL_SMARTCARDSNO + "' OR ISNULL(NEWDIGITCODE,'') = '" + GBL_SMARTCARDSNO + "') And  ISNULL(NEWDIGITCODE,'') <> '' ";
                    dtC = GCon.getDataSet(sql);
                    if (dtC.Rows.Count > 0)
                    {
                        DataRow dr1 = dtC.Rows[0];
                        Txt_Cardid.Text = dr1["DigitCode"].ToString();
                        GBL_SMARTCARDSNO = dr1["DigitCode"].ToString();
                    }
                }
                sql = "SELECT * FROM SM_CARDFILE_HDR WHERE [16_DIGIT_CODE] = '" + GBL_SMARTCARDSNO + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    DataTable Mem = new DataTable();
                    DataTable MPhoto = new DataTable();
                    sql = "SELECT MCODE,MNAME,MEMIMAGE,ISNULL(CONTADD1,'') as CONTADD1,ISNULL(CONTADD2,'') as CONTADD2,ISNULL(CONTADD3,'') as CONTADD3,ISNULL(ContCity,'') as ContCity,ISNULL(ContPin,'') as ContPin,ISNULL(CONTSTATE,'') as CONTSTATE FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') In ('ACTIVE','LIVE') AND ISNULL(freeze,'') <> 'Y' and mcode = '" + dr["MEMBERCODE"].ToString() + "'";
                    Mem = GCon.getDataSet(sql);
                    if (Mem.Rows.Count > 0 && dr["ISSUETYPE"].ToString() == "MEM")
                    {
                        Lbl_MCode.Text = Mem.Rows[0].ItemArray[0].ToString();
                        Lbl_Mname.Text = Mem.Rows[0].ItemArray[1].ToString();
                        Add = "Address :" + Mem.Rows[0].ItemArray[3].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[4].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[5].ToString() + System.Environment.NewLine;
                        Add = Add + Mem.Rows[0].ItemArray[6].ToString() + "-" + Mem.Rows[0].ItemArray[7].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[8].ToString();
                        Lbl_Address.Text = Add;
                        MCode = Mem.Rows[0].ItemArray[0].ToString();
                        MName = Mem.Rows[0].ItemArray[1].ToString();
                        MemYN = true;
                    }
                    else 
                    {
                        Lbl_MCode.Text = dr["MEMBERCODE"].ToString();
                        Lbl_Mname.Text = dr["NAME"].ToString();
                        Lbl_Address.Text = "";
                        MCode = dr["MEMBERCODE"].ToString();
                        MName = dr["NAME"].ToString();
                        MemYN = false;
                    }
                    Lbl_CardCode.Text = dr["CARDCODE"].ToString();
                    Lbl_CardHolderName.Text = dr["CARDHOLDERNAME"].ToString();
                    Lbl_CardBal.Text = "Card Bal: " + dr["BALANCE"].ToString(); ;
                    sql = "select mcode,mname,memimage from MEMPHOTO WHERE MCODE ='" + dr["MEMBERCODE"].ToString() + "' AND MNAME='" + dr["CARDHOLDERNAME"].ToString() + "' AND MEMIMAGE IS NOT NULL";
                    MPhoto = GCon.getDataSet(sql);
                    if (MPhoto.Rows.Count > 0)
                    {
                        byte[] picbyte = MPhoto.Rows[0].ItemArray[2] as byte[] ?? null;
                        if (picbyte != null)
                        {
                            var data = (Byte[])MPhoto.Rows[0].ItemArray[2];
                            var stream = new MemoryStream(data);
                            pictureBox1.Image = Image.FromStream(stream);
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    CardCode = dr["CARDCODE"].ToString();
                    CardName = dr["CARDHOLDERNAME"].ToString();
                    DCode = GBL_SMARTCARDSNO;
                }
                else 
                {
                    Lbl_MCode.Text = "";
                    Lbl_Mname.Text = "";
                    Lbl_Address.Text = "";
                    Lbl_CardCode.Text = "";
                    Lbl_CardHolderName.Text = "";
                    Lbl_OutStanding.Text = "";
                    Lbl_CardBal.Text = "";
                    pictureBox1.Image = null;
                    MCode = "";
                    MName = "";
                    CardCode = "";
                    CardName = "";
                    DCode = "";
                    MemYN = false;
                }
            }
            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true && MemYN == true)
            {
                DataTable MemOut = new DataTable();
                sql = "SELECT ISNULL((SUM(DEB)-SUM(CRE)),0) AS OUTABL from Get_CreditBal where SLCODE = '" + (Lbl_MCode.Text) + "'";
                MemOut = GCon.getDataSet(sql);
                if (MemOut.Rows.Count > 0) 
                {
                    if (Convert.ToDouble(MemOut.Rows[0].ItemArray[0]) > 0)
                    {
                        Lbl_OutStanding.Text = "Outstanding is : " + MemOut.Rows[0].ItemArray[0].ToString() + " Dr.";
                    }
                    else 
                    {
                        Lbl_OutStanding.Text = "Outstanding is : " + Convert.ToDouble(MemOut.Rows[0].ItemArray[0]) * - 1 + " Cr.";
                    }
                }
                else { Lbl_OutStanding.Text = ""; }
            }
            else { Lbl_OutStanding.Text = ""; }

            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true && GlobalVariable.CreditCheck == "YES" && MemYN == true) 
            {
                bool CStat = CreditCheck(Lbl_MCode.Text);
                if (CStat == false) 
                {
                    MessageBox.Show("Member Crossed Credit Limit, Plz check account", GlobalVariable.gCompanyName);
                    Cmd_Clear_Click(sender, e);
                }
            }
            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true && GlobalVariable.DefaulterCheck == "YES" && MemYN == true) 
            {
                DataTable DefCheck = new DataTable();
                GCon.getDataSet("Exec Check_Defaulter '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "','" + Lbl_MCode.Text + "'");
                sql = "SELECT ISNULL(Balance,0) as Balance FROM Defaulter_List Where  Mcode = '" + (Lbl_MCode.Text) + "' ";
                DefCheck = GCon.getDataSet(sql);
                if (DefCheck.Rows.Count > 0) 
                {
                    if (Convert.ToDouble(DefCheck.Rows[0].ItemArray[0]) > 0) 
                    {
                        MessageBox.Show("Member in Defaulter, Plz check account", GlobalVariable.gCompanyName);
                        Cmd_Clear_Click(sender, e);
                    }
                }
            }
            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true && GlobalVariable.AccessCheckValidate == "Y" && MemYN == true) 
            {
                DataTable AccessCheck = new DataTable();
                sql = "SELECT * FROM SM_MEMBERENTRYLOG Where MEMBERCODE = '" + (Lbl_MCode.Text) + "' And CAST(CONVERT(VARCHAR(11),DATETIME,106) AS datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' ";
                AccessCheck = GCon.getDataSet(sql);
                if (AccessCheck.Rows.Count == 0) 
                {
                    MessageBox.Show("Please Check-in at Reception,Entry Not Found for today", GlobalVariable.gCompanyName);
                    Cmd_Clear_Click(sender, e);
                }
            }
            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true && MemYN == true) 
            {
                DataTable BirthCheck = new DataTable();
                sql = "SELECT MCODE,Isnull(salut,'') + ' ' + MNAME as MNAME,DOB,DOJ,CurentStatus FROM MEMBERMASTER WHERE MCODE = '" + (Lbl_MCode.Text) + "' AND MONTH(DOB) = " + GlobalVariable.ServerDate.Month.ToString() + " AND DAY(DOB) = " + GlobalVariable.ServerDate.Day.ToString() + "";
                BirthCheck = GCon.getDataSet(sql);
                if (BirthCheck.Rows.Count > 0)
                {
                    MessageBox.Show("TODAY MEMBER HAS BIRTHDAY");
                    Lbl_BirthdayWish.Text = "Happy Birthday " + BirthCheck.Rows[0].ItemArray[1].ToString();
                }
                else { Lbl_BirthdayWish.Text = ""; }
            }
            if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true)
            {
                DataTable SplCheck = new DataTable();
                string SplYN = "N";
                sql = "SELECT MCODE,Isnull(pos_splinfoyn,'N') as pos_splinfoyn,Isnull(POS_SPLINFOTEXT,'') as POS_SPLINFOTEXT FROM MEMBERMASTER WHERE MCODE = '" + (Lbl_MCode.Text) + "'";
                SplCheck = GCon.getDataSet(sql);
                if (SplCheck.Rows.Count > 0)
                {
                    SplYN = SplCheck.Rows[0].ItemArray[1].ToString();
                    if (SplYN == "Y")
                    {
                        MessageBox.Show("Special Info : " + SplCheck.Rows[0].ItemArray[2].ToString());
                    }
                }
            }
        }

        public bool CreditCheck(string MemCode) 
        {
            Double CrLimit = 0;
            string CreditYN = "";
            DataTable MemCat = new DataTable();
            DataTable MemCheck = new DataTable();
            DataTable MemOut = new DataTable();
            sql = "SELECT ISNULL(Creditlimit,0) AS Creditlimit,ISNULL(creditlimityn,'N') AS creditlimityn FROM SUBCATEGORYMASTER WHERE SUBTYPECODE IN (SELECT MEMBERTYPECODE FROM MEMBERMASTER WHERE MCODE = '" + MemCode + "')";
            MemCat = GCon.getDataSet(sql);
            if (MemCat.Rows.Count > 0)
            {
                CrLimit = Convert.ToDouble(MemCat.Rows[0].ItemArray[0]);
                CreditYN = Convert.ToString(MemCat.Rows[0].ItemArray[1]);
            }
            else 
            {
                CrLimit = 0;
                CreditYN = "N";
            }
            sql = "SELECT ISNULL(MEMLIMIT,0) AS MEMLIMIT FROM MEMBERMASTER WHERE MCODE = '" + MemCode + "'";
            MemCheck = GCon.getDataSet(sql);
            if (MemCheck.Rows.Count > 0) 
            {
                if (Convert.ToDouble(MemCheck.Rows[0].ItemArray[0]) > 0) 
                {
                    CrLimit = Convert.ToDouble(MemCheck.Rows[0].ItemArray[0]);
                    CreditYN = "Y";
                }
            }
            if (CreditYN == "Y")
            {
                sql = "SELECT ISNULL((SUM(DEB)-SUM(CRE)),0) AS OUTABL from Get_CreditBal where SLCODE = '" + (Lbl_MCode.Text) + "'";
                MemOut = GCon.getDataSet(sql);
                if (MemOut.Rows.Count > 0)
                {
                    CrLimit = CrLimit - Convert.ToDouble(MemOut.Rows[0].ItemArray[0]);
                }
                if (CrLimit < 0) 
                {
                    return false;
                }
                else { return true; }
            }
            else 
            { return true; }
        }

        private void TxtMember_KeyDown(object sender, KeyEventArgs e) 
        {
            string Add = "";
            if (e.KeyCode == Keys.Enter) 
            {
                if (ProcType.ToUpper() == "NO")
                {
                    DataTable Mem = new DataTable();
                    sql = "SELECT MCODE,MNAME,MEMIMAGE,ISNULL(CONTADD1,'') as CONTADD1,ISNULL(CONTADD2,'') as CONTADD2,ISNULL(CONTADD3,'') as CONTADD3,ISNULL(ContCity,'') as ContCity,ISNULL(ContPin,'') as ContPin,ISNULL(CONTSTATE,'') as CONTSTATE FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') In ('ACTIVE','LIVE') AND ISNULL(freeze,'') <> 'Y' and mcode = '" + TxtMember.Text + "'";
                    Mem = GCon.getDataSet(sql);
                    if (Mem.Rows.Count > 0)
                    {
                        Lbl_MCode.Text = Mem.Rows[0].ItemArray[0].ToString();
                        Lbl_Mname.Text = Mem.Rows[0].ItemArray[1].ToString();
                        Add = "Address :" + Mem.Rows[0].ItemArray[3].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[4].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[5].ToString() + System.Environment.NewLine;
                        Add = Add + Mem.Rows[0].ItemArray[6].ToString() + "-" + Mem.Rows[0].ItemArray[7].ToString() + System.Environment.NewLine + Mem.Rows[0].ItemArray[8].ToString();
                        Lbl_Address.Text = Add;
                        Lbl_CardCode.Text = "";
                        Lbl_CardHolderName.Text = "";
                        byte[] picbyte = Mem.Rows[0].ItemArray[2] as byte[] ?? null;
                        if (picbyte != null)
                        {
                            var data = (Byte[])Mem.Rows[0].ItemArray[2];
                            var stream = new MemoryStream(data);
                            pictureBox1.Image = Image.FromStream(stream);
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }
                        MCode = Mem.Rows[0].ItemArray[0].ToString();
                        MName = Mem.Rows[0].ItemArray[1].ToString();
                        CardCode = "";
                        CardName = "";
                        DCode = "";
                    }
                    if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true)
                    {
                        DataTable BirthCheck = new DataTable();
                        sql = "SELECT MCODE,Isnull(salut,'') + ' ' + MNAME as MNAME,DOB,DOJ,CurentStatus FROM MEMBERMASTER WHERE MCODE = '" + (Lbl_MCode.Text) + "' AND MONTH(DOB) = " + GlobalVariable.ServerDate.Month.ToString() + " AND DAY(DOB) = " + GlobalVariable.ServerDate.Day.ToString() + "";
                        BirthCheck = GCon.getDataSet(sql);
                        if (BirthCheck.Rows.Count > 0)
                        {
                            //MessageBox.Show("TODAY MEMBER HAS BIRTHDAY");
                            Lbl_BirthdayWish.Text = "Happy Birthday " + BirthCheck.Rows[0].ItemArray[1].ToString();
                        }
                        else { Lbl_BirthdayWish.Text = ""; }
                    }
                    if (Lbl_MCode.Text != "" && Rdb_Mem.Checked == true)
                    {
                        DataTable SplCheck = new DataTable();
                        string SplYN = "N";
                        sql = "SELECT MCODE,Isnull(pos_splinfoyn,'N') as pos_splinfoyn,Isnull(POS_SPLINFOTEXT,'') as POS_SPLINFOTEXT FROM MEMBERMASTER WHERE MCODE = '" + (Lbl_MCode.Text) + "'";
                        SplCheck = GCon.getDataSet(sql);
                        if (SplCheck.Rows.Count > 0)
                        {
                            SplYN = SplCheck.Rows[0].ItemArray[1].ToString();
                            if (SplYN == "Y") 
                            {
                                MessageBox.Show("Special Info : " + SplCheck.Rows[0].ItemArray[2].ToString());
                            }
                        }
                    }
                    Cmd_OK_Click(sender, e);
                }
            }
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();

            if (MCode == "" && MName == "" && Rdb_Mem.Checked == true)
            {
                MessageBox.Show("No Member Selected Must be select");
                if (Rdb_Mem.Checked == true) { MemType = "M"; }
                else if (Rdb_Walk.Checked == true) { MemType = "W"; }
                this.Hide();
            }
            else 
            {
                DataTable BookTableonCard = new DataTable();
                if (GlobalVariable.CapYN == "Y") 
                {
                    sqlstring = "SELECT Kotdetails,TableNo FROM KOT_HDR WHERE ISNULL(BillStatus,'') = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(Delflag,'') <> 'Y' And CARDHOLDERCODE = '" + Lbl_CardCode.Text + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    BookTableonCard = GCon.getDataSet(sqlstring);
                    if (BookTableonCard.Rows.Count > 0)
                    {
                        MessageBox.Show("Given Card Already have Table Booked, Can't Processed Again. Table No is : " + Convert.ToString(BookTableonCard.Rows[0]["TableNo"]));
                        return;
                    }
                }
                MCode = Lbl_MCode.Text;
                MName = Lbl_Mname.Text;
                CardCode = Lbl_CardCode.Text;
                CardName = Lbl_CardHolderName.Text;
                DCode = Txt_Cardid.Text;
                GMobNo = Txt_GuestMobNo.Text;
                GName = Txt_GuestName.Text;
                if (Rdb_Mem.Checked == true) { MemType = "M"; }
                else if (Rdb_Walk.Checked == true) { MemType = "W"; }
                this.Hide(); 
            }
            DataTable GCheck = new DataTable();
            if (MemType == "W" && GMobNo != "" && GName != "")  
            {
                sql = "select * from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_GuestMobNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    sqlstring = "UPDATE Tbl_HomeTakeAwayGuest SET GuestName = '" + Txt_GuestName.Text + "'  Where MobileNo = '" + Txt_GuestMobNo.Text + "' ";
                    List.Add(sqlstring);
                }
                else
                {
                    sqlstring = "Insert Into Tbl_HomeTakeAwayGuest (GuestName,GuestAdd,MobileNo,AddUser,AddDate,GuestGSTIN,GuestPAN,GuestCity,GuestPin) ";
                    sqlstring = sqlstring + " Values ('" + Txt_GuestName.Text + "','','" + Txt_GuestMobNo.Text + "','" + GlobalVariable.gUserName + "',getdate(),'','','','')";
                    List.Add(sqlstring);
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                }
            }

            if (GlobalVariable.gCompName == "SKYYE" && (CardCode != "" || DCode != ""))
            {
                bool TrueValidate = false;
                Double ChgForTheDay = 0;
                DataTable CheckTable = new DataTable();
                sql = "";
            }
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_Cardid.Text = "";
            TxtMember.Text = "";
            Lbl_MCode.Text = "";
            Lbl_Mname.Text = "";
            Lbl_Address.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_OutStanding.Text = "";
            Lbl_CardBal.Text = "";
            Lbl_BirthdayWish.Text = "";
            pictureBox1.Image = null;
            MCode = "";
            MName = "";
            CardCode = "";
            CardName = "";
            DCode = "";
            Rdb_Mem.Checked = true;
            if (ProcType.ToUpper() == "NO") { TxtMember.Focus(); }
            else if (ProcType.ToUpper() == "YES") { Txt_Cardid.Focus(); }
            else { TxtMember.Focus(); }
            if (LocEntryType == "WALK-IN") { Rdb_Mem.Enabled = false; Rdb_Walk.Checked = true; }
            else if (LocEntryType == "MEMBER") { Rdb_Walk.Enabled = false; }
        }

        private void Rdb_Walk_CheckedChanged(object sender, EventArgs e)
        {
            Grp_WalkinInfo.Visible = true;
            Txt_GuestMobNo.Text = "";
            Txt_GuestName.Text = "";
            Txt_Cardid.Text = "";
            TxtMember.Text = "";
            Lbl_MCode.Text = "";
            Lbl_Mname.Text = "";
            Lbl_Address.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            pictureBox1.Image = null;
            MCode = "";
            MName = "";
            CardCode = "";
            CardName = "";
            DCode = "";
            Txt_Cardid.Enabled = false;
            TxtMember.Enabled = false;
            Cmd_OK.Enabled = false;
            MemType = "W";
        }

        private void Rdb_Mem_CheckedChanged(object sender, EventArgs e)
        {
            Grp_WalkinInfo.Visible = false;
            Txt_GuestMobNo.Text = "";
            Txt_GuestName.Text = "";

            if (ProcType.ToUpper() == "NO")
            {
                Txt_Cardid.Enabled = false;
                TxtMember.Enabled = true;
                Cmd_OK.Enabled = true;
                AutoComplete();
                Cmd_OK.Text = "Search";
                TxtMember.Text = "";
                TxtMember.Focus();
            }
            else if (ProcType.ToUpper() == "YES")
            {
                Txt_Cardid.Enabled = true;
                if (GlobalVariable.gCompName == "RTC") { }
                else { Txt_Cardid.ReadOnly = true; }
                TxtMember.Enabled = false;
                Cmd_OK.Enabled = true;
                Cmd_OK.Text = "Read Card";
                Txt_Cardid.Focus();
            }
            else
            {
                Txt_Cardid.Enabled = false;
                TxtMember.Enabled = true;
                Cmd_OK.Enabled = true;
                AutoComplete();
                Cmd_OK.Text = "Search";
                TxtMember.Text = "";
                TxtMember.Focus();
            }
            if (GlobalVariable.gCompName == "SKYYE")
            {
                if (ProcType.ToUpper() == "YES")
                {
                    Txt_Cardid.Enabled = true;
                    if (GlobalVariable.gCompName == "RTC") { }
                    else { Txt_Cardid.ReadOnly = false; }
                    TxtMember.Enabled = false;
                    Cmd_OK.Text = "Read Card";
                    Txt_Cardid.Focus();
                }
            }
        }

        private void Cmd_KeyBoard_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "osk.exe");
        }

        private void AutoCompleteMobile()
        {
            sql = "Select MobileNo,GuestName from Tbl_HomeTakeAwayGuest ";
            dtGuest = GCon.getDataSet(sql);
            string[] postSource1 = dtGuest
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MobileNo"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource1);
            Txt_GuestMobNo.AutoCompleteCustomSource = source1;
            Txt_GuestMobNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_GuestMobNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Txt_GuestMobNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "select MobileNo,GuestName from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_GuestMobNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                }
            }
        }

        private void Txt_GuestMobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Chk_WithCard_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_WithCard.Checked == true)
            {
                ProcType = Convert.ToString("YES");
                Txt_Cardid.Enabled = true;
                TxtMember.Enabled = false;
                Cmd_OK.Enabled = true;
                Cmd_OK.Text = "Read Card";
                Txt_Cardid.Focus();
            }
            else
            {
                ProcType = Convert.ToString("NO");
                Txt_Cardid.Enabled = false;
                TxtMember.Enabled = true;
                Cmd_OK.Enabled = true;
                Cmd_OK.Text = "Search";
                TxtMember.Focus();
            }
            Cmd_Clear_Click(sender, e);
        }
    }
}
