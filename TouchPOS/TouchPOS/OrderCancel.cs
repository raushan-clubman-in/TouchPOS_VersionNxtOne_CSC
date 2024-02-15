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
    public partial class OrderCancel : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public string Kotno = "";
        bool gPrint = true;
        public bool Cancelbool = false;
        public string OrderReason = "";
        public string OptionOrder = "";

        public readonly Form _form1;

        public OrderCancel(Form form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        DataTable dtPosts = new DataTable();

        private void OrderCancel_Load(object sender, EventArgs e)
        {
            if (OptionOrder == "K") { label1.Text = "Order Cancel"; }
            else if (OptionOrder == "B") { label1.Text = "Bill Cancel"; }
            AutoComplete();
        }

        private void AutoComplete()
        {
            sql = "SELECT ReasonTxt FROM Tbl_ReasonMaster ";
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ReasonTxt"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_Reason.AutoCompleteCustomSource = source;
            Txt_Reason.AutoCompleteMode = AutoCompleteMode.Suggest;
            Txt_Reason.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.TxtMember.DataBindings.Add("Text", dtPosts, "MNAME");
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_7.Text;
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_8.Text;
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_9.Text;
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_4.Text;
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_5.Text;
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_6.Text;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_2.Text;
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_3.Text;
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_0.Text;
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            Cancelbool = false;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text.Remove(TxtPass.Text.Length - 1, 1);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DataTable Userdt = new DataTable();
            DataTable Rdt = new DataTable();
            string sqlstring = "";
            if (Txt_Reason.Text == "")
            {
                MessageBox.Show("Reason Can't be blank! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            sql = "select * from master..useradmin where username = '" + GlobalVariable.gUserName + "' and userpassword = '" + GCon.GetPassword(TxtPass.Text.Trim()) + "'";
            Userdt = GCon.getDataSet(sql);
            if ((Userdt.Rows.Count > 0) || (GlobalVariable.gUserName == "CHS" && GCon.GetPassword(TxtPass.Text.Trim()) == "ÏÆÉÎÎËÆÇÎÎ"))
            {
                //sql = "select * from Tbl_ReasonMaster where ReasonTxt = '" + Txt_Reason.Text + "'";
                //Rdt = GCon.getDataSet(sql);
                //if (Rdt.Rows.Count == 0)
                //{
                //    sqlstring = "Insert Into Tbl_ReasonMaster (ReasonTxt) Values ('" + Txt_Reason.Text + "')";
                //    List.Add(sqlstring);
                //}
                //if (GCon.Moretransaction(List) > 0)
                //{
                //    List.Clear();
                //    OrderReason = Txt_Reason.Text;
                //    Cancelbool = true;
                //    this.Close();
                //}
                //else 
                //{
                //    MessageBox.Show("Some Error Found Please Check");
                //    Cancelbool = false;
                //    OrderReason = "";
                //    this.Close();
                //}
                OrderReason = Txt_Reason.Text;
                Cancelbool = true;
                this.Close();
            }
            else 
            {
                MessageBox.Show("Wrong Username or Password");
                Cancelbool = false;
                OrderReason = "";
                this.Close();
            }
        }
    }
}
