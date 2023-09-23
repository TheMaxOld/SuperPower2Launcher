using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SuperPower_Launcher
{
    public partial class Form3 : Form
    {
        string FileGame = Properties.Settings.Default.FilePath;
        bool myVariable = false;
        public Form3()
        {
            InitializeComponent();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
            string configFilePath = FileGame + "\\joshua.cfg";
            string configContent = File.ReadAllText(configFilePath);
            Match match = Regex.Match(configContent, "<WIDTH>(.*?)</WIDTH>");
            string modValue = match.Groups[1].Value;
            textBox1.Text = modValue;

            Match match2 = Regex.Match(configContent, "<HEIGHT>(.*?)</HEIGHT>");
            string modValue2 = match2.Groups[1].Value;
            textBox2.Text = modValue2;

            Match match3 = Regex.Match(configContent, "<COLOR_DEPTH>(.*?)</COLOR_DEPTH>");
            string modValue3 = match3.Groups[1].Value;
            textBox3.Text = modValue3;

            Match match4 = Regex.Match(configContent, "<FULLSCREEN>(.*?)</FULLSCREEN>");
            string modValue4 = match4.Groups[1].Value;
            bool boolValue = bool.Parse(modValue4);
            checkBox1.Checked = boolValue;

            Match match5 = Regex.Match(configContent, "<MOD>(.*?)</MOD>");
            string modValue5 = match5.Groups[1].Value;
            textBox5.Text = modValue5;

            string configFilePath2 = FileGame + "\\MODS\\"+ "SP2" + "\\sp2_cfg_client.xml";
            string configContents = File.ReadAllText(configFilePath2);

            Match match6 = Regex.Match(configContents, "<Sun>(.*?)</Sun>");
            string modValue6 = match6.Groups[1].Value;
            bool boolValue2 = bool.Parse(modValue6);
            checkBox2.Checked = boolValue2;

            Match match7 = Regex.Match(configContents, "<Moon>(.*?)</Moon>");
            string modValue7 = match7.Groups[1].Value;
            bool boolValue3 = bool.Parse(modValue7);
            checkBox3.Checked = boolValue3;

            Match match8 = Regex.Match(configContents, "<Clouds>(.*?)</Clouds>");
            string modValue8 = match8.Groups[1].Value;
            bool boolValue4 = bool.Parse(modValue8);
            checkBox4.Checked = boolValue4;

            Match match9 = Regex.Match(configContents, "<Day_Light_Cycle>(.*?)</Day_Light_Cycle>");
            string modValue9 = match9.Groups[1].Value;
            bool boolValue5 = bool.Parse(modValue9);
            checkBox5.Checked = boolValue5;

            Match match10 = Regex.Match(configContents, "<Player_Name>(.*?)</Player_Name>");
            string modValue10 = match10.Groups[1].Value;
            textBox4.Text = modValue10;

            Match match11 = Regex.Match(configContents, "<Autosave_Filename>(.*?)</Autosave_Filename>");
            string modValue11 = match11.Groups[1].Value;
            textBox6.Text = modValue11;

            Match match12 = Regex.Match(configContents, "<Autosave_Frequency>(.*?)</Autosave_Frequency>");
            string modValue12 = match12.Groups[1].Value;
            textBox7.Text = modValue12;

            Match match13 = Regex.Match(configContent, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue13 = match13.Groups[1].Value;
            if (modValue13 == "russian")
            {
                comboBox1.Text = "Русский";

            }
            if (modValue13 == "english")
            {
                comboBox1.Text = "English";
            }

            textBox8.Text = FileGame;

            string configFilePath22 = FileGame + "\\SP2Launcher.cfg";
            string filePathConfigLaunch2 = File.ReadAllText(configFilePath22);
            Match match22 = Regex.Match(filePathConfigLaunch2, "<LANGUAGE>(.*?)</LANGUAGE>");
            string modValue22 = match22.Groups[1].Value;
            if (modValue22 == "russian")
            {
                groupBox1.Text = "Настроки Графики";
                groupBox2.Text = "Настройка игры";
                groupBox3.Text = "Настройки мода";
                label1.Text = "Ширина";
                label2.Text = "Высота";
                label3.Text = "Гамма";
                label4.Text = "Имя игрока";
                label5.Text = "Мод";
                label6.Text = "Язык";
                label7.Text = "Имя автосохранения";
                label8.Text = "Сохранять каждые";
                label9.Text = "минут";
                label10.Text = "Путь к игре";
                checkBox1.Text = "Полный экран";
                checkBox2.Text = "Солнце";
                checkBox3.Text = "Луна";
                checkBox4.Text = "Облока";
                checkBox5.Text = "Смена дня и ночи";
                button1.Text = "Применить";
                button2.Text = "Выбрать";
                Text = "Настроки";
            }
            if (modValue22 == "english")
            {
                groupBox1.Text = "Graphics Settings";
                groupBox2.Text = "Setting up the game";
                groupBox3.Text = "Mod Settings";
                label1.Text = "Width";
                label2.Text = "Height";
                label3.Text = "Gamma";
                label4.Text = "Player Name";
                label5.Text = "Mod";
                label6.Text = "Lang";
                label7.Text = "Autosave name";
                label8.Text = "Save every";
                label9.Text = "minutes";
                label10.Text = "Path to game";
                checkBox1.Text = "Full screen";
                checkBox2.Text = "Sun";
                checkBox3.Text = "Moon";
                checkBox4.Text = "Clouds";
                checkBox5.Text = "Change of day and night";
                button1.Text = "Apply";
                button2.Text = "select";
                Text = "Settings";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string configFilePath = FileGame + "\\joshua.cfg";
            string configContent = File.ReadAllText(configFilePath);
            var selectedValue = textBox1.Text;
            var selectedValue2 = textBox2.Text;
            var selectedValue3 = textBox3.Text;
            var selectedValue4 = checkBox1.Checked;
            var selectedValue5 = checkBox2.Checked;
            var selectedValue6 = checkBox3.Checked;
            var selectedValue7 = checkBox4.Checked;
            var selectedValue8 = checkBox5.Checked;
            var selectedValue9 = textBox4.Text;
            var selectedValue10 = textBox6.Text;
            var selectedValue11 = textBox7.Text;

            configContent = Regex.Replace(configContent, "<WIDTH>.*</WIDTH>",
            $"<WIDTH>{selectedValue}</WIDTH>");
            File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);

            configContent = Regex.Replace(configContent, "<HEIGHT>.*</HEIGHT>",
            $"<HEIGHT>{selectedValue2}</HEIGHT>");
            File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);

            configContent = Regex.Replace(configContent, "<COLOR_DEPTH>.*</COLOR_DEPTH>",
            $"<COLOR_DEPTH>{selectedValue3}</COLOR_DEPTH>");
            File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);

            configContent = Regex.Replace(configContent, "<FULLSCREEN>.*</FULLSCREEN>",
            $"<FULLSCREEN>{selectedValue4.ToString().ToLower()}</FULLSCREEN>");
            File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);

            string configFilePath2 = FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml";
            string configContents = File.ReadAllText(configFilePath2);

            configContents = Regex.Replace(configContents, "<Sun>.*</Sun>",
            $"<Sun>{selectedValue5.ToString().ToLower()}</Sun>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            configContents = Regex.Replace(configContents, "<Moon>.*</Moon>",
            $"<Moon>{selectedValue6.ToString().ToLower()}</Moon>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            configContents = Regex.Replace(configContents, "<Clouds>.*</Clouds>",
            $"<Clouds>{selectedValue7.ToString().ToLower()}</Clouds>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            configContents = Regex.Replace(configContents, "<Day_Light_Cycle>.*</Day_Light_Cycle>",
            $"<Day_Light_Cycle>{selectedValue8.ToString().ToLower()}</Day_Light_Cycle>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            configContents = Regex.Replace(configContents, "<Player_Name>.*</Player_Name>",
            $"<Player_Name>{selectedValue9}</Player_Name>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            if (comboBox1.SelectedIndex == 0)
            {
                configContent = Regex.Replace(configContent, "<LANGUAGE>.*</LANGUAGE>",
                $"<LANGUAGE>russian</LANGUAGE>");
                File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);
            }
            else
            {
                configContent = Regex.Replace(configContent, "<LANGUAGE>.*</LANGUAGE>",
                $"<LANGUAGE>english</LANGUAGE>");
                File.WriteAllText("" + FileGame + "\\joshua.cfg", configContent);
            }

            configContents = Regex.Replace(configContents, "<Autosave_Filename>.*</Autosave_Filename>",
            $"<Autosave_Filename>{selectedValue10}</Autosave_Filename>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            configContents = Regex.Replace(configContents, "<Autosave_Frequency>.*</Autosave_Frequency>",
            $"<Autosave_Frequency>{selectedValue11}</Autosave_Frequency>");
            File.WriteAllText("" + FileGame + "\\MODS\\" + "SP2" + "\\sp2_cfg_client.xml", configContents);

            if (myVariable == true)
            {
                Properties.Settings.Default.FilePath = @"" + FileGame;
                Properties.Settings.Default.Save();
                Application.Restart();
            }

            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    string chosenDirectory = folderDialog.SelectedPath;
                    string exeFilePath = Path.Combine(chosenDirectory, "joshua.exe");

                    if (File.Exists(exeFilePath))
                    {
                        // выполнение действий с найденной папкой и файлом joshua.exe
                        string selectedPath = folderDialog.SelectedPath;
                        FileGame = selectedPath;
                        Properties.Settings.Default.FilePath = @"" + FileGame;
                        Properties.Settings.Default.Save();
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Файл joshua.exe не найден в выбранной папке. Выберите другую папку.");
                        return; // выход из метода, дальнейшие действия не выполняются
                    }
                }
                else
                {
                    // пользователь отменил выбор папки, выходим из метода
                    return;
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
