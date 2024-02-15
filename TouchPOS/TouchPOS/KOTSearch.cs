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

namespace TouchPOS
{
    public partial class KOTSearch : Form
    {
        GlobalClass GCon = new GlobalClass();

        public KOTSearch()
        {
            InitializeComponent();
        }

        string sql = "";

        private void KOTSearch_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = "Business Date: " + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");

            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;

            Dtp_FromDate.MinDate = GlobalVariable.ServerDate;
            Dtp_FromDate.MaxDate = GlobalVariable.ServerDate;
            Dtp_ToDate.MinDate = GlobalVariable.ServerDate;
            Dtp_ToDate.MaxDate = GlobalVariable.ServerDate;
            if (GlobalVariable.gUserCategory != "S") { GetRights(); }

        }

        private void GetRights()
        {
            DataTable Rights = new DataTable();
            Cmd_Delete.Enabled = false;
            sql = "select Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM from Tbl_TransactionFormUserTag Where FormName = 'KOT ENTRY FORM' And UserName = '" + GlobalVariable.gUserName + "' ";
            Rights = GCon.getDataSet(sql);
            if (Rights.Rows.Count > 0)
            {
                if (Rights.Rows[0].ItemArray[2].ToString() == "Y")
                { Cmd_Delete.Enabled = true; }
                else { Cmd_Delete.Enabled = false; }
            }
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);

            myGroupBox myGroupBox2 = new myGroupBox();
            myGroupBox2.Text = "";
            myGroupBox2.BorderColor = Color.Black;
            myGroupBox2.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox2);
        }

        private void Cmd_Search_Click(object sender, EventArgs e)
        {
            DataTable BillData = new DataTable();
            sql = "SELECT KotDetails,KotDate,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' and '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' and '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
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

        private void Cmd_BPOS_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Hide();
        }

        private void Cmd_Delete_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string OrderNo = "";
            string OrderReason = "";
            bool CancelBool = false;
            int rowindex = dataGridView1.CurrentRow.Index;
            string Kno = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();

            OrderNo = Kno;
            OrderCancel OC = new OrderCancel(this);
            OC.Kotno = OrderNo;
            OC.ShowDialog();
            OrderReason = OC.OrderReason;
            CancelBool = OC.Cancelbool;
            if (CancelBool == false || OrderReason == "")
            {
                return;
            }

            DataTable BTData = new DataTable();
            sql = "SELECT KotDetails,KotDate,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And KotDetails = '" + Kno + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And KotDetails = '" + Kno + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc";
            BTData = GCon.getDataSet(sql);
            if (BTData.Rows.Count > 0)
            {
                OrderNo = BTData.Rows[0].ItemArray[0].ToString();
                sqlstring = " UPDATE KOT_HDR SET DelFlag = 'Y',DELUSER = '" + GlobalVariable.gUserName + "' ,DELDATETIME = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET SET DelFlag = 'Y' ,KotStatus = 'Y',reason = '" + OrderReason + "' ,UpdUserid = '" + GlobalVariable.gUserName + "' ,Upddatetime = '" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = " UPDATE KOT_DET_TAX SET KOTSTATUS = 'Y',VOID = 'Y',VOIDUSER= '" + GlobalVariable.gUserName + "',VOIDDATE ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = "DELETE FROM closingqty where TRNNO = '" + OrderNo + "' ";
                List.Add(sqlstring);
                sqlstring = "UPDATE SUBSTORECONSUMPTIONDETAIL SET Void = 'Y' WHERE Docdetails = '" + OrderNo + "'";
                List.Add(sqlstring);
               
                if (GCon.Moretransaction(List) > 0)
                {
                    MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RefreshGrid()
        {
            DataTable BillData = new DataTable();
            sql = "SELECT KotDetails,KotDate,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' and '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "' and '" + Dtp_ToDate.Value.ToString("dd-MMM-yyyy") + "' And isnull(Delflag,'') <> 'Y') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
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
