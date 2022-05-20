using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace ContSealApp
{
    public partial class InputForm1 : Form
    {
        public InputForm1()
        {
            InitializeComponent();
            StartButton.Click += StartButton_Click;
        }

        public void StartButton_Click(object? sender, EventArgs e)
        {
            outputBox.Clear();
            totalContainersBox.Clear();

            //����������� ������ �� ���� input 1 
            //string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            //string[] inputList = inputText.Split('\n');
            //string[] containerList = new string[inputList.Length];
            //string[] weightList = new string[inputList.Length];

            //������ �� ���� input 2
            //string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim(); 
            //string[] inputList2 = inputText2.Split('\n');
            //string[] containerList2 = new string[inputList2.Length];
            //string[] sealList = new string[inputList2.Length];

            try
            {
                //��������� ��� ����
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                TextSplitToContainers(inputBox.Text);
                TextSplitToWeight(inputBox.Text);

                //for (int n = 0; n < inputList.Length; n++)
                //{
                //    //��������� �� 2 ������� - ��������� + ���
                //    //string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                //    //containerList[n] = temp[0];
                //    //weightList[n] = temp[1];

                //    //��������� �� 2 ������� - ��������� + ������
                //    string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                //    containerList2[n] = temp2[0];
                //    sealList[n] = temp2[1];
                //}

                //for (int i = 0; i < inputList.Length; i++)
                //{
                //    for (int k = 0; k < inputList2.Length; k++)
                //    {
                //        if (containerList[i] == containerList2[k])
                //        {
                //            (string, double, string) containerInfo = (containerList[i], Convert.ToDouble(weightList[i]) * multiplierValue, sealList[k]);
                //            outputBox.Text += containerInfo;

                //            string[] outputContainerList = new string[] { containerInfo.Item1 };
                //            double[] outputWeightList = new double[] { containerInfo.Item2 };
                //            string[] outputSealList = new string[] { containerInfo.Item3 };

                //            break;
                //        }
                //        else if (!containerList2.Contains(containerList[i]))
                //        {
                //            outputBox.Text += ($"{containerList[i]} - �� ������!");
                //            break;
                //        }
                //    }
                //}
                //totalContainersBox.Text += containerList.Length;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
        }

        // ������ � ����� Excel.
        public void WriteToExcel_Click(object sender, EventArgs e)
        {
            // �������� ������ ���������� Excel.
            Excel.Application excel_app = new Excel.Application();

            // ������� Excel �������
            excel_app.Visible = true;

            // ������� �����.
            Excel.Workbook workbook = excel_app.Workbooks.Add(); // � �������� ��������� ����� �������� ������

            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets.Add();

            // ��������� �������
            sheet.Cells[1, 1] = "���������";
            sheet.Cells[1, 2] = "���";
            sheet.Cells[1, 3] = "������";

            // ���� ������/����� ���������
            Excel.Range header_range = sheet.get_Range("A1", "C1");
            header_range.Font.Bold = true;
            header_range.Font.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Black);
            header_range.Interior.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.LightGreen);

            // �������� ������ � �������� �����
            for (int j = 1; j <= 0; j++)
            {
                sheet.Cells[1, j] = j;
            }

            string[,] values =
            {
                { "1", "2", "3" },
            };
            Excel.Range value_range = sheet.get_Range("A2", "C5"); //�������� �������� �� ����� �������
            value_range.Value2 = values;

            workbook.Close(true, Type.Missing, Type.Missing);
            excel_app.Quit();

            MessageBox.Show("���������!");
        }

        public string[] TextSplitToContainers(string input)
        {
            input = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            string[] inputList = input.Split('\n');
            string[] containerList = new string[inputList.Length];

            for (int n = 0; n < inputList.Length; n++)
            {
                string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                containerList[n] = temp[0];
            }
            return containerList;
        }
        public string[] TextSplitToWeight(string input)
        {
            input = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            string[] inputList = input.Split('\n');
            string[] weightList = new string[inputList.Length];

            for (int n = 0; n < inputList.Length; n++)
            {
                string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                weightList[n] = temp[1];
            }
            return weightList;
        }

        public void ContainersCompare()
        {
            for (int i = 0; i < inputList.Length; i++)
            {
                for (int k = 0; k < inputList2.Length; k++)
                {
                    if (containerList[i] == containerList2[k])
                    {
                        (string, double, string) containerInfo = (containerList[i], Convert.ToDouble(weightList[i]) * multiplierValue, sealList[k]);
                        outputBox.Text += containerInfo;

                        string[] outputContainerList = new string[] { containerInfo.Item1 };
                        double[] outputWeightList = new double[] { containerInfo.Item2 };
                        string[] outputSealList = new string[] { containerInfo.Item3 };

                        break;
                    }
                    else if (!containerList2.Contains(containerList[i]))
                    {
                        outputBox.Text += ($"{containerList[i]} - �� ������!");
                        break;
                    }
                }
            }
        }
    }
}
