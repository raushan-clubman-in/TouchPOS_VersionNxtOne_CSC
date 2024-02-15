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
    public partial class Support : Form
    {
        public readonly ServiceType _form1;

        public Support(ServiceType form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        private void Support_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.gCompName == "CSC") 
            {
                string add = "";
                add = add + "Phone : - +91 44 42070027, 9380732461 (Sandeep Mohanty) " + Environment.NewLine + "";
                add = add + "Email : - info@clubman.in" + Environment.NewLine + "";
                label2.Text = add;
            }
            else 
            {
                string add = "No.123, 3rd Floor,Goudyamutt Road" + Environment.NewLine + "Royapettah,Chennai-600014" + Environment.NewLine + "";
                add = add + "Phone : - +91 44 42070027, 9380732461 (Sandeep Mohanty) " + Environment.NewLine + "";
                add = add + "Email : - info@clubman.in" + Environment.NewLine + "";
                add = add + "WebSite : - http://www.clubman.in" + Environment.NewLine + "";
                label2.Text = add;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
