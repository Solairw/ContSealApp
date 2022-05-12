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
            this.outputContainersBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputWeightBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.weightMultiplierValueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.testBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(13, 58);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputBox.Size = new System.Drawing.Size(232, 423);
            this.inputBox.TabIndex = 1;
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
            // outputContainersBox
            // 
            this.outputContainersBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.outputContainersBox.Location = new System.Drawing.Point(439, 58);
            this.outputContainersBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputContainersBox.Multiline = true;
            this.outputContainersBox.Name = "outputContainersBox";
            this.outputContainersBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputContainersBox.Size = new System.Drawing.Size(167, 423);
            this.outputContainersBox.TabIndex = 3;
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
            // outputWeightBox
            // 
            this.outputWeightBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.outputWeightBox.Location = new System.Drawing.Point(614, 58);
            this.outputWeightBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputWeightBox.Multiline = true;
            this.outputWeightBox.Name = "outputWeightBox";
            this.outputWeightBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputWeightBox.Size = new System.Drawing.Size(126, 423);
            this.outputWeightBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Containers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(614, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Weight";
            // 
            // weightMultiplierValueBox
            // 
            this.weightMultiplierValueBox.Location = new System.Drawing.Point(275, 191);
            this.weightMultiplierValueBox.Name = "weightMultiplierValueBox";
            this.weightMultiplierValueBox.Size = new System.Drawing.Size(144, 29);
            this.weightMultiplierValueBox.TabIndex = 11;
            this.weightMultiplierValueBox.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Weight Multiplier";
            // 
            // testBox1
            // 
            this.testBox1.Location = new System.Drawing.Point(748, 58);
            this.testBox1.Margin = new System.Windows.Forms.Padding(4);
            this.testBox1.Multiline = true;
            this.testBox1.Name = "testBox1";
            this.testBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.testBox1.Size = new System.Drawing.Size(227, 423);
            this.testBox1.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(748, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "Test Box";
            // 
            // InputForm1
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1156, 591);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.testBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.weightMultiplierValueBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputWeightBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputContainersBox);
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
        private TextBox outputContainersBox;
        private Label label1;
        private TextBox outputWeightBox;
        private Label label3;
        private Label label4;
        private TextBox weightMultiplierValueBox;
        private Label label2;
        private TextBox testBox1;
        private Label label5;
    }
}