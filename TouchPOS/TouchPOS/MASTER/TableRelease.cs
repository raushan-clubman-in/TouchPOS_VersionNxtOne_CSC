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
    public partial class TableRelease : Form
    {
        GlobalClass GCon = new GlobalClass();

        public TableRelease()
        {
            InitializeComponent();
        }

        string sql = "";
        DataTable Ocpd = new DataTable();

        private void TableRelease_Load(object sender, EventArgs e)
        {

            sql = "SELECT TableNo FROM TableMaster WHERE ISNULL(OpenStatus,'')<> '' Order by 1";
            Ocpd = GCon.getDataSet(sql);
            if (Ocpd.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in Ocpd.Rows)
                {
                    lst.Add(r["TableNo"].ToString());
                }
                FromListBox.Items.Clear();
                FromListBox.DataSource = lst;
            }

        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            string selectedItem = "";
            if (FromListBox.SelectedItems.Count == 0) { return; }
            selectedItem = FromListBox.SelectedItem.ToString();
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string[] FromItem = selectedItem.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (FromItem[0].ToString() != "") 
            {
                sqlstring = " UPDATE TableMaster SET OPENSTATUS = '' WHERE TableNo = '" + FromItem[0] + "' ";
                List.Add(sqlstring);
                sqlstring = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE TableNo = '" + GlobalVariable.TableNo + "' ";
                List.Add(sqlstring);
            }
            if (GCon.Moretransaction(List) > 0)
            {
                MessageBox.Show("Release Sucessfully ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                List.Clear();
                RefreseTable();
            }
        }

        private void RefreseTable() 
        {
            sql = "SELECT TableNo FROM TableMaster WHERE ISNULL(OpenStatus,'')<> '' Order by 1";
            Ocpd = GCon.getDataSet(sql);
            if (Ocpd.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in Ocpd.Rows)
                {
                    lst.Add(r["TableNo"].ToString());
                }
                //FromListBox.Items.Clear();
                FromListBox.DataSource = lst;
            }
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
