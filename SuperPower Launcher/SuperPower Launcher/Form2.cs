using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;



namespace SuperPower_Launcher
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            FileGame = FileGame;
            this.TopMost = true;

        }
        public string FileGame { get; set; }

        private void button1_Click(object sender, EventArgs e)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label1.Text = "Укажите путь к папке с игрой SuperPower 2";
                button1.Text = "Выбрать";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label1.Text = "Select the path to SuperPower 2";
                label1.TextAlign = ContentAlignment.TopCenter;
                button1.Text = "Select";
            }
        }
    }
}
