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
    public partial class AddChairTable : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string TableNumber = "";
        public int loccode = 0;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public readonly ServiceLocation _form1;

        public AddChairTable(ServiceLocation form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        int lastChairno = 1;

        private void AddChairTable_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            label1.Text = "Chair List For Table No:" + TableNumber;
            FillChiar();
            TxtChair.Text = (lastChairno+1).ToString();
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            //myGroupBox myGroupBox1 = new myGroupBox();
            //myGroupBox1.Text = "";
            //myGroupBox1.BorderColor = Color.Black;
            //myGroupBox1.Size = groupBox1.Size;
            //groupBox1.Controls.Add(myGroupBox1);
        }

        private void FillChiar() 
        {
            int PHeight = 0;
            DataTable Btndt = new DataTable();
            sql = "Select TableNo,'-7270000' BkColor,sum(isnull(BillAmount,0)) AS GrandTotal,ChairSeqNo from Kot_Hdr where TableNo = '" + TableNumber + "' and LocCode = " + loccode + "  And KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(delflag,'') <> 'Y' AND BILLSTATUS = 'PO' AND ISNULL(FinYear,'') = '" + FinYear1 + "' group by TableNo,ChairSeqNo";
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0) 
            {
                int X = 10;
                int Y = 10;
                PHeight = (groupBox1.Height -20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows) 
                {
                    Button btn = new Button();
                    btn.Text = dr1[3].ToString() + " (Amt " + dr1[2].ToString() + ")";
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.Red;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 360;
                    btn.Height = PHeight;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Location = new Point(X, Y);
                    groupBox1.Controls.Add(btn);
                    Y = Y + (PHeight+10);
                    lastChairno = Convert.ToInt16(dr1[3]);
                }
            }
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_7.Text;
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_8.Text;
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_9.Text;
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_4.Text;
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_5.Text;
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_6.Text;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_2.Text;
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_3.Text;
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text + Button_0.Text;
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            TxtChair.Text = "";
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            _form1.AddChairFlag = false;
            this.Hide();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            DataTable Chkdt = new DataTable();
            bool ChkChair = false;
            sql = "Select TableNo,'-7270000' BkColor,sum(isnull(BillAmount,0)) AS GrandTotal,ChairSeqNo from Kot_Hdr where TableNo = '" + TableNumber + "' and LocCode = " + loccode + "  And KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(delflag,'') <> 'Y' AND BILLSTATUS = 'PO' AND ISNULL(FinYear,'') = '" + FinYear1 + "' group by TableNo,ChairSeqNo";
            Chkdt = GCon.getDataSet(sql);
            if (Chkdt.Rows.Count > 0) 
            {
                for (int i = 0; i <= Chkdt.Rows.Count - 1; i++) 
                {
                    var RData = Chkdt.Rows[i];
                    if (Convert.ToInt32(RData["ChairSeqNo"]) == Convert.ToInt32(TxtChair.Text))
                    {
                        MessageBox.Show("Sorry given Chair Already in Use Plz Select Another one", GlobalVariable.gCompanyName);
                        TxtChair.Text = "";
                        ChkChair = true;
                    }
                }
                if (ChkChair == true)
                { return; }
                else
                {
                    ArrayList List = new ArrayList();
                    string sqlstring = "";
                    sqlstring = " Insert into Tbl_TableAddedChair Values (" + loccode + ",'" + TableNumber + "'," + Convert.ToInt32(TxtChair.Text) + ")";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                        this.Hide();
                        _form1.AddChairFlag = false;
                        _form1.AddChairEntry(TableNumber, Convert.ToInt32(TxtChair.Text),loccode);
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtChair.Text = TxtChair.Text.Remove(TxtChair.Text.Length - 1, 1);
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
