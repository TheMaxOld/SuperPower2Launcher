using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SuperPower_Launcher
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        string FileGame = Properties.Settings.Default.FilePath;
        private void button1_Click(object sender, EventArgs e)
        {
            string ip_connect = listBox1.GetItemText(listBox1.SelectedItem);
            Process iStartProcess = new Process();
            iStartProcess.StartInfo.WorkingDirectory = @"" + FileGame;
           
            string path24 = @"" + FileGame + "\\joshua64.exe";
            if (File.Exists(path24))
            {
                iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua64.exe";
                iStartProcess.StartInfo.Arguments = "-connect " + ip_connect;
                iStartProcess.Start();
            }
            else
            {
                iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua.exe";
                iStartProcess.StartInfo.Arguments = "-connect " + ip_connect;
                iStartProcess.Start();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("joshua");

            if (processes.Length > 0)
            {
                string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
                string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
                Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
                string modValue22 = match22.Groups[1].Value;

                if (modValue22 == "russian")
                {
                    MessageBox.Show("Сервер или Клиент уже запущен!", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                if (modValue22 == "english")
                {
                    MessageBox.Show("The server or Client is already running!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }


                return;
            }
            else
            {
                Process iStartProcess = new Process();
                iStartProcess.StartInfo.WorkingDirectory = @"" + FileGame;
                iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua.exe";
                iStartProcess.StartInfo.Arguments = "-server";
                iStartProcess.Start();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

            List<string> ipList = new List<string>();
            button4.Click += button4_Click;
            // Загрузка списка IP-адресов из JSON-файла
            string jsonFilePath = FileGame + "\\ip_list.json";
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                ipList = JsonConvert.DeserializeObject<List<string>>(json);
            }

            // Добавление элементов ListBox
            foreach (string ipItem in ipList)
            {
                listBox1.Items.Add(ipItem);
            }

            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;
            if (modValue22 == "russian")
            {
               
                button1.Text = "Подключиться";
                button2.Text = "Запустить сервер";
                button3.Text = "Добавить сервер";
                button4.Text = "Удалить выбранный сервер";
                Text = "Мультиплеер";
            }
            if (modValue22 == "english")
            {

                button1.Text = "Connect";
                button2.Text = "Start server";
                button3.Text = "Add server";
                button4.Text = "Delete selected server";
                Text = "Multiplayer";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newIp = textBox1.Text;

            if (listBox1.Items.Cast<string>().Contains(newIp))
            {
                string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
                string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
                Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
                string modValue22 = match22.Groups[1].Value;
                if (modValue22 == "russian")
                {
                    MessageBox.Show("Данный IP-адрес уже существует в списке!", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                if (modValue22 == "english")
                {
                    MessageBox.Show("This IP address already exists in the list!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
              
            }
            else
            {
                // Добавление нового IP-адреса в список
                listBox1.Items.Add(newIp);
                // Сохранение обновленного списка в JSON-файл
                string jsonFilePath = FileGame + "\\ip_list.json";
                List<string> ipList = listBox1.Items.Cast<string>().ToList();
                string json = JsonConvert.SerializeObject(ipList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex != -1)
            {
                listBox1.Items.RemoveAt(selectedIndex);

                // Сохранение обновленного списка в JSON-файл
                string jsonFilePath = FileGame + "\\ip_list.json";
                List<string> ipList = listBox1.Items.Cast<string>().ToList();
                string json = JsonConvert.SerializeObject(ipList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
        }
    }
}
