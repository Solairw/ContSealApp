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

                ContainerFromClient containerFromClient = new() { containerNumber = containersList1[n], containerWeight = weightsList[n] };
                ContainerFromFile containerFromFile = new() { containerNumber = containersList2[n], containerSeal = sealsList[n] };

                IfContainersTheSameAddSealAndShow(containerFromClient.containerNumber, containerFromFile.containerNumber, containerFromClient.containerWeight, containerFromFile.containerSeal);
            }
            totalContainersBox.Text += containersList1.Length;
        }
        public void IfContainersTheSameAddSealAndShow(string fromClient, string fromFile, string weight, string seal)
        {
            int weightMultiplier = int.Parse(weightMultiplierValueBox.Text);
            if (fromClient == fromFile)
            {
                fromClient = fromFile;
                outputBox.Text += $"{fromClient}, {Convert.ToDouble(weight) * weightMultiplier} , {seal}\n";
            }
            else if (fromClient != fromFile)
            {
                outputBox.Text += $"{fromClient} - совпадений не найдено!\n";
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

            sheet.Cells[1, 1] = "Контейнер";
            sheet.Cells[1, 2] = "Вес";
            sheet.Cells[1, 3] = "Пломба";

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
            MessageBox.Show("Выполнено!");
        }
        public void ReadFromExcel_Click(object sender, EventArgs e)
        {
            //Очищаем от старого текста окно вывода.
            testBox1.Clear();
            string path = @"C:\Users\user\source\repos\Si02Vl\ContSealApp\Report.xls";

            //Открываем файл Экселя
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Создаём приложение.
                Excel.Application ObjExcel = new();
                Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); //(openFileDialog1.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

                for (int i = 1; i < 52; i++) // определять длину столбца автоматически
                {
                    //Выбираем область таблицы и выводим текст в форму
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

    public class ContainerFromClient
    {
        public string containerNumber;
        public string containerWeight;
        public string containerSeal;
    }
    public class ContainerFromFile : ContainerFromClient
    {
        new public string containerNumber;
        new public string containerSeal;
    }
}
