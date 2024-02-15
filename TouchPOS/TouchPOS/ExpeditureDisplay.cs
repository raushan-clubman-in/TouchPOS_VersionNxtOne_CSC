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
    public partial class ExpeditureDisplay : Form
    {
        GlobalClass GCon = new GlobalClass();

        public ExpeditureDisplay()
        {
            InitializeComponent();
        }

        string sql = "";

        private void KitchenDisplay_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.DoubleBuffered = true;
            int X = 0;
            int Y = 0;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Utility.relocate(this, 1368, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            int pageHeight = 0, pageWidth = 0;
            int panelHeight = 0, panelWidht = 0;
            pageHeight = tabControl1.Height;
            pageWidth = tabControl1.Width;

            panelHeight = (pageHeight / 2) - 20;
            panelWidht = (pageWidth / 4) - 20;

            RefreshExpediture();
            timer1.Interval = 30000;
            timer1.Enabled = true;
            label1.Text = "";

            //tabControl1.TabPages.Clear();
            //tabControl1.Click += new EventHandler(tabControl1_Changed);
           
            //X = 10;
            //Y = 10;
            //int i=0;

            //DataTable KitTable = new DataTable();
            //DataTable PageTable = new DataTable();

            ////sql = "Select DISTINCT I.kitchencode,KH.kitchenName from Kot_Det K INNER JOIN ItemMaster I ON K.ITEMCODE=I.ITEMCODE";
            ////sql = sql + " INNER JOIN kitchenmaster KH ON I.kitchencode = KH.kitchenCode Where   Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' And Isnull(DeliveryStatus,'') = '' ORDER BY kitchenName   ";
            //sql = "select kitchencode,kitchenName from kitchenmaster order by 2";
            //KitTable = GCon.getDataSet(sql);
            //if (KitTable.Rows.Count > 0) 
            //{
            //    foreach (DataRow dr in KitTable.Rows) 
            //    {
            //        TabPage myTabPage = new TabPage();
            //        myTabPage.Text = dr[1].ToString();
            //        myTabPage.Tag = dr[0].ToString();
            //        myTabPage.BackColor = Color.LightBlue;
            //        //Font fnt = new System.Drawing.Font("Microsoft Sans Serif", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        //myTabPage.Font = fnt;
            //        sql = "Select DISTINCT KOTDETAILS,I.kitchencode,KH.kitchenName from Kot_Det K INNER JOIN ItemMaster I ON K.ITEMCODE=I.ITEMCODE";
            //        sql = sql + " INNER JOIN kitchenmaster KH ON I.kitchencode = KH.kitchenCode Where   Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And i.kitchencode = '" + dr[0].ToString() + "'  And Isnull(DelFlag,'') <> 'Y' And Isnull(DeliveryStatus,'') = 'Ready' ORDER BY kitchenName,KOTDETAILS   ";
            //        PageTable = GCon.getDataSet(sql);
            //        if (PageTable.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr1 in PageTable.Rows) 
            //            {
            //                i++;
            //                Panel Pnl = new Panel();
            //                ExpeditureForm objForm = new ExpeditureForm();
            //                objForm.KOrderNo = Convert.ToString(dr1[0].ToString());
            //                objForm.KKitCode = Convert.ToString(dr[0].ToString());
            //                if (i == 5) { Y = panelHeight + 10; X = 10; }
            //                Pnl.Width = panelWidht;
            //                Pnl.Height = panelHeight;
            //                Pnl.BorderStyle = BorderStyle.Fixed3D;
            //                Pnl.BackColor = Color.White;
            //                Pnl.Location = new Point(X, Y);
            //                Pnl.Controls.Clear();
            //                objForm.Dock = DockStyle.Fill;
            //                Pnl.Controls.Add(objForm);
            //                myTabPage.Controls.Add(Pnl);
            //                X = X + (panelWidht + 10);
            //            }
            //        }
            //        tabControl1.TabPages.Add(myTabPage);
            //        i = 0;
            //        X = 10;
            //        Y = 10;
            //    }
            //}

        }

        private void RefreshExpediture() 
        {
            this.DoubleBuffered = true;
            int X = 0;
            int Y = 0;

            int pageHeight = 0, pageWidth = 0;
            int panelHeight = 0, panelWidht = 0;
            pageHeight = tabControl1.Height;
            pageWidth = tabControl1.Width;

            panelHeight = (pageHeight / 2) - 20;
            panelWidht = (pageWidth / 4) - 20;

            tabControl1.TabPages.Clear();
            tabControl1.Click += new EventHandler(tabControl1_Changed);

            X = 10;
            Y = 10;
            int i = 0, P = 0, PageCount = 1, Count = 0;
            //Count = Convert.ToDouble(PageTable.Rows.Count);
            //PageCount = Math.Ceiling(Count/8);

            DataTable KitTable = new DataTable();
            DataTable PageTable = new DataTable();

            sql = "Select DISTINCT KOTDETAILS from Kot_Det Where Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' And Isnull(KotStatus,'') <> 'Y' And Isnull(DeliveryStatus,'') in ('','Ready') and isnull(Billdetails,'') = '' ORDER BY KOTDETAILS";
            PageTable = GCon.getDataSet(sql);
            if (PageTable.Rows.Count > 0) 
            {
                PageCount = 1;
                TabPage myTabPage  = new TabPage();
                for (i = 1; i <= PageTable.Rows.Count; i++) 
                {
                    Count++;
                    if (Count == 1)
                    {
                        myTabPage = new TabPage();
                        myTabPage.Text = "Page " + PageCount;
                        myTabPage.BackColor = Color.LightBlue;
                    }
                    Panel Pnl = new Panel();
                    ExpeditureForm objForm = new ExpeditureForm();
                    objForm.KOrderNo = Convert.ToString(PageTable.Rows[i-1].ItemArray[0].ToString());
                    if (Count == 5) { Y = panelHeight + 10; X = 10; }
                    Pnl.Width = panelWidht;
                    Pnl.Height = panelHeight;
                    Pnl.BorderStyle = BorderStyle.Fixed3D;
                    Pnl.BackColor = Color.White;
                    Pnl.Location = new Point(X, Y);
                    Pnl.Controls.Clear();
                    objForm.Dock = DockStyle.Fill;
                    Pnl.Controls.Add(objForm);
                    myTabPage.Controls.Add(Pnl);
                    X = X + (panelWidht + 10);
                    if (Count == 8)
                    { 
                        tabControl1.TabPages.Add(myTabPage);
                        Count = 0;
                        X = 10;
                        Y = 10;
                        PageCount = PageCount + 1;
                    }
                }
                if (Count > 0) { tabControl1.TabPages.Add(myTabPage); }
                label1.Text = "Expediter " + tabControl1.SelectedTab.Text.ToString();
            }
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            ServiceType ST = new ServiceType();
            ST.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int index = tabControl1.SelectedIndex;
            RefreshExpediture();
            tabControl1.SelectedIndex = index;
            timer1.Enabled = true;
        }

        private void RefreshPage() 
        {
            DataTable KitTable = new DataTable();
            DataTable PageTable = new DataTable();

            string KitCode, KitName;
            int index = tabControl1.SelectedIndex;
            KitName = tabControl1.SelectedTab.Text.ToString();
            KitCode = tabControl1.SelectedTab.Tag.ToString();

            int X = 0;
            int Y = 0;

            int pageHeight = 0, pageWidth = 0;
            int panelHeight = 0, panelWidht = 0;
            pageHeight = tabControl1.Height;
            pageWidth = tabControl1.Width;

            panelHeight = (pageHeight / 2) - 20;
            panelWidht = (pageWidth / 4) - 20;

            X = 10;
            Y = 10;
            int i = 0;

            tabControl1.TabPages.RemoveAt(index);
            TabPage myTabPage = new TabPage();
            myTabPage.Text = KitName;
            myTabPage.Tag = KitCode;
            myTabPage.BackColor = Color.LightBlue;
            tabControl1.TabPages.Insert(index, myTabPage);

            sql = "Select DISTINCT KOTDETAILS,I.kitchencode,KH.kitchenName from Kot_Det K INNER JOIN ItemMaster I ON K.ITEMCODE=I.ITEMCODE";
            sql = sql + " INNER JOIN kitchenmaster KH ON I.kitchencode = KH.kitchenCode Where   Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And i.kitchencode = '" + KitCode + "'  And Isnull(DelFlag,'') <> 'Y' And Isnull(KotStatus,'') <> 'Y' And Isnull(DeliveryStatus,'') = 'Ready' and isnull(Billdetails,'') = '' ORDER BY kitchenName,KOTDETAILS   ";
            PageTable = GCon.getDataSet(sql);
            if (PageTable.Rows.Count > 0)
            {
                foreach (DataRow dr1 in PageTable.Rows)
                {
                    i++;
                    Panel Pnl = new Panel();
                    ExpeditureForm objForm = new ExpeditureForm();
                    objForm.KOrderNo = Convert.ToString(dr1[0].ToString());
                    objForm.KKitCode = KitCode;
                    if (i == 5) { Y = panelHeight + 10; X = 10; }
                    Pnl.Width = panelWidht;
                    Pnl.Height = panelHeight;
                    Pnl.BorderStyle = BorderStyle.Fixed3D;
                    Pnl.BackColor = Color.White;
                    Pnl.Location = new Point(X, Y);
                    Pnl.Controls.Clear();
                    objForm.Dock = DockStyle.Fill;
                    Pnl.Controls.Add(objForm);
                    myTabPage.Controls.Add(Pnl);
                    X = X + (panelWidht + 10);
                }
            }
            tabControl1.SelectedIndex = index;
            label1.Text = KitName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            RefreshExpediture();
            ////int X = 0;
            ////int Y = 0;

            ////int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            ////int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            ////Utility.relocate(this, 1368, 768);
            ////Utility.repositionForm(this, screenWidth, screenHeight);

            ////int pageHeight = 0, pageWidth = 0;
            ////int panelHeight = 0, panelWidht = 0;
            ////pageHeight = tabControl1.Height;
            ////pageWidth = tabControl1.Width;

            ////panelHeight = (pageHeight / 2) - 20;
            ////panelWidht = (pageWidth / 4) - 20;

            ////tabControl1.TabPages.Clear();

            ////X = 10;
            ////Y = 10;
            ////int i = 0;

            ////DataTable KitTable = new DataTable();
            ////DataTable PageTable = new DataTable();
            //////sql = "Select DISTINCT I.kitchencode,KH.kitchenName from Kot_Det K INNER JOIN ItemMaster I ON K.ITEMCODE=I.ITEMCODE";
            //////sql = sql + " INNER JOIN kitchenmaster KH ON I.kitchencode = KH.kitchenCode Where   Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And Isnull(DelFlag,'') <> 'Y' And Isnull(DeliveryStatus,'') = '' ORDER BY kitchenName   ";
            ////sql = "select kitchencode,kitchenName from kitchenmaster order by 2";
            ////KitTable = GCon.getDataSet(sql);
            ////if (KitTable.Rows.Count > 0)
            ////{
            ////    foreach (DataRow dr in KitTable.Rows)
            ////    {
            ////        TabPage myTabPage = new TabPage();
            ////        myTabPage.Text = dr[1].ToString();
            ////        myTabPage.Tag = dr[0].ToString();
            ////        myTabPage.BackColor = Color.LightBlue;
            ////        //Font fnt = new System.Drawing.Font("Microsoft Sans Serif", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ////        //myTabPage.Font = fnt;
            ////        sql = "Select DISTINCT KOTDETAILS,I.kitchencode,KH.kitchenName from Kot_Det K INNER JOIN ItemMaster I ON K.ITEMCODE=I.ITEMCODE";
            ////        sql = sql + " INNER JOIN kitchenmaster KH ON I.kitchencode = KH.kitchenCode Where   Cast(Convert(varchar(11),kotdate,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And i.kitchencode = '" + dr[0].ToString() + "'  And Isnull(DelFlag,'') <> 'Y' And Isnull(DeliveryStatus,'') = 'Ready' ORDER BY kitchenName,KOTDETAILS   ";
            ////        PageTable = GCon.getDataSet(sql);
            ////        if (PageTable.Rows.Count > 0)
            ////        {
            ////            foreach (DataRow dr1 in PageTable.Rows)
            ////            {
            ////                i++;
            ////                Panel Pnl = new Panel();
            ////                ExpeditureForm objForm = new ExpeditureForm();
            ////                objForm.KOrderNo = Convert.ToString(dr1[0].ToString());
            ////                objForm.KKitCode = Convert.ToString(dr[0].ToString());
            ////                if (i == 5) { Y = panelHeight + 10; X = 10; }
            ////                Pnl.Width = panelWidht;
            ////                Pnl.Height = panelHeight;
            ////                Pnl.BorderStyle = BorderStyle.Fixed3D;
            ////                Pnl.BackColor = Color.White;
            ////                Pnl.Location = new Point(X, Y);
            ////                Pnl.Controls.Clear();
            ////                objForm.Dock = DockStyle.Fill;
            ////                Pnl.Controls.Add(objForm);
            ////                myTabPage.Controls.Add(Pnl);
            ////                X = X + (panelWidht + 10);
            ////            }
            ////        }
            ////        tabControl1.TabPages.Add(myTabPage);
            ////        i = 0;
            ////        X = 10;
            ////        Y = 10;
            ////    }
            ////}
            timer1.Start();
            if (tabControl1.TabCount > 0) 
            {
                label1.Text = tabControl1.SelectedTab.Text.ToString();
            }
           
        }

        private void tabControl1_Changed(object sender, EventArgs e) 
        {
            label1.Text = "Expediter " + tabControl1.SelectedTab.Text.ToString();
            //timer1.Enabled = false;
            //RefreshPage();
            //timer1.Enabled = true;
        }
    }
}
