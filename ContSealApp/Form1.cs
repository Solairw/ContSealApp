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
        string[] containersFromFile;
        string[] sealsFromFile;

        public InputForm1()
        {
            InitializeComponent();
            startButton.Click += StartButton_Click;
        }
        public void StartButton_Click(object? sender, EventArgs e)
        {
            outputBox.Clear();
            totalContainersBox.Clear();
            ReadFromExcel();

            try
            {
                InputTextToContainersWeightsAndSeals(inputBox.Text, containersFromFile, sealsFromFile);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
            totalContainersBox.Text += outputBox.Lines.Count();
        }
        public void InputTextToContainersWeightsAndSeals(string inputTextFromClient, string[] inputContainersFromFile, string[] inputSealsFromFile)
        {
            inputTextFromClient = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            string[] inputList1 = inputTextFromClient.Split('\n');
            string[] containersList1 = new string[inputList1.Length];
            string[] weightsList = new string[inputList1.Length];

            for (int n = 0; n < inputList1.Length; n++)
            {
                string[] temp1 = inputList1[n].Split(new char[] { ' ', '\t' });
                containersList1[n] = temp1[0];
                weightsList[n] = temp1[1];

                ContainerFromClientList containerFromClient = new() { ID = n, ContainerNumber = containersList1[n], ContainerWeight = weightsList[n] };
                ContainerFromFileList containerFromFile = new() { ID = n, ContainerNumber = inputContainersFromFile[n], ContainerSeal = inputSealsFromFile[n] };

                IfContainersTheSameAddSealAndShow(containerFromClient.ID, containerFromClient.ContainerNumber, containerFromFile.ContainerNumber, containerFromClient.ContainerWeight, containerFromFile.ContainerSeal);
            }
            //totalContainersBox.Text += containersList1.Length;
        }
        public void IfContainersTheSameAddSealAndShow(int id, string numberFromClient, string numberFromFile, string weight, string seal)
        {
            int weightMultiplier = int.Parse(weightMultiplierValueBox.Text);
            if (numberFromClient == numberFromFile)
            {
                numberFromClient = numberFromFile;
                outputBox.Text += $"{id} - {numberFromClient}, {Convert.ToDouble(weight) * weightMultiplier} , {seal}\r\n";
            }
            else if (numberFromClient != numberFromFile)
            {
                outputBox.Text += $"{id} - {numberFromClient} - ���������� �� �������!\r\n";
            }
        }
        public void ReadFromExcel()
        {
            //������� �� ������� ������ ���� ������.
            testBox1.Clear();

            //��������� ���� ������
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //������ ����������.
                Excel.Application objExcel = new();
                Excel.Workbook objWorkBook = objExcel.Workbooks.Open(openFileDialog1.FileName);
                Excel.Worksheet objWorkSheet = (Excel.Worksheet)objWorkBook.Sheets[1];

                for (int i = 1; i < 52; i++) // ���������� ����� ������� �������������
                {

                    Excel.Range containersRange = objWorkSheet.UsedRange.Columns["A"];
                    Array containersFromFileArray = (System.Array)containersRange.Value;
                    string?[] containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();
                    //testBox1.Text += $"{containersFromFile[i]}\n";

                    Excel.Range sealsRange = objWorkSheet.UsedRange.Columns["B"];
                    Array sealsFromFileArray = (System.Array)sealsRange.Value;
                    string?[] sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();
                    //testBox2.Text += $"{sealsFromFile[i]}\n";
                }
                Application.DoEvents();
                objExcel.Quit();
            }
        }
        public void WriteToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new()
            {
                Visible = true
            };

            // Create new book and sheet
            Excel.Workbook workBook = excelApp.Workbooks.Add();
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.Add();

            workSheet.Cells[1, 1] = "���������";
            workSheet.Cells[1, 2] = "���";
            workSheet.Cells[1, 3] = "������";

            Excel.Range headerRange = workSheet.get_Range("A1", "C1");
            headerRange.Font.Bold = true;
            headerRange.Font.Color = ColorTranslator.ToOle(Color.Black);
            headerRange.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);

            //for (int j = 1; j <= 50; j++)
            //{
            //    workSheet.Cells[j + 1, 1] = ref ;
            //    workSheet.Cells[j + 1, 2] = containerWeight;
            //    workSheet.Cells[j + 1, 3] = containerSeal;
            //}

            excelApp.Quit();
            MessageBox.Show("Done!");
        }
    }

    public class ContainerFromClientList
    {
        public int ID = 0;
        public string? ContainerNumber;
        public string? ContainerWeight;
        public string? ContainerSeal;

        public void Container(int id, string containerNumber, string containerWeight, string containerSeal)
        {
            ID = id;
            ContainerNumber = containerNumber;
            ContainerWeight = containerWeight;
            ContainerSeal = containerSeal;
        }
    }
    public class ContainerFromFileList : ContainerFromClientList
    {
        new public string? ContainerNumber;
        new public string? ContainerSeal;
    }
}