using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using Excel = Microsoft.Office.Interop.Excel;

namespace ContSealApp
{
    public partial class InputForm1 : Form
    {
        public InputForm1()
        {
            InitializeComponent();
            startButton.Click += StartButton_Click;
        }
        private void StartButton_Click(object? sender, EventArgs e)
        {
            outputBox.Clear();

            try
            {
                //множитель дл€ веса
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
                    }
                }
                totalContainersBox.Text += 
            }
            catch (FormatException ex)
            {
                MessageBox.Show("ќшибка - " + ex.Message);
            }
        }

        // „тение из книги Excel.
        //static void Excel(string[] args)
        //{
        //    Excel.Application ObjWorkExcel = new Excel.Application(); //открыть эксель
        //    Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(@"d:\kursy\C#\Spiski\Spisok.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //открыть файл
        //    Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1 лист
        //    var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 €чейку
        //    string[,] list = new string[lastCell.Column, lastCell.Row]; // массив значений с листа равен по размеру листу
        //    for (int i = 0; i < (int)lastCell.Column; i++) //по всем колонкам
        //        for (int j = 0; j < (int)lastCell.Row; j++) // по всем строкам
        //            list[i, j] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();//считываем текст в строку
        //    ObjWorkBook.Close(false, Type.Missing, Type.Missing); //закрыть не сохран€€
        //    ObjWorkExcel.Quit(); // выйти из эксел€
        //    GC.Collect(); // убрать за собой
        //    for (int i = 1; i < (int)lastCell.Column; i++) //по всем колонкам
        //        for (int j = 1; j < (int)lastCell.Row; j++) // по всем строкам 
        //            Console.Write(list[i, j]);//выводим строку
        //    Console.ReadLine();
        //}
    }
}