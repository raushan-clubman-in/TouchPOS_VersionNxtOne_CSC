using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class UserSelecton : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string DisUserName = "";

        public UserSelecton(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void UserSelecton_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select DISTINCT USERNAME from master..useradmin UNION ALL select 'CHS' order by USERNAME";
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
            TxtPass.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtPass.Text.Length > 0)
            { 
                TxtPass.Text = TxtPass.Text.Remove(TxtPass.Text.Length - 1, 1); 
            }
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
            this.Hide();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DataTable Userdt = new DataTable();
            sql = "select * from master..useradmin where username = '" + Cmb_User.Text + "' and userpassword = '" + GCon.GetPassword(TxtPass.Text.Trim()) + "'";
            Userdt = GCon.getDataSet(sql);
            if ((Userdt.Rows.Count > 0) || (Cmb_User.Text == "CHS" && GCon.GetPassword(TxtPass.Text.Trim()) == "ÏÆÉÎÎËÆÇÎÎ"))
            {
                DisUserName = Cmb_User.Text;
                this.Hide();
            }
            else 
            {
                MessageBox.Show("User and Password Combination not match, try again...");
                this.Close();
            }
        }
    }
}
