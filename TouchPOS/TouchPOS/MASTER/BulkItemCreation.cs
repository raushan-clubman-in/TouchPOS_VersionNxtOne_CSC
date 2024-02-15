using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Data.OleDb;
using System.Reflection;

namespace TouchPOS.MASTER
{
    public partial class BulkItemCreation : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        SqlConnection Connection = new SqlConnection();

        public BulkItemCreation(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        private void BulkItemCreation_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.fitFormToScreen(this, screenHeight, screenWidth);
            this.CenterToScreen();
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            label2.Text = "";
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofImport = new OpenFileDialog();
            ofImport.Title = "Select file";
            ofImport.InitialDirectory = Application.StartupPath;
            ofImport.FileName = textBox1.Text;
            ofImport.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            ofImport.FilterIndex = 1;
            ofImport.RestoreDirectory = true;

            if (ofImport.ShowDialog() == DialogResult.OK)
            {

                string path = System.IO.Path.GetFullPath(ofImport.FileName);
                //string query = "SELECT * FROM Customer.xlsx";
                string query = "SELECT * FROM [Sheet1$]";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ofImport.FileName + ";Extended Properties=" + "\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);

                DataSet dsSource = new System.Data.DataSet();
                adapter.Fill(dsSource);
                dataGridView1.DataSource = dsSource.Tables[0];
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.ReadOnly = true;
            }
            else
            {
                ofImport.Dispose();
            }   
        }

