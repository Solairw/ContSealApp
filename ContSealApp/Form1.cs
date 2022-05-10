using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContSealApp
{
    public partial class inputForm1 : Form
    {
        public inputForm1()
        {
            InitializeComponent();
            startButton.Click += StartButton_Click;
        }
        private void StartButton_Click(object? sender, EventArgs e)
        {
            try
            {
                string inputText = inputContBox.Text;
                string inputIndex = inputSealBox.Text;

                string[] newText = inputText.Split('\n');

                int n = Convert.ToInt32(inputIndex);
                outputTextBox.Text = newText[n - 1];
                using StreamWriter outputText = new("Result.xls", true); // запись результатов в файл
                outputText.WriteLine(newText[n - 1]);

            }
            catch (FormatException ex)
            {
                outputTextBox.Text = "Ошибка - " + ex.Message;
            }
        }
    }
}