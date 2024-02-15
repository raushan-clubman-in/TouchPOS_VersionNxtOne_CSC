using System;
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
    public partial class ServiceLocationDisplay : Form
    {
        GlobalClass GCon = new GlobalClass();
        public bool AddChairFlag = false;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());

        public ServiceLocationDisplay()
        {
            InitializeComponent();
        }

        string sql = "";

        private void ServiceLocationDisplay_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1366, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            label1.Text = "The Calcutta Swimming Club";
            FillLoacation();
        }

        private void Cmd_BPOS_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Close();
        }

        private void FillLoacation() 
        {
            string TabName = "KOT" + GlobalVariable.gUserName;
            int intX = Screen.PrimaryScreen.Bounds.Width;
            int intY = Screen.PrimaryScreen.Bounds.Height;
            int X = 0;
            int Y = 0;
            DataTable Locdt = new DataTable();
            DataTable Btndt = new DataTable();
            tabControl1.TabPages.Clear();
            this.tabControl1.Padding = new System.Drawing.Point(10, 10);
            TabPage myTabPage = new TabPage();
            myTabPage.Text = "Service Location";
            Font fnt = new System.Drawing.Font("Microsoft Sans Serif", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myTabPage.Font = fnt;
            myTabPage.BackColor = Color.Gainsboro;
            myTabPage.BorderStyle = BorderStyle.Fixed3D;
            myTabPage.AutoScroll = true;
            X = 10;
            Y = 10;
            if (GlobalVariable.gUserCategory == "S")
            {
                sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            }
            else
            {
                sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' And LocCode in (Select Loccode from Tbl_LocationUserTag Where UserName = '" + GlobalVariable.gUserName + "') ";
            }
            Btndt = GCon.getDataSet(sql);
            if (Btndt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in Btndt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr1[0].ToString();
                    btn.Tag = dr1[3].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.DarkSlateBlue;
                    btn.ImageAlign = ContentAlignment.MiddleRight;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 150;
                    btn.Height = 80;
                    //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btn.Width = 110;
                    //btn.Height = 50;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btn.Dock = DockStyle.Left;
                    btn.Location = new Point(X, Y);
                    myTabPage.Controls.Add(btn);
                    btn.Click += new EventHandler(button2_Click);
                    if (X > intX - 450) 
                    //if (X > intX - 300)
                    {
                        X = 10;
                        Y = Y + 90;
                        //X = 10;
                        //Y = Y + 60;
                    }
                    else
                    {
                        X = X + 180;
                        //X = X + 140;
                    }
                }
            }
            tabControl1.TabPages.Add(myTabPage);
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            Button selectedBtn = sender as Button;
            GlobalVariable.SLocation = selectedBtn.Text.ToString();
            GlobalVariable.DisLocCode = Int32.Parse(selectedBtn.Tag.ToString());
            ServiceLocation SL = new ServiceLocation();
            SL.Show();
            this.Close();
        }
    }
}
