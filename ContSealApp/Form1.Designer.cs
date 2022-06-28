namespace ContSealApp
{
    partial class InputForm1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.weightMultiplierValueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.totalContainersBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.writeToExcel = new System.Windows.Forms.Button();
            this.testBox1 = new System.Windows.Forms.TextBox();
            this.testBox2 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.WriteToDB = new System.Windows.Forms.Button();
            this.dbStatusBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.AcceptsReturn = true;
            this.inputBox.Location = new System.Drawing.Point(13, 58);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputBox.Size = new System.Drawing.Size(215, 467);
            this.inputBox.TabIndex = 1;
            this.inputBox.Text = "TRHU1760959 22.4\r\nTCLU3172470 22.34\r\nFESU2109587 22.35\r\nFICU3552444 20.55";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(13, 533);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(232, 45);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input";
            // 
            // weightMultiplierValueBox
            // 
            this.weightMultiplierValueBox.Location = new System.Drawing.Point(236, 178);
            this.weightMultiplierValueBox.Name = "weightMultiplierValueBox";
            this.weightMultiplierValueBox.Size = new System.Drawing.Size(106, 29);
            this.weightMultiplierValueBox.TabIndex = 11;
            this.weightMultiplierValueBox.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(236, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Weight Multiplier";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(351, 58);
            this.outputBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputBox.Size = new System.Drawing.Size(361, 469);
            this.outputBox.TabIndex = 13;
            this.outputBox.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "Cntr + Weight + Seal";
            // 
            // totalContainersBox
            // 
            this.totalContainersBox.Location = new System.Drawing.Point(645, 542);
            this.totalContainersBox.Name = "totalContainersBox";
            this.totalContainersBox.Size = new System.Drawing.Size(67, 29);
            this.totalContainersBox.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 545);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "Total";
            // 
            // writeToExcel
            // 
            this.writeToExcel.Location = new System.Drawing.Point(350, 533);
            this.writeToExcel.Name = "writeToExcel";
            this.writeToExcel.Size = new System.Drawing.Size(228, 45);
            this.writeToExcel.TabIndex = 22;
            this.writeToExcel.Text = "Write to File";
            this.writeToExcel.UseVisualStyleBackColor = true;
            this.writeToExcel.Click += new System.EventHandler(this.WriteToExcel_Click);
            // 
            // testBox1
            // 
            this.testBox1.Location = new System.Drawing.Point(725, 58);
            this.testBox1.Multiline = true;
            this.testBox1.Name = "testBox1";
            this.testBox1.Size = new System.Drawing.Size(149, 467);
            this.testBox1.TabIndex = 23;
            // 
            // testBox2
            // 
            this.testBox2.Location = new System.Drawing.Point(880, 58);
            this.testBox2.Multiline = true;
            this.testBox2.Name = "testBox2";
            this.testBox2.Size = new System.Drawing.Size(149, 469);
            this.testBox2.TabIndex = 24;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // WriteToDB
            // 
            this.WriteToDB.Location = new System.Drawing.Point(1040, 533);
            this.WriteToDB.Name = "WriteToDB";
            this.WriteToDB.Size = new System.Drawing.Size(354, 46);
            this.WriteToDB.TabIndex = 25;
            this.WriteToDB.Text = "Write to DB";
            this.WriteToDB.UseVisualStyleBackColor = true;
            this.WriteToDB.Click += new System.EventHandler(this.WriteToDB_Click);
            // 
            // dbStatusBox
            // 
            this.dbStatusBox.Location = new System.Drawing.Point(1040, 58);
            this.dbStatusBox.Multiline = true;
            this.dbStatusBox.Name = "dbStatusBox";
            this.dbStatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dbStatusBox.Size = new System.Drawing.Size(354, 469);
            this.dbStatusBox.TabIndex = 26;
            // 
            // InputForm1
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1422, 591);
            this.Controls.Add(this.dbStatusBox);
            this.Controls.Add(this.WriteToDB);
            this.Controls.Add(this.testBox2);
            this.Controls.Add(this.testBox1);
            this.Controls.Add(this.writeToExcel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalContainersBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.weightMultiplierValueBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.inputBox);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ContTextApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox inputBox;
        private Button startButton;
        private Label label1;
        private TextBox weightMultiplierValueBox;
        private Label label2;
        private Label label5;
        private TextBox totalContainersBox;
        private Label label3;
        private Button writeToExcel;
        private TextBox testBox1;
        private TextBox testBox2;
        private OpenFileDialog openFileDialog1;
        public TextBox outputBox;
        private Button WriteToDB;
        private TextBox dbStatusBox;
    }
}