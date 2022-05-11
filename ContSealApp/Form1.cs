using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ContSealApp
{
    public partial class InputForm1 : Form
    {
        public InputForm1()
        {
            InitializeComponent();
            startButton.Click += StartButton_Click;
        }
        private void StartButton_Click(object? sender, EventArgs e)
        {
            try
            {
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",");
                string[] newText = inputText.Split('\n'); // разделяем текст на строки

                //разделяем номер контейнера и вес и отправляем в нужные окна
                outputContainersBox.Clear();
                for (int b = 0; b < newText.Length; b++)
                {
                    newText[b] = newText[b].Substring(0, 12);
                    outputContainersBox.Text += newText[b];
                }

                outputWeightBox.Clear();
                for (int b = 0; b < newText.Length; b++)
                {
                    string weight = newText[b].Substring(13, newText[b].Length - 1);
                    int intWeight = int.Parse(weight) * 1000;
                    outputWeightBox.Text += newText[b];
                }

                
                // запись результатов в файл
                //using StreamWriter outputText = new("Result.xls", true); 
                //outputText.WriteLine(newText[n - 1]);

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
            }
        }
    }
}