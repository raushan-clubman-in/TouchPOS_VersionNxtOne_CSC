using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class BusinessSource : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;


        public BusinessSource(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(22, 10);
        }

        string sql = "";
        string sqlstring = "";

        private void BusinessSource_Load(object sender, EventArgs e)
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
            DataTable BSMaster = new DataTable();
            sql = " select Isnull(BusinessSource,'') as BusinessSource from Tbl_BusinessSource Where Isnull(void,'') <> 'Y' Order by 1 ";
            BSMaster = GCon.getDataSet(sql);
            if (BSMaster.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < BSMaster.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = BSMaster.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, System.EventArgs e)
        {
            FillGrid();
        }

        private void Cmd_AddRow_Click(object sender, System.EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void Cmd_RemoveRow_Click(object sender, System.EventArgs e)
        {
            DataTable ChkTrans = new DataTable();
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(index);
            //string val = dataGridView1.Rows[index].Cells[0].Value.ToString();
            //sql = "select distinct BusinessSource from kot_det where isnull(BusinessSource,'') = '" + val + "'";
            //ChkTrans = GCon.getDataSet(sql);
            //if (ChkTrans.Rows.Count > 0)
            //{
            //    MessageBox.Show("Already Used this Business Source Can't Remove");
            //}
            //else 
            //{
            //    dataGridView1.Rows.RemoveAt(index);
            //}
        }

        private void btn_save_Click(object sender, System.EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            string BCateGory = "";

            sqlstring = " Update Tbl_BusinessSource Set  void = 'Y' Where Isnull(Void,'') <> 'Y' ";
            List.Add(sqlstring);

            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    BCateGory = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
                else { BCateGory = ""; }

                if (BCateGory != "")
                {
                    sqlstring = " Insert Into Tbl_BusinessSource (BusinessSource,AdduserId,AddDate,Void) Values('" + BCateGory + "','" + GlobalVariable.gUserName + "',getdate(),'N') ";
                    List.Add(sqlstring);
                }
            }
            
            if (List.Count > 0)
            {
                if (GCon.Moretransaction(List) > 0)
                { List.Clear(); FillGrid(); }
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
