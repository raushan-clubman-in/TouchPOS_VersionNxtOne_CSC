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
    public partial class ChangeUserPassword : Form
    {
        GlobalClass GCon = new GlobalClass();
        public string SelUsername = "";
        public readonly Form1 _form1;

        public ChangeUserPassword(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtPass.Text = TxtPass.Text + Button_0.Text;
        }

        private void ChangeUserPassword_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            sql = "select DISTINCT USERNAME from master..useradmin order by USERNAME";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_User.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_User.Items.Add(dt.Rows[i]["USERNAME"].ToString());
                }
                Cmb_User.SelectedIndex = 0;
            }
            Cmb_User.SelectedItem = SelUsername;
            Cmb_User.Enabled = false;
            TxtPass.Focus();
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

        private void ButtonC_Click(object sender, EventArgs e)
        {
            TxtPass.Text = "";
            TxtPass.Focus();
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            string user = "";
            string NPass = "";
            if (TxtPass.Text != "") 
            {
                user = Cmb_User.Text.Trim();
                NPass = GCon.abcdAdd(TxtPass.Text.Trim());
                sql = "Update master..useradmin set userpassword = '" + NPass + "' where username = '" + user + "' ";
                GCon.dataOperation(1, sql);
                MessageBox.Show("Password updated Successfully");
                this.Close();
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
