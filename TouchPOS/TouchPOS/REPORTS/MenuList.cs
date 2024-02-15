using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.REPORTS
{
    public partial class MenuList : Form
    {
        GlobalClass GCon = new GlobalClass();

        public MenuList()
        {
            InitializeComponent();
        }

        private void MenuList_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            FillPosLocations();
            FillGroup();
            FillSubGroup();
            Fillcategory();
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        public void FillPosLocations()
        {
            String sqlstring;
            chklist_POSlocation.Items.Clear();
            int i;
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC FROM posmaster ";
            GCon.getDataSet1(sqlstring, "posmaster");
            if (GlobalVariable.gdataset.Tables["posmaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["posmaster"].Rows.Count; i++)
                {
                    chklist_POSlocation.Items.Add(GlobalVariable.gdataset.Tables["posmaster"].Rows[i].Field<String>("POSDESC").Trim());
                }
            }
        }

        public void FillGroup()
        {
            String sqlstring;
            LstGroup.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT Groupcode,Groupdesc FROM GroupMaster ";
            GCon.getDataSet1(sqlstring, "GroupMaster");
            if (GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["GroupMaster"].Rows.Count; i++)
                {
                    LstGroup.Items.Add(GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupcode").Trim() + "-" + GlobalVariable.gdataset.Tables["GroupMaster"].Rows[i].Field<String>("Groupdesc").Trim());
                }
            }
        }

        public void FillSubGroup()
        {
            String sqlstring;
            lstsubgroup.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT subGROUPDESC  FROM SUBGROUPMASTER ";
            GCon.getDataSet1(sqlstring, "SUBGROUPMASTER");
            if (GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows.Count > 0)
            {
                for (i = 0; i < GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows.Count; i++)
                {
                    lstsubgroup.Items.Add(GlobalVariable.gdataset.Tables["SUBGROUPMASTER"].Rows[i].Field<String>("subGROUPDESC").Trim());
                }
            }
        }
        
        public void Fillcategory()
        {
            String sqlstring;
            lstcategory.Items.Clear();
            int i;
            sqlstring = "SELECT DISTINCT CATEGORY FROM ITEMMaster ";
            GCon.getDataSet1(sqlstring, "ITEMMaster");
            if (GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count > 0)
                for (i = 0; i < GlobalVariable.gdataset.Tables["ITEMMaster"].Rows.Count; i++)
                {
                    {
                        lstcategory.Items.Add(GlobalVariable.gdataset.Tables["ITEMMaster"].Rows[i].Field<String>("CATEGORY").Trim());
                    }
                }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Chk_POSlocation.Checked = false;
            Chk_SelectAllGroup.Checked = false;
            Chk_SelectAllsubgroup.Checked = false;
            Chk_SelectAllCategory.Checked = false;
            FillPosLocations();
            FillGroup();
            FillSubGroup();
            Fillcategory();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            String[] servercode;
            int i;
            String sqlstring, ssql1, ssql3;
            String[] POSdesc, MemberCode, GRPCODE, TYPE;
            string HNAME, POSNAME, Catname;
            Report rv = new Report();
            TextObject txtobj1, TXTOBJ10;

            CRYSTAL.Crpt_MenuList_Lat ML = new CRYSTAL.Crpt_MenuList_Lat();
            POSNAME = "";

            sqlstring = " SELECT ITEMCODE,ITEMDESC,GROUPCODEDEC,GROUPCODE,SUBGROUPDESC,CATEGORY,UOM,ITEMRATE,POSDESC,SUM(TAXAMT) AS TAXAMT  FROM [View_MenuList] Where ";
                      
            if (chklist_POSlocation.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " POSDESC IN (";
                for (i = 0; i <= chklist_POSlocation.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + chklist_POSlocation.CheckedItems[i] + "', ";
                    POSNAME = POSNAME + chklist_POSlocation.CheckedItems[i] + ", ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                POSNAME = POSNAME.Remove(POSNAME.Length - 2);
            }
            else
            {
                MessageBox.Show("Select the POS LOCATIONS(s)", GlobalVariable.gCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            if (LstGroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and GROUPCODE in (";

                for (i = 0; i <= LstGroup.CheckedItems.Count - 1; i++)
                {
                    TYPE = LstGroup.CheckedItems[i].ToString().Split('-');
                    sqlstring = sqlstring + " '" + TYPE[0] + "', ";

                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";

            }

            if (lstsubgroup.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and subGROUPDESC in (";
                HNAME = "(";
                for (i = 0; i <= lstsubgroup.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstsubgroup.CheckedItems[i] + "', ";
                    HNAME = HNAME + " '" + lstsubgroup.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                sqlstring = sqlstring + ")";
                HNAME = HNAME.Remove(HNAME.Length - 2);
                HNAME = HNAME + ")";
            }

            if (lstcategory.CheckedItems.Count != 0)
            {
                sqlstring = sqlstring + " and Category in (";
                Catname = "";
                for (i = 0; i <= lstcategory.CheckedItems.Count - 1; i++)
                {
                    sqlstring = sqlstring + " '" + lstcategory.CheckedItems[i] + "', ";
                    Catname = Catname + " '" + lstcategory.CheckedItems[i] + "', ";
                }
                sqlstring = sqlstring.Remove(sqlstring.Length - 2);
                Catname = Catname.Remove(Catname.Length - 2);
                Catname = Catname + "";
                sqlstring = sqlstring + ")";

            }

            sqlstring = sqlstring + " GROUP BY ITEMCODE,ITEMDESC,GROUPCODE,GROUPCODEDEC,SUBGROUPDESC,CATEGORY,UOM,ITEMRATE,POSDESC ORDER BY POSDESC,GROUPCODEDEC,SUBGROUPDESC ";
            GCon.getDataSet1(sqlstring, "View_MenuList");
            if (GlobalVariable.gdataset.Tables["View_MenuList"].Rows.Count > 0)
            {
                rv.GetDetails(sqlstring, "View_MenuList", ML);
                ML.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = ML;
                rv.crystalReportViewer1.Zoom(100);
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)ML.ReportDefinition.ReportObjects["Text11"];
                TXTOBJ1.Text = "MENU LIST";
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
                TXTOBJ2 = (TextObject)ML.ReportDefinition.ReportObjects["Text12"];
                TXTOBJ2.Text = GlobalVariable.gUserName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)ML.ReportDefinition.ReportObjects["Text10"];
                TXTOBJ3.Text = GlobalVariable.gCompanyName;
                rv.Show();
            }
            else
            {
                MessageBox.Show("No Records To Display..");
            }

        }

        private void Chk_POSlocation_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_POSlocation.Checked == true)
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < chklist_POSlocation.Items.Count; i++)
                {
                    chklist_POSlocation.SetItemChecked(i, false);
                }
            }
        }

        private void Chk_SelectAllGroup_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_SelectAllGroup.Checked == true)
            {
                for (i = 0; i < LstGroup.Items.Count; i++)
                {
                    LstGroup.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < LstGroup.Items.Count; i++)
                {
                    LstGroup.SetItemChecked(i, false);
                }
            }
        }

        private void Chk_SelectAllsubgroup_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_SelectAllsubgroup.Checked == true)
            {
                for (i = 0; i < lstsubgroup.Items.Count; i++)
                {
                    lstsubgroup.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < lstsubgroup.Items.Count; i++)
                {
                    lstsubgroup.SetItemChecked(i, false);
                }
            }
        }

        private void Chk_SelectAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (Chk_SelectAllCategory.Checked == true)
            {
                for (i = 0; i < lstcategory.Items.Count; i++)
                {
                    lstcategory.SetItemChecked(i, true);
                }
            }
            else
            {
                for (i = 0; i < lstcategory.Items.Count; i++)
                {
                    lstcategory.SetItemChecked(i, false);
                }
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
