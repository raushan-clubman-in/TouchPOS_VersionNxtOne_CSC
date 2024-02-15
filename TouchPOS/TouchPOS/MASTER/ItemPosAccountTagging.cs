using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.MASTER
{
    public partial class ItemPosAccountTagging : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        SqlConnection Connection = new SqlConnection();

        public ItemPosAccountTagging(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable AName = new DataTable();
        DataTable ChargeName = new DataTable();
        DataTable ChargeName1 = new DataTable();

        private void ItemPosAccountTagging_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillPosSource();
            Cmb_OnSelect.SelectedIndex = 0;
            //FillSubGroupSource();
            FillGrid();
            string queryItem = "Select accode,acdesc+'=>'+accode as acdesc from AccountsGlAccountMaster ";
            AName = GCon.getDataSet(queryItem);
            string queryItem1 = "select CHARGECODE,CHARGEDESC+'=>'+CHARGECODE as CHARGEDESC from chargemaster ";
            ChargeName = GCon.getDataSet(queryItem1);
            string queryItem2 = "select CHARGECODE,CHARGEDESC+'=>'+CHARGECODE as CHARGEDESC from chargemaster ";
            ChargeName1 = GCon.getDataSet(queryItem2);
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }


        private void FillPosSource()
        {
            DataTable dt = new DataTable();
            sql = "Select poscode,posdesc from posmaster Order by posdesc";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_PosLocation.Items.Clear();
                Cmb_PosLocation.Items.Add("ALL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_PosLocation.Items.Add(dt.Rows[i]["posdesc"].ToString());
                }
                Cmb_PosLocation.SelectedIndex = 0;
            }
        }

        private void FillSubGroupSource()
        {
            DataTable dt = new DataTable();
            sql = "Select subgroupCode,subGroupdesc from SubGroupMaster Order by subGroupdesc";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_OnSelect.Items.Clear();
                Cmb_OnSelect.Items.Add("ALL");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_OnSelect.Items.Add(dt.Rows[i]["subGroupdesc"].ToString());
                }
                Cmb_OnSelect.SelectedIndex = 0;
            }
        }

        public void FillGrid()
        {
            DataTable ACSetup = new DataTable();
            sql = "Exec Check_TaxAccountTagging";
            GCon.dataOperation(1, sql);
            //sql = " Select * from AAB Order By posdesc,ItemDesc ";
            if (Cmb_PosLocation.Text == "ALL")
            {
                if (Cmb_OnSelect.Text == "CATEGORY")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "GROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.GroupCode,'') AS ITEMCODE,ISNULL(I.GROUPCODEDEC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "SUBGROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.SUBGROUPCODE,'') AS ITEMCODE,ISNULL(I.SUBGROUPDESC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
            }
            else
            {
                if (Cmb_OnSelect.Text == "CATEGORY")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "GROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.GroupCode,'') AS ITEMCODE,ISNULL(I.GROUPCODEDEC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "SUBGROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.SUBGROUPCODE,'') AS ITEMCODE,ISNULL(I.SUBGROUPDESC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                //sql = " Select * from AAB Where posdesc = '" + Cmb_PosLocation.Text + "' And ItemCode in (Select ItemCode from Itemmaster Where SUBGROUPDESC = '" + Cmb_OnSelect.Text + "') Order By posdesc,ItemDesc ";
            }
            ACSetup = GCon.getDataSet(sql);
            if (ACSetup.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersHeight = 30;
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (Cmb_OnSelect.Text == "CATEGORY") { this.dataGridView1.Columns[3].HeaderText = "Category"; }
                else if (Cmb_OnSelect.Text == "GROUP") { this.dataGridView1.Columns[3].HeaderText = "Group"; }
                else if (Cmb_OnSelect.Text == "SUBGROUP") { this.dataGridView1.Columns[3].HeaderText = "Sub Group"; }

                for (int i = 0; i < ACSetup.Rows.Count; i++)
                {
                    DataRow dr = ACSetup.Rows[i];
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr["Pos"];
                    dataGridView1.Rows[i].Cells[1].Value = dr["posdesc"];
                    dataGridView1.Rows[i].Cells[2].Value = dr["ItemCode"];
                    dataGridView1.Rows[i].Cells[3].Value = dr["ItemDesc"];
                    dataGridView1.Rows[i].Cells[4].Value = dr["TaxOnItem"];
                    dataGridView1.Rows[i].Cells[5].Value = dr["AccountCode"];
                    if (Convert.ToString(dr["AccountCode"]) != "") { dataGridView1.Rows[i].Cells[6].Value = dr["AcountCodeDesc"] + "=>" + dr["AccountCode"]; }
                    else { dataGridView1.Rows[i].Cells[6].Value = ""; }
                    dataGridView1.Rows[i].Cells[7].Value = dr["TaxOnItemSEZ"];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (dataGridView1.CurrentCell.ColumnIndex == 6)
            //{
            //    AutoCompleteStringCollection kode = new AutoCompleteStringCollection();
            //    DataTable AName = new DataTable();
            //    int currentRow = dataGridView1.CurrentRow.Index;
            //    //TODO fill Customer list
            //    string queryItem = "Select accode,acdesc from AccountsGlAccountMaster WHERE acdesc LIKE '%" + dataGridView1.Rows[currentRow].Cells[6].Value.ToString() + "%'";
            //    AName = GCon.getDataSet(queryItem);
            //    if (AName.Rows.Count > 0)
            //    {
            //        for (int j = 0; j < AName.Rows.Count - 1; j++)
            //        { 
            //            kode.Add(AName.Rows[j].ItemArray[0].ToString()); 
            //        }
            //    }
            //    else { MessageBox.Show("Data not Found"); }
            //    TextBox kodeTxt = e.Control as TextBox;
            //    if (kodeTxt != null)
            //    {
            //        kodeTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //        kodeTxt.AutoCompleteCustomSource = kode;
            //        kodeTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    }
            //}

            if (dataGridView1.EditingControl.GetType() == typeof(DataGridViewTextBoxEditingControl))
            {
                int currentRow = dataGridView1.CurrentRow.Index;
                TextBox prodCode = e.Control as TextBox;
                TextBox prodCode1 = e.Control as TextBox;
                TextBox prodCode2 = e.Control as TextBox;
                if (dataGridView1.CurrentCell.ColumnIndex == 6)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<DataRow, String>(AName.Select(), delegate(DataRow row) { return (String)row["acdesc"]; });
                    source.AddRange(stringArray);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                //else { prodCode.AutoCompleteCustomSource = null; }
                else if (dataGridView1.CurrentCell.ColumnIndex == 4)
                {
                    var source1 = new AutoCompleteStringCollection();
                    String[] stringArray1 = Array.ConvertAll<DataRow, String>(ChargeName.Select(), delegate(DataRow row) { return (String)row["CHARGEDESC"]; });
                    source1.AddRange(stringArray1);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode1 != null)
                    {
                        prodCode1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode1.AutoCompleteCustomSource = source1;
                        prodCode1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 7)
                {
                    var source2 = new AutoCompleteStringCollection();
                    String[] stringArray2 = Array.ConvertAll<DataRow, String>(ChargeName1.Select(), delegate(DataRow row) { return (String)row["CHARGEDESC"]; });
                    source2.AddRange(stringArray2);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode2 != null)
                    {
                        prodCode2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode2.AutoCompleteCustomSource = source2;
                        prodCode2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                //else { prodCode1.AutoCompleteCustomSource = null; }
            }
        }

        private void Cmd_Search_Click(object sender, EventArgs e)
        {
            DataTable ACSetup = new DataTable();
            sql = "Exec Check_TaxAccountTagging";
            GCon.dataOperation(1, sql);

            if (Cmb_PosLocation.Text == "ALL") 
            {
                if (Cmb_OnSelect.Text == "CATEGORY") 
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "GROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.GroupCode,'') AS ITEMCODE,ISNULL(I.GROUPCODEDEC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "SUBGROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.SUBGROUPCODE,'') AS ITEMCODE,ISNULL(I.SUBGROUPDESC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
                else 
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode ORDER BY 2,4 ";
                }
            }
            else 
            {
                if (Cmb_OnSelect.Text == "CATEGORY")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "GROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.GroupCode,'') AS ITEMCODE,ISNULL(I.GROUPCODEDEC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else if (Cmb_OnSelect.Text == "SUBGROUP")
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.SUBGROUPCODE,'') AS ITEMCODE,ISNULL(I.SUBGROUPDESC,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                else 
                {
                    sql = " SELECT DISTINCT Pos,posdesc,ISNULL(I.CATEGORY,'') AS ITEMCODE,ISNULL(I.CATEGORY,'') AS ITEMDESC,TaxOnItem,AccountCode,AcountCodeDesc,TaxOnItemSEZ FROM AAB A,ITEMMASTER I WHERE A.ItemCode = I.ItemCode And posdesc = '" + Cmb_PosLocation.Text + "' ORDER BY 2,4 ";
                }
                //sql = " Select * from AAB Where posdesc = '" + Cmb_PosLocation.Text + "' And ItemCode in (Select ItemCode from Itemmaster Where SUBGROUPDESC = '" + Cmb_OnSelect.Text + "') Order By posdesc,ItemDesc ";
            }
            ACSetup = GCon.getDataSet(sql);
            dataGridView1.Rows.Clear();
            if (ACSetup.Rows.Count > 0)
            {
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (Cmb_OnSelect.Text == "CATEGORY") { this.dataGridView1.Columns[3].HeaderText = "Category"; }
                else if (Cmb_OnSelect.Text == "GROUP") { this.dataGridView1.Columns[3].HeaderText = "Group"; }
                else if (Cmb_OnSelect.Text == "SUBGROUP") { this.dataGridView1.Columns[3].HeaderText = "Sub Group"; }

                for (int i = 0; i < ACSetup.Rows.Count; i++)
                {
                    DataRow dr = ACSetup.Rows[i];
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr["Pos"];
                    dataGridView1.Rows[i].Cells[1].Value = dr["posdesc"];
                    dataGridView1.Rows[i].Cells[2].Value = dr["ItemCode"];
                    dataGridView1.Rows[i].Cells[3].Value = dr["ItemDesc"];
                    dataGridView1.Rows[i].Cells[4].Value = dr["TaxOnItem"];
                    dataGridView1.Rows[i].Cells[5].Value = dr["AccountCode"];
                    if (Convert.ToString(dr["AccountCode"]) != "") { dataGridView1.Rows[i].Cells[6].Value = dr["AcountCodeDesc"] + "=>" + dr["AccountCode"]; }
                    else { dataGridView1.Rows[i].Cells[6].Value = ""; }
                    dataGridView1.Rows[i].Cells[7].Value = dr["TaxOnItemSEZ"];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FillGrid();
            string queryItem = "Select accode,acdesc+'=>'+accode as acdesc from AccountsGlAccountMaster ";
            AName = GCon.getDataSet(queryItem);
            string queryItem1 = "select CHARGECODE,CHARGEDESC+'=>'+CHARGECODE as CHARGEDESC from chargemaster ";
            ChargeName = GCon.getDataSet(queryItem1);
            FillPosSource();
            //FillSubGroupSource();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string PosCode = "", Itemcode = "", TaxACode = "", ACCode = "",Scode = "",TaxACodeSez = "";
            string[] SplitCode = { "", "" };
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                 { PosCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                 else { PosCode = ""; }
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                 { Itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                 else { Itemcode = ""; }
                if (dataGridView1.Rows[i].Cells[4].Value != null)
                 { Scode = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value); }
                 else { Scode = ""; }
                if (Scode != "")
                {
                    SplitCode = Scode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    TaxACode = SplitCode[1];
                }
                else { TaxACode = ""; }
                if (dataGridView1.Rows[i].Cells[6].Value != null)
                { Scode = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value); }
                else { Scode = ""; }
                if (Scode != "")
                {
                    SplitCode = Scode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    ACCode = SplitCode[1];
                }
                else { ACCode = ""; }

                if (dataGridView1.Rows[i].Cells[7].Value != null)
                { Scode = Convert.ToString(dataGridView1.Rows[i].Cells[7].Value); }
                else { Scode = ""; }
                if (Scode != "")
                {
                    SplitCode = Scode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    TaxACodeSez = SplitCode[1];
                }
                else { TaxACodeSez = ""; }

                if (PosCode != "" && Itemcode != "" ) 
                {
                    if (Cmb_OnSelect.Text == "CATEGORY") 
                    {
                        sqlstring = "Update POSMENULINK Set TaxOnItem = '" + TaxACode + "',AcountCode = '" + ACCode + "',TaxOnItemSEZ = '" + TaxACodeSez + "' Where POS = '" + PosCode + "' And ItemCode IN (select ItemCode From ItemMaster Where CATEGORY = '" + Itemcode + "') ";
                        List.Add(sqlstring);
                    }
                    else if (Cmb_OnSelect.Text == "GROUP") 
                    {
                        sqlstring = "Update POSMENULINK Set TaxOnItem = '" + TaxACode + "',AcountCode = '" + ACCode + "',TaxOnItemSEZ = '" + TaxACodeSez + "' Where POS = '" + PosCode + "' And ItemCode IN (select ItemCode From ItemMaster Where GroupCode = '" + Itemcode + "') ";
                        List.Add(sqlstring);
                    }
                    else if (Cmb_OnSelect.Text == "SUBGROUP") 
                    {
                        sqlstring = "Update POSMENULINK Set TaxOnItem = '" + TaxACode + "',AcountCode = '" + ACCode + "',TaxOnItemSEZ = '" + TaxACodeSez + "' Where POS = '" + PosCode + "' And ItemCode IN (select ItemCode From ItemMaster Where SUBGROUPCODE = '" + Itemcode + "') ";
                        List.Add(sqlstring);
                    }
                }
            }
            sqlstring = "UPDATE POSMENULINK SET AcountCodeDesc = A.ACDESC  FROM accountsglaccountmaster A,POSMENULINK P WHERE A.ACCODE = P.ACOUNTCODE ";
            List.Add(sqlstring);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                Cmd_Search_Click(sender, e);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable ShowData = new DataTable();
            int index =  dataGridView1.CurrentRow.Index;
            if (Cmb_OnSelect.Text == "CATEGORY") 
            {
                sql = "SELECT posdesc,ISNULL(I.CATEGORY,'') as CATEGORY,A.ItemCode,A.ItemDesc,TaxOnItem,AccountCode = CASE WHEN AccountCode <> '' THEN AcountCodeDesc+'=>'+AccountCode ELSE '' END,TaxOnItemSEZ FROM AAB A,ITEMMASTER I ";
                sql = sql + " WHERE A.ItemCode = I.ItemCode And posdesc = '" + dataGridView1.Rows[index].Cells[1].Value.ToString() + "' AND ISNULL(CATEGORY,'') = '" + dataGridView1.Rows[index].Cells[2].Value.ToString() + "' ORDER BY 1,2,4 ";
            }
            else if (Cmb_OnSelect.Text == "GROUP") 
            {
                sql = "SELECT posdesc,ISNULL(I.GroupCode,'') as GroupCode,ISNULL(I.GROUPCODEDEC,'') as GroupCodeDesc,A.ItemCode,A.ItemDesc,TaxOnItem,AccountCode = CASE WHEN AccountCode <> '' THEN AcountCodeDesc+'=>'+AccountCode ELSE '' END,TaxOnItemSEZ FROM AAB A,ITEMMASTER I ";
                sql = sql + " WHERE A.ItemCode = I.ItemCode And posdesc = '" + dataGridView1.Rows[index].Cells[1].Value.ToString() + "' AND  ISNULL(I.GroupCode,'') = '" + dataGridView1.Rows[index].Cells[2].Value.ToString() + "' ORDER BY 1,3,5 ";
            }
            else if (Cmb_OnSelect.Text == "SUBGROUP")
            {
                sql = "SELECT posdesc,ISNULL(I.SUBGROUPCODE,'') as SUBGROUPCODE,ISNULL(I.SUBGROUPDESC,'') as SUBGROUPDESC,A.ItemCode,A.ItemDesc,TaxOnItem,AccountCode = CASE WHEN AccountCode <> '' THEN AcountCodeDesc+'=>'+AccountCode ELSE '' END,TaxOnItemSEZ FROM AAB A,ITEMMASTER I ";
                sql = sql + " WHERE A.ItemCode = I.ItemCode And posdesc = '" + dataGridView1.Rows[index].Cells[1].Value.ToString() + "' AND  ISNULL(I.SUBGROUPCODE,'') = '" + dataGridView1.Rows[index].Cells[2].Value.ToString() + "' ORDER BY 1,3,5 ";
            }
            ShowData = GCon.getDataSet(sql);
            if (ShowData.Rows.Count > 0) 
            {
                ItemPosShowDetails IPS = new ItemPosShowDetails(this);
                IPS.FillData = ShowData;
                IPS.ShowDialog();
            }
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
