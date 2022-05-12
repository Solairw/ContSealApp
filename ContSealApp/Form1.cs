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
                }

                //foreach (var containerNumber in containersList)
                //{
                //    outputContainersBox.Text += containerNumber + " ";
                //}

                //for (int b = 0; b < containersList.Length; b++)
                //{
                //    //выделяем номер контейнера - в окно Containers
                //    containersList[b] = containersList[b].Substring(0, 11);
                //    outputContainersBox.Text += containersList[b];

                //    //выделяем вес и перемножаем на множитель - в окно Weight 
                //    string weight = weightList[b].Substring(12, weightList[b].Length - 12);
                //    int multiplierValue = int.Parse(weightMultiplierValueBox.Text);
                //    outputWeightBox.Text += Convert.ToDouble(weight) * multiplierValue;

                //    //заносим данные в кортеж  - (string, double, string) containerInfo - в окно Test Box
                //    (string, string) containerInfo = (containersList[b].Substring(0, 11), weightList[b].Substring(12, weightList[b].Length - 12));
                //    testBox1.Text += containerInfo;
                //}

                //*int.Parse(weightMultiplyerValueBox.Text
                //помещаем блок в цикл. выводим результац циклом в виде кортежа
                //string[] containerNumber = containersList;
                //string[] containerSeal = sealsList;
                //double containerWeight = 22.1;
                //(string, string, double) containerInfo = (containerNumber[b], containerSeal[b], containerWeight[b] * int.Parse(weightMultiplyerValueBox.Text));
                //testBox1.Text += containerInfo;


                //for (int b = 0; b < sealsList.Length; b++)
                //{
                //    string weight = sealsList[b].Substring(11, sealsList[b].Length - 11);
                //    int multiplyerValue = int.Parse(weightMultiplyerValueBox.Text);
                //    outputWeightBox.Text += Convert.ToDouble(weight) * multiplyerValue;
                //}

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