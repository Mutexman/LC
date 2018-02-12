using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;

namespace LC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
            SingleInstanceApplication.Run(new FormMain(), StartupNextInstanceHandler);
        }
        static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            // что-то делаем с e.CommandLine...
        }
    }
}