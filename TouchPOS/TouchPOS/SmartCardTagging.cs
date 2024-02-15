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
    public partial class SmartCardTagging : Form
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
        public string KotOrderNo = "";

        private string osk = "C:/Windows/System32/osk.exe";
        private Process oskProcess;

        public readonly PayForm _form1;

        public SmartCardTagging(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        string GBL_SMARTCARDSNO = "";

        private void SmartCardTagging_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            if (GlobalVariable.gCompName == "CSC")
            {
                groupBox1.Visible = false;
            }
            Lbl_MCode.Text = "";
            Lbl_Mname.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_Address.Text = "";
            Lbl_OutStanding.Text = "";
            Lbl_CardBal.Text = "";
            pictureBox1.Image = null;
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            string Add = "";

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
                }
                else
                {
                    Lbl_MCode.Text = dr["MEMBERCODE"].ToString();
                    Lbl_Mname.Text = dr["NAME"].ToString();
                    Lbl_Address.Text = "";
                    MCode = dr["MEMBERCODE"].ToString();
                    MName = dr["NAME"].ToString();
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
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_Cardid.Text = "";
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
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable GCheck = new DataTable();
            if (Txt_Cardid.Text == "" )
            {
                MessageBox.Show("Card Id Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            if (MCode == "")
            {
                MessageBox.Show("MCode Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            if (MName == "")
            {
                MessageBox.Show("Name Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            if (CardCode == "")
            {
                MessageBox.Show("Holder Code Can't Blank", GlobalVariable.gCompanyName);
                return;
            }

            sql = "SELECT * FROM SM_CARDFILE_HDR WHERE [16_DIGIT_CODE] = '" + Txt_Cardid.Text.Trim() + "' ";
            GCheck = GCon.getDataSet(sql);
            if (GCheck.Rows.Count == 0)
            {
                MessageBox.Show("Card id Not Found,Please Read again and Try to Tag", GlobalVariable.gCompanyName);
                return;
            }
            sqlstring = " UPDATE KOT_HDR SET MCODE = '" + MCode + "',Mname = '" + MName + "',CARDHOLDERCODE = '" + CardCode + "',CARDHOLDERNAME = '" + CardName + "',[16_DIGIT_CODE] = '" + DCode + "' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y'";
            List.Add(sqlstring);
            sqlstring = " UPDATE KOT_DET SET MCODE = '" + MCode + "' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' ";
            List.Add(sqlstring);
            string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '0' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
            if (RoomMember != "")
            {
                sqlstring = "UPDATE kot_hdr SET Checkin = 0, RoomNo = '0',Guest = ''  Where Kotdetails = '" + KotOrderNo + "' ";
                List.Add(sqlstring);
            }
            string ARMember = Convert.ToString(GCon.getValue("SELECT ARCode FROM Tbl_ARFlagUpdation Where KotNo = '" + KotOrderNo + "'"));
            if (ARMember != "")
            {
                sqlstring = "DELETE FROM Tbl_ARFlagUpdation Where KotNo = '" + KotOrderNo + "'";
                List.Add(sqlstring);
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                MessageBox.Show("Infomation Updated Successfully");
                this.Hide();
            }
        }
    }
}
