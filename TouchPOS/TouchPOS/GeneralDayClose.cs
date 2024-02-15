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

namespace TouchPOS
{
    public partial class GeneralDayClose : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly ServiceType _form1;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public GeneralDayClose(ServiceType form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable PenKot = new DataTable();
        DataTable CheckDiscPerc = new DataTable();
        DataTable CheckDiffDebitOne = new DataTable();
        Boolean BoolDayClose = true;
        Double TotDeb = 0, TotCre = 0;

        private void GeneralDayClose_Load(object sender, EventArgs e)
        {
            Cmd_Confirm.Visible = false;
            label2.Text = "Process Date :" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
            label3.Text = "";
        }

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            DataTable FillData = new DataTable();
            ArrayList List = new ArrayList();
            DataTable PaidData = new DataTable();
            DateTime CurrServerDate = Convert.ToDateTime(GCon.getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
            TotDeb = 0;
            TotCre = 0;
            int i = 0;
            BoolDayClose = true;
            sql = "SELECT KotDetails,Isnull(BillAmount,0) as BillAmount,Isnull(SerType,'') as SerType,Isnull(LocName,'') as LocName,Isnull(Tableno,'') as Tableno From Kot_Hdr H where Billstatus = 'PO' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' ";
            sql = sql + " And Isnull(Kotdetails,'') in (select Isnull(kotdetails,'') from KOT_det where isnull(billdetails,'') = '' And Cast(Convert(varchar(11),kotdate,106) as Datetime) between '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') And isnull(Delflag,'') <> 'Y' Order by Kotdate Desc,Kotdetails Desc ";
            PenKot = GCon.getDataSet(sql);
            if (PenKot.Rows.Count > 0)
            {
                label3.Text = "Pending KOT Found";
                BoolDayClose = false;
                FillData = PenKot;
                if (FillData.Rows.Count > 0)
                {
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = FillData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = FillData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = FillData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
                }
                if (BoolDayClose == false) { return; }
            }
            if (GlobalVariable.ServerDate <= CurrServerDate)
            {}
            else { label3.Text = "You Can't Processed for future Business date"; BoolDayClose = false; return; }

            sql = " select CARDCODE,ISSUETYPE,VALID_TO,CARDHOLDERNAME,balance from sm_cardfile_hdr where issuetype='PREP' ";
            PaidData = GCon.getDataSet(sql);
            if (PaidData.Rows.Count > 0)
            {
                    label3.Text = "Cards will be Forfeitted after Confirm.";
                    BindingSource SBind = new BindingSource();
                    SBind.DataSource = PaidData;
                    dataGridView1.AutoGenerateColumns = true;  //must be "true" here
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SBind;
                    for (i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].DataPropertyName = PaidData.Columns[i].ColumnName;
                        dataGridView1.Columns[i].HeaderText = PaidData.Columns[i].Caption;
                    }
                    dataGridView1.Enabled = true;
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Refresh();
            }

            Cmd_Confirm.Visible = true;
            Cmd_Processed.Enabled = false;
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Confirm_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DateTime ToNextDate;
            DateTime CurrServerDate = Convert.ToDateTime(GCon.getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
            if (GlobalVariable.ServerDate <= CurrServerDate)
            { }
            else { MessageBox.Show("You Can't Processed for future Business date"); return; }
            ToNextDate = GlobalVariable.ServerDate;
            List.Clear();
            sqlstring = sqlstring = "UPDATE POSSETUP SET BillCloseDate = '" + ToNextDate.ToString("dd-MMM-yyyy") + "'";
            List.Add(sqlstring);
            if (GlobalVariable.gCompName == "SKYYE")
            {
                sqlstring = "EXEC dayendsmartcard '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "'";
                List.Add(sqlstring);
                sqlstring = "EXEC DAYENDsmartcardfinal '" + GlobalVariable.gUserName + "'";
                List.Add(sqlstring);
            }
           
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }

            GlobalVariable.ServerDate = Convert.ToDateTime(GCon.getValue("SELECT Isnull(BillCloseDate,'') FROM POSSETUP"));
            GlobalVariable.ServerDate = GlobalVariable.ServerDate.AddDays(1);
            MessageBox.Show("Day Close Completed Successfully");
            this.Close();
        }
    }
}
