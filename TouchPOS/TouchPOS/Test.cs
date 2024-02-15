using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    public partial class Test : Form
    {
        string sql = "";
        GlobalClass GCon = new GlobalClass();
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Utility.relocate(this, 1366, 768);
            Utility.repositionForm(this, screenWidth, screenHeight);

            ////dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            ////this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////this.dataGridView1.Columns[0].Width = 50;
            ////this.dataGridView1.Columns[1].Width = 200;
            ////this.dataGridView1.Columns[2].Width = 50;
            ////dataGridView1.ColumnHeadersVisible = false;
            ////dataGridView1.RowHeadersVisible = false;
            ////DataTable KotData = new DataTable();
            ////sql = "Select KOTNO,KOTDETAILS,ITEMCODE,ITEMDESC,ITEMTYPE,POSCODE,UOM,QTY,RATE,AMOUNT,SLNO,MODIFIER,AUTOID from KOT_det where KOTDETAILS = '1' ";
            ////KotData = GCon.getDataSet(sql);
            ////if (KotData.Rows.Count > 0) 
            ////{
            ////    for (int i = 0; i < KotData.Rows.Count; i++)
            ////    {
            ////        dataGridView1.Rows.Add();
            ////        dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt16(KotData.Rows[i].ItemArray[7]);
            ////        dataGridView1.Rows[i].Cells[1].Value = KotData.Rows[i].ItemArray[3];
            ////        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(KotData.Rows[i].ItemArray[9]);
            ////    }
            ////}

            //var doc = new PrintDocument();
            //doc.PrintPage += new PrintPageEventHandler(ProvideContent);
            //doc.Print();

            //ShowOrder cf = new ShowOrder();
            //cf.MdiParent = this;
            //cf.Width = 242;
            //cf.Height = 290;
            //cf.StartPosition = FormStartPosition.Manual;

            ////cf.AutoSize = true;
            ////cf.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ////cf.ClientSize = new System.Drawing.Size(panel3.Width, panel3.Height);
            ////cf.Location = new Point(((this.ClientSize.Width - cf.Width) + panel3.Width) / 2, (this.ClientSize.Height - cf.Height) / 2);
            ////cf.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ////cf.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //panel3.Controls.Add(cf);
            ////cf.Dock = DockStyle.Fill;
            ////cf.BringToFront();
            //cf.Show();


            //panel3.AutoScroll = true;
            //ShowOrder ucA = new ShowOrder(); //ucAdmin is a user control u had created.
            //ucA.Width = 242;
            //ucA.Height = 290;
            //ucA.TopLevel = false;
            //ucA.Visible = true;
            //ucA.Dock = DockStyle.Fill;

            //this.panel3.Controls.Clear(); 
            //this.panel3.Controls.Add(ucA);

            //panel3.Controls.Clear();//contentpnl is the panelname
            //WinForm purchasebk = new WinForm();//purchasebook is a formname
           

            //purchasebk.TopLevel = false;
            //purchasebk.AutoScroll = true;
          
            ////Resize_Form(purchasebk, panel3.Height, panel3.Width, purchasebk.Height, purchasebk.Width);
            //panel3.Controls.Add(purchasebk);
           
            //Utility.relocate(purchasebk, purchasebk.Width * 6, 768);
            //Utility.repositionForm(purchasebk, screenWidth, screenHeight);

            //purchasebk.Dock = DockStyle.Fill;
            //purchasebk.Show();

            //WinForm myForm = new WinForm();
            ////myForm.TopLevel = false;
            ////myForm.AutoScroll = true;
            //myForm.Width = panel3.Width;
            //myForm.Height = panel3.Height;
            //panel3.AutoScroll = false;
            //this.panel3.Controls.Clear();
            //this.panel3.Controls.Add(myForm);
            //myForm.Dock = DockStyle.Fill;
            ////myForm.FormBorderStyle = FormBorderStyle.None;
            //myForm.Show();
            Winform wn = new Winform();
            //wn.Width = panel3.Width;
            //wn.Height = panel3.Height;
            //wn.Location = new Point(10, 10);
            wn.Dock = DockStyle.Fill;
            panel3.Controls.Add(wn);
           
          

            //panel3.Controls.Clear();
            //panel3.Controls.Add(new Winform());
           
        }

        public void ProvideContent(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);

            float fontHeight = font.GetHeight();

            int startX = 0;
            int startY = 0;
            int Offset = 20;

            e.PageSettings.PaperSize.Width = 50;

            graphics.DrawString("Welcome to MSST", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Ticket No:" + "4525554654545",
                        new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("Ticket Date :" + "21/12/215",
                        new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String underLine = "------------------------------------------";

            graphics.DrawString(underLine, new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Grosstotal = "Total Amount to Pay = " + "2566";

            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(Grosstotal, new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }


        public void setFrmPosition(Form frm, Form MDIFRM)
        {
            int frmHeight, frmWidth, frmPrevHeight, frmPrevWidth;

            //frmPrevHeight = frm.Height;
            //frmPrevWidth = frm.Width;
            //frm.StartPosition = FormStartPosition.Manual;
            //frm.MdiParent = MDIFRM;

            //frm.Top = panelHdr.Height + 1;
            //frm.Left = 0;
            //frmHeight = (this.Height - (panelHdr.Height + statusStrip.Height + 45));
            //frmWidth = (this.Width - (treeViewMenu.Width + 20));
            //frm.Height = frmHeight;
            //frm.Width = frmWidth;

            //resizefrm.Resize_Form(frm, frmHeight, frmWidth, frmPrevHeight, frmPrevWidth);
        }


        public void Resize_Form(Form frmname, int frmheight, int frmwidth, int orgHeight, int orgWidth)
        {
            int i_i = 0;
            int L = 0;
            int M = 0;
            int n = 0;
            int o = 0;
            int P = 0;
            int Q = 0;
            int R = 0;
            int S = 0;
            int cntrlOrgWidth, cntrlOrgHeight, SubCtrlOrgWidth, SubCtrlOrgHeight;

            frmname.Width = frmwidth;
            frmname.Height = frmheight;
            Form curForm = frmname;

            for (i_i = 0; i_i <= curForm.Controls.Count - 1; i_i++)
            {

                cntrlOrgWidth = curForm.Controls[i_i].Size.Width;
                cntrlOrgHeight = curForm.Controls[i_i].Size.Height;

                if (curForm.Controls[i_i] is Label)
                {
                    if (curForm.Controls[i_i].Location.X == 0)
                    {
                        L = 0;
                    }
                    else
                    {
                        L = Convert.ToInt32(curForm.Width / Math.Round((Convert.ToDouble(orgWidth) / Convert.ToDouble(curForm.Controls[i_i].Location.X)), 2));
                    }

                    if (curForm.Controls[i_i].Location.Y == 0)
                    {
                        L = 0;
                    }
                    else
                    {
                        M = Convert.ToInt32(curForm.Height / Math.Round((Convert.ToDouble(orgHeight) / Convert.ToDouble(curForm.Controls[i_i].Location.Y)), 2));
                    }
                    curForm.Controls[i_i].Left = L;
                    curForm.Controls[i_i].Top = M;

                    if (curForm.Controls[i_i].Size.Width == 0)
                    {
                        n = 0;
                    }
                    else
                    {
                        n = Convert.ToInt32(curForm.Width / Math.Round((Convert.ToDouble(orgWidth) / Convert.ToDouble(curForm.Controls[i_i].Size.Width)), 2));
                    }
                    if (curForm.Controls[i_i].Size.Height == 0)
                    {
                        o = 0;
                    }
                    else
                    {
                        o = Convert.ToInt32(curForm.Height / Math.Round((Convert.ToDouble(orgHeight) / Convert.ToDouble(curForm.Controls[i_i].Size.Height)), 2));
                    }
                    curForm.Controls[i_i].Width = n;
                    curForm.Controls[i_i].Height = o;
                }
                //|| curForm.Controls[i_i] is TabControl
                else if (curForm.Controls[i_i] is Panel || curForm.Controls[i_i] is GroupBox || curForm.Controls[i_i] is TabControl)
                {
                    if (curForm.Controls[i_i].Location.X == 0)
                    {
                        L = 0;
                    }
                    else
                    {
                        L = Convert.ToInt32(curForm.Width / Math.Round((Convert.ToDouble(orgWidth) / Convert.ToDouble(curForm.Controls[i_i].Location.X)), 2));
                    }

                    if (curForm.Controls[i_i].Location.Y == 0)
                    {
                        L = 0;
                    }
                    else
                    {
                        M = Convert.ToInt32(curForm.Height / Math.Round((Convert.ToDouble(orgHeight) / Convert.ToDouble(curForm.Controls[i_i].Location.Y)), 2));
                    }
                    curForm.Controls[i_i].Left = L;
                    curForm.Controls[i_i].Top = M;

                    if (curForm.Controls[i_i].Size.Width == 0)
                    {
                        n = 0;
                    }
                    else
                    {
                        n = Convert.ToInt32(curForm.Width / Math.Round((Convert.ToDouble(orgWidth) / Convert.ToDouble(curForm.Controls[i_i].Size.Width)), 2));
                    }

                    if (curForm.Controls[i_i].Size.Height == 0)
                    {
                        o = 0;
                    }
                    else
                    {
                        o = Convert.ToInt32(curForm.Height / Math.Round((Convert.ToDouble(orgHeight) / Convert.ToDouble(curForm.Controls[i_i].Size.Height)), 2));
                    }
                    curForm.Controls[i_i].Width = n;
                    curForm.Controls[i_i].Height = o;


                    foreach (Control cControl in curForm.Controls[i_i].Controls)
                    {
                        //--------******************----------
                        SubCtrlOrgWidth = cControl.Width;
                        SubCtrlOrgHeight = cControl.Height;

                        if (cControl is Panel || cControl is GroupBox || cControl is TabControl)
                        {

                            if (cControl.Size.Width == 0)
                            {
                                P = 0;
                            }
                            else
                            {
                                P = Convert.ToInt32(curForm.Controls[i_i].Size.Width / Math.Round((Convert.ToDouble(cntrlOrgWidth) / Convert.ToDouble(cControl.Size.Width)), 2));
                            }

                            if (cControl.Size.Height == 0)
                            {
                                Q = 0;
                            }
                            else
                            {
                                Q = Convert.ToInt32(curForm.Controls[i_i].Size.Height / Math.Round((Convert.ToDouble(cntrlOrgHeight) / Convert.ToDouble(cControl.Size.Height)), 2));
                            }
                            cControl.Width = P;
                            cControl.Height = Q;

                            if (cControl.Location.X == 0)
                            {
                                R = 0;
                            }
                            else
                            {
                                R = Convert.ToInt32(curForm.Controls[i_i].Size.Width / Math.Round((Convert.ToDouble(cntrlOrgWidth) / Convert.ToDouble(cControl.Location.X)), 2));
                            }
                            if (cControl.Location.Y == 0)
                            {
                                S = 0;
                            }
                            else
                            {
                                S = Convert.ToInt32(curForm.Controls[i_i].Size.Height / Math.Round((Convert.ToDouble(cntrlOrgHeight) / Convert.ToDouble(cControl.Location.Y)), 2));
                            }
                            cControl.Left = R;
                            cControl.Top = S;


                            foreach (Control subcontrol in cControl.Controls)
                            {
                                if (subcontrol.Size.Width == 0)
                                {
                                    P = 0;
                                }
                                else
                                {
                                    P = Convert.ToInt32(cControl.Width / Math.Round((Convert.ToDouble(SubCtrlOrgWidth) / Convert.ToDouble(subcontrol.Size.Width)), 2));
                                }

                                if (subcontrol.Size.Height == 0)
                                {
                                    Q = 0;
                                }
                                else
                                {
                                    Q = Convert.ToInt32(cControl.Height / Math.Round((Convert.ToDouble(SubCtrlOrgHeight) / Convert.ToDouble(subcontrol.Size.Height)), 2));
                                }
                                subcontrol.Width = P;
                                subcontrol.Height = Q;

                                if (subcontrol.Location.X == 0)
                                {
                                    R = 0;
                                }
                                else
                                {
                                    R = Convert.ToInt32(cControl.Width / Math.Round((Convert.ToDouble(SubCtrlOrgWidth) / Convert.ToDouble(subcontrol.Location.X)), 2));
                                }
                                if (cControl.Location.Y == 0)
                                {
                                    S = 0;
                                }
                                else
                                {
                                    S = Convert.ToInt32(cControl.Height / Math.Round((Convert.ToDouble(SubCtrlOrgHeight) / Convert.ToDouble(subcontrol.Location.Y)), 2));
                                }
                                subcontrol.Left = R;
                                subcontrol.Top = S;
                            }
                            //--------*****************------------
                        }
                        else
                        {
                            if (cControl.Size.Width == 0)
                            {
                                P = 0;
                            }
                            else
                            {
                                P = Convert.ToInt32(curForm.Controls[i_i].Size.Width / Math.Round((Convert.ToDouble(cntrlOrgWidth) / Convert.ToDouble(cControl.Size.Width)), 2));
                            }

                            if (cControl.Size.Height == 0)
                            {
                                Q = 0;
                            }
                            else
                            {
                                Q = Convert.ToInt32(curForm.Controls[i_i].Size.Height / Math.Round((Convert.ToDouble(cntrlOrgHeight) / Convert.ToDouble(cControl.Size.Height)), 2));
                            }
                            cControl.Width = P;
                            cControl.Height = Q;

                            if (cControl.Location.X == 0)
                            {
                                R = 0;
                            }
                            else
                            {
                                R = Convert.ToInt32(curForm.Controls[i_i].Size.Width / Math.Round((Convert.ToDouble(cntrlOrgWidth) / Convert.ToDouble(cControl.Location.X)), 2));
                            }
                            if (cControl.Location.Y == 0)
                            {
                                S = 0;
                            }
                            else
                            {
                                S = Convert.ToInt32(curForm.Controls[i_i].Size.Height / Math.Round((Convert.ToDouble(cntrlOrgHeight) / Convert.ToDouble(cControl.Location.Y)), 2));
                            }
                            cControl.Left = R;
                            cControl.Top = S;

                        }
                    }

                }
            }
        }




    }
}
