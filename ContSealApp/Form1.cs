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
            outputBox.Clear();

            try
            {
                //��������� ��� ����
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                //����������� ������ �� ���� input 1 
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
                string[] inputList = inputText.Split('\n');
                string[] containerList = new string[inputList.Length];
                string[] weightList = new string[inputList.Length];

                //������ �� ���� input 2
                string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim(); // - ���������� ������ �� ����� 
                string[] inputList2 = inputText2.Split('\n');
                string[] containerList2 = new string[inputList2.Length];
                string[] sealList = new string[inputList2.Length];

                for (int n = 0; n < inputList.Length; n++)
                {
                    //��������� �� 2 ������� - ��������� + ���
                    string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                    containerList[n] = temp[0];
                    weightList[n] = temp[1];
                    
                    //��������� �� 2 ������� - ��������� + ������
                    string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                    containerList2[n] = temp2[0];
                    sealList[n] = temp2[1];
                }

                //���������� ��� ������� �����������. ��� ���������� ������ ���������� ����� ������
                //������������ � ������� ������ �� ������� �� ���������� + ��� �� ������������ �������
                //else - ������ � �� �������� ������ ����������
                for (int i = 0; i < inputList.Length; i++)
                {
                    if (containerList[i] == containerList2[i])
                    {
                        (string, double, string) containerInfo = (containerList[i], Convert.ToDouble(weightList[i]) * multiplierValue, sealList[i]);
                        outputBox.Text += containerInfo;
                        
                        //������ ����������� � ����
                        using StreamWriter outputText = new("Result.csv", true);
                        outputText.WriteLine(containerInfo);
                    }
                    else 
                    {
                        containerList2[i] = "����� ���������� �� ������, ������� �����!";
                        outputBox.Text += containerList2[i];
                        
                        //������ ����������� � ����
                        using StreamWriter outputText = new("Result.csv", true);
                        outputText.WriteLine(containerList2[i]);
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
        }
    }
}