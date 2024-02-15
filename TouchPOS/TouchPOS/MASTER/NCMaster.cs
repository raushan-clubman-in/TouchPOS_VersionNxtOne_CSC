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
    public partial class NCMaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);
        

        public NCMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(22, 10);
        }

        string sql = "";
        string sqlstring = "";

        private void NCMaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillGrid();
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
            DataTable NCMaster = new DataTable();
            sql = " select Isnull(NCCategory,'') as NCCategory,Isnull(TypeForRate,'') as TypeForRate,Isnull(PSPercent,0) as PSPercent from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' Order by 1 ";
            NCMaster = GCon.getDataSet(sql);
            if (NCMaster.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnHeadersHeight = 30;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < NCMaster.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = NCMaster.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = NCMaster.Rows[i].ItemArray[1];

                    if (NCMaster.Rows[i].ItemArray[1].ToString() == "PS")
                    {
                        dataGridView1.Rows[i].Cells[2].ReadOnly = false;
                    }
                    else { dataGridView1.Rows[i].Cells[2].ReadOnly = true; }

                    if (Convert.ToDouble(NCMaster.Rows[i].ItemArray[2]) == 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = "";
                    }
                    else 
                    {
                        dataGridView1.Rows[i].Cells[2].Value = NCMaster.Rows[i].ItemArray[2].ToString();
                    }
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                // Remove an existing event-handler, if present, to avoid 
                // adding multiple handlers when the editing control is reused.
                combo.SelectedIndexChanged -=
                    new EventHandler(ComboBox_SelectedIndexChanged);

                // Add the event handler. 
                combo.SelectedIndexChanged +=
                    new EventHandler(ComboBox_SelectedIndexChanged);
            }
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string TypeVal = ((ComboBox)sender).SelectedItem.ToString();
            if (TypeVal == "PS") { dataGridView1.Rows[index].Cells[2].ReadOnly = false; }
            else { dataGridView1.Rows[index].Cells[2].ReadOnly = true; dataGridView1.Rows[index].Cells[2].Value = ""; }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string CateGory = "",TypeForRate;
            string CatePerc = "";

            sqlstring = " Update Tbl_NCCategoryMaster Set  void = 'Y' Where Isnull(Void,'') <> 'Y' ";
            List.Add(sqlstring);

            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    CateGory = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
                else { CateGory = ""; }

                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    TypeForRate = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
                else { TypeForRate = ""; }

                if (dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    CatePerc = dataGridView1.Rows[i].Cells[2].Value.ToString();
                }
                else { CatePerc = "0"; }
                if (CatePerc == "") { CatePerc = "0"; }

                if (CateGory != "") 
                {
                    sqlstring = " Insert Into Tbl_NCCategoryMaster (NCCategory,TypeForRate,PSPercent,Void,AdduserId,AddDate) Values('" + CateGory + "','" + TypeForRate + "','" + CatePerc + "','N','" + GlobalVariable.gUserName + "',getdate()) ";
                    List.Add(sqlstring);
                }
            }

            if (List.Count > 0)
            {
                if (GCon.Moretransaction(List) > 0)
                { List.Clear(); FillGrid(); }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void Cmd_AddRow_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count -1].Cells[0];
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = "P";
        }

        private void Cmd_RemoveRow_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(index);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
