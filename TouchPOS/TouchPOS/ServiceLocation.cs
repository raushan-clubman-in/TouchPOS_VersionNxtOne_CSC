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
    public partial class ServiceLocation : Form
    {
        GlobalClass GCon = new GlobalClass();
        public bool AddChairFlag = false;
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        

        public ServiceLocation()
        {
            InitializeComponent();
        }
        string sql = "";
        ToolTip Tbtn = new ToolTip();

        private void ServiceLocation_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1366, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);
            label1.Text = GlobalVariable.gUserName;
            label2.Text = GlobalVariable.ServiceType;
            FillLoacation();
            if (GlobalVariable.ServiceType == "Dine-In")
            {
                Cmd_AddChair.Enabled = true;
                Cmd_MergeTransfer.Enabled = true;
                Cmd_Transfer.Enabled = true;
            }
            else 
            {
                Cmd_AddChair.Enabled = false;
                Cmd_MergeTransfer.Enabled = false;
                Cmd_Transfer.Enabled = false;
            }

            if (GlobalVariable.LocTabIndex > 0) 
            {
                tabControl1.SelectedIndex = GlobalVariable.LocTabIndex;
            }
            GCon.GetBillCloseDate();
            Lbl_BusinessDate.Text = GlobalVariable.ServerDate.ToString("dd-MMM-yyyy");
            //if (GlobalVariable.gUserName == "5") { Cmd_Split.Visible = true; }
            //Cmd_Split.Visible = true;
            GC.Collect();
        }

        private void FillLoacation() 
        {
            string TabName = "KOT"+GlobalVariable.gUserName;
            int intX = Screen.PrimaryScreen.Bounds.Width;
            int intY = Screen.PrimaryScreen.Bounds.Height;
            int X = 0;
            int Y = 0;
            DataTable Locdt = new DataTable();
            DataTable Btndt = new DataTable();
            if (GlobalVariable.gUserCategory == "S")
            {
                sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            }
            else 
            {
                sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' And LocCode in (Select Loccode from Tbl_LocationUserTag Where UserName = '" + GlobalVariable.gUserName + "') ";
            }
            if (GlobalVariable.gCompName == "CSC" && GlobalVariable.DisLocCode != 0) 
            {
                if (GlobalVariable.gUserCategory == "S")
                {
                    sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' And LocCode = " + GlobalVariable.DisLocCode + " ";
                }
                else
                {
                    sql = "SELECT LocName,'-7278960' BkColor,0 AS GrandTotal,LocCode,TableBillingYn FROM ServiceLocation_Hdr WHERE ISNULL(Void,'') <> 'Y' AND Isnull(ServiceFlag,'') = 'D' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' And LocCode in (Select Loccode from Tbl_LocationUserTag Where UserName = '" + GlobalVariable.gUserName + "') And LocCode = " + GlobalVariable.DisLocCode + " ";
                }
            }
            Locdt = GCon.getDataSet(sql);
            if (Locdt.Rows.Count > 0) 
            {
                tabControl1.TabPages.Clear();
                this.tabControl1.Padding = new System.Drawing.Point(10, 10);

                sql = "IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + TabName + "' ) Begin DROP TABLE " + TabName + " End SELECT * INTO " + TabName + " FROM KOT_DET WHERE cast(convert(varchar(11),KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                GCon.dataOperation(1, sql);

                foreach (DataRow dr in Locdt.Rows)
                {
                    X = 10;
                    Y = 10;
                    // do something with dr
                    TabPage myTabPage = new TabPage();
                    myTabPage.Text = dr[0].ToString();
                    myTabPage.Tag = dr[3].ToString();
                    Font fnt = new System.Drawing.Font("Microsoft Sans Serif", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    myTabPage.Font = fnt;
                    //myTabPage.BackColor = Color.White;
                    myTabPage.BackColor = Color.Gainsboro;
                    myTabPage.BorderStyle = BorderStyle.Fixed3D;
                    myTabPage.AutoScroll = true;
                    
                    if (dr[4].ToString() == "N")
                    {
                        //sql = "select t.TableNo,'-7270000' BkColor,0 AS GrandTotal,t.TableNo from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and k.KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo";
                        ////sql = "select t.TableNo,'-7270000' BkColor,0 AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and k.KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear  WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,T.AutoId ORDER BY T.AutoId";
                        ////sql = "select t.TableNo,'-7270000' BkColor,0 AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and k.KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear  WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,isnull(T.TableOrder,0) ORDER BY isnull(T.TableOrder,0)";
                        sql = "select t.TableNo,'-7270000' BkColor,0 AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  " + TabName + " k on t.TableNo = k.TABLENO and k.KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear  WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,isnull(T.TableOrder,0) ORDER BY isnull(T.TableOrder,0)";
                    }
                    else 
                    {
                        //sql = "select t.TableNo,'-7270000' BkColor,sum(isnull(AMOUNT,0)+isnull(TAXAMOUNT,0)) AS GrandTotal,t.TableNo from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and cast(convert(varchar(11),k.KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo";
                        //sql = "select t.TableNo,'-7270000' BkColor,sum(isnull(AMOUNT,0)+isnull(TAXAMOUNT,0)) AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and cast(convert(varchar(11),k.KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join Tbl_CheckPrint C on c.KotNo = k.KOTDETAILS  WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,T.AutoId ORDER BY T.AutoId";
                        ////sql = "select t.TableNo,'-7270000' BkColor,Round(sum(isnull(AMOUNT,0)+isnull(TAXAMOUNT,0)+isnull(packamount,0)+isnull(TIPSAMT,0)),0) AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and cast(convert(varchar(11),k.KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear   WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,T.AutoId ORDER BY T.AutoId";
                        ////sql = "select t.TableNo,'-7270000' BkColor,Round(sum(isnull(AMOUNT,0)+isnull(TAXAMOUNT,0)+isnull(packamount,0)+isnull(TIPSAMT,0)),0) AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  kot_det k on t.TableNo = k.TABLENO and cast(convert(varchar(11),k.KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear   WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,isnull(T.TableOrder,0) ORDER BY isnull(T.TableOrder,0)";
                        sql = "select t.TableNo,'-7270000' BkColor,Round(sum(isnull(AMOUNT,0)+isnull(TAXAMOUNT,0)+isnull(packamount,0)+isnull(TIPSAMT,0)),0) AS GrandTotal,t.TableNo,isnull(c.KotNo,'') as Checkprint from tablemaster t left outer join  " + TabName + " k on t.TableNo = k.TABLENO and cast(convert(varchar(11),k.KOTDATE,106) as datetime) = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' and isnull(k.billdetails,'') = '' and isnull(kotstatus,'') <> 'Y' and isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' left outer join (Select distinct kotno,finyear from Tbl_CheckPrint) C on c.KotNo = k.KOTDETAILS and c.FinYear = k.FinYear   WHERE Pos = '" + dr[3].ToString() + "' AND ISNULL(FREEZE,'') <> 'Y' group by t.TableNo,c.KotNo,isnull(T.TableOrder,0) ORDER BY isnull(T.TableOrder,0)";
                    }
                    Btndt = GCon.getDataSet(sql);
                    if (Btndt.Rows.Count > 0) 
                    {
                        foreach (DataRow dr1 in Btndt.Rows) 
                        {
                            Button btn = new Button();
                            btn.Text = dr1[0].ToString() + Environment.NewLine + dr1[2].ToString();
                            btn.Tag = dr1[3].ToString();
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                            if (Convert.ToDouble(dr1[2]) > 0)
                            {
                                //btn.Image = Image.FromFile(@"twopersonattablered.jpg");
                                if (Convert.ToString(dr1[4]) != "") 
                                {
                                    btn.BackgroundImage = Image.FromFile(@"twopersonattableyellow.jpg");
                                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                                    btn.BackColor = Color.Red;
                                }
                                else 
                                {
                                    btn.BackgroundImage = Image.FromFile(@"twopersonattablered.jpg");
                                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                                    btn.BackColor = Color.Red;
                                }
                            }
                            else
                            {
                                //btn.Image = Image.FromFile(@"twopersonattableblue.jpg");
                                btn.BackgroundImage = Image.FromFile(@"twopersonattableblue.jpg");
                                btn.BackgroundImageLayout = ImageLayout.Stretch;
                                //btn.BackColor = Color.LightGreen;
                                btn.BackColor = Color.DarkSlateBlue;
                            }
                            //btn.Image = Image.FromFile(@"twopersonattable.PNG");
                            btn.ImageAlign = ContentAlignment.MiddleRight;
                            btn.TextAlign = ContentAlignment.MiddleLeft; 
                            btn.ForeColor = Color.White;
                            btn.FlatStyle = FlatStyle.Flat;
                            //btn.Width = 150;
                            //btn.Height = 80;
                            //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            btn.Width = 110;
                            btn.Height = 50;
                            btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //btn.Dock = DockStyle.Left;
                            btn.Location = new Point(X, Y);
                            myTabPage.Controls.Add(btn);
                            btn.Click += new EventHandler(button2_Click);
                            btn.MouseHover += new EventHandler(button2_MouseHover);
                            //if (X > intX - 450) 
                            if (X > intX - 300) 
                            {
                                //X = 10;
                                //Y = Y + 90;
                                X = 10;
                                Y = Y + 60;
                            }
                            else
                            {
                                 //X = X + 180;
                                X = X + 140;
                            }
                        }
                    }
                    tabControl1.TabPages.Add(myTabPage);
                }
            }
        }


        private void button2_MouseHover(object sender, EventArgs e)
        {
            DataTable ChkChair = new DataTable();
            int ChNo = 1;
            GlobalVariable.LocTabIndex = tabControl1.SelectedIndex;
            Button selectedBtn = sender as Button;
            GlobalVariable.SLocation = selectedBtn.Parent.Text.ToString();
            GlobalVariable.TableNo = selectedBtn.Tag.ToString();
            string Remarks = Convert.ToString(GCon.getValue("SELECT Top 1 isnull(mcode,'') FROM KOT_HDR WHERE LocCode = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + " AND TableNo = '" + selectedBtn.Tag.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
           
            if (Remarks != "")
            {
                Tbtn.Show(Remarks, selectedBtn);
            }
        }


        private void FindControl() 
        {
            TabPage selectedTab = tabControl1.SelectedTab;
            TabControl selectedRtb = selectedTab.Controls.Find(textBox1.Text, true).First() as TabControl;
        }

        private void Cmd_BPOS_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.gCompName == "CSC") 
            {
                ServiceLocationDisplay ST = new ServiceLocationDisplay();
                ST.Show();
            }
            else 
            {
                ServiceType ST = new ServiceType();
                ST.Show();
            }
            AddChairFlag = false;
            //this.Hide();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            string TableOpenStatus = "";
            GlobalVariable.LocTabIndex = tabControl1.SelectedIndex;
            if (AddChairFlag == true) 
            {
                Button selectedBtn = sender as Button;
                GlobalVariable.SLocation = selectedBtn.Parent.Text.ToString();
                GlobalVariable.TableNo = selectedBtn.Tag.ToString();
                TableOpenStatus = Convert.ToString(GCon.getValue("SELECT ISNULL(OpenStatus,'') FROM TableMaster WHERE Pos = '" + selectedBtn.Parent.Tag.ToString() + "' AND TableNo = '" + selectedBtn.Tag.ToString() + "'"));
                if (TableOpenStatus == "O") { return; }

                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + " AND TableNo = '" + selectedBtn.Tag.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    AddChairTable ACT = new AddChairTable(this);
                    ACT.TableNumber = selectedBtn.Tag.ToString();
                    ACT.loccode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                    ACT.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Selected Table Add Chair Not Allowed Because Chair Already Vacant", GlobalVariable.gCompanyName);
                    AddChairFlag = false;
                }
            }
            else 
            {
                Button selectedBtn = sender as Button;
                GlobalVariable.SLocation = selectedBtn.Parent.Text.ToString();
                GlobalVariable.TableNo = selectedBtn.Tag.ToString();
                string TableBilling = Convert.ToString(GCon.getValue("SELECT ISNULL(TableBillingYn,'N') FROM ServiceLocation_Hdr WHERE LOCCODE = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + ""));
                TableOpenStatus = Convert.ToString(GCon.getValue("SELECT ISNULL(OpenStatus,'') FROM TableMaster WHERE Pos = '" + selectedBtn.Parent.Tag.ToString() + "' AND TableNo = '" + selectedBtn.Tag.ToString() + "'"));

                if (TableOpenStatus == "O") { return; }

                int RowCnt1 = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + " AND TableNo = '" + selectedBtn.Tag.ToString() + "' AND BILLSTATUS = 'PO'  and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt1 > 1)
                {
                    SelectChairTable SCT = new SelectChairTable(this);
                    SCT.TableNumber = selectedBtn.Tag.ToString();
                    SCT.loccode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                    SCT.ShowDialog();
                }
                else 
                {
                    if (TableBilling == "Y") 
                    {
                        DataTable ChkChair = new DataTable();
                        int ChNo = 1;
                        EntryForm EF = new EntryForm();

                        //GlobalVariable.LCode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                        EF.Loccode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                        AddChairFlag = false;
                        sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + " AND TableNo = '" + selectedBtn.Tag.ToString() + "' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                        ChkChair = GCon.getDataSet(sql);
                        if (ChkChair.Rows.Count > 0)
                        {
                            ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                        }
                        else { ChNo = 1; }

                        int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Int32.Parse(selectedBtn.Parent.Tag.ToString()) + " AND TableNo = '" + selectedBtn.Tag.ToString() + "' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                        if (RowCnt > 0)
                        {
                            EF.UpdFlag = true;
                            GlobalVariable.ChairNo = ChNo;
                        }
                        else
                        {
                            EF.UpdFlag = false;
                            GlobalVariable.ChairNo = ChNo;
                            PaxForm PF = new PaxForm(this);
                            PF.ShowDialog();
                            EF.Pax = PF.SPax;
                            if (PF.CancelFlag == true) { return; }
                            if (GlobalVariable.EntryType.ToUpper() == "MEMBER" || GlobalVariable.EntryType.ToUpper() == "BOTH")
                            {
                                MemValidate MV = new MemValidate(this);
                                MV.LocCode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                                MV.ShowDialog();
                                if (MV.MCode == "" && MV.MemType == "M") { return; }
                                if (MV.CancelFlag == true) { return; }
                                EF.MemberCode = MV.MCode;
                                EF.MemberName = MV.MName;
                                EF.CardHolderCode = MV.CardCode;
                                EF.CardHolderName = MV.CardName;
                                EF.DigitCode = MV.DCode;
                                EF.GuestMobno = MV.GMobNo;
                                EF.GuestName = MV.GName;
                            }
                        }
                        EF.Show();
                        //this.Hide();
                        this.Close();
                    }
                    else if (TableBilling == "N") 
                    {
                        DataTable ChkChair = new DataTable();
                        int ChNo = 1;
                        EntryForm EF = new EntryForm();
                        EF.Loccode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                        AddChairFlag = false;
                        EF.UpdFlag = false;
                        GlobalVariable.ChairNo = ChNo;
                        EF.Pax = 0;
                        if (GlobalVariable.EntryType.ToUpper() == "MEMBER" || GlobalVariable.EntryType.ToUpper() == "BOTH")
                        {
                            MemValidate MV = new MemValidate(this);
                            MV.LocCode = Int32.Parse(selectedBtn.Parent.Tag.ToString());
                            MV.ShowDialog();
                            if (MV.MCode == "" && MV.MemType == "M") { return; }
                            if (MV.CancelFlag == true) { return; }
                            EF.MemberCode = MV.MCode;
                            EF.MemberName = MV.MName;
                            EF.CardHolderCode = MV.CardCode;
                            EF.CardHolderName = MV.CardName;
                            EF.DigitCode = MV.DCode;
                            EF.TabBill = "N";
                        }
                        EF.Show();
                        //this.Hide();
                        this.Close();
                    }
                }
            }
        }


        private void Cmd_AddChair_Click(object sender, EventArgs e)
        {
            AddChairFlag = true;
        }

        public void AddChairEntry(string TabNo, int ChNo,int lcode) 
        {
            EntryForm EF = new EntryForm();
            GlobalVariable.ChairNo = ChNo;
            EF.Loccode = lcode;
            int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + lcode + " AND TableNo = '" + TabNo + "' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + GlobalVariable.ChairNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (RowCnt > 0)
            {
                EF.UpdFlag = true;
            }
            else
            {
                EF.UpdFlag = false;
                PaxForm PF = new PaxForm(this);
                PF.ShowDialog();
                EF.Pax = PF.SPax;
                if (PF.CancelFlag == true) { return; }
                if (GlobalVariable.EntryType.ToUpper() == "MEMBER" || GlobalVariable.EntryType.ToUpper() == "BOTH")
                {
                    MemValidate MV = new MemValidate(this);
                    MV.LocCode = lcode;
                    MV.ShowDialog();
                    if (MV.MCode == "" && MV.MemType == "M") { return; }
                    if (MV.CancelFlag == true) { return; }
                    EF.MemberCode = MV.MCode;
                    EF.MemberName = MV.MName;
                    EF.CardHolderCode = MV.CardCode;
                    EF.CardHolderName = MV.CardName;
                    EF.DigitCode = MV.DCode;
                }
            }
            EF.Show();
            //this.Hide();
            this.Close();
        }

        private void Cmd_MergeTransfer_Click(object sender, EventArgs e)
        {
            MergeTable MT = new MergeTable();
            MT.LocationCode = Convert.ToInt32(tabControl1.SelectedTab.Tag.ToString());
            MT.Show();
            //this.Hide();
            this.Close();
        }

        private void Cmd_Transfer_Click(object sender, EventArgs e)
        {
            TransferTable TT = new TransferTable();
            TT.LocationCode =Convert.ToInt32(tabControl1.SelectedTab.Tag.ToString());
            TT.Show();
            //this.Hide();
            this.Close();
        }

        private void Cmd_Split_Click(object sender, EventArgs e)
        {
            SplitTableItem STI = new SplitTableItem(this);
            STI.LocationCode = Convert.ToInt32(tabControl1.SelectedTab.Tag.ToString());
            STI.ShowDialog();
        }

        private void Cmd_PreBalCheck_Click(object sender, EventArgs e)
        {
            PrepaidBalanceCheck PBC = new PrepaidBalanceCheck();
            PBC.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            FindControl();
        }

        ////private void PrintOperation(string Bno)
        ////{
        ////    int rowj = 0;
        ////    int CountItem = 0;
        ////    long Vrowcount = 0;
        ////    string vFilepath = null;
        ////    string vOutfile = null;
        ////    DataTable PData = new DataTable();
        ////    DataTable TData = new DataTable();
        ////    DataTable SData = new DataTable();
        ////    StreamWriter Filewrite = default(StreamWriter);
        ////    Double Total = 0, BillTotal = 0, TaxTotal = 0, OthTotal = 0, MFTotal = 0, DiscAmount = 0;
        ////    Double DisPercent = 0;
        ////    Double ExtraTips = 0, RefundAmt = 0;

        ////    VBMath.Randomize();
        ////    vOutfile = Strings.Mid("BIL" + (VBMath.Rnd() * 800000), 1, 8);
        ////    vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
        ////    vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

        ////    const string ESC1 = "\u001B";
        ////    const string BoldOn = ESC1 + "E" + "\u0001";
        ////    const string BoldOff = ESC1 + "E" + "\0";

        ////    //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
        ////    string Add1 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string Add2 = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(ADD2,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string City = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(CITY,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string PinNo = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Pincode,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string GSTIN = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(GSTINNO,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string Phone = Convert.ToString(GCon.getValue(" SELECT TOP 1 ISNULL(Phone1,'') FROM MASTER..CLUBMASTER WHERE DATAFILE IN (SELECT DB_NAME())"));
        ////    string SecLine = Add2 + ", " + City + "-" + PinNo;
        ////    string ItemHSN = "";

        ////    sql = "SELECT b.BillDetails,D.KOTDETAILS,D.Kotdate,B.Billdate,B.BillTime,b.Adddatetime,b.Adduserid,b.LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,(isnull(d.packamount,0)+isnull(d.TipsAmt,0)+isnull(d.AdCgsAmt,0)+isnull(d.PartyAmt,0)+isnull(d.RoomAmt,0)) as OthAmount,(isnull(d.ModifierCharges,0)) as MFAmount,Isnull(ItemDiscPerc,0) as ItemDiscPerc,H.STWCODE,H.STWNAME,(select isnull(HSNNO,'NA') from itemmaster I Where I.ItemCode = D.ITEMCODE) AS HSNNO ";
        ////    sql = sql + " FROM KOT_DET D,KOT_HDR H,BILL_HDR b WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND D.BILLDETAILS = b.BillDetails AND ISNULL(D.FinYear,'') = ISNULL(B.FinYear,'')  AND B.BillDetails = '" + Bno + "'  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(B.FinYear,'') = '" + FinYear1 + "' ORDER BY HSNNO ";
        ////    PData = GCon.getDataSet(sql);
        ////    if (PData.Rows.Count > 0)
        ////    {
        ////        ////DisPercent = Convert.ToDouble(GCon.getValue(" SELECT Isnull(DiscPercent,0) as DiscPercent From Bill_Hdr Where Billdetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
        ////        Filewrite = File.AppendText(vFilepath);
        ////        for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
        ////        {
        ////            CountItem = CountItem + 1;
        ////            var RData = PData.Rows[rowj];
        ////            if (Vrowcount == 0)
        ////            {
        ////                ////Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - GlobalVariable.gCompanyName.Length) / 2) + (char)27 + (char)14 + GlobalVariable.gCompanyName + (char)27 + (char)18);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - GlobalVariable.gCompanyName.Length) / 2) + BoldOn + GlobalVariable.gCompanyName + BoldOff);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - Add1.Length) / 2) + Add1);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - SecLine.Length) / 2) + SecLine);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("GSTIN:-" + GSTIN).ToString().Length) / 2) + "GSTIN:-" + GSTIN);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - ("TEL NO:" + Phone).ToString().Length) / 2) + "TEL NO:" + Phone);
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "TAX INVOICE".Length) / 2) + "TAX INVOICE");
        ////                string NCFlag = Convert.ToString(GCon.getValue("SELECT ISNULL(NCFlag,'N') FROM KOT_HDR WHERE Kotdetails IN (SELECT DISTINCT Kotdetails FROM kot_det WHERE BILLDETAILS = '" + Bno + "' And FinYear = '" + FinYear1 + "') And FinYear = '" + FinYear1 + "'"));
        ////                if (NCFlag == "Y") { Filewrite.WriteLine(Strings.Space(4) + Strings.Space((41 - "NC".Length) / 2) + "NC"); }
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////                Filewrite.WriteLine(Strings.Space(4) + "CREW : " + RData["Adduserid"] + " STEWARD :" + RData["STWNAME"]);
        ////                Filewrite.WriteLine(Strings.Space(4) + "LOC :" + RData["LOCNAME"] + "/" + RData["TABLENO"] + " PAX:" + RData["Covers"]);
        ////                Filewrite.WriteLine(Strings.Space(4) + "INV NO:" + RData["BillDetails"] + "    ORD NO:" + RData["OrderNo"]);
        ////                Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Billdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["BillTime"], "T")), 1, 10));
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////                //Filewrite.WriteLine();
        ////                Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "HSN", "", "", "", "");
        ////                Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", "QTY", "", "ITEM", "RATE", "AMOUNT");
        ////                //Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}", "", "", "", "ITEM", "AMOUNT");
        ////                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////                Vrowcount = 16;
        ////            }
        ////            if (ItemHSN != RData["HSNNO"].ToString())
        ////            {
        ////                Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Mid(RData["HSNNO"].ToString(), 1, 5), "", "", "", "");
        ////                Vrowcount = Vrowcount + 1;
        ////                ItemHSN = RData["HSNNO"].ToString();
        ////            }
        ////            Filewrite.WriteLine("{0,-4}{1,5}{2,-2}{3,-18}{4,8}{5,8}", "", Strings.Format(RData["QTY"], "0"), "", Strings.Mid(RData["ITEMDESC"].ToString(), 1, 18), Strings.Format(RData["RATE"], "0.00"), Strings.Format(RData["AMOUNT"], "0.00"));
        ////            Vrowcount = Vrowcount + 1;
        ////            DisPercent = Convert.ToDouble(RData["ItemDiscPerc"]);
        ////            Total = Total + Convert.ToDouble(RData["AMOUNT"]);
        ////            DiscAmount = DiscAmount + ((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100);
        ////            if (DisPercent > 0)
        ////            {
        ////                Filewrite.WriteLine("{0,-4}{1,7}{2,-26}", "", "", "DISC " + DisPercent.ToString() + "%  " + Strings.Format(((Convert.ToDouble(RData["AMOUNT"]) * DisPercent) / 100), "0.00"));
        ////                Vrowcount = Vrowcount + 1;
        ////            }
        ////            //OthTotal = OthTotal + Convert.ToDouble(RData["OthAmount"]);
        ////            //MFTotal = MFTotal + Convert.ToDouble(RData["MFAmount"]);
        ////            OthTotal = OthTotal + (Convert.ToDouble(RData["OthAmount"]) - ((Convert.ToDouble(RData["OthAmount"]) * DisPercent) / 100));
        ////            MFTotal = MFTotal + (Convert.ToDouble(RData["MFAmount"]) - ((Convert.ToDouble(RData["MFAmount"]) * DisPercent) / 100));
        ////        }
        ////        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "GROSS AMT:", Strings.Format(Total, "0.00"));
        ////        if (DiscAmount > 0)
        ////        {
        ////            //Filewrite.WriteLine("{0,-4}{1,25}{2,8}", "", "DISC AMT @ " + DisPercent + "%:", Strings.Format((Total * DisPercent) / 100, "0.00"));
        ////            //Total = Total - ((Total * DisPercent) / 100);
        ////            Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "DISC AMT:", Strings.Format(DiscAmount, "0.00"));
        ////            Total = Total - DiscAmount;
        ////        }
        ////        if (MFTotal > 0)
        ////        {
        ////            Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Modifier CHG:", Strings.Format(MFTotal, "0.00"));
        ////            //MFTotal = MFTotal - ((MFTotal * DisPercent) / 100);
        ////        }
        ////        if (OthTotal > 0)
        ////        {
        ////            Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "OTH CHG:", Strings.Format(OthTotal, "0.00"));
        ////            //OthTotal = OthTotal - ((OthTotal * DisPercent) / 100);
        ////        }
        ////        sql = "SELECT A.taxdesc,SUM(T.TAXAMT) - (sum(((T.TAXAMT * Isnull(ItemDiscPerc,0)) /100 ))) AS TAMOUNT FROM KOT_DET_TAX T,KOT_DET D,accountstaxmaster A WHERE ISNULL(T.KOTDETAILS,'') = ISNULL(D.KOTDETAILS,'') AND ISNULL(T.ITEMCODE,'') = ISNULL(D.ITEMCODE,'') AND ISNULL(T.SLNO,0) = ISNULL(D.SLNO,0) AND ISNULL(T.FinYear,'') = ISNULL(D.FinYear,'') ";
        ////        sql = sql + " AND ISNULL(T.TAXCODE,'') = ISNULL(A.taxcode,0) AND D.BILLDETAILS = '" + Bno + "' AND ISNULL(D.FinYear,'') = '" + FinYear1 + "' AND ISNULL(D.KOTSTATUS,'') <> 'Y' GROUP BY A.taxdesc ";
        ////        TData = GCon.getDataSet(sql);
        ////        if (TData.Rows.Count > 0)
        ////        {
        ////            for (int i = 0; i <= TData.Rows.Count - 1; i++)
        ////            {
        ////                var RData = TData.Rows[i];
        ////                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["taxdesc"] + ":", Strings.Format(RData["TAMOUNT"], "0.00"));
        ////                TaxTotal = TaxTotal + Convert.ToDouble(RData["TAMOUNT"]);
        ////            }
        ////        }
        ////        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "NET AMT:", Strings.Format(Total + TaxTotal + OthTotal + MFTotal, "0.00"));
        ////        Double Rnd = Math.Round(Total + TaxTotal + OthTotal + MFTotal) - (Total + TaxTotal + OthTotal + MFTotal);
        ////        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "Round off:", Strings.Format(Rnd, "0.00"));
        ////        BillTotal = Total + TaxTotal + OthTotal + MFTotal + Rnd;
        ////        Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TOTAL AMT:", Strings.Format(BillTotal, "0.00"));

        ////        sql = " SELECT PAYMENTMODE,PAYAMOUNT FROM BILLSETTLEMENT WHERE BILLNO = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY AUTOID ";
        ////        SData = GCon.getDataSet(sql);
        ////        if (SData.Rows.Count > 0)
        ////        {
        ////            for (int i = 0; i <= SData.Rows.Count - 1; i++)
        ////            {
        ////                var RData = SData.Rows[i];
        ////                Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", RData["PAYMENTMODE"] + ":", Strings.Format(RData["PAYAMOUNT"], "0.00"));
        ////            }
        ////        }

        ////        ExtraTips = Convert.ToDouble(GCon.getValue(" Select Isnull(ExtraTips,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
        ////        RefundAmt = Convert.ToDouble(GCon.getValue(" Select Isnull(RefundAmt,0) from BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
        ////        if (ExtraTips > 0)
        ////        {
        ////            Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "TIPS:", Strings.Format(ExtraTips, "0.00"));
        ////        }
        ////        if (RefundAmt > 0)
        ////        {
        ////            Filewrite.WriteLine("{0,-4}{1,33}{2,8}", "", "REFUND:", Strings.Format(RefundAmt, "0.00"));
        ////        }

        ////        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////        DataTable CData = new DataTable();
        ////        DataTable MData = new DataTable();
        ////        DataTable ARMData = new DataTable();
        ////        DataTable RoomData = new DataTable();
        ////        sql = "SELECT MCODE,MNAME,CURENTSTATUS FROM MEMBERMASTER Where MCode IN (SELECT MCODE FROM BILL_HDR WHERE BILLDETAILS = '" + Bno + "') ";
        ////        MData = GCon.getDataSet(sql);
        ////        if (MData.Rows.Count > 0)
        ////        {
        ////            var RData1 = MData.Rows[0];
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "MCODE: " + RData1["MCODE"]);
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "MNAME: " + RData1["MNAME"]);
        ////        }
        ////        else
        ////        {
        ////            sql = "SELECT ARCode,ARName FROM Tbl_ARFlagUpdation Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
        ////            ARMData = GCon.getDataSet(sql);
        ////            if (ARMData.Rows.Count > 0)
        ////            {
        ////                var RData1 = ARMData.Rows[0];
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Code: " + RData1["ARCode"]);
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "AR Name: " + RData1["ARName"]);
        ////            }
        ////            else
        ////            {
        ////                sql = " SELECT * FROM Tbl_HomeTakeAwayBill Where KotNo in (select KOTDETAILS from KOT_det where BILLDETAILS = '" + Bno + "') ";
        ////                CData = GCon.getDataSet(sql);
        ////                if (CData.Rows.Count > 0)
        ////                {
        ////                    var RData1 = CData.Rows[0];
        ////                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "Customer Info");
        ////                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
        ////                    Filewrite.WriteLine("{0,-4}{1,-41}", "", RData1["GuestName"]);
        ////                    Filewrite.WriteLine("{0,-4}{1,-33}", "", "GSTIN: " + RData1["GuestGSTIN"]);
        ////                    Filewrite.WriteLine("{0,-4}{1,-41}", "", "ADD: " + RData1["GuestAdd"]);
        ////                    Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(41, "-"));
        ////                }
        ////            }
        ////        }
        ////        sql = "SELECT TOP 1 ChkNo,R.RoomNo,ISNULL(First_name,'') + ' ' + ISNULL(Middlename,'') as Mname FROM RoomCheckin R,kot_hdr H,kot_det D where H.Kotdetails = D.KOTDETAILS and R.ChkNo = H.Checkin and D.BILLDETAILS = '" + Bno + "' ";
        ////        RoomData = GCon.getDataSet(sql);
        ////        if (RoomData.Rows.Count > 0)
        ////        {
        ////            var RData1 = RoomData.Rows[0];
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Info");
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "-------------");
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Guest Name: " + RData1["Mname"]);
        ////            Filewrite.WriteLine("{0,-4}{1,-41}", "", "Room No   : " + RData1["RoomNo"] + "  [" + RData1["ChkNo"] + "]");
        ////        }
        ////        Filewrite.WriteLine();
        ////        DataTable Remark = new DataTable();
        ////        sql = "SELECT ISNULL(REMARKS,'') AS REMARKS,ISNULL(NCRemarks,'') AS NCRemarks  FROM BILL_HDR WHERE BillDetails = '" + Bno + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
        ////        Remark = GCon.getDataSet(sql);
        ////        if (Remark.Rows.Count > 0)
        ////        {
        ////            var ReData = Remark.Rows[0];
        ////            if (ReData["REMARKS"] != "")
        ////            {
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "Remarks : " + ReData["REMARKS"]);
        ////            }
        ////            if (ReData["NCRemarks"] != "")
        ////            {
        ////                Filewrite.WriteLine("{0,-4}{1,-41}", "", "NC Remarks : " + ReData["NCRemarks"]);
        ////            }
        ////        }
        ////        Filewrite.WriteLine();
        ////        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "**Thank You Visit Again**".Length) / 2) + "**Thank You Visit Again**");
        ////        for (int i = 1; i <= 4; i++)
        ////        {
        ////            Filewrite.WriteLine("");
        ////        }

        ////        if (gPrint == true)
        ////        {
        ////            char GS = Strings.Chr(29);
        ////            char ESC = Strings.Chr(27);
        ////            String CMD;
        ////            CMD = ESC + "i";
        ////            Filewrite.WriteLine(CMD);
        ////        }
        ////        Filewrite.Close();
        ////        if (gPrint == false)
        ////        {
        ////            GCon.OpenTextFile(vOutfile);
        ////        }
        ////        else
        ////        {
        ////            GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
        ////        }
        ////    }
        ////}
       
    }
}
