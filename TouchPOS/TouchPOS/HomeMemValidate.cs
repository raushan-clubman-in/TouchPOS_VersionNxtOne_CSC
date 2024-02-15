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
using System.Collections;

namespace TouchPOS
{
    public partial class HomeMemValidate : Form
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
        public bool CancelFlag = false;
        public string HTPhoneNo = "";

        public readonly ServiceType _form1;

        public HomeMemValidate(ServiceType form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtPosts = new DataTable();
        DataTable dtGuest = new DataTable();
        string GBL_SMARTCARDSNO = "";
        string LocEntryType = "BOTH";

        private void TakeAwayMemValidate_Load(object sender, EventArgs e)
        {
            AutoComplete();
            AutoCompleteGuest();
            Lbl_CardBal.Text = "";
            Lbl_OutStanding.Text = "";

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
            else 
            {
                Rdb_Walk.Checked = true;
                Rdb_Walk.Enabled = false;
                Rdb_Mem.Enabled = false;
            }

            LocEntryType = Convert.ToString(GCon.getValue("Select Isnull(APPLIED_TO,'BOTH') as APPLIED_TO from ServiceLocation_Hdr Where LocCode = " + LocCode + ""));

            if (LocEntryType == "WALK-IN") { Rdb_Mem.Enabled = false; Rdb_Walk.Checked = true; }
            else if (LocEntryType == "MEMBER") { Rdb_Walk.Enabled = false; }
        }

        private void Rdb_Walk_CheckedChanged(object sender, EventArgs e)
        {
            Cmd_Clear_Click(sender, e);
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
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

        private void Rdb_Mem_CheckedChanged(object sender, EventArgs e)
        {
            Cmd_Clear_Click(sender, e);
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
        }

        private void AutoComplete()
        {
            sql = "SELECT MCODE,MNAME + '=>'+ MCODE as MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') = 'ACTIVE' AND ISNULL(freeze,'') <> 'Y' ";
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

        private void AutoCompleteGuest() 
        {
            sql = "Select MobileNo,GuestName from Tbl_HomeTakeAwayGuest ";
            dtGuest = GCon.getDataSet(sql);
            string[] postSource1 = dtGuest
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MobileNo"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource1);
            Txt_MobileNo.AutoCompleteCustomSource = source;
            Txt_MobileNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_MobileNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
            pictureBox1.Image = null;
            Txt_MobileNo.Text = "";
            Txt_GuestName.Text = "";
            Txt_Address.Text = "";
            MCode = "";
            MName = "";
            CardCode = "";
            CardName = "";
            DCode = "";
            if (LocEntryType == "WALK-IN") { Rdb_Mem.Enabled = false; Rdb_Walk.Checked = true; }
            else if (LocEntryType == "MEMBER") { Rdb_Walk.Enabled = false; Rdb_Mem.Checked = true; }
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            string Add = "";
            bool MemYN = false;
            if (ProcType.ToUpper() == "NO")
            {
                DataTable Mem = new DataTable();
                sql = "SELECT MCODE,MNAME,MEMIMAGE,ISNULL(CONTADD1,'') as CONTADD1,ISNULL(CONTADD2,'') as CONTADD2,ISNULL(CONTADD3,'') as CONTADD3,ISNULL(ContCity,'') as ContCity,ISNULL(ContPin,'') as ContPin,ISNULL(CONTSTATE,'') as CONTSTATE FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') = 'ACTIVE' AND ISNULL(freeze,'') <> 'Y' and mcode = '" + TxtMember.Text + "'";
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
                byte[] ResultSN = new byte[11];
                byte[] TagType = new byte[50];
                GBL_SMARTCARDSNO = "";

                if (GlobalVariable.gCompName == "RTC")
                {
                    if (Txt_Cardid.Text == "")
                    {
                        return;
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
                sql = "SELECT * FROM SM_CARDFILE_HDR WHERE [16_DIGIT_CODE] = '" + GBL_SMARTCARDSNO + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    DataTable Mem = new DataTable();
                    DataTable MPhoto = new DataTable();
                    sql = "SELECT MCODE,MNAME,MEMIMAGE,ISNULL(CONTADD1,'') as CONTADD1,ISNULL(CONTADD2,'') as CONTADD2,ISNULL(CONTADD3,'') as CONTADD3,ISNULL(ContCity,'') as ContCity,ISNULL(ContPin,'') as ContPin,ISNULL(CONTSTATE,'') as CONTSTATE FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') = 'ACTIVE' AND ISNULL(freeze,'') <> 'Y' and mcode = '" + dr["MEMBERCODE"].ToString() + "'";
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
                    Lbl_CardBal.Text = "Card Bal: " + dr["BALANCE"].ToString(); 
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
                        Lbl_OutStanding.Text = "Member Outstanding is : " + MemOut.Rows[0].ItemArray[0].ToString() + " Dr.";
                    }
                    else
                    {
                        Lbl_OutStanding.Text = "Member Outstanding is : " + Convert.ToDouble(MemOut.Rows[0].ItemArray[0]) * -1 + " Cr.";
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
                    CrLimit = Convert.ToDouble(MemCat.Rows[0].ItemArray[0]);
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

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            if (Rdb_Mem.Checked == true) 
            {
                if (MCode == "" && MName == "")
                {
                    MessageBox.Show("No Member Selected Must be select");
                    if (Rdb_Mem.Checked == true) { MemType = "M"; }
                    else if (Rdb_Walk.Checked == true) { MemType = "W"; }
                    this.Hide();
                }
                else
                {
                    MCode = Lbl_MCode.Text;
                    MName = Lbl_Mname.Text;
                    CardCode = Lbl_CardCode.Text;
                    CardName = Lbl_CardHolderName.Text;
                    DCode = Txt_Cardid.Text;
                    HTPhoneNo = "";
                    if (Rdb_Mem.Checked == true) { MemType = "M"; }
                    else if (Rdb_Walk.Checked == true) { MemType = "W"; }
                    this.Hide();
                }
            }
            else if (Rdb_Walk.Checked == true) 
            {
                ArrayList List = new ArrayList();
                string sqlstring = "";
                DataTable GCheck = new DataTable();
                sql = "select * from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_MobileNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0) 
                {
                    sqlstring = "UPDATE Tbl_HomeTakeAwayGuest SET GuestAdd = '" + Txt_Address.Text + "' Where MobileNo = '" + Txt_MobileNo.Text + "' ";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                    }
                }
                else 
                {
                    sqlstring = "Insert Into Tbl_HomeTakeAwayGuest (GuestName,GuestAdd,MobileNo,AddUser,AddDate) ";
                    sqlstring = sqlstring + " Values ('" + Txt_GuestName.Text + "','" + Txt_Address.Text + "','" + Txt_MobileNo.Text + "','" + GlobalVariable.gUserName + "',getdate())";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0) 
                    {
                        List.Clear();
                    }
                }
                MCode = "";
                MName = "";
                CardCode = "";
                CardName = "";
                DCode = "";
                HTPhoneNo = Txt_MobileNo.Text;
                if (Rdb_Mem.Checked == true) { MemType = "M"; }
                else if (Rdb_Walk.Checked == true) { MemType = "W"; }
                this.Hide();
            }
        }

        private void Txt_MobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) 
            {
                sql = "select MobileNo,GuestName,GuestAdd from Tbl_HomeTakeAwayGuest Where MobileNo = '" + Txt_MobileNo.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_Address.Text = GCheck.Rows[0].ItemArray[2].ToString();
                }
            }
        }

        
    }
}
