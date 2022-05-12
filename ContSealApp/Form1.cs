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
            outputContainersBox.Clear();
            outputWeightBox.Clear();
            outputSealBox.Clear();
                testBox1.Clear();

            try
            {
                //����������� ������ �� ���� input 1 
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",");
                string[] inputList = inputText.Split('\n');
                string[] containerList = new string[inputList.Length];
                string[] weightList = new string[inputList.Length];

                //������ �� ���� input 2 - ���������� ������ �� ����� 
                string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",");
                string[] inputList2 = inputText2.Split('\n');
                string[] containerList2 = new string[inputList2.Length];
                string[] sealList = new string[inputList2.Length];
                
                //��������� ��� ����
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                for (int n = 0; n < inputList.Length; n++)
                {
                    //��������� �� 2 ������� - ��������� + ���
                    string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                    containerList[n] = temp[0];
                    weightList[n] = temp[1];
                    outputContainersBox.Text += containerList[n];
                    outputWeightBox.Text += Convert.ToDouble(weightList[n]) * multiplierValue;
                    
                    //��������� �� 2 ������� - ��������� + ������
                    string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                    containerList2[n] = temp2[0];
                    sealList[n] = temp2[1];
                    outputSealBox.Text += sealList[n];
                }

                //���������� ��� ������� �����������. ��� ���������� ������ ���������� ����� ������
                //������������ � ������� ������ �� ������� �� ���������� + ��� �� ������������ �������
                //else - ������ � �� �������� ������ ����������
                for (int i = 0; i < inputList.Length; i++)
                {
                    if (containerList[i] == containerList2[i])
                    {
                        (string, double, string) containerInfo = (containerList[i], Convert.ToDouble(weightList[i]) * multiplierValue, sealList[i]);
                        testBox1.Text += containerInfo;
                    }
                    else 
                    {
                        containerList2[i] = "����� ���������� �� ������, ������� �����!";
                        testBox1.Text += containerList2[i];
                    }
                ////������ ����������� � ����
                //using StreamWriter outputText = new("Result.csv", true);
                //outputText.WriteLine(containerInfo);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
        }
    }
}