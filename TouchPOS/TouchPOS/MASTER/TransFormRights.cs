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
    public partial class TransFormRights : Form
    {
        GlobalClass GCon = new GlobalClass();

        public TransFormRights()
        {
            InitializeComponent();
        }

        string sql = "";

        private void TransFormRights_Load(object sender, EventArgs e)
        {
            dataGridView2.RowHeadersVisible = false;
            Filluser();
            FillForm();
        }

        public void Filluser()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select '' as username union all select distinct username  from Master..UserAdmin Where CATEGORY <> 'S' Order by 1";
            dt = GCon.getDataSet(sql);
            Cmb_User.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_User.Items.Add(dt.Rows[i]["username"].ToString());
                }
                Cmb_User.SelectedIndex = 0;
            }
        }

        public void FillForm()
        {
            DataTable dt = new DataTable();
            sql = "Select FormName from TPOS_MasterReportForm Where FormType = 'TRANSACTION' Order by 1";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[0].Width = 300;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    dataGridView2.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string AddM = "", EditM = "",DelM = "", FormName = "";
            if (Cmb_User.Text == "") { MessageBox.Show("User Can't be Blank"); return; }
            sql = "Delete From Tbl_TransactionFormUserTag Where UserName = '" + Cmb_User.Text + "'";
            List.Add(sql);
            for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[1].Value) == true))
                {
                    AddM = "Y";
                }
                else { AddM = "N"; }
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                {
                    EditM = "Y";
                }
                else { EditM = "N"; }
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[3].Value) == true))
                {
                    DelM = "Y";
                }
                else { DelM = "N"; }
                if (dataGridView2.Rows[i].Cells[0].Value != null) { FormName = dataGridView2.Rows[i].Cells[0].Value.ToString(); }
                else { FormName = ""; }
                if ((FormName != "") && (AddM == "Y" || EditM == "Y" || DelM == "Y"))
                {
                    sql = " insert into Tbl_TransactionFormUserTag (UserName,FormName,AddM,EditM,DelM,AddUser,AddDate) ";
                    sql = sql + "values ('" + Cmb_User.Text + "','" + FormName + "','" + AddM + "','" + EditM + "','" + DelM + "','" + GlobalVariable.gUserName + "',GETDATE())";
                    List.Add(sql);
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                MessageBox.Show("Data Updated Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Cmb_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AddM = "", EditM = "", DelM = "", FormName = "";
            for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
            {
                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[1];
                chkbox.Value = false;
                chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[2];
                chkbox.Value = false;
                chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[3];
                chkbox.Value = false;
            }

            DataTable dt = new DataTable();
            sql = "select FormName,AddM,EditM,DelM from Tbl_TransactionFormUserTag Where UserName = '" + Cmb_User.Text + "' Order by 1";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FormName = (dt.Rows[i][0].ToString());
                    AddM = (dt.Rows[i][1].ToString());
                    EditM = (dt.Rows[i][2].ToString());
                    DelM = (dt.Rows[i][3].ToString());
                    for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                    {
                        string p = dataGridView2.Rows[j].Cells[0].Value.ToString();
                        if (FormName == p)
                        {
                            if (AddM == "Y")
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[1];
                                chkbox.Value = true;
                            }
                            if (EditM == "Y")
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[2];
                                chkbox.Value = true;
                            }
                            if (DelM == "Y")
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[3];
                                chkbox.Value = true;
                            }
                        }
                    }
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
