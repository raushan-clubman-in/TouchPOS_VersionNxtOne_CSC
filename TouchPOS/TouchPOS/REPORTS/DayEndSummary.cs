using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutLook = Microsoft.Office.Interop.Outlook; 

namespace TouchPOS.REPORTS
{
    public partial class DayEndSummary : Form
    {
        GlobalClass GCon = new GlobalClass();

        public DayEndSummary()
        {
            InitializeComponent();
        }

        string sql = "", sqlstring = "";

        private void DayEndSummary_Load(object sender, EventArgs e)
        {
            BlackGroupBox();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void BlackGroupBox()
        {
            GlobalClass.myGroupBox myGroupBox1 = new GlobalClass.myGroupBox();
            myGroupBox1.Text = "";
            myGroupBox1.BorderColor = Color.Black;
            myGroupBox1.Size = groupBox1.Size;
            groupBox1.Controls.Add(myGroupBox1);
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            DataTable BillData = new DataTable();

            if (Convert.ToDateTime(dtp2.Value).Date < Convert.ToDateTime(dtp1.Value).Date)
            {
                MessageBox.Show("To Date Can't be less the From Date", GlobalVariable.gCompanyName);
                return;
            }
            dataGridView1.Rows.Clear();
            //sqlstring = "Exec DayEnd_Report '" + dtp1.Value.ToString("dd-MMM-yyyy") + "'";
            sqlstring = "Exec DayEnd_Report_Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "','" + dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(sqlstring);

            //sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            BillData = GCon.getDataSet(sql);
            if (BillData.Rows.Count > 0)
            {
                int slno = 0;
                dataGridView1.Rows.Clear();
                //DataGridViewCellStyle style = new DataGridViewCellStyle();
                //style.Font = new Font(dataGridView1.Font, FontStyle.Bold);

                for (int i = 0; i < BillData.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    slno = Convert.ToInt32(BillData.Rows[i].ItemArray[0]);
                    dataGridView1.Rows[i].Cells[0].Value = BillData.Rows[i].ItemArray[1];
                    if (slno == 6 || slno == 12 || slno == 16)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = "Amount";
                    }
                    else 
                    {
                        dataGridView1.Rows[i].Cells[1].Value = BillData.Rows[i].ItemArray[2];
                    }
                    if (slno == 6) { dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen; }
                    if (slno == 9 || slno == 10 || slno == 16) { dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Silver; }
                    if (slno == 19) { dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange; }
                    //dataGridView1.Rows[i].DefaultCellStyle = style;
                    //dataGridView1.Rows[i].Height = 30;
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmd_Export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1) 
            {
                MessageBox.Show("Before Export Run Get Details", GlobalVariable.gCompanyName);
                return;
            }

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] = "Day End " + " Report Between " + " " + dtp1.Value.ToString("dd-MMM-yyyy");
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[2, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 3, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 3, j + 1] = "";
                    }
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void Cmd_ViewRpt_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;
            Report rv = new Report();
            CRYSTAL.Crpt_DayEndReport RPS = new CRYSTAL.Crpt_DayEndReport();

            if (Convert.ToDateTime(dtp2.Value) < Convert.ToDateTime(dtp1.Value)) 
            {
                MessageBox.Show("To Date Can't be less the From Date",GlobalVariable.gCompanyName);
                return;
            }

            //sqlstring = "Exec DayEnd_Report '" + dtp1.Value.ToString("dd-MMM-yyyy") + "'";
            sqlstring = "Exec DayEnd_Report_Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "','" + dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(sqlstring);

            //sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            GCon.getDataSet1(sql, "DayEndReport");
            if (GlobalVariable.gdataset.Tables["DayEndReport"].Rows.Count > 0) 
            {
                rv.GetDetails(sql, "DayEndReport", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
                TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ2.Text = "Date : " + dtp1.Value.ToString("dd-MMM-yyyy") + " To " + dtp2.Value.ToString("dd-MMM-yyyy");
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text7"];
                TXTOBJ3.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                if (GlobalVariable.gCompName == "SKYYE") { }
                else 
                {
                    CrystalDecisions.CrystalReports.Engine.PictureObject p1;
                    p1 = (PictureObject)RPS.ReportDefinition.ReportObjects["Picture1"];
                    p1.Height = 0;
                    p1.Width = 0;
                }

                rv.Show();
            }
        }

