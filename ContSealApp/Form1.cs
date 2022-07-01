using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Data.SqlClient;
using System.Reflection;

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
            testBox1.Clear();

            try
            {
                Test(InputTextSplitToContainerNumbersAndWeights(), GetInfoFromExcel());
            }
            catch (FormatException ex)
            {
                MessageBox.Show("������ - " + ex.Message);
            }
            totalContainersBox.Text += outputBox.Lines.Length - 1;
        }
        public List<Container> InputTextSplitToContainerNumbersAndWeights()
        {
            var inputTextFromClient = Regex.Replace(inputBox.Text, @"\.", ",").Trim();
            var inputList = inputTextFromClient.Split('\n');
            var inputContainersList = new string[inputList.Length];
            var inputWeightsList = new double[inputList.Length];

            //double[] outputWeightList = inputWeightsList.Select(s => Double.Parse(s)).ToArray();

            int weightMultiplier = int.Parse(weightMultiplierValueBox.Text);

            List<Container> containersFromClientList = new();

            for (int n = 0; n < inputList.Length; n++)
            {
                string[] temp1 = inputList[n].Split(new char[] { ' ', '\t' });
                inputContainersList[n] = temp1[0];
                inputWeightsList[n] = Double.Parse(temp1[1]) * weightMultiplier;

                Container containerFromClient = new(n, inputContainersList[n], "None", inputWeightsList[n]);
                containersFromClientList.Add(containerFromClient);
            }
            return containersFromClientList;
        }
        public List<Container> GetInfoFromExcel()
        {
            List<Container> containersFromFileList = new();

            //��������� ���� ������
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //������ ����������.
                Excel.Application objExcel = new();
                var objWorkBook = objExcel.Workbooks.Open(openFileDialog1.FileName);
                var objWorkSheet = (Excel.Worksheet)objWorkBook.Sheets[1];
                var containersRange = objWorkSheet.UsedRange.Columns["A"];
                var sealsRange = objWorkSheet.UsedRange.Columns["B"];
                
                var containersFromFileArray = (Array)containersRange.Value;
                if (containersFromFileArray == null)
                    return containersFromFileList;
                
                var containersFromFile = containersFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();
                
                var sealsFromFileArray = (Array)sealsRange.Value;
                if (sealsFromFileArray == null)
                    return containersFromFileList;
                
                var sealsFromFile = sealsFromFileArray.OfType<object>().Select(o => o.ToString()).ToArray();

                if (containersFromFileArray.Length != sealsFromFile.Length)
                    return containersFromFileList;

                // комментарий на русском
                for (int i = 0; i < containersFromFileArray.Length; i++) // ������� ����������� ����� ������� ��������������
                {
                    testBox1.Text += $"{containersFromFile[i]}\n";

                    testBox2.Text += $"{sealsFromFile[i]}\n";

                    Container containerFromFile = new(i, containersFromFile[i], sealsFromFile[i], 0.0);
                    containersFromFileList.Add(containerFromFile);
                }
                Application.DoEvents();
                objExcel.Quit();
            }
            
            return containersFromFileList;
        }
        public void Test(List<Container> containersFromClientList, List<Container> containersFromFileList)
        {
            var result = containersFromClientList.Union(containersFromFileList);
            var sortedResult = from p in result
                               orderby p.ContainerNumber
                               select p;
            outputBox.Text += sortedResult;
        }
        public void WriteToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new()
            {
                Visible = true
            };

            // Create new book and sheet
            var workBook = excelApp.Workbooks.Add();
            var workSheet = (Excel.Worksheet)workBook.Sheets.Add();

            workSheet.Cells[1, 1] = "���������";
            workSheet.Cells[1, 2] = "���";
            workSheet.Cells[1, 3] = "������";

            var headerRange = workSheet.get_Range("A1", "C1");
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
        //        dbStatusBox.Text += $"\r\n������ �����������: {connection.ConnectionString} " +
        //            $"\r\n���� ������: {connection.Database}" +
        //            $"\r\n������: {connection.DataSource}" +
        //            $"\r\n������ �������: {connection.ServerVersion}" +
        //            $"\r\n���������: {connection.State}" +
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
        //            dbStatusBox.Text += $"\r\n���� ������ �������";
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
        //            dbStatusBox.Text += $"\r\n������� ���������";
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
        //            dbStatusBox.Text += $"\r\n���������� ��������";
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
    public class Container
    {
        public int ID { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerSeal { get; set; }
        public double ContainerWeight { get; set; }

        public Container(int id, string containerNumber, string containerSeal, double containerWeight)
        {
            ID = id;
            ContainerNumber = containerNumber;
            ContainerSeal = containerSeal;
            ContainerWeight = containerWeight;
        }
    }
}
