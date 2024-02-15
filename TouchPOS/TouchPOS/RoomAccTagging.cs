using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class RoomAccTagging : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public RoomAccTagging(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtCheckin = new DataTable();
        DataTable dtRoom = new DataTable();
        DataTable dtGuest = new DataTable();
       
        private void RoomAccTagging_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            DataTable KotNonCheck = new DataTable();
            //AutoCompleteCheckin();
            //AutoCompleteRoom();
            //AutoCompleteRoomGuest();

            DataTable ChekinData = new DataTable();
            sql = "select Isnull(First_name,'') + ' '+ isnull(Middlename,'') as GuestName,Isnull(RoomNo,0) as RoomNo,Isnull(ChkNo,0) as CheckinNo from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> 0 And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            ChekinData = GCon.getDataSet(sql);
            if (ChekinData.Rows.Count > 0) 
            {
                BindingSource SBind = new BindingSource();
                SBind.DataSource = ChekinData;

                dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = SBind;

                //set DGV's column names and headings from the Datatable properties
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DataPropertyName = ChekinData.Columns[i].ColumnName;
                    dataGridView1.Columns[i].HeaderText = ChekinData.Columns[i].Caption;
                }
                dataGridView1.ForeColor = Color.Black;
                dataGridView1.Enabled = true;
                dataGridView1.Refresh();
                this.dataGridView1.Columns[0].ReadOnly = true;
                this.dataGridView1.Columns[1].ReadOnly = true;
                this.dataGridView1.Columns[2].ReadOnly = true;
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[0].Width = 200;
            }

            sql = "select Checkin,RoomNo,Guest from kot_hdr Where Kotdetails = '" + KotOrderNo + "' And Isnull(Checkin,0) <> 0 And Isnull(RoomNo,0) <> '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0)
            {
                Txt_CheckNo.Text = KotNonCheck.Rows[0].ItemArray[0].ToString();
                Txt_RoomNo.Text = KotNonCheck.Rows[0].ItemArray[1].ToString();
                Txt_GuestName.Text = KotNonCheck.Rows[0].ItemArray[2].ToString();
            }
            
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "Room Tagging For Bill  ";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void AutoCompleteCheckin()
        {
            sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') +'=>'+cast(ChkNo as varchar(10)) ChkGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            dtCheckin = GCon.getDataSet(sql);
            string[] postSource = dtCheckin
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ChkGuest"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_CheckNo.AutoCompleteCustomSource = source;
            Txt_CheckNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_CheckNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void AutoCompleteRoom()
        {
            //sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') +'=>'+cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            sql = "select ChkNo,cast(Roomno as varchar(10)) + '=>'+Isnull(First_name,'') + ' '+ isnull(Middlename,'')  RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            dtRoom = GCon.getDataSet(sql);
            string[] postSource1 = dtRoom
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("RoomGuest"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource1);
            Txt_RoomNo.AutoCompleteCustomSource = source1;
            Txt_RoomNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_RoomNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void AutoCompleteRoomGuest()
        {
            sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') +'=>'+cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') + '=>'+ cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate And Isnull(First_name,'') + ' '+ isnull(Middlename,'') like '%" + Txt_GuestName.Text + "%' ";
            dtGuest = GCon.getDataSet(sql);
            string[] postSource2 = dtGuest
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("RoomGuest"))
                    .ToArray();
            var source2 = new AutoCompleteStringCollection();
            source2.AddRange(postSource2);
            Txt_GuestName.AutoCompleteCustomSource = source2;
            Txt_GuestName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_GuestName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Txt_CheckNo_KeyDown(object sender, KeyEventArgs e)
        {
            string[] SplitCode = { "", "" };
            DataTable GCheck = new DataTable();
            SplitCode = Txt_CheckNo.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "Select ChkNo,Roomno,First_name + ' '+ Middlename as GuestName from RoomCheckin Where ChkNo = '" + SplitCode[1] + "'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_CheckNo.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_RoomNo.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[2].ToString();
                }
            }
        }

        private void Txt_RoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            string[] SplitCode = { "", "" };
            DataTable GCheck = new DataTable();
            SplitCode = Txt_RoomNo.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "Select ChkNo,Roomno,First_name + ' '+ Middlename as GuestName from RoomCheckin Where Roomno = '" + SplitCode[0] + "' And Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_CheckNo.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_RoomNo.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[2].ToString();
                }
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_CheckNo.Text = "";
            Txt_RoomNo.Text = "";
            Txt_GuestName.Text = "";
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            DataTable GCheck = new DataTable();
            if (Txt_CheckNo.Text == "" || Txt_RoomNo.Text == "" || Txt_GuestName.Text == "")
            {
                MessageBox.Show("Checkin,RoomNo & Name Can't Blank", GlobalVariable.gCompanyName);
                return;
            }
            DataTable KotCheck = new DataTable();
            sql = "select * from kot_hdr Where Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
            KotCheck = GCon.getDataSet(sql);
            if (KotCheck.Rows.Count > 0)
            {
                sqlstring = "UPDATE kot_hdr SET Checkin = " + Txt_CheckNo.Text + ", RoomNo = '" + Txt_RoomNo.Text + "',Guest = '" + Txt_GuestName.Text + "'  Where Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                string Member = Convert.ToString(GCon.getValue("SELECT MCODE FROM MEMBERMASTER WHERE Membertypecode not in ('NM','NMG') And MCODE IN (select mcode from KOT_HDR WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y')"));
                if (Member != "") 
                {
                    sqlstring = " UPDATE KOT_HDR SET MCODE = '',Mname = '',CARDHOLDERCODE = '',CARDHOLDERNAME = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y'";
                    List.Add(sqlstring);
                    sqlstring = " UPDATE KOT_DET SET MCODE = '' WHERE Kotdetails = '" + KotOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND ISNULL(DELFLAG,'') <> 'Y' ";
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

        private void Txt_GuestName_TextChanged(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand();
            //ConnectionString css = new ConnectionString();
            //SqlConnection Myconn = new SqlConnection();
            //try
            //{
            //    Txt_GuestName.AutoCompleteMode = AutoCompleteMode.Suggest;
            //    Txt_GuestName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            //    Myconn.ConnectionString = css.ReadCS();
            //    Myconn.Open();
            //    sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') + '=>'+ cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate And Isnull(First_name,'') + ' '+ isnull(Middlename,'') like '%" + Txt_GuestName.Text + "%' ";
            //    cmd = new SqlCommand(sql, Myconn);
            //    SqlDataReader sdr = null;
            //    sdr = cmd.ExecuteReader();
            //    while (sdr.Read())
            //    {
            //        col.Add(sdr["RoomGuest"].ToString());
            //    }
            //    sdr.Close();

            //    Txt_GuestName.AutoCompleteCustomSource = col;
            //    Myconn.Close();
            //}
            //catch
            //{
            //}
        }

        public string[] SuggestStrings(string abc)
        {
            //sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') +'=>'+cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate ";
            //sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') + '=>'+ cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate And Isnull(First_name,'') + ' '+ isnull(Middlename,'') like '%" + Txt_GuestName.Text + "%' ";
            //dtGuest = GCon.getDataSet(sql);
            string[] postSource3 = dtGuest
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("RoomGuest"))
                    .ToArray();
            return postSource3;
        }

        private void Txt_RoomNo_TextChanged(object sender, EventArgs e)
        {
            //sql = "select ChkNo,Isnull(First_name,'') + ' '+ isnull(Middlename,'') +'=>'+cast(Roomno as varchar(10)) RoomGuest from RoomCheckin Where Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y' And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between Arrivaldate And Deptdate And Isnull(First_name,'') + ' '+ isnull(Middlename,'') Like '%" + Txt_RoomNo.Text + "%' ";
            //dtRoom = GCon.getDataSet(sql);
            //var source1 = new AutoCompleteStringCollection();
            //String[] stringArray = Array.ConvertAll<DataRow, String>(dtRoom.Select(), delegate(DataRow row) { return (String)row["RoomGuest"]; });
            //source1.AddRange(stringArray);
            //TextBox prodCode = Txt_RoomNo;
            //if (prodCode != null)
            //{
            //    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //    prodCode.AutoCompleteCustomSource = source1;
            //    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //}
        }

        private void Txt_GuestName_KeyDown(object sender, KeyEventArgs e)
        {
            //AutoCompleteRoomGuest();
            string[] SplitCode = { "", "" };
            DataTable GCheck = new DataTable();
            SplitCode = Txt_GuestName.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                sql = "Select ChkNo,Roomno,First_name + ' '+ Middlename as GuestName from RoomCheckin Where Roomno = '" + SplitCode[1] + "' And Isnull(ChkNo,0) <> 0 And Isnull(RoomNo,0) <> '' And Isnull(CheckOut,'') <> 'Y'";
                GCheck = GCon.getDataSet(sql);
                if (GCheck.Rows.Count > 0)
                {
                    Txt_CheckNo.Text = GCheck.Rows[0].ItemArray[0].ToString();
                    Txt_RoomNo.Text = GCheck.Rows[0].ItemArray[1].ToString();
                    Txt_GuestName.Text = GCheck.Rows[0].ItemArray[2].ToString();
                }
            }
            
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataIndexNo = dataGridView1.Rows[e.RowIndex].Index.ToString();
            int cellValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            DataTable GCheck = new DataTable();
            sql = "Select ChkNo,Roomno,First_name + ' '+ Middlename as GuestName from RoomCheckin Where ChkNo = '" + cellValue + "'";
            GCheck = GCon.getDataSet(sql);
            if (GCheck.Rows.Count > 0)
            {
                Txt_CheckNo.Text = GCheck.Rows[0].ItemArray[0].ToString();
                Txt_RoomNo.Text = GCheck.Rows[0].ItemArray[1].ToString();
                Txt_GuestName.Text = GCheck.Rows[0].ItemArray[2].ToString();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
