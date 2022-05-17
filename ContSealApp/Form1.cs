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
                        else if (!containerList2.Contains(containerList[i])) 
                        {
                            outputBox.Text += ($"{containerList[i]} - �� ������!");
                            break;
                        }
                    }
                }
                totalContainersBox.Text += containerList.Length;//����� ����������� ��������� � ������� containerList
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
        }

        //������� ������� ���� � �������� ������.
        private static Excel.Worksheet FindSheet(Excel.Workbook workbook, string sheet_name)
        {
            foreach (Excel.Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == sheet_name) return sheet;
            }
            return null;
        }

        // ������ � ����� Excel.
        private void WriteToExcel_Click(object sender, EventArgs e)
        {
            // �������� ������ ���������� Excel.
            Excel.Application excel_app = new Excel.Application();

            // ������� Excel �������
            excel_app.Visible = true;

            // �������� �����.
            Excel.Workbook workbook = excel_app.Workbooks.Add(); // � �������� ��������� ����� �������� ������

            // C��������� �� ������� ����.
            string sheet_name = DateTime.Now.ToString("MM-dd-yy");

            Excel.Worksheet sheet = FindSheet(workbook, sheet_name);
            if (sheet == null)
            {
                // �������� ���� � �����.
                sheet = (Excel.Worksheet)workbook.Sheets.Add(
                    Type.Missing, workbook.Sheets[workbook.Sheets.Count],
                    1, Excel.XlSheetType.xlWorksheet);
                sheet.Name = DateTime.Now.ToString("MM-dd-yy");
            }

            // �������� ��������� ������ � ��������� ������.
            sheet.Cells[1, 1] = "���������";
            sheet.Cells[1, 2] = "���";
            sheet.Cells[1, 3] = "������";

            // ������ ���� �������� ����� ������ � �������.
            Excel.Range header_range = sheet.get_Range("A1", "C1");
            header_range.Font.Bold = true;
            header_range.Font.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Red);
            header_range.Interior.Color =
                System.Drawing.ColorTranslator.ToOle(
                    System.Drawing.Color.Pink);

            // �������� ��������� ������ � �������� �����.
            int[,] values =
            {
                { 2,  4,  6},
                { 3,  6,  9},
                { 4,  8, 12},
                { 5, 10, 15},
            };
            Excel.Range value_range = sheet.get_Range("A2", "C5");
            value_range.Value2 = values;

            // ��������� ��������� � �������� �����.
            workbook.Close(true, Type.Missing, Type.Missing);

            // �������� ������ Excel.
            excel_app.Quit();

            MessageBox.Show("���������!");
        }
    }
}