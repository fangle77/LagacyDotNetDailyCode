using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class ProcessManager
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static void GetProcess()
        {
            int chars = 256;
            StringBuilder buff = new StringBuilder(chars);
            while (true)
            {
                // Obtain the handle of the active window.
                IntPtr handle = GetForegroundWindow();

                // Update the controls.
                if (GetWindowText(handle, buff, chars) > 0)
                {
                    Console.WriteLine(buff.ToString());
                    Console.WriteLine(handle.ToString());
                }
                Thread.Sleep(1000);
            }
        }
    }
}
