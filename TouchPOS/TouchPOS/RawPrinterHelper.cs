using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace TouchPOS
{
    class RawPrinterHelper
    {
        private static string xDocumentName = "XXX";
        public string _DocumentName
        {
            set
            {
                xDocumentName = value;
            }
        }
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DOCINFOW
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool OpenPrinter(string src, ref IntPtr hPrinter, long pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool StartDocPrinter(IntPtr hPrinter, Int32 level, ref DOCINFOW pDI);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, ref Int32 dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            IntPtr hPrinter = default(IntPtr); // The printer handle.
            Int32 dwError = 0; // Last error - in case there was trouble.
            DOCINFOW di = new DOCINFOW(); // Describes your document (name, port, data type).
            Int32 dwWritten = 0; // The number of bytes written by WritePrinter().
            bool bSuccess = false; // Your success code.

            // Set up the DOCINFO structure.
            di.pDocName = xDocumentName;
            di.pDataType = "RAW";
            // Assume failure unless you specifically succeed.
            bSuccess = false;
            if (OpenPrinter(szPrinterName, ref hPrinter, 0))
            {
                if (StartDocPrinter(hPrinter, 1, ref di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your printer-specific bytes to the printer.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, ref dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        } // SendBytesToPrinter()

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            int fslen = Convert.ToInt32(fs.Length);

            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes large enough to hold the file's contents.
            byte[] bytes = new byte[fs.Length + 1];
            bool bSuccess = false;
            // Your unmanaged pointer
            IntPtr pUnmanagedBytes = default(IntPtr);
            bytes = br.ReadBytes(fslen);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(fslen);
            Marshal.Copy(bytes, 0, pUnmanagedBytes, fslen);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fslen);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            fs.Close();
            fs = null;
            return bSuccess;
        } // SendFileToPrinter()

        public static object SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes = default(IntPtr);
            Int32 dwCount = 0;
            dwCount = szString.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }
    }
}
