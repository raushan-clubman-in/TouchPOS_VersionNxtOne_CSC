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
    public partial class MemberTagging : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public MemberTagging(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtCode = new DataTable();
        DataTable dtName = new DataTable();

        private void MemberTagging_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            DataTable KotNonCheck = new DataTable();
            AutoCompleteCode();
            AutoCompleteName();
            sql = "select Isnull(MCODE,'') as MCODE,Isnull(MCODE,'') as MCODE,Isnull(MNAME,'') as MNAME from KOT_HDR Where Kotdetails = '" + KotOrderNo + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                Txt_MCode.Text = KotNonCheck.Rows[0].ItemArray[1].ToString();
                Txt_MName.Text = KotNonCheck.Rows[0].ItemArray[2].ToString();
            }
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "Member Tagging or Changes";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void AutoCompleteCode()
        {
            sql = "SELECT MCODE,MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') in ('ACTIVE','LIVE') ";
            dtCode = GCon.getDataSet(sql);
            string[] postSource = dtCode
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MCODE"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_MCode.AutoCompleteCustomSource = source;
            Txt_MCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_MCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void AutoCompleteName()
        {
            sql = "SELECT MCODE,MNAME+'=>'+MCODE as MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') in ('ACTIVE','LIVE')";
            dtName = GCon.getDataSet(sql);
            string[] postSource1 = dtName
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("MNAME"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource1);
            Txt_MName.AutoCompleteCustomSource = source1;
            Txt_MName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_MName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Txt_MCode_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable GCheck = new DataTable();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "SELECT MCODE,MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') in ('ACTIVE','LIVE') And MCODE = '" + Txt_MCode.Text + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_MName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                }
            }
        }

        private void Txt_MName_KeyDown(object sender, KeyEventArgs e)
        {
            string[] SplitCode = { "", "" };
            DataTable GCheck = new DataTable();
            SplitCode = Txt_MName.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "SELECT MCODE,MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') in ('ACTIVE','LIVE') And MCODE = '" + SplitCode[1] + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_MCode.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_MName.Text = GCheck.Rows[0].ItemArray[1].ToString();
                }
            }
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_MCode.Text = "";
            Txt_MName.Text = "";
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
            if (Txt_MCode.Text == "" || Txt_MName.Text == "")
            {
                MessageBox.Show("Code & Name Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            sql = "SELECT MCODE,MNAME FROM MEMBERMASTER WHERE ISNULL(CURENTSTATUS,'') in ('ACTIVE','LIVE') And MCODE = '" + Txt_MCode.Text + "'";
            GCheck = GCon.getDataSet(sql);
            if (GCheck.Rows.Count == 0) 
            {
                MessageBox.Show("Select Member Not Found! Please Check in Member Control.", GlobalVariable.gCompanyName);
                return;
            }
            sqlstring = " UPDATE KOT_HDR SET MCODE = '" + Txt_MCode.Text + "',Mname = '" + Txt_MName.Text + "' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y'";
            List.Add(sqlstring);
            sqlstring = " UPDATE KOT_DET SET MCODE = '" + Txt_MCode.Text + "' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' ";
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
