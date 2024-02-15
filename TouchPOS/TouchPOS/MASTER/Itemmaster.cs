using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS.MASTER
{
    public partial class Itemmaster : Form
    {
        GlobalClass GCon = new GlobalClass();

        public readonly MastersForm _form1;
        public Itemmaster(MastersForm form1)
        {
            _form1 = form1;
            InitializeComponent();
        }

        public Itemmaster()
        {
            // TODO: Complete member initialization
        }



        string sql = "";
        string pos1;
        string charge;
        string acc;
        string sqlstring = "";
        string itemcode;
        string vseqno, gvseqno;
        string ssql = "";
        string subgroupcode, groupcode, kitchencode, taxdesc, vstring, VarPOSCODE;
        public string pvseqno, INVSEQNO;
        private void Itemmaster_Load(object sender, EventArgs e)
        {
            BlackGroupBox();

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.CenterToScreen();


            ////Utility.relocate(this, 1368, 768);
            //Utility.fitFormToScreen(this, screenHeight, screenWidth);

            fillpos();
            fillgridtax();
            fillstorecode();



            fillkitchen();
            fillsubgroup();
            fillgroup();
            fillUom();
            filltax();
            fillinvuom();
            fillinvitem();

            cmb_invuom.Visible = false;
            cmb_invitem.Visible = false;
            label20.Visible = false;
            label21.Visible = false;

            showHideColumns(false);

            txt_mrprate.Text = "1.00";
            txt_salesrate.Text = "1.00";
            txt_purchaserate.Text = "1.00";

            txt_itemcode.ReadOnly = true;

            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;

            cmb_category.Enabled = false;
            cmb_category.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_group.Enabled = false;
            cmb_group.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Dtp_EffectiveDate.Value = DateTime.Now;
            this.Dtp_EffectiveDate.MinDate = DateTime.Now.Date;
            //this.Dtp_EffectiveDate.Enabled = false;

            groupBox7.Visible = false;
            cmb_group.SelectedIndex = -1;
            Cmb_subgroup.SelectedIndex = -1;
            cmb_uom.SelectedIndex = -1;
            cmb_kitchen.SelectedIndex = -1;
            cmb_taxtype.SelectedIndex = -1;
            cmb_invitem.SelectedIndex = -1;
            cmb_invuom.SelectedIndex = -1;

            Cmb_MFierType.SelectedIndex = 0;

            this.dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Maxid();
            fillCategory();
            cmb_category.SelectedIndex = -1;
            chk_gg.Checked = true;
            fillGRID();
        }

        public void BlackGroupBox()
        {
            myGroupBox myGroupBox = new myGroupBox();
            myGroupBox.Text = "";
            myGroupBox.BorderColor = Color.Black;
            myGroupBox.Size = groupBox3.Size;
            groupBox3.Controls.Add(myGroupBox);

            myGroupBox myGroupBox1 = new myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }



        public void fillGRID()
        {

            DataTable PosCate = new DataTable();
            sql = " select  distinct accode,acdesc  from accountsglaccountmaster where ISNULL(freezeflag,'')<>'y' ";
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();

                this.dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView3.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];


                }
            }
        }

        public void FillItemTypeDetails_Det()
        {

            int i;
            ChkList_ItemTypeDet.Items.Clear();
            DataTable dta = new DataTable();
            dta = new DataTable();

            sqlstring = " SELECT ISNULL(TAXCODE,'') AS TAXCODE,ISNULL(TAXPERCENTAGE,0) AS TAXPERC,ISNULL(TAXON,'') AS TAXON FROM ItemTypeMaster ";
            sqlstring = sqlstring + " WHERE ITEMTYPECODE IN ( SELECT TAXTYPECODE FROM CHARGEMASTER WHERE CHARGEdesc = '" + cmb_taxtype.Text + "' ) ";
            sqlstring = sqlstring + " ORDER BY TAXON ";

            dta = GCon.getDataSet(sqlstring);
            if (dta.Rows.Count > 0)
            {
                for (i = 0; i <= dta.Rows.Count - 1; i++)
                {
                    ChkList_ItemTypeDet.Items.Add(dta.Rows[i].ItemArray[0].ToString() + "->" + dta.Rows[i].ItemArray[1].ToString() + "->" + dta.Rows[i].ItemArray[2].ToString());
                    ChkList_ItemTypeDet.SetItemChecked(i, true);

                }
            }

        }

        public void Maxid()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sql = "select Isnull(max(Convert(int, itemcode)),0)+1 as itemcode from Itemmaster where isnumeric(itemcode) = 1";

            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                txt_itemcode.Text = dt.Rows[0].ItemArray[0].ToString();
            }
        }





        private void Chk_pos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void fillpos()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            sqlstring = "SELECT ISNULL(POSCODE,'') AS POSCODE,ISNULL(POSDESC,'') AS POSDESC,ISNULL(POSSEQNO,0) AS POSSEQNO FROM POSMaster WHERE ISNULL(Freeze,'') <> 'Y'  ORDER BY POSCODE";
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.RowHeadersVisible = false;
                this.dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[1].ToString();
                    dataGridView2.Rows[i].Cells[0].ReadOnly = true;
                    //dataGridView2.Rows[i].Cells[1].Value = "NO";
                    dataGridView2.Rows[i].Height = 30;
                }
            }
        }






        public void fillsubgroup()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct SUBGroupcode,SUBGroupdesc from SUBGROUPMASTER where isnull(Freeze,'')<>'y' Order by SUBGroupdesc   ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                Cmb_subgroup.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cmb_subgroup.Items.Add(dt.Rows[i]["SUBGroupdesc"].ToString());
                }
                Cmb_subgroup.SelectedIndex = 0;
            }
        }


        public void fillgridtax()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT distinct ISNULL(CHARGECODE,'') AS CHARGECODE,chargedesc FROM CHARGEMASTER  WHERE ISNULL(RATE,0)=0 AND ISNULL(Freeze,'') <> 'Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {


                for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                {

                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridView2.Rows[i].Cells[4];
                    for (int j = 0; j <= dt.Rows.Count - 1; j++)
                    { cell.Items.Add(dt.Rows[j]["chargedesc"].ToString()); }

                }
            }
        }


        public void fillgriacccode()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT distinct ISNULL(CHARGECODE,'') AS CHARGECODE,chargedesc FROM CHARGEMASTER  WHERE ISNULL(RATE,0)=0 AND ISNULL(Freeze,'') <> 'Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {


                for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                {

                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridView2.Rows[i].Cells[4];
                    for (int j = 0; j <= dt.Rows.Count - 1; j++)
                    { cell.Items.Add(dt.Rows[j]["chargedesc"].ToString()); }

                }
            }
        }

        public void filltax()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT ISNULL(CHARGECODE,'') AS CHARGECODE,chargedesc FROM CHARGEMASTER  WHERE ISNULL(RATE,0)=0 AND ISNULL(Freeze,'') <> 'Y'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {


                taxdesc = dt.Rows[0]["chargedesc"].ToString();
                cmb_taxtype.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_taxtype.Items.Add(dt.Rows[i]["chargedesc"].ToString());
                }
                cmb_taxtype.SelectedIndex = 0;



            }
        }

        public void fillkitchen()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct kitchencode,kitchenname from kitchenmaster where isnull(Freeze,'')<>'y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                kitchencode = dt.Rows[0]["kitchencode"].ToString();
                cmb_kitchen.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_kitchen.Items.Add(dt.Rows[i]["kitchenname"].ToString());
                }
                cmb_kitchen.SelectedIndex = 0;
            }
        }


        public void fillUom()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            //sql = "SELECT DISTINCT ISNULL(UOMDESC,'') AS UOMDESC FROM UOMMaster WHERE ISNULL(Freeze,'') <> 'Y' ORDER BY UOMDESC ASC ";
            sql = "SELECT DISTINCT ISNULL(UOMCODE,'') AS UOMDESC FROM UOMMaster WHERE ISNULL(Freeze,'') <> 'Y' ORDER BY UOMDESC ASC ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                cmb_uom.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_uom.Items.Add(dt.Rows[i]["UOMDESC"].ToString());
                }
                cmb_uom.SelectedIndex = 0;
            }
        }
        public void fillgroup()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct Groupcode,Groupdesc from Groupmaster where isnull(Freeze,'')<>'y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {

                cmb_group.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_group.Items.Add(dt.Rows[i]["Groupdesc"].ToString());
                }
                cmb_group.SelectedIndex = 0;
            }
        }

        public void fillCategory()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct Categorycode,categoryname from poscategorymaster where isnull(Freeze,'')<>'y' ";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                cmb_category.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_category.Items.Add(dt.Rows[i]["categoryname"].ToString());
                }
                cmb_category.SelectedIndex = 0;
            }
        }

        public void fillinvitem()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "SELECT  DISTINCT ISNULL(I.ITEMCODE,'') AS ITEMCODE,ISNULL(I.ITEMNAME,'') AS ITEMNAME,ISNULL(O.uom,'') AS STOCKUOM FROM inv_inventoryitemmaster AS I  INNER JOIN inv_InventoryOpenningstock AS O ON O.ITEMCODE = I.ITEMCODE";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                cmb_invitem.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_invitem.Items.Add(dt.Rows[i]["ITEMNAME"].ToString());
                }
                cmb_invitem.SelectedIndex = 0;

            }
        }
        public void fillinvuom()
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct TRANUOM  from [INVITEM_TRANSUOM_LINK]";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                cmb_invuom.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_invuom.Items.Add(dt.Rows[i]["TRANUOM"].ToString());
                }
                cmb_invuom.SelectedIndex = 0;

            }
        }


        private void Cmb_stocakable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (Cmb_stocakable.Text != "YES")
            {
                cmb_invuom.Visible = false;
                cmb_invitem.Visible = false;
                label20.Visible = false;
                label21.Visible = false;


            }
            else
            {

                cmb_invuom.Visible = true;
                cmb_invitem.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
            }

        }
        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void showHideColumns(Boolean colHideStatus)
        {
            dataGridView2.Columns[1].Visible = colHideStatus;

        }

        private void CM_Stockcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (CM_Stockcontrol.Text != "NO")
            {

                dataGridView2.Columns[1].Visible = true;
                fillstorecode();

            }
            else
            {
                showHideColumns(false);

            }
        }
        private void cmb_group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        string CATEGORY;
        private void Cmb_subgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (Cmb_subgroup.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "SELECT s.subgroupcode, s.subgroupdesc,s.groupcode,g.groupdesc,g.category,c.categoryCode FROM subGroupMaster s inner join  groupmaster g on  s.groupcode=g.GroupCode inner join poscategorymaster c on c.categoryName=g.category   and subGroupDESC= '" + Cmb_subgroup.Text + "' ";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    CATEGORY = dt.Rows[0]["category"].ToString();
                    cmb_category.Text = CATEGORY;
                    Cmb_subgroup.Text = dt.Rows[0]["subgroupdesc"].ToString();
                    subgroupcode = dt.Rows[0]["SUBGroupcode"].ToString();
                    cmb_group.Text = dt.Rows[0]["groupdesc"].ToString();
                    groupcode = dt.Rows[0]["Groupcode"].ToString();

                }

            }

        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_taxtype_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_taxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Text;
            if (cmb_taxtype.Text != "")
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                sql = "SELECT ISNULL(CHARGECODE,'') AS CHARGECODE,ISNULL(CHARGEDESC,'') AS CHARGEDESC FROM CHARGEMASTER  WHERE ISNULL(RATE,0)=0  AND CHARGEDESC='" + cmb_taxtype.Text + "' AND ISNULL(Freeze,'') <> 'Y'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    taxdesc = dt.Rows[0]["CHARGECODE"].ToString();

                }
                //fillgridtax();
                FillItemTypeDetails_Det();

            }

        }





        private void txt_mrprate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }


        }

        private void txt_salesrate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_addcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_purchaserate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        Boolean MeValidate;
        int i;
        public void checkvalidate()
        {
            DataTable dt = new DataTable();
            MeValidate = false;
            if (Txt_itemname.Text == "")
            {
                MessageBox.Show(" Item Name cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_itemname.Focus();
                MeValidate = true;
                return;
            }
            if (Cmb_subgroup.Text == "")
            {
                MessageBox.Show(" Subgroup cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_subgroup.Focus();
                MeValidate = true;
                return;

            }
            if (cmb_taxtype.Text == "")
            {
                MessageBox.Show(" Tax  code cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_subgroup.Focus();
                MeValidate = true;
                return;
            }


            if (cmb_kitchen.Text == "")
            {
                MessageBox.Show(" Kitchen code cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_kitchen.Focus();
                MeValidate = true;
                return;
            }

            if (txt_mrprate.Text == "")
            {
                MessageBox.Show("MRP rate  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_subgroup.Focus();
                MeValidate = true;
                return;
            }

            if (txt_salesrate.Text == "")
            {
                MessageBox.Show("Sale rate  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_salesrate.Focus();
                MeValidate = true;
                return;
            }
            if (txt_purchaserate.Text == "")
            {
                txt_purchaserate.Focus();
                txt_purchaserate.Text = "0.00";
                MeValidate = false;

            }
            if (txt_addcess.Text == "")
            {
                txt_addcess.Focus();
                txt_addcess.Text = "0.00";
                MeValidate = false;
            }
            if (cmb_uom.Text == "")
            {
                MessageBox.Show("UOM  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_uom.Focus();

                MeValidate = false;
                return;
            }

            if (txt_maxqty.Text == "")
            {
                txt_maxqty.Focus();
                txt_maxqty.Text = "0.00";
                MeValidate = false;
            }


            if (Cmb_stocakable.Text == "YES")
            {
                if (cmb_invitem.Text == "")
                {
                    MessageBox.Show("item code  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmb_uom.Focus();
                    MeValidate = false;
                    return;
                }
                if (cmb_invuom.Text == "")
                {
                    MessageBox.Show("item uom  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmb_uom.Focus();
                    MeValidate = false;
                    return;
                }

            }
            if (CM_Stockcontrol.Text == "YES")
            {
                DataTable dts = new DataTable();

                for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                {
                    string G = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                    string T = Convert.ToString(dataGridView2.Rows[i].Cells[2].Value);
                    string R = Convert.ToString(dataGridView2.Rows[i].Cells[1].Value);

                    if (T == "True" && R == "")
                    {
                        MessageBox.Show("Store Code  cant be blank", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmb_uom.Focus();
                        MeValidate = true;
                        return;

                    }

                }
            }




            string groupstr = "";
            var grouplist = new List<string>();
            for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                {
                    grouplist.Add(dataGridView2.Rows[i].Cells[2].Value.ToString());
                    groupstr = groupstr + "'" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "',";
                }
            }
            groupstr = groupstr + "'',";
            if (grouplist.Count >= 1)
            {

                groupstr = groupstr.Substring(0, groupstr.Length - 1);
            }
            else { groupstr = groupstr.Substring(0, groupstr.Length - 1); }

            sqlstring = "";

            if (grouplist.Count >= 1)
            {
                sqlstring = " select loccode  from servicelocation_det where poscode in(" + groupstr + ")  group by loccode having count(loccode) >1";
            }
            else
            {
                sqlstring = " select loccode  from servicelocation_det where poscode in(" + groupstr + ")  group by loccode having count(loccode) >1";
            }
            dt = GCon.getDataSet(sqlstring);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show(" Pos is Tagged With More Than One  Service Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[i].Cells[2];
                chkbox.Value = false;



                MeValidate = true;
                return;

            }

            int j;

            var posList = new List<string>();

            for (i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                {
                    posList.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }

            if (posList.Count == 0)
            {
                MessageBox.Show("Kindly Select Minimum One Pos Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                dataGridView2.Focus();
                MeValidate = true;
                return;
            }




            MeValidate = false;
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            ArrayList List = new ArrayList();
            DateTime vDate;

            if (txt_itemcode.Text != "")
            {
                checkvalidate();

                if (MeValidate == true)
                {
                    return;
                }
                DataTable dt = new DataTable();
                itemcode = txt_itemcode.Text;
                sql = "select [dbo].[GetSeqno]('" + itemcode + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }

                sql = "select * FROM itemmaster  WHERE ItemCode='" + txt_itemcode.Text + "'";

                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {


                    sql = "UPDATE Itemmaster SET ItemDesc = '" + Txt_itemname.Text + "',";
                    sql = sql + " CATEGORY ='" + (cmb_category.Text) + "',";
                    sql = sql + " STORECODE ='',";
                    sql = sql + " kitchencode ='" + cmb_kitchen.Text + "',";
                    sql = sql + " stockable ='" + Cmb_stocakable.Text + "',";
                    //sql = sql + " KITCHENCODE ='" + cmb_kitchen.Text+ "',";
                    sql = sql + " ShortName = '" + (txt_itemcode.Text) + "',ItemTypeDESC='" + (cmb_taxtype.Text) + "',ITEMTYPECODE='" + taxdesc + "',";
                    sql = sql + " GroupCode = '" + groupcode + "',GROUPCODEDEC = '" + cmb_group.Text + "',subGroupCode = '" + subgroupcode + "',SUBGROUPDESC = '" + Cmb_subgroup.Text + "',";


                    if (Cmb_freeze.Text == "YES")
                    {
                        sql = sql + " freeze = 'Y',";
                    }
                    else
                    {
                        sql = sql + " freeze = 'N',";
                    }

                    sql = sql + " Multirate = 'N',";
                    string Text;
                    if (Cmb_fb.Text != "YES")
                    {
                        sql = sql + " FBFLAG = 'N',";
                    }
                    else
                    {
                        sql = sql + " FBFLAG = 'Y',";
                    }
                    sql = sql + " LWTFLAG = 'N',";
                    sql = sql + " mrptag = 'N',";
                    sql = sql + " mrpRate = " + Convert.ToDecimal(txt_mrprate.Text) + ",";
                    sql = sql + " TYPE_OF_ITEM ='General',";
                    sql = sql + " MaxQtyDay =" + (txt_maxqty.Text) + ",";
                    sql = sql + " StkCtl ='" + (CM_Stockcontrol.Text) + "',";
                    sql = sql + " ModifierType ='" + (Cmb_MFierType.Text) + "',";
                    sql = sql + " AddCessAmt =" + (txt_addcess.Text) + ",";
                    ssql = sql + " BaseUOMstd = '" + cmb_uom.Text + "',BaseRatestd=" + Convert.ToDecimal(txt_salesrate.Text) + ",";
                    if (rdb_opf.Checked == true)
                    {
                        sql = sql + " Openfacility = 'Y',";
                    }
                    else
                    {
                        sql = sql + " Openfacility = 'N',";
                    }
                    if (Rdb_KitItem.Checked == true)
                    {
                        sql = sql + " KitchenYN = 'Y',";
                    }
                    else
                    {
                        sql = sql + " KitchenYN = 'N',";
                    }
                    sql = sql + " UPDATEUSER = '" + GlobalVariable.gUserName + "', UPDATETIME ='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") + "'";


                    sql = sql + " WHERE ItemCode = '" + txt_itemcode.Text + "'";
                    List.Add(sql);

                    sql = "DELETE FROM POSMenulink WHERE ItemCode='" + txt_itemcode.Text + "'";
                    List.Add(sql);

                    vstring = Strings.Format(vstring, "dd-MMM-yyyy");
                    {
                        if (vstring == Strings.Format((DateTime)Dtp_EffectiveDate.Value, "dd-MMM-yyyy"))
                        {
                            {
                                vDate = ((DateTime)Dtp_EffectiveDate.Value).AddDays(-1);
                                // vDate = Strings.DateAdd(DateInterval.Day, -1, (DateTime)Dtp_EffectiveDate.Value);
                            }
                            sql = "UPDATE Ratemaster SET Endingdate='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' where ItemCodeseqno=" + (vseqno) + " And (EndingDate is Null OR Isnull(EndingDate,'') = '') ";
                            List.Add(sql);
                            sql = "UPDATE Ratemaster_log SET Endingdate='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' where ItemCodeseqno=" + (vseqno) + " And (EndingDate is Null OR Isnull(EndingDate,'') = '') ";
                            List.Add(sql);
                        }
                        else
                        {
                            {
                                vDate = ((DateTime)Dtp_EffectiveDate.Value).AddDays(-1);
                            }
                            sql = "UPDATE Ratemaster SET Endingdate='" + Strings.Format(vDate, "dd-MMM-yyyy") + "' where ItemCodeseqno=" + (vseqno) + " And (EndingDate is Null OR Isnull(EndingDate,'') = '') ";
                            List.Add(sql);
                            sql = "UPDATE Ratemaster_log SET Endingdate='" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' where ItemCodeseqno=" + (vseqno) + " And (EndingDate is Null OR Isnull(EndingDate,'') = '') ";
                            List.Add(sql);
                        }
                    }



                    int i;
                    var posList = new List<string>();
                    for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                    {
                        if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                        {
                            ssql = "select [dbo].[GetSeqno]('" + (dataGridView2.Rows[i].Cells[0].Value) + "')as vseqno";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pvseqno = dt.Rows[0]["vseqno"].ToString();
                            }
                            ssql = "select * from posmaster where POSDesc='" + (dataGridView2.Rows[i].Cells[0].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pos1 = dt.Rows[0]["poscode"].ToString();
                            }
                            ssql = "select chargecode from chargemaster where chargedesc='" + (dataGridView2.Rows[i].Cells[4].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                charge = dt.Rows[0]["chargecode"].ToString();
                            }

                            ssql = "select accode from accountsglaccountmaster where acdesc='" + (dataGridView2.Rows[i].Cells[5].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                acc = dt.Rows[0]["accode"].ToString();
                            }



                            sql = "INSERT INTO POSMenulink (ItemCodeseqno,Itemcode,POS,POSseqno,taxonitem,AcountCode,AcountCodeDesc) ";
                            sql = sql + " VALUES(" + (vseqno) + ", '" + txt_itemcode.Text + "'";
                            sql = sql + " ,'" + pos1 + "' , '" + pvseqno + "','" + charge + "','" + acc + "','" + (dataGridView2.Rows[i].Cells[5].Value) + "')";
                            List.Add(sql);
                        }
                    }
                    //foreach (string pos in posList)
                    //{



                    //}
                    sql = "UPDATE POSMENULINK SET TaxOnItem = ISNULL(ITEMTYPECODE,'') From ItemMaster I,POSMENULINK P Where I.ItemCode = P.ItemCode AND ISNULL(TaxOnItem,'') = ''";
                    List.Add(sql);

                    sql = " UPDATE POSMENULINK SET GSTFLAG = 'NO' WHERE TaxOnItem IN (SELECT CHARGECODE FROM CHARGEMASTER WHERE TAXTYPECODE IN (SELECT ItemTypeCode FROM ITEMTYPEMASTER WHERE ISNULL(GSTFlag,'NO') = 'NO')) AND ITEMCODE = '" + (txt_itemcode.Text) + "' ";
                    List.Add(sql);
                    ssql = " UPDATE POSMENULINK SET GSTFLAG = 'YES' WHERE TaxOnItem IN (SELECT CHARGECODE FROM CHARGEMASTER WHERE TAXTYPECODE IN (SELECT ItemTypeCode FROM ITEMTYPEMASTER WHERE ISNULL(GSTFlag,'NO') = 'YES')) AND ITEMCODE = '" + (txt_itemcode.Text) + "' ";
                    List.Add(ssql);


                    ////RATE UPDATE////
                    int j;
                    var invposList = new List<string>();
                    for (j = 0; j <= dataGridView2.RowCount - 1; j++)
                    {
                        if ((Convert.ToBoolean(dataGridView2.Rows[j].Cells[2].Value) == true))
                        {
                            ssql = "select [dbo].[GetSeqno]('" + dataGridView2.Rows[j].Cells[2].Value + "')as vseqno";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pvseqno = dt.Rows[0]["vseqno"].ToString();
                            }

                            ssql = "select * from posmaster where POSDesc='" + dataGridView2.Rows[j].Cells[0].Value + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pos1 = dt.Rows[0]["poscode"].ToString();
                            }

                            sql = " INSERT INTO RateMaster(ItemCodeseqno,ItemCode,UOM,ItemRate,PRITEMRATE,purcahseRate,MRPRate,";
                            sql = sql + " Startingdate,AddUserId,AddDateTime,rposcode)";
                            sql = sql + " VALUES( " + (vseqno) + ",";
                            sql = sql + " '" + txt_itemcode.Text + "','" + cmb_uom.Text + "',";
                            sql = sql + " " + Convert.ToDecimal(dataGridView2.Rows[j].Cells[3].Value) + ",0.00,";
                            sql = sql + "" + Convert.ToDecimal(txt_purchaserate.Text) + "," + Convert.ToDecimal(txt_mrprate.Text) + ",";
                            sql = sql + " '" + Strings.Format((DateTime)Dtp_EffectiveDate.Value, "dd-MMM-yyyy") + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' ";
                            sql = sql + " ,'" + pos1 + "' )";

                            List.Add(sql);
                        }

                    }


                    //////////////RATE UPDATE END//////////////////////

                }
                else
                {

                    //***********************************************************************
                    sql = "select upper(isnull(itemdesc,''))as itemdesc  from itemmaster ";
                    dt = GCon.getDataSet(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {

                            string s = (dt.Rows[k][0].ToString());
                            string p = Txt_itemname.Text;

                            if (s == p)
                            {
                                MessageBox.Show("Item Name Already Exist ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MeValidate = true;
                                return;
                            }
                        }

                    }
                    //************************************

                    int i;
                    string poschecked;
                    var posList = new List<string>();
                    for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                    {
                        if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                        {

                            ssql = "select [dbo].[GetSeqno]('" + (dataGridView2.Rows[i].Cells[0].Value) + "')as vseqno";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pvseqno = dt.Rows[0]["vseqno"].ToString();
                            }
                            ssql = "select * from posmaster where POSDesc='" + (dataGridView2.Rows[i].Cells[0].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pos1 = dt.Rows[0]["poscode"].ToString();
                            }
                            ssql = "select chargecode from chargemaster where chargedesc='" + (dataGridView2.Rows[i].Cells[4].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                charge = dt.Rows[0]["chargecode"].ToString();
                            }

                            ssql = "select accode from accountsglaccountmaster where acdesc='" + (dataGridView2.Rows[i].Cells[5].Value) + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                acc = dt.Rows[0]["accode"].ToString();
                            }



                            sql = "INSERT INTO POSMenulink (ItemCodeseqno,Itemcode,POS,POSseqno,taxonitem,AcountCode,AcountCodeDesc) ";
                            sql = sql + " VALUES(" + (vseqno) + ", '" + txt_itemcode.Text + "'";
                            sql = sql + " ,'" + pos1 + "' , '" + pvseqno + "','" + charge + "','" + acc + "','" + (dataGridView2.Rows[i].Cells[5].Value) + "')";
                            List.Add(sql);
                        }

                    }
                    //foreach (string pos in posList)
                    //{
                    //    ssql = "select [dbo].[GetSeqno]('" + pos + "')as vseqno";
                    //    dt = GCon.getDataSet(ssql);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        pvseqno = dt.Rows[0]["vseqno"].ToString();
                    //    }

                    //    sql = "INSERT INTO POSMenulink (ItemCodeseqno,Itemcode,POS,POSseqno) ";
                    //    sql = sql + " VALUES(" + (vseqno) + ", '" + txt_itemcode.Text + "'";
                    //    sql = sql + " ,'" + pos + "' , '" + pvseqno + "')";
                    //    List.Add(sql);
                    //}






                    sql = "INSERT INTO ItemMaster(ItemCodeseqno,ItemCode,ShortName,Category,kitchencode,ItemDesc,ItemTypeCode,ITEMTYPEDESC,ItemTypeseqno,GroupCode,GROUPCODEDEC,subGroupCode,SUBGROUPDESC,FBFLAG,LWTFLAG,Openfacility,KitchenYN,Groupseqno,";
                    sql = sql + " Freeze,Multirate,BaseUOMstd,BaseRatestd,BaseRate,mrptag,MRPRate,BaseUOM,BaseQty,StockControl,PromItemcode,";
                    sql = sql + " PromItemseqno,PromUOM,PromQty,PromRate,AddUserId,AddDateTime,StartingDate,TYPE_OF_ITEM,StkCtl,MaxQtyDay,AddCessAmt,STOCKABLE,ModifierType)";

                    sql = sql + " VALUES(" + vseqno + ",";
                    sql = sql + " '" + txt_itemcode.Text + "','" + txt_shortname.Text + "',";
                    sql = sql + " '" + (cmb_category.Text) + "','" + cmb_kitchen.Text + "',";
                    sql = sql + " '" + Txt_itemname.Text + "','" + taxdesc + "','" + cmb_taxtype.Text + "',";
                    string tvseqno;
                    ssql = "select [dbo].[GetSeqno]('" + cmb_taxtype.Text + "')as vseqno";
                    dt = GCon.getDataSet(ssql);
                    if (dt.Rows.Count > 0)
                    {
                        tvseqno = dt.Rows[0]["vseqno"].ToString();
                    }
                    sql = sql + " " + vseqno + ",'" + groupcode + "','" + cmb_group.Text + "','" + subgroupcode + "','" + Cmb_subgroup.Text + "',";
                    sql = sql + " 'N',";
                    sql = sql + " 'N',";

                    if (rdb_opf.Checked == true)
                    {
                        sql = sql + " 'Y',";
                    }
                    else
                    {
                        sql = sql + "  'N',";
                    }

                    if (Rdb_KitItem.Checked == true)
                    {
                        sql = sql + " 'Y',";
                    }
                    else
                    {
                        sql = sql + " 'N',";
                    }

                    ssql = "select [dbo].[GetSeqno]('" + groupcode + "')as vseqno";
                    dt = GCon.getDataSet(ssql);
                    if (dt.Rows.Count > 0)
                    {
                        gvseqno = dt.Rows[0]["vseqno"].ToString();
                    }
                    sql = sql + "" + gvseqno + ",'N','N',";

                    sql = sql + "'" + cmb_uom.Text + "'," + Convert.ToDecimal(txt_salesrate.Text) + ",0.00,";
                    //sql = sql + " 0,";

                    sql = sql + " 'N',";

                    sql = sql + "" + txt_mrprate.Text + ",'',0,'','',0,'',0,0,'" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' ,";
                    sql = sql + " '" + Strings.Format(Dtp_EffectiveDate.Value, "dd-MMM-yyyy") + "','General','" + CM_Stockcontrol.Text + "','" + txt_maxqty.Text + "','" + txt_addcess.Text + "','" + Cmb_stocakable.Text + "','" + Cmb_MFierType.Text + "')";
                    List.Add(sql);
                    int j;
                    var invposList = new List<string>();
                    for (j = 0; j <= dataGridView2.RowCount - 1; j++)
                    {
                        if ((Convert.ToBoolean(dataGridView2.Rows[j].Cells[2].Value) == true))
                        {
                            //invposList.Add(dataGridView2.Rows[j].Cells[0].Value.ToString());

                            ssql = "select [dbo].[GetSeqno]('" + dataGridView2.Rows[j].Cells[2].Value + "')as vseqno";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pvseqno = dt.Rows[0]["vseqno"].ToString();
                            }
                            ssql = "select * from posmaster where POSDesc='" + dataGridView2.Rows[j].Cells[0].Value + "'";
                            dt = GCon.getDataSet(ssql);
                            if (dt.Rows.Count > 0)
                            {
                                pos1 = dt.Rows[0]["poscode"].ToString();
                            }

                            sql = " INSERT INTO RateMaster(ItemCodeseqno,ItemCode,UOM,ItemRate,PRITEMRATE,purcahseRate,MRPRate,";
                            sql = sql + " Startingdate,AddUserId,AddDateTime,rposcode)";
                            sql = sql + " VALUES( " + (vseqno) + ",";
                            sql = sql + " '" + txt_itemcode.Text + "','" + cmb_uom.Text + "',";
                            sql = sql + " " + Convert.ToDecimal(dataGridView2.Rows[j].Cells[3].Value) + ",0.00,";
                            sql = sql + "" + Convert.ToDecimal(txt_purchaserate.Text) + "," + Convert.ToDecimal(txt_mrprate.Text) + ",";
                            sql = sql + " '" + Strings.Format((DateTime)Dtp_EffectiveDate.Value, "dd-MMM-yyyy") + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' ";
                            sql = sql + " ,'" + pos1 + "' )";

                            List.Add(sql);

                        }

                    }
                    //foreach (string pos in posList)
                    //{

                    //}


                }
                if (GCon.Moretransaction(List) > 0)
                {
                    MessageBox.Show("Data Added Successfully... ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List.Clear();
                    if (CM_Stockcontrol.Text == "YES")
                    {
                        POSSTORE();
                    }
                    rate_log();
                    if (Cmb_stocakable.Text == "YES")
                    {

                        sql = "delete from BOM_hdr where itemcode='" + txt_itemcode.Text + "'";
                        dt = GCon.getDataSet(sql);

                        sql = "delete from BOM_det where itemcode='" + txt_itemcode.Text + "'";
                        dt = GCon.getDataSet(sql);
                        addBOM();
                    }
                    ;

                    sql = " UPDATE POSMENULINK SET GSTFLAG = 'NO' WHERE TaxOnItem IN (SELECT CHARGECODE FROM CHARGEMASTER WHERE TAXTYPECODE IN (SELECT ItemTypeCode FROM ITEMTYPEMASTER WHERE ISNULL(GSTFlag,'NO') = 'NO')) AND ITEMCODE = '" + (txt_itemcode.Text) + "' ";
                    dt = GCon.getDataSet(sql);
                    ssql = " UPDATE POSMENULINK SET GSTFLAG = 'YES' WHERE TaxOnItem IN (SELECT CHARGECODE FROM CHARGEMASTER WHERE TAXTYPECODE IN (SELECT ItemTypeCode FROM ITEMTYPEMASTER WHERE ISNULL(GSTFlag,'NO') = 'YES')) AND ITEMCODE = '" + (txt_itemcode.Text) + "' ";
                    dt = GCon.getDataSet(ssql);

                    ssql = " UPDATE posmenulink SET ACOUNTCODEDESC = A.ACDESC FROM ACCOUNTSGLACCOUNTMASTER A,posmenulink P WHERE A.ACCODE = P.ACOUNTCODE AND ISNULL(P.ACOUNTCODEDESC,'') = ''  ";
                    dt = GCon.getDataSet(ssql);
                    ssql = " UPDATE posmenulink SET ACOUNTCODEDESC = A.ACDESC FROM ACCOUNTSGLACCOUNTMASTER A,posmenulink P WHERE A.ACCODE = P.ACOUNTCODE AND ISNULL(P.ITEMCODE,'') = '" + txt_itemcode + "'  ";
                    dt = GCon.getDataSet(ssql);

                    btn_new_Click(sender, e);
                }
            }
        }



        private void rate_log()
        {

            ArrayList List = new ArrayList();
            DataTable dt = new DataTable();
            int j;
            var posList = new List<string>();
            for (j = 0; j <= dataGridView2.RowCount - 1; j++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[j].Cells[2].Value) == null))
                {
                    posList.Add(dataGridView2.Rows[j].Cells["POSCODE"].Value.ToString());
                }

            }
            foreach (string pos in posList)
            {
                ssql = "select [dbo].[GetSeqno]('" + pos + "')as vseqno";
                dt = GCon.getDataSet(ssql);
                if (dt.Rows.Count > 0)
                {
                    pvseqno = dt.Rows[0]["vseqno"].ToString();
                }
                sql = " INSERT INTO RateMaster_log(ItemCodeseqno,ItemCode,UOM,ItemRate,PRITEMRATE,purcahseRate,MRPRate,";
                sql = sql + " Startingdate,AddUserId,AddDateTime,rposcode)";
                sql = sql + " VALUES( " + (vseqno) + ",";
                sql = sql + " '" + txt_itemcode.Text + "','" + cmb_uom.Text + "',";
                sql = sql + " " + Convert.ToDecimal(txt_salesrate.Text) + ",0.00,";
                sql = sql + "" + Convert.ToDecimal(txt_purchaserate.Text) + "," + Convert.ToDecimal(txt_mrprate.Text) + ",";
                sql = sql + " '" + Strings.Format((DateTime)Dtp_EffectiveDate.Value, "dd-MMM-yyyy") + "','" + GlobalVariable.gUserName + "','" + Strings.Format(DateAndTime.Now, "dd-MMM-yyyy") + "' ";
                sql = sql + " ,'" + pos + "' )";

                List.Add(sql);

            }
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
        }

        double covvalue;
        private void addBOM()
        {

            ArrayList List = new ArrayList();
            DataTable dt = new DataTable(); sql = "select convvalue FROM INVENTORY_TRANSCONVERSION WHERE BASEUOM='" + cmb_uom.Text + "' AND TRANSUOM='" + cmb_invuom.Text + "'"; dt = GCon.getDataSet(sql); if (dt.Rows.Count > 0) { covvalue = Convert.ToDouble(dt.Rows[0]["convvalue"].ToString()); } sqlstring = "INSERT INTO BOM_hdr(Itemcode,Itemname,ItemUOM,Void,Adduser,Adddate) values('" + txt_itemcode.Text + "','" + Txt_itemname.Text + "','" + cmb_uom.Text + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateTime.Now, "dd-MMM-yyyy") + "')"; List.Add(sqlstring);

            sqlstring = "INSERT INTO BOM_det(Itemcode,Itemname,ItemUOM,gitemcode, gitemname,gUOM,gqty,gdblamt,Void,Adduser,Adddate,CTYN)  values ('" + txt_itemcode.Text + "','" + Txt_itemname.Text + "','" + cmb_uom.Text + "','" + cmb_invitem.Text + "','" + cmb_invitem.Text + "','" + cmb_invuom.Text + "','1','" + covvalue.ToString() + "','N','" + GlobalVariable.gUserName + "','" + Strings.Format(DateTime.Now, "dd-MMM-yyyy") + "','N')";
            List.Add(sqlstring);
            if (GCon.Moretransaction(List) > 0)
            {
                List.Clear();
            }
        }


        private void POSSTORE()
        {
            ArrayList List = new ArrayList();
            DataTable dt = new DataTable();
            if (CM_Stockcontrol.Text == "YES")
            {

                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {

                    ssql = "DELETE FROM POSITEMSTORELINK WHERE ITEMCODE = '" + txt_itemcode.Text + "'";
                    dt = GCon.getDataSet(ssql);
                    string T = Convert.ToString(dataGridView2.Rows[i].Cells[1].Value);
                    string R = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                    if (T != "")
                    {

                        ssql = "INSERT INTO POSITEMSTORELINK (POS,ITEMCODE,STORECODE) VALUES ('" + R + "','" + txt_itemcode.Text + "','" + T + "')";
                        List.Add(ssql);



                    }
                }
                if (GCon.Moretransaction(List) > 0)
                {
                    List.Clear();
                }
            }
        }




        //private void STORE()
        //{
        //    ArrayList List = new ArrayList();
        //    DataTable dt = new DataTable();
        //    if (CM_Stockcontrol.Text == "YES")
        //    {
        //        int j;
        //        var listinv = new List<string>();
        //        var listpos = new List<string>();
        //        for (j = 0; j <= dataGridView1.RowCount - 1; j++)
        //        {
        //            if ((dataGridView1.Rows[j].Cells[1].Value != null))
        //            {
        //                ssql = "DELETE FROM POSITEMSTORELINK WHERE ITEMCODE = '" + txt_itemcode.Text + "'";
        //                dt = GCon.getDataSet(ssql);
        //                listinv.Add(dataGridView1.Rows[j].Cells["STORECODE"].Value.ToString());
        //                listpos.Add(dataGridView1.Rows[j].Cells["POSCODE"].Value.ToString());
        //            }

        //        }
        //            foreach (string STORE in listinv)
        //            {


        //                foreach (string pos in listpos)
        //                {


        //                    ssql = "INSERT INTO POSITEMSTORELINK (POS,ITEMCODE,STORECODE) VALUES ('" + pos + "','" + txt_itemcode.Text + "','" + STORE + "')";
        //                    List.Add(ssql);

        //                }


        //                }

        //                
        //        }



        //}




        public void Fillitemcode(string itemcode, int rowindex)
        {
            if (itemcode != "")
            {
                txt_itemcode.Text = itemcode;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void btn_edit_Click(object sender, EventArgs e)
        {

            Itemselection IS = new Itemselection(this);
            IS.ShowDialog();
            Cmb_freeze.Enabled = true;

            //this.Hide();
        }
        private void fillstorecode()
        {
            int i, j;
            sql = "select storecode from storemaster";

            DataTable dt = new DataTable();
            dt = GCon.getDataSet(sql);

            for (i = 0; i <= dataGridView2.RowCount - 1; i++)
            {

                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView2.Rows[i].Cells[1];
                for (j = 0; j <= dt.Rows.Count - 1; j++)
                { cbCell.Items.Add(dt.Rows[j]["storecode"].ToString()); }

            }
        }



        private void txt_itemcode_Validated(object sender, EventArgs e)
        {

            if (txt_itemcode.Text != "")
            {
                DataTable dt = new DataTable();
                DataTable dts = new DataTable();
                itemcode = txt_itemcode.Text;
                sql = "select [dbo].[GetSeqno]('" + itemcode + "')as vseqno";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    vseqno = dt.Rows[0]["vseqno"].ToString();
                }


                ssql = "SELECT isnull(wefdate,'') as wefdate,ISNULL(ITEMDESC,'') AS ITEMDESC,ISNULL(SHORTNAME,'') AS SHORTNAME,isnull(category,'') as CATEGORY,ISNULL(ITEMTYPEDESC,'') AS ITEMTYPEDESC,ISNULL(SUBSCODE,'') AS SUBSCODE,ISNULL(ITEMTYPECODE,'') AS ITEMTYPECODE,ISNULL(BASEUOMSTD,'') as BASEUOMSTD,";
                ssql = ssql + " ISNULL(BASERATESTD,'0.00') as BASERATESTD,ISNULL(GROUPCODE,'') AS GROUPCODE,ISNULL(FREEZE,'') AS FREEZE,ISNULL(ADDDATETIME,GETDATE()) AS ADDDATETIME,";
                ssql = ssql + " ISNULL(MULTIRATE,'') AS MULTIRATE,ISNULL(PROMOTIONAL,0) AS PROMOTIONAL,ISNULL(BASEUOM,'') AS BASEUOM,ISNULL(BASEQTY,0) AS BASEQTY,";
                ssql = ssql + " ISNULL(BASERATE,0) AS BASERATE,ISNULL(MRPTAG,'') AS MRPTAG,ISNULL(MRPRATE,0) AS MRPRATE,isnull(LWTFLAG,'') as LWTFLAG,isnull(fbflag,'') fbflag, ISNULL(STARTINGDATE,GETDATE()) AS STARTINGDATE,ISNULL(ENDINGDATE,GETDATE()) AS ENDINGDATE,";
                ssql = ssql + " ISNULL(PROMUOM,'') AS PROMUOM,ISNULL(PROMQTY,0) AS PROMQTY,isnull(kitchencode,'') as kitchencode,ISNULL(PROMRATE,0) AS PROMRATE,ISNULL(PROMITEMCODE,'') AS PROMITEMCODE,ISNULL(OPENFACILITY,'') AS OPENFACILITY,ISNULL(SALESACCTIN,'') AS SALESACCTIN,ISNULL(SALESSLCODE,'') AS SALESSLCODE,ISNULL(STORECODE,'') AS STORECODE,ISNULL(subgroupcode,'') AS subgroupcode,ISNULL(subgroupdesc,'') AS subgroupdesc,ISNULL(TYPE_OF_ITEM,'') AS TYPE_OF_ITEM,ISNULL(StkCtl,'NO') AS StkCtl,ISNULL(MaxQtyDay,0) AS MaxQtyDay,ISNULL(AddCessAmt,0) AS AddCessAmt, ";
                ssql = ssql + " ISNULL(EveLimit,0) AS EveLimit,ISNULL(EveFromDate,'') AS EveFromDate,ISNULL(EveToDate,'') AS EveToDate,isnull(groupcodedec,'')as groupcodedec,isnull(stockable,'')as stockable,Isnull(ModifierType,'No') as ModifierType,Isnull(KitchenYN,'N') as KitchenYN FROM ItemMaster WHERE ItemCode='" + txt_itemcode.Text + "'";

                dt = GCon.getDataSet(ssql);
                if (dt.Rows.Count > 0)
                {
                    Txt_itemname.Text = dt.Rows[0]["ITEMDESC"].ToString();
                    Cmb_subgroup.Text = dt.Rows[0]["subgroupdesc"].ToString();
                    cmb_group.Text = dt.Rows[0]["groupcodedec"].ToString();
                    cmb_category.Text = dt.Rows[0]["CATEGORY"].ToString();
                    txt_shortname.Text = dt.Rows[0]["SHORTNAME"].ToString();
                    cmb_kitchen.Text = dt.Rows[0]["kitchencode"].ToString();
                    cmb_taxtype.Text = dt.Rows[0]["ITEMTYPEDESC"].ToString();
                    txt_addcess.Text = dt.Rows[0]["AddCessAmt"].ToString();
                    txt_maxqty.Text = dt.Rows[0]["MaxQtyDay"].ToString();
                    CM_Stockcontrol.Text = dt.Rows[0]["StkCtl"].ToString();
                    string opfaclity = dt.Rows[0]["OPENFACILITY"].ToString();
                    Cmb_stocakable.Text = dt.Rows[0]["stockable"].ToString();
                    Cmb_MFierType.Text = dt.Rows[0]["ModifierType"].ToString();
                    string KitYN = dt.Rows[0]["KitchenYN"].ToString().Trim();
                    string FBFLAG = dt.Rows[0]["FBFLAG"].ToString();
                    string FREEZE = dt.Rows[0]["FREEZE"].ToString();
                    if (FREEZE == "N")
                    {

                        Cmb_freeze.Text = "NO";
                    }
                    else
                    {
                        Cmb_freeze.Text = "YES";
                    }
                    if (FBFLAG == "N")
                    {
                        Cmb_fb.Text = "NO";
                    }
                    else
                    {
                        Cmb_fb.Text = "YES";
                    }
                    if (opfaclity == "Y ")
                    {
                        rdb_opf.Checked = true;
                    }
                    else
                    {
                        rdb_opf.Checked = false;
                    }
                    if (KitYN == "Y")
                    {
                        Rdb_KitItem.Checked = true;
                    }
                    else
                    {
                        Rdb_KitItem.Checked = false;
                    }

                }

                ssql = "SELECT ISNULL(StartingDate,'') as StartingDate,ISNULL(UOM,'') AS UOM,ISNULL(ITEMRATE,0) AS ITEMRATE,ISNULL(MRPRATE,0) AS MRPRATE,ISNULL(PURCAHSERATE,0) AS PURCAHSERATE FROM RateMaster WHERE Itemcode='" + txt_itemcode.Text + "' and ISNULL(ENDINGDATE,'')='' ";
                dt = GCon.getDataSet(ssql);
                if (dt.Rows.Count > 0)
                {
                    txt_purchaserate.Text = dt.Rows[0]["PURCAHSERATE"].ToString();
                    txt_salesrate.Text = dt.Rows[0]["ITEMRATE"].ToString();
                    txt_mrprate.Text = dt.Rows[0]["MRPRATE"].ToString();
                    cmb_uom.Text = dt.Rows[0]["uom"].ToString();
                }
                string item;
                ssql = "Select isnull(gitemname,'') as itemname,isnull(guom,'') itemuom from bom_det where itemcode='" + txt_itemcode.Text + "'";
                dt = GCon.getDataSet(ssql);
                if (dt.Rows.Count > 0)
                {
                    item = dt.Rows[0]["itemname"].ToString();
                    cmb_invitem.Text = item;
                    cmb_invuom.Text = dt.Rows[0]["itemuom"].ToString();
                }


                //sql = "SELECT b.itemcode ,* FROM POSmenulink a inner join ratemaster b on a.itemcode=b.itemcode  WHERE Itemcode='" + txt_itemcode.Text + "'";
                sql = "SELECT ISNULL(ITEMRATE,0) AS ITEMRATE,ISNULL(RPOSCODE,'') AS POS FROM RateMaster WHERE Itemcode='" + txt_itemcode.Text + "'and ISNULL(EndingDate,'')=''";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        pos1 = "";
                        sql = "select posdesc from posmaster where poscode='" + (dt.Rows[i][1].ToString()) + "'";
                        dts = GCon.getDataSet(sql);
                        if (dts.Rows.Count > 0)
                        {
                            pos1 = dts.Rows[0][0].ToString();
                        }
                        for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                        {



                            string s = pos1;
                            string p = dataGridView2.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewCheckBoxCell chkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[j].Cells[2];
                                chkbox.Value = true;
                                dataGridView2.Rows[j].Cells[3].Value = dt.Rows[i].ItemArray[0].ToString();
                            }
                        }

                    }
                }


                sql = "SELECT ISNULL(taxonitem,'') AS taxonitem,ISNULL(AcountCodeDesc,'') AcountCodeDesc, POS FROM POSMENULINK WHERE Itemcode='" + txt_itemcode.Text + "'";
                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        pos1 = "";
                        sql = "select posdesc from posmaster where poscode='" + (dt.Rows[i]["POS"].ToString()) + "'";
                        dts = GCon.getDataSet(sql);
                        if (dts.Rows.Count > 0)
                        {
                            pos1 = dts.Rows[0][0].ToString();
                        }

                        charge = "";
                        sql = "select chargedesc from chargemaster where chargecode='" + (dt.Rows[i]["taxonitem"].ToString()) + "'";
                        dts = GCon.getDataSet(sql);
                        if (dts.Rows.Count > 0)
                        {
                            charge = dts.Rows[0][0].ToString();
                        }
                        acc = "";
                        sql = "select accode from accountsglaccountmaster where acdesc='" + (dt.Rows[i]["AcountCodeDesc"].ToString()) + "'";
                        dts = GCon.getDataSet(sql);
                        if (dts.Rows.Count > 0)
                        {
                            acc = dts.Rows[0][0].ToString();
                        }

                        for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                        {



                            string s = pos1;
                            string p = dataGridView2.Rows[j].Cells[0].Value.ToString();
                            if (s == p)
                            {
                                DataGridViewComboBoxCell cbCell1 = (DataGridViewComboBoxCell)dataGridView2.Rows[j].Cells[4];
                                cbCell1.Value = charge.ToString();
                                dataGridView2.Rows[j].Cells[5].Value = dt.Rows[i].ItemArray[1].ToString();
                            }
                        }
                    }
                }




                sql = "select isnull(pos,'')pos,isnull(itemcode,'')itemcode,isnull(storecode,'')as storecode from POSITEMSTORELINK WHERE Itemcode='" + txt_itemcode.Text + "'";
                dt = GCon.getDataSet(sql);
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            for (int j = 0; j < dataGridView2.RowCount - 1; j++)
                            {
                                string T = (dt.Rows[i]["POS"].ToString());
                                string R = Convert.ToString(dataGridView2.Rows[j].Cells[0].Value);
                                if (T == R)
                                {
                                    string STORECODE = dt.Rows[i]["storecode"].ToString();
                                    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView2.Rows[j].Cells[1];

                                    cbCell.Value = STORECODE.ToString();


                                }


                            }
                        }






                    }
                }
                catch (Exception asdf)
                {

                }


            }
        }


        private void txt_itemcode_TextChanged(object sender, EventArgs e)
        {
            txt_itemcode_Validated(sender, e);
            if (txt_itemcode.Text != "")
            {
                DataTable dt = new DataTable();


                sql = "SELECT itemcode FROM itemmaster WHERE ItemCode='" + txt_itemcode.Text + "'";

                dt = GCon.getDataSet(sql);
                if (dt.Rows.Count == 0)
                {
                    btn_new_Click(sender, e);
                    txt_itemcode.Text = itemcode;
                }
                txt_itemcode.Text = itemcode;
            }

        }

        private void txt_itemcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_itemcode_Validated(sender, e);

        }

        private void btn_new_Click(object sender, EventArgs e)
        {

            txt_mrprate.Text = "1.00";
            txt_salesrate.Text = "1.00";
            txt_purchaserate.Text = "1.00";

            this.Dtp_EffectiveDate.Value = DateTime.Now;
            cmb_category.Enabled = false;
            cmb_category.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_group.Enabled = false;
            cmb_group.DropDownStyle = ComboBoxStyle.DropDownList;
            Txt_itemname.Focus();
            Cmb_freeze.SelectedIndex = 0;
            Cmb_freeze.Enabled = false;
            Cmb_freeze.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_taxtype.Text = "";
            Cmb_fb.Text = "";
            Txt_itemname.Text = "";
            txt_shortname.Text = "";
            cmb_category.Text = "";
            cmb_group.Text = "";
            Cmb_stocakable.Text = "";
            CM_Stockcontrol.Text = "";
            txt_salesrate.Text = "";
            txt_mrprate.Text = "";
            txt_maxqty.Text = "";
            txt_purchaserate.Text = "";
            txt_maxqty.Text = "";
            Cmb_subgroup.Text = "";
            cmb_kitchen.Text = "";
            cmb_uom.Text = "";
            Rdb_KitItem.Checked = false;
            cmb_category.Text = "";
            txt_addcess.Text = "";
            rdb_opf.Checked = false;
            cmb_invitem.Text = "";
            cmb_invuom.Text = "";

            cmb_category.SelectedIndex = -1;

            cmb_group.SelectedIndex = -1;

            Cmb_subgroup.SelectedIndex = -1;

            cmb_uom.SelectedIndex = -1;

            cmb_kitchen.SelectedIndex = -1;
            Cmb_fb.SelectedIndex = -1;

            Cmb_MFierType.SelectedIndex = 0;

            cmb_taxtype.SelectedIndex = -1;
            Cmb_stocakable.SelectedIndex = -1;
            cmb_invuom.SelectedIndex = -1;
            CM_Stockcontrol.SelectedIndex = -1;
            cmb_invitem.SelectedIndex = -1;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            Maxid();

            fillstorecode();
            fillpos();
            fillgridtax();
            showHideColumns(false);
            //this.Dtp_EffectiveDate.Enabled = false;
        }

        public bool TRUE { get; set; }

        //private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    MessageBox.Show("Error happened " + e.Exception.ToString());
        //}

        private void cmb_invitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = new DataTable();
            sql = "select distinct TRANUOM  from [INVITEM_TRANSUOM_LINK] where itemname='" + cmb_invitem.Text + "'";
            dt = GCon.getDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                cmb_invuom.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_invuom.Items.Add(dt.Rows[i]["TRANUOM"].ToString());
                }
                cmb_invuom.SelectedIndex = 0;

            }
        }
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Error happened " + e.Exception.ToString());

        }

        private void txt_mrprate_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_category_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Txt_itemname_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataTable dt = new DataTable();
            int rowid = dataGridView2.CurrentRow.Index;

            if (dataGridView2.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 2)
            {

                if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
                {
                    dataGridView2.Rows[rowid].Cells[2].Value = false;
                }
                else
                {
                    dataGridView2.Rows[rowid].Cells[2].Value = true;
                }
                //****************************************
                int i;
                string groupstr = "";
                var grouplist = new List<string>();
                for (i = 0; i <= dataGridView2.RowCount - 1; i++)
                {
                    if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value) == true))
                    {
                        grouplist.Add(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        groupstr = groupstr + "'" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "',";
                    }
                }
                groupstr = groupstr + "'',";
                if (grouplist.Count >= 1)
                {

                    groupstr = groupstr.Substring(0, groupstr.Length - 1);
                }
                else { groupstr = groupstr.Substring(0, groupstr.Length - 1); }

                sqlstring = "";

                if (grouplist.Count >= 1)
                {
                    sqlstring = " select loccode  from servicelocation_det where poscode in(" + groupstr + ")  group by loccode having count(loccode) >1";
                }
                else
                {
                    sqlstring = " select loccode  from servicelocation_det where poscode in(" + groupstr + ")  group by loccode having count(loccode) >1";
                }
                dt = GCon.getDataSet(sqlstring);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(" Pos is Tagged With More Than One  Service Location", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
                    {
                        dataGridView2.Rows[rowid].Cells[2].Value = false;
                    }
                    else
                    {
                        dataGridView2.Rows[rowid].Cells[2].Value = true;
                    }


                    return;

                }
                //************************************************



                for (int j = 0; j <= dataGridView2.RowCount - 1; j++)
                {
                    if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
                    {
                        dataGridView2.Rows[rowid].Cells[3].Value = txt_salesrate.Text;
                        string taxcode = cmb_taxtype.Text;
                        DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView2.Rows[rowid].Cells[4];

                        cbCell.Value = taxcode.ToString();


                    }
                    else
                    {
                        dataGridView2.Rows[rowid].Cells[3].Value = "";
                        dataGridView2.Rows[rowid].Cells[4].Value = "";
                    }
                }
                if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
                {


                    if (dataGridView2.CurrentCell.ColumnIndex == 5)
                    {
                        groupBox7.Visible = true;

                        //Text = this.dataGridView3.CurrentRow.Cells[1].Value.ToString();

                        //string accountcode = Text;
                        //dataGridView2.Rows[rowid].Cells[5].Value = accountcode;
                        //groupBox7.Visible = false;



                    }

                }
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void chk_gg_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_gg.Checked == true)
            {
                fillgridtax();
            }
        }

        //private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //     int rowid = dataGridView2.CurrentRow.Index;
        //     if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
        //     {


        //         if (dataGridView2.CurrentCell.ColumnIndex == 5)
        //         {
        //             groupBox7.Visible = true;

        //             //Text = this.dataGridView3.CurrentRow.Cells[1].Value.ToString();

        //             //string accountcode = Text;
        //             //dataGridView2.Rows[rowid].Cells[5].Value = accountcode;
        //             //groupBox7.Visible = false;



        //         }

        //     }
        //}

        //private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //   

        //}

        private void Cmd_Exit_Click(object sender, EventArgs e)
        {

            groupBox7.Visible = false;
        }

        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            int rowid = dataGridView2.CurrentRow.Index;
            Text = this.dataGridView3.CurrentRow.Cells[1].Value.ToString();
            string accountcode = Text;
            dataGridView2.Rows[rowid].Cells[5].Value = accountcode;
            groupBox7.Visible = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = dataGridView2.CurrentRow.Index;
            if ((Convert.ToBoolean(dataGridView2.Rows[rowid].Cells[2].Value) == true))
            {
                DataTable dt = new DataTable();


                if (dataGridView2.CurrentCell.ColumnIndex == 5)
                {
                    groupBox7.Visible = true;



                }
            }

        }

        public void fillGRIDitem()
        {
            DataTable PosCate = new DataTable();
            if (comboBox2.Text == "Accode")
            {
                sql = " select distinct Accode,Acdesc from ACCOUNTSGLACCOUNTMASTER where Accode like'%" + Txt_Modifier.Text + "%' ";
            }
            else if (comboBox2.Text == "ACDESC")
            {
                sql = " select distinct Accode,Acdesc from ACCOUNTSGLACCOUNTMASTER where Acdesc like'%" + Txt_Modifier.Text + "%' ";
            }
            else
            {
                sql = " select distinct Accode,Acdesc from ACCOUNTSGLACCOUNTMASTER where Acdesc like'%" + Txt_Modifier.Text + "%' ";

            }
            PosCate = GCon.getDataSet(sql);
            if (PosCate.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();

                this.dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                for (int i = 0; i < PosCate.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = PosCate.Rows[i].ItemArray[0];
                    dataGridView3.Rows[i].Cells[1].Value = PosCate.Rows[i].ItemArray[1];

                }
            }
        }


        private void Txt_Modifier_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Modifier.Text != "")
            {
                fillGRIDitem();
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
