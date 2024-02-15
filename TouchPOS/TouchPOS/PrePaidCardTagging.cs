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
    public partial class PrePaidCardTagging : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public PrePaidCardTagging(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void PrePaidCardTagging_Load(object sender, EventArgs e)
        {
            DataTable KotNonCheck = new DataTable();
            Txt_Cardid.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_CardBal.Text = "";
            Txt_Cardid.Enabled = true;
            Txt_Cardid.Focus();
            sql = "select Isnull(KotNo,'') as KotNo,Isnull(FinYear,'') as FinYear,Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(HolderName,'') as HolderName from Tbl_PrePaidCardTagging Where KotNo = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                Txt_Cardid.Text = KotNonCheck.Rows[0].ItemArray[2].ToString();
                Lbl_CardCode.Text = KotNonCheck.Rows[0].ItemArray[3].ToString();
                Lbl_CardHolderName.Text = KotNonCheck.Rows[0].ItemArray[4].ToString();
                Txt_Cardid.Enabled = false;
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_Cardid.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_CardBal.Text = "";
            Txt_Cardid.Enabled = true;
            Txt_Cardid.Focus();
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            sql = "SELECT * FROM SM_CARDFILE_HDR WHERE [16_DIGIT_CODE] = '" + Txt_Cardid.Text.Trim() + "' And Isnull(Activation_Flag,'') = 'Y' And Isnull(IssueType,'') = 'PREP' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Lbl_CardCode.Text = dr["CARDCODE"].ToString();
                Lbl_CardHolderName.Text = dr["CARDHOLDERNAME"].ToString();
                Lbl_CardBal.Text = "Card Bal: " + dr["BALANCE"].ToString();
                Txt_Cardid.Enabled = false;
            }
            else 
            {
                MessageBox.Show("Details Not Found,Plz Check Card In SmartCard Module", GlobalVariable.gCompanyName);
                Cmd_Clear_Click(sender, e);
            }
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            if (Txt_Cardid.Text == "" || Lbl_CardCode.Text == "" || Lbl_CardHolderName.Text == "")
            {
                MessageBox.Show("CardId,HolderCode & Name Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            DataTable KotCheck = new DataTable();
            sql = "select * from Tbl_PrePaidCardTagging Where KotNo = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "'";
            KotCheck = GCon.getDataSet(sql);
            KotCheck = GCon.getDataSet(sql);
            if (KotCheck.Rows.Count > 0)
            {
                sqlstring = "UPDATE Tbl_PrePaidCardTagging SET DigitCode = '" + Txt_Cardid.Text + "', HolderCode = '" + Lbl_CardCode.Text + "', HolderName = '" + Lbl_CardHolderName.Text + "',UpdUser = '" + GlobalVariable.gUserName + "', UpdDate = getdate()  Where KotNo = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
            }
            else 
            {
                sqlstring = "Insert Into Tbl_PrePaidCardTagging (KotNo,FinYear,DigitCode,HolderCode,HolderName,AddUser,AddDate) ";
                sqlstring = sqlstring + " Values ('" + KotOrderNo + "','" + FinYear1 + "','" + Txt_Cardid.Text + "','" + Lbl_CardCode.Text + "','" + Lbl_CardHolderName.Text + "','" + GlobalVariable.gUserName + "',getdate())";
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
