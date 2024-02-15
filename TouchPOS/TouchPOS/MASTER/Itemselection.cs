using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.MASTER
{
    public partial class Itemselection : Form
    {
        GlobalClass GCon = new GlobalClass();
        public int Rowno = 0;
        public readonly Itemmaster _form1;
        string sql;

        public Itemselection(Itemmaster form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Itemselection_Load(object sender, EventArgs e)
        {
            fillGRID();

            comboBox1.SelectedIndex = 0;
        }


        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " SELECT ITEMCODE,ItemDesc,SHORTNAME FROM ITEMMASTER  ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];

                }
            }
        }

        public void fillGRIDITEM()
        {

            DataTable PosCate = new DataTable();
            if (comboBox1.Text == "ITEMCODE")
            {
                sql = " SELECT ITEMCODE,ItemDesc,SHORTNAME FROM ITEMMASTER WHERE ITEMCODE LIKE '%" + Txt_Modifier.Text + "%'  ";
            }
            else if (comboBox1.Text == "ITEMDESC")
            {
                sql = " SELECT ITEMCODE,ItemDesc,SHORTNAME FROM ITEMMASTER WHERE ItemDesc LIKE '%" + Txt_Modifier.Text + "%'  ";

            }
            else
            {
                sql = " SELECT ITEMCODE,ItemDesc,SHORTNAME FROM ITEMMASTER";
            }
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];

                }
            }
        }

        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            
                int rowindex = dataGridView1.CurrentRow.Index;
                Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            


            _form1.Fillitemcode(Text, Rowno);
            this.Hide();
        }

        private void Txt_Modifier_TextChanged(object sender, EventArgs e)
      {
            fillGRIDITEM();
        }


    }
}