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
    public partial class DiscBasisSelection : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string BasisType = "";
        public string DCategory = "";
        public string DiscType = "";
        public double GlobalDiscPerc = 0;
        public double GBillValue = 0;

        public DiscBasisSelection(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void DiscBasisSelection_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "Select Name From DiscountTypeMaster Order by 1 ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_DiscCategory.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_DiscCategory.Items.Add(dt.Rows[i]["Name"].ToString());
                }
                Cmb_DiscCategory.SelectedIndex = 0;
            }
            Txt_DiscPerc.Text = Convert.ToString(Txt_DiscPerc.Text = string.IsNullOrEmpty(Txt_DiscPerc.Text) ? "0.00" : Txt_DiscPerc.Text);
            if (DiscType == "OPEN PERCENTAGE")
            {
                Txt_DiscPerc.Enabled = true;
                Txt_Amount.Enabled = false;
                Lbl_BillValue.Text = "";
            }
            else if (DiscType == "OPEN AMOUNT")
            {
                Txt_DiscPerc.Enabled = false;
                Txt_Amount.Enabled = true;
                Rdb_ItemGroup.Enabled = false;
                groupBox1.Enabled = false;
                Lbl_BillValue.Text = "On " + GBillValue;
            }
            else 
            {
                Txt_DiscPerc.Enabled = false;
                Txt_Amount.Enabled = false;
                Txt_DiscPerc.Text = GlobalDiscPerc.ToString();
                Lbl_BillValue.Text = "";
            }
        }


        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            BasisType = "B";
            GlobalDiscPerc = 0;
            this.Hide();
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            if (Rdb_Bill.Checked == true) 
            {
                BasisType = "B";
                DCategory = Cmb_DiscCategory.Text;
                GlobalDiscPerc = Convert.ToDouble(Txt_DiscPerc.Text = string.IsNullOrEmpty(Txt_DiscPerc.Text) ? "0.00" : Txt_DiscPerc.Text);
            }
            else if (Rdb_ItemGroup.Checked == true) 
            {
                BasisType = "I";
                DCategory = Cmb_DiscCategory.Text;
                GlobalDiscPerc = 0;
            }
            this.Hide();
        }

        private void Txt_DiscPerc_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Txt_DiscPerc_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(Txt_DiscPerc.Text, out value))
            {
                if (value > 100)
                    Txt_DiscPerc.Text = "100";
                else if (value < 0)
                    Txt_DiscPerc.Text = "0";
            }
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

        private void Txt_Amount_TextChanged(object sender, EventArgs e)
        {
            int value;
            double val1;
            if (int.TryParse(Txt_Amount.Text, out value))
            {
                if (value > GBillValue)
                    Txt_Amount.Text = GBillValue.ToString();
                else if (value < 0)
                    Txt_Amount.Text = "0";
            }
            val1 = (Convert.ToDouble(Txt_Amount.Text = string.IsNullOrEmpty(Txt_Amount.Text) ? "0.00" : Txt_Amount.Text) / GBillValue) * 100;
            val1 = Math.Round(val1, 2);
            Txt_DiscPerc.Text = val1.ToString();
        }
    }
}
