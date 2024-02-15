using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;

namespace TouchPOS
{
    public partial class ExpeditureForm : UserControl
    {
        GlobalClass GCon = new GlobalClass();
        public string KOrderNo;
        public string KKitCode;

        public ExpeditureForm()
        {
            InitializeComponent();
        }

        string sql = "";
        DateTime startTime, endtime;

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "", itemcode = "";
            Boolean Select;
            //sqlstring = " UPDATE Kot_Det SET DeliveryStatus = 'Delivered' WHERE KOTDETAILS = '" + KOrderNo + "' And Itemcode in (select itemcode from itemmaster where kitchencode = '" + KKitCode + "') ";
            //List.Add(sqlstring);
            //if (GCon.Moretransaction(List) > 0)
            //{
            //    List.Clear();
            //    button1.Enabled = false;
            //    button1.Text = "Wait.....";
            //}
            button1.Enabled = false;
            button2.Enabled = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Select = Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value);
                if (Select == true) 
                {
                    if (dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[2].Value != null && dataGridView1.Rows[i].Cells[4].Value != null)
                    {
                        itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                        sqlstring = " UPDATE Kot_Det SET DeliveryStatus = 'Delivered',DeliveryDateTime ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' WHERE KOTDETAILS = '" + KOrderNo + "' And Itemcode = '" + itemcode + "' ";
                        List.Add(sqlstring);
                    }
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                button1.Text = "Wait.....";
            }
        }

        private void ShowOrder_Load(object sender, EventArgs e)
        {
            DataTable KHdr = new DataTable();
            DataTable KDet = new DataTable();
            string Stype = "";
            label1.Text = "KOT No. :" + KOrderNo;
            sql = "select LocName,TableNo,Adddatetime,SerType from kot_hdr where kotdetails = '" + KOrderNo + "'";
            KHdr = GCon.getDataSet(sql);
            if (KHdr.Rows.Count > 0) 
            {
                label3.Text = Convert.ToString(KHdr.Rows[0].ItemArray[0] + "/" + KHdr.Rows[0].ItemArray[1]);
                startTime = Convert.ToDateTime(KHdr.Rows[0].ItemArray[2]);
                label4.Text = Convert.ToString(KHdr.Rows[0].ItemArray[3]); 
            }
            sql = "Select QTY,K.ITEMDESC,MODIFIER,K.ITEMCODE,Isnull(DeliveryStatus,'') as DeliveryStatus from Kot_Det K,ItemMaster I Where K.ITEMCODE=I.ITEMCODE AND KOTDETAILS = '" + KOrderNo + "' And Isnull(KotStatus,'') <> 'Y' And Isnull(DeliveryStatus,'') in ('','Ready') and isnull(Billdetails,'') = '' Order by Isnull(DeliveryStatus,'') Desc,K.ITEMDESC ";
            KDet = GCon.getDataSet(sql);
            if (KDet.Rows.Count > 0) 
            {
                dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[0].Width = 50;
                this.dataGridView1.Columns[3].Width = 20;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                this.dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 10);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                for (int i = 0; i < KDet.Rows.Count; i++) 
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KDet.Rows[i].ItemArray[0]);
                    dataGridView1.Rows[i].Cells[1].Value = KDet.Rows[i].ItemArray[1] + Environment.NewLine + KDet.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[2].Value = KDet.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                    dataGridView1.Rows[i].Cells[4].Value = Convert.ToString(KDet.Rows[i].ItemArray[4]);
                    if (Convert.ToString(KDet.Rows[i].ItemArray[4]) == "") 
                    {
                        dataGridView1.Rows[i].ReadOnly = true;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Maroon;
                    }
                    if (Convert.ToString(KDet.Rows[i].ItemArray[4]) != "")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                }
                dataGridView1.CurrentCell.Selected = false;
            }

            //label4.Text = "";
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int totmin;
            endtime = Convert.ToDateTime(DateTime.Now);
            TimeSpan duration = endtime -startTime ;
            label2.Text = duration.ToString(@"hh\:mm\:ss");
            totmin = duration.Hours * 60;
            totmin = totmin + duration.Minutes;
            //if (totmin >= 30) 
            //{
            //    tableLayoutPanel1.BackColor = Color.Orange;
            //    //if (tableLayoutPanel1.BackColor == Color.Blue) 
            //    //{
            //    //    tableLayoutPanel1.BackColor = Color.Orange;
            //    //}
            //    //else if (tableLayoutPanel1.BackColor == Color.Orange)
            //    //{
            //    //    tableLayoutPanel1.BackColor = Color.Blue;
            //    //}
            //}
            if (totmin >= 30)
            {
                tableLayoutPanel1.BackColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "", itemcode = "";
            Boolean Select;
            button1.Enabled = false;
            button2.Enabled = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Select = Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value);
                if (Select == true)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[2].Value != null && dataGridView1.Rows[i].Cells[4].Value != null)
                    {
                        itemcode = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                        sqlstring = " UPDATE Kot_Det SET DeliveryStatus = '',BUMPDateTime = '' WHERE KOTDETAILS = '" + KOrderNo + "' And Itemcode = '" + itemcode + "' ";
                        List.Add(sqlstring);
                    }
                }
            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
                button2.Text = "Wait.....";
            }
        }

       
    }
}
