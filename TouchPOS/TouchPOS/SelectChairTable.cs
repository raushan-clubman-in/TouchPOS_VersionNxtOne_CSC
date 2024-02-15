using System;
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
    public partial class SelectChairTable : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string TableNumber = "";
        public int loccode = 0;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public readonly ServiceLocation _form1;

        public SelectChairTable(ServiceLocation form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void SelectChairTable_Load(object sender, EventArgs e)
        {
            label2.Text = "Chair List For Table No:" + TableNumber;
            FillChiar();
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
                PHeight = (groupBox1.Height - 20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[3].ToString() + " (Amt " + dr1[2].ToString() + ")";
                    btn.Tag = dr1[3].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.Red;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 400;
                    btn.Height = PHeight;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Location = new Point(X, Y);
                    groupBox1.Controls.Add(btn);
                    btn.Click += new EventHandler(button1_Click);
                    Y = Y + (PHeight + 10);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            Button selectedBtn = sender as Button;
            this.Hide();
            _form1.AddChairFlag = false;
            _form1.AddChairEntry(TableNumber, Convert.ToInt32(selectedBtn.Tag.ToString()), loccode);
        }
    }
}
