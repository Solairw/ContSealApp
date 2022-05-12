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

                string[] inputList = inputText.Split('\n');
                string[] containerList = new string[inputList.Length];
                string[] weightList = new string[inputList.Length];
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                outputContainersBox.Clear();
                outputWeightBox.Clear();
                testBox1.Clear();

                for (int n = 0; n < inputList.Length; n++)
                {
                    string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                    containerList[n] = temp[0];
                    weightList[n] = temp[1];
                    outputContainersBox.Text += containerList[n];
                    outputWeightBox.Text += Convert.ToDouble(weightList[n]) * multiplierValue;

                    //заносим данные в кортеж  - (string, double, string) containerInfo - в окно Test Box
                    (string, double) containerInfo = (containerList[n], Convert.ToDouble(weightList[n]) * multiplierValue);
                    testBox1.Text += containerInfo;

                    //запись результатов в файл
                    using StreamWriter outputText = new("Result.csv", true);
                    outputText.WriteLine(containerInfo);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
            }
        }
    }
}