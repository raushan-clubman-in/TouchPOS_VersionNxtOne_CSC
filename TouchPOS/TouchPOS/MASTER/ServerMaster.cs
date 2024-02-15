using Microsoft.VisualBasic;
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
    public partial class ServerMaster : Form
    {
        GlobalClass GCon = new GlobalClass();
        public readonly MastersForm _form1;

        public ServerMaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(22, 10);
        }

        string sql = "";
        string sqlstring = "";
        Boolean MeValidate = false;
        string vseqno;

        private void ServerMaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillGrid();
            Txt_Code.Text = "";
            Txt_Code.Enabled = true;
            Txt_Name.Text = "";
            Cmb_Type.SelectedIndex = 0;
            Cmb_Freeze.SelectedIndex = 0;
            Maxid();
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

        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, ServerCode)),0)+1 as ServerCode from ServerMaster where isnumeric(ServerCode) = 1";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Txt_Code.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }

        public void FillGrid()
        {
            DataTable SMaster = new DataTable();
            sql = " Select ServerCode,ServerName,SERVERTYPE,Freeze from ServerMaster ";
            SMaster = GCon.getDataSet(sql);
            if (SMaster.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.ReadOnly = true;
                for (int i = 0; i < SMaster.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = SMaster.Rows[i].ItemArray[0];
                    dataGridView1.Rows[i].Cells[1].Value = SMaster.Rows[i].ItemArray[1];
                    dataGridView1.Rows[i].Cells[2].Value = SMaster.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FillGrid();
            Txt_Code.Text = "";
            Txt_Code.Enabled = true;
            Txt_Name.Text = "";
            Cmb_Type.SelectedIndex = 0;
            Cmb_Freeze.SelectedIndex = 0;
            Maxid();
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataTable ServerMaster = new DataTable();
            Txt_Code.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sqlstring = "Select ServerCode,ServerName,SERVERTYPE,Freeze from ServerMaster Where ServerCode = '" + Txt_Code.Text + "'";
            ServerMaster = GCon.getDataSet(sqlstring);
            if (ServerMaster.Rows.Count > 0) 
            {
                Txt_Code.Text = Convert.ToString(ServerMaster.Rows[0].ItemArray[0]);
                Txt_Name.Text = Convert.ToString(ServerMaster.Rows[0].ItemArray[1]);
                Cmb_Type.Text = Convert.ToString(ServerMaster.Rows[0].ItemArray[2]);
                if (Convert.ToString(ServerMaster.Rows[0].ItemArray[2]) == "Y")
                {
                    Cmb_Freeze.Text = "YES";
                }
                else 
                {
                    Cmb_Freeze.Text = "NO";
                }
                Txt_Code.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            checkvalidate();

            if (MeValidate == true)
            { return;}

            sql = "Select * from ServerMaster  where ServerCode = '" + Txt_Code.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update  ServerMaster Set ServerName = '" + Txt_Name.Text + "',SERVERTYPE = '" + Cmb_Type.Text + "',";
                sql = sql + "UPDATEUSER='" + GlobalVariable.gUserName + "',";
                sql = sql + "UPDATETIME=getdate(),";
                if (Cmb_Freeze.Text == "NO")
                {
                    sql = sql + " Freeze='N'";
                }
                else
                {
                    sql = sql + "Freeze='Y'";
                }
                sql = sql + " where ServerCode = '" + Txt_Code.Text + "'";
                dt = GCon.getDataSet(sql);
                MessageBox.Show("Data Updated successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
            else 
            {
                sql = "select [dbo].[GetSeqno]('" + Txt_Code.Text + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sqlstring = "INSERT INTO ServerMaster (ServerCode,ServerSeqno,ServerName,SERVERTYPE,Freeze,AddUSer,AddDatetime) ";
                sqlstring = sqlstring + " Values ('" + Txt_Code.Text + "','" + vseqno + "','" + Txt_Name.Text + "','" + Cmb_Type.Text + "',";
                if (Cmb_Freeze.Text == "NO")
                {
                    sqlstring = sqlstring + "'N',";
                }
                else
                {
                    sqlstring = sqlstring + "'Y',";
                }
                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "',getdate())";
                dt = GCon.getDataSet(sqlstring);
                MessageBox.Show("Transaction completed successfully.... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_new_Click(sender, e);
            }
        }

        
        public void checkvalidate()
        {
            MeValidate = false;
            if (Txt_Code.Text == "")
            {
                MessageBox.Show(" Code can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Code.Focus();
                MeValidate = true;
                return;
            }
            if (Txt_Name.Text == "")
            {
                MessageBox.Show(" Name can't be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Name.Focus();
                MeValidate = true;
                return;
            }
            MeValidate = false;
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
