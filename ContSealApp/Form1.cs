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
        private void StartButton_Click(object? sender, EventArgs e)
        {
            outputBox.Clear();
            totalContainersBox.Clear();

            try
            {
                //множитель для веса
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                //изначальные данные из окна input 1 
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
                string[] inputList = inputText.Split('\n');
                string[] containerList = new string[inputList.Length];
                string[] weightList = new string[inputList.Length];

                //данные из окна input 2
                string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim(); // - прикрутить чтение из файла 
                string[] inputList2 = inputText2.Split('\n');
                string[] containerList2 = new string[inputList2.Length];
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
                }

                for (int i = 0; i < inputList.Length; i++)
                {
                    for (int k = 0; k < inputList2.Length; k++)
                    {
                        if (containerList[i] == containerList2[k])
                        {
                            (string, double, string) containerInfo = (containerList[i], Convert.ToDouble(weightList[i]) * multiplierValue, sealList[k]);
                            outputBox.Text += containerInfo;

                            //запись результатов в файл
                            using StreamWriter outputText = new("Result.csv", true);
                            outputText.WriteLine(containerInfo);
                            break;
                        }
                        else if (!containerList2.Contains(containerList[i])) 
                        {
                            outputBox.Text += ($"{containerList[i]} - не найден!");
                            break;
                        }
                    }
                }
                totalContainersBox.Text += containerList.Length;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
            }
        }

        // Запись в книгу Excel.
        private void WriteToExcel_Click(object sender, EventArgs e)
        {
            // Получить объект приложения Excel.
            Excel.Application excel_app = new Excel.Application();

            // Сделать Excel видимым
            excel_app.Visible = true;

            // Создать книгу.
            Excel.Workbook workbook = excel_app.Workbooks.Add(); // в качестве параметра можно передать шаблон

            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets.Add();

            // Добавить некоторые данные в отдельные ячейки.
            sheet.Cells[1, 1] = "Контейнер";
            sheet.Cells[1, 2] = "Вес";
            sheet.Cells[1, 3] = "Пломба";

            // Делаем этот диапазон ячеек жирным и красным.
            Excel.Range header_range = sheet.get_Range("A1", "C1");
            header_range.Font.Bold = true;
            header_range.Font.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Black);
            header_range.Interior.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.LightGreen);

            // Добавьте некоторые данные в диапазон ячеек.
            int[,] values =
            {
                { 2,  4,  6},
                { 3,  6,  9},
                { 4,  8, 12},
                { 5, 10, 15},
            };
            Excel.Range value_range = sheet.get_Range("A2", "C5");
            value_range.Value2 = values;

            // Сохраните изменения и закройте книгу.
            workbook.Close(true, Type.Missing, Type.Missing);
            excel_app.Quit();

            MessageBox.Show("Выполнено!");
        }
    }
}