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
    public partial class Board : Form
    {
        public Form _form1;
        TextBox TB;
        public Board(Form form1,TextBox TB1)
        {
            _form1 = form1;
            TB = TB1;
            InitializeComponent();
        }

        private void Board_Load(object sender, EventArgs e)
        {
            
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_0.Text;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_9.Text;
        }

        private void Button_Q_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_Q.Text;
        }

        private void Button_W_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_W.Text;
        }

        private void Button_E_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_E.Text;
        }

        private void Button_R_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_R.Text;
        }

        private void Button_T_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_T.Text;
        }

        private void Button_Y_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_Y.Text;
        }

        private void Button_U_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_U.Text;
        }

        private void Button_I_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_I.Text;
        }

        private void Button_O_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_O.Text;
        }

        private void Button_P_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_P.Text;
        }

        private void Button_A_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_A.Text;
        }

        private void Button_S_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_S.Text;
        }

        private void Button_D_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_D.Text;
        }

        private void Button_F_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_F.Text;
        }

        private void Button_G_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_G.Text;
        }

        private void Button_H_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_H.Text;
        }

        private void Button_J_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_J.Text;
        }

        private void Button_K_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_K.Text;
        }

        private void Button_L_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_L.Text;
        }

        private void Button_Z_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_Z.Text;
        }

        private void Button_X_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_X.Text;
        }

        private void Button_C_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_C.Text;
        }

        private void Button_V_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_V.Text;
        }

        private void Button_B_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_B.Text;
        }

        private void Button_N_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_N.Text;
            //KeyPress += KeyPressHandler;
            
        }

        private void Button_M_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_M.Text;
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_3.Text;
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_4.Text;
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_5.Text;
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_6.Text;
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_7.Text;
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            TB.Text = TB.Text + Button_8.Text;
        }


    }
}
