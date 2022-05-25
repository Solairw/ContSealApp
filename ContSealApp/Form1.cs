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
            startButton.Click += StartButton_Click;
        }

        public void StartButton_Click(object? sender, EventArgs e)
        {
            outputBox.Clear();
            totalContainersBox.Clear();
            int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

            try //������� � ��������� �����
            {
                InputTextSplitToContainersAndWeightsArray(inputBox.Text);
                InputTextSplitToContainersAndSealsArray(inputBox2.Text);

                ContainerFromClient containerFromClient = new() { containerNumber = containerList[n], containerWeight = weightList[n] };
                ContainerFromFile containerFromFile = new() { containerNumber = containerList2[n], containerSeal = sealList[n] };

                if (containerFromClient.containerNumber == containerFromFile.containerNumber)
                {
                    containerFromClient.containerSeal = containerFromFile.containerSeal;
                    outputBox.Text += $"{containerFromClient.containerNumber}, {Convert.ToDouble(containerFromClient.containerWeight) * multiplierValue}, {containerFromClient.containerSeal}\n";
                }
                else if (containerFromClient.containerSeal != containerFromFile.containerSeal)
                {
                    outputBox.Text += ($"{containerFromClient.containerNumber} - ���������� �� �������!" + "\n");
                }
                totalContainersBox.Text += containerList.Length;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
        }

        public void WriteToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excel_app = new()
            {
                Visible = true
            };

            // Create new book and sheet
            Excel.Workbook workbook = excel_app.Workbooks.Add();
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets.Add();

            sheet.Cells[1, 1] = "���������";
            sheet.Cells[1, 2] = "���";
            sheet.Cells[1, 3] = "������";

            Excel.Range header_range = sheet.get_Range("A1", "C1");
            header_range.Font.Bold = true;
            header_range.Font.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Black);
            header_range.Interior.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.LightGreen);

            //for (int j = 1; j <= 100; j++)
            //{
            //    sheet.Cells[j+1, 1] = containerFromClient.containerNumber;
            //    sheet.Cells[j+1, 2] = containerFromClient.containerWeight;
            //    sheet.Cells[j+1, 3] = containerFromClient.containerSeal;
            //}
            excel_app.Quit();
            MessageBox.Show("���������!");
        }
        public void ReadFromExcel_Click(object sender, EventArgs e)
        {
            //������� �� ������� ������ ���� ������.
            testBox1.Clear();
            string path = @"C:\Users\user\source\repos\Si02Vl\ContSealApp\Report.xls";

            //��������� ���� ������
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //������ ����������.
                Excel.Application ObjExcel = new ();
                Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); //(openFileDialog1.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

                for (int i = 1; i < 52; i++) // ���������� ����� ������� �������������
                {
                    //�������� ������� ������� � ������� ����� � �����
                    Excel.Range containersRange = ObjWorkSheet.UsedRange.Columns["A"];
                    Array containersFromFileArray = (System.Array)containersRange.Value;
                    string?[] containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox1.Text += containersFromFile[i].ToString() + "\n";

                    Excel.Range sealsRange = ObjWorkSheet.UsedRange.Columns["B"];
                    Array sealsFromFileArray = (System.Array)sealsRange.Value;
                    string?[] sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox2.Text += sealsFromFile[i].ToString() + "\n";
                }
                Application.DoEvents();
                ObjExcel.Quit();
            }
        }
    }
    public class ContainerFromClient
    {
        public string? containerNumber;
        public string? containerWeight;
        public string? containerSeal;
        public static void InputTextSplitToContainersAndWeightsArray(string inputBox)
        {
            string inputText = Regex.Replace(inputBox, @"\.", ",").Trim();
            string[] inputList = inputText.Split('\n');
            string[] containersList = new string[inputList.Length];
            string[] weightsList = new string[inputList.Length];

             for (int n = 0; n < inputList.Length; n++)
             {
                 //��������� �� 2 ������� - ��������� + ���
                 string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                 containersList[n] = temp[0];
                 weightsList[n] = temp[1];
             }
        }
    }
    public class ContainerFromFile : ContainerFromClient
    {
        new public string? containerNumber;
        new public string? containerSeal;
        public static void InputTextSplitToContainersAndSealsArray(string inputBox2) 
        {
            string inputText = Regex.Replace(inputBox2, @"\.", ",").Trim();
            string[] inputList = inputText.Split('\n');
            string[] containersList = new string[inputList.Length];
            string[] sealsList = new string[inputList.Length];

            for (int n = 0; n < inputList.Length; n++)
            {
                //��������� �� 2 ������� - ��������� + ���
                string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                containersList[n] = temp[0];
                sealsList[n] = temp[1];
            }
        }      
    }
}
