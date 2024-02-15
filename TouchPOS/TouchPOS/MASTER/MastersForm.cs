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
    public partial class MastersForm : Form
    {
        GlobalClass GCon = new GlobalClass();

        public MastersForm()
        {
            InitializeComponent();
        }

        string sql = "";

        private void MastersForm_Load(object sender, EventArgs e)
        {
            
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            UpdateForm();
            if (GlobalVariable.gUserCategory != "S") 
            {
                CheckRights();
            }
            if (GlobalVariable.gUserCategory == "S") 
            {
                Cmd_UserRelease.Enabled = true;
            }
            else {Cmd_UserRelease.Enabled = false;}
        }

        private void UpdateForm() 
        {
            ArrayList List = new ArrayList();
            List<Control> list = new List<Control>();
            GetAllControl(this, list);
            sql = "Delete from TPOS_MasterReportForm Where FormType = 'MASTER'";
            List.Add(sql);
            foreach (Control control in list)
            {
                if (control.GetType() == typeof(Button))
                {
                    //all btn
                    if (control.Text.ToUpper() != "EXIT") 
                    {
                        sql = "Insert into TPOS_MasterReportForm (FormType,FormName)";
                        sql = sql + " Values ('MASTER','" + control.Text.ToUpper() + "') ";
                        List.Add(sql);
                    }
                }
            }
            GCon.OpenConnection();
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
        }

        private void CheckRights() 
        {
            DataTable UserData = new DataTable();
            ArrayList List = new ArrayList();
            List<Control> list = new List<Control>();
            GetAllControl(this, list);
            sql = "select FormName from Tbl_MasterFormUserTag Where UserName = '" + GlobalVariable.gUserName + "' ";
            UserData = GCon.getDataSet(sql);
            foreach (Control control in list)
            {
                if (control.GetType() == typeof(Button))
                {
                    if (control.Text.ToUpper() != "EXIT")
                    {
                        control.Enabled = false;
                        if (UserData.Rows.Count > 0) 
                        {
                            for (int i = 0; i <= UserData.Rows.Count - 1; i++) 
                            {
                                if (control.Text.ToUpper() == UserData.Rows[i].ItemArray[0].ToString().ToUpper()) 
                                {
                                    control.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);
                //if (control.GetType() == typeof(Button))
                //    GetAllControl(control, list);
            }
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Close();
        }

        private void GR_MASTER_Click(object sender, EventArgs e)
        {
            Groupmaster GM = new Groupmaster(this);
            GM.ShowDialog();
        }

        private void Btn_SubGroupmaster_Click(object sender, EventArgs e)
        {
            SubGroupmaster SGM = new SubGroupmaster(this);
            SGM.ShowDialog();
        }

        private void Btn_category_Click(object sender, EventArgs e)
        {
            Categotymaster CM = new Categotymaster(this);
            CM.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Itemmaster IM = new Itemmaster(this);
            IM.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Discountmaster DM = new Discountmaster(this);
            DM.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UomMaster DM = new UomMaster(this);
            DM.ShowDialog();
        }

        private void Cmd_Table_Click(object sender, EventArgs e)
        {
            Tablemater DM = new Tablemater(this);
            DM.ShowDialog();
        }

        private void Cmd_SerLoc_Click(object sender, EventArgs e)
        {
            Servicelocation DM = new Servicelocation(this);
            DM.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PromotionMaster DM = new PromotionMaster(this);
            DM.ShowDialog();
        }

        private void Cmd_HappyHour_Click(object sender, EventArgs e)
        {
            HappyhourMaster DM = new HappyhourMaster(this);
            DM.ShowDialog();
        }

        private void Cmd_Kitchen_Click(object sender, EventArgs e)
        {
            Kitchenmaster KM = new Kitchenmaster(this);
            KM.ShowDialog();
        }

        private void Cmd_POS_Click(object sender, EventArgs e)
        {
            PosMaster PM = new PosMaster(this);
            PM.ShowDialog();
        }

        private void Cmd_Waiter_Click(object sender, EventArgs e)
        {
            ServerMaster SM = new ServerMaster(this);
            SM.ShowDialog();
        }

        private void Cmd_TaxType_Click(object sender, EventArgs e)
        {
            Taxmaster TM = new Taxmaster(this);
            TM.ShowDialog();
        }

        private void Cmd_Charge_Click(object sender, EventArgs e)
        {
            ChargeMaster CM = new ChargeMaster(this);
            CM.ShowDialog();
        }

        private void Cmd_NCSetup_Click(object sender, EventArgs e)
        {
            NCMaster NCM = new NCMaster(this);
            NCM.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ItemPosAccountTagging IPAT = new ItemPosAccountTagging(this);
            IPAT.ShowDialog();
        }

        private void Cmd_PayAccSetup_Click(object sender, EventArgs e)
        {
            PayAccountSetup PAS = new PayAccountSetup(this);
            PAS.ShowDialog();
        }

        private void Cmd_TabRelease_Click(object sender, EventArgs e)
        {
            TableRelease TRL = new TableRelease();
            TRL.ShowDialog();
        }

        private void Cmd_BSource_Click(object sender, EventArgs e)
        {
            BusinessSource BS = new BusinessSource(this);
            BS.ShowDialog();
        }

        private void Cmd_ModifierMaster_Click(object sender, EventArgs e)
        {
            ModifierMaster MM = new ModifierMaster(this);
            MM.ShowDialog();
        }

        private void Cmd_ModifierTagging_Click(object sender, EventArgs e)
        {
            ItemModifierTagging IMT = new ItemModifierTagging(this);
            IMT.ShowDialog();
        }

        private void Cmd_TableNum_Click(object sender, EventArgs e)
        {
            TableOrderNumbering TON = new TableOrderNumbering(this);
            TON.ShowDialog();
        }

        private void Cmd_BulkIten_Click(object sender, EventArgs e)
        {
            BulkItemCreation BIC = new BulkItemCreation(this);
            BIC.ShowDialog();
        }

        private void Cmd_UserRelease_Click(object sender, EventArgs e)
        {
            UserLogRelease ULR = new UserLogRelease();
            ULR.ShowDialog();
        }
    }
}
