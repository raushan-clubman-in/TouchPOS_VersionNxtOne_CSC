using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class EntryForm : Form
    {
        
        public int Loccode = 0;
        GlobalClass GCon = new GlobalClass();
        public string KOrderNo = "";
        public bool UpdFlag = false;
        bool gPrint = true;
        public int Pax = 0;
        public string MemberCode = "";
        public string MemberName = "";
        public string CardHolderCode = "";
        public string CardHolderName = "";
        public string DigitCode = "";
        public string GuestMobno = "";
        public string GuestName = "";
        public string HTPhoneNo = "";
        public string FinYear = (GlobalVariable.FinStart.Year.ToString()).Substring(2,2) + "-"+ (GlobalVariable.FinEnd.Year.ToString()).Substring(2,2);
        public string FinYear1 = (GlobalVariable.FinStart.Year.ToString()) + "-" + (GlobalVariable.FinEnd.Year.ToString());
        public string TabBill = "";
        public string DocType = "TKOT";
        public event System.Windows.Forms.DataGridViewCellEventHandler CellEnter;
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);
        AutoCompleteStringCollection searchResults = new AutoCompleteStringCollection();
        AutoCompleteTextBox Txt_ItemAuto = new AutoCompleteTextBox();
        AutoCompleteStringCollection NwSearchSource = new AutoCompleteStringCollection();

        public EntryForm()
        {
            InitializeComponent();
        }

        string sql = "";
        string PutQty = "";
        DataTable dtPosts = new DataTable();
        DataTable dtPostsbar = new DataTable();
        DataTable dtPostsitem = new DataTable();
        string KotCompName = "", KotPrinterName = "";
        DataView dtView;
        Boolean SelfClick = false;
        Double TotBillAmt = 0;
        //string DocType = "TKOT";

        private void EntryForm_Load(object sender, EventArgs e)
        {

            //Txt_ItemAuto.AutoCompleteMode = AutoCompleteMode.Suggest;
            //Txt_ItemAuto.Location = new Point(9,26);
            //Txt_ItemAuto.Size = new Size(220, 40);
            //Txt_ItemAuto.Parent = panel4;
           
            //dataGridView1.DefaultCellStyle.BackColor = Color.Gainsboro;
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //dataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.OutsetDouble;

            if (GlobalVariable.ServiceType == "Direct-Billing")
            {
                Pnl_DirectB.Visible = true;
                GetTableColor();
            }
            else 
            {
                Pnl_DirectB.Visible = false;
            }

            DocType = Convert.ToString(GCon.getValue("Select Isnull(KotPrefix,'') as KotPrefix from ServiceLocation_Hdr Where LocCode = " + Loccode + ""));
            sql = "UPDATE TableMaster SET OpenStatus = 'O' WHERE Pos = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);
            sql = "UPDATE ServiceLocation_Tables SET OpenStatus = 'O' WHERE LocCode = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);
            GCon.GetBillCloseDate();
            //int ONo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' ")) + 1;
            int ONo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' AND DOCTYPE = '" + DocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ")) + 1;
            label1.Text = "Staff:" + GlobalVariable.gUserName + "/ SalesDates:" + GlobalVariable.ServerDate.ToString("dd MMM") + "/ Order No : " + ONo;
            if (GlobalVariable.gCompName == "CSC")
            {
                ONo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0) FROM PoSKotDoc Where DocType = 'KOT' ")) + 1;
                string ONo1 = "1";
                ONo1 = ONo.ToString("000000");
                label1.Text = "Staff:" + GlobalVariable.gUserName + "/ SalesDates:" + GlobalVariable.ServerDate.ToString("dd MMM") + "/ KOT No : KOT/" + ONo1 + "/" + FinYear;
            }
            label2.Text = GlobalVariable.gCompanyName + "";
            label3.Text = "TableNo: " + GlobalVariable.SLocation + "/"+ GlobalVariable.TableNo;
            label5.Text = "ChairNo : " + GlobalVariable.ChairNo;
            label4.Text = GlobalVariable.ServiceType;

            if (SelfClick == false) 
            {
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                Utility.relocate(this, 1368, 768);
                Utility.repositionForm(this, screenWidth, screenHeight);
                AutoComplete();
            }
            
            ////flpItem.Dock = DockStyle.Fill;
            //flpItem.SizeChanged += panel1_SizeChanged;
            if (GlobalVariable.gCompName == "NZC")
            {
                Lbl_BarCode.Visible = true;
                Txt_BarCode.Visible = true;
                AutoCompleteBarCode();
                Txt_BarCode.Text = "";
                Chk_SearchByCode.Visible = true;
            }
            else 
            {
                Lbl_BarCode.Visible = false;
                Txt_BarCode.Visible = false;
                Chk_SearchByCode.Visible = false;
            }
            //AutoComplete();
            FillGroup();
            FillBusinessSource();
            FillSteward();
            FillNCCategory();
            Chk_NCApply_CheckedChanged(sender, e);
            Txt_Item.Text = "";
            Lbl_Modifier.Text = "";
            Lbl_Qty.Text = "";
            Lbl_TotAmt.Text = "";
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[16].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].Width = 150;
            this.dataGridView1.Columns[2].Width = 50;
            this.dataGridView1.Columns[16].Width = 20;
            KOrderNo = ONo.ToString();
            if (UpdFlag == true)
            {
                DataTable KotData = new DataTable();
                sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,SLNO,MODIFIER,AUTOID,isnull(OrderNo,0) as OrderNo,Isnull(KotStatus,'N') as KotStatus,Isnull(PROMOTIONALST,'') AS PROMOTIONALST,Isnull(FinYear,'') as FinYear,Isnull(BusinessSource,'') as BusinessSource,Isnull(HAPPYSTATUS,'N') as HAPPYSTATUS,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges,Isnull(ItemPrintFlag,'N') as ItemPrintFlag from KOT_det where KOTDETAILS IN ";
                sql = sql + "(SELECT kotdetails FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = '" + GlobalVariable.TableNo + "' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + GlobalVariable.ChairNo + " And KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(delflag,'') <> 'Y') And TABLENO = '" + GlobalVariable.TableNo + "' And KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' And isnull(delflag,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ORDER BY SLNO";
                KotData = GCon.getDataSet(sql);
                if (KotData.Rows.Count > 0) 
                {
                    label1.Text = "Staff:" + GlobalVariable.gUserName + "/ SalesDates:" + GlobalVariable.ServerDate.ToString("dd MMM") + "/ Order No : " + KotData.Rows[0].ItemArray[0];
                    if (GlobalVariable.gCompName == "CSC") 
                    {
                        label1.Text = "Staff:" + GlobalVariable.gUserName + "/ SalesDates:" + GlobalVariable.ServerDate.ToString("dd MMM") + "/ KOT No : " + KotData.Rows[0].ItemArray[1];
                    }
                    //KOrderNo = KotData.Rows[0].ItemArray[0].ToString();
                    KOrderNo = KotData.Rows[0].ItemArray[1].ToString();
                    FinYear1 = KotData.Rows[0].ItemArray[16].ToString();
                    Cmb_BSource.Text = KotData.Rows[0].ItemArray[17].ToString();
                    Pax = Convert.ToInt16(GCon.getValue("Select Top 1 Isnull(Covers,0) as Covers from KOT_HDR Where Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    for (int i = 0; i < KotData.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = KotData.Rows[i].ItemArray[2];
                        dataGridView1.Rows[i].Cells[1].Value = KotData.Rows[i].ItemArray[3];
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
                        dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[8]);
                        dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[9]);
                        dataGridView1.Rows[i].Cells[5].Value = KotData.Rows[i].ItemArray[5];
                        dataGridView1.Rows[i].Cells[6].Value = KotData.Rows[i].ItemArray[6];
                        dataGridView1.Rows[i].Cells[7].Value = KotData.Rows[i].ItemArray[11];
                        dataGridView1.Rows[i].Cells[8].Value = KotData.Rows[i].ItemArray[4];
                        dataGridView1.Rows[i].Cells[9].Value = KotData.Rows[i].ItemArray[10];
                        dataGridView1.Rows[i].Cells[10].Value = KotData.Rows[i].ItemArray[12];
                        dataGridView1.Rows[i].Cells[11].Value = KotData.Rows[i].ItemArray[13];
                        dataGridView1.Rows[i].Cells[12].Value = KotData.Rows[i].ItemArray[14];
                        if (KotData.Rows[i].ItemArray[14].ToString() == "Y")
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        dataGridView1.Rows[i].Cells[13].Value = KotData.Rows[i].ItemArray[15];
                        dataGridView1.Rows[i].Cells[14].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
                        dataGridView1.Rows[i].Cells[15].Value = KotData.Rows[i].ItemArray[18];
                        dataGridView1.Rows[i].Cells[16].Value = KotData.Rows[i].ItemArray[19];
                        dataGridView1.Rows[i].Cells[16].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[17].Value = KotData.Rows[i].ItemArray[20];
                        if (KotData.Rows[i].ItemArray[21].ToString() == "Y")
                        {
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.SkyBlue;
                        }
                    }
                    DataTable KotMem = new DataTable();
                    sql = "Select ISNULL(MCode,'') AS MCode,ISNULL(Mname,'') AS Mname,ISNULL(CARDHOLDERCODE,'') AS CARDHOLDERCODE,ISNULL(CARDHOLDERNAME,'') AS CARDHOLDERNAME,ISNULL([16_DIGIT_CODE],'') AS DCODE,ISNULL([STWNAME],'') AS STWNAME,ISNULL(Remarks,'') AS Remarks,ISNULL(NCFlag,'N') AS NCFlag,ISNULL(NCCategory,'') AS NCCategory from KOT_HDR Where Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    KotMem = GCon.getDataSet(sql);
                    if (KotMem.Rows.Count > 0) 
                    {
                        MemberCode = KotMem.Rows[0].ItemArray[0].ToString();
                        MemberName = KotMem.Rows[0].ItemArray[1].ToString();
                        CardHolderCode = KotMem.Rows[0].ItemArray[2].ToString();
                        CardHolderName = KotMem.Rows[0].ItemArray[3].ToString();
                        DigitCode = KotMem.Rows[0].ItemArray[4].ToString();
                        Cmb_Steward.Text = KotMem.Rows[0].ItemArray[5].ToString();
                        Txt_Remarks.Text = KotMem.Rows[0].ItemArray[6].ToString();
                        if (KotMem.Rows[0].ItemArray[7].ToString() == "Y")
                        {
                            Chk_NCApply.Checked = true;
                            Cmb_NCCategory.Text = KotMem.Rows[0].ItemArray[8].ToString();
                            Chk_NCApply.Enabled = false;
                            Cmb_NCCategory.Enabled = false;
                        }
                        else 
                        {
                            if (GlobalVariable.gCompName == "SKYYE") 
                            {
                                Chk_NCApply.Checked = false;
                                Chk_NCApply.Enabled = true;
                                Cmb_NCCategory.Enabled = true;
                            }
                            else 
                            {
                                Chk_NCApply.Checked = false;
                                Chk_NCApply.Enabled = false;
                                Cmb_NCCategory.Enabled = false;
                            }
                        }
                        if (MemberCode != "") 
                        {
                            label1.Text = label1.Text + "/ Member Code: " + MemberCode;
                        }
                    }
                    if (GlobalVariable.gCompName == "NZC")
                    {
                        string BCode = GCon.getValue("select top 1 isnull(MBCode,'') as MBCode from kot_det where Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'").ToString();
                        Txt_BarCode.Text = BCode;
                    }
                    Calculate();
                }
                //Cmb_BSource.Enabled = false;
                Cmb_Steward.Enabled = false;
            }
            if (GlobalVariable.gCompName == "CSC")
            { 
                Cmd_SubGroup.Text = "Cost Center";
                label7.Text = "Server";
                Cmd_Save.BackgroundImage = global::TouchPOS.Properties.Resources.Blue;
                Cmd_Pay.BackgroundImage = global::TouchPOS.Properties.Resources.GreenbuttonMaster;
                Cmd_Save.ForeColor = Color.White;
                Cmd_Pay.ForeColor = Color.White;
                //panel10.Enabled = false;
            }
            if (GlobalVariable.gUserCategory != "S") { GetRights(); }
            GC.Collect();
        }

        private void FillBusinessSource() 
        {
            DataTable dt = new DataTable();
            sql = "select BusinessSource from Tbl_BusinessSource Where Isnull(void,'') <> 'Y' Order by 1";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_BSource.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_BSource.Items.Add(dt.Rows[i]["BusinessSource"].ToString());
                }
                Cmb_BSource.SelectedIndex = 0;
            }
        }

        private void FillSteward()
        {
            DataTable dt = new DataTable();
            //Cmb_Steward.DisplayMember = "ServerName";
            //Cmb_Steward.ValueMember = "ServerCode";
            sql = "SELECT ServerName,ServerCode FROM ServerMaster wHERE SERVERTYPE = 'STEWARD' AND ISNULL(FREEZE,'') <> 'Y' ORDER BY 1 ";
            if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT ServerName,ServerCode FROM ServerMaster wHERE ISNULL(FREEZE,'') <> 'Y' ORDER BY 1 ";
            }
            else if (GlobalVariable.gCompName == "CFC")
            {
                sql = "SELECT ServerName,ServerCode FROM ServerMaster wHERE ISNULL(FREEZE,'') <> 'Y' ORDER BY 1 ";
            }
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                //Cmb_Steward.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Cmb_Steward.Items.Add(dt.Rows[i]["ServerName"].ToString());
                    //Cmb_Steward.Items.Add(new { Text = dt.Rows[i]["ServerName"].ToString(), Value = dt.Rows[i]["ServerCode"].ToString() });
                }
                Cmb_Steward.DataSource = dt;
                Cmb_Steward.DisplayMember = "ServerName";
                Cmb_Steward.ValueMember = "ServerCode";
               
                Cmb_Steward.SelectedIndex = 0;
            }
        }

        private void FillNCCategory()
        {
            DataTable dt = new DataTable();
            sql = "select NCCategory from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' Order by 1";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                Cmb_NCCategory.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_NCCategory.Items.Add(dt.Rows[i]["NCCategory"].ToString());
                }
                Cmb_NCCategory.SelectedIndex = 0;
            }
        }

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {
            //ServiceType ST = new ServiceType();
            //ST.Show();
            //this.Hide();

            sql = "UPDATE TableMaster SET OpenStatus = '' WHERE Pos = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);
            sql = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE LocCode = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
            GCon.dataOperation(1, sql);

            if (GlobalVariable.ServiceType == "Dine-In")
            {
                ServiceLocation SL = new ServiceLocation();
                GlobalVariable.SLocation = "";
                GlobalVariable.TableNo = "";
                GlobalVariable.ChairNo = 1;
                SL.Show();
                //this.Hide();
                this.Close();
            }
            else 
            {
                ServiceType SL = new ServiceType();
                GlobalVariable.SLocation = "";
                GlobalVariable.TableNo = "";
                GlobalVariable.ChairNo = 1;
                SL.Show();
                //this.Hide();
                this.Close();
            }
        }

        private void Cmd_Group_Click(object sender, EventArgs e)
        {
            FillGroup();
            //int totButtons = flpItem.Controls.OfType<Button>().Count();

            //Point curPos = new Point(0, 0);
            //foreach (Button but in flpItem.Controls.OfType<Button>())
            //{
            //    but.Width = flpItem.Width / totButtons;
            //    but.Location = curPos;
            //    curPos = new Point(curPos.X + but.Width, 0);
            //}
        }

        private void FillGroup()
        {
            DataTable Grpdt = new DataTable();
            int intX = 10;
            int intY = 10;
            int ActPanalSize = flpItem.Width;
            sql = "SELECT DISTINCT GroupCode,GROUPCODEDEC FROM ItemMaster WHERE ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + "))  And Isnull(Freeze,'') <> 'Y' And Isnull(GroupCode,'') <> '' ORDER BY 2";
            Grpdt = GCon.getDataSet(sql);
            if (Grpdt.Rows.Count > 0) 
            {
                flpItem.Controls.Clear();
                flpItem.AutoScroll = true;
                //flpItem.AutoScrollMinSize = 200F;
                ////flpItem.AutoSize = true;
                ////flpItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                foreach (DataRow dr in Grpdt.Rows) 
                {
                    Button btn = new Button();
                    btn.Text = dr[1].ToString() ;
                    btn.Tag = dr[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.BackColor = Color.White;
                    btn.Width = 110;
                    btn.Height = 80;
                    //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    //btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    btn.Location = new Point(intX, intY);
                    flpItem.Controls.Add(btn);
                    ////intX = btn.Location.X + 160;
                    ////intY = btn.Location.Y + 90;
                    if (intX > ActPanalSize - 200)
                    {
                        intX = 10;
                        intY = intY + 90;
                    }
                    else
                    {
                        intX = intX + 120;
                    }
                    btn.Click += new EventHandler(button1_Click);
                }
            }
        }

        private void FillSubGroup()
        {
            DataTable Grpdt = new DataTable();
            int intX = 10;
            int intY = 10;
            int ActPanalSize = flpItem.Width;
            sql = "SELECT DISTINCT SUBGROUPCODE,SUBGROUPDESC FROM ItemMaster WHERE ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + "))  And Isnull(Freeze,'') <> 'Y' And Isnull(SUBGROUPCODE,'') <> '' ORDER BY 2";
            if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT POSCode,POSDESC = Case When isnull(APPPOSDesc,'') <> '' Then isnull(APPPOSDesc,'') Else POSDesc End FROM POSMASTER WHERE POSCode IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")  And Isnull(Freeze,'') <> 'Y' And Isnull(POSCode,'') <> '' ORDER BY 2";
            }
            Grpdt = GCon.getDataSet(sql);
            if (Grpdt.Rows.Count > 0)
            {
                flpItem.Controls.Clear();
                flpItem.AutoScroll = true;
                //flpItem.AutoScrollMinSize = 200F;
                ////flpItem.AutoSize = true;
                ////flpItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                foreach (DataRow dr in Grpdt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr[1].ToString();
                    btn.Tag = dr[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.BackColor = Color.White;
                    btn.Width = 110;
                    btn.Height = 80;
                    //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    //btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    btn.Location = new Point(intX, intY);
                    flpItem.Controls.Add(btn);
                    ////intX = btn.Location.X + 160;
                    ////intY = btn.Location.Y + 90;
                    if (intX > ActPanalSize - 200)
                    {
                        intX = 10;
                        intY = intY + 90;
                    }
                    else
                    {
                        intX = intX + 120;
                    }
                    btn.Click += new EventHandler(button3_Click);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e) 
        {
            Button selectedBtn = sender as Button;
            FillItem(selectedBtn.Tag.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button selectedBtn = sender as Button;
            FillSubGroupItem(selectedBtn.Tag.ToString());
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            DataTable dt = new DataTable();
            DataTable Posdt = new DataTable();
            DataTable Uomdt = new DataTable();
            Button selectedBtn = sender as Button;
            int RowCnt;
            double Rate;

            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }

            if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,P.Pos as rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R,PosMenuLink P WHERE I.ITEMCODE = R.ITEMCODE and i.itemcode = p.ItemCode ";
                sql = sql + " AND I.ITEMCODE = '" + selectedBtn.Tag.ToString() + "' AND P.Pos IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate())  ";
            }
            else 
            {
                sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                sql = sql + " AND I.ITEMCODE = '" + selectedBtn.Tag.ToString() + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
            }
            ////sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
            ////sql = sql + " AND I.ITEMCODE = '" + selectedBtn.Tag.ToString() + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0) 
            {
                DataRow dr = dt.Rows[0];
                RowCnt = dataGridView1.RowCount;
                if (Chk_NCApply.Checked == true)
                {
                    string TypeRate = GCon.getValue(" select Isnull(TypeForRate,'') as TypeForRate from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "' ").ToString();
                    Double PSPercent = Convert.ToDouble(GCon.getValue(" select Isnull(PSPercent,0) as PSPercent from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "'"));
                    if (TypeRate == "P") { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                    else if (TypeRate == "S") { Rate = Convert.ToDouble(dr["ItemRate"]); }
                    else if (TypeRate == "PS") { Rate = (Convert.ToDouble(dr["ItemRate"]) * PSPercent) / 100; }
                    else { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                }
                else 
                {
                    Rate = Convert.ToDouble(dr["ItemRate"]);
                }
                dataGridView1.Rows.Add();
                dataGridView1.Rows[RowCnt-1].Cells[0].Value = dr["Itemcode"].ToString();
                //object Chargecode = GCon.getValue("select Itemtypecode from itemmaster where itemcode = '" + dr["Itemcode"].ToString() + "'");
                object Chargecode = GCon.getValue("SELECT ISNULL(TaxOnItem,'') AS TaxOnItem FROM posmenulinK WHERE ItemCode = '" + dr["Itemcode"].ToString() + "' AND POS = '" + dr["rposcode"].ToString() + "'");
                dataGridView1.Rows[RowCnt-1].Cells[1].Value = dr["ItemDesc"].ToString();
                dataGridView1.Rows[RowCnt-1].Cells[2].Value = 1;
                dataGridView1.Rows[RowCnt-1].Cells[3].Value = Rate;
                sql = " select P.Pos,M.POSDesc from PosMenuLink P,PosMaster M where P.Pos = M.Poscode And itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ")";
                Posdt = GCon.getDataSet(sql);
                if (Posdt.Rows.Count > 1) 
                {
                    PosSelection PS = new PosSelection(this);
                    PS.ItemCode = dr["Itemcode"].ToString();
                    PS.loccode = Loccode;
                    PS.ShowDialog();
                    if (PS.PosCode != "")
                    { dataGridView1.Rows[RowCnt - 1].Cells[5].Value = PS.PosCode; }
                }
                else
                {
                    dataGridView1.Rows[RowCnt-1].Cells[5].Value = dr["rposcode"].ToString();
                }
                //dataGridView1.Rows[RowCnt-1].Cells[5].Value = dr["rposcode"].ToString();
                if (GlobalVariable.gCompName == "CSC")
                {
                    sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode And R.itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
                }
                else
                {
                    sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode and P.Pos = R.Rposcode And R.itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
                }
                Uomdt = GCon.getDataSet(sql);
                if (Uomdt.Rows.Count > 1)
                {
                    UOMRateSelection URS = new UOMRateSelection(this);
                    URS.ItemCode = dr["Itemcode"].ToString();
                    URS.loccode = Loccode;
                    URS.ShowDialog();
                    if (URS.UomCode != "")
                    { 
                        dataGridView1.Rows[RowCnt - 1].Cells[6].Value = URS.UomCode;
                        dataGridView1.Rows[RowCnt - 1].Cells[3].Value = URS.UomRate;
                    }
                }
                else
                {
                    dataGridView1.Rows[RowCnt - 1].Cells[6].Value = dr["UOM"].ToString();
                }
                //dataGridView1.Rows[RowCnt-1].Cells[6].Value = dr["UOM"].ToString();
                dataGridView1.Rows[RowCnt - 1].Cells[8].Value = Chargecode;
                CheckHappyHour(dr["Itemcode"].ToString(), RowCnt - 1);
                dataGridView1.Rows[RowCnt - 1].Cells[16].Value = 1;
                dataGridView1.CurrentCell = dataGridView1.Rows[RowCnt - 1].Cells[1];
                Promotational(RowCnt - 1);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, RowCnt - 1);
                if (ClsVal == 0)
                {
                    dataGridView1.Rows.RemoveAt(RowCnt - 1);
                }
                if (GlobalVariable.gCompName == "EPC" && GlobalVariable.AccountPostFlag == "YES") 
                {
                    DataTable AccCheck = new DataTable();
                    DataTable AccCheck1 = new DataTable();
                    sql = "SELECT * FROM accountstag WHERE ITEMCODE = '" + dr["Itemcode"].ToString() + "'";
                    AccCheck = GCon.getDataSet(sql);
                    if (AccCheck.Rows.Count > 0)
                    {
                        sql = "SELECT * FROM accountstag WHERE ITEMCODE = '" + dr["Itemcode"].ToString() + "' AND (ISNULL(accountcode,'') = '' OR ISNULL(accountcode,'') IN (SELECT ACCODE FROM ACCOUNTSGLACCOUNTMASTER WHERE ISNULL(freezeflag,'') = 'Y')) ";
                        AccCheck1 = GCon.getDataSet(sql);
                        if (AccCheck1.Rows.Count > 0) 
                        {
                            MessageBox.Show(" Account Code Not Tag For Item ");
                            dataGridView1.Rows.RemoveAt(RowCnt - 1);
                        }
                    }
                    else 
                    {
                        MessageBox.Show(" Account Code Not Tag For Item " );
                        dataGridView1.Rows.RemoveAt(RowCnt - 1);
                    }
                }
                if (dr["Openfacility"].ToString().Trim() == "Y") 
                { 
                    dataGridView1.Rows[RowCnt-1].Cells[3].ReadOnly = false;
                }
                else { dataGridView1.Rows[RowCnt - 1].Cells[3].ReadOnly = true; }
                CheckDuplicate(dr["Itemcode"].ToString(), RowCnt - 1);
                SmartBalanceChecking(RowCnt - 1);
            }
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly = false;
            Calculate();
        }

        private void CheckHappyHour(string itemcode, int Rowno) 
        {
            DataTable CheckHappy = new DataTable();
            DataTable ItemCheck = new DataTable();
            string varposcode="", PosItemUom="";
            double HPercentange = 0,HItemRate = 0;
            varposcode = Convert.ToString(dataGridView1.Rows[Rowno].Cells[5].Value);
            PosItemUom = Convert.ToString(dataGridView1.Rows[Rowno].Cells[6].Value);
            if (Chk_NCApply.Checked == true) { return; }
            sql = "SELECT * FROM HappyLink WHERE ISNULL(POSCODE,'') = '" + (varposcode) + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN FROMDATE AND TODATE AND '" + DateAndTime.Now.ToString("HH:MM") + "' BETWEEN FROMTIME AND TOTIME AND WDAY = (SELECT UPPER(DATENAME(weekday, getdate()))) AND itemcode = '" + (itemcode) + "' ";
            CheckHappy = GCon.getDataSet(sql);
            if (CheckHappy.Rows.Count > 0)
            {
                 DataRow dr = CheckHappy.Rows[0];
                 HPercentange = Convert.ToDouble(dr["DiscPer"]) / 100;
                 sql = "SELECT ITEMRATE FROM RateMaster WHERE rposcode = '" + (varposcode) + "' AND ITEMCODE = '" + (itemcode) + "' AND UOM  = '" + PosItemUom + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN StartingDate and ISNULL(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
                 ItemCheck = GCon.getDataSet(sql);
                 if (ItemCheck.Rows.Count > 0) 
                 {
                     HItemRate = Convert.ToDouble(ItemCheck.Rows[0].ItemArray[0]);
                     dataGridView1.Rows[Rowno].Cells[3].Value = HItemRate - (HItemRate*HPercentange);
                     dataGridView1.Rows[Rowno].Cells[15].Value = "Y";
                 }
             }
             else 
             {
                 dataGridView1.Rows[Rowno].Cells[15].Value = "N";
             }
        }

        private void CheckDuplicate(string itemcode, int RowNo) 
        {
            int cnt=0;
            string RItemcode= "";
            string CancelFlag = "N";
            for (int i = 0; i < dataGridView1.RowCount - 1; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                { RItemcode = dataGridView1.Rows[i].Cells[0].Value.ToString(); }
                else { dataGridView1.Rows[i].Cells[0].Value = ""; }
                if (dataGridView1.Rows[i].Cells[12].Value != null)
                {
                    CancelFlag = dataGridView1.Rows[i].Cells[12].Value.ToString();
                }
                else { CancelFlag = "N"; }
                if (RItemcode == itemcode && CancelFlag == "N") 
                {
                    cnt = cnt + 1;
                }
            }
            if (GlobalVariable.DupItemAllowed.ToUpper() == "YES") { return; }
            if (cnt > 1) 
            {
                MessageBox.Show("Duplicate Item Selected", GlobalVariable.gCompanyName);
                dataGridView1.Rows.RemoveAt(RowNo);
            }
        }

        private void FillItem(string Gcode) 
        {
            DataTable Itemdt = new DataTable();
            int intX = 10;
            int intY = 10;
            int ActPanalSize = flpItem.Width;
            //sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' ORDER BY 2 ";
            //sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM posmenulinK WHERE POS IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " )) ORDER BY 2 ";
            if (GlobalVariable.gCompName == "NZC")
            {
                //sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
                sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
            }
            else if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM posmenulink WHERE Pos IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + ") And ItemCode in (Select Itemcode from RateMaster Where '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate()))) ORDER BY 2";
            }
            else
            {
                //sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
                sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
            }
            Itemdt = GCon.getDataSet(sql);
            if (Itemdt.Rows.Count > 0) 
            {
                flpItem.Controls.Clear();
                flpItem.AutoScroll = true;
                foreach (DataRow dr in Itemdt.Rows) 
                {
                    Button btn = new Button();
                    btn.Text = dr[1].ToString();
                    btn.Tag = dr[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.BackColor = Color.White;
                    btn.Width = 110;
                    btn.Height = 80;
                    //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //if (btn.Text.Length > 20) 
                    //{
                    //    btn.Font = new System.Drawing.Font("Microsoft Sans Serif",4.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //}

                    if (GlobalVariable.gCompName != "")
                    {
                        byte[] data = (byte[])dr[2];
                        if (data.Length > 4)
                        {
                            MemoryStream ms = new MemoryStream(data);
                            btn.BackgroundImage = Image.FromStream(ms);
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                            btn.BackColor = Color.Black;
                            btn.ForeColor = Color.White;
                        }
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        //btn.ForeColor = Color.White;
                        //btn.BackColor = Color.Gray;
                    }

                    //btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    btn.Location = new Point(intX, intY);
                    //btn.Font = new System.Drawing.Font(btn.Font.FontFamily, btn.Height - 10, btn.Font.Style, GraphicsUnit.Pixel);
                    //FitControlFont(btn);
                    flpItem.Controls.Add(btn);
                    ////intX = btn.Location.X + 160;
                    ////intY = btn.Location.Y + 90;
                    if (intX > ActPanalSize - 200)
                    {
                        intX = 10;
                        intY = intY + 90;
                    }
                    else
                    {
                        intX = intX + 120;
                    }
                    btn.Click += new EventHandler(button2_Click);
                }
            }
        }

        public static float NewFontSize(Graphics graphics, Size size, Font font, string str)
        {
            SizeF stringSize = graphics.MeasureString(str, font);
            float wRatio = size.Width / stringSize.Width;
            float hRatio = size.Height / stringSize.Height;
            float ratio = Math.Min(hRatio, wRatio);
            return font.Size * ratio;
        }

        private void FillSubGroupItem(string GScode)
        {
            DataTable Itemdt = new DataTable();
            int intX = 10;
            int intY = 10;
            int ActPanalSize = flpItem.Width;
            //sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE SubGroupCode = '" + GScode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM posmenulinK WHERE POS IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " )) ORDER BY 2 ";
            if (GlobalVariable.gCompName == "NZC")
            {
                //sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE GroupCode = '" + Gcode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
                //sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE SubGroupCode = '" + GScode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
                sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE SubGroupCode = '" + GScode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
            }
            else if (GlobalVariable.gCompName == "CSC")
            {
                sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM posmenulink WHERE Pos IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " And PosCode = '" + GScode + "') And ItemCode in (Select Itemcode from RateMaster Where '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate()))) ORDER BY 2";
            }
            else
            {
                //sql = "SELECT DISTINCT ItemCode,ItemDesc FROM ItemMaster WHERE SubGroupCode = '" + GScode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
                sql = "SELECT ItemCode,ItemDesc,cast(ISNULL([PreviewImage],CONVERT(VARBINARY(MAX), 0)) as image) AS PreviewImage FROM ItemMaster WHERE SubGroupCode = '" + GScode + "' AND ISNULL(FREEZE,'') <> 'Y' AND ITEMCODE IN (SELECT ItemCode FROM RateMaster WHERE rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LocCode = " + Loccode + " ) And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between StartingDate And Isnull(Endingdate,getdate())) ORDER BY 2 ";
            }
            
            Itemdt = GCon.getDataSet(sql);
            if (Itemdt.Rows.Count > 0)
            {
                flpItem.Controls.Clear();
                flpItem.AutoScroll = true;
                foreach (DataRow dr in Itemdt.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr[1].ToString();
                    btn.Tag = dr[0].ToString();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.BackColor = Color.White;
                    btn.Width = 110;
                    btn.Height = 80;
                    //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    if (GlobalVariable.gCompName != "NZC")
                    {
                        byte[] data = (byte[])dr[2];
                        if (data.Length > 4)
                        {
                            MemoryStream ms = new MemoryStream(data);
                            btn.BackgroundImage = Image.FromStream(ms);
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                            btn.BackColor = Color.Black;
                            btn.ForeColor = Color.White;
                        }
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        //btn.ForeColor = Color.White;
                        //btn.BackColor = Color.Gray;
                    }

                    //btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    btn.Location = new Point(intX, intY);
                    //btn.Font = new System.Drawing.Font(btn.Font.FontFamily, btn.Height - 10, btn.Font.Style, GraphicsUnit.Pixel);
                    //FitControlFont(btn);
                    flpItem.Controls.Add(btn);
                    ////intX = btn.Location.X + 160;
                    ////intY = btn.Location.Y + 90;
                    if (intX > ActPanalSize - 200)
                    {
                        intX = 10;
                        intY = intY + 90;
                    }
                    else
                    {
                        intX = intX + 120;
                    }
                    btn.Click += new EventHandler(button2_Click);
                }
            }
        }

        public static void FitControlFont(Control control)
        {
            if (control.Text.Length == 0)
            {
                return;
            }

            try
            {
                Font currentFont = control.Font;
                Graphics graphics = control.CreateGraphics();
                SizeF newSize = graphics.MeasureString(control.Text, control.Font);
                graphics.Dispose();

                float factorX = control.Width / newSize.Width;
                float factorY = control.Height / newSize.Height;
                float factor = factorX > factorY ? factorY : factorX;
                if (control.InvokeRequired)
                {
                    control.Invoke(new MethodInvoker(delegate { control.Font = new Font(currentFont.Name, currentFont.SizeInPoints * factor); }));
                }
                else
                {
                    control.Font = new Font(currentFont.Name, currentFont.SizeInPoints * factor);
                }
            }
            catch (Exception ex)
            {
                throw;
                return;
            }
        }

        private void Cmd_ClearAll_Click(object sender, EventArgs e)
        {
            if (UpdFlag == true) { }
            else
            {
                dataGridView1.Rows.Clear();
                Lbl_Qty.Text = "";
                Lbl_TotAmt.Text = "";
            }
        }

        private void Cmd_ClearOne_Click(object sender, EventArgs e)
        {
            int Autoid =0;
            int rowindex = dataGridView1.CurrentRow.Index;
            if (dataGridView1.Rows[rowindex].Cells[10].Value != null)
            { Autoid = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[10].Value); }
            else {Autoid = 0;}
            if (Autoid == 0)
            { 
                dataGridView1.Rows.RemoveAt(rowindex); 
            }
            Calculate();
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 

            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_0.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0) 
                {
                    PutQty =LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }

            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 

            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_1.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            }
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_2.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";
            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            }
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_3.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_Qty_Click(object sender, EventArgs e)
        {
            PutQty = "";
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";
            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            }
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_4.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }

            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 

            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }

            if (val != "")
            {
                PutQty = PutQty + Button_5.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";
            string LastQty = "0";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_6.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            string LastQty = "0";
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_7.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            string LastQty = "0";
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_8.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }
            string LastQty = "0";
            if (dataGridView1.Rows[rowindex].Cells[2].Value != null)
            {
                LastQty = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            } 
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                PutQty = PutQty + Button_9.Text;
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0)
                {
                    PutQty = LastQty;
                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(PutQty);
                }
            }
            Calculate();
        }

        private void Calculate() 
        {
            int counter,qty,totQty=0;
            double Rate,TotAmt=0,TaxTotal=0;
            string icode,kotstatus="";
            for (counter = 0; counter < (dataGridView1.Rows.Count - 1);counter++)
            {
                icode = dataGridView1.Rows[counter].Cells[0].Value.ToString();
                if (dataGridView1.Rows[counter].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[counter].Cells[12].Value.ToString(); }
                else { kotstatus = "N"; }
                qty = Convert.ToInt16(dataGridView1.Rows[counter].Cells[2].Value.ToString());
                Rate = Convert.ToDouble(dataGridView1.Rows[counter].Cells[3].Value.ToString());
                if (kotstatus == "N") 
                {
                    TaxTotal = 0;
                    totQty = totQty + qty;
                    TotAmt = TotAmt + (qty * Rate);
                    TaxTotal = TaxCalculate(icode, dataGridView1.Rows[counter].Cells[8].Value.ToString(), dataGridView1.Rows[counter].Cells[5].Value.ToString(), (qty * Rate));
                    TotAmt = TotAmt + TaxTotal;
                }
                dataGridView1.Rows[counter].Cells[4].Value = qty*Rate;
            }
            TotAmt = Math.Round(TotAmt, 0);
            Lbl_Qty.Text = "Quantity " + totQty.ToString();
            Lbl_TotAmt.Text = "Total Amount: " + TotAmt.ToString();
            TotBillAmt = TotAmt;
            //SmartBalanceChecking(TotAmt);
        }

        public void SmartBalanceChecking(int RowNo) 
        {
            DataTable CardCheckone = new DataTable();
            string sqlstring = "";
            Double CardBal = 0;
            Calculate();
            sqlstring = "SELECT CARDCODE,BALANCE FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardHolderCode + "' AND [16_DIGIT_CODE] = '" + DigitCode + "' ";
            CardCheckone = GCon.getDataSet(sqlstring);
            if (CardCheckone.Rows.Count > 0)
            {
                CardBal = Convert.ToDouble(CardCheckone.Rows[0]["BALANCE"]);
            }
            else { CardBal = 0; }
            if (GlobalVariable.gUserCategory != "S") 
            {
                if (CardBal < TotBillAmt && GlobalVariable.CapYN == "Y")
                {
                    MessageBox.Show("Balace is Not Available as per Order, Card Bal is Only:-" + CardBal, " System ll not processed for Order");
                    dataGridView1.Rows.RemoveAt(RowNo);
                    return;
                }
            }
        }

        public Double SmartBalanceReturn()
        {
            DataTable CardCheckone = new DataTable();
            string sqlstring = "";
            Double CardBal = 0;
            sqlstring = "SELECT CARDCODE,BALANCE FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardHolderCode + "' AND [16_DIGIT_CODE] = '" + DigitCode + "' ";
            CardCheckone = GCon.getDataSet(sqlstring);
            if (CardCheckone.Rows.Count > 0)
            {
                CardBal = Convert.ToDouble(CardCheckone.Rows[0]["BALANCE"]);
            }
            else { CardBal = 0; }
            return CardBal;
        }

        public Double TaxCalculate(string ItemCode,string ChargeCode,string PosCode,Double Amt) 
        {
            string sqlstring = "";
            int i = 0;
            string Taxon, Taxcode, IType;
            double TPercent = 0.00;
            double Rate = 0.00;
            int Qty = 1; 
            double Zero1, ZeroA1, ZeroB1, One1, OneA1, OneB1, Two1, TwoA1, TwoB1, Three1, ThreeA1, ThreeB1;
            double GZero1, GZeroA1, GZeroB1, GOne1, GOneA1, GOneB1, GTwo1, GTwoA1, GTwoB1, GThree1, GThreeA1, GThreeB1;
            Double GTotal = 0;
            Double OthAmt1 = 0, OthAmt2 = 0, OthAmt3 = 0;
            object ItemTypeCode = GCon.getValue("select Distinct TAXTYPECODE from CHARGEMASTER Where Chargecode = '" + ChargeCode + "'");
            OthAmt1 = Convert.ToDouble(GCon.getValue("select " + Amt + " * (isnull(PACKINGPERCENT,0)/100) from PosMaster Where poscode = '" + PosCode + "'"));
            OthAmt2 = Convert.ToDouble(GCon.getValue("select " + Amt + " * (isnull(tips,0)/100) from PosMaster Where poscode = '" + PosCode + "'"));
            OthAmt3 = Convert.ToDouble(GCon.getValue("select " + Amt + " * (isnull(ADCHARGE,0)/100) from PosMaster Where poscode = '" + PosCode + "'"));
            Rate = Convert.ToDouble(Amt + OthAmt1 + OthAmt2 + OthAmt3);
            Qty = 1;
            sqlstring = "SELECT ItemTypeCode,TaxCode,TAXON,TaxPercentage FROM ITEMTYPEMASTER WHERE ItemTypeCode = '" + ItemTypeCode + "' ORDER BY TAXON";
            DataTable TAXON = new DataTable();
            TAXON = GCon.getDataSet(sqlstring);
            if (TAXON.Rows.Count > 0)
            {
                Zero1 = 0; ZeroA1 = 0; ZeroB1 = 0; One1 = 0; OneA1 = 0; OneB1 = 0; Two1 = 0; TwoA1 = 0; TwoB1 = 0; Three1 = 0; ThreeA1 = 0; ThreeB1 = 0;
                GZero1 = 0; GZeroA1 = 0; GZeroB1 = 0; GOne1 = 0; GOneA1 = 0; GOneB1 = 0; GTwo1 = 0; GTwoA1 = 0; GTwoB1 = 0; GThree1 = 0; GThreeA1 = 0; GThreeB1 = 0;
                for (int j = 0; j < TAXON.Rows.Count; j++)
                {
                    DataRow dr = TAXON.Rows[j];
                    IType = Convert.ToString(dr["ItemTypeCode"]);
                    Taxcode = Convert.ToString(dr["Taxcode"]);
                    Taxon = Convert.ToString(dr["TAXON"]);
                    TPercent = Convert.ToDouble(dr["TaxPercentage"]);
                    if (dr["TAXON"].ToString() == "0")
                    {
                        Zero1 = (Rate * TPercent) / 100;
                        GZero1 = GZero1 + Zero1;
                    }
                    else if (dr["TAXON"].ToString() == "0A")
                    {
                        ZeroA1 = (GZero1 * TPercent) / 100;
                        GZeroA1 = GZeroA1 + ZeroA1;
                    }
                    else if (dr["TAXON"].ToString() == "0B")
                    {
                        ZeroB1 = ((GZero1 + GZeroA1) * TPercent) / 100;
                        GZeroB1 = GZeroB1 + ZeroB1;
                    }
                    else if (dr["TAXON"].ToString() == "1")
                    {
                        One1 = ((Rate + GZero1 + GZeroA1) * TPercent) / 100;
                        GOne1 = GOne1 + One1;
                    }
                    else if (dr["TAXON"].ToString() == "1A")
                    {
                        OneA1 = (One1 * TPercent) / 100;
                        GOneA1 = GOneA1 + OneA1;
                    }
                    else if (dr["TAXON"].ToString() == "1B")
                    {
                        OneB1 = ((GOne1 + GOneA1) * TPercent) / 100;
                        GOneB1 = GOneB1 + OneB1;
                    }
                    else if (dr["TAXON"].ToString() == "2")
                    {
                        Two1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1) * TPercent) / 100;
                        GTwo1 = GTwo1 + Two1;
                    }
                    else if (dr["TAXON"].ToString() == "2A")
                    {
                        TwoA1 = (Two1 * TPercent) / 100;
                        GTwoA1 = GTwoA1 + TwoA1;
                    }
                    else if (dr["TAXON"].ToString() == "2B")
                    {
                        TwoB1 = ((GTwo1 + GTwoA1) * TPercent) / 100;
                        GTwoB1 = GTwoB1 + TwoB1;
                    }
                    else if (dr["TAXON"].ToString() == "3")
                    {
                        Three1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1 + GTwo1 + GTwoA1) * TPercent) / 100;
                        GThree1 = GThree1 + Three1;
                    }
                    else if (dr["TAXON"].ToString() == "3A")
                    {
                        ThreeA1 = (Three1 * TPercent) / 100;
                        GThreeA1 = GThreeA1 + ThreeA1;
                    }
                    else if (dr["TAXON"].ToString() == "3B")
                    {
                        ThreeB1 = ((GThree1 + GThreeA1) * TPercent) / 100;
                        GThreeB1 = GThreeB1 + ThreeB1;
                    }
                }
                GTotal = GZero1 + GZeroA1 + GZeroB1 + GOne1 + GOneA1 + GOneB1 + GTwo1 + GTwoA1 + GTwoB1 + GThree1 + GThreeA1 + GThreeB1;
            }
            return GTotal + OthAmt1 + OthAmt2 + OthAmt3;
        }

        private void dataGridView1_CellContentChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Calculate();7
            PutQty = "";
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "";
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[7].Value as String))
            { val = ""; }
            else { val = dataGridView1.Rows[rowindex].Cells[7].Value.ToString(); }
            Lbl_Modifier.Text = val;
            Calculate();
        }

        //private void AutoComplete()
        //{
        //    this.Txt_Item.DataBindings.Clear();
        //    //sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
        //    sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) ";
        //    dtPosts = GCon.getDataSet(sql);
        //    string[] postSource = dtPosts
        //            .AsEnumerable()
        //            .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
        //            .ToArray();
        //    var source = new AutoCompleteStringCollection();
        //    source.AddRange(postSource);
        //    Txt_Item.AutoCompleteCustomSource = source;
        //    Txt_Item.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    // AutoCompleteStringCollection col = new AutoCompleteStringCollection();
        //    this.Txt_Item.DataBindings.Add("Text", dtPosts, "ItemDesc");

        //    //dtView = new DataView(dtPosts);
        //    this.Txt_Item.Text = "";
        //}

        //private void AutoComplete()
        //{
           
        //    //sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
        //    sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) ";
        //    dtPosts = GCon.getDataSet(sql);
        //    string[] postSource = dtPosts
        //            .AsEnumerable()
        //            .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
        //            .ToArray();

        //    Txt_ItemAuto.Values = postSource;
        //}



        private void AutoComplete()
        {
            //this.Txt_Item.DataBindings.Clear();
            //sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
            sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) ";
            if (GlobalVariable.gCompName == "CSC") 
            {
                sql = "SELECT DISTINCT Itemcode,ItemDesc+'=>'+Itemcode As ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) ";
            }
            dtPosts = GCon.getDataSet(sql);
            string[] postSource = dtPosts
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
                    .ToArray();

            NwSearchSource.AddRange(postSource);
            text_auto_nw.AutoCompleteCustomSource = NwSearchSource;
            text_auto_nw.AutoCompleteMode = AutoCompleteMode.None;
            text_auto_nw.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            // AutoCompleteStringCollection col = new AutoCompleteStringCollection();
           // this.text_auto_nw.DataBindings.Add("Text", dtPosts, "ItemDesc");
        }

        private void hideSearchList()
        {
            listBox_Items.Visible=false;
        }

        private void text_auto_nw_TextChanged(object sender, EventArgs e)
        {
            Int32 maxlen = 1;
            listBox_Items.Items.Clear();
            if (text_auto_nw.Text.Length == 0)
            {
                hideSearchList();
                return;
            }

            foreach (string s in text_auto_nw.AutoCompleteCustomSource)
            { 
                if(s.ToUpper().Contains(text_auto_nw.Text.ToUpper()))
                {
                   listBox_Items.Items.Add(s);
                   listBox_Items.Visible = true;
                   //maxlen = s.Length;
                   if (maxlen < s.Length)
                   { maxlen = s.Length; }
                }             
            }
            if (11 * maxlen < 226) { listBox_Items.Width = 226; }
            else { listBox_Items.Width = 11 * maxlen; }
                                                                                                                                                                                                                                                                                 
        }

        private void AutoCompleteItem()
        {
            this.Txt_Item.DataBindings.Clear();
            sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) ";
            dtPostsitem = GCon.getDataSet(sql);
            string[] postSource2 = dtPostsitem
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("Itemcode"))
                    .ToArray();
            var source2 = new AutoCompleteStringCollection();
            source2.AddRange(postSource2);
            Txt_Item.AutoCompleteCustomSource = source2;
            Txt_Item.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            this.Txt_Item.DataBindings.Add("Text", dtPostsitem, "Itemcode");
            //dtView = new DataView(dtPosts);
            this.Txt_Item.Text = "";
        }

        private void AutoCompleteBarCode()
        {
            sql = "SELECT Storecode,Storedesc FROM StoreMaster WHERE ISNULL(FREEZE,'') <> 'Y' AND ISNULL(Storestatus,'') <> 'M' ";
            dtPostsbar = GCon.getDataSet(sql);
            string[] postSource1 = dtPostsbar
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("Storecode"))
                    .ToArray();
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(postSource1);
            Txt_BarCode.AutoCompleteCustomSource = source1;
            Txt_BarCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Txt_BarCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.Txt_BarCode.DataBindings.Add("Text", dtPostsbar, "Storecode");
        }

        private void Cmd_BackSpace_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //string icode;
            //int RowCnt;
            //double Rate;
            //BindingManagerBase b = this.Txt_Item.BindingContext[dtPosts, "ItemDesc"];
            //icode = (dtPosts.Select("ItemDesc = '" + b.Current.ToString() + "'"))[0][0].ToString();

            //sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,rposcode,UOM FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
            //sql = sql + " AND I.ITEMCODE = '" + icode + "' AND rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") ";
            //dt = GCon.getDataSet(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt.Rows[0];
            //    RowCnt = dataGridView1.RowCount;
            //    Rate = Convert.ToDouble(dr["ItemRate"]);
            //    dataGridView1.Rows.Add();
            //    dataGridView1.Rows[RowCnt - 1].Cells[0].Value = dr["Itemcode"].ToString();
            //    dataGridView1.Rows[RowCnt - 1].Cells[1].Value = dr["ItemDesc"].ToString();
            //    dataGridView1.Rows[RowCnt - 1].Cells[2].Value = 1;
            //    dataGridView1.Rows[RowCnt - 1].Cells[3].Value = Rate;
            //    dataGridView1.Rows[RowCnt - 1].Cells[5].Value = dr["rposcode"].ToString();
            //    dataGridView1.Rows[RowCnt - 1].Cells[6].Value = dr["UOM"].ToString();

            //}
            //Calculate(); 
            if (Txt_Item.SelectionStart > 0)
            {
                int index = Txt_Item.SelectionStart;
                Txt_Item.Text = Txt_Item.Text.Remove(Txt_Item.SelectionStart - 1, 1);
                Txt_Item.Select(index - 1, 1);
                Txt_Item.Focus();
            }
        }

        
        private void Txt_Item_KeyDown(object sender, KeyEventArgs e) 
        {
           if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                DataTable Posdt = new DataTable();
                DataTable Uomdt = new DataTable();
                string icode;
                int RowCnt;
                double Rate;

                DataTable CheckPrintFlag = new DataTable();
                sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                CheckPrintFlag = GCon.getDataSet(sql);
                if (CheckPrintFlag.Rows.Count > 0)
                {
                    MessageBox.Show("Check Print Done, You can't Modify");
                    return;
                }

                //BindingManagerBase b = this.Txt_Item.BindingContext[dtPosts, "ItemDesc"];
                //icode = (dtPosts.Select("ItemDesc = '" + b.Current.ToString().Replace("'", "''") + "'"))[0][0].ToString();
                if (GlobalVariable.gCompName == "NZC")
                {
                    if (Chk_SearchByCode.Checked == true)
                    {
                        sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                        sql = sql + " AND I.ItemCode = '" + Txt_Item.Text.Replace("'", "''") + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    }
                    else
                    {
                        sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                        sql = sql + " AND I.ItemDesc = '" + Txt_Item.Text.Replace("'", "''") + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    }
                }
                else if (GlobalVariable.gCompName == "CSC") 
                {
                    string[] SplitCode = { "", "" };
                    SplitCode = Txt_Item.Text.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                    Txt_Item.Text = SplitCode[0].ToString();
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,P.Pos as rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R,PosMenuLink P WHERE I.ITEMCODE = R.ITEMCODE and i.itemcode = p.ItemCode  ";
                    sql = sql + " AND I.ItemDesc = '" + Txt_Item.Text.Replace("'", "''") + "' AND  P.Pos IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate())";
                }
                else
                {
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                    sql = sql + " AND I.ItemDesc = '" + Txt_Item.Text.Replace("'", "''") + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                }
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    RowCnt = dataGridView1.RowCount;
                    if (Chk_NCApply.Checked == true)
                    {
                        string TypeRate = GCon.getValue(" select Isnull(TypeForRate,'') as TypeForRate from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "' ").ToString();
                        Double PSPercent = Convert.ToDouble(GCon.getValue(" select Isnull(PSPercent,0) as PSPercent from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "'"));
                        if (TypeRate == "P") { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        else if (TypeRate == "S") { Rate = Convert.ToDouble(dr["ItemRate"]); }
                        else if (TypeRate == "PS") { Rate = (Convert.ToDouble(dr["ItemRate"]) * PSPercent) / 100; }
                        else { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        //Rate = Convert.ToDouble(dr["PurcahseRate"]);
                    }
                    else
                    {
                        Rate = Convert.ToDouble(dr["ItemRate"]);
                    }
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[RowCnt - 1].Cells[0].Value = dr["Itemcode"].ToString();
                    //object Chargecode = GCon.getValue("select Itemtypecode from itemmaster where itemcode = '" + dr["Itemcode"].ToString() + "'");
                    object Chargecode = GCon.getValue("SELECT ISNULL(TaxOnItem,'') AS TaxOnItem FROM posmenulinK WHERE ItemCode = '" + dr["Itemcode"].ToString() + "' AND POS = '" + dr["rposcode"].ToString() + "'");
                    dataGridView1.Rows[RowCnt - 1].Cells[1].Value = dr["ItemDesc"].ToString();
                    dataGridView1.Rows[RowCnt - 1].Cells[2].Value = 1;
                    dataGridView1.Rows[RowCnt - 1].Cells[3].Value = Rate;

                    sql = " select P.Pos,M.POSDesc from PosMenuLink P,PosMaster M where P.Pos = M.Poscode And itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ")";
                    Posdt = GCon.getDataSet(sql);
                    if (Posdt.Rows.Count > 1)
                    {
                        PosSelection PS = new PosSelection(this);
                        PS.ItemCode = dr["Itemcode"].ToString();
                        PS.loccode = Loccode;
                        PS.ShowDialog();
                        if (PS.PosCode != "")
                        { dataGridView1.Rows[RowCnt - 1].Cells[5].Value = PS.PosCode; }
                    }
                    else
                    {
                        dataGridView1.Rows[RowCnt - 1].Cells[5].Value = dr["rposcode"].ToString();
                    }
                    //dataGridView1.Rows[RowCnt - 1].Cells[5].Value = dr["rposcode"].ToString();
                    if (GlobalVariable.gCompName == "CSC")
                    {
                        sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode And R.itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
                    }
                    else 
                    {
                        sql = " select Distinct UOM,ItemRate from PosMenuLink P,RATEMASTER R where P.ItemCode = R.ItemCode and P.Pos = R.Rposcode And R.itemcode = '" + dr["Itemcode"].ToString() + "' And P.Pos In (Select PosCode from ServiceLocation_Det Where LocCode = " + Loccode + ") And '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' Between R.StartingDate and Isnull(EndingDate,'" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "') ";
                    }
                    Uomdt = GCon.getDataSet(sql);
                    if (Uomdt.Rows.Count > 1)
                    {
                        UOMRateSelection URS = new UOMRateSelection(this);
                        URS.ItemCode = dr["Itemcode"].ToString();
                        URS.loccode = Loccode;
                        URS.ShowDialog();
                        if (URS.UomCode != "")
                        {
                            dataGridView1.Rows[RowCnt - 1].Cells[6].Value = URS.UomCode;
                            dataGridView1.Rows[RowCnt - 1].Cells[3].Value = URS.UomRate;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[RowCnt - 1].Cells[6].Value = dr["UOM"].ToString();
                    }
                    //dataGridView1.Rows[RowCnt - 1].Cells[6].Value = dr["UOM"].ToString();
                    dataGridView1.Rows[RowCnt - 1].Cells[8].Value = Chargecode;
                    dataGridView1.Rows[RowCnt - 1].Cells[16].Value = 1;
                    CheckHappyHour(dr["Itemcode"].ToString(), RowCnt - 1);
                    Promotational(RowCnt - 1);
                    int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, RowCnt - 1);
                    if (ClsVal == 0)
                    {
                        dataGridView1.Rows.RemoveAt(RowCnt - 1);
                    }
                    if (GlobalVariable.gCompName == "EPC" && GlobalVariable.AccountPostFlag == "YES")
                    {
                        DataTable AccCheck = new DataTable();
                        DataTable AccCheck1 = new DataTable();
                        sql = "SELECT * FROM accountstag WHERE ITEMCODE = '" + dr["Itemcode"].ToString() + "'";
                        AccCheck = GCon.getDataSet(sql);
                        if (AccCheck.Rows.Count > 0)
                        {
                            sql = "SELECT * FROM accountstag WHERE ITEMCODE = '" + dr["Itemcode"].ToString() + "' AND (ISNULL(accountcode,'') = '' OR ISNULL(accountcode,'') IN (SELECT ACCODE FROM ACCOUNTSGLACCOUNTMASTER WHERE ISNULL(freezeflag,'') = 'Y')) ";
                            AccCheck1 = GCon.getDataSet(sql);
                            if (AccCheck1.Rows.Count > 0)
                            {
                                MessageBox.Show(" Account Code Not Tag For Item ");
                                dataGridView1.Rows.RemoveAt(RowCnt - 1);
                            }
                        }
                        else
                        {
                            MessageBox.Show(" Account Code Not Tag For Item ");
                            dataGridView1.Rows.RemoveAt(RowCnt - 1);
                        }
                    }
                    if (dr["Openfacility"].ToString().Trim() == "Y")
                    {
                        dataGridView1.Rows[RowCnt - 1].Cells[3].ReadOnly = false;
                    }
                    else { dataGridView1.Rows[RowCnt - 1].Cells[3].ReadOnly = true; }

                    CheckDuplicate(dr["Itemcode"].ToString(), RowCnt - 1);
                    SmartBalanceChecking(RowCnt - 1);
                }
                Txt_Item.Text = "";
                text_auto_nw.Text = "";
                Calculate();
            }
            else 
            {


                ////var term = Txt_Item.Text;

                //////this.Txt_Item.DataBindings.Clear();
                //////sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' ";
                ////sql = "SELECT DISTINCT Itemcode,ItemDesc FROM ItemMaster WHERE ISNULL(FREEZE,'') <> 'Y' And ItemCode IN (SELECT ItemCode FROM POSMENULINK WHERE POS IN (Select PosCode from ServiceLocation_Det WHERE LocCode =" + Loccode + ")) and itemdesc like '%" + term + "%' ";
                ////dtPosts = GCon.getDataSet(sql);
                ////string[] postSource = dtPosts
                ////        .AsEnumerable()
                ////        .Select<System.Data.DataRow, String>(x => x.Field<String>("ItemDesc"))
                ////        .ToArray();
                ////var source = new AutoCompleteStringCollection();
                ////source.AddRange(postSource);
                ////Txt_Item.AutoCompleteCustomSource = source;
                ////Txt_Item.AutoCompleteMode = AutoCompleteMode.Suggest;
                ////Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ////// AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                ////this.Txt_Item.DataBindings.Clear();
                ////this.Txt_Item.DataBindings.Add("Text", dtPosts, "ItemDesc");

                //////dtView = new DataView(dtPosts);
                //////this.Txt_Item.Text = "";
            }
        }

        private void Promotational(int RowId) 
        {
            string varitemcode = "", Basedon = "", WeekDay = "", varposcode = "", buom = "";
            Double ITEMQTY = 0, rkotqty = 0;
            DataTable Prom = new DataTable();
            DataTable PromAvail = new DataTable();
            DataTable PromData = new DataTable();
            Double baseqty = 0, Freeqty = 0, FreeRate = 0, FreeDis = 0, DActRate = 0, PKotQty = 0, PROQTY_GRID = 0;
            string Pitemcode = "", Puom = "", Type = "", MAvail = "Y";
            string Status = "",Status1 = "";

            varitemcode = Convert.ToString(dataGridView1.Rows[RowId].Cells[0].Value);
            varposcode = Convert.ToString(dataGridView1.Rows[RowId].Cells[5].Value);
            buom = Convert.ToString(dataGridView1.Rows[RowId].Cells[6].Value);
            ITEMQTY = Convert.ToInt32(dataGridView1.Rows[RowId].Cells[2].Value);

            //if (varitemcode != "") { return; }
            
            sql = "SELECT 'Y' AS Promotional,BASEDON AS PromItemcode FROM PROMMASTER WHERE BASEDITEMCODE = '" + varitemcode + "' AND ISNULL(FREEZE,'') <> 'Y'  AND '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN FROMDATE AND TODATE";
            Prom = GCon.getDataSet(sql);
            if (Prom.Rows.Count > 0)
            {
                WeekDay = GlobalVariable.ServerDate.DayOfWeek.ToString();
                Basedon = Convert.ToString(Prom.Rows[0].ItemArray[1]);
                if (Basedon == "Q")
                {
                    sql = "SELECT PITEMCODE,isnull(saleqty,0)as fromqty,ISNULL(FREEQTY,0) AS FREEQTY,ISNULL(PUOM,'') AS PUOM,ISNULL(TYPE,'') AS TYPE,ISNULL(FIXEDRATE,0) AS FIXEDRATE,ISNULL(DISCOUNT,0) AS DISCOUNT,ISNULL(DISCOUNTPRICE,0) AS DISCOUNTPRICE,cast(((" + ITEMQTY + " /saleqty )- " + rkotqty + " )*ISNULL(FreeQty,0) as integer) as prom FROM PROMMASTER WHERE BASEDON = 'Q' AND BASEDITEMCODE = '" + varitemcode + "' and baseduom= '" + buom + "' AND '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN FROMDATE AND TODATE AND '" + Strings.Format(DateTime.Now, "HH:mm") + "' BETWEEN FROMTIME AND TOTIME AND WDAY = '" + WeekDay + "' AND POSCODE = '" + varposcode + "' AND ISNULL(FREEZE,'') <> 'Y' ";
                }
                else if (Basedon == "F")
                {
                    sql = "SELECT PITEMCODE,isnull(saleqty,0)as fromqty,ISNULL(FREEQTY,0) AS FREEQTY,ISNULL(PUOM,'') AS PUOM,'" + Basedon + "' AS TYPE,ISNULL(FIXEDRATE,0) AS FIXEDRATE,ISNULL(DISCOUNT,0) AS DISCOUNT,ISNULL(DISCOUNTPRICE,0) AS DISCOUNTPRICE,cast(((" + ITEMQTY + " /saleqty )- " + rkotqty + " )*ISNULL(FreeQty,0) as integer) as prom FROM PROMMASTER WHERE BASEDON = 'F' AND BASEDITEMCODE = '" + varitemcode + "' and baseduom= '" + buom + "' AND '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN FROMDATE AND TODATE AND '" + Strings.Format(DateTime.Now, "HH:mm") + "' BETWEEN FROMTIME AND TOTIME AND WDAY = '" + WeekDay + "' AND POSCODE = '" + varposcode + "' AND ISNULL(FREEZE,'') <> 'Y'";
                }
                else if (Basedon == "D")
                {
                    sql = "SELECT PITEMCODE,isnull(saleqty,0)as fromqty,ISNULL(FREEQTY,0) AS FREEQTY,ISNULL(PUOM,'') AS PUOM,ISNULL(TYPE,'') AS TYPE,ISNULL(FIXEDRATE,0) AS FIXEDRATE,ISNULL(DISCOUNT,0) AS DISCOUNT,ISNULL(DISCOUNTPRICE,0) AS DISCOUNTPRICE,cast(((" + ITEMQTY + " /saleqty )- " + rkotqty + " )*ISNULL(FreeQty,0) as integer) as prom FROM PROMMASTER WHERE BASEDON = 'D' AND BASEDITEMCODE = '" + varitemcode + "' and baseduom= '" + buom + "' AND '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN FROMDATE AND TODATE AND '" + Strings.Format(DateTime.Now, "HH:mm") + "' BETWEEN FROMTIME AND TOTIME AND WDAY = '" + WeekDay + "' AND POSCODE = '" + varposcode + "' AND ISNULL(FREEZE,'') <> 'Y' ";
                }
                PromAvail = GCon.getDataSet(sql);
                if (PromAvail.Rows.Count > 0) 
                {
                    DataRow dr = PromAvail.Rows[0];
                    if (Convert.ToDouble(dr["prom"]) > 0 && Basedon == "Q")
                    {
                        baseqty = Convert.ToDouble(dr["fromqty"]);
                        Pitemcode = dr["PITEMCODE"].ToString();
                        Freeqty = Convert.ToDouble(dr["prom"]);
                        Puom = dr["PUOM"].ToString();
                        Type = dr["TYPE"].ToString();
                        FreeRate = Convert.ToDouble(dr["DISCOUNTPRICE"]);
                        FreeDis = Convert.ToDouble(dr["DISCOUNT"]);
                    }
                    if (Convert.ToDouble(dr["prom"]) >= 0 && (Basedon == "F" || Basedon == "D"))
                    {
                        baseqty = Convert.ToDouble(dr["fromqty"]);
                        Pitemcode = dr["PITEMCODE"].ToString();
                        Freeqty = Convert.ToDouble(dr["prom"]);
                        Puom = dr["PUOM"].ToString();
                        Type = dr["TYPE"].ToString();
                        FreeRate = Convert.ToDouble(dr["DISCOUNTPRICE"]);
                        FreeDis = Convert.ToDouble(dr["DISCOUNT"]);
                    }
                    if (Basedon == "Q") 
                    {
                        sql = "SELECT DISTINCT " + Freeqty + " AS PROMQTY, I.ITEMCODE,'" + Pitemcode + "' AS PROMITEMCODE, I.ITEMDESC, I.GROUPCODE, I.ITEMTYPECODE, P.POSCODE, P.POSDESC,TL.STARTINGDATE,TL.ENDINGDATE, 'QTY' AS PROMTYPE, ";
                        sql = sql + " '" + Puom + "' AS PROMUOM, " + Freeqty + " AS PROMQTY," + ITEMQTY + " AS BASEQTY , I.BASERATESTD AS PROMRATE FROM ITEMMASTER AS I INNER JOIN CHARGEMASTER AS CH ON CH.CHARGECODE = I.ItemTypecode INNER JOIN POSMENULINK AS PL ON I.ITEMCODE = PL.ITEMCODE INNER JOIN ";
                        sql = sql + " POSMASTER AS P ON PL.POS = P.POSCODE INNER JOIN ITEMTYPEMASTER AS TL ON TL.ITEMTYPECODE = CH.TAXTYPECODE ";
                        sql = sql + " WHERE (I.ITEMCODE = '" + Pitemcode + "') AND ISNULL(I.FREEZE,'') <>'Y' ";
                        sql = sql + " AND  '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN TL.StartingDate AND TL.EndingDate  AND PL.POS='" + varposcode + "'";
                    }
                    else if (Basedon == "D")
                    {
                        sql = "SELECT DISTINCT " + Freeqty + " AS PROMQTY, I.ITEMCODE,'" + Pitemcode + "' AS PROMITEMCODE, I.ITEMDESC, I.GROUPCODE, I.ITEMTYPECODE, P.POSCODE, P.POSDESC,TL.STARTINGDATE,TL.ENDINGDATE, 'QTY' AS PROMTYPE,";
                        sql = sql + " '" + Puom + "' AS PROMUOM, " + Freeqty + " AS PROMQTY," + ITEMQTY + " AS BASEQTY , " + DActRate + " AS PROMRATE FROM ITEMMASTER AS I INNER JOIN CHARGEMASTER AS CH ON CH.CHARGECODE = I.ItemTypecode INNER JOIN POSMENULINK AS PL ON I.ITEMCODE = PL.ITEMCODE INNER JOIN";
                        sql = sql + " POSMASTER AS P ON PL.POS = P.POSCODE INNER JOIN ITEMTYPEMASTER AS TL ON TL.ITEMTYPECODE = CH.TAXTYPECODE ";
                        sql = sql + " WHERE (I.ITEMCODE = '" + Pitemcode + "') AND ISNULL(I.FREEZE,'') <>'Y' ";
                        sql = sql + " AND  '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN TL.StartingDate AND TL.EndingDate  AND PL.POS='" + varposcode + "'";
                    }
                    else if (Basedon == "F") 
                    {
                        sql = "SELECT DISTINCT " + Freeqty + " AS PROMQTY, I.ITEMCODE,'" + Pitemcode + "' AS PROMITEMCODE, I.ITEMDESC, I.GROUPCODE, I.ITEMTYPECODE, P.POSCODE, P.POSDESC,TL.STARTINGDATE,TL.ENDINGDATE, 'QTY' AS PROMTYPE,";
                        sql = sql + " '" + Puom + "' AS PROMUOM, " + Freeqty + " AS PROMQTY," + ITEMQTY + " AS BASEQTY , I.BASERATESTD AS PROMRATE FROM ITEMMASTER AS I INNER JOIN CHARGEMASTER AS CH ON CH.CHARGECODE = I.ItemTypecode INNER JOIN POSMENULINK AS PL ON I.ITEMCODE = PL.ITEMCODE  INNER JOIN";
                        sql = sql + " POSMASTER AS P ON PL.POS = P.POSCODE INNER JOIN ITEMTYPEMASTER AS TL ON TL.ITEMTYPECODE = CH.TAXTYPECODE ";
                        sql = sql + " WHERE (I.ITEMCODE = '" + Pitemcode + "') AND ISNULL(I.FREEZE,'') <>'Y' ";
                        sql = sql + " AND  '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy") + "' BETWEEN TL.StartingDate AND TL.EndingDate  AND PL.POS='" + varposcode + "'";
                    }
                    PromData = GCon.getDataSet(sql);
                    if (Basedon == "Q") 
                    {
                        if (PromData.Rows.Count > 0 && MAvail == "Y" && (ITEMQTY + PKotQty) >= baseqty)
                        {
                            DataRow dr1 = PromData.Rows[0];
                            PROQTY_GRID = ((Math.Floor(ITEMQTY + PKotQty) / (Math.Floor(Convert.ToDouble(dr1["BASEQTY"])))) * ((Convert.ToDouble(dr1["PROMQTY"]))));
                            if (PROQTY_GRID != 0 && Basedon == "Q")
                            {
                                if (dataGridView1.Rows[RowId].Cells[13].Value != null)
                                {
                                    Status = dataGridView1.Rows[RowId].Cells[13].Value.ToString();
                                }
                                else { Status = ""; }
                                if (Status == "") { dataGridView1.Rows.Insert(RowId + 1, 1); }
                                dataGridView1.Rows[RowId + 1].Cells[0].Value = dr1["PROMITEMCODE"].ToString();
                                dataGridView1.Rows[RowId + 1].Cells[1].Value = dr1["ITEMDESC"].ToString();
                                dataGridView1.Rows[RowId + 1].Cells[2].Value = PROQTY_GRID;
                                dataGridView1.Rows[RowId + 1].Cells[3].Value = 0;
                                dataGridView1.Rows[RowId + 1].Cells[5].Value = dr1["POSCODE"].ToString();
                                dataGridView1.Rows[RowId + 1].Cells[6].Value = dr1["PROMUOM"].ToString();
                                dataGridView1.Rows[RowId + 1].Cells[8].Value = dr1["ITEMTYPECODE"].ToString();
                                dataGridView1.Rows[RowId + 1].Cells[16].Value = 1;
                                dataGridView1.Rows[RowId + 1].Cells[13].Value = "Y";
                                dataGridView1.Rows[RowId].Cells[13].Value = "Q";
                            }
                            
                            else
                            {
                                if (dataGridView1.Rows[RowId].Cells[13].Value != null)
                                {
                                    Status = dataGridView1.Rows[RowId].Cells[13].Value.ToString();
                                }
                                else { Status = ""; }
                                if (Status == "Q")
                                {
                                    if (dataGridView1.Rows[RowId + 1].Cells[13].Value != null)
                                    {
                                        Status1 = dataGridView1.Rows[RowId + 1].Cells[13].Value.ToString();
                                    }
                                    else { Status1 = ""; }
                                }
                                if (Status == "Q" && Status1 == "Y") { dataGridView1.Rows.RemoveAt(RowId + 1); }
                            }

                        }
                        else
                        {
                            if (dataGridView1.Rows[RowId].Cells[13].Value != null)
                            {
                                Status = dataGridView1.Rows[RowId].Cells[13].Value.ToString();
                            }
                            else { Status = ""; }
                            if (Status == "Q")
                            {
                                if (dataGridView1.Rows[RowId + 1].Cells[13].Value != null)
                                {
                                    Status1 = dataGridView1.Rows[RowId + 1].Cells[13].Value.ToString();
                                }
                                else { Status1 = ""; }
                            }
                            if (Status == "Q" && Status1 == "Y")
                            {
                                dataGridView1.Rows.RemoveAt(RowId + 1);
                                dataGridView1.Rows[RowId].Cells[13].Value = "";
                            }
                        }
                    }
                    if (Basedon != "Q")
                    {
                        if (PromData.Rows.Count > 0 && MAvail == "Y" && (ITEMQTY + PKotQty) >= baseqty)
                        {
                            DataRow dr1 = PromData.Rows[0];
                            PROQTY_GRID = ((Math.Floor(ITEMQTY + PKotQty) / (Math.Floor(Convert.ToDouble(dr1["BASEQTY"])))) * ((Convert.ToDouble(dr1["PROMQTY"]))));
                            if (Basedon == "F" && (ITEMQTY + PKotQty) >= baseqty)
                            {
                                dataGridView1.Rows[RowId].Cells[3].Value = FreeRate;
                                dataGridView1.Rows[RowId].Cells[13].Value = "F";
                            }
                        }
                        else if (PromData.Rows.Count > 0 && MAvail == "Y" )
                        {
                            if (dataGridView1.Rows[RowId].Cells[13].Value != null)
                            {
                                Status = dataGridView1.Rows[RowId].Cells[13].Value.ToString();
                            }
                            else { Status = ""; }
                            if (Status == "F")
                            {
                                if (dataGridView1.Rows[RowId + 1].Cells[13].Value != null)
                                {
                                    Status1 = dataGridView1.Rows[RowId + 1].Cells[13].Value.ToString();
                                }
                                else { Status1 = ""; }
                            }
                            if (Status == "F" || Status1 == "D")
                            {
                                DataTable dt = new DataTable();
                                sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate,Isnull(openfacility,'') as Openfacility FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                                sql = sql + " AND I.itemcode = '" + varitemcode.Replace("'", "''") + "' AND R.rposcode IN (SELECT PosCode FROM ServiceLocation_Det WHERE LOCCODE = " + Loccode + ") AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                                dt = GCon.getDataSet(sql);
                                if (dt.Rows.Count > 0) 
                                {
                                    DataRow dr2 = dt.Rows[0];
                                    dataGridView1.Rows[RowId ].Cells[3].Value = Convert.ToDouble(dr2["ItemRate"]); ;
                                }
                                dataGridView1.Rows[RowId].Cells[13].Value = "";
                            }
                        }
                    }
                }
            }
        }
       
        private void Txt_Item_Enter(object sender, System.EventArgs e)
        {
            
        }

        private void Cmd_Add_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N",PromStaus = "";
            Double CardBal1 = 0;

            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }

            int Autoid = 0;
            if (dataGridView1.Rows[rowindex].Cells[10].Value != null)
            { Autoid = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[10].Value); }
            else { Autoid = 0; }
            if (Autoid > 0)
            {
                //return;
            }

            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "")
            {
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[2].Value) + 1;
                Promotational(rowindex);
                int ClsVal = GCon.STOCKAVAILABILITY(dataGridView1, rowindex);
                if (ClsVal == 0) { dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[2].Value) - 1; }
                Calculate();
                if (GlobalVariable.gUserCategory != "S") 
                {
                    CardBal1 = SmartBalanceReturn();
                    if (CardBal1 < TotBillAmt && GlobalVariable.CapYN == "Y")
                    {
                        MessageBox.Show("Balace is Not Available as per Order, Card Bal is Only:-" + CardBal1, " System ll not processed for Order");
                        dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[2].Value) - 1;
                        Calculate();
                        return;
                    }
                }
            }
        }

        private void Cmd_Less_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "", kotstatus = "N", PromStaus = "";

            DataTable CheckPrintFlag = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
            CheckPrintFlag = GCon.getDataSet(sql);
            if (CheckPrintFlag.Rows.Count > 0)
            {
                MessageBox.Show("Check Print Done, You can't Modify");
                return;
            }

            if (dataGridView1.Rows[rowindex].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }
            if (kotstatus == "Y")
            {
                return;
            }
            if (dataGridView1.Rows[rowindex].Cells[13].Value != null) { PromStaus = dataGridView1.Rows[rowindex].Cells[13].Value.ToString(); }
            else { PromStaus = ""; }
            if (PromStaus == "Y")
            {
                return;
            }

            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[1].Value as String))
            { }
            else { val = dataGridView1.Rows[rowindex].Cells[1].Value.ToString(); }
            if (val != "" && Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[2].Value) > 1)
            {
                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[2].Value) - 1;
                Promotational(rowindex);
                Calculate();
            }
        }

        public void FillModifier(string MText,int rowindex,double ChargesAmt) 
        {
            if (MText != "") 
            {
                dataGridView1.Rows[rowindex].Cells[7].Value = MText;
            }
            dataGridView1.Rows[rowindex].Cells[17].Value = ChargesAmt;
        }

        private void Cmd_Modifier_Click(object sender, EventArgs e)
        {
            string MItemCode = "";
            DataTable MTable = new DataTable();

            int rowindex = dataGridView1.CurrentRow.Index;
            MItemCode = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();

            sql = "SELECT ISNULL(ModifierType,'None') as ModifierType FROM itemmaster Where ItemCode = '" + MItemCode + "'";
            MTable = GCon.getDataSet(sql);
            if (MTable.Rows.Count > 0) 
            {
                DataRow dr = MTable.Rows[0];
                if (dr["ModifierType"].ToString() == "None")
                {
                    MessageBox.Show("Modifier Type is None, Please set Fixed,Open or Both to set Modifier");
                }
                else 
                {
                    Modifier MF = new Modifier(this, dataGridView1);
                    MF.Rowno = rowindex;
                    MF.ShowDialog();

                    string val = "";
                    if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[7].Value as String))
                    { val = ""; }
                    else { val = dataGridView1.Rows[rowindex].Cells[7].Value.ToString(); }
                    Lbl_Modifier.Text = val;
                }
            }
        }

        public bool CheckCard() 
        {
            string DescVal = "", icode = "", kotstatus= "";
            Double CardBal = 0;
            string sqlstring = "";
            string Taxon, Taxcode, IType;
            double TPercent = 0.00;
            double Rate=0.00,TotTax = 0.00,TotalAmt = 0.00,TotalTax=0.00;
            int Qty = 1,Slno = 0;
            double Zero1, ZeroA1, ZeroB1, One1, OneA1, OneB1, Two1, TwoA1, TwoB1, Three1, ThreeA1, ThreeB1;
            double GZero1 = 0, GZeroA1 = 0, GZeroB1 = 0, GOne1 = 0, GOneA1 = 0, GOneB1 = 0, GTwo1 = 0, GTwoA1 = 0, GTwoB1 = 0, GThree1 = 0, GThreeA1 = 0, GThreeB1 = 0;
            DataTable Member = new DataTable();
           
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                object ItemTypeCode = GCon.getValue("select Distinct TAXTYPECODE from CHARGEMASTER Where Chargecode = '" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "'");
                Rate = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                Qty = Convert.ToInt16(dataGridView1.Rows[i].Cells[2].Value);
                Slno = Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value);
                sqlstring = "SELECT ItemTypeCode,TaxCode,TAXON,TaxPercentage FROM ITEMTYPEMASTER WHERE ItemTypeCode = '" + ItemTypeCode + "' ORDER BY TAXON";
                DataTable TAXON = new DataTable();
                TAXON = GCon.getDataSet(sqlstring);
                if (TAXON.Rows.Count > 0)
                {
                    Zero1 = 0; ZeroA1 = 0; ZeroB1 = 0; One1 = 0; OneA1 = 0; OneB1 = 0; Two1 = 0; TwoA1 = 0; TwoB1 = 0; Three1 = 0; ThreeA1 = 0; ThreeB1 = 0;
                    GZero1 = 0; GZeroA1 = 0; GZeroB1 = 0; GOne1 = 0; GOneA1 = 0; GOneB1 = 0; GTwo1 = 0; GTwoA1 = 0; GTwoB1 = 0; GThree1 = 0; GThreeA1 = 0; GThreeB1 = 0;
                    for (int j = 0; j < TAXON.Rows.Count; j++)
                    {
                        DataRow dr = TAXON.Rows[j];
                        TPercent = Convert.ToDouble(dr["TaxPercentage"]);
                        if (dr["TAXON"].ToString() == "0")
                        {
                            Zero1 = (Rate * TPercent) / 100;
                            GZero1 = GZero1 + Zero1;
                        }
                        else if (dr["TAXON"].ToString() == "0A")
                        {
                            ZeroA1 = (GZero1 * TPercent) / 100;
                            GZeroA1 = GZeroA1 + ZeroA1;
                        }
                        else if (dr["TAXON"].ToString() == "0B")
                        {
                            ZeroB1 = ((GZero1 + GZeroA1) * TPercent) / 100;
                            GZeroB1 = GZeroB1 + ZeroB1;
                        }
                        else if (dr["TAXON"].ToString() == "1")
                        {
                            One1 = ((Rate + GZero1 + GZeroA1) * TPercent) / 100;
                            GOne1 = GOne1 + One1;
                        }
                        else if (dr["TAXON"].ToString() == "1A")
                        {
                            OneA1 = (One1 * TPercent) / 100;
                            GOneA1 = GOneA1 + OneA1;
                        }
                        else if (dr["TAXON"].ToString() == "1B")
                        {
                            OneB1 = ((GOne1 + GOneA1) * TPercent) / 100;
                            GOneB1 = GOneB1 + OneB1;
                        }
                        else if (dr["TAXON"].ToString() == "2")
                        {
                            Two1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1) * TPercent) / 100;
                            GTwo1 = GTwo1 + Two1;
                        }
                        else if (dr["TAXON"].ToString() == "2A")
                        {
                            TwoA1 = (Two1 * TPercent) / 100;
                            GTwoA1 = GTwoA1 + TwoA1;
                        }
                        else if (dr["TAXON"].ToString() == "2B")
                        {
                            TwoB1 = ((GTwo1 + GTwoA1) * TPercent) / 100;
                            GTwoB1 = GTwoB1 + TwoB1;
                        }
                        else if (dr["TAXON"].ToString() == "3")
                        {
                            Three1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1 + GTwo1 + GTwoA1) * TPercent) / 100;
                            GThree1 = GThree1 + Three1;
                        }
                        else if (dr["TAXON"].ToString() == "3A")
                        {
                            ThreeA1 = (Three1 * TPercent) / 100;
                            GThreeA1 = GThreeA1 + ThreeA1;
                        }
                        else if (dr["TAXON"].ToString() == "3B")
                        {
                            ThreeB1 = ((GThree1 + GThreeA1) * TPercent) / 100;
                            GThreeB1 = GThreeB1 + ThreeB1;
                        }
                    }
                }
                TotTax = GZero1 + GZeroA1 + GZeroB1 + GOne1 + GOneA1 + GOneB1 + GTwo1 + GTwoA1 + GTwoB1 + GThree1 + GThreeA1 + GThreeB1;
                TotalTax = TotalTax + (TotTax * Qty);
            }

            for (int counter = 0; counter < (dataGridView1.Rows.Count - 1); counter++)
            {
                icode = dataGridView1.Rows[counter].Cells[0].Value.ToString();
                if (dataGridView1.Rows[counter].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[counter].Cells[12].Value.ToString(); }
                else { kotstatus = "N"; }
                Qty = Convert.ToInt16(dataGridView1.Rows[counter].Cells[2].Value.ToString());
                Rate = Convert.ToDouble(dataGridView1.Rows[counter].Cells[3].Value.ToString());
                if (kotstatus == "N")
                {
                    TotalAmt = TotalAmt + (Qty * Rate);
                }
            }
            TotalAmt = TotalAmt + TotalTax;

            Member = GCon.getDataSet("select membercode,CARDCODE,[16_DIGIT_CODE],ISSUETYPE,Isnull(Balance,0) as Balance from SM_CARDFILE_HDR where CARDCODE = '" + CardHolderCode + "' And [16_DIGIT_CODE] = '" + DigitCode + "' And isnull(Activation_Flag,'') = 'Y'");
            if (Member.Rows.Count > 0)
            {
                DescVal = Member.Rows[0].ItemArray[3].ToString();
                CardBal = Convert.ToDouble(Member.Rows[0].ItemArray[4]);
                if (DescVal == "MEM") { return true; }
                else
                {
                    if (TotalAmt > CardBal && DescVal != "MEM")
                    {
                        MessageBox.Show("Smart Card Balance Not Available", GlobalVariable.gCompanyName);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            { return true; }
        }

        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            string sqlstring = "";
            bool TrnDone = false;
            int i = 0;
            string Taxon, Taxcode, IType;
            double TPercent = 0.00;
            double Rate=0.00;
            int Qty = 1,Slno = 0;
            int KotNo = 1;
            string kotNo1 = "1";
            string kotdetails = "";
            double Zero1, ZeroA1, ZeroB1, One1, OneA1, OneB1, Two1, TwoA1, TwoB1, Three1, ThreeA1, ThreeB1;
            double GZero1, GZeroA1, GZeroB1, GOne1, GOneA1, GOneB1, GTwo1, GTwoA1, GTwoB1, GThree1, GThreeA1, GThreeB1;
            string SeZFalg = "N";
            string NCFlagHPRC = "N";
            string CheckNo = "",CheckDocType= "";
            int CheckNumberInt = 1;
            Double CardBalS = 0;

            bool PaySettle = CheckCard();
            if (PaySettle == false)
            {
                return;
            }
            if (GlobalVariable.gCompName == "NZC" && Txt_BarCode.Text =="") 
            {
                MessageBox.Show("Bar Code Should Not Blank");
                Txt_BarCode.Focus();
                return;
            }
            if (dataGridView1.Rows.Count == 1) 
            {
                MessageBox.Show("No any Items Found in Grid");
                return;
            }
            if (GlobalVariable.gCompName == "CFC" ) 
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("R U Sure to Save Changes " , GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes)
                {
                }
                else 
                {
                    return;
                }
            }

            Calculate();
            if (GlobalVariable.gUserCategory != "S") 
            {
                CardBalS = SmartBalanceReturn();
                if (CardBalS < TotBillAmt && GlobalVariable.CapYN == "Y")
                {
                    MessageBox.Show("Balace is Not Available as per Order, Card Bal is Only:-" + CardBalS, " System ll not processed for Order");
                    return;
                }
            }

            string SteWardid, SteWardName;
            DataRowView drv = (DataRowView)Cmb_Steward.SelectedItem;
            SteWardid = drv["ServerCode"].ToString();
            SteWardName = drv["ServerName"].ToString();

            CheckDocType = Convert.ToString(GCon.getValue("Select Isnull(KotOrderPrefix,'') as KotOrderPrefix from ServiceLocation_Hdr Where LocCode = " + Loccode + ""));

            try
            {
                if (UpdFlag == false)
                {
                    //DateTime Sdate = Convert.ToDateTime(GCon.getValue("SELECT SERVERDATE FROM VIEW_SERVER_DATETIME"));
                    DateTime Sdate = Convert.ToDateTime(GlobalVariable.ServerDate);
                    //KotNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' ")) + 1;
                    //KotNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' AND DOCTYPE = '" + DocType + "' ")) + 1;
                    KotNo = Convert.ToInt16(GCon.getValue("SELECT  ISNULL(MAX(Cast(SUBSTRING(KOTno,1,6) As Numeric)),0)  FROM KOT_HDR  WHERE SALETYPE ='SALE' AND ISNULL(TTYPE,'') <> 'S' AND DOCTYPE = '" + DocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ")) + 1;
                    //kotdetails = DocType+"/"+ KotNo.ToString("000000") + "/" + FinYear;
                    kotNo1 = KotNo.ToString("000000");
                    kotdetails = DocType + "/" + KotNo.ToString("000000");
                    if (GlobalVariable.gCompName == "CSC") 
                    {
                        KotNo = Convert.ToInt32(GCon.getValue("SELECT Isnull(DocNo,0) FROM PoSKotDoc Where DocType = 'KOT' ")) + 1;
                        kotNo1 = KotNo.ToString("000000");
                        kotdetails = DocType + "/" + KotNo.ToString("000000") + "/" + FinYear;
                    }
                    CheckNumberInt = Convert.ToInt32(GCon.getValue("SELECT ISNULL(MAX(Cast(SUBSTRING(CheckNo,len('" + CheckDocType + "')+2,6) As Numeric)),0)+1  FROM kot_det  WHERE substring(CheckNo,1,len('" + CheckDocType + "')) = '" + CheckDocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                    CheckNo = CheckDocType + "/" + CheckNumberInt.ToString("000000");
                    if (TabBill == "N") { GlobalVariable.ChairNo = KotNo; }
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        dataGridView1.Rows[i].Cells[9].Value = i + 1;

                        if (dataGridView1.Rows[i].Cells[10].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[10].Value = 0; }

                        if (dataGridView1.Rows[i].Cells[7].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[7].Value = ""; }

                        dataGridView1.Rows[i].Cells[11].Value = 1;

                        if (dataGridView1.Rows[i].Cells[13].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[13].Value = ""; }

                        if (dataGridView1.Rows[i].Cells[14].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[14].Value = dataGridView1.Rows[i].Cells[2].Value; }

                        if (dataGridView1.Rows[i].Cells[15].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[15].Value = "N"; }

                        if (dataGridView1.Rows[i].Cells[17].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[17].Value = 0; }
                    }
                    //Kot_Hdr
                    sqlstring = "INSERT INTO KOT_HDR (FinYear,KotNo,Kotdetails,Kotdate,DocType,SaleType,AccountCode,SLCode,MCode,Mname,CARDHOLDERCODE,CARDHOLDERNAME,TableNo,STCode,Receiptstatus,ServerName,Covers,Pricetype,";
                    sqlstring = sqlstring + "PaymentType,SubPaymentMode,ServiceType,Postingtype,Remarks,Adduserid,Adddatetime,upduserid,BillStatus,ServerCode,DelFlag,Manualbillstatus,DiscountAmt,PackAmt,TipsAmt,AdCgsAmt,PartyAmt,RoomAmt,SerType,LocCode,LocName,ChairSeqNo,[16_DIGIT_CODE],STWCODE,STWNAME)";
                    sqlstring = sqlstring + " VALUES('" + FinYear1 + "','" + kotNo1 + "','" + kotdetails + "','" + Sdate.ToString("dd-MMM-yyyy") + "','" + DocType + "','SALE','','" + MemberCode + "','" + MemberCode + "','" + MemberName + "','" + CardHolderCode + "','" + CardHolderName + "','" + GlobalVariable.TableNo + "','','N','','" + Pax + "','N',";
                    sqlstring = sqlstring + " '','','SALE','AUTO','" + Txt_Remarks.Text + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','','PO','','N','N',0,0,0,0,0,0,'" + GlobalVariable.ServiceType + "'," + Loccode + ",'" + GlobalVariable.SLocation + "'," + GlobalVariable.ChairNo + ",'" + DigitCode + "','" + SteWardid + "','" + SteWardName + "') ";
                    List.Add(sqlstring);
                    //Kot_Det
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        sqlstring = "INSERT INTO KOT_det (FinYear,KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                        sqlstring = sqlstring + "TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode,";
                        sqlstring = sqlstring + "TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,QTY2,BusinessSource,HAPPYSTATUS,ServiceOrder,ModifierCharges,CheckNo)";
                        sqlstring = sqlstring + " VALUES('" + FinYear1 + "','" + kotNo1 + "','" + kotdetails + "','" + Sdate.ToString("dd-MMM-yyyy") + "','','','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','','" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "', ";
                        sqlstring = sqlstring + " '" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[2].Value + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value) + ",'SALES','',0, ";
                        sqlstring = sqlstring + " '','N','" + MemberCode + "','','" + Pax + "','" + GlobalVariable.TableNo + "','" + DocType + "','','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','',0,0,'" + dataGridView1.Rows[i].Cells[13].Value.ToString() + "','', ";
                        sqlstring = sqlstring + " 0,0,0,0,0,0,0,0,''," + Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value) + ",'" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "'," + Convert.ToInt16(dataGridView1.Rows[i].Cells[11].Value) + "," + dataGridView1.Rows[i].Cells[14].Value + ",'" + Cmb_BSource.Text + "','" + dataGridView1.Rows[i].Cells[15].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[16].Value.ToString() + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value) + ",'" + CheckNo + "') ";
                        List.Add(sqlstring);
                    }
                    //Tax Details
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        object ItemTypeCode = GCon.getValue("select Distinct TAXTYPECODE from CHARGEMASTER Where Chargecode = '" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "'");
                        Rate = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                        Qty = Convert.ToInt16(dataGridView1.Rows[i].Cells[2].Value);
                        Slno = Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value);
                        sqlstring = "SELECT ItemTypeCode,TaxCode,TAXON,TaxPercentage FROM ITEMTYPEMASTER WHERE ItemTypeCode = '" + ItemTypeCode + "' ORDER BY TAXON";
                        DataTable TAXON = new DataTable();
                        TAXON = GCon.getDataSet(sqlstring);
                        if (TAXON.Rows.Count > 0)
                        {
                            Zero1 = 0; ZeroA1 = 0; ZeroB1 = 0; One1 = 0; OneA1 = 0; OneB1 = 0; Two1 = 0; TwoA1 = 0; TwoB1 = 0; Three1 = 0; ThreeA1 = 0; ThreeB1 = 0;
                            GZero1 = 0; GZeroA1 = 0; GZeroB1 = 0; GOne1 = 0; GOneA1 = 0; GOneB1 = 0; GTwo1 = 0; GTwoA1 = 0; GTwoB1 = 0; GThree1 = 0; GThreeA1 = 0; GThreeB1 = 0;
                            for (int j = 0; j < TAXON.Rows.Count; j++)
                            {
                                DataRow dr = TAXON.Rows[j];
                                IType = Convert.ToString(dr["ItemTypeCode"]);
                                Taxcode = Convert.ToString(dr["Taxcode"]);
                                Taxon = Convert.ToString(dr["TAXON"]);
                                TPercent = Convert.ToDouble(dr["TaxPercentage"]);
                                sqlstring = "INSERT INTO KOT_DET_TAX (FinYear,KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO) VALUES ( ";
                                sqlstring = sqlstring + " '" + FinYear1 + "', '" + kotdetails + "','" + Sdate.ToString("dd-MMM-yyyy") + "','SALE','" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "','" + Strings.Trim(IType) + "'";
                                if (dataGridView1.Rows[i].Cells[5].Value != null)
                                {
                                    sqlstring = sqlstring + ",'" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'"; //Poscode
                                }
                                else { sqlstring = sqlstring + ",''"; }
                                if (dataGridView1.Rows[i].Cells[0].Value != null)
                                {
                                    sqlstring = sqlstring + ",'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'"; //Itemcode
                                }
                                else { sqlstring = sqlstring + ",''"; }
                                sqlstring = sqlstring + ",'N','" + Taxcode + "','" + Taxon + "'";
                                sqlstring = sqlstring + ",'" + Rate + "'"; //rate
                                sqlstring = sqlstring + ",'" + Qty + "'"; //qty
                                sqlstring = sqlstring + ",'" + TPercent + "',";
                                if (dr["TAXON"].ToString() == "0")
                                {
                                    Zero1 = (Rate * TPercent) / 100;
                                    GZero1 = GZero1 + Zero1;
                                    sqlstring = sqlstring + "'" + Zero1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "0A")
                                {
                                    ZeroA1 = (GZero1 * TPercent) / 100;
                                    GZeroA1 = GZeroA1 + ZeroA1;
                                    sqlstring = sqlstring + "'" + ZeroA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "0B")
                                {
                                    ZeroB1 = ((GZero1 + GZeroA1) * TPercent) / 100;
                                    GZeroB1 = GZeroB1 + ZeroB1;
                                    sqlstring = sqlstring + "'" + ZeroB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1")
                                {
                                    One1 = ((Rate + GZero1 + GZeroA1) * TPercent) / 100;
                                    GOne1 = GOne1 + One1;
                                    sqlstring = sqlstring + "'" + One1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1A")
                                {
                                    OneA1 = (One1 * TPercent) / 100;
                                    GOneA1 = GOneA1 + OneA1;
                                    sqlstring = sqlstring + "'" + OneA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1B")
                                {
                                    OneB1 = ((GOne1 + GOneA1) * TPercent) / 100;
                                    GOneB1 = GOneB1 + OneB1;
                                    sqlstring = sqlstring + "'" + OneB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2")
                                {
                                    Two1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1) * TPercent) / 100;
                                    GTwo1 = GTwo1 + Two1;
                                    sqlstring = sqlstring + "'" + Two1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2A")
                                {
                                    TwoA1 = (Two1 * TPercent) / 100;
                                    GTwoA1 = GTwoA1 + TwoA1;
                                    sqlstring = sqlstring + "'" + TwoA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2B")
                                {
                                    TwoB1 = ((GTwo1 + GTwoA1) * TPercent) / 100;
                                    GTwoB1 = GTwoB1 + TwoB1;
                                    sqlstring = sqlstring + "'" + TwoB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3")
                                {
                                    Three1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1 + GTwo1 + GTwoA1) * TPercent) / 100;
                                    GThree1 = GThree1 + Three1;
                                    sqlstring = sqlstring + "'" + Three1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3A")
                                {
                                    ThreeA1 = (Three1 * TPercent) / 100;
                                    GThreeA1 = GThreeA1 + ThreeA1;
                                    sqlstring = sqlstring + "'" + ThreeA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3B")
                                {
                                    ThreeB1 = ((GThree1 + GThreeA1) * TPercent) / 100;
                                    GThreeB1 = GThreeB1 + ThreeB1;
                                    sqlstring = sqlstring + "'" + ThreeB1 * Qty + "',";
                                }
                                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N',''," + Slno + ")";
                                List.Add(sqlstring);
                            }
                        }
                    }
                    if (MemberCode == "" && HTPhoneNo != "")
                    {
                        sqlstring = "Insert into Tbl_HomeTakeAwayBill (KotNo,GuestName,GuestAdd,MobileNo,AddUser,AddDate) select '" + kotdetails + "',GuestName,GuestAdd,MobileNo,'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "' from Tbl_HomeTakeAwayGuest Where MobileNo = '" + HTPhoneNo + "' ";
                        List.Add(sqlstring);
                    }
                    if (GuestMobno != "" && GuestName != "") 
                    {
                        sqlstring = "Insert into Tbl_HomeTakeAwayBill (KotNo,GuestName,GuestAdd,MobileNo,AddUser,AddDate,GuestGSTIN,GuestPAN,GuestCity,GuestPin) ";
                        sqlstring = sqlstring + " Values ('" + kotdetails + "','" + GuestName + "','','" + GuestMobno + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','','','','' ) ";
                        List.Add(sqlstring);
                    }
                    if (GlobalVariable.gCompName == "CSC") 
                    {
                        sqlstring = "update KOT_hdr set DocType = 'SALE' WHERE KOTDETAILS = '" + kotdetails + "'";
                        List.Add(sqlstring);
                        sqlstring = "Update PoSKotDoc Set DocNo = " + KotNo + ",DOCFLAG='N' Where DocType = 'KOT'";
                        List.Add(sqlstring);
                    }
                }
                else if(UpdFlag == true)
                {
                    DataTable CheckPrintFlag = new DataTable();
                    sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                    CheckPrintFlag = GCon.getDataSet(sql);
                    if (CheckPrintFlag.Rows.Count > 0)
                    {
                        String CheckNC = Convert.ToString(GCon.getValue("SELECT TOP 1 ISNULL(NCFlag,'N') AS NCFlag FROM Kot_Hdr WHERE KOTDETAILS = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' "));
                        if (CheckNC == "N" && Chk_NCApply.Checked == true)
                        { }
                        else 
                        {
                            MessageBox.Show("Check Print Done, You can't Modify");
                            return;
                        }
                    }

                    DateTime Sdate = Convert.ToDateTime(GCon.getValue("SELECT Kotdate FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                    //KotNo = Convert.ToInt16(KOrderNo);
                    KotNo = Convert.ToInt32(GCon.getValue("SELECT KOTNO FROM KOT_HDR WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    kotNo1 = KotNo.ToString("000000");
                    kotdetails = KOrderNo;
                    string kotstatus = "N";
                    SeZFalg = Convert.ToString(GCon.getValue("SELECT Isnull(SezFlag,'N') as SezFlag FROM Tbl_HomeTakeAwayBill Where KotNo = '" + KOrderNo + "'"));
                    if (SeZFalg == "Y") { MessageBox.Show("SEZ Tagging done, for Order Updation first untag SEZ"); return; }
                    int NOdrNo = Convert.ToInt32(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0)+1 as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                    //sql = "SELECT ISNULL(MAX(Cast(SUBSTRING(CheckNo,len('" + CheckDocType + "')+2,6) As Numeric)),0)+1  FROM kot_det  WHERE substring(CheckNo,1,len('" + CheckDocType + "')) = '" + CheckDocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                    CheckNumberInt = Convert.ToInt32(GCon.getValue("SELECT ISNULL(MAX(Cast(SUBSTRING(CheckNo,len('" + CheckDocType + "')+2,6) As Numeric)),0)+1  FROM kot_det  WHERE substring(CheckNo,1,len('" + CheckDocType + "')) = '" + CheckDocType + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'"));
                    CheckNo = CheckDocType + "/" + CheckNumberInt.ToString("000000");
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        dataGridView1.Rows[i].Cells[9].Value = i + 1;

                        if (dataGridView1.Rows[i].Cells[10].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[10].Value = 0; }

                        if (dataGridView1.Rows[i].Cells[7].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[7].Value = ""; }

                        if (dataGridView1.Rows[i].Cells[11].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[11].Value = NOdrNo; }

                        if (dataGridView1.Rows[i].Cells[13].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[13].Value = ""; }

                        if (dataGridView1.Rows[i].Cells[14].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[14].Value = dataGridView1.Rows[i].Cells[2].Value; }

                        if (dataGridView1.Rows[i].Cells[15].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[15].Value = "N"; }

                        if (dataGridView1.Rows[i].Cells[17].Value != null)
                        { }
                        else { dataGridView1.Rows[i].Cells[17].Value = 0; }
                    }
                    //Kot_Hdr
                    sqlstring = "UPDATE KOT_HDR SET Kotdate='" + Sdate.ToString("dd-MMM-yyyy") + "',SLCode='" + MemberCode + "',MCode='" + MemberCode + "',Mname='" + MemberName + "',CARDHOLDERCODE='" + CardHolderCode + "',CARDHOLDERNAME='" + CardHolderName + "',TableNo='" + GlobalVariable.TableNo + "',STCode='',ServerName='',Covers=" + Pax + ",";
                    sqlstring = sqlstring + "PaymentType='',Postingtype='AUTO',Remarks='" + Txt_Remarks.Text + "',upduserid='" + GlobalVariable.gUserName + "',Upddatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',BillStatus='PO',ServerCode='',DelFlag='N',Manualbillstatus='N',DiscountAmt=0,PackAmt=0,TipsAmt=0,AdCgsAmt=0,PartyAmt=0,RoomAmt=0,SerType='" + GlobalVariable.ServiceType + "',LocCode=" + Loccode + ",LocName='" + GlobalVariable.SLocation + "',ChairSeqNo=" + GlobalVariable.ChairNo + ",[16_DIGIT_CODE] = '" + DigitCode + "',STWCODE = '" + SteWardid + "',STWNAME = '" + SteWardName + "' ";
                    sqlstring = sqlstring + " Where KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    //Kot_Det
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[10].Value) > 0)
                        {
                            if (dataGridView1.Rows[i].Cells[12].Value != null) {kotstatus =  dataGridView1.Rows[i].Cells[12].Value.ToString(); }
                            else { kotstatus = "N"; }
                            sqlstring = "UPDATE KOT_det Set KOTDATE = '" + Sdate.ToString("dd-MMM-yyyy") + "',BILLDETAILS='',CATEGORY='',ITEMCODE='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "',ITEMDESC='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "',GROUPCODE='',ITEMTYPE='" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "',POSCODE='" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "',UOM='" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "',QTY=" + dataGridView1.Rows[i].Cells[2].Value + ",RATE=" + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) + ",AMOUNT=" + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value) + ",TAXTYPE='SALES',TAXCODE='',TAXAMOUNT=0,";
                            sqlstring = sqlstring + "TAXACCOUNTCODE='',KOTSTATUS='" + kotstatus + "',MCODE='" + MemberCode + "',SCODE='',COVERS=" + Pax + ",TABLENO='" + GlobalVariable.TableNo + "',PAYMENTMODE='',DelFlag='N',UpdUserid='" + GlobalVariable.gUserName + "',Upddatetime='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "',PACKPERCENT=0,PACKAMOUNT=0,PROMOTIONALST='" + dataGridView1.Rows[i].Cells[13].Value.ToString() + "',SUBGroupCode='',";
                            sqlstring = sqlstring + "TIPSPER=0,TipsAmt=0,AdCgsPer=0,AdCgsAmt=0,PartyPer=0,PartyAmt=0,RoomPer=0,RoomAmt=0,MKOTNO='',SLNO=" + Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value) + ",OrderNo=" + Convert.ToInt16(dataGridView1.Rows[i].Cells[11].Value) + ",Modifier='" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "',QTY2=" + dataGridView1.Rows[i].Cells[14].Value + ",BusinessSource = '" + Cmb_BSource.Text + "',HAPPYSTATUS = '" + dataGridView1.Rows[i].Cells[15].Value.ToString() + "',ModifierCharges = " + Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value) + " Where KOTDETAILS = '" + KOrderNo + "' AND ITEMCODE = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "' AND AUTOID = " + dataGridView1.Rows[i].Cells[10].Value + " AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                            List.Add(sqlstring);
                        }
                        else 
                        {
                            sqlstring = "INSERT INTO KOT_det (FinYear,KOTNO,KOTDETAILS,KOTDATE,BILLDETAILS,CATEGORY,ITEMCODE,ITEMDESC,GROUPCODE,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,TAXTYPE,TAXCODE,TAXAMOUNT,";
                            sqlstring = sqlstring + "TAXACCOUNTCODE,KOTSTATUS,MCODE,SCODE,COVERS,TABLENO,KOTTYPE,PAYMENTMODE,DelFlag,AddUserid,Adddatetime,UpdUserid,PACKPERCENT,PACKAMOUNT,PROMOTIONALST,SUBGroupCode,";
                            sqlstring = sqlstring + "TIPSPER,TipsAmt,AdCgsPer,AdCgsAmt,PartyPer,PartyAmt,RoomPer,RoomAmt,MKOTNO,SLNO,Modifier,OrderNo,QTY2,BusinessSource,HAPPYSTATUS,ServiceOrder,ModifierCharges,CheckNo)";
                            sqlstring = sqlstring + " VALUES('" + FinYear1 + "','" + kotNo1 + "','" + kotdetails + "','" + Sdate.ToString("dd-MMM-yyyy") + "','','','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','','" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "', ";
                            sqlstring = sqlstring + " '" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[2].Value + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value) + ",'SALES','',0, ";
                            sqlstring = sqlstring + " '','N','" + MemberCode + "','','" + Pax + "','" + GlobalVariable.TableNo + "','" + DocType + "','','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','',0,0,'" + dataGridView1.Rows[i].Cells[13].Value.ToString() + "','', ";
                            sqlstring = sqlstring + " 0,0,0,0,0,0,0,0,''," + Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value) + ",'" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "'," + Convert.ToInt16(dataGridView1.Rows[i].Cells[11].Value) + "," + dataGridView1.Rows[i].Cells[14].Value + ",'" + Cmb_BSource.Text + "','" + dataGridView1.Rows[i].Cells[15].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[16].Value.ToString() + "," + Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value) + ",'" + CheckNo + "') ";
                            List.Add(sqlstring);
                        }
                    }
                    //Tax Details
                    sqlstring = "DELETE FROM KOT_DET_TAX WHERE KOTDETAILS = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                    List.Add(sqlstring);
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        object ItemTypeCode = GCon.getValue("select Distinct TAXTYPECODE from CHARGEMASTER Where Chargecode = '" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "'");
                        Rate = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                        Qty = Convert.ToInt16(dataGridView1.Rows[i].Cells[2].Value);
                        Slno = Convert.ToInt16(dataGridView1.Rows[i].Cells[9].Value);

                        if (dataGridView1.Rows[i].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[i].Cells[12].Value.ToString(); }
                        else { kotstatus = "N"; }

                        sqlstring = "SELECT ItemTypeCode,TaxCode,TAXON,TaxPercentage FROM ITEMTYPEMASTER WHERE ItemTypeCode = '" + ItemTypeCode + "' ORDER BY TAXON";
                        DataTable TAXON = new DataTable();
                        TAXON = GCon.getDataSet(sqlstring);
                        if (TAXON.Rows.Count > 0)
                        {
                            Zero1 = 0; ZeroA1 = 0; ZeroB1 = 0; One1 = 0; OneA1 = 0; OneB1 = 0; Two1 = 0; TwoA1 = 0; TwoB1 = 0; Three1 = 0; ThreeA1 = 0; ThreeB1 = 0;
                            GZero1 = 0; GZeroA1 = 0; GZeroB1 = 0; GOne1 = 0; GOneA1 = 0; GOneB1 = 0; GTwo1 = 0; GTwoA1 = 0; GTwoB1 = 0; GThree1 = 0; GThreeA1 = 0; GThreeB1 = 0;
                            for (int j = 0; j < TAXON.Rows.Count; j++)
                            {
                                DataRow dr = TAXON.Rows[j];
                                IType = Convert.ToString(dr["ItemTypeCode"]);
                                Taxcode = Convert.ToString(dr["Taxcode"]);
                                Taxon = Convert.ToString(dr["TAXON"]);
                                TPercent = Convert.ToDouble(dr["TaxPercentage"]);
                                sqlstring = "INSERT INTO KOT_DET_TAX (FinYear,KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,VOIDUSER,SLNO) VALUES ( ";
                                sqlstring = sqlstring + " '" + FinYear1 + "','" + kotdetails + "','" + Sdate.ToString("dd-MMM-yyyy") + "','SALE','" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "','" + Strings.Trim(IType) + "'";
                                if (dataGridView1.Rows[i].Cells[5].Value != null)
                                {
                                    sqlstring = sqlstring + ",'" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'"; //Poscode
                                }
                                else { sqlstring = sqlstring + ",''"; }
                                if (dataGridView1.Rows[i].Cells[0].Value != null)
                                {
                                    sqlstring = sqlstring + ",'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'"; //Itemcode
                                }
                                else { sqlstring = sqlstring + ",''"; }
                                sqlstring = sqlstring + ",'" + kotstatus + "','" + Taxcode + "','" + Taxon + "'";
                                sqlstring = sqlstring + ",'" + Rate + "'"; //rate
                                sqlstring = sqlstring + ",'" + Qty + "'"; //qty
                                sqlstring = sqlstring + ",'" + TPercent + "',";
                                if (dr["TAXON"].ToString() == "0")
                                {
                                    Zero1 = (Rate * TPercent) / 100;
                                    GZero1 = GZero1 + Zero1;
                                    sqlstring = sqlstring + "'" + Zero1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "0A")
                                {
                                    ZeroA1 = (GZero1 * TPercent) / 100;
                                    GZeroA1 = GZeroA1 + ZeroA1;
                                    sqlstring = sqlstring + "'" + ZeroA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "0B")
                                {
                                    ZeroB1 = ((GZero1 + GZeroA1) * TPercent) / 100;
                                    GZeroB1 = GZeroB1 + ZeroB1;
                                    sqlstring = sqlstring + "'" + ZeroB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1")
                                {
                                    One1 = ((Rate + GZero1 + GZeroA1) * TPercent) / 100;
                                    GOne1 = GOne1 + One1;
                                    sqlstring = sqlstring + "'" + One1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1A")
                                {
                                    OneA1 = (One1 * TPercent) / 100;
                                    GOneA1 = GOneA1 + OneA1;
                                    sqlstring = sqlstring + "'" + OneA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "1B")
                                {
                                    OneB1 = ((GOne1 + GOneA1) * TPercent) / 100;
                                    GOneB1 = GOneB1 + OneB1;
                                    sqlstring = sqlstring + "'" + OneB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2")
                                {
                                    Two1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1) * TPercent) / 100;
                                    GTwo1 = GTwo1 + Two1;
                                    sqlstring = sqlstring + "'" + Two1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2A")
                                {
                                    TwoA1 = (Two1 * TPercent) / 100;
                                    GTwoA1 = GTwoA1 + TwoA1;
                                    sqlstring = sqlstring + "'" + TwoA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "2B")
                                {
                                    TwoB1 = ((GTwo1 + GTwoA1) * TPercent) / 100;
                                    GTwoB1 = GTwoB1 + TwoB1;
                                    sqlstring = sqlstring + "'" + TwoB1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3")
                                {
                                    Three1 = ((Rate + GZero1 + GZeroA1 + GOne1 + GOneA1 + GTwo1 + GTwoA1) * TPercent) / 100;
                                    GThree1 = GThree1 + Three1;
                                    sqlstring = sqlstring + "'" + Three1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3A")
                                {
                                    ThreeA1 = (Three1 * TPercent) / 100;
                                    GThreeA1 = GThreeA1 + ThreeA1;
                                    sqlstring = sqlstring + "'" + ThreeA1 * Qty + "',";
                                }
                                else if (dr["TAXON"].ToString() == "3B")
                                {
                                    ThreeB1 = ((GThree1 + GThreeA1) * TPercent) / 100;
                                    GThreeB1 = GThreeB1 + ThreeB1;
                                    sqlstring = sqlstring + "'" + ThreeB1 * Qty + "',";
                                }
                                sqlstring = sqlstring + "'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "','N',''," + Slno + ")";
                                List.Add(sqlstring);
                            }
                        }
                    }
                }

                if (Chk_NCApply.Checked == false) 
                {
                    sqlstring = "UPDATE KOT_det SET PACKPERCENT = P.packingpercent,PACKAMOUNT = (AMOUNT*ISNULL(P.packingpercent,0))/100,TipsPer = P.tips,TipsAmt = (AMOUNT*ISNULL(P.tips,0))/100, ";
                    sqlstring = sqlstring + " AdCgsPer = P.adcharge,AdCgsAmt = (AMOUNT*ISNULL(P.adcharge,0))/100  FROM posmaster P,KOT_det K WHERE P.poscode = K.POSCODE AND KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }

                sqlstring = " UPDATE KOT_DET SET CATEGORY = I.CATEGORY,GROUPCODE = I.GroupCode,SUBGroupCode = I.SUBGROUPCODE,OPENFACILITYST = Isnull(I.openfacility,'N') FROM ItemMaster I,KOT_det D WHERE I.ItemCode = D.ITEMCODE AND KOTDETAILS = '" + kotdetails + "' AND ISNULL(D.CATEGORY,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                List.Add(sqlstring);

                if (GlobalVariable.gCompName == "NZC" && Txt_BarCode.Text != "") 
                {
                    sqlstring = "UPDATE KOT_DET SET MBCode = '" + (Txt_BarCode.Text) + "' WHERE KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }

                sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,ModifierCharges,QTY,i.TaxPercentage,((ModifierCharges * i.TaxPercentage) /100) as TAXAMT,'" + GlobalVariable.gUserName + "' as ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'MC',FinYear ";
                sqlstring = sqlstring + " from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I WHERE d.ITEMTYPE = c.CHARGECODE And c.TAXTYPECODE = i.ItemTypeCode And KOTDETAILS = '" + kotdetails + "' And FinYear = '" + FinYear1 + "' And isnull(ModifierCharges,0) > 0 ";
                List.Add(sqlstring);

                if (GlobalVariable.gCompName == "SKYYE")
                {
                    sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                    sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,PACKAMOUNT,QTY,i.TaxPercentage,((PACKAMOUNT * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'PP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.pos and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(PACKAMOUNT,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,TipsAmt,QTY,i.TaxPercentage,((TipsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'TP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE d.POSCODE = p.pos and c.TAXTYPECODE = i.ItemTypeCode  and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(TipsAmt,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,D.ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,AdCgsAmt,QTY,i.TaxPercentage,((AdCgsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'AD',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,PosMenuLink p WHERE  c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.pos and c.CHARGECODE = p.TAXONITEM AND D.ITEMCODE = P.ItemCode ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(AdCgsAmt,0) > 0  ";
                    List.Add(sqlstring);
                }
                else 
                {
                    sqlstring = "Insert Into kot_det_tax (KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,TYPE_CODE,POSCODE,ITEMCODE,KOTSTATUS,TAXCODE,TAXON,RATE,QTY,TAXPERCENT,TAXAMT,ADD_USER,ADD_DATE,VOID,SLNO,Trans_Flag,FinYear) ";
                    sqlstring = sqlstring + " select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,PACKAMOUNT,QTY,i.TaxPercentage,((PACKAMOUNT * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'PP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.poscode and c.CHARGECODE = p.PChgCode  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(PACKAMOUNT,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,TipsAmt,QTY,i.TaxPercentage,((TipsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'TP',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE d.POSCODE = p.poscode and c.TAXTYPECODE = i.ItemTypeCode  and c.CHARGECODE = p.TipsChgCode  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(TipsAmt,0) > 0  Union all  ";
                    sqlstring = sqlstring + "  select KOTDETAILS,KOTDATE,TTYPE,CHARGECODE,ItemTypeCode,d.POSCODE,ITEMCODE,KOTSTATUS,i.TaxCode,TAXON,AdCgsAmt,QTY,i.TaxPercentage,((AdCgsAmt * i.TaxPercentage) /100) as TAXAMT, ";
                    sqlstring = sqlstring + " '" + GlobalVariable.gUserName + "' ADD_USER,getdate() as ADD_DATE,DelFlag,SLNO,'AD',FinYear  from kot_det d,CHARGEMASTER c,ITEMTYPEMASTER I,posmaster p WHERE  c.TAXTYPECODE = i.ItemTypeCode and d.POSCODE = p.poscode and c.CHARGECODE = p.AdChgChgCode  ";
                    sqlstring = sqlstring + " And KOTDETAILS = '" + kotdetails + "'  And FinYear = '" + FinYear1 + "' And isnull(AdCgsAmt,0) > 0  ";
                    List.Add(sqlstring);
                }

                if (Chk_NCApply.Checked == true)
                {
                    sqlstring = " UPDATE Kot_Hdr SET NCFlag = 'Y',NCCategory = '" + Cmb_NCCategory.Text + "',TABLELOC = " + Loccode + " ,TABLEPAX = " + Pax + " WHERE KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    if (GlobalVariable.gCompName == "CSC") { }
                    else 
                    {
                        sqlstring = " UPDATE KOT_DET_TAX SET TAXAMT = 0 WHERE KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'  ";
                        List.Add(sqlstring);
                    }
                }
                else 
                {
                    sqlstring = " UPDATE Kot_Hdr SET NCFlag = 'N',NCCategory = '',TABLELOC = " + Loccode + " ,TABLEPAX = " + Pax + " WHERE KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                }

                if (GlobalVariable.gCompName == "HPRC") 
                {
                    NCFlagHPRC = "N";
                    DataTable NCMember = new DataTable();
                    DataTable NCCardCode = new DataTable();
                    NCMember = GCon.getDataSet("SELECT mcode,mname FROM membermaster WHERE mcode = '" + MemberCode + "' and curentstatus in('LIVE','ACTIVE') and membertypecode in(select subtypecode from subcategorymaster where isnull(clubaccount,'')='YES')");
                    if (NCMember.Rows.Count > 0)
                    {
                        NCFlagHPRC = "Y";
                    }
                    NCCardCode = GCon.getDataSet("SELECT * FROM SM_CARDFILE_HDR WHERE CARDCODE = '" + CardHolderCode + "' AND ISNULL(CLUB_ACCOUNT_YN,'') = 'Y' ");
                    if (NCCardCode.Rows.Count > 0)
                    {
                        NCFlagHPRC = "Y";
                    }
                    if (NCFlagHPRC == "Y") 
                    {
                        sqlstring = "UPDATE KOT_DET_TAX SET TAXAMT = 0 WHERE KOTDETAILS IN (SELECT KOTDETAILS FROM KOT_det WHERE KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' AND MCODE = '" + MemberCode + "')  AND KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                    }
                }

                if (GlobalVariable.gCompName == "CSC") 
                {
                    sqlstring = "UPDATE KOT_DET SET GSTFlagKot = ISNULL(P.GSTFlag,'NO'),TaxPerc = 0,SerPer = 0 FROM PosMenuLink P,KOT_DET D WHERE P.Pos = D.POSCODE AND P.ItemCode = D.ITEMCODE AND D.KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                    List.Add(sqlstring);
                    sqlstring = "UPDATE KOT_DET SET SCODE = '" + SteWardid + "' Where KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                    List.Add(sqlstring);
                    sqlstring = "UPDATE Kot_Hdr SET STCode = '" + SteWardid + "',ServerName = '" + SteWardName + "' Where KOTDETAILS = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                    List.Add(sqlstring);
                }

                sqlstring = "DELETE FROM CLOSINGQTY WHERE Trnno = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                GCon.dataOperation(1, sqlstring);
                sqlstring = "DELETE FROM SUBSTORECONSUMPTIONDETAIL WHERE Docdetails = '" + kotdetails + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "'";
                List.Add(sqlstring);
                string ItemCode = "", PosItemCode = "", PosItemUom = "", VarPosCode = "", SUBSTORECODE = "", uom1 = "", kotST="N";
                Double ClQty = 0, ClValue = 0, dblCalqty = 0, convvalue = 0, clsrate = 0, AVGRATE=0 ;
                DataTable Store = new DataTable();
                DataTable BOMDet = new DataTable();
                DataTable Closing = new DataTable();
                for (i = 0; i < dataGridView1.RowCount - 1; i++) 
                {
                    SUBSTORECODE = "";
                    ItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemUom = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    VarPosCode = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    if (dataGridView1.Rows[i].Cells[12].Value != null) { kotST = dataGridView1.Rows[i].Cells[12].Value.ToString(); }
                    else { kotST = "N"; }
                    SUBSTORECODE = Convert.ToString(GCon.getValue("SELECT STORECODE FROM POSITEMSTORELINK WHERE POS='" + (VarPosCode) + "' AND ITEMCODE='" + PosItemCode + "'"));
                    sqlstring = "SELECT STOREDESC FROM STOREMASTER WHERE STORECODE='" + (SUBSTORECODE) + "' AND ISNULL(FREEZE,'') <> 'Y'";
                    Store = GCon.getDataSet(sqlstring);
                    if (Store.Rows.Count > 0) 
                    {
                        sql = "SELECT GITEMCODE,GITEMNAME,GUOM,GQTY,ISNULL(CTYN,'N') AS CTYN,GRATE,GAMOUNT,GDBLAMT,ISNULL(GHIGHRATIO,0) AS GHIGHRATIO,ISNULL(GGROUPCODE,'')AS GGROUPCODE,ISNULL(GSUBGROUPCODE,'') AS GSUBGROUPCODE,VOID FROM BOM_DET WHERE ITEMCODE='" + PosItemCode + "' AND ITEMUOM='" + PosItemUom + "' AND ISNULL(VOID,'') <> 'Y'";
                        BOMDet = GCon.getDataSet(sql);
                        if (BOMDet.Rows.Count > 0) 
                        {
                            for (int K = 0; K < BOMDet.Rows.Count; K++) 
                            {
                                DataRow dr = BOMDet.Rows[K];

                                sqlstring = "INSERT INTO SUBSTORECONSUMPTIONDETAIL(FinYear,Docno,Docdetails,Docdate,Storelocationcode,STORELOCATIONNAME,";
                                sqlstring = sqlstring + " Itemcode,Itemname,Uom,Qty,Rate,Amount,";
                                sqlstring = sqlstring + " Dblamt,Highratio,Groupcode,Subgroupcode,Void,Adduser,adddatetime,Updateuser,Updatetime)";
                                sqlstring = sqlstring + " VALUES ('" + FinYear1 + "','" + Strings.Trim(Convert.ToString(kotNo1)) + "','" + Strings.Trim(Convert.ToString(kotdetails)) + "',";
                                sqlstring = sqlstring + " '" + Strings.Format(GlobalVariable.ServerDate, "dd-MMM-yyyy hh:mm:ss") + "',";
                                sqlstring = sqlstring + " '" + Strings.Trim(SUBSTORECODE) + "',";
                                sqlstring = sqlstring + " '" + Strings.Trim(SUBSTORECODE) + "',";
                                sqlstring = sqlstring + " '" + Strings.Trim(BOMDet.Rows[K]["GITEMCODE"].ToString()) + "',";
                                sqlstring = sqlstring + " '" + Strings.Trim(BOMDet.Rows[K]["GITEMNAME"].ToString()) + "',";
                                sqlstring = sqlstring + " '" + Strings.Trim(BOMDet.Rows[K]["GUOM"].ToString()) + "',";
                                dblCalqty = double.Parse(dataGridView1.Rows[i].Cells[2].Value + "");
                                sqlstring = sqlstring + dblCalqty * double.Parse((BOMDet.Rows[K]["GQTY"].ToString()) + "") + ",";
                                AVGRATE = 0;
                                sqlstring = sqlstring + AVGRATE + ",";
                                sqlstring = sqlstring + dblCalqty * double.Parse(BOMDet.Rows[K]["GQTY"].ToString()) * AVGRATE + ",";
                                sqlstring = sqlstring + dblCalqty * double.Parse(BOMDet.Rows[K]["GDBLAMT"].ToString()) + ",";
                                sqlstring = sqlstring + double.Parse(BOMDet.Rows[K]["GHIGHRATIO"].ToString()) + ",";
                                sqlstring = sqlstring + " '" + (Strings.Trim(BOMDet.Rows[K]["GGROUPCODE"].ToString()) + "") + "',";
                                sqlstring = sqlstring + " '" + (Strings.Trim(BOMDet.Rows[K]["GSUBGROUPCODE"].ToString()) + "") + "',";
                                sqlstring = sqlstring + "'" + kotST + "',";
                                sqlstring = sqlstring + " '','" + Strings.Format(DateTime.Today, "dd-MMM-yyyy hh:mm:ss") + "',";
                                sqlstring = sqlstring + " ' ','" + Strings.Format(DateTime.Today, "dd-MMM-yyyy HH:mm:ss") + "')";
                                List.Add(sqlstring);

                                if (kotST == "" || kotST == "N")
                                {
                                    sql = "select top(1) ISNULL(closingstock,0) AS closingstock,isnull(closingvalue,0) as closingvalue,uom from closingqty where itemcode='" + Convert.ToString(dr["GITEMCODE"]) + "' and storecode='" + SUBSTORECODE + "'  and trndate<= Getdate() order by AUTOID desc";
                                    Closing = GCon.getDataSet(sql);
                                    if (Closing.Rows.Count > 0)
                                    {
                                        uom1 = Closing.Rows[0].ItemArray[2].ToString();
                                        convvalue = Convert.ToDouble(GCon.getValue("select convvalue from INVENTORY_TRANSCONVERSION where baseuom='" + uom1 + "' and transuom='" + Convert.ToString(dr["GUOM"]) + "'"));
                                        ClQty = Convert.ToDouble(Closing.Rows[0].ItemArray[0]) * convvalue;
                                        ClValue = Convert.ToDouble(Closing.Rows[0].ItemArray[1]);
                                    }
                                    sqlstring = "insert into closingqty(FinYear,Trnno,itemcode,uom,storecode,Trndate,openningstock,openningvalue,qty,closingstock,closingvalue,batchyn,ttype,batchno,rate)";
                                    sqlstring = sqlstring + " Values ('" + FinYear1 + "','" + Strings.Trim(Convert.ToString(kotdetails)) + "','" + Strings.Trim(BOMDet.Rows[K]["GITEMCODE"].ToString()) + "',";
                                    sqlstring = sqlstring + "'" + Strings.Trim(uom1) + "','" + Strings.Trim(SUBSTORECODE) + "',";
                                    sqlstring = sqlstring + "'" + Strings.Format(DateTime.Today, "dd-MMM-yyyy HH:mm:ss") + "','" + double.Parse(ClQty.ToString()) + "','" + double.Parse(ClValue.ToString()) + "',";
                                    dblCalqty = double.Parse(dataGridView1.Rows[i].Cells[2].Value + "");
                                    dblCalqty = dblCalqty * double.Parse(BOMDet.Rows[K]["GQTY"].ToString());
                                    sqlstring = sqlstring + ((dblCalqty * (convvalue)) * -1) + ",";
                                    sqlstring = sqlstring + (ClQty - (dblCalqty * (convvalue))) + "," + double.Parse("0") + ",'N','KOT',''," + (clsrate) + ")";
                                    List.Add(sqlstring);
                                }
                            }
                        }
                    }
                }
               
                
                if (GCon.Moretransaction(List) > 0)
                {
                    //MessageBox.Show("Transaction Completed ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TrnDone = true;
                    List.Clear();
                    sqlstring = sqlstring = "EXEC Update_Kot_DetHdr '" + (kotdetails) + "'";
                    List.Add(sqlstring);

                    if (UpdFlag == false)
                    {
                        sqlstring = "SET IDENTITY_INSERT [dbo].[PosTableStatus] off ";
                        sqlstring = sqlstring + "Insert into PosTableStatus (LocCode,TableNo,Ttype,Mcode,Mname,CardCode,TotalPax,TotalKot,OccupiedFrom,Remarks,AddUserId,AddDateTime,TotalAmt)";
                        sqlstring = sqlstring + " select LocCode,TableNo,'CLUBMEMBER',Mcode,Mname,CARDHOLDERCODE,Covers,1,GETDATE(),Remarks,'" + GlobalVariable.gUserName + "',getdate(),BillAmount from Kot_Hdr where Kotdetails = '" + kotdetails + "'";
                        //List.Add(sqlstring);
                        GCon.dataOperation(1, sqlstring);
                    }
                    else 
                    {
                        sqlstring = sqlstring + "update PosTableStatus set TotalAmt = BillAmount from Kot_Hdr H,PosTableStatus P where h.LocCode = p.LocCode and p.TableNo = h.TableNo  ";
                        sqlstring = sqlstring + " and H.Kotdetails = '" + kotdetails + "' AND FinYear = '" + FinYear1 + "' ";
                        List.Add(sqlstring);
                    }

                    sqlstring = "Insert into KotItemAddCancel (KOTDETAILS,ITEMCODE,SLNO,OrderNo,QTY,Flag,Adduser,AddDate) ";
                    sqlstring = sqlstring + " select DISTINCT D.KOTDETAILS,D.ITEMCODE,SLNO,OrderNo,(ISNULL(QTY,0)-ISNULL(QTY2,QTY)) AS QTY,'ADD','" + GlobalVariable.gUserName + "',getdate() from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode WHERE H.KOTDETAILS = '" + (kotdetails) + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) UNION ALL ";
                    sqlstring = sqlstring + " select DISTINCT D.KOTDETAILS,D.ITEMCODE,SLNO,OrderNo,(ISNULL(QTY2,QTY)-ISNULL(QTY,0)) AS QTY,'CANCEL','" + GlobalVariable.gUserName + "',getdate() from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode WHERE H.KOTDETAILS = '" + (kotdetails) + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY)";
                    List.Add(sqlstring);

                    if (GCon.Moretransaction(List) > 0)
                    {
                        List.Clear();
                    }
                    gPrint = true;
                    //PrintToKitchen(kotdetails);

                    EntryPrintDialog EPD = new EntryPrintDialog(this);
                    EPD.KotOrderNo = kotdetails;
                    EPD.ShowDialog();
                    Cmd_ClearAll_Click(sender, e);

                    if (GlobalVariable.gCompName == "SKYYE")
                    {
                        GCon.SendSMS_SkyyeKot(kotdetails,UpdFlag);
                    }

                    sql = "UPDATE TableMaster SET OpenStatus = '' WHERE Pos = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
                    GCon.dataOperation(1, sql);
                    sql = "UPDATE ServiceLocation_Tables SET OpenStatus = '' WHERE LocCode = '" + Loccode + "' AND TableNo = '" + GlobalVariable.TableNo + "' ";
                    GCon.dataOperation(1, sql);

                    if (GlobalVariable.ServiceType == "Dine-In") 
                    {
                        if (TabBill == "N") 
                        {
                            int Rowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + kotdetails + "' AND isnull(billdetails,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                            if (Rowcnt > 0)
                            {
                                PayForm PF = new PayForm();
                                PF.KOrderNo = kotdetails;
                                PF.TabBill = TabBill;
                                PF.Show();
                                this.Close();
                            }
                        }
                        else 
                        {
                            ServiceLocation SL = new ServiceLocation();
                            SL.Show();
                            this.Close();
                        }
                    }
                    else 
                    {
                        //ServiceType SL = new ServiceType();
                        //SL.Show();
                        //this.Close();
                        if (GlobalVariable.ServiceType == "Direct-BillingNO")
                        {
                            GlobalVariable.ServiceType = "Direct-Billing";
                            DataTable dt = new DataTable();
                            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
                            dt = GCon.getDataSet(sql);
                            if (dt.Rows.Count > 0)
                            {
                                DataTable ChkChair = new DataTable();
                                int ChNo = 1;
                                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                                GlobalVariable.TableNo = "V1";
                                Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                                ChkChair = GCon.getDataSet(sql);
                                if (ChkChair.Rows.Count > 0)
                                {
                                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                                }
                                else { ChNo = 1; }
                                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                                if (RowCnt > 0)
                                {
                                    UpdFlag = true;
                                    GlobalVariable.ChairNo = ChNo;
                                }
                                else
                                {
                                    UpdFlag = false;
                                    GlobalVariable.ChairNo = ChNo;
                                    Pax = 1;
                                }
                                Cmd_D1_DBilling_Click(sender, e);
                            }
                        }
                        else 
                        {
                            int Rowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + kotdetails + "' AND isnull(billdetails,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                            if (Rowcnt > 0)
                            {
                                PayForm PF = new PayForm();
                                PF.KOrderNo = kotdetails;
                                PF.Show();
                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Transaction not completed , Please Try again... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TrnDone = false;
                }
            }
            catch
            {
                throw;
            }
        }

        private void Cmd_Pay_Click(object sender, EventArgs e)
        {
            int Rowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + KOrderNo + "' AND isnull(billdetails,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (Rowcnt > 0) 
            {
                PayForm PF = new PayForm();
                PF.KOrderNo = KOrderNo;
                PF.Show();
                this.Close();
            }
        }

        private void PrintToKitchen(string kotno) 
        {
            string PName = "";
            DataTable PData = new DataTable();
            int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + kotno + "' AND ISNULL(UpdUserid,'') = '' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            sql = "select DISTINCT kitchencode from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo = " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0) 
            {
                for (int i = 0; i < PData.Rows.Count ; i++) 
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    PrintKitchen(kotno, KotPrinterName, PName, NOdrNo);
                }
            }
            PData = new DataTable();
            sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) > ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0) 
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    PrintKitchenAdd(kotno, KotPrinterName, PName, Convert.ToInt16(dr["SLNO"]), Convert.ToString(dr["ItemCode"]));
                }
            }

            PData = new DataTable();
            sql = "select DISTINCT kitchencode,D.ItemCode,SLNO from kot_Det D INNER JOIN KOT_HDR H ON D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') INNER JOIN ItemMaster I ON D.ITEMCODE = I.ItemCode ";
            sql = sql + " WHERE H.KOTDETAILS = '" + kotno + "' And OrderNo <> " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(QTY,0) < ISNULL(QTY2,QTY) AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                for (int i = 0; i < PData.Rows.Count; i++)
                {
                    KotPrinterName = "";
                    KotCompName = "";
                    DataRow dr = PData.Rows[i];
                    PName = Convert.ToString(dr["kitchencode"]);
                    GetPrinter_KOT(PName);
                    PrintKitchenLess(kotno,Convert.ToInt16(dr["SLNO"]),Convert.ToString(dr["ItemCode"]), KotPrinterName, PName);
                }
            }
        }

        public void GetPrinter_KOT(string KitCode)
        {
            OleDbConnection ServerConn = new OleDbConnection();
            OleDbDataAdapter servercmd;
            DataSet getserver = new DataSet();
            DataTable dt = new DataTable();
            string sql, ssql;
            sql = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + GlobalVariable.appPath + "\\DBS_KEY.MDB";
            ServerConn.ConnectionString = sql;
            try
            {
                ServerConn.Open();
                ssql = "SELECT COMPUTERNAME ,PRINTERNAME FROM KotPrinterSetup WHERE POSCODE = '" + (KitCode) + "'";
                servercmd = new OleDbDataAdapter(ssql, ServerConn);
                servercmd.Fill(getserver, "admin");
                dt = getserver.Tables["admin"];
                if (dt.Rows.Count > 0)
                {
                    DataRow da = dt.Rows[0];
                    KotCompName = Convert.ToString(da["Computername"]);
                    KotPrinterName = Convert.ToString(da["printername"]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                ServerConn.Close();
                
            }
        }

        private void PrintKitchen(string kotno,string PrintName,string KitCode,int OrdNo)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "",Remarks = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER,Isnull(H.Remarks,'') as Remarks,Isnull(ServiceOrder,1) as ServiceOrder FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And OrderNo = " + OrdNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME             SORD");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Remarks = RData["Remarks"].ToString();
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-22}{3,4}", "", Strings.Format(RData["QTY"], "0"), Strings.Mid(RData["ITEMDESC"].ToString(), 1, 20),RData["ServiceOrder"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (Remarks != "")
                {
                    Filewrite.WriteLine(Strings.Space(4) + "Remarks  : " + Remarks);
                    Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                }
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    if (PrintName != "") 
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void PrintKitchenAdd(string kotno, string PrintName, string KitCode, int slno,string itemcode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);
            string KitName = "";

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            //int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));
            KitName = Convert.ToString(GCon.getValue("SELECT kitchenName FROM kitchenmaster where kitchenCode = '" + KitCode + "'"));
            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,D.ITEMCODE,D.ITEMDESC,(Isnull(QTY,0)-Isnull(QTY2,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER FROM KOT_DET D,KOT_HDR	H,Itemmaster I WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') and D.ITEMCODE = i.ItemCode AND H.KOTDETAILS = '" + kotno + "' And Slno = " + slno + "  AND ISNULL(KOTSTATUS,'') <> 'Y'  AND kitchencode = '" + KitCode + "' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER " + "[" + KitName + "]");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void PrintKitchenLess(string kotno, int sno, string icode,string PrintName, string KitCode)
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,(Isnull(QTY2,0)-Isnull(QTY,0)) as QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,Isnull(D.reason,'') as reason FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND ISNULL(D.FinYear,'') = ISNULL(H.FinYear,'') AND H.KOTDETAILS = '" + kotno + "' And itemcode = '" + icode + "' And SLNO = " + sno + "  AND ISNULL(KOTSTATUS,'') <> 'Y' AND ISNULL(H.FinYear,'') = '" + FinYear1 + "' Order by ServiceOrder ";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - "ORDER CANCEL".ToString().Length) / 2) + "ORDER CANCEL");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:" + Strings.Mid(Strings.Format(RData["Kotdate"], "dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" + RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length) / 2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", Strings.Format(RData["QTY"], "0"), RData["ITEMDESC"]);
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "", RData["reason"]);
                    Vrowcount = Vrowcount + 2;
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else
                {
                    if (PrintName != "")
                    {
                        GCon.PrintTextFile1(vFilepath, PrintName);
                    }
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }

        private void PrintOperation(string kotno) 
        {
            int rowj = 0;
            int CountItem = 0;
            long Vrowcount = 0;
            string vFilepath = null;
            string vOutfile = null;
            DataTable PData = new DataTable();
            StreamWriter Filewrite = default(StreamWriter);

            VBMath.Randomize();
            vOutfile = Strings.Mid("Ste" + (VBMath.Rnd() * 800000), 1, 8);
            vOutfile = vOutfile + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss");
            vFilepath = Application.StartupPath + @"\Reports\" + vOutfile + ".txt";
            int NOdrNo = Convert.ToInt16(GCon.getValue("select Isnull(Max(isnull(OrderNo,0)),0) as OrderNo from KOT_det where kotdetails = '" + KOrderNo + "'"));

            sql = "SELECT D.KOTNO,D.KOTDETAILS,D.Kotdate,H.Adddatetime,D.Adduserid,LOCNAME,H.TABLENO,H.Covers,ITEMCODE,ITEMDESC,QTY,RATE,AMOUNT,isnull(OrderNo,0) as OrderNo,ISNULL(MODIFIER,'') AS MODIFIER FROM KOT_DET D,KOT_HDR	H WHERE D.KOTDETAILS = H.Kotdetails AND H.KOTDETAILS = '" + kotno + "' And OrderNo = " + NOdrNo + "  AND ISNULL(KOTSTATUS,'') <> 'Y'";
            PData = GCon.getDataSet(sql);
            if (PData.Rows.Count > 0)
            {
                Filewrite = File.AppendText(vFilepath);
                for (rowj = 0; rowj <= PData.Rows.Count - 1; rowj++)
                {
                    CountItem = CountItem + 1;
                    var RData = PData.Rows[rowj];
                    if (Vrowcount == 0)
                    {
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT PRINTER");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "DATE:"+ Strings.Mid(Strings.Format(RData["Kotdate"],"dd-MMM-yyyy"), 1, 20) + Strings.Space(2) + Strings.Mid(Strings.Trim(Strings.Format(RData["Adddatetime"], "T")), 1, 15));
                        Filewrite.WriteLine(Strings.Space(4) + "KOT No: " + RData["KOTDETAILS"] + "  ORDER ID:" + RData["OrderNo"]);
                        Filewrite.WriteLine(Strings.Space(4) + "CREW  : " + RData["Adduserid"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + RData["LOCNAME"] + "/" +  RData["TABLENO"] + "--PAX:" + RData["Covers"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + Strings.Space((33 - RData["LOCNAME"].ToString().Length)/2) + RData["LOCNAME"]);
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Filewrite.WriteLine(Strings.Space(4) + "QTY    ITEM NAME");
                        Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                        Vrowcount = 13;
                    }
                    Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "",Strings.Format(RData["QTY"],"0"), RData["ITEMDESC"]);
                    Vrowcount = Vrowcount + 1;
                    string modifier = RData["MODIFIER"].ToString();
                    if (modifier != "")
                    {
                        Filewrite.WriteLine("{0,-4}{1,-7}{2,-26}", "", "", RData["MODIFIER"]);
                        Vrowcount = Vrowcount + 1;
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    Filewrite.WriteLine("");
                }
                Filewrite.WriteLine(Strings.Space(4) + Strings.StrDup(33, "-"));
                if (gPrint == true)
                {
                    char GS = Strings.Chr(29);
                    char ESC = Strings.Chr(27);
                    String CMD ;
                    CMD = ESC + "i";
                    Filewrite.WriteLine(CMD);
                }
                Filewrite.Close();
                if (gPrint == false)
                {
                    GCon.OpenTextFile(vOutfile);
                }
                else 
                {
                    GCon.PrintTextFile1(vFilepath, GlobalVariable.PrinterName);
                }
            }
        }


        private void Cmd_ChangePT_Click(object sender, EventArgs e)
        {
            gPrint = false;
            //PrintOperation(KOrderNo);
            PrintToKitchen(KOrderNo);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataIndexNo = dataGridView1.Rows[e.RowIndex].Index.ToString();
            string cellValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string kotstatus = "N";

            if (GlobalVariable.gCompName == "CFC" && GlobalVariable.gUserCategory != "S") 
            {
                return;
            }
            //MessageBox.Show("The row index = " + dataIndexNo.ToString() + " and the row data in second column is: "
            //    + cellValue.ToString());
            int rowindex1 = dataGridView1.CurrentRow.Index;
            if (dataGridView1.Rows[rowindex1].Cells[12].Value != null) { kotstatus = dataGridView1.Rows[rowindex1].Cells[12].Value.ToString(); }
            else { kotstatus = "N"; }

            if (kotstatus == "Y") 
            {
                MessageBox.Show(cellValue.ToString() + " Already Cancelled");
                return;
            }

            int Rowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + KOrderNo + "' And Finyear = '" + FinYear1 + "'"));
            if (Rowcnt > 0)
            {
                int TotRowcnt = Convert.ToInt16(GCon.getValue("Select count(*) from kot_det where kotdetails = '" + KOrderNo + "' And isnull(Kotstatus,'') <> 'Y' And Finyear = '" + FinYear1 + "' And Isnull(Delflag,'') <> 'Y' "));
                if (TotRowcnt == 1) 
                {
                    MessageBox.Show(" Single Item Cannot be Cancel, U have to Cancel whole Order");
                    return;
                }
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("R U Sure to Cancel " + cellValue.ToString(), GlobalVariable.gCompanyName, buttons);
                if (result == DialogResult.Yes)
                {
                    //this.Close();
                    int rowindex = dataGridView1.CurrentRow.Index;
                    ItemCancel IC = new ItemCancel(this);
                    IC.Rowno = rowindex;
                    IC.ItemDesc = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                    IC.Itemcode = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                    IC.Kotno = KOrderNo;
                    IC.Slno = Convert.ToInt16(dataGridView1.Rows[rowindex].Cells[9].Value);
                    IC.ShowDialog();
                }
                else
                {
                    // Do something  
                }
            }
        }

        public void RefreshGrid(string Kotno)
        {
            DataTable KotData = new DataTable();
            sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,SLNO,MODIFIER,AUTOID,isnull(OrderNo,0) as OrderNo,Isnull(KotStatus,'N') as KotStatus,Isnull(PROMOTIONALST,'') AS PROMOTIONALST,Isnull(FinYear,'') as FinYear,Isnull(BusinessSource,'') as BusinessSource,Isnull(HAPPYSTATUS,'N') as HAPPYSTATUS,Isnull(ServiceOrder,1) as ServiceOrder,Isnull(ModifierCharges,0) as ModifierCharges from KOT_det where KOTDETAILS = '" + Kotno + "' And Finyear = '" + FinYear1 + "' ORDER BY SLNO";
            KotData = GCon.getDataSet(sql);
            if (KotData.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < KotData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = KotData.Rows[i].ItemArray[2];
                    dataGridView1.Rows[i].Cells[1].Value = KotData.Rows[i].ItemArray[3];
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[8]);
                    dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[9]);
                    dataGridView1.Rows[i].Cells[5].Value = KotData.Rows[i].ItemArray[5];
                    dataGridView1.Rows[i].Cells[6].Value = KotData.Rows[i].ItemArray[6];
                    dataGridView1.Rows[i].Cells[7].Value = KotData.Rows[i].ItemArray[11];
                    dataGridView1.Rows[i].Cells[8].Value = KotData.Rows[i].ItemArray[4];
                    dataGridView1.Rows[i].Cells[9].Value = KotData.Rows[i].ItemArray[10];
                    dataGridView1.Rows[i].Cells[10].Value = KotData.Rows[i].ItemArray[12];
                    dataGridView1.Rows[i].Cells[11].Value = KotData.Rows[i].ItemArray[13];
                    dataGridView1.Rows[i].Cells[12].Value = KotData.Rows[i].ItemArray[14];
                    if (KotData.Rows[i].ItemArray[14].ToString() == "Y") 
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    dataGridView1.Rows[i].Cells[13].Value = KotData.Rows[i].ItemArray[15];
                    dataGridView1.Rows[i].Cells[14].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
                    dataGridView1.Rows[i].Cells[15].Value = KotData.Rows[i].ItemArray[18];
                    dataGridView1.Rows[i].Cells[16].Value = KotData.Rows[i].ItemArray[19];
                    dataGridView1.Rows[i].Cells[16].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[17].Value = KotData.Rows[i].ItemArray[20];
                }
                Calculate();
            }
        }

        private void dataGridView1_CellEnter(object sender,DataGridViewCellEventArgs e) 
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string val = "";
            if (String.IsNullOrEmpty(dataGridView1.Rows[rowindex].Cells[7].Value as String))
            { val = ""; }
            else { val = dataGridView1.Rows[rowindex].Cells[7].Value.ToString(); }
            Lbl_Modifier.Text = val;
        }

        private void Cmb_Steward_SelectedindexChaged(object sender, EventArgs e)
        {
            string id, value;
            DataRowView drv = (DataRowView)Cmb_Steward.SelectedItem;
            id = drv["ServerCode"].ToString();
            value = drv["ServerName"].ToString();
            //MessageBox.Show("you selected " + id + " " + value);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    // here we do not come although the contextmenustrip shows up under the mouse pointer
            //    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    this.contextMenuStrip1.Show(this.dataGridView1, new Point(e.RowIndex, e.ColumnIndex));
            //}
        }

        private void Chk_NCApply_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_NCApply.Checked == true)
            {
                Cmb_NCCategory.Enabled = true;
            }
            else 
            {
                Cmb_NCCategory.Enabled = false;
            }
            Cmb_NCCategory_SelectedIndexChanged(sender, e);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Calculate();
            ////if (e.ColumnIndex == 16) // 1 should be your column index
            ////{
            ////    int i;

            ////    if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
            ////    {
            ////        e.Cancel = true;
            ////        MessageBox.Show("please enter numeric");
            ////        //int rowindex1 = dataGridView1.CurrentRow.Index;
            ////        //if (dataGridView1.Rows[rowindex1].Cells[1].Value != null) { }
            ////        //else 
            ////        //{
            ////        //    if (dataGridView1.Rows[rowindex1].Cells[16].Value != null) 
            ////        //    {
            ////        //        dataGridView1.Rows.RemoveAt(rowindex1); 
            ////        //    }
            ////        //}
            ////    }
            ////    else
            ////    {
                   
            ////    }
            ////}
        }
        
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 16 || dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
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

        private void Txt_Item_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Txt_Item_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsControl(e.KeyChar))
            //{
            //    dtView.RowFilter = dtView.Table.Columns[1].ColumnName + " Like '%" + e.KeyChar + "%'";
            //    foreach (DataRowView dtViewRow in dtView)
            //        searchResults.Add(dtViewRow[0].ToString());

            //    Txt_Item.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //    Txt_Item.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    Txt_Item.AutoCompleteCustomSource = searchResults;
            //}
        }

        private void Cmd_OptionalPrint_Click(object sender, EventArgs e)
        {
            EntryPrintDialog EPD = new EntryPrintDialog(this);
            EPD.KotOrderNo = KOrderNo;
            EPD.ShowDialog();
        }

        private void Cmd_SubGroup_Click(object sender, EventArgs e)
        {
            FillSubGroup();
        }

        private void Cmb_NCCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtNC = new DataTable();
            string PosItemCode = "", PosItemUom = "", VarPosCode = "";
            double Rate;
            if (UpdFlag == false && Chk_NCApply.Checked == true) 
            {
                string TypeRate = GCon.getValue(" select Isnull(TypeForRate,'') as TypeForRate from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "' ").ToString();
                Double PSPercent = Convert.ToDouble(GCon.getValue(" select Isnull(PSPercent,0) as PSPercent from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "'"));
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    PosItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemUom = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    VarPosCode = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                    sql = sql + " AND I.ITEMCODE = '" + PosItemCode + "' AND R.rposcode = '" + VarPosCode + "' AND R.UOM = '" + PosItemUom + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    dtNC = GCon.getDataSet(sql);
                    if (dtNC.Rows.Count > 0) 
                    {
                        DataRow dr = dtNC.Rows[0];
                        if (TypeRate == "P") { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        else if (TypeRate == "S") { Rate = Convert.ToDouble(dr["ItemRate"]); }
                        else if (TypeRate == "PS") { Rate = (Convert.ToDouble(dr["ItemRate"]) * PSPercent) / 100; }
                        else { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        dataGridView1.Rows[i].Cells[3].Value = Rate;
                    }
                }
            }
            else if (UpdFlag == true && Chk_NCApply.Checked == true && GlobalVariable.gCompName == "SKYYE") 
            {
                string TypeRate = GCon.getValue(" select Isnull(TypeForRate,'') as TypeForRate from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "' ").ToString();
                Double PSPercent = Convert.ToDouble(GCon.getValue(" select Isnull(PSPercent,0) as PSPercent from Tbl_NCCategoryMaster Where Isnull(void,'') <> 'Y' And NCCategory = '" + Cmb_NCCategory.Text + "'"));
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    PosItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemUom = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    VarPosCode = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                    sql = sql + " AND I.ITEMCODE = '" + PosItemCode + "' AND R.rposcode = '" + VarPosCode + "' AND R.UOM = '" + PosItemUom + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    dtNC = GCon.getDataSet(sql);
                    if (dtNC.Rows.Count > 0)
                    {
                        DataRow dr = dtNC.Rows[0];
                        if (TypeRate == "P") { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        else if (TypeRate == "S") { Rate = Convert.ToDouble(dr["ItemRate"]); }
                        else if (TypeRate == "PS") { Rate = (Convert.ToDouble(dr["ItemRate"]) * PSPercent) / 100; }
                        else { Rate = Convert.ToDouble(dr["PurcahseRate"]); }
                        dataGridView1.Rows[i].Cells[3].Value = Rate;
                    }
                }
            }
            else if (UpdFlag == true && Chk_NCApply.Checked == false && GlobalVariable.gCompName == "SKYYE") 
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    PosItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemUom = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    VarPosCode = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                    sql = sql + " AND I.ITEMCODE = '" + PosItemCode + "' AND R.rposcode = '" + VarPosCode + "' AND R.UOM = '" + PosItemUom + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    dtNC = GCon.getDataSet(sql);
                    if (dtNC.Rows.Count > 0)
                    {
                        DataRow dr = dtNC.Rows[0];
                        Rate = Convert.ToDouble(dr["ItemRate"]);
                        dataGridView1.Rows[i].Cells[3].Value = Rate;
                    }
                }
            }
            else if (UpdFlag == false && Chk_NCApply.Checked == false)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    PosItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    PosItemUom = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    VarPosCode = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    sql = "SELECT I.ItemCode,I.ItemDesc,R.ItemRate,R.rposcode,UOM,Isnull(R.PurcahseRate,0) as PurcahseRate FROM ITEMMASTER I,RATEMASTER R WHERE I.ITEMCODE = R.ITEMCODE ";
                    sql = sql + " AND I.ITEMCODE = '" + PosItemCode + "' AND R.rposcode = '" + VarPosCode + "' AND R.UOM = '" + PosItemUom + "' AND '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' BETWEEN r.StartingDate And isnull(r.Endingdate,getdate()) ";
                    dtNC = GCon.getDataSet(sql);
                    if (dtNC.Rows.Count > 0)
                    {
                        DataRow dr = dtNC.Rows[0];
                        Rate = Convert.ToDouble(dr["ItemRate"]);
                        dataGridView1.Rows[i].Cells[3].Value = Rate;
                    }
                }
            }
            Calculate();
        }

        private void Cmd_AddPax_Click(object sender, EventArgs e)
        {
            if (UpdFlag == true) 
            {
                AddPaxForm APF = new AddPaxForm(this);
                APF.KotOrder = KOrderNo;
                APF.ShowDialog();
                Pax = Convert.ToInt16(GCon.getValue("Select Top 1 Isnull(Covers,0) as Covers from KOT_HDR Where Kotdetails = '" + KOrderNo + "' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            }
        }

        private void Cmd_TransferItem_Click(object sender, EventArgs e)
        {
            if (UpdFlag == true) 
            {
                DataTable CheckPrintFlag = new DataTable();
                sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo = '" + KOrderNo + "' And FinYear = '" + FinYear1 + "' ";
                CheckPrintFlag = GCon.getDataSet(sql);
                if (CheckPrintFlag.Rows.Count > 0)
                {
                    MessageBox.Show("Check Print Done, You can't Modify");
                    return;
                }
                TransferItem TFI = new TransferItem(this);
                TFI.KotOrderNo = KOrderNo;
                TFI.LocationCode = Loccode;
                TFI.ShowDialog();
            }
        }


        private void GetRights() 
        {
            DataTable Rights = new DataTable();
            Cmd_Save.Enabled = false;
            sql = "select Isnull(AddM,'N') as AddM,Isnull(EditM,'N') as EditM,Isnull(DelM,'N') as DelM from Tbl_TransactionFormUserTag Where FormName = 'KOT ENTRY FORM' And UserName = '" + GlobalVariable.gUserName + "' ";
            Rights = GCon.getDataSet(sql);
            if (Rights.Rows.Count > 0)
            {
                if (UpdFlag == false)
                {
                    if (Rights.Rows[0].ItemArray[0].ToString() == "Y")
                    { Cmd_Save.Enabled = true; }
                }
                else 
                {
                    if (Rights.Rows[0].ItemArray[1].ToString() == "Y")
                    { Cmd_Save.Enabled = true; }
                }
            }
            else 
            {
                Cmd_Save.Enabled = false;
            }
        }

        private void Chk_SearchByCode_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_SearchByCode.Checked == true)
            {
                AutoCompleteItem();
            }
            else 
            {
                AutoComplete();
            }
        }

        private void listBox_Items_SelectedIndexChanged(object sender, EventArgs e)
        {
            text_auto_nw.Text = listBox_Items.Items[listBox_Items.SelectedIndex].ToString();
            Txt_Item.Text = text_auto_nw.Text;
            Txt_Item_KeyDown(Txt_Item, new KeyEventArgs(Keys.Enter));
            hideSearchList();
        }

        private void text_auto_nw_Leave(object sender, EventArgs e)
        {
            //hideSearchList();
        }

        private void Cmd_D1_DBilling_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Direct-Billing";
            SelfClick = true;
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = "V1";
                Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    Pax = 1;
                }
                EntryForm_Load(sender, e);
            }
        }

        private void Cmd_D2_DBilling_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Direct-Billing";
            SelfClick = true;
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = "V2";
                Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V2' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V2' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    Pax = 1;
                }
                EntryForm_Load(sender, e);
            }
        }

        private void Cmd_D3_DBilling_Click(object sender, EventArgs e)
        {
            GlobalVariable.ServiceType = "Direct-Billing";
            SelfClick = true;
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            sql = "select LocCode,LocName from ServiceLocation_Hdr Where Isnull(ServiceFlag,'') = 'F' And Isnull(KotPrefix,'') <> '' And Isnull(BillPrefix,'') <> '' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                DataTable ChkChair = new DataTable();
                int ChNo = 1;
                GlobalVariable.SLocation = dt.Rows[0].ItemArray[1].ToString();
                GlobalVariable.TableNo = "V3";
                Loccode = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                sql = "SELECT isnull(ChairSeqNo,1) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V3' AND BILLSTATUS = 'PO' and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
                ChkChair = GCon.getDataSet(sql);
                if (ChkChair.Rows.Count > 0)
                {
                    ChNo = Convert.ToInt16(ChkChair.Rows[0].ItemArray[0]);
                }
                else { ChNo = 1; }
                int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Convert.ToInt32(dt.Rows[0].ItemArray[0]) + " AND TableNo = 'V3' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = " + ChNo + " and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
                if (RowCnt > 0)
                {
                    UpdFlag = true;
                    GlobalVariable.ChairNo = ChNo;
                }
                else
                {
                    UpdFlag = false;
                    GlobalVariable.ChairNo = ChNo;
                    Pax = 1;
                }
                EntryForm_Load(sender, e);
            }
        }


        public void GetTableColor() 
        {
            int RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (RowCnt > 0)
            {
                Cmd_D1_DBilling.BackColor = Color.Red;
            }
            else 
            {
                Cmd_D1_DBilling.BackColor = Color.Green;
            }
            RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V2' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (RowCnt > 0)
            {
                Cmd_D2_DBilling.BackColor = Color.Red;
            }
            else
            {
                Cmd_D2_DBilling.BackColor = Color.Green;
            }
            RowCnt = Convert.ToInt16(GCon.getValue("SELECT Count(*) FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V3' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "' "));
            if (RowCnt > 0)
            {
                Cmd_D3_DBilling.BackColor = Color.Red;
            }
            else
            {
                Cmd_D3_DBilling.BackColor = Color.Green;
            }
            DataTable Checkprint = new DataTable();
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo IN (SELECT Kotdetails FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V1' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "') AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            Checkprint = GCon.getDataSet(sql);
            if (Checkprint.Rows.Count > 0) 
            {
                Cmd_D1_DBilling.BackColor = Color.Yellow;
            }
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo IN (SELECT Kotdetails FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V2' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "') AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            Checkprint = GCon.getDataSet(sql);
            if (Checkprint.Rows.Count > 0)
            {
                Cmd_D2_DBilling.BackColor = Color.Yellow;
            }
            sql = "SELECT * FROM Tbl_CheckPrint WHERE KotNo IN (SELECT Kotdetails FROM KOT_HDR WHERE LocCode = " + Loccode + " AND TableNo = 'V3' AND BILLSTATUS = 'PO' And Isnull(ChairSeqNo,0) = 1 and KOTDATE = '" + GlobalVariable.ServerDate.ToString("dd-MMM-yyyy") + "' AND ISNULL(DELFLAG,'') <> 'Y' AND ISNULL(FinYear,'') = '" + FinYear1 + "') AND ISNULL(FinYear,'') = '" + FinYear1 + "' ";
            Checkprint = GCon.getDataSet(sql);
            if (Checkprint.Rows.Count > 0)
            {
                Cmd_D3_DBilling.BackColor = Color.Yellow;
            }
        }
        

    }
}
