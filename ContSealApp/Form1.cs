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
            
            //множитель для веса
            int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

            try
            {
                //изначальные данные из окна input 1 и input 2
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
                string[] inputList = inputText.Split('\n');
                string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim();
                string[] inputList2 = inputText2.Split('\n');
                
                string[] containerList = new string[inputList.Length];
                string[] containerList2 = new string[inputList2.Length];
                string[] weightList = new string[inputList.Length];
                string[] sealList = new string[inputList2.Length];

                for (int n = 0; n < inputList.Length; n++)
                {
                    //разбиваем на 2 массива - контейнер + вес
                    string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                    containerList[n] = temp[0];
                    weightList[n] = temp[1];
                    
                    //разбиваем на 2 массива - контейнер + пломба
                    string[] temp2 = inputList2[n].Split(new char[] { ' ', '\t' });
                    containerList2[n] = temp2[0];
                    sealList[n] = temp2[1];

                    Container containerFromClient = new() { containerNumber = containerList[n], containerWeight = weightList[n] };
                    Container containerFromBase = new() { containerNumber = containerList2[n], containerSeal = sealList[n] };
                                        
                    if (containerFromClient.containerNumber == containerFromBase.containerNumber)
                    {
                        containerFromClient.containerSeal = containerFromBase.containerSeal;
                        outputBox.Text += $"{containerFromClient.containerNumber}, {Convert.ToDouble(containerFromClient.containerWeight) * multiplierValue}, {containerFromClient.containerSeal}\n";
                    }
                    else if (containerFromClient.containerSeal != containerFromBase.containerSeal)
                    {
                        outputBox.Text += ($"{containerFromClient.containerNumber} - совпадений не найдено!");
                    }
                }

                totalContainersBox.Text += containerList.Length;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
            }
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

        private void ReadFromExcel_Click(object sender, EventArgs e)
        {
            //Очищаем от старого текста окно вывода.
            testBox1.Clear();

            //Открываем файл Экселя
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Создаём приложение.
                Excel.Application ObjExcel = new ();                                                                                                                                                      
                Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(openFileDialog1.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

                for (int i = 1; i < 10; i++)
                {
                    //Выбираем область таблицы
                    Excel.Range containersRange = ObjWorkSheet.Range["A1"];
                    Excel.Range sealsRange = ObjWorkSheet.Range["B1"];
                    //Добавляем полученный текст.
                    testBox1.Text = containersRange.Text.ToString() + "\n";
                    testBox2.Text = sealsRange.Text.ToString() + "\n";

                    Application.DoEvents();
                }
                ObjExcel.Quit();
            }
        }
    }
    public class Container
    {
        public string? containerNumber;
        public string? containerWeight;
        public string? containerSeal;
    }
}
