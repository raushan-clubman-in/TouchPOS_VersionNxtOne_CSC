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

namespace TouchPOS.MASTER
{
    public partial class TableOrderNumbering : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        SqlConnection Connection = new SqlConnection();
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);

        public TableOrderNumbering(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "", Servicelocationcode = "";
        DataTable GLName = new DataTable();
        DataTable SGLName = new DataTable();

        private void TableOrderNumbering_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            fillservicelocation();
        }

        public void fillservicelocation()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select distinct loccode,LocName from ServiceLocation_Hdr where isnull(void,'')<>'Y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Servicelocationcode = dt.Rows[0]["loccode"].ToString();
                cmb_servicelocation.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_servicelocation.Items.Add(dt.Rows[i]["LocName"].ToString());
                }
                cmb_servicelocation.SelectedIndex = 0;
            }
        }

        private void cmb_servicelocation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string Text, loccode = "";
            if (cmb_servicelocation.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "select distinct loccode,LocName from ServiceLocation_Hdr where locname='" + cmb_servicelocation.Text + "' AND ISNULL(void,'') <> 'Y'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    loccode = dt.Rows[0]["loccode"].ToString();
                }
                FillGrid(loccode);
            }
        }

        public void FillGrid(string Loc)
        {
            DataTable PosCate = new DataTable();
            sql = " SELECT Isnull(Pos,'') as Code,Isnull(Posdesc,'') as Posdesc,isnull(TableNo,'') as TableNo,isnull(TableOrder,0) as TableOrder  FROM Tablemaster Where Isnull(Freeze,'') <> 'Y' and Pos= '" + Loc + "' Order by isnull(TableOrder,0),TableNo ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                //dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //this.dataGridView1.ReadOnly = true;
                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = PosCate.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = PosCate.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void btn_new_Click(object sender, System.EventArgs e)
        {
            fillservicelocation();
        }

        private void Btn_exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, System.EventArgs e)
        {
            ArrayList List = new ArrayList();
            string PosCode = "", Table = "";
            Int32 TNumber = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { PosCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                else { PosCode = ""; }
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                { Table = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                else { Table = ""; }
                if (dataGridView1.Rows[i].Cells[3].Value != null)
                { TNumber = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value); }
                else { TNumber = 0; }
                if (PosCode != "" && Table != "")
                {
                    sqlstring = "Update Tablemaster Set TableOrder = " + TNumber + " Where Pos = '" + PosCode + "' And TableNo = '" + Table + "' ";
                    List.Add(sqlstring);
                    sqlstring = "Update ServiceLocation_Tables Set TableOrder = " + TNumber + " Where LocCode = '" + PosCode + "' And TableNo = '" + Table + "' ";
                    List.Add(sqlstring);
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                btn_new_Click(sender, e);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3 )
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
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
    }
}
