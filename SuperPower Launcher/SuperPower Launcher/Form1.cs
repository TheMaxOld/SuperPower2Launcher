using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace SuperPower_Launcher
{
    
    public partial class Form1 : Form
    {
        


        public Form1()
        {
            InitializeComponent();
           

        }
       
       
        // устанавливаем эту форму как стартовую


        string FileGame = Properties.Settings.Default.FilePath;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();
            }
            else
            {
                string configFilePath = FileGame + "\\joshua64.exe";
                if (System.IO.File.Exists(configFilePath))
                {
                    Form6 form6 = new Form6();
                    form6.Show();
                }
                else
                {
                    Process iStartProcess = new Process();
                    iStartProcess.StartInfo.WorkingDirectory = @"" + FileGame + "";
                    iStartProcess.StartInfo.FileName = @"" + FileGame + "\\joshua.exe";
                    iStartProcess.Start();
                }
               
            }

        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = listBox1.SelectedItem;

            if (selectedValue == null)
            {
                return;
            }
            string configFilePath = FileGame + "\\joshua.cfg";

            if (!File.Exists(configFilePath))
            {
                return;
            }

            string configContent = File.ReadAllText(configFilePath);

            configContent = Regex.Replace(configContent, "<MOD>.*</MOD>",
            $"<MOD>{selectedValue}</MOD>");

            File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);

            string selectedText = (string)listBox1.SelectedItem;

            // Проверяем, выбран ли элемент с именем SP2 в ListBox1
            if (selectedText == "SP2" || selectedText == "ColdWarMultiMOD" || selectedText == "1900MultiMOD" || selectedText == "RealismMultiMOD")
            {
                // Отключаем ListBox2
                listBox2.Enabled = false;
                listBox2.Items.Clear();
            }
            else
            {
                // Включаем ListBox2 (если он был отключен)
                listBox2.Enabled = true;
                // Выбираем первый элемент в ListBox2
                listBox2.Items.Clear();
                string[] dirssdk = Directory.GetDirectories(@"" + FileGame + "\\sdk");
                foreach (string dir in dirssdk)
                {
                    listBox2.Items.Add(Path.GetFileName(dir));
                }

            }
            

            string configFilePath4 = FileGame + "\\joshua.cfg";
            string configContent4 = File.ReadAllText(configFilePath4);
            Match match4 = Regex.Match(configContent4, "<MOD>(.*?)</MOD>");
            string modValue4 = match4.Groups[1].Value;
            string filePath = @"" + FileGame + "\\MODS\\" + modValue4 + "\\sdkconfig.cfg";

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                // Если файла нет, создаем его с помощью метода Create и закрываем поток
                using (FileStream fs = File.Create(filePath))
                {
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("<SDK></SDK>");
                }
            }
            string configFilePath2 = FileGame + "\\MODS\\" + modValue4 + "\\sdkconfig.cfg";
            string configContent2 = File.ReadAllText(configFilePath2);
            Match match2 = Regex.Match(configContent2, "<SDK>(.*?)</SDK>");
            string modValue2 = match2.Groups[1].Value;
            // label3.Text = modValue;
            listBox2.SelectedItem = modValue2;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(""+FileGame+"\\MODS");
            if (string.IsNullOrEmpty(FileGame))
            {
                listBox1.Items.Add("Укажит путь к SP2");
                listBox2.Items.Add("Укажит путь к SP2");
            }
            else
            {
                string[] dirs = Directory.GetDirectories(@"" + FileGame + "\\MODS");
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(Path.GetFileName(dir));
                }
                string[] dirssdk = Directory.GetDirectories(@"" + FileGame + "\\sdk");
                foreach (string dir in dirssdk)
                {
                    listBox2.Items.Add(Path.GetFileName(dir));
                }
                
                string configFilePath = FileGame + "\\joshua.cfg";
                string configContent = File.ReadAllText(configFilePath);
                Match match = Regex.Match(configContent, "<MOD>(.*?)</MOD>");
                string modValue = match.Groups[1].Value;
              //  label3.Text = modValue;
                listBox1.SelectedItem = modValue;

                string configFilePath2 = FileGame + "\\MODS\\"+ modValue + "\\sdkconfig.cfg";
                string configContent2 = File.ReadAllText(configFilePath2);
                Match match2 = Regex.Match(configContent2, "<SDK>(.*?)</SDK>");
                string modValue2 = match2.Groups[1].Value;
                // label3.Text = modValue;
                listBox2.SelectedItem = modValue2;
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessId();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();
 
            }
            else
            {

                string pathflode = @"" + FileGame + "\\sdk"; // указываем путь к создаваемой папке
                if (!Directory.Exists(pathflode)) // проверяем, существует ли папка
                {
                    Directory.CreateDirectory(pathflode); // создаем папку
                }
                string configFilePath5 = FileGame + "\\joshua.cfg";
                string configContent5 = File.ReadAllText(configFilePath5);
                Match match5 = Regex.Match(configContent5, "<MOD>(.*?)</MOD>");
                string modValue5 = match5.Groups[1].Value;

                string filePath = @"" + FileGame + "\\MODS\\" + modValue5 + "\\sdkconfig.cfg";
                string filePathConfigLaunch = @"" + FileGame + "\\SP2Launcher.cfg";

                // Проверяем, существует ли файл
                if (!File.Exists(filePath))
                {
                    // Если файла нет, создаем его с помощью метода Create и закрываем поток
                    using (FileStream fs = File.Create(filePath))
                    {
                    }
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write("<SDK></SDK>");
                    }
                }

                if (!File.Exists(filePathConfigLaunch))
                {
                    // Если файла нет, создаем его с помощью метода Create и закрываем поток
                    using (FileStream fs = File.Create(filePathConfigLaunch))
                    {
                    }
                    using (StreamWriter writer = new StreamWriter(filePathConfigLaunch))
                    {
                        writer.Write("<LANGUAGE>russian</LANGUAGE>");
                    }
                }

                string[] dirs = Directory.GetDirectories(@"" + FileGame + "\\MODS");
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(Path.GetFileName(dir));
                }
                string[] dirssdk = Directory.GetDirectories(@"" + FileGame + "\\sdk");
                foreach (string dir in dirssdk)
                {
                    listBox2.Items.Add(Path.GetFileName(dir));
                }
                string configFilePath = FileGame + "\\joshua.cfg";
                string configContent = File.ReadAllText(configFilePath);
                Match match = Regex.Match(configContent, "<MOD>(.*?)</MOD>");
                string modValue = match.Groups[1].Value;
               // label3.Text = modValue;
                listBox1.SelectedItem = modValue;

                //Загрузка выбранного языка
                string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
                string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
                Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
                string modValue22 = match22.Groups[1].Value;
                if (modValue22 == "russian")
                {
                    comboBox1.SelectedIndex = 0;
                }
                if (modValue22 == "english")
                {
                    comboBox1.SelectedIndex = 1;
                }


                string configFilePath2 = FileGame + "\\MODS\\"+ modValue + "\\sdkconfig.cfg";
                string configContent2 = File.ReadAllText(configFilePath2);
                Match match2 = Regex.Match(configContent2, "<SDK>(.*?)</SDK>");
                string modValue2 = match2.Groups[1].Value;
                // label3.Text = modValue;
                listBox2.SelectedItem = modValue2;
                Form2 form2 = new Form2();
                form2.Close();

            }



          

        }
        


        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();

            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();

            }
            else
            {
                Form4 form4 = new Form4();
                form4.Show();

            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();

            }
            else
            {
                Form5 form5 = new Form5();
                form5.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileGame))
            {
                Form2 form2 = new Form2();
                form2.Show();

            }
            else
            {
                Form7 form7 = new Form7();
                form7.Show();

            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string configFilePath = FileGame + "\\joshua.cfg";
            string configContent = File.ReadAllText(configFilePath);
            Match match = Regex.Match(configContent, "<MOD>(.*?)</MOD>");
            string modValue = match.Groups[1].Value;

            string selectedFolderName = (string)listBox2.SelectedItem; // получаем имя выбранной папки
            string sourceFolder = Path.Combine(@"" + FileGame + "\\sdk\\", selectedFolderName); // формируем путь к исходной папке
            string destFolder = Path.Combine(@"" + FileGame + "\\MODS\\", modValue); // формируем путь к целевой папке

            DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder); // создаем объект DirectoryInfo для исходной папки
            DirectoryInfo targetDir = new DirectoryInfo(destFolder); // создаем объект DirectoryInfo для целевой папки
            Directory.CreateDirectory(destFolder); // создаем новую папку, если она не существует
            CopyAll(sourceDir, targetDir); // копируем папку с заменой существующих файлов

            void CopyAll(DirectoryInfo source, DirectoryInfo target)
            {
                if ((string)listBox2.SelectedItem == "SP2Vanila")
                {
                    string serverFilePath = @"" + FileGame + "\\MODS\\" + modValue + "\\Server.dll";
                    string clientFilePath = @"" + FileGame + "\\MODS\\" + modValue + "\\Client.dll";

                    if (File.Exists(serverFilePath))
                    {
                        File.Delete(serverFilePath);
                    }

                    if (File.Exists(clientFilePath))
                    {
                        File.Delete(clientFilePath);
                    }
                }
                else
                {
                    string serverFilePath = @"" + FileGame + "\\MODS\\" + modValue + "\\Server.dll";
                    string clientFilePath = @"" + FileGame + "\\MODS\\" + modValue + "\\Client.dll";

                    if (File.Exists(serverFilePath))
                    {
                        File.Delete(serverFilePath);
                    }

                    if (File.Exists(clientFilePath))
                    {
                        File.Delete(clientFilePath);
                    }
                    // копируем все файлы в папке source
                    foreach (FileInfo file in source.GetFiles())
                    {
                        file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    }

                    // рекурсивно копируем все подпапки
                    foreach (DirectoryInfo sourceSubDir in source.GetDirectories())
                    {
                        DirectoryInfo targetSubDir = target.CreateSubdirectory(sourceSubDir.Name);
                        CopyAll(sourceSubDir, targetSubDir);
                    }
                }
               
            }

            var selectedValue = listBox2.SelectedItem;

            if (selectedValue == null)
            {
                return;
            }
            string configFilePath4 = FileGame + "\\MODS\\"+ modValue + "\\sdkconfig.cfg";

            if (!File.Exists(configFilePath))
            {
                return;
            }

            string configContent4 = File.ReadAllText(configFilePath4);

            configContent4 = Regex.Replace(configContent4, "<SDK>.*</SDK>",
            $"<SDK>{selectedValue}</SDK>");

            File.WriteAllText("" + FileGame + "\\MODS\\" + modValue + "\\sdkconfig.cfg", configContent4);

            string selectedText = (string)listBox2.SelectedItem;


        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {
               
                if (string.IsNullOrEmpty(FileGame))
                {
                    label2.Text = "Список модификаций";
                    label3.Text = "Список SDK";
                    label4.Text = "Разработчик: Бганцев Н.А.";
                    button1.Text = "Играть";
                    button3.Text = "Настройки";
                    button2.Text = "Обновить список";
                    button4.Text = "Загрузить моды";
                    button5.Text = "Обновления";
                    button6.Text = "Мультиплеер";

                }
                else
                {
                    label2.Text = "Список модификаций";
                    label3.Text = "Список SDK";
                    label4.Text = "Разработчик: Бганцев Н.А.";
                    button1.Text = "Играть";
                    button3.Text = "Настройки";
                    button2.Text = "Обновить список";
                    button4.Text = "Загрузить моды";
                    button5.Text = "Обновления";
                    button6.Text = "Мультиплеер";
                    string configFilePath = FileGame + "\\SP2Launcher.cfg";
                    string configContent = File.ReadAllText(configFilePath);
                    configContent = Regex.Replace(configContent, "<LANGUAGE>.*</LANGUAGE>",
                    $"<LANGUAGE>russian</LANGUAGE>");
                    File.WriteAllText("" + FileGame + "\\SP2Launcher.cfg", configContent);
                }
               
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(FileGame))
                {
                    label2.Text = "Modification list";
                    label3.Text = "SDK list";
                    label4.Text = "Developer: Bgantsev N.A.";
                    button1.Text = "Play";
                    button3.Text = "Settings";
                    button2.Text = "Update list";
                    button4.Text = "Download mods";
                    button5.Text = "Updates";
                    button6.Text = "Multiplayer";

                }
                else
                {
                    label2.Text = "Modification list";
                    label3.Text = "SDK list";
                    label4.Text = "Developer: Bgantsev N.A.";
                    button1.Text = "Play";
                    button3.Text = "Settings";
                    button2.Text = "Update list";
                    button4.Text = "Download mods";
                    button5.Text = "Updates";
                    button6.Text = "Multiplayer";
                    string configFilePath = FileGame + "\\SP2Launcher.cfg";
                    string configContent = File.ReadAllText(configFilePath);
                    configContent = Regex.Replace(configContent, "<LANGUAGE>.*</LANGUAGE>",
                    $"<LANGUAGE>english</LANGUAGE>");
                    File.WriteAllText("" + FileGame + "\\SP2Launcher.cfg", configContent);
                }
               
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
    
}
