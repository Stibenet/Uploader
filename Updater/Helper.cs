using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Updater
{
    public class Helper
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        public static string ApplicationName
        {
            get
            {
                var fileName = Process.GetCurrentProcess().MainModule.FileName;
                return Path.GetFileNameWithoutExtension(fileName);
            }
        }

        public static void SetForegroundWindow()
        {
            Process[] process = Process.GetProcessesByName(ApplicationName);
            if (process.Length == 0)
                throw new FileNotFoundException(ApplicationName);

            SetForegroundWindow(process[0].MainWindowHandle);
        }
    }
}
