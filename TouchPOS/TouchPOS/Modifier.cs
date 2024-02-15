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
    public partial class Modifier : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public readonly EntryForm _form1;
        public DataGridView DG1;

        public Modifier(EntryForm form1, DataGridView DG)
        {
            _form1 = form1;
            DG1 = DG;
            InitializeComponent();
        }
        string sql = "";
        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            string Text;
            double Charges = 0;
            if (Txt_Modifier.Text != "")
            {
                Text = Txt_Modifier.Text;
            }
            else    
            {
                int rowindex = dataGridView1.CurrentRow.Index;
                Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            }
            //Charges = Convert.ToDouble(Txt_ModiCharges.Text);
            Charges = Convert.ToDouble(Txt_ModiCharges.Text = string.IsNullOrEmpty(Txt_ModiCharges.Text) ? "0.00" : Txt_ModiCharges.Text);
            _form1.FillModifier(Text, Rowno, Charges);
            this.Hide();
        }

        private void Modifier_Load(object sender, EventArgs e)
        {
            ////int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            ////int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            ////Utility.relocate(this, 1368, 768);
            ////Utility.repositionForm(this, screenWidth, screenHeight);
            string MItemCode = "";
            DataTable MTable = new DataTable();
            DataTable dt = new DataTable();

            dataGridView1.ReadOnly = true;

            MItemCode = Convert.ToString(DG1.Rows[Rowno].Cells[0].Value);
            sql = "SELECT ISNULL(ModifierType,'None') as ModifierType FROM itemmaster Where ItemCode = '" + MItemCode + "'";
            MTable = GCon.getDataSet(sql);
            if (MTable.Rows.Count > 0) 
            {
                DataRow dr = MTable.Rows[0];
                if (dr["ModifierType"].ToString() == "Fixed" || dr["ModifierType"].ToString() == "Both")
                {
                    sql = "SELECT T.MTEXT FROM ItemModifierTag M,Tbl_Modifier T Where M.MID = T.MID AND M.ITEMCODE = '" + (MItemCode) + "' Order by M.AutoId ";
                }
                else
                { sql = "SELECT T.MTEXT FROM ItemModifierTag M,Tbl_Modifier T Where M.MID = T.MID AND M.ITEMCODE = '" + (MItemCode) + "' Order by M.AutoId "; }
                //sql = "select MText as FixedModifier from Tbl_Modifier Order by AutoId ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dr["ModifierType"].ToString() == "Open" ) 
                {
                    dataGridView1.Rows.Clear();
                    Txt_Modifier.Enabled = true;
                }
                else if (dr["ModifierType"].ToString() == "Both") 
                {
                    Txt_Modifier.Enabled = true;
                }
                else if (dr["ModifierType"].ToString() == "Fixed")
                {
                    Txt_Modifier.Enabled = false;
                }
                else { Txt_Modifier.Enabled = false; }

                Txt_Modifier.Text = Convert.ToString(DG1.Rows[Rowno].Cells[7].Value);
                if (DG1.Rows[Rowno].Cells[17].Value != null)
                {
                    Txt_ModiCharges.Text = DG1.Rows[Rowno].Cells[17].Value.ToString();
                }
            }
            
        }

        private void Txt_ModiCharges_KeyPress(object sender, KeyPressEventArgs e)
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

    }
}
