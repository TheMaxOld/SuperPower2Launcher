using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace SuperPower_Launcher
{
    public partial class Form4 : Form
    {
        public event EventHandler FunctionOneCompleted;

        public Form4()
        {
            InitializeComponent();
        }
        string FileGame = Properties.Settings.Default.FilePath;
        


        public void Zipdownloads(string path, string url, string extractPath)
        {
            progressBar1.Show();
            // Возможно не до обнулился и взял макс значение
            int fileCount = 100;
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
                        Zipinstall(path,extractPath);
                    };

                    webClient.DownloadFileAsync(new Uri(url), path);

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки файла: {ex.Message}");
                }
            }
            FunctionOneCompleted?.Invoke(this, EventArgs.Empty);
        }

        public void Zipinstall(string path , string extractPath)
        {
            long archiveSize = new FileInfo(path).Length;
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                // получаем количество файлов в архиве
                int fileCount = archive.Entries.Count;

                // обнуляем прогресс бар
                progressBar1.Value = 0;
                progressBar1.Maximum = fileCount;

                // распаковываем архив
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string entryPath = Path.Combine(extractPath, entry.FullName);

                    if (entry.FullName.EndsWith("/") || entry.FullName.EndsWith("\\"))
                    {
                        // если в архиве встретился путь каталога, то создаем его
                        Directory.CreateDirectory(entryPath);
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(entryPath));
                        // распаковываем файлы архива
                        entry.ExtractToFile(entryPath, true);
                    }

                    // проверяем размер распакованных файлов и обновляем прогресс бар
                    long extractedSize = new DirectoryInfo(extractPath).GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
                    int percent = (int)((extractedSize * 100) / archiveSize);
                    progressBar1.Value = Math.Min(percent, progressBar1.Maximum);
                }
            }

            // удаляем архив
            File.Delete(path);
            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;

            if (modValue22 == "russian")
            {
                MessageBox.Show("Установлено!", "Готово", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            if (modValue22 == "english")
            {
                MessageBox.Show("Installed!", "Done", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

           
            label1.Hide();

            progressBar1.Hide();

        }
        
        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Hide();
            progressBar1.Hide();

            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;

            if (modValue22 == "russian")
            {
                groupBox1.Text = "Модификации";
                groupBox3.Text = "Сборки";
                button5.Text = "Загрузить";
                label1.Text = "Загрузка...";
                Text = "Загрузка модификаций";
            }
            if (modValue22 == "english")
            {
                groupBox1.Text = "Modifications";
                groupBox3.Text = "Assemblies";
                button5.Text = "Download";
                label1.Text = "Loading...";
                Text = "Loading modifications";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0) // проверяем, выбран ли первый элемент
            {
                string url = "https://drive.google.com/uc?export=download&id=1PIsfd0Ws4GzLlIhnKh_sKmwjifAqJrQI";
                string path = @"" + FileGame + "\\MODS\\Realism2016.zip"; // путь к файлу, включая его имя
                string path2 = @"" + FileGame + "\\MODS\\Realism2016"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path2))
                {
                    // Файл существует, кнопку нельзя нажать
                   
                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать
                  
                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox1.SelectedIndex == 1) // проверяем, выбран ли второй элемент
            {
                string url = "https://drive.google.com/uc?export=download&id=1z5hj7M-WL-puP2-fMLuP0QZqUQwpiNay";
                string path = @"" + FileGame + "\\MODS\\World-War-2.zip"; // путь к файлу, включая его имя
                                                                          // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\World-War-2"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать
                   
                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать
                   
                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox1.SelectedIndex == 2) // проверяем, выбран ли третий элемент
            {
                string url = "https://drive.google.com/uc?export=download&id=1Zz-JAhmq3i26pxZ0QGHRqfk-dFmlJRlK";
                string path = @"" + FileGame + "\\MODS\\ColdWar.zip"; // путь к файлу, включая его имя
                                                                      // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\ColdWar"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать
                  
                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать
                   
                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox1.SelectedIndex == 3) // проверяем, выбран ли третий элемент
            {
                string url = "https://drive.google.com/uc?export=download&id=1JLETJhjnKlzOm3F-40pecFWpSleaF3Mr";
                string path = @"" + FileGame + "\\MODS\\Geopolitics.zip"; // путь к файлу, включая его имя
                                                                          // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\Geopolitics"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать
                   
                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать
                    
                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox1.SelectedIndex == 4) // проверяем, выбран ли третий элемент
            {
                string url = "https://drive.google.com/uc?export=download&id=1eTs1xt-KKElF7dVkeNjPSHDDjkpSAf72";
                string path = @"" + FileGame + "\\MODS\\1900.zip"; // путь к файлу, включая его имя
                                                                          // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\1900"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox1.SelectedIndex == 5) // проверяем, выбран ли третий элемент
            {
                string url = "https://www.googleapis.com/drive/v3/files/1y4QBHtsL3xSJn0hZENmrT0EkXEJWONEw?alt=media&key=AIzaSyBQhLVxVSK7hmO-rwFAw74TMy6pT5NZ5nQ";
                string path = @"" + FileGame + "\\MODS\\SP2.zip"; // путь к файлу, включая его имя
                                                                   // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\SP2"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

               
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                
            }
            else if (listBox1.SelectedIndex == 6) // проверяем, выбран ли третий элемент
            {
                string url = "https://www.googleapis.com/drive/v3/files/19fAgFVvQj5fEIgn-3wXFyyq0-jKjLkF_?alt=media&key=AIzaSyBQhLVxVSK7hmO-rwFAw74TMy6pT5NZ5nQ";
                string path = @"" + FileGame + "\\SP2.zip"; // путь к файлу, включая его имя
                                                                  // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame; // путь к файлу, включая его имя
                string path24 = @"" + FileGame + "\\joshua64.exe";
                string extractPath = Path.GetDirectoryName(path);

                if (File.Exists(path24))
                {
                   

                    MessageBox.Show("Мод уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }

                // Файл не существует, кнопку можно нажать

              /*  Zipdownloads(path, url, extractPath);
                label1.Show();
                progressBar1.Show();*/

            }




            // Для SDK
            if (listBox2.SelectedIndex == 0) // проверяем, выбран ли первый элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1einMw1JdSv73EKnA6YkYu3V74_HsUt-D";
                string path = @"" + FileGame + "\\sdk\\god43.zip"; // путь к файлу, включая его имя
                                                                  // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\god43"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                   

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 1) // проверяем, выбран ли второй элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1TrYNcEXJh8BOR3NSR9J4mdY7GX38_VUb";
                string path = @"" + FileGame + "\\sdk\\hdm8.zip"; // путь к файлу, включая его имя
                                                                  // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\hdm8"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 2) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=16OQ36jDqqf-ooUnvgo7daXgy3ySo4cAx";
                string path = @"" + FileGame + "\\sdk\\hdm9.zip"; // путь к файлу, включая его имя
                                                                  // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\hdm9"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 3) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1BMSax2Tk3KEd1jM5gfPJFNWlCYUY5Xcl";
                string path = @"" + FileGame + "\\sdk\\hdm11.zip"; // путь к файлу, включая его имя
                                                                  // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\hdm11"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 4) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1KGHZgS8wWuy6ayfdBJjhPGsijYfeSLH1";
                string path = @"" + FileGame + "\\sdk\\uberfoxmod10.zip"; // путь к файлу, включая его имя
                                                                   // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\uberfoxmod10"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 5) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1pjCO2tddUImOCcQk5KJsPySoKyW8_sRR";
                string path = @"" + FileGame + "\\sdk\\Flashpoint.zip"; // путь к файлу, включая его имя
                                                                          // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\Flashpoint"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox2.SelectedIndex == 6) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1M45AAI3zm81xYXQlNMdVSVFAxylLrBTc";
                string path = @"" + FileGame + "\\sdk\\SP2Vanila.zip"; // путь к файлу, включая его имя
                                                                         // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\sdk\\SP2Vanila"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {
                    // Файл существует, кнопку нельзя нажать

                    MessageBox.Show("SDK уже установлен!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    // Файл не существует, кнопку можно нажать

                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }

            //Для сборок

            if (listBox3.SelectedIndex == 0) // проверяем, выбран ли первый элемент
            {
                string pathflode = @"" + FileGame + "\\MODS"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1oJ6h8HJv03Ec99p7_Ux5y_9xLhNAONyZ";
                string path = @"" + FileGame + "\\MODS\\ColdWarMultiMOD.zip"; // путь к файлу, включая его имя
                                                                   // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\ColdWarMultiMOD"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {


                    MessageBox.Show("Сборка уже установлена!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {


                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox3.SelectedIndex == 1) // проверяем, выбран ли второй элемент
            {
                string pathflode = @"" + FileGame + "\\MODS"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1CY9KGeLFubiZakq0ue7hqG0Mjrxg3pf7";
                string path = @"" + FileGame + "\\MODS\\1900MultiMOD.zip"; // путь к файлу, включая его имя
                                                                                // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\1900MultiMOD"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {


                    MessageBox.Show("Сборка уже установлена!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {


                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
            else if (listBox3.SelectedIndex == 2) // проверяем, выбран ли третий элемент
            {
                string pathflode = @"" + FileGame + "\\MODS"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string url = "https://drive.google.com/uc?export=download&id=1f-BaKpfwGYEpbKuc06DaOT0Jr5_DxfXh";
                string path = @"" + FileGame + "\\MODS\\RealismMultiMOD.zip"; // путь к файлу, включая его имя
                                                                            // string path = Path.Combine(FileGame, "MODS", "Cold-War.zip");
                string path23 = @"" + FileGame + "\\MODS\\RealismMultiMOD"; // путь к файлу, включая его имя
                string extractPath = Path.GetDirectoryName(path);

                if (Directory.Exists(path23))
                {


                    MessageBox.Show("Сборка уже установлена!", "Установка", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {


                    Zipdownloads(path, url, extractPath);
                    label1.Show();
                    progressBar1.Show();
                }
            }
        }
        private int prevIndex = -1;
        private bool isResetting = false;
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isResetting)
            {
                isResetting = true;
                listBox1.ClearSelected();
                listBox2.ClearSelected();
                prevIndex = listBox3.SelectedIndex;
                isResetting = false;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isResetting)
            {
                isResetting = true;
                listBox3.ClearSelected();
                listBox1.ClearSelected();
                prevIndex = listBox2.SelectedIndex;
                isResetting = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isResetting)
            {
                isResetting = true;
                listBox2.ClearSelected();
                listBox3.ClearSelected();
                prevIndex = listBox1.SelectedIndex;
                isResetting = false;
            }
        }
    }

}
