using Microsoft.VisualBasic;
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
    public partial class ServiceLocationUsers : Form
    {
        GlobalClass GCon = new GlobalClass();

        public ServiceLocationUsers()
        {
            InitializeComponent();
        }

        string sql = "";

        private void ServiceLocationUsers_Load(object sender, EventArgs e)
        {
            dataGridView2.RowHeadersVisible = false;
            FillLocation();
            Filluser();
            Cmb_Location_SelectedIndexChanged(sender, e);
        }

        private void FillLocation()
        {
            DataTable dt = new DataTable();
            sql = "SELECT LocName,LocCode FROM ServiceLocation_Hdr Where ServiceFlag = 'D' And Isnull(Void,'') <> 'Y' Order by 1 ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_Location.Items.Clear();
                Cmb_Location.DisplayMember = "LocName";
                Cmb_Location.ValueMember = "LocCode";
                Cmb_Location.DataSource = dt;
                Cmb_Location.SelectedIndex = 0;
            }
        }

        public void Filluser()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select distinct username  from Master..UserAdmin Where CATEGORY <> 'S' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();
                    dataGridView2.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            var userList = new List<string>();
            for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[1].Value) == true))
                {
                    userList.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }

            string Locdid, LocName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locdid = drv["LocCode"].ToString();
            LocName = drv["LocName"].ToString();

            sql = "Delete From Tbl_LocationUserTag Where Loccode = " + Locdid + "";
            List.Add(sql);
            foreach (string user in userList)
            {
                sql = "INSERT INTO Tbl_LocationUserTag (Loccode,LocName,UserName,AddUser,AddDate) ";
                sql = sql + " Values ('" + Locdid + "','" + LocName + "','" + user + "',";
                sql = sql + "'" + GlobalVariable.gUserName + "',getdate())";
                List.Add(sql);
            }

            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                MessageBox.Show("Data Updated Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Cmb_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Locdid, LocName;
            DataRowView drv = (DataRowView)Cmb_Location.SelectedItem;
            Locdid = drv["LocCode"].ToString();
            LocName = drv["LocName"].ToString();
            DataTable dt = new DataTable();

            for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
            {
                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[1];
                chkbox.Value = false;
            }

            sql = "select Loccode,LocName,UserName from Tbl_LocationUserTag Where Loccode = " + Locdid + " ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                    {
                        string s = (dt.Rows[i][2].ToString());
                        string p = dataGridView2.Rows[j].Cells[0].Value.ToString();
                        if (s == p)
                        {
                            DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[1];
                            chkbox.Value = true;
                        }
                    }

                }
            }
        }
    }
}
