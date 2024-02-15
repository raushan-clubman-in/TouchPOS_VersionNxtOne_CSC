using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModecardVB;
using System.Collections;

namespace TouchPOS
{
    public partial class PrePaidMultiCardTagging : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly PayForm _form1;
        public string KotOrderNo = "";
        public Double BillAmount = 0;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);

        public PrePaidMultiCardTagging(PayForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";

        private void Cmd_Processed_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string CId = "", CCode = "", CHolder ="";
            double DedAmt = 0;
            Calculate();
            if (dataGridView1.Rows.Count == 0) 
            {
                MessageBox.Show("No Card Tagged");
                return;
            }
            if (Convert.ToDouble(Lbl_Balance.Text) < 0) 
            {
                MessageBox.Show("Sorry! for Processed,Tagged Amount is More then Bill Amount");
                return;
            }
            if (dataGridView1.Rows.Count > 0) 
            {
                sqlstring = "Delete from Tbl_PrePaidCardTagging Where KotNo = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                List.Add(sqlstring);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null) { CId = dataGridView1.Rows[i].Cells[0].Value.ToString(); } else { CId = ""; }
                    if (dataGridView1.Rows[i].Cells[1].Value != null) { CCode = dataGridView1.Rows[i].Cells[1].Value.ToString(); } else { CCode = ""; }
                    if (dataGridView1.Rows[i].Cells[2].Value != null) { CHolder = dataGridView1.Rows[i].Cells[2].Value.ToString(); } else { CHolder = ""; }
                    if (dataGridView1.Rows[i].Cells[3].Value != null) { DedAmt = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value); } else { DedAmt = 0; }
                    if (CId != "" && CCode != "" && DedAmt > 0)
                    {
                        sqlstring = "Insert Into Tbl_PrePaidCardTagging (KotNo,FinYear,DigitCode,HolderCode,HolderName,AddUser,AddDate,DeductAmt) ";
                        sqlstring = sqlstring + " Values ('" + KotOrderNo + "','" + FinYear1 + "','" + CId + "','" + CCode + "','" + CHolder + "','" + GlobalVariable.gUserName + "',getdate()," + DedAmt + ")";
                        List.Add(sqlstring);
                    }
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                MessageBox.Show("Infomation Updated Successfully");
                this.Hide();
            }
        }


        private void PrePaidMultiCardTagging_Load(object sender, EventArgs e)
        {
            DataTable KotNonCheck = new DataTable();
            Txt_Cardid.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_CardBal.Text = "0";
            Txt_Cardid.Enabled = true;
            Lbl_BillAmount.Text = BillAmount.ToString();
            dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            sql = "select Isnull(KotNo,'') as KotNo,Isnull(FinYear,'') as FinYear,Isnull(DigitCode,'') as DigitCode,Isnull(HolderCode,'') as HolderCode,Isnull(HolderName,'') as HolderName,Isnull(DeductAmt,0) as DeductAmt from Tbl_PrePaidCardTagging Where KotNo = '" + KotOrderNo + "' And FinYear = '" + FinYear1 + "'";
            KotNonCheck = GCon.getDataSet(sql);
            if (KotNonCheck.Rows.Count > 0) 
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < KotNonCheck.Rows.Count; i++) 
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = KotNonCheck.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[1].Value = KotNonCheck.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[2].Value = KotNonCheck.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[3].Value = KotNonCheck.Rows[i].ItemArray[5];
                }
            }
            Calculate();
            Txt_Cardid.Focus();
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_OK_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            sql = "SELECT * FROM SM_CARDFILE_HDR WHERE [16_DIGIT_CODE] = '" + Txt_Cardid.Text.Trim() + "' And Isnull(Activation_Flag,'') = 'Y' And Isnull(IssueType,'') = 'PREP' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Lbl_CardCode.Text = dr["CARDCODE"].ToString();
                Lbl_CardHolderName.Text = dr["CARDHOLDERNAME"].ToString();
                Lbl_CardBal.Text = dr["BALANCE"].ToString();
                Txt_Cardid.Enabled = false;
            }
            else
            {
                MessageBox.Show("Details Not Found,Plz Check Card In SmartCard Module", GlobalVariable.gCompanyName);
                Cmd_Clear_Click(sender, e);
            }
        }

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Txt_Cardid.Text = "";
            Lbl_CardCode.Text = "";
            Lbl_CardHolderName.Text = "";
            Lbl_CardBal.Text = "0";
            Txt_Cardid.Enabled = true;
            Txt_Cardid.Focus();
        }

        private void Cmd_AddRow_Click(object sender, EventArgs e)
        {
            int RowCnt;

            RowCnt = dataGridView1.RowCount;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null )
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == Lbl_CardCode.Text) 
                    {
                        MessageBox.Show(Lbl_CardCode.Text + " Already Selected in Grid");
                        return;
                    }
                }
            }
            if (Txt_Cardid.Text == "" && Lbl_CardCode.Text == "") 
            {
                MessageBox.Show("No Any Card is Swiped?");
                return;
            }
            if (Convert.ToDouble(Lbl_CardBal.Text) <= 0) 
            {
                MessageBox.Show("Balance Not Available");
                return;
            }

            dataGridView1.Rows.Add();
            dataGridView1.Rows[RowCnt ].Cells[0].Value = Txt_Cardid.Text;
            dataGridView1.Rows[RowCnt ].Cells[1].Value = Lbl_CardCode.Text;
            dataGridView1.Rows[RowCnt].Cells[2].Value = Lbl_CardHolderName.Text;
            if (Convert.ToDouble(Lbl_Balance.Text) > 0) 
            {
                if (Convert.ToDouble(Lbl_CardBal.Text) > Convert.ToDouble(Lbl_Balance.Text))
                {
                    dataGridView1.Rows[RowCnt ].Cells[3].Value = Convert.ToDouble(Lbl_Balance.Text);
                }
                else
                {
                    dataGridView1.Rows[RowCnt ].Cells[3].Value = Convert.ToDouble(Lbl_CardBal.Text);
                }
            }
            Calculate();
        }

        private void Calculate() 
        {
            double CardBal = 0;
            double BalAmt = 0;
            BalAmt = Convert.ToDouble(BillAmount);
            CardBal = Convert.ToDouble(Lbl_CardBal.Text);
            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[3].Value != null) 
                {
                    BalAmt = BalAmt - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                }
            }
            Lbl_Balance.Text = String.Format("{0:0.##}", Math.Round(BalAmt, 0));
        }

        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Calculate();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Calculate();
        }

        private void Cmd_RemoveRow_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(index);
            Calculate();
        }
    }
}
