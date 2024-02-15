using System;
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
    public partial class FormsRights : Form
    {
        public FormsRights()
        {
            InitializeComponent();
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Close();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "Node_ServiceLocation")
            {
                ServiceLocationUsers SLU = new ServiceLocationUsers();
                SLU.ShowDialog();
            }
            else if (e.Node.Name == "Node_MasterForm") 
            {
                MasterFormRights MFR = new MasterFormRights();
                MFR.ShowDialog();
            }
            else if (e.Node.Name == "Node_TransForm")
            {
                TransFormRights MFR = new TransFormRights();
                MFR.ShowDialog();
            }
        }

        private void FormsRights_Load(object sender, EventArgs e)
        {

        }
    }
}
