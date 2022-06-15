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
using Microsoft.Data.SqlClient;

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
            testBox1.Clear();

            try
            {
                ReadFromExcel();
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

            List<ContainerFromClient> containersFromClientList = new();//добавляем экземпляр в класс

            for (int n = 0; n < inputList1.Length; n++)
            {
                string[] temp1 = inputList1[n].Split(new char[] { ' ', '\t' });
                containersList1[n] = temp1[0];
                weightsList[n] = temp1[1];

                ContainerFromClient containerFromClient = new(n, containersList1[n], weightsList[n]);
                containersFromClientList.Add(containerFromClient);
            }
        }
        public void ContainersComparison(int id, string numberFromClient, string numberFromFile, string weightFromClient, string sealFromFile)
        {
            
        }
        public void ReadFromExcel()
        {
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

                    ContainerFromFile containerFromFile = new(i, containersFromFile[i], sealsFromFile[i]);
                    containersFromFileList.Add(containerFromFile);
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
        private async void WriteToDB_Click(object sender, EventArgs e)
        {
        //    string connectionString = "Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        //    string connectionStringToDB = "Server=.\\SQLEXPRESS;Database=CONTAINERS_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        dbStatusBox.Text += "Connection is open";
        //        dbStatusBox.Text += $"\r\nСтрока подключения: {connection.ConnectionString} " +
        //            $"\r\nБаза данных: {connection.Database}" +
        //            $"\r\nСервер: {connection.DataSource}" +
        //            $"\r\nВерсия сервера: {connection.ServerVersion}" +
        //            $"\r\nСостояние: {connection.State}" +
        //            $"\r\nWork Station Id: {connection.WorkstationId}";
        //    }

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        try
        //        {
        //            SqlCommand createDB = new SqlCommand();
        //            createDB.CommandText = "CREATE DATABASE CONTAINERS_DATABASE";
        //            createDB.Connection = connection;
        //            await createDB.ExecuteNonQueryAsync();
        //            dbStatusBox.Text += $"\r\nБаза данных создана";
        //        }
        //        catch (SqlException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    using (SqlConnection connection = new SqlConnection(connectionStringToDB))
        //    {
        //        await connection.OpenAsync();
        //        try
        //        {
        //            SqlCommand createTable = new SqlCommand();
        //            createTable.CommandText = "CREATE TABLE Containers_From_File (Id INT IDENTITY, Number NVARCHAR(11), Weight INT, Seal NVARCHAR(10))";
        //            createTable.Connection = connection;
        //            await createTable.ExecuteNonQueryAsync();
        //            dbStatusBox.Text += $"\r\nТаблица добавлена";
        //        }
        //        catch (SqlException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        try
        //        {
        //            SqlCommand createContainer = new SqlCommand();
        //            createContainer.CommandText = "INSERT INTO Containers_From_File (Number, Seal, Weight) VALUES (, '', )";
        //            createContainer.Connection = connection;
        //            await createContainer.ExecuteNonQueryAsync();
        //            dbStatusBox.Text += $"\r\nКонтейнера добавлен";
        //        }
        //        catch(SqlException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    dbStatusBox.Text += $"\r\nConnection is closed";
        //    dbStatusBox.Text += $"\r\nProgram is closed";
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
    public class ResultContainersInfoList
    {
        public int ID;
        public string ResultContainerNumber;
        public string ResultContainerWeight;
        public string ResultContainerSeal;

        public ResultContainersInfoList(int id, string resultContainerNumber, string resultContainerWeight, string resultContainerSeal)
        {
            ID = id;
            ResultContainerNumber = resultContainerNumber;
            ResultContainerWeight = resultContainerWeight;
            ResultContainerSeal = resultContainerSeal;
        }
    }
}