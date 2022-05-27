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

            try
            {
                InputTextToContainersWeightsAndSeals(inputBox.Text, inputBox2.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
            //totalContainersBox.Text += outputBox.Lines.Count();
        }
        public void InputTextToContainersWeightsAndSeals(string inputTextFromClient, string inputTextFromFile)
        {
            inputTextFromClient = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            inputTextFromFile = Regex.Replace(inputBox2.Text, @"\.", ",").Trim();

            string[] inputList1 = inputTextFromClient.Split('\n');
            string[] inputList2 = inputTextFromFile.Split('\n');

            string[] containersList1 = new string[inputList1.Length];
            string[] containersList2 = new string[inputList2.Length];

            string[] weightsList = new string[inputList1.Length];
            string[] sealsList = new string[inputList2.Length];

            for (int n = 0; n < inputList1.Length; n++)
            {
                string[] temp1 = inputList1[n].Split(new char[] { ' ', '\t' });
                containersList1[n] = temp1[0];
                weightsList[n] = temp1[1];

                string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                containersList2[n] = temp2[0];
                sealsList[n] = temp2[1];

                ContainerFromClientList containerFromClient = new() { ID = n + 1, containerNumber = containersList1[n], containerWeight = weightsList[n] };
                ContainerFromFileList containerFromFile = new() { ID = n + 1, containerNumber = containersList2[n], containerSeal = sealsList[n] };

                IfContainersTheSameAddSealAndShow(containerFromClient.ID, containerFromClient.containerNumber, containerFromFile.containerNumber, containerFromClient.containerWeight, containerFromFile.containerSeal);
            }
            totalContainersBox.Text += containersList1.Length;
        }
        public void IfContainersTheSameAddSealAndShow(int id, string fromClient, string fromFile, string weight, string seal)
        {
            int weightMultiplier = int.Parse(weightMultiplierValueBox.Text);
            if (fromClient == fromFile)
            {
                fromClient = fromFile;
                outputBox.Text += $"{id} - {fromClient}, {Convert.ToDouble(weight) * weightMultiplier} , {seal}\r\n";
            }
            else if (fromClient != fromFile)
            {
                outputBox.Text += $"{id} - {fromClient} - ���������� �� �������!\r\n";
            }
        }
        public void WriteToExcel_Click(object sender, EventArgs e) //ref ContainerFromClientList containerNumber, ref ContainerFromClientList containerWeight, ref ContainerFromClientList containerSeal)
        {
            Excel.Application excel_app = new()
            {
                Visible = true
            };

            // Create new book and sheet
            Excel.Workbook workBook = excel_app.Workbooks.Add();
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.Add();

            workSheet.Cells[1, 1] = "���������";
            workSheet.Cells[1, 2] = "���";
            workSheet.Cells[1, 3] = "������";

            Excel.Range header_range = workSheet.get_Range("A1", "C1");
            header_range.Font.Bold = true;
            header_range.Font.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Black);
            header_range.Interior.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.LightGreen);
            
            //for (int j = 1; j <= 50; j++)
            //{
            //    workSheet.Cells[j + 1, 1] = ref ;
            //    workSheet.Cells[j + 1, 2] = containerWeight;
            //    workSheet.Cells[j + 1, 3] = containerSeal;
            //}

            excel_app.Quit();
            MessageBox.Show("Done!");
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
                Excel.Application ObjExcel = new();
                Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); //(openFileDialog1.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

                for (int i = 1; i < 52; i++) // ���������� ����� ������� �������������
                {
                    //�������� ������� ������� � ������� ����� � �����
                    Excel.Range containersRange = ObjWorkSheet.UsedRange.Columns["A"];
                    Array containersFromFileArray = (System.Array)containersRange.Value;
                    string?[] containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox1.Text += $"{containersFromFile[i]}\n";

                    Excel.Range sealsRange = ObjWorkSheet.UsedRange.Columns["B"];
                    Array sealsFromFileArray = (System.Array)sealsRange.Value;
                    string?[] sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox2.Text += $"{sealsFromFile[i]}\n";
                }
                Application.DoEvents();
                ObjExcel.Quit();
            }
        }
    }

    public class ContainerFromClientList
    {
        public int ID = 0;
        public string containerNumber;
        public string containerWeight;
        public string containerSeal;
    }
    public class ContainerFromFileList : ContainerFromClientList
    {
        new public string containerNumber;
        new public string containerSeal;
    }
}
