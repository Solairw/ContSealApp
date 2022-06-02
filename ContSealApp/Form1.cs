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
                MessageBox.Show("Ошибка - " + ex.Message);
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
                object[] ContainerFromClientList = new object[inputList1.Length];
                object[] ContainerFromFileList = new object[inputList2.Length];

                string[] temp1 = inputList1[n].Split(new char[] { ' ', '\t' });
                containersList1[n] = temp1[0];
                weightsList[n] = temp1[1];

                string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                containersList2[n] = temp2[0];
                sealsList[n] = temp2[1];

                ContainerFromClientList containerFromClient = new() { ID = n, ContainerNumber = containersList1[n], ContainerWeight = weightsList[n] };
                ContainerFromFileList containerFromFile = new() { ID = n, ContainerNumber = containersList2[n], ContainerSeal = sealsList[n] };

                IfContainersTheSameAddSealAndShow(containerFromClient.ID, containerFromClient.ContainerNumber, containerFromFile.ContainerNumber, containerFromClient.ContainerWeight, containerFromFile.ContainerSeal);
            }
            totalContainersBox.Text += containersList1.Length;
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
                outputBox.Text += $"{id} - {numberFromClient} - совпадений не найдено!\r\n";
            }
        }
        public void WriteToExcel_Click(object sender, EventArgs e) //ref ContainerFromClientList containerNumber, ref ContainerFromClientList containerWeight, ref ContainerFromClientList containerSeal)
        {
            Excel.Application excelApp = new()
            {
                Visible = true
            };

            // Create new book and sheet
            Excel.Workbook workBook = excelApp.Workbooks.Add();
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.Add();

            workSheet.Cells[1, 1] = "Контейнер";
            workSheet.Cells[1, 2] = "Вес";
            workSheet.Cells[1, 3] = "Пломба";

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
        public void ReadFromExcel_Click(object sender, EventArgs e)
        {
            //Очищаем от старого текста окно вывода.
            testBox1.Clear();
            //string path = @"C:\Users\user\source\repos\Si02Vl\ContSealApp\Report.xls";

            //Открываем файл Экселя
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Создаём приложение.
                Excel.Application objExcel = new();
                Excel.Workbook objWorkBook = objExcel.Workbooks.Open(openFileDialog1.FileName); //(openFileDialog1.FileName); //objExcel.Workbooks.Open(path)
                Excel.Worksheet objWorkSheet = (Excel.Worksheet)objWorkBook.Sheets[1];

                for (int i = 1; i < 52; i++) // определять длину столбца автоматически
                {
                    //Выбираем область таблицы и выводим текст в форму
                    Excel.Range containersRange = objWorkSheet.UsedRange.Columns["A"];
                    Array containersFromFileArray = (System.Array)containersRange.Value;
                    string?[] containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox1.Text += $"{containersFromFile[i]}\n";

                    Excel.Range sealsRange = objWorkSheet.UsedRange.Columns["B"];
                    Array sealsFromFileArray = (System.Array)sealsRange.Value;
                    string?[] sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray(); //WTF?)
                    testBox2.Text += $"{sealsFromFile[i]}\n";
                }
                Application.DoEvents();
                objExcel.Quit();
            }
        }
    }

    public class ContainerFromClientList
    {
        public int ID = 0;
        public string ContainerNumber;
        public string ContainerWeight;
        public string ContainerSeal;

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
        new public string ContainerNumber;
        new public string ContainerSeal;
    }
}
