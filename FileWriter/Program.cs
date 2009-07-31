using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace FileWriter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            using(StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "report.log"), true, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.Ticks.ToString());
                sw.Close();
            }
        }
    }
}