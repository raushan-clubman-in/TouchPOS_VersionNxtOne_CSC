using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class UOMRateSelection : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string ItemCode = "";
        public int loccode = 0;
        public string UomCode = "";
        public double UomRate = 0;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public readonly EntryForm _form1;

        public UOMRateSelection(EntryForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void UOMRateSelection_Load(object sender, EventArgs e)
        {
            FillUom();
        }

        private void FillUom()
        {
            int PHeight = 0;
            DataTable Btndt = new DataTable();
            if (GlobalVariable.gCompName == "CSC")
            {
                sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode And R.itemcode = '" + ItemCode + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
            }
            else 
            {
                sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode and P.Pos = R.Rposcode And R.itemcode = '" + ItemCode + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
            }
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0)
            {
                int X = 10;
                int Y = 10;
                PHeight = (groupBox1.Height - 20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[0].ToString() + " ==> " + dr1[1].ToString();
                    btn.Tag = dr1[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.Blue;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 270;
                    btn.Height = PHeight;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            UomCode = selectedBtn.Tag.ToString();
            string[] SplitCode = { "", "" };
            SplitCode = selectedBtn.Text.ToString().Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            //string[] words = selectedBtn.Text.ToString().Split('==>');
            UomRate = Convert.ToDouble(SplitCode[1]);

        }
    }
}
