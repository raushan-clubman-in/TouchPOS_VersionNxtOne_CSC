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
    public partial class UserLogRelease : Form
    {
        GlobalClass GCon = new GlobalClass();

        public UserLogRelease()
        {
            InitializeComponent();
        }

        string sql = "";
        DataTable Ocpd = new DataTable();

        private void UserLogRelease_Load(object sender, EventArgs e)
        {
            sql = " select UserId from UserActiveSession Order by 1";
            Ocpd = GCon.getDataSet(sql);
            if (Ocpd.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in Ocpd.Rows)
                {
                    lst.Add(r["UserId"].ToString());
                }
                FromListBox.Items.Clear();
                FromListBox.DataSource = lst;
            }
        }

        private void RefreseTable()
        {
            sql = " select UserId from UserActiveSession Order by 1 ";
            Ocpd = GCon.getDataSet(sql);
            if (Ocpd.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in Ocpd.Rows)
                {
                    lst.Add(r["UserId"].ToString());
                }
               // FromListBox.Items.Clear();
                FromListBox.DataSource = lst;
            }
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            string selectedItem = "";
            string deviceInformation = System.Environment.MachineName;
            if (FromListBox.SelectedItems.Count == 0) { return; }
            selectedItem = FromListBox.SelectedItem.ToString();
            ArrayList List = new ArrayList();
           
            if (selectedItem.ToString() != "")
            {
                sql = "Insert into UserActiveSession_Log (UserId,ModuleName,SessionStart,SessionType,DeviceId) Select UserId,ModuleName,SessionStart,'LogIn',DeviceId From UserActiveSession Where UserId = '" + selectedItem.ToString() + "' ";
                List.Add(sql);
                sql = "Insert into UserActiveSession_Log (UserId,ModuleName,SessionStart,SessionType,DeviceId) Values ('" + selectedItem.ToString() + "','TPOS',Getdate(),'LogOut-R','" + deviceInformation + "')";
                List.Add(sql);
                sql = "Delete From UserActiveSession Where UserId = '" + selectedItem.ToString() + "'";
                List.Add(sql);
            }
            if (GCon.Moretransaction(List) > 0)
            {
                MessageBox.Show("Release Sucessfully ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                List.Clear();
                RefreseTable();
            }
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
