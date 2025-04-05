using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

public class RawPrinterHelper
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class DOCINFOA
    {
        [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
        [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
        [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
    }

    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true)]
    public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter")]
    public static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA")]
    public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] DOCINFOA di);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter")]
    public static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter")]
    public static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter")]
    public static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter")]
    public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

    public static bool SendStringToPrinter(string printerName, string command)
    {
        IntPtr pBytes;
        int dwCount = command.Length;
        pBytes = Marshal.StringToCoTaskMemAnsi(command);
        bool success = false;

        IntPtr hPrinter;
        DOCINFOA di = new DOCINFOA
        {
            pDocName = "Label",
            pDataType = "RAW"
        };

        if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
        {
            if (StartDocPrinter(hPrinter, 1, di))
            {
                if (StartPagePrinter(hPrinter))
                {
                    success = WritePrinter(hPrinter, pBytes, dwCount, out _);
                    EndPagePrinter(hPrinter);
                }
                EndDocPrinter(hPrinter);
            }
            ClosePrinter(hPrinter);
        }

        Marshal.FreeCoTaskMem(pBytes);
        return success;
    }
}