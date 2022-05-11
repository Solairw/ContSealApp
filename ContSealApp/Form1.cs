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
                string[] containersList = inputText.Split('\n');
                string[] sealsList = inputText.Split('\n');

                //разделяем номер контейнера и вес и отправляем в нужные окна + возможность выбора множителя для веса
                outputContainersBox.Clear();
                for (int b = 0; b < containersList.Length; b++)
                {
                    containersList[b] = containersList[b].Substring(0, 12);
                    outputContainersBox.Text += containersList[b];
                }

                outputWeightBox.Clear();
                for (int b = 0; b < sealsList.Length; b++)
                {
                    string weight = sealsList[b].Substring(11, sealsList[b].Length - 11);
                    int multiplyerValue = int.Parse(weightMultiplyerValueBox.Text);
                    outputWeightBox.Text += Convert.ToDouble(weight) * multiplyerValue;
                }

                //запись результатов в файл
                //using StreamWriter outputText = new("Result.csv", true);
                //outputText.WriteLine(containersList);
                //outputText.WriteLine(sealsList);

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
            }
        }
    }
}