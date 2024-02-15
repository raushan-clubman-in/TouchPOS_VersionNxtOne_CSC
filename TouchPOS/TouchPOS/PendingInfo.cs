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
    public partial class PendingInfo : Form
    {
        GlobalClass GCon = new GlobalClass();
        public DataTable FillData;
        public string datatype = "";
        public readonly DayClose _form1;

        public PendingInfo(DayClose form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        private void PendingInfo_Load(object sender, EventArgs e)
        {
            Grp_Data.Text = datatype;
            if (FillData.Rows.Count > 0) 
            {
                BindingSource SBind = new BindingSource();
                SBind.DataSource = FillData;
                dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = SBind;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DataPropertyName = FillData.Columns[i].ColumnName;
                    dataGridView1.Columns[i].HeaderText = FillData.Columns[i].Caption;
                }
                dataGridView1.Enabled = true;
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Export_Click(object sender, EventArgs e)
        {
            
        }
    }
}
