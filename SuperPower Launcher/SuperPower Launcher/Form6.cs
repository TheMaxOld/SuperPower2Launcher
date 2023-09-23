using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SuperPower_Launcher
{
    public partial class Form6 : Form
    {

        public Form6()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        string FileGame = Properties.Settings.Default.FilePath;
        private void Form6_Load(object sender, EventArgs e)
        {
            string FileGame = Properties.Settings.Default.FilePath;
            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;
            if (modValue22 == "russian")
            {
                label2.Text = "Запуск игры";

                Text = "Запуск игры";
            }
            if (modValue22 == "english")
            {
                label2.Text = "Starting the game";
                Text = "Starting the game";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process iStartProcess = new Process();
            iStartProcess.StartInfo.WorkingDirectory = @"" + FileGame + "";
            iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua.exe";
            iStartProcess.Start();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process iStartProcess = new Process();
            iStartProcess.StartInfo.WorkingDirectory = @"" + FileGame + "";
            iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua64.exe";
            iStartProcess.Start();
            Close();
        }
    }
}
