using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TouchPOS.MASTER
{
    public partial class ModifierMaster : Form
    {
        public readonly MastersForm _form1;

        public ModifierMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        GlobalClass GCon = new GlobalClass();
        string vseqno;
        Boolean MeValidate = false;
        string sql = "";
        string sqlstring = "";

        private void ModifierMaster_Load(object sender, EventArgs e)
        {
            Txt_MId.Text = "";
            Txt_MText.Text = "";
            Cmb_MType.SelectedIndex = 0;
            Cmb_freeze.SelectedIndex = 0;
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersVisible = false;
            MaxNumber();
            FillGrid();
            Txt_MText.Focus();
        }

        public void MaxNumber()
        {
            try 
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "select isnull(Max(MID),0) + 1 as MaxNumber from Tbl_Modifier";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    Txt_MId.Text = dt.Rows[0].ItemArray[0].ToString();
                }
                else { Txt_MId.Text = "1"; }
            }
            catch (Exception ex)
            {
                throw;
                return;
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Txt_MId.Text = "";
            Txt_MText.Text = "";
            Cmb_MType.SelectedIndex = 0;
            Cmb_freeze.SelectedIndex = 0;
            MaxNumber();
            FillGrid();
            Txt_MText.Focus();
        }

        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_MId.Text == "")
            {
                MessageBox.Show(" Modifier ID can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_MId.Focus();
                MeValidate = true;
                return;
            }
            if (Txt_MText.Text == "")
            {
                MessageBox.Show(" Modifier Text can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_MText.Focus();
                MeValidate = true;
                return;
            }
            MeValidate = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            checkvalidate();

            if (MeValidate == true)
            { return; }

            sql = "Select * from Tbl_Modifier  where MID = '" + Txt_MId.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  Tbl_Modifier Set MType = '" + (Cmb_MType.Text) + "', MText = '" + (Txt_MText.Text) + "',";
                sql = sql + "ADDUSER = '" + GlobalVariable.gUserName + "', ADDDATETIME = GETDATE(),";
                if (Cmb_freeze.Text == "NO")
                {
                    sql = sql + " VOID='N'";
                }
                else
                {
                    sql = sql + "VOID='Y'";
                }
                sql = sql + " where MID = '" + Txt_MId.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
            else 
            {
                sql = "Insert into Tbl_Modifier (MID,MType,MText,VOID,ADDUSER,ADDDATETIME) VALUES (";
                sql = sql + " '" + (Txt_MId.Text) + "','" + (Cmb_MType.Text) + "','" + (Txt_MText.Text) + "', ";
                sql = sql + " 'N','" + GlobalVariable.gUserName + "',GETDATE()) ";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
        }

        public void FillGrid()
        {
            DataTable MMaster = new DataTable();
            sql = " select MID,MType,MText,VOID from Tbl_Modifier ";
            MMaster = GCon.getDataSet(sql);
            if (MMaster.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.ReadOnly = true;
                for (int i = 0; i < MMaster.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = MMaster.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = MMaster.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = MMaster.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[3].Value = MMaster.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataTable ModifierMaster = new DataTable();
            Txt_MId.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sqlstring = "Select MID,MType,MText,VOID from Tbl_Modifier Where MID = '" + Txt_MId.Text + "'";
            ModifierMaster = GCon.getDataSet(sqlstring);
            if (ModifierMaster.Rows.Count > 0)
            {
                Txt_MId.Text = Convert.ToString(ModifierMaster.Rows[0].ItemArray[0]);
                Cmb_MType.Text = Convert.ToString(ModifierMaster.Rows[0].ItemArray[1]);
                Txt_MText.Text = Convert.ToString(ModifierMaster.Rows[0].ItemArray[2]);
                if (Convert.ToString(ModifierMaster.Rows[0].ItemArray[3]) == "Y")
                {
                    Cmb_freeze.Text = "YES";
                }
                else
                {
                    Cmb_freeze.Text = "NO";
                }
                Txt_MId.Enabled = false;
            }
        }
    }
}
