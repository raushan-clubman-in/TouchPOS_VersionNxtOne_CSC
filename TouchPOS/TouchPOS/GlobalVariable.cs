using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchPOS
{
    class GlobalVariable
    {
        public static string gUserName = "";
        public static string gUserCategory = "";
        public static string ServiceType = "";
        public static string gCompanyName = "";
        public static string SLocation = "";
        public static int LCode = 0;
        public static string TableNo = "";
        public static int ChairNo = 1;
        public static DateTime ServerDate = System.DateTime.Now.Date;
        public static string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        public static string ComputerName = "";
        public static string PrinterName = "";
        public static string KotOptionLocal = "";
        public static string EntryType = "";
        public static string CardType = "";
        public static string gCompName = "";
        public static string CreditCheck = "";
        public static string DefaulterCheck = "";
        public static DateTime FinStart = System.DateTime.Now.Date;
        public static DateTime FinEnd = System.DateTime.Now.Date;
        public static string DupItemAllowed = "NO";
        public static string AccessCheckValidate = "N";
        public static string MultiPayMode = "NO";
        public static string AccountPostFlag = "NO";
        public static string KotSMSYN = "N";
        public static string BillSMSYN = "N";
                
        public static DataSet gdataset = new DataSet();
        public static SqlDataAdapter gadapter = null;
        public static String gprintername;
        public static ArrayList gkotprinter1 = new ArrayList();
        public static ArrayList gkotprinter2 = new ArrayList();

        public static int LocTabIndex = 0;
        public static string AR_ACCode = "";
        public static string MainCardDeductFlag = "N";

        public static Boolean chkdatevalidate;
        public static string gadd1;
        public static string gadd2;

        public static DateTime CapDate;
        public static string CapYN;

        public static int DisLocCode = 0;

    }
}
