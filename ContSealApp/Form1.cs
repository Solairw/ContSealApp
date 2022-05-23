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

            //Wheight Multiplier
            _ = int.Parse(weightMultiplierValueBox.Text);
            //Initial Imput Text From Client To Form
            _ = Regex.Replace(inputBox.Text, @"\.", ",").Trim();

            //try
            //{
            //    //����������� ������ �� ���� input 1 � input 2
            //    string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            //    string[] inputList = inputText.Split('\n');
            //    string[] containerList = new string[inputList.Length];           
            //    string[] weightList = new string[inputList.Length];
            //
            //    string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim();
            //    string[] inputList2 = inputText2.Split('\n');
            //    string[] containerList2 = new string[inputList2.Length];
            //    string[] sealList = new string[inputList2.Length];

            //    for (int n = 0; n < inputList.Length; n++)
            //    {
            //        //��������� �� 2 ������� - ��������� + ���
            //        string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
            //        containerList[n] = temp[0];
            //        weightList[n] = temp[1];

            //        //��������� �� 2 ������� - ��������� + ������
            //        string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
            //        containerList2[n] = temp2[0];
            //        sealList[n] = temp2[1];

            //        ContainerFromClient containerFromClient = new() { containerNumber = containerList[n], containerWeight = weightList[n] };
            //        //ContainerFromFile containerFromFile = new() { containerNumber = containerList2[n], containerSeal = sealList[n] };

            //        if (containerFromClient.containerNumber == containerFromFile.containerNumber)
            //        {
            //            containerFromClient.containerSeal = containerFromFile.containerSeal;
            //            outputBox.Text += $"{containerFromClient.containerNumber}, {Convert.ToDouble(containerFromClient.containerWeight) * multiplierValue}, {containerFromClient.containerSeal}\n";
            //        }
            //        else if (containerFromClient.containerSeal != containerFromFile.containerSeal)
            //        {
            //            outputBox.Text += ($"{containerFromClient.containerNumber} - ���������� �� �������!" + "\n");
            //        }
            //    }

            //    totalContainersBox.Text += containerList.Length;
            //}
            //catch (FormatException ex)
            //{
            //    MessageBox.Show("������ - " + ex.Message);
            //}
        }

        // Write to Excel.
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
        
        //Read from excel
        private void ReadFromExcel_Click(object sender, EventArgs e)
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

        public void InputTextSplitToContainersAndWeightArray(string inputText, string  inputBox)
        {
            inputText = Regex.Replace(inputBox, @"\.", ",").Trim();
            string[] inputList = inputText.Split('\n');
            string[] containerList = new string[inputList.Length];
            string[] weightList = new string[inputList.Length];

            for (int n = 0; n < inputList.Length; n++)
            {
                //��������� �� 2 ������� - ��������� + ���
                string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                containerList[n] = temp[0];
                weightList[n] = temp[1];
            }
        }
    }
    //public class ContainerFromFile : Container
    //{
    //    public string? containerNumber;
    //    public string? containerSeal;
    //}
}
