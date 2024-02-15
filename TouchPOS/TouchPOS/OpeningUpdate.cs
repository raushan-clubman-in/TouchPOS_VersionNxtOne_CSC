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
    public partial class OpeningUpdate : Form
    {
        public readonly ServiceType _form1;
        GlobalClass GCon = new GlobalClass();

        public OpeningUpdate(ServiceType form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        private void OpeningUpdate_Load(object sender, EventArgs e)
        {
            Dtp_Date.Value = GlobalVariable.ServerDate;
        }

        private void Txt_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";

            if (String.IsNullOrEmpty(Txt_Amount.Text))
            {
                return;
            }
            if (Convert.ToDouble(Txt_Amount.Text) < 0) 
            {
                return;
            }
            sqlstring = "Insert Into CashOpeningBal(OpenDate,OpenBal,Adduser,AddDate) Values ('" + Dtp_Date.Value.ToString("dd-MMM-yyyy") + "'," + Txt_Amount.Text + ",'" + GlobalVariable.gUserName + "',getdate())";
            List.Add(sqlstring);
            if (GCon.Moretransaction(List) > 0) 
            {
                List.Clear();
                this.Hide();
            }
        }
    }
}
