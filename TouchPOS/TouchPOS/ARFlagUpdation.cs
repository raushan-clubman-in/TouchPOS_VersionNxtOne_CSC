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
    public partial class ARFlagUpdation : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ARFlagUpdation(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtCode = new DataTable();
        DataTable dtName = new DataTable();

        private void ARFlagUpdation_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            DataTable KotNonCheck = new DataTable();
            AutoCompleteCode();
            AutoCompleteName();
            sql = "select Isnull(ACCode,'') as ACCode,Isnull(ARCode,'') as ARCode,Isnull(ArName,'') as ArName from Tbl_ARFlagUpdation Where KotNo = '" + KotOrderNo + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                Txt_ARCode.Text = KotNonCheck.Rows[0].ItemArray[1].ToString();
                Txt_ARName.Text = KotNonCheck.Rows[0].ItemArray[2].ToString();
            }
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "AR Tagging For Bill  ";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void AutoCompleteCode()
        {
            sql = "SELECT slcode,slname FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' ";
            dtCode = GCon.getDataSet(sql);
            string[] postSource = dtCode
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("slcode"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_ARCode.AutoCompleteCustomSource = source;
            Txt_ARCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_ARCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void AutoCompleteName()
        {
            sql = "SELECT slcode,slname+'=>'+slcode as slname FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' ";
            dtName = GCon.getDataSet(sql);
            string[] postSource1 = dtName
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("slname"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource1);
            Txt_ARName.AutoCompleteCustomSource = source1;
            Txt_ARName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_ARName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Txt_ARCode_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "SELECT slcode,slname FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And Slcode = '" + Txt_ARCode.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_ARName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                }
            }
        }

        private void Txt_ARName_KeyDown(object sender, KeyEventArgs e)
        {
            string[] SplitCode = { "", "" };
            DataTable GCheck = new DataTable();
            SplitCode = Txt_ARName.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "SELECT slcode,slname FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And Slcode = '" + SplitCode[1] + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_ARCode.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_ARName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                }
            }
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_ARCode.Text = "";
            Txt_ARName.Text = "";
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable GCheck = new DataTable();
            if (Txt_ARCode.Text == "" || Txt_ARName.Text == "")
            {
                MessageBox.Show("Code & Name Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            sql = "SELECT slcode,slname FROM ACCOUNTSSUBLEDGERMASTER WHERE ACCODE = '" + GlobalVariable.AR_ACCode + "' And Slcode = '" + Txt_ARCode.Text + "'";
            GCheck = GCon.getDataSet(sql);
            if (GCheck.Rows.Count == 0)
            {
                MessageBox.Show("Select AR Member Not Found! Please Check in AR Control Master.", GlobalVariable.gCompanyName);
                return;
            }

            DataTable KotCheck = new DataTable();
            sql = "select * from Tbl_ARFlagUpdation Where KotNo = '" + KotOrderNo + "'";
            KotCheck = GCon.getDataSet(sql);
            if (KotCheck.Rows.Count > 0)
            {
                sqlstring = "UPDATE Tbl_ARFlagUpdation SET ARCode = '" + Txt_ARCode.Text + "', ArName = '" + Txt_ARName.Text + "',ACCode = '" + GlobalVariable.AR_ACCode + "'  Where KotNo = '" + KotOrderNo + "' ";
                List.Add(sqlstring);
                string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
                if (Member != "")
                {
                    sqlstring = " UPDATE KOT_HDR SET MCODE = '',Mname = '',CARDHOLDERCODE = '',CARDHOLDERNAME = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y'";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_DET SET MCODE = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' ";
                    List.Add(sqlstring);
                }
                string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
                if (RoomMember != "")
                {
                    sqlstring = "UPDATE kot_hdr SET Checkin = 0, RoomNo = '0',Guest = ''  Where Kotdetails = '" + KotOrderNo + "' ";
                    List.Add(sqlstring);
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    MessageBox.Show("Infomation Updated Successfully");
                    this.Hide();
                }
            }
            else
            {
                sqlstring = "Insert Into Tbl_ARFlagUpdation (KotNo,ACCode,ARCode,ARName,AddUser,AddDate) ";
                sqlstring = sqlstring + " Values ('" + KotOrderNo + "','" + GlobalVariable.AR_ACCode + "','" + Txt_ARCode.Text + "','" + Txt_ARName.Text + "','" + GlobalVariable.gUserName + "',getdate())";
                List.Add(sqlstring);
                string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
                if (Member != "")
                {
                    sqlstring = " UPDATE KOT_HDR SET MCODE = '',Mname = '',CARDHOLDERCODE = '',CARDHOLDERNAME = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y'";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_HDR SET MCODE = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' ";
                }
                string RoomMember = Convert.ToString(GCon.getValue("select Checkin from kot_hdr where Checkin in (select ChkNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate) And Isnull(RoomNo,0) <> 0 And Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' "));
                if (RoomMember != "") 
                {
                    sqlstring = "UPDATE kot_hdr SET Checkin = 0, RoomNo = '0',Guest = '0'  Where Kotdetails = '" + KotOrderNo + "' ";
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
    }
}
