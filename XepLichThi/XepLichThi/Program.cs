using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace XepLichThi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void KiemTraThuVien()
        {
            Assembly Assemb = Assembly.GetExecutingAssembly();
            FileStream fs;
            BinaryWriter bw;
            if (!File.Exists("DevComponents.DotNetBar2.dll"))
            {
                fs = new FileStream("DevComponents.DotNetBar2.dll", FileMode.CreateNew);
                bw = new BinaryWriter(fs);
                bw.Write(global::XepLichThi.Properties.Resources.DevComponents_DotNetBar2, 0, global::XepLichThi.Properties.Resources.DevComponents_DotNetBar2.Length);
                bw.Flush();
                bw.Close();
            }
            if (!File.Exists("DataAccess.dll"))
            {
                fs = new FileStream("DataAccess.dll", FileMode.CreateNew);
                bw = new BinaryWriter(fs);
                bw.Write(global::XepLichThi.Properties.Resources.DataAccess, 0, global::XepLichThi.Properties.Resources.DataAccess.Length);
                bw.Flush();
                bw.Close();
            }
        }
        static bool KiemTraChay()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(Application.ProductName);
                if (p.Length > 1)
                {
                    SetForegroundWindow(p[0].MainWindowHandle);
                    ShowWindow(p[0].MainWindowHandle, 5);
                    return true;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmLoadData());
            }
            catch (Exception)
            {
            }
            return false;
        }
        static void Main()
        {
            KiemTraThuVien();
            KiemTraChay();

        }
    }
}
