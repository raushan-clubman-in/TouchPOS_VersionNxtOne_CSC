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
    public partial class PosSelection : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string ItemCode = "";
        public int loccode = 0;
        public string PosCode = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public readonly EntryForm _form1;

        public PosSelection(EntryForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void PosSelection_Load(object sender, EventArgs e)
        {
            FillPos();
        }

        private void FillPos()
        {
            int PHeight = 0;
            DataTable Btndt = new DataTable();
            sql = " select P.Pos,M.POSDesc from PosMenuLink P,PosMaster M where P.Pos = M.Poscode And itemcode = '" + ItemCode + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + loccode + ")";
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0)
            {
                int X = 10;
                int Y = 10;
                PHeight = (groupBox1.Height - 20) / Btndt.Rows.Count;
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[1].ToString();
                    btn.Tag = dr1[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.Red;
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
            PosCode = selectedBtn.Tag.ToString();
            //_form1.AddChairFlag = false;
            //_form1.AddChairEntry(TableNumber, Convert.ToInt32(selectedBtn.Tag.ToString()), loccode);
        }

    }
}
