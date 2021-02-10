using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extract_Rar_Files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string SourceFile = @"CBR-2.cbr";
            string DestinationPath = @"\Test\";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = false;
            process.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", SourceFile, DestinationPath);
            process.Start();
            MessageBox.Show("Done");
        }
    }
}