        public void fromCrystalExportTo(Object report)
        {
            try
            {
                Boolean X;
                String vpath, vLog, strpath;
                vpath = Application.StartupPath + @"\Reports\DayEndSummary";
                vpath = vpath + ".pdf";
                if (File.Exists(vpath) == true)
                {
                    File.Delete(vpath);
                }
                ((ReportDocument)report).ExportToDisk(ExportFormatType.PortableDocFormat, vpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export to PDF not Done");
            }

        }

        private void Cmd_Mail_Click(object sender, EventArgs e)
        {
            int i;
            String sqlstring;

            //if (System.Diagnostics.Process.GetProcessesByName("OUTLOOK").Any()) { }
            //else { MessageBox.Show("OutLook not Open, plz open first then try send Email"); return; }

            Report rv = new Report();
            CRYSTAL.Crpt_DayEndReport RPS = new CRYSTAL.Crpt_DayEndReport();

            if (Convert.ToDateTime(dtp2.Value.ToString()) < Convert.ToDateTime(dtp1.Value.ToString()))
            {
                MessageBox.Show("To Date Can't be less the From Date", GlobalVariable.gCompanyName);
                return;
            }
            //sqlstring = "Exec DayEnd_Report '" + dtp1.Value.ToString("dd-MMM-yyyy") + "'";
            sqlstring = "Exec DayEnd_Report_Between '" + dtp1.Value.ToString("dd-MMM-yyyy") + "','" + dtp2.Value.ToString("dd-MMM-yyyy") + "'";
            GCon.ExecuteStoredProcedure(sqlstring);

            //sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp1.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            sql = "select SlNo,DayDescription,DayAmount from DayEndReport Where DayDate = '" + dtp2.Value.ToString("dd-MMM-yyyy") + "' order by SlNo  ";
            GCon.getDataSet1(sql, "DayEndReport");
            if (GlobalVariable.gdataset.Tables["DayEndReport"].Rows.Count > 0)
            {
                rv.GetDetails(sql, "DayEndReport", RPS);
                RPS.SetDataSource(GlobalVariable.gdataset);
                rv.crystalReportViewer1.ReportSource = RPS;
                rv.crystalReportViewer1.Zoom(100);

                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ1;
                TXTOBJ1 = (TextObject)RPS.ReportDefinition.ReportObjects["Text4"];
                TXTOBJ1.Text = GlobalVariable.gCompanyName;
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ2;
                TXTOBJ2 = (TextObject)RPS.ReportDefinition.ReportObjects["Text6"];
                TXTOBJ2.Text = "Date : " + dtp1.Value.ToString("dd-MMM-yyyy") + " To " + dtp2.Value.ToString("dd-MMM-yyyy");
                CrystalDecisions.CrystalReports.Engine.TextObject TXTOBJ3;
                TXTOBJ3 = (TextObject)RPS.ReportDefinition.ReportObjects["Text7"];
                TXTOBJ3.Text = "Printed On " + Strings.Format((DateTime)DateTime.Now, "dd/MM/yyyy") + " at " + Strings.Format((DateTime)DateTime.Now, "HH:mm") + " by " + GlobalVariable.gUserName;

                fromCrystalExportTo(RPS);
                rv.Show();
                rv.Close();
                rv.Dispose();
                RPS.Dispose();
                if (GlobalVariable.gCompName == "SKYYE")
                {
                    sendmailSkyye();
                }
            }
        }

        private void sendmailSkyye()
        {
            try
            {
                String sql;
                Boolean state;
                String emailid = "report@skyye.in";
                String password = "Mrbiz#rep0rts19feb";
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mailMessage = new MailMessage();

                //mailMessage.To.Add("pankaj@clubman.in");

                mailMessage.To.Add("vasanth@skyye.in");
                mailMessage.To.Add("systemadmin@skyye.in");
                mailMessage.To.Add("accounts@skyye.in");

                mailMessage.To.Add("bhaskar@divyasree.com");
                mailMessage.To.Add("bharath.skyye@gmail.com");
                mailMessage.To.Add("tony.kunnel@atelierglobal.com");
                mailMessage.To.Add("vsiphone5@icloud.com");
                mailMessage.To.Add("flaxensolutions@gmail.com");
               
                mailMessage.From = new MailAddress(emailid);
                mailMessage.Subject = "Day End Summary For the Date " + dtp1.Value.ToString("dd-MMM-yyyy") + " To " + dtp2.Value.ToString("dd-MMM-yyyy");

                string vpath = Application.StartupPath + @"\Reports\DayEndSummary.pdf";
                mailMessage.Body = System.IO.File.ReadAllText(vpath);
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = "";
                if (vpath != "")
                {
                    mailMessage.Attachments.Add(new Attachment(vpath));
                }
               
                SmtpClient smtpClient = new SmtpClient();
                state = false;
                smtpClient.EnableSsl = state;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(emailid, password);
                SmtpServer.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.skyye.in";
                smtpClient.Port = 587;
                //smtpClient.Host = "mail.skyye.in";
                //smtpClient.Port = 25;

                smtpClient.Send(mailMessage);
                MessageBox.Show("Mail Sent Sucessfully");
                // Response.End();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //public void SendEmail()
        //{

        //        MailMessage msg = new MailMessage();

        //        msg.From = new MailAddress("report@skyye.in");
        //        msg.To.Add("pankaj@clubman.in");
        //        //msg.CC.Add(cc);
        //        msg.Subject = "Day End Summary For the Date " + dtp1.Value.ToString("dd-MMM-yyyy") + " To " + dtp2.Value.ToString("dd-MMM-yyyy");
        //        msg.Body = "";
        //        msg.IsBodyHtml = true;

        //        System.Net.Mail.Attachment attachment;
        //        string vpath = Application.StartupPath + @"\Reports\DayEndSummary.pdf";
                
        //        //if (vpath != "")
        //        //{
        //        //    mailMessage.Attachments.Add(new Attachment(vpath));
        //        //}
        //        attachment = new System.Net.Mail.Attachment(vpath);
        //        msg.Attachments.Add(attachment);

        //        SmtpClient client = new SmtpClient("mail.skyye.in", 25);

        //        client.UseDefaultCredentials = false;
        //        client.EnableSsl = true;
        //        client.Credentials = new NetworkCredential("report@skyye.in", "Mrbiz#rep0rts19feb");
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        //client.EnableSsl = true;

        //        try
        //        {
        //            client.Send(msg);
        //        }
        //        catch (Exception)
        //        {
        //            //return false;
        //        }
        //        //return true;
        //   }

        //public void Outlook() 
        //{
        //    try
        //    {
                
        //        Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
        //        // Create a new mail item.
        //        Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //        // Set HTMLBody. 
        //        //add the body of the email
        //        oMsg.HTMLBody = "Day End Summary";
        //        //Add an attachment.
        //        String sDisplayName = "Skyye";
        //        int iPosition = (int)oMsg.Body.Length + 1;
        //        int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
        //        //now attached the file
        //        string vpath = Application.StartupPath + @"\Reports\DayEndSummary.pdf";
        //        Microsoft.Office.Interop.Outlook.Attachment oAttach = oMsg.Attachments.Add
        //                                     (vpath, iAttachType, iPosition, sDisplayName);
        //        //Subject line
        //        oMsg.Subject = "Day End Summary For the Date " + dtp1.Value.ToString("dd-MMM-yyyy") + " To " + dtp2.Value.ToString("dd-MMM-yyyy");
        //        // Add a recipient.
        //        Microsoft.Office.Interop.Outlook.Recipients oRecips = (Microsoft.Office.Interop.Outlook.Recipients)oMsg.Recipients;
        //        // Change the recipient in the next line if necessary.
        //        Microsoft.Office.Interop.Outlook.Recipient oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("report@skyye.in");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("bhaskar@divyasree.com");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("bharath.skyye@gmail.com");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("tony.kunnel@atelierglobal.com");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("vsiphone5@icloud.com");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("flaxensolutions@gmail.com");
        //        //oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("accounts@skyye.in");
        //        oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("vasanth@skyye.in");
        //        oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("systemadmin@skyye.in");
        //        oRecip.Resolve();
        //        // Send.
        //        oMsg.Send();
        //        // Clean up.
        //        oRecip = null;
        //        oRecips = null;
        //        oMsg = null;
        //        oApp = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("{0} Exception caught.", ex);
        //    }
        //}
    }
}
