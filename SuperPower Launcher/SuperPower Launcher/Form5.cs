using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace SuperPower_Launcher
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        const string VersionLauncher = "4.0";
        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = VersionLauncher;

            string FileGame = Properties.Settings.Default.FilePath;
            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;
            if (modValue22 == "russian")
            {
                label2.Text = "Версия вашего лаунчера";
                button1.Text = "Проверить обновления";
                Text = "Обновления";
            }
            if (modValue22 == "english")
            {
                label2.Text = "Version of your launcher";
                button1.Text = "Check for updates";
                Text = "Updates";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FileGame = Properties.Settings.Default.FilePath;
            string url = "https://drive.google.com/uc?export=download&id=1m_uE9jgzUbNnrPG8QMcGZislhctzZ5IR";
            string path = @"" + FileGame + "\\MODS\\update.cfg"; // путь к файлу, включая его имя
            string extractPath = Path.GetDirectoryName(path);
            
            int fileCount = 100;

            // обнуляем прогресс бар
            progressBar1.Value = 0;
            progressBar1.Maximum = fileCount;

            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadProgressChanged += (s, ew) =>
                    {
                        // Вычисляем процент загрузки и устанавливаем его в прогресс бар
                        double bytesIn = double.Parse(ew.BytesReceived.ToString());
                        double totalBytes = double.Parse(ew.TotalBytesToReceive.ToString());
                        double percentage = bytesIn / totalBytes * 100;
                        progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                    };

                    webClient.DownloadFileCompleted += (s, ew) =>
                    {
                        // Сообщаем о завершении загрузки
                        Console.WriteLine("Файл успешно загружен!");
                        string configFilePath2 = FileGame + "\\MODS\\update.cfg";
                        string configContents = File.ReadAllText(configFilePath2);

                        Match match6 = Regex.Match(configContents, "<VERSION>(.*?)</VERSION>");
                        string modValue6 = match6.Groups[1].Value;
                        if (modValue6 == VersionLauncher)
                        {

                            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
                            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
                            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
                            string modValue22 = match22.Groups[1].Value;

                            if (modValue22 == "russian")
                            {
                                MessageBox.Show("У вас актуальная версия!", "Информация", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            if (modValue22 == "english")
                            {
                                MessageBox.Show("You have the current version!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }

                            
                        }
                        else
                        {
                            UpdateLauncher();
                            
                        }
                    };

                    webClient.DownloadFileAsync(new Uri(url), path);

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки файла: {ex.Message}");
                }
            }
                void UpdateLauncher() {

                string configFilePath2 = FileGame + "\\MODS\\update.cfg";
                string configContents = File.ReadAllText(configFilePath2);

                Match match6 = Regex.Match(configContents, "<URL>(.*?)</URL>");
                string urls = match6.Groups[1].Value;

                string paths = @"" + FileGame + "\\SuperPower2Launcher.zip"; // путь к файлу, включая его имя
                string extractPaths = Path.GetDirectoryName(paths);

               

                using (var webClient = new WebClient())
                {
                    try
                    {
                        webClient.DownloadProgressChanged += (s, ew) =>
                        {
                            // Вычисляем процент загрузки и устанавливаем его в прогресс бар
                     
                        };

                        webClient.DownloadFileCompleted += (s, ew) =>
                        {
                            // Сообщаем о завершении загрузки
                            Console.WriteLine("Файл успешно загружен!");
                            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
                            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
                            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
                            string modValue22 = match22.Groups[1].Value;

                            if (modValue22 == "russian")
                            {
                                MessageBox.Show("Ваша версия лаунчера обновлена! Лаунчер находится в папке с игрой SP2", "Информация", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            if (modValue22 == "english")
                            {
                                MessageBox.Show("Your launcher version has been updated! The launcher is located in the folder with the game SP2", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                           

                        };

                        webClient.DownloadFileAsync(new Uri(urls), paths);

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка загрузки файла: {ex.Message}");
                    }
                }
            }
           
        }
    }
}
