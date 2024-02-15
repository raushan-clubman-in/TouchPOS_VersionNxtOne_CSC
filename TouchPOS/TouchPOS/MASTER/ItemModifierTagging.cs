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

namespace TouchPOS.MASTER
{
    public partial class ItemModifierTagging : Form
    {
        public readonly MastersForm _form1;
        GlobalClass GCon = new GlobalClass();

        public ItemModifierTagging(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable dtPosts = new DataTable();
        DataTable dtPostsitem = new DataTable();
        DataTable AName = new DataTable();

        private void ItemModifierTagging_Load(object sender, EventArgs e)
        {
            AutoComplete();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            string queryItem = "Select MID,MText+'=>'+MID as MText from Tbl_Modifier Where Isnull(VOID,'') <> 'Y' ";
            AName = GCon.getDataSet(queryItem);
        }

        private void AutoComplete()
        {
            this.Txt_Item.DataBindings.Clear();
            //sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
            sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And Isnull(ModifierType,'') in ('Fixed','Both') ";
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            Txt_Item.AutoCompleteCustomSource = source;
            Txt_Item.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            this.Txt_Item.DataBindings.Add("Text", dtPosts, "ItemDesc");
            //dtView = new DataView(dtPosts);
            this.Txt_Item.Text = "";
        }

        private void AutoCompleteItem()
        {
            this.Txt_Item.DataBindings.Clear();
            sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And Isnull(ModifierType,'') in ('Fixed','Both')  ";
            dtPostsitem = GCon.getDataSet(sql);
            string[] postSource2 = dtPostsitem
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("Itemcode"))
                    .ToArray();
            var source2 = new AutoCompleteStringCollection();
            source2.AddRange(postSource2);
            Txt_Item.AutoCompleteCustomSource = source2;
            Txt_Item.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            this.Txt_Item.DataBindings.Add("Text", dtPostsitem, "Itemcode");
            //dtView = new DataView(dtPosts);
            this.Txt_Item.Text = "";
        }

        private void Chk_SearchByCode_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SearchByCode.Checked == true)
            {
                AutoCompleteItem();
            }
            else
            {
                AutoComplete();
            }
        }

        private void Txt_Item_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable MDetail = new DataTable();
            if (e.KeyCode == Keys.Enter) 
            {
                DataTable dt = new DataTable();
                string icode;
                int RowCnt;
                if (Chk_SearchByCode.Checked == true)
                {
                    sql = "SELECT ItemCode,ItemDesc FROM ITEMMASTER  Where ItemCode = '" + Txt_Item.Text.Replace("'", "''") + "' And ISNULL(FREEZE,'') <> 'Y' ";
                }
                else
                {
                    sql = "SELECT ItemCode,ItemDesc FROM ITEMMASTER  Where ItemDesc = '" + Txt_Item.Text.Replace("'", "''") + "' And ISNULL(FREEZE,'') <> 'Y' ";
                }
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0) 
                {
                    DataRow dr = dt.Rows[0];
                    Lbl_ItemCode.Text = dr["Itemcode"].ToString();
                    Lbl_ItemName.Text = dr["ItemDesc"].ToString();
                    Txt_Item.Text = "";
                    sql = "SELECT M.ITEMCODE,M.MID,T.MTEXT,T.MText+'=>'+M.MID as MText1 FROM ItemModifierTag M,Tbl_Modifier T Where M.MID = T.MID AND M.ITEMCODE = '" + (Lbl_ItemCode.Text) + "' Order by M.AutoId ";
                    MDetail = GCon.getDataSet(sql);
                    if (MDetail.Rows.Count > 0) 
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                        this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        for (int i = 0; i < MDetail.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = MDetail.Rows[i].ItemArray[3];
                            dataGridView1.Rows[i].Height = 50;
                        }
                    }
                }
            }
        }

        private void Lbl_ItemName_Click(object sender, EventArgs e)
        {

        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_AddRow_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            if (dataGridView1.Rows.Count > 0) 
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Height = 50;
                }
            }
            
        }

        private void Cmd_DeleteRow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0) 
            {
                int index = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.RemoveAt(index);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Txt_Item.Text = "";
            Chk_SearchByCode.Checked = false;
            Lbl_ItemName.Text = "";
            Lbl_ItemCode.Text = "";
            dataGridView1.Rows.Clear();
            string queryItem = "Select MID,MText+'=>'+MID as MText from Tbl_Modifier Where Isnull(VOID,'') <> 'Y' ";
            AName = GCon.getDataSet(queryItem);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.EditingControl.GetType() == typeof(DataGridViewTextBoxEditingControl)) 
            {
                int currentRow = dataGridView1.CurrentRow.Index;
                TextBox prodCode = e.Control as TextBox;
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<DataRow, String>(AName.Select(), delegate(DataRow row) { return (String)row["MText"]; });
                    source.AddRange(stringArray);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string MIDCode = "",CheckCode;
            string[] SplitCode = { "", "" };
            if (dataGridView1.RowCount > 0 ) 
            {
                sqlstring = "Delete From ItemModifierTag Where ITEMCODE = '" + Lbl_ItemCode.Text + "' ";
                List.Add(sqlstring);
            }
            for (int i = 0; i < dataGridView1.RowCount; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { CheckCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                else { CheckCode = ""; }
                if (CheckCode != "")
                {
                    SplitCode = CheckCode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    MIDCode = SplitCode[1];
                }
                else { MIDCode = ""; }
                if (Lbl_ItemCode.Text != "" && MIDCode != "") 
                {
                    sqlstring = "Insert Into ItemModifierTag (ITEMCODE,MID) VALUES (";
                    sqlstring = sqlstring + " '" + (Lbl_ItemCode.Text) + "','" + MIDCode + "')";
                    List.Add(sqlstring);
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
        }
    }
}
