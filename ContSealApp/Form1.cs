namespace ContSealApp
{
    public partial class inputForm1 : Form
    {
        public inputForm1()
        {
            InitializeComponent();
            startButton.Click += startButton_Click;
        }
        private void startButton_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Рандомный текст");
        }
    }
}