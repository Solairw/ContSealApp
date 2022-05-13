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
                //��������� ��� ����
                int multiplierValue = int.Parse(weightMultiplierValueBox.Text);

                //����������� ������ �� ���� input 1 
                string inputText = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
                string[] inputList = inputText.Split('\n');
                string[] containerList = new string[inputList.Length];
                string[] weightList = new string[inputList.Length];

                //������ �� ���� input 2
                string inputText2 = Regex.Replace(inputBox2.Text, @"\.", ",").Trim(); // - ���������� ������ �� ����� 
                string[] inputList2 = inputText2.Split('\n');
                string[] containerList2 = new string[inputList2.Length];
                string[] sealList = new string[inputList2.Length];

                for (int n = 0; n < inputList.Length; n++)
                {
                    //��������� �� 2 ������� - ��������� + ���
                    string[] temp = inputList[n].Split(new char[] { ' ', '\t' });
                    containerList[n] = temp[0];
                    weightList[n] = temp[1];

                    //��������� �� 2 ������� - ��������� + ������
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

                            //������ ����������� � ����
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
                MessageBox.Show("������ - " + ex.Message);
            }
        }

        // ������ �� ����� Excel.
        //static void Excel(string[] args)
        //{
        //    Excel.Application ObjWorkExcel = new Excel.Application(); //������� ������
        //    Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(@"d:\kursy\C#\Spiski\Spisok.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //������� ����
        //    Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //�������� 1 ����
        //    var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 ������
        //    string[,] list = new string[lastCell.Column, lastCell.Row]; // ������ �������� � ����� ����� �� ������� �����
        //    for (int i = 0; i < (int)lastCell.Column; i++) //�� ���� ��������
        //        for (int j = 0; j < (int)lastCell.Row; j++) // �� ���� �������
        //            list[i, j] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();//��������� ����� � ������
        //    ObjWorkBook.Close(false, Type.Missing, Type.Missing); //������� �� ��������
        //    ObjWorkExcel.Quit(); // ����� �� ������
        //    GC.Collect(); // ������ �� �����
        //    for (int i = 1; i < (int)lastCell.Column; i++) //�� ���� ��������
        //        for (int j = 1; j < (int)lastCell.Row; j++) // �� ���� ������� 
        //            Console.Write(list[i, j]);//������� ������
        //    Console.ReadLine();
        //}
    }
}