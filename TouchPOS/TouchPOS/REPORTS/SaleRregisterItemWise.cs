using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office;

namespace TouchPOS.REPORTS
{
    public partial class SaleRregisterItemWise : Form
    {
        GlobalClass GCon = new GlobalClass();

        public SaleRregisterItemWise()
        {
            InitializeComponent();
        }

        string sql = "";

        private void SaleRregisterItemWise_Load(object sender, EventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dtp1.Value = GlobalVariable.ServerDate;
            dtp2.Value = GlobalVariable.ServerDate;
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            string SSQL;
            SSQL = "EXEC TAXDETAILS_POS '" + this.dtp1.Value.ToString("dd-MMM-yyyy") + "','" + this.dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(SSQL);
            dataGridView1.Columns.Clear();
            DataTable BillData = new DataTable();
            sql = "select * from FinalTaxDetails Order by BillNo ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0)
            {
                BindingSource SBind = new BindingSource();
                SBind.DataSource = BillData;
                dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                
                dataGridView1.DataSource = SBind;

                //set DGV's column names and headings from the Datatable properties
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DataPropertyName = BillData.Columns[i].ColumnName;
                    dataGridView1.Columns[i].HeaderText = BillData.Columns[i].Caption;
                }
                dataGridView1.Enabled = true;
                dataGridView1.Refresh();
            }
        }

        private void Cmd_Export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) 
            {
                MessageBox.Show("First Generate from View then click export", GlobalVariable.ComputerName);
                return;
            }
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] =  "Sale Register Item wise Between " + " " + dtp1.Value.ToString("dd-MMM-yyyy") + " And  " + dtp2.Value.ToString("dd-MMM-yyyy");
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[2, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 3, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 3, j + 1] = "";
                    }
                }
            }
        }
    }
}