        private void Btn_exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Import_Click(object sender, System.EventArgs e)
        {
            try 
            {
                ArrayList List = new ArrayList();
                string sqlstring = "";
                string ItemName = "", UOM = "", SubGroup = "", ItemType = "", POS = "",KitCode = "";
                Double ItemRate = 0,CostRate = 0;
                int i = 0;

                label2.Text = "";
                if (dataGridView1.RowCount == 0)
                { MessageBox.Show("Sorry, Import data from excel "); return; }

                DataTable ChkDt = new DataTable();
                sqlstring = "SELECT * FROM SYSOBJECTS WHERE name = 'BulkItem'";
                ChkDt = GCon.getDataSet(sqlstring);
                if (ChkDt.Rows.Count == 0)
                {
                    sqlstring = "Create Table BulkItem (ItemName Varchar(100),UOM varchar(20),SubGroup Varchar(20),ItemType varchar(20),ItemRate Numeric(18,2),CostRate Numeric(18,2),KitCode varchar(20)) ";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0) { List.Clear(); }
                }
                else 
                {
                    sqlstring = "Drop Table BulkItem ";
                    List.Add(sqlstring);
                    sqlstring = "Create Table BulkItem (ItemName Varchar(100),UOM varchar(20),SubGroup Varchar(20),ItemType varchar(20),ItemRate Numeric(18,2),CostRate Numeric(18,2),KitCode varchar(20)) ";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0) { List.Clear(); }
                }
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    { ItemName = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                    else { ItemName = ""; }
                    if (dataGridView1.Rows[i].Cells[1].Value != null)
                    { UOM = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value); }
                    else { UOM = ""; }
                    if (dataGridView1.Rows[i].Cells[2].Value != null)
                    { SubGroup = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                    else { SubGroup = ""; }
                    if (dataGridView1.Rows[i].Cells[3].Value != null)
                    { ItemType = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value); }
                    else { ItemType = ""; }
                    if (dataGridView1.Rows[i].Cells[4].Value != null)
                    { ItemRate = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value); }
                    else { ItemRate = 0; }
                    //if (dataGridView1.Rows[i].Cells[5].Value != null)
                    //{ POS = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value); }
                    //else { POS = ""; }
                    if (dataGridView1.Rows[i].Cells[5].Value != null)
                    { CostRate = Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value); }
                    else { CostRate = 0; }
                    if (dataGridView1.Rows[i].Cells[6].Value != null)
                    { KitCode = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value); }
                    else { KitCode = ""; }

                    if (ItemName != "")
                    {
                        sqlstring = "Insert Into BulkItem (ItemName,UOM,SubGroup,ItemType,ItemRate,CostRate,KitCode) VALUES (";
                        sqlstring = sqlstring + " '" + (ItemName) + "','" + UOM + "','" + SubGroup + "','" + ItemType + "','" + ItemRate + "','" + CostRate + "','" + KitCode + "')";
                        List.Add(sqlstring);
                    }
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    //MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DataTable CheckData = new DataTable();
                sqlstring = " select * from BulkItem Where Isnull(ItemName,'') = '' ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "Item Name is Blank on your Excel";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }
                sqlstring = " Select * from BulkItem Where ItemName In (select ItemDesc from ItemMaster) ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "Listed Items already exits in Item master";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }
                sqlstring = " select * from  BulkItem Where isnull(UOM,'') Not in (select uomcode from UoMMaster) ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "UOM Not Found in Uom Master,Listed in Grid ";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }
                sqlstring = " select * from  BulkItem Where isnull(SubGroup,'') Not in (select subGroupCode from subgroupmaster Where isnull(Freeze,'') <> 'Y') ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "SubGroup Not Found in Sub Group Master,Listed in Grid ";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }
                sqlstring = " select * from  BulkItem Where isnull(ItemType,'') Not in (select CHARGECODE from CHARGEMASTER Where isnull(Freeze,'') <> 'Y') ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "TaxType Not Found in Charge Master,Listed in Grid ";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }
                ////sqlstring = " select * from  BulkItem Where isnull(POS,'') Not in (select POSCode from PosMaster Where isnull(Freeze,'') <> 'Y') ";
                ////CheckData = GCon.getDataSet(sqlstring);
                ////if (CheckData.Rows.Count > 0)
                ////{
                ////    label2.Text = "POS Not Found in POS Master,Listed in Grid ";
                ////    BindingSource SBind = new BindingSource();
                ////    SBind.DataSource = CheckData;
                ////    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                ////    dataGridView1.Columns.Clear();
                ////    dataGridView1.DataSource = SBind;
                ////    for (i = 0; i < dataGridView1.Columns.Count; i++)
                ////    {
                ////        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                ////        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                ////    }
                ////    dataGridView1.Enabled = true;
                ////    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ////    dataGridView1.Refresh();
                ////    return;
                ////}

                sqlstring = " select * from  BulkItem Where isnull(KitCode,'') Not in (select kitchenCode from kitchenmaster) ";
                CheckData = GCon.getDataSet(sqlstring);
                if (CheckData.Rows.Count > 0)
                {
                    label2.Text = "Kitchen Code Not Found in Kitchen Master,Listed in Grid ";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = CheckData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = CheckData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = CheckData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                    return;
                }

                List.Clear();
                sqlstring = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BulkItem' AND  COLUMN_NAME = 'ItemCode') Begin Alter Table BulkItem Add ItemCode varchar(20) End";
                List.Add(sqlstring);
                sqlstring = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BulkItem' AND  COLUMN_NAME = 'autoid') Begin Alter Table BulkItem Add autoid int identity(1,1) End";
                List.Add(sqlstring);
                sqlstring = "IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BulkItem' AND  COLUMN_NAME = 'itemSeq') Begin Alter Table BulkItem Add itemSeq decimal(38, 0) End";
                List.Add(sqlstring);
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                    dataGridView1.DataSource = null;
                    //dataGridView1.Rows.Clear();
                    sqlstring = "EXEC BulkItemPosting ";
                    List.Add(sqlstring);
                    if (GCon.Moretransaction(List) > 0)
                    {
                        MessageBox.Show("Transaction completed ... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        List.Clear();
                        
                    }
                    else 
                    {
                        MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        List.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error While Import Data " + ex);
            }
        }
    }
}
