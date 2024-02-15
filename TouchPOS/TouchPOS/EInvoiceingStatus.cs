using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using WinHttp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using MessagingToolkit.QRCode;
using System.IO;
using System.Drawing.Imaging;
using CrystalDecisions.CrystalReports.Engine;

namespace TouchPOS
{
    public partial class EInvoiceingStatus : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        MessagingToolkit.QRCode.Codec.QRCodeEncoder QR_Generator = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
          

        public EInvoiceingStatus()
        {
            InitializeComponent();
        }

        string sql = "";

        private void EInvoiceingStatus_Load(object sender, EventArgs e)
        {
            GCon.GetBillCloseDate();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;
        }

        private void Cmd_Search_Click(object sender, System.EventArgs e)
        {
            DataTable BillData = new DataTable();
            sql = " SELECT B.BillDetails,B.BILLDATE,TOTALAMOUNT,ISNULL(SUCCESS,'') AS SuccessStatus,ISNULL(MSG,'') AS MSG,FinYear FROM BILL_HDR B ";
            sql = sql + " LEFT OUTER JOIN GSPIRNUPDATE G ON B.BillDetails = G.billdetails AND ISNULL(G.void,'') <> 'Y' WHERE B.BILLDATE = '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y'  ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(dataGridView1.Font, FontStyle.Bold);

                for (int i = 0; i < BillData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = BillData.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = Strings.Format(BillData.Rows[i].ItemArray[1], "dd/MM/yyyy");
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(BillData.Rows[i].ItemArray[2]);
                    dataGridView1.Rows[i].Cells[3].Value = BillData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[4].Value = BillData.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[5].Value = BillData.Rows[i].ItemArray[5];
                    dataGridView1.Rows[i].DefaultCellStyle = style;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Cmd_BPOS_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Process_Click(object sender, System.EventArgs e)
        {
            string CBillNo = "",FYear = "";
            ArrayList List = new ArrayList();
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            FYear = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
            EInvoicing(CBillNo, FYear);
            Cmd_Search_Click(sender, e);
        }

        public void EInvoicing(string Bno,string FYear)
        {
            string Reqid = "";
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable EData = new DataTable();
            DataTable GspDetails = new DataTable();
            DataTable CheckExit = new DataTable();
            VBMath.Randomize();
            Reqid = Strings.Mid("R" + (VBMath.Rnd() * 800000), 1, 5);

            sql = "Select  ISNULL((select case when (select Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')<>'' then (Select  isnull(gstinno,'') from membermaster where mcode=(select top 1  Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) ";
            sql = sql + " else (select  isnull(GuestGSTIN,'')  from Tbl_HomeTakeAwayBill where KotNo=(select top 1  KOTDETAILS from KOT_DET where BILLDETAILS='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) end ),'') as gstinno ";
            EData = GCon.getDataSet(sql);
            if (EData.Rows.Count > 0)
            {
                if (Convert.ToString(EData.Rows[0].ItemArray[0]) == "") 
                {
                    MessageBox.Show("GSTIN NO not Provide for this Bill");
                    return;
                }
                sql = "select * from GSPIRNUPDATE where billdetails = '" + Bno + "' and isnull(Void,'') <> 'Y'";
                CheckExit = GCon.getDataSet(sql);
                if (CheckExit.Rows.Count > 0) 
                {
                    MessageBox.Show("IRN Already Created,First Delete then try again for new Registration");
                    return;
                }
                CheckExit = new DataTable();
                sql = "select top 1 ReqId from GSPIRNUPDATE where billdetails = '" + Bno + "' AND ISNULL(void,'') = 'Y' order by adddatetime desc";
                CheckExit = GCon.getDataSet(sql);
                if (CheckExit.Rows.Count > 0)
                {
                    Reqid = CheckExit.Rows[0].ItemArray[0].ToString();
                }
                sql = "  Select  gspappid,gspappsecret,user_name,password,gstin from gspdetails ";
                GspDetails = GCon.getDataSet(sql);
                if (GspDetails.Rows.Count > 0)
                {
                    string gspappid = "", gspappsecret = "", user_name = "", password = "", gstin = "";
                    gspappid = GspDetails.Rows[0].ItemArray[0].ToString();
                    gspappsecret = GspDetails.Rows[0].ItemArray[1].ToString();
                    user_name = GspDetails.Rows[0].ItemArray[2].ToString();
                    password = GspDetails.Rows[0].ItemArray[3].ToString();
                    gstin = GspDetails.Rows[0].ItemArray[4].ToString();
                    object xmlobj;
                    string strPostData, params1 = "";
                    string USERID, SID, PWD, SMCODE;
                    Int32 i;
                    string url;
                    WinHttpRequest HttpReq = new WinHttpRequest();
                    try
                    {
                        string token = "";
                        SMCODE = Bno;
                        url = "https://gsp.adaequare.com/gsp/authenticate?action=GSP&grant_type=token";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("gspappid", gspappid);
                        HttpReq.SetRequestHeader("gspappsecret", gspappsecret);
                        HttpReq.Send();
                        string result2;
                        result2 = HttpReq.ResponseText;
                        if (result2.Length > 1)
                        {
                            JObject ser = JObject.Parse(result2);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";

                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name.ToUpper())
                                {
                                    case "ACCESS_TOKEN":
                                        token = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                }
                            }
                        }
                        //url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        url = "https://gsp.adaequare.com/enriched/ei/api/invoice";
                        string insert;
                        DataTable dt = new DataTable();
                        sqlstring = "exec get_einvoicePOS '" + SMCODE + "'";
                        dt = GCon.getDataSet(sqlstring);
                        if (dt.Rows.Count > 0)
                        {
                            params1 = dt.Rows[0].ItemArray[0].ToString();
                        }
                        //url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        url = "https://gsp.adaequare.com/enriched/ei/api/invoice";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("Content-Type", "application/json");
                        HttpReq.SetRequestHeader("user_name", user_name);
                        HttpReq.SetRequestHeader("password", password);
                        HttpReq.SetRequestHeader("gstin", gstin);
                        HttpReq.SetRequestHeader("requestid",Reqid + SMCODE);
                        HttpReq.SetRequestHeader("Authorization", "Bearer " + token);
                        HttpReq.Send(params1);
                        string result;
                        result = HttpReq.ResponseText;

                        SqlConnection CC = new SqlConnection();
                        CC.ConnectionString = GCon.GetConnection();
                        CC.Open();
                        SqlCommand osqlcommand = new SqlCommand();
                        osqlcommand = new SqlCommand("proc_GSPIRNUPDATE", CC);
                        osqlcommand.CommandType = CommandType.StoredProcedure;
                        Boolean desbool = false;
                        if (result.Length > 1)
                        {
                            JObject ser = JObject.Parse(result);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";
                            desbool = false;
                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name)
                                {
                                    case "success":
                                        desbool = true;
                                        //osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString();
                                        break;
                                    case "message":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "result":
                                        string abc = item.Value.ToString();
                                        JObject ser1 = JObject.Parse(abc);
                                        List<JToken> data1 = ser1.Children().ToList();
                                        string output1 = "";
                                        foreach (JProperty item1 in data1)
                                        {
                                            switch (item1.Name)
                                            {
                                                case "AckNo":
                                                    desbool = true;
                                                    //osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item1.Value.ToString();
                                                    break;
                                                case "AckDt":
                                                    DateTime ackdt;
                                                    ackdt = Convert.ToDateTime(item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2));
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = ackdt;
                                                    break;
                                                case "Irn":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedInvoice":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedQRCode":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "Status":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        if (desbool == false)
                        {
                            string result1 = "";
                            //result1 = result.Replace(vbCr, "").Replace(vbLf, "");
                            result1 = result1.Replace(",", "");
                            result1 = result1.Replace("'", "");
                            //result1 = result1.Replace("""", "");
                            osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = result1;
                        }
                        //osqlcommand.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "mobile_membermasteruinserted";
                        osqlcommand.Parameters.Add("@adduser", SqlDbType.VarChar, 50).Value = GlobalVariable.gUserName.ToString();
                        //osqlcommand.Parameters.Add("@col", SqlDbType.VarChar, 50).Value = "MembershipNo";
                        osqlcommand.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = SMCODE;
                        osqlcommand.ExecuteNonQuery();
                        if (desbool == true) 
                        {
                            sqlstring = "UPDATE GSPIRNUPDATE SET ReqId = '" + Reqid + "' where billdetails = '" + SMCODE + "' and isnull(Void,'') <> 'Y'";
                            List.Add(sqlstring);
                            if (GCon.Moretransaction(List) > 0)
                            { List.Clear(); }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public void EinvoicingDelete(string Bno, string FYear) 
        {
            string Reqid = "";
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable EData = new DataTable();
            DataTable GspDetails = new DataTable();
            DataTable CheckExit = new DataTable();
            string delirn;
            DateTime deldate;

            sql = "Select  ISNULL((select case when (select Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')<>'' then (Select  isnull(gstinno,'') from membermaster where mcode=(select top 1  Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) ";
            sql = sql + " else (select  isnull(GuestGSTIN,'')  from Tbl_HomeTakeAwayBill where KotNo=(select top 1  KOTDETAILS from KOT_DET where BILLDETAILS='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) end ),'') as gstinno ";
            EData = GCon.getDataSet(sql);
            if (EData.Rows.Count > 0)
            {
                if (Convert.ToString(EData.Rows[0].ItemArray[0]) == "")
                {
                    MessageBox.Show("GSTIN NO not Provide for this Bill");
                    return;
                }
                sql = "select * from GSPIRNUPDATE where billdetails = '" + Bno + "' and isnull(Void,'') <> 'Y'";
                CheckExit = GCon.getDataSet(sql);
                if (CheckExit.Rows.Count == 0)
                {
                    MessageBox.Show("Registration Not Found,Please Check");
                    return;
                }
                CheckExit = new DataTable();
                sql = "select ReqId from GSPIRNUPDATE where billdetails = '" + Bno + "' and isnull(Void,'') <> 'Y'";
                CheckExit = GCon.getDataSet(sql);
                if (CheckExit.Rows.Count > 0)
                {
                    Reqid = CheckExit.Rows[0].ItemArray[0].ToString();
                }

                sql = "  Select  gspappid,gspappsecret,user_name,password,gstin from gspdetails ";
                GspDetails = GCon.getDataSet(sql);
                if (GspDetails.Rows.Count > 0)
                {
                    string gspappid = "", gspappsecret = "", user_name = "", password = "", gstin = "";
                    gspappid = GspDetails.Rows[0].ItemArray[0].ToString();
                    gspappsecret = GspDetails.Rows[0].ItemArray[1].ToString();
                    user_name = GspDetails.Rows[0].ItemArray[2].ToString();
                    password = GspDetails.Rows[0].ItemArray[3].ToString();
                    gstin = GspDetails.Rows[0].ItemArray[4].ToString();
                    object xmlobj;
                    string strPostData, params1 = "";
                    string USERID, SID, PWD, SMCODE;
                    Int32 i;
                    string url;
                    WinHttpRequest HttpReq = new WinHttpRequest();
                    try
                    {
                        string token = "";
                        SMCODE = Bno;
                        url = "https://gsp.adaequare.com/gsp/authenticate?action=GSP&grant_type=token";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("gspappid", gspappid);
                        HttpReq.SetRequestHeader("gspappsecret", gspappsecret);
                        HttpReq.Send();
                        string result2;
                        result2 = HttpReq.ResponseText;
                        if (result2.Length > 1)
                        {
                            JObject ser = JObject.Parse(result2);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";

                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name.ToUpper())
                                {
                                    case "ACCESS_TOKEN":
                                        token = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                }
                            }
                        }
                        //url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice/cancel";
                        url = "https://gsp.adaequare.com/enriched/ei/api/invoice/cancel";
                        string insert;
                        DataTable dt = new DataTable();
                        sqlstring = "exec get_delinvoicePOS '" + SMCODE + "'";
                        dt = GCon.getDataSet(sqlstring);
                        if (dt.Rows.Count > 0)
                        {
                            params1 = dt.Rows[0].ItemArray[0].ToString();
                        }
                        //url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice/cancel";
                        url = "https://gsp.adaequare.com/enriched/ei/api/invoice/cancel";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("Content-Type", "application/json");
                        HttpReq.SetRequestHeader("user_name", user_name);
                        HttpReq.SetRequestHeader("password", password);
                        HttpReq.SetRequestHeader("gstin", gstin);
                        HttpReq.SetRequestHeader("requestid",Reqid + SMCODE);
                        HttpReq.SetRequestHeader("Authorization", "Bearer " + token);
                        HttpReq.Send(params1);
                        string result;
                        result = HttpReq.ResponseText;

                        SqlConnection CC = new SqlConnection();
                        CC.ConnectionString = GCon.GetConnection();
                        CC.Open();
                        SqlCommand osqlcommand = new SqlCommand();
                        osqlcommand = new SqlCommand("proc_GSPIRNUPDATE", CC);
                        osqlcommand.CommandType = CommandType.StoredProcedure;
                        Boolean desbool = false;
                        if (result.Length > 1)
                        {
                            JObject ser = JObject.Parse(result);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";
                            desbool = false;
                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name)
                                {
                                    case "success":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "message":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "result":
                                        string abc = item.Value.ToString();
                                        JObject ser1 = JObject.Parse(abc);
                                        List<JToken> data1 = ser1.Children().ToList();
                                        string output1 = "";
                                        foreach (JProperty item1 in data1)
                                        {
                                            switch (item1.Name)
                                            {
                                                case "AckNo":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "AckDt":
                                                    DateTime ackdt;
                                                    ackdt = Convert.ToDateTime(item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2));
                                                    deldate = ackdt;
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = ackdt;
                                                    break;
                                                case "Irn":
                                                    desbool = true;
                                                    delirn = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedInvoice":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedQRCode":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "Status":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                                case "CancelDate":
                                                    desbool = true;
                                                    deldate = Convert.ToDateTime(item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2));
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item1.Value.ToString().Substring(1, item1.Value.ToString().Length - 2);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        if (desbool == false)
                        {
                            string result1 = "";
                            //result1 = result.Replace(vbCr, "").Replace(vbLf, "");
                            result1 = result1.Replace(",", "");
                            result1 = result1.Replace("'", "");
                            //result1 = result1.Replace("""", "");
                            osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = result1;
                        }
                        //osqlcommand.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "mobile_membermasteruinserted";
                        osqlcommand.Parameters.Add("@adduser", SqlDbType.VarChar, 50).Value = GlobalVariable.gUserName.ToString();
                        //osqlcommand.Parameters.Add("@col", SqlDbType.VarChar, 50).Value = "MembershipNo";
                        osqlcommand.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = "p256" + SMCODE;
                        //osqlcommand.ExecuteNonQuery();
                        if (desbool == true)
                        {
                            sqlstring = "UPDATE GSPIRNUPDATE SET void = 'Y',voidddatetime = '" + Strings.Format(DateTime.Now, "dd-MMM-yyyy HH:mm") + "',voiduser = '" + GlobalVariable.gUserName + "' where billdetails = '" + SMCODE + "' and isnull(Void,'') <> 'Y'";
                            List.Add(sqlstring);
                            if (GCon.Moretransaction(List) > 0)
                            { List.Clear(); }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public void EInvoicing_old(string Bno, string FYear)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable EData = new DataTable();
            DataTable GspDetails = new DataTable();

            sql = "Select  ISNULL((select case when (select Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "')<>'' then (Select  isnull(gstinno,'') from membermaster where mcode=(select top 1  Mcode from BILL_HDR where BillDetails='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) ";
            sql = sql + " else (select  isnull(GuestGSTIN,'')  from Tbl_HomeTakeAwayBill where KotNo=(select top 1  KOTDETAILS from KOT_DET where BILLDETAILS='" + Bno + "' AND ISNULL(FinYear,'') = '" + FYear + "')) end ),'') as gstinno ";
            EData = GCon.getDataSet(sql);
            if (EData.Rows.Count > 0)
            {
                sql = "  Select  gspappid,gspappsecret,user_name,password,gstin from gspdetails ";
                GspDetails = GCon.getDataSet(sql);
                if (GspDetails.Rows.Count > 0)
                {
                    string gspappid = "", gspappsecret = "", user_name = "", password = "", gstin = "";
                    gspappid = GspDetails.Rows[0].ItemArray[0].ToString();
                    gspappsecret = GspDetails.Rows[0].ItemArray[1].ToString();
                    user_name = GspDetails.Rows[0].ItemArray[2].ToString();
                    password = GspDetails.Rows[0].ItemArray[3].ToString();
                    gstin = GspDetails.Rows[0].ItemArray[4].ToString();
                    object xmlobj;
                    string strPostData, params1 = "";
                    string USERID, SID, PWD, SMCODE;
                    Int32 i;
                    string url;
                    WinHttpRequest HttpReq = new WinHttpRequest();
                    try
                    {
                        string token = "";
                        SMCODE = Bno;
                        url = "https://gsp.adaequare.com/gsp/authenticate?action=GSP&grant_type=token";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("gspappid", gspappid);
                        HttpReq.SetRequestHeader("gspappsecret", gspappsecret);
                        HttpReq.Send();
                        string result2;
                        result2 = HttpReq.ResponseText;
                        if (result2.Length > 1)
                        {
                            JObject ser = JObject.Parse(result2);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";

                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name.ToUpper())
                                {
                                    case "ACCESS_TOKEN":
                                        token = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                }
                            }
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        string insert;
                        DataTable dt = new DataTable();
                        sqlstring = "exec get_einvoicePOS '" + SMCODE + "'";
                        dt = GCon.getDataSet(sqlstring);
                        if (dt.Rows.Count > 0)
                        {
                            params1 = dt.Rows[0].ItemArray[0].ToString();
                        }
                        url = "https://gsp.adaequare.com/test/enriched/ei/api/invoice";
                        HttpReq.Open("POST", url, false);
                        HttpReq.SetRequestHeader("Content-Type", "application/json");
                        HttpReq.SetRequestHeader("user_name", user_name);
                        HttpReq.SetRequestHeader("password", password);
                        HttpReq.SetRequestHeader("gstin", gstin);
                        HttpReq.SetRequestHeader("requestid", "p255" + SMCODE);
                        HttpReq.SetRequestHeader("Authorization", "Bearer " + token);
                        HttpReq.Send(params1);
                        string result;
                        result = HttpReq.ResponseText;

                        SqlConnection CC = new SqlConnection();
                        CC.ConnectionString = GCon.GetConnection();
                        CC.Open();
                        SqlCommand osqlcommand = new SqlCommand();
                        osqlcommand = new SqlCommand("proc_GSPIRNUPDATE", CC);
                        osqlcommand.CommandType = CommandType.StoredProcedure;
                        Boolean desbool = false;
                        if (result.Length > 1)
                        {
                            JObject ser = JObject.Parse(result);
                            List<JToken> data = ser.Children().ToList();
                            string output = "";
                            desbool = false;
                            foreach (JProperty item in data)
                            {
                                item.CreateReader();
                                switch (item.Name)
                                {
                                    case "success":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "message":
                                        desbool = true;
                                        osqlcommand.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                        break;
                                    case "result":
                                        JObject ser1 = JObject.Parse(result);
                                        List<JToken> data1 = ser1.Children().ToList();
                                        string output1 = "";
                                        foreach (JProperty item1 in data1)
                                        {
                                            switch (item1.Name)
                                            {
                                                case "AckNo":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckNo", SqlDbType.VarChar, 1000).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "AckDt":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@AckDt", SqlDbType.DateTime).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "Irn":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Irn", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedInvoice":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedInvoice", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "SignedQRCode":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@SignedQRCode", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                                case "Status":
                                                    desbool = true;
                                                    osqlcommand.Parameters.Add("@Status", SqlDbType.Text).Value = item.Value.ToString().Substring(1, item.Value.ToString().Length - 2);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        if (desbool == false)
                        {
                            string result1 = "";
                            //result1 = result.Replace(vbCr, "").Replace(vbLf, "");
                            result1 = result1.Replace(",", "");
                            result1 = result1.Replace("'", "");
                            //result1 = result1.Replace("""", "");
                            osqlcommand.Parameters.Add("@val", SqlDbType.VarChar, 1000).Value = result1;
                        }
                        //osqlcommand.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "mobile_membermasteruinserted";
                        osqlcommand.Parameters.Add("@adduser", SqlDbType.VarChar, 50).Value = GlobalVariable.gUserName.ToString();
                        //osqlcommand.Parameters.Add("@col", SqlDbType.VarChar, 50).Value = "MembershipNo";
                        osqlcommand.Parameters.Add("@key", SqlDbType.VarChar, 50).Value = "p255" + SMCODE;
                        osqlcommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        private void Cmd_DelEInvoice_Click(object sender, System.EventArgs e)
        {
            string CBillNo = "", FYear = "";
            ArrayList List = new ArrayList();
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            FYear = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
            EinvoicingDelete(CBillNo, FYear);
            Cmd_Search_Click(sender, e);
        }

        private void Cmd_PrintBill_Click(object sender, System.EventArgs e)
        {
            
            DataTable BillData = new DataTable();
            string QrPath;
            FileStream qAFILE;
            String QrCodeCard,QrString;
            string CBillNo = "", FYear = "";
            int rowindex = dataGridView1.CurrentRow.Index;
            CBillNo = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            FYear = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
            sql = "SELECT billdetails,SignedQRCode,replace(replace(billdetails+reqid,'/',''),'-','') QrPicName FROM GSPIRNUPDATE WHERE BILLDETAILS = '" + CBillNo + "' AND ISNULL(VOID,'') <> 'Y'";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0) 
            {
                QrCodeCard = BillData.Rows[0].ItemArray[2].ToString();
                QrString = BillData.Rows[0].ItemArray[1].ToString();
                QrGenerate(QrString);
                if (QrCodeCard != "") 
                {
                    QrPath = Application.StartupPath + "/Reports/" + QrCodeCard + ".bmp" ;
                    if (File.Exists(QrPath)) 
                    {
                        File.Delete(QrPath);
                    }
                    Pic_QR.Image.Save(QrPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    WindowsPring(CBillNo, "O", QrPath);
                }
            }
        }

        public void WindowsPring(string Bno, string Type, string QrPath)
        {
            String sqlstring, sql, sql1;
            string CustPrint1 = "", CustPrint2 = "", CustPrint3 = "";
            Report rv = new Report();
            CRYSTAL.BillPrint RPS = new CRYSTAL.BillPrint();

            string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
            string SecLine = Add2 + ", " + City + "-" + PinNo;

            sql = "SELECT BILLDETAILS as BILLDETAILSTAX,A.taxdesc as TAXCODE,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAXAMT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
            sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc,BILLDETAILS  ";
            GCon.getDataSet1(sql, "KOT_DET_TAX");
            if (GlobalVariable.gdataset.Tables["KOT_DET_TAX"].Rows.Count > 0)
            {
                rv.GetDetails(sql, "KOT_DET_TAX", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sql1 = " SELECT BILLNO AS BillDetails,PAYMENTMODE,PAYAMOUNT AS TotalAmount FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
            GCon.getDataSet1(sql1, "BILL_HDR");
            if (GlobalVariable.gdataset.Tables["BILL_HDR"].Rows.Count > 0)
            {
                rv.GetDetails(sql1, "BILL_HDR", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
            }
            sqlstring = "  select * from BillPrintCrystal where billdetails = '" + Bno + "' and FinYear = '" + FinYear1 + "' ";
            GCon.getDataSet1(sqlstring, "BillPrintCrystal");
            if (GlobalVariable.gdataset.Tables["BillPrintCrystal"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "BillPrintCrystal", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);
            }

            DataTable CData = new DataTable();
            DataTable MData = new DataTable();
            DataTable ARMData = new DataTable();
            DataTable RoomData = new DataTable();
            sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
            MData = GCon.getDataSet(sql);
            if (MData.Rows.Count > 0)
            {
                var RData1 = MData.Rows[0];
                CustPrint1 = "MCODE: " + RData1["MCODE"];
                CustPrint2 = "MNAME: " + RData1["MNAME"];
                CustPrint3 = "";
            }
            else
            {
                sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "') ";
                ARMData = GCon.getDataSet(sql);
                if (ARMData.Rows.Count > 0)
                {
                    var RData1 = ARMData.Rows[0];
                    string ArGSt = GCon.getValue("SELECT isnull(GSTINNO,'') as GSTINNO FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And slcode = '" + RData1["ARCode"].ToString() + "' ").ToString();
                    CustPrint1 = "AR Code: " + RData1["ARCode"];
                    CustPrint2 = "AR Name: " + RData1["ARName"];
                    CustPrint3 = "GSTIN  : " + ArGSt;
                }
                else
                {
                    sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
                    CData = GCon.getDataSet(sql);
                    if (CData.Rows.Count > 0)
                    {
                        var RData1 = CData.Rows[0];
                        CustPrint1 = RData1["GuestName"].ToString();
                        CustPrint2 = "GSTIN: " + RData1["GuestGSTIN"];
                        CustPrint3 = "ADD: " + RData1["GuestAdd"];
                    }
                }
            }
            sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS AND H.FinYear = D.FinYear and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' AND ISNULL(d.FinYear,'') = '" + FinYear1 + "' ";
            RoomData = GCon.getDataSet(sql);
            if (RoomData.Rows.Count > 0)
            {
                var RData1 = RoomData.Rows[0];
                CustPrint1 = "Guest Name: " + RData1["Mname"];
                CustPrint2 = "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]";
                CustPrint3 = "";
            }
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
            TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text27"];
            TXTOBJ1.Text = CustPrint1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
            TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text28"];
            TXTOBJ2.Text = CustPrint2;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
            TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text30"];
            TXTOBJ3.Text = CustPrint3;

            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ4;
            TXTOBJ4 = (TextObject)RPS.ReportDefinition.ReportObjects["Text1"];
            TXTOBJ4.Text = GlobalVariable.gCompanyName;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ5;
            TXTOBJ5 = (TextObject)RPS.ReportDefinition.ReportObjects["Text2"];
            TXTOBJ5.Text = Add1;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ6;
            TXTOBJ6 = (TextObject)RPS.ReportDefinition.ReportObjects["Text3"];
            TXTOBJ6.Text = SecLine;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ7;
            TXTOBJ7 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
            TXTOBJ7.Text = "GSTIN:-" + GSTIN;
            CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ8;
            TXTOBJ8 = (TextObject)RPS.ReportDefinition.ReportObjects["Text5"];
            TXTOBJ8.Text = "TEL NO:" + Phone;

            RPS.SetParameterValue("ImageUrl", QrPath);

            rv.Show();
        }

        public void QrGenerate(string Qry) 
        {
            string TxtString;
            if (Qry != "")
            {
                TxtString = Qry;
                ////MessagingToolkit.QRCode.Codec.QRCodeEncoder QR_Generator = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
                ////QR_Generator.QRCodeErrorCorrect = MessagingToolkit.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H;
                ////QR_Generator.QRCodeScale = 8;
                ////QR_Generator.QRCodeEncodeMode = MessagingToolkit.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                ////Bitmap bmp = new Bitmap(QR_Generator.Encode(TxtString), new Size(200, 200));
                Pic_QR.Image = QR_Generator.Encode(TxtString);
            }
        }

        public void QrGenerate1(string Qry,string savename)
        {
            string path = @"d:\\QRCode\\" + savename + ".bmp";  
            MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();  
            encoder.QRCodeScale = 8;
            Bitmap bmp = new Bitmap(encoder.Encode(Qry), new Size(200, 200));
            Pic_QR.Image = bmp;  
            var newBmp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format8bppIndexed);  
            
          //  folderBrowserDialog1.SelectedPath = path;  
                              
          //      if (text_Invoice.Text.Trim() == string.Empty)  
          //      {  
          //          MessageBox.Show("Please enter Invoce number before generate the QRCode");  
          //          return;   
          //      }  
          //      else if (textencode.Text.Trim() == string.Empty)  
          //      {  
          //          MessageBox.Show("Please select the text file ");  
          //          return;  
          //      }  
          //      else  
          //      {  
          //          newBmp.Save(path, ImageFormat.Bmp);  
          //          MessageBox.Show("QRCode  "+ text_Invoice.Text+ ".bmp save sucessfully");  
  
          //          textencode.Text = "";  
          //          text_Invoice.Text = "";  
          //      }  
          //   }  
        }
    }
}
