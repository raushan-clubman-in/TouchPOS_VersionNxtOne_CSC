using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class DiscItemUpdation : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public string DUserName = "";
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        public string DiscType = "";
        public double GlobalDiscPerc = 0;
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);

        public DiscItemUpdation(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void DiscItemUpdation_Load(object sender, EventArgs e)
        {
            Rdb_Item.Checked = true;
            FillItemBasis();
        }

        private void FillItemBasis() 
        {
            DataTable KotData = new DataTable();
            sql = "SELECT Itemcode,ItemDesc FROM KOT_det WHERE KOTDETAILS = '" + KotOrderNo + "' And Isnull(KotStatus,'') <> 'Y' And Isnull(Delflag,'') <> 'Y' And Isnull(FinYear,'') = '" + FinYear1 + "' Group by Itemcode,ItemDesc ";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0) 
            {
                BindingSource SBind = new BindingSource();
                SBind.DataSource = KotData;

                dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = SBind;

                //set DGV's column names and headings from the Datatable properties
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DataPropertyName = KotData.Columns[i].ColumnName;
                    dataGridView1.Columns[i].HeaderText = KotData.Columns[i].Caption;
                }
                dataGridView1.Enabled = true;
                dataGridView1.Refresh();
                DataTable Userdt = new DataTable();
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.HeaderText = "DiscPerc";
                dgvCmb.Items.Add("0.00");
                sql = "SELECT DISTINCT DISCPERCENT FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' AND USERNAME = '" + DUserName + "' And Isnull(DiscType,'FIXED PERCENTAGE') = 'FIXED PERCENTAGE' ";
                Userdt = GCon.getDataSet(sql);
                if (Userdt.Rows.Count > 0) 
                {
                    for (int i = 0; i < Userdt.Rows.Count; i++) 
                    {
                        dgvCmb.Items.Add(Userdt.Rows[i].ItemArray[0].ToString()); 
                    }
                }
                dgvCmb.Name = "cmbName";
                dataGridView1.Columns.Add(dgvCmb);
                //DataGridViewTextBoxColumn dgvTxt = new DataGridViewTextBoxColumn();
                //dgvTxt.HeaderText = "OpenPerc";
                //dgvTxt.Name = "txtOpenPerc";
                //dataGridView1.Columns.Add(dgvTxt);
                DataGridViewTextBoxColumn dc = new DataGridViewTextBoxColumn();
                dc.HeaderText = "DescPerc";
                dataGridView1.Columns.Add(dc);
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[0].Width = 50;
                this.dataGridView1.Columns[1].Width = 200;
                this.dataGridView1.Columns[2].Width = 75;
                this.dataGridView1.Columns[3].Width = 75;
                if (DiscType == "FIXED PERCENTAGE") { this.dataGridView1.Columns[3].ReadOnly = true; }
                else if (DiscType == "OPEN PERCENTAGE") { this.dataGridView1.Columns[2].ReadOnly = true; }
                else
                {
                    this.dataGridView1.Columns[2].ReadOnly = true;
                    this.dataGridView1.Columns[3].ReadOnly = true;
                }
            }
        }

        private void FillGroupBasis() 
        {
            DataTable KotData = new DataTable();
            sql = "SELECT D.GroupCode,G.GroupDesc FROM KOT_det D,groupmaster G WHERE d.GROUPCODE = g.GroupCode And KOTDETAILS = '" + KotOrderNo + "' And Isnull(KotStatus,'') <> 'Y' And Isnull(Delflag,'') <> 'Y' And Isnull(FinYear,'') = '" + FinYear1 + "' Group by D.GroupCode,G.GroupDesc ";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0)
            {
                BindingSource SBind = new BindingSource();
                SBind.DataSource = KotData;

                dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = SBind;

                //set DGV's column names and headings from the Datatable properties
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DataPropertyName = KotData.Columns[i].ColumnName;
                    dataGridView1.Columns[i].HeaderText = KotData.Columns[i].Caption;
                }
                dataGridView1.Enabled = true;
                dataGridView1.Refresh();
                DataTable Userdt = new DataTable();
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.HeaderText = "DiscPerc";
                dgvCmb.Items.Add("0.00");
                sql = "SELECT DISTINCT DISCPERCENT FROM DISCOUNTEDUSERLIST WHERE ISNULL(FREEZE,'') <> 'Y' AND USERNAME = '" + DUserName + "' And Isnull(DiscType,'FIXED PERCENTAGE') = 'FIXED PERCENTAGE' ";
                Userdt = GCon.getDataSet(sql);
                if (Userdt.Rows.Count > 0)
                {
                    for (int i = 0; i < Userdt.Rows.Count; i++)
                    {
                        dgvCmb.Items.Add(Userdt.Rows[i].ItemArray[0].ToString());
                    }
                }
                dgvCmb.Name = "cmbName";
                dataGridView1.Columns.Add(dgvCmb);
                //DataGridViewTextBoxColumn dgvTxt = new DataGridViewTextBoxColumn();
                //dgvTxt.HeaderText = "OpenPerc";
                //dgvTxt.Name = "txtOpenPerc";
                //dataGridView1.Columns.Add(dgvTxt);
                DataGridViewTextBoxColumn dc = new DataGridViewTextBoxColumn();
                dc.HeaderText = "DescPerc";
                dataGridView1.Columns.Add(dc);
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[0].Width = 50;
                this.dataGridView1.Columns[1].Width = 200;
                this.dataGridView1.Columns[2].Width = 75;
                this.dataGridView1.Columns[3].Width = 75;
                if (DiscType == "FIXED PERCENTAGE") { this.dataGridView1.Columns[3].ReadOnly = true; }
                else if (DiscType == "OPEN PERCENTAGE") { this.dataGridView1.Columns[2].ReadOnly = true; }
                else 
                {
                    this.dataGridView1.Columns[2].ReadOnly = true;
                    this.dataGridView1.Columns[3].ReadOnly = true;
                }
            }
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Rdb_Item_CheckedChanged(object sender, EventArgs e)
        {
            FillItemBasis();
        }

        private void Rdb_Group_CheckedChanged(object sender, EventArgs e)
        {
            FillGroupBasis();
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string Code = "";
            double CodePerc = 0;
            CheckGrid();
            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    Code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
                else { Code = ""; }

                if (DiscType == "FIXED PERCENTAGE") 
                {
                    if (dataGridView1.Rows[i].Cells[2].Value != null)
                    {
                        CodePerc = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    }
                    else { CodePerc = 0; }
                }
                else if (DiscType == "OPEN PERCENTAGE")
                {
                    if (dataGridView1.Rows[i].Cells[3].Value != null)
                    {
                        CodePerc = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                    }
                    else { CodePerc = 0; }
                }
                else 
                {
                    if (dataGridView1.Rows[i].Cells[2].Value != null)
                    {
                        CodePerc = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    }
                    else { CodePerc = 0; }
                }

                if (Rdb_Item.Checked == true && Code != "") 
                {
                    sqlstring = "Update Kot_Det Set ItemDiscPerc = " + CodePerc + "  Where KOTDETAILS = '" + KotOrderNo + "' And ItemCode = '" + Code + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }
                else if (Rdb_Group.Checked == true) 
                {
                    sqlstring = "Update Kot_Det Set ItemDiscPerc = " + CodePerc + "  Where KOTDETAILS = '" + KotOrderNo + "' And GROUPCODE = '" + Code + "' And isnull(Kotstatus,'') <> 'Y' And isnull(Delflag,'')<> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }
            }
            if (List.Count > 0)
            {
                if (GCon.Moretransaction(List) > 0)
                { List.Clear(); }
            }
            this.Hide();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
            TextBox combo = e.Control as TextBox;
            if (combo != null)
            {
                combo.TextChanged -=
                    new EventHandler(TextBox_TextChanged);
                combo.TextChanged +=
                    new EventHandler(TextBox_TextChanged);
            }
        }

        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string TypeVal = ((TextBox)sender).Text.ToString();
            CheckGrid();
            //int value;
            //if (int.TryParse(TypeVal, out value))
            //{
            //    if (value > 100)
            //        dataGridView1.Rows[index].Cells[3].Value = 100; 
            //    else if (value < 0)
            //        dataGridView1.Rows[index].Cells[3].Value = 0;
            //    dataGridView1.Refresh();
            //}
        }

        private void CheckGrid() 
        {
            int value;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) 
            {
                if (dataGridView1.Rows[i].Cells[3].Value != null)
                {
                    value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }
                else { value = 0; }

                if (value > 100)
                    dataGridView1.Rows[i].Cells[3].Value = 100;
                else if (value < 0)
                    dataGridView1.Rows[i].Cells[3].Value = 0;
            }
        }
    }
}
