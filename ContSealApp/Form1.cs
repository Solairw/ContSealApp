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
                MessageBox.Show("Ошибка - " + ex.Message);
            }
            totalContainersBox.Text += outputBox.Lines.Length - 1;
        }
        public void InputTextToContainersWeightsAndSeals(string inputTextFromClient, string[] inputContainersFromFile, string[] inputSealsFromFile)
        {
            inputTextFromClient = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            string[] inputList1 = inputTextFromClient.Split('\n');
            string[] containersList1 = new string[inputList1.Length];
            string[] weightsList = new string[inputList1.Length];

            int weightMultiplier = int.Parse(weightMultiplierValueBox.Text);

            List<ContainerFromClient> containersFromClientList = new();

            for (int n = 0; n < inputList1.Length; n++) //сперва сравнение сравнение номера в массиве и при совпадении - добавление пломбы и запись объекта. Или сравнение двух классов, с добавление пломбы к созданному уже объету после???
            {
                string[] temp1 = inputList1[n].Split(new char[] { ' ', '\t' });
                containersList1[n] = temp1[0];
                weightsList[n] = temp1[1];

                ContainerFromClient containerFromClientObject = new(n, containersList1[n], weightsList[n]);

                outputBox.Text += $"{containersFromClientList[n].ContainerNumber} - {Convert.ToDouble(containersFromClientList[n].ContainerWeight) * weightMultiplier}\r\n";
                //IfContainersTheSameAddSealAndShow(ContainerFromClient.ID, ContainerFromClient.ContainerNumber, ContainerFromFile.ContainerNumber, ContainerFromClient.ContainerWeight, ContainerFromFile.ContainerSeal);
            }
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
        public void ReadFromExcel()
        {
            //Очищаем от старого текста окно вывода.
            testBox1.Clear();

            //Открываем файл Экселя
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Создаём приложение.
                Excel.Application objExcel = new();
                Excel.Workbook objWorkBook = objExcel.Workbooks.Open(openFileDialog1.FileName);
                Excel.Worksheet objWorkSheet = (Excel.Worksheet)objWorkBook.Sheets[1];
                Excel.Range containersRange = objWorkSheet.UsedRange.Columns["A"];
                Excel.Range sealsRange = objWorkSheet.UsedRange.Columns["B"];

                List<ContainerFromFile> containersFromFileList = new();

                for (int i = 0; i < 50; i++) // определять длину столбца автоматически
                {
                    Array containersFromFileArray = (Array)containersRange.Value;
                    string?[] containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();
                    testBox1.Text += $"{containersFromFile[i]}\n";

                    Array sealsFromFileArray = (Array)sealsRange.Value;
                    string?[] sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();
                    testBox2.Text += $"{sealsFromFile[i]}\n";

                    ContainerFromFile containerFromFileObject = new(i, containersFromFile[i], sealsFromFile[i]);
                    containersFromFileList.Add(containerFromFileObject);
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
    }

    public class ContainerFromClient
    {
        public int ID;
        public string ContainerNumber;
        public string ContainerWeight;
        public string ContainerSeal;

        public ContainerFromClient(int id, string containerNumber, string containerWeight)
        {
            ID = id;
            ContainerNumber = containerNumber;
            ContainerWeight = containerWeight;
            ContainerSeal = "None";
        }
    }
    public class ContainerFromFile
    {
        public int ID;
        public string ContainerNumber;
        public string ContainerWeight;
        public string ContainerSeal;

        public ContainerFromFile(int id, string containerNumber, string containerSeal)
        {
            ID = id;
            ContainerNumber = containerNumber;
            ContainerWeight = "None";
            ContainerSeal = containerSeal;
        }
    }
}