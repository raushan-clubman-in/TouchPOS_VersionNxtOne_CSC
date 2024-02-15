using Microsoft.VisualBasic;
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
    public partial class AddPaxForm : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int SPax = 0;
        public string KotOrder= "";
        public readonly EntryForm _form1;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public AddPaxForm(EntryForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void AddPaxForm_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            TxtPax.Text = "";
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }


        private void Button_0_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_0.Text;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_2.Text;
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_3.Text;
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_4.Text;
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_5.Text;
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_6.Text;
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_7.Text;
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_8.Text;
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            TxtPax.Text = TxtPax.Text + Button_9.Text;
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtPax.Text != "")
            {
                TxtPax.Text = TxtPax.Text.Remove(TxtPax.Text.Length - 1, 1);
            }
        }

        private void TxtPax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPax_TextChanged(object sender, EventArgs e)
        {
            if (TxtPax.Text.Length > 3)
            {
                TxtPax.Text = TxtPax.Text.Remove(TxtPax.Text.Length - 1, 1);
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            int Pax = 0;
            if (KotOrder != "") 
            {
                Pax = Convert.ToInt16(GCon.getValue("Select Top 1 Isnull(Covers,0) as Covers from KOT_HDR Where Kotdetails = '" + KotOrder + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                Pax = Pax + Convert.ToInt16(TxtPax.Text = string.IsNullOrEmpty(TxtPax.Text) ? "0" : TxtPax.Text);
                List.Clear();
                sql = "Update kot_hdr set COVERS = " + Pax + " Where Kotdetails = '" + KotOrder + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                List.Add(sql);
                sql = "Update kot_det set COVERS = " + Pax + " Where Kotdetails = '" + KotOrder + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                List.Add(sql);
                if (GCon.Moretransaction(List) > 0) 
                {
                    MessageBox.Show("Pax Updated Successfully");
                    List.Clear(); 
                }
            }
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
