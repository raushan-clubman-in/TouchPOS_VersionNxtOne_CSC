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
    public partial class SearchGrid : Form
    {
        GlobalClass GCon = new GlobalClass();
        public Form _form1;
        DataGridView DB1;
        string Search = "", SearchField = "";
        public int GridCol = 0;

        public SearchGrid(Form form1,DataGridView DB,string Search1,string SearchField1)
        {
            _form1 = form1;
            DB1 = DB;
            Search = Search1;
            SearchField = SearchField1;
            InitializeComponent();
        }

        string StrSql = "";

        private void SearchGrid_Load(object sender, EventArgs e)
        {
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            DataTable FillData = new DataTable();
            string[] SearchFieldLat = SearchField.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            StrSql = Search;
            if (SearchFieldLat.Length > 0) 
            {
                StrSql = StrSql + " Where ";
                for (int i = 0; i < SearchFieldLat.Length; i++) 
                {
                    StrSql = StrSql + SearchFieldLat[i] + " Like '%" + Txt_SearchBox.Text + "%' or ";
                }
                StrSql = StrSql.Substring(0 , StrSql.Length - 4);
            }
            FillData = GCon.getDataSet(StrSql);
            if (FillData.Rows.Count > 0) 
            {
                DB1.Rows.Clear();
                for (int i = 0; i < FillData.Rows.Count; i++)
                {
                    DB1.Rows.Add();
                    if (GridCol == 2)
                    {
                        DB1.Rows[i].Cells[0].Value = FillData.Rows[i].ItemArray[0];
                        DB1.Rows[i].Cells[1].Value = FillData.Rows[i].ItemArray[1];
                    }
                    if (GridCol == 3)
                    {
                        DB1.Rows[i].Cells[0].Value = FillData.Rows[i].ItemArray[0];
                        DB1.Rows[i].Cells[1].Value = FillData.Rows[i].ItemArray[1];
                        DB1.Rows[i].Cells[2].Value = FillData.Rows[i].ItemArray[2];
                    }
                    if (GridCol == 4) 
                    {
                        DB1.Rows[i].Cells[0].Value = FillData.Rows[i].ItemArray[0];
                        DB1.Rows[i].Cells[1].Value = FillData.Rows[i].ItemArray[1];
                        DB1.Rows[i].Cells[2].Value = FillData.Rows[i].ItemArray[2];
                        DB1.Rows[i].Cells[3].Value = FillData.Rows[i].ItemArray[3];
                    }
                    if (GridCol == 5)
                    {
                        DB1.Rows[i].Cells[0].Value = FillData.Rows[i].ItemArray[0];
                        DB1.Rows[i].Cells[1].Value = FillData.Rows[i].ItemArray[1];
                        DB1.Rows[i].Cells[2].Value = FillData.Rows[i].ItemArray[2];
                        DB1.Rows[i].Cells[3].Value = FillData.Rows[i].ItemArray[3];
                        DB1.Rows[i].Cells[4].Value = FillData.Rows[i].ItemArray[4];
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
