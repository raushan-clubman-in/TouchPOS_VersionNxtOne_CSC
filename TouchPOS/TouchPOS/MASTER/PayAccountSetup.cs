using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.MASTER
{
    public partial class PayAccountSetup : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        SqlConnection Connection = new SqlConnection();

        public PayAccountSetup(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        string sql = "";
        string sqlstring = "";
        DataTable GLName = new DataTable();
        DataTable SGLName = new DataTable();

        private void PayAccountSetup_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillGrid();
            string queryItem = "Select accode,acdesc+'=>'+accode as acdesc from AccountsGlAccountMaster Where Isnull(freezeflag,'') <> 'Y' ";
            GLName = GCon.getDataSet(queryItem);
            string queryItem1 = "select slcode,sldesc+''+slcode as sldesc from ACCOUNTSSUBLEDGERMASTER Where isnull(Freezeflag,'') <> 'Y' And isnull(Slcode,'') <> '' And accode = '' ";
            SGLName = GCon.getDataSet(queryItem1);
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox2.Size;
            groupBox2.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void FillGrid()
        {
            DataTable ACSetup = new DataTable();
            sql = " SELECT PaymentCode AS PayMode,MemberStatus,Isnull(Accountin,'') accode, ISNULL(A.acdesc,'') AS acdesc,isnull(P.slcode,'') as slcode,Isnull(S.sldesc,'') as slname,Isnull(P.Freeze,'N') as Freeze FROM PAYMENTMODEMASTER P ";
            sql = sql + " Left Outer Join accountsglaccountmaster A on P.AccountIn = A.accode Left Outer join ACCOUNTSSUBLEDGERMASTER S on P.AccountIn = S.accode and P.slcode = s.slcode Order by 1 ";
            ACSetup = GCon.getDataSet(sql);
            if (ACSetup.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersHeight = 30;
                
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //DataGridViewCellStyle style = new DataGridViewCellStyle();
                //style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                //dataGridView1.DefaultCellStyle = style;
                for (int i = 0; i < ACSetup.Rows.Count; i++)
                {
                    DataRow dr = ACSetup.Rows[i];
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr["PayMode"];
                    dataGridView1.Rows[i].Cells[1].Value = dr["MemberStatus"];
                    if (Convert.ToString(dr["accode"]) != "") { dataGridView1.Rows[i].Cells[2].Value = dr["acdesc"] + "=>" + dr["accode"]; }
                    else { dataGridView1.Rows[i].Cells[2].Value = ""; }
                    if (Convert.ToString(dr["slcode"]) != "") { dataGridView1.Rows[i].Cells[3].Value = dr["slname"] + "=>" + dr["slcode"]; }
                    else { dataGridView1.Rows[i].Cells[3].Value = ""; }
                    //dataGridView1.Rows[i].DefaultCellStyle = style;
                    dataGridView1.Rows[i].Height = 30;
                }
            }
            this.dataGridView1.Columns[0].ReadOnly = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string acccode = "";
            string[] SplitCode = { "", "" };
            int index1 = 0;
            index1 = dataGridView1.CurrentRow.Index; ;
            if (dataGridView1.Rows[index1].Cells[2].Value != null)
            { acccode = Convert.ToString(dataGridView1.Rows[index1].Cells[2].Value); }
            else { acccode = ""; }
            if (acccode != "")
            {
                SplitCode = acccode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                acccode = SplitCode[1];
            }
            else { acccode = ""; }

            string queryItem1 = "select slcode,sldesc+'=>'+slcode as sldesc from ACCOUNTSSUBLEDGERMASTER Where isnull(Freezeflag,'') <> 'Y' And isnull(Slcode,'') <> '' And accode = '" + acccode + "' ";
            SGLName = GCon.getDataSet(queryItem1);

            if (dataGridView1.EditingControl.GetType() == typeof(DataGridViewTextBoxEditingControl))
            {
                int currentRow = dataGridView1.CurrentRow.Index;
                TextBox prodCode = e.Control as TextBox;
                TextBox prodCode1 = e.Control as TextBox;
                if (dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<DataRow, String>(GLName.Select(), delegate(DataRow row) { return (String)row["acdesc"]; });
                    source.AddRange(stringArray);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                //else { prodCode.AutoCompleteCustomSource = null; }
                else if (dataGridView1.CurrentCell.ColumnIndex == 3)
                {
                    var source1 = new AutoCompleteStringCollection();
                    String[] stringArray1 = Array.ConvertAll<DataRow, String>(SGLName.Select(), delegate(DataRow row) { return (String)row["sldesc"]; });
                    source1.AddRange(stringArray1);
                    //TextBox prodCode = e.Control as TextBox;
                    if (prodCode1 != null)
                    {
                        prodCode1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode1.AutoCompleteCustomSource = source1;
                        prodCode1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                //else { prodCode1.AutoCompleteCustomSource = null; }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FillGrid();
            string queryItem = "Select accode,acdesc+'=>'+accode as acdesc from AccountsGlAccountMaster Where Isnull(freezeflag,'') <> 'Y' ";
            GLName = GCon.getDataSet(queryItem);
            string queryItem1 = "select slcode,sldesc+''+slcode as sldesc from ACCOUNTSSUBLEDGERMASTER Where isnull(Freezeflag,'') <> 'Y' And isnull(Slcode,'') <> '' And accode = '' ";
            SGLName = GCon.getDataSet(queryItem1);
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DataTable GLCheck = new DataTable();
            string PayCode = "", ACCode = "", SGLcode = "", ReqInput = "", Scode = "";
            string[] SplitCode = { "", "" };
            for (int i = 0; i < dataGridView1.RowCount; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { PayCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); }
                else { PayCode = ""; }
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                { ReqInput = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value); }
                else { ReqInput = ""; }

                if (dataGridView1.Rows[i].Cells[2].Value != null)
                { Scode = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value); }
                else { Scode = ""; }
                if (Scode != "")
                {
                    SplitCode = Scode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    ACCode = SplitCode[1];
                }
                else { ACCode = ""; }

                if (dataGridView1.Rows[i].Cells[3].Value != null)
                { Scode = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value); }
                else { Scode = ""; }
                if (Scode != "")
                {
                    SplitCode = Scode.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    SGLcode = SplitCode[1];
                }
                else { SGLcode = ""; }
                if (PayCode != "" && ACCode != "") 
                {
                    sqlstring = "select * from accountsglaccountmaster Where accode = '" + ACCode + "' And Isnull(freezeflag,'') <> 'Y' ";
                    GLCheck = GCon.getDataSet(sqlstring);
                    if (GLCheck.Rows.Count == 0) 
                    { 
                        MessageBox.Show("Given Account Not Valid");
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[2];
                        return;
                    }
                    sqlstring = "Update PAYMENTMODEMASTER Set MemberStatus = '" + ReqInput + "',AccountIn = '" + ACCode + "',slcode = '" + SGLcode + "' Where PaymentCode = '" + PayCode + "' ";
                    List.Add(sqlstring);
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                btn_new_Click(sender, e);
            }
        }

        public class myGroupBox : GroupBox
        {

            private Color borderColor;

            public Color BorderColor
            {

                get { return this.borderColor; }

                set { this.borderColor = value; }

            }



            public myGroupBox()
            {

                this.borderColor = Color.Black;

            }



            protected override void OnPaint(PaintEventArgs e)
            {

                Size tSize = TextRenderer.MeasureText(this.Text, this.Font);



                Rectangle borderRect = e.ClipRectangle;

                borderRect.Y += tSize.Height / 2;

                borderRect.Height -= tSize.Height / 2;

                ControlPaint.DrawBorder(e.Graphics, borderRect, this.borderColor, ButtonBorderStyle.Solid);



                Rectangle textRect = e.ClipRectangle;

                textRect.X += 6;

                textRect.Width = tSize.Width;

                textRect.Height = tSize.Height;

                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);

                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);

            }

        }
    }
}
