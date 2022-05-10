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
                string inputText = inputContBox.Text; // разбить на строки
                string[] newText = inputText.Split('\n');
                outputTextBox.Text = newText[2];
            }
            catch (FormatException ex)
            {
                outputTextBox.Text = "Ошибка - " + ex.Message;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}